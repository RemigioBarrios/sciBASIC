﻿Imports System.IO
Imports std = System.Math

Namespace GMM

    ''' <summary>
    ''' Gaussian Mixture Model Unsupervised Clustering
    ''' </summary>
    ''' <remarks>
    ''' https://github.com/thearrow/ai-GMM/tree/master
    ''' </remarks>
    Public Class Mixture

        Public ReadOnly Property components As Component()
        Public ReadOnly Property data As DatumList

        Public Sub New(data As DatumList)
            Dim div As Double = 1.0 / data.components

            Me.data = data
            Me.components = New Component(Me.data.components() - 1) {}

            Dim mean = data.Mean / Me.data.components
            Dim stdev = data.Stdev

            ' random initialization of component parameters
            For i As Integer = 0 To Me.data.components() - 1
                components(i) = New Component(div, i * mean, 1)
            Next
        End Sub

        Public Overridable Sub Expectation()
            For i As Integer = 0 To data.size() - 1
                Dim probs As Double() = New Double(data.components() - 1) {}
                For j As Integer = 0 To components.Length - 1
                    Dim c = components(j)
                    Dim p = gaussian(data.get(i).val, c.Mean, c.Stdev) * c.Weight

                    If p < 0 Then
                        probs(j) = 0
                    Else
                        probs(j) = p
                    End If
                Next

                'alpha normalize and set probs
                Dim sum As Double = probs.Sum

                For j As Integer = 0 To probs.Length - 1
                    Dim normProb = If(sum = 0, 0.000001, probs(j) / sum)
                    data.get(i).setProb(j, normProb)
                Next
            Next
        End Sub

        Public Overridable Sub Maximization()
            Dim newMean = 0.0
            Dim newStdev = 0.0
            Dim dj As Datum
            Dim psi As Double

            For i = 0 To components.Length - 1
                'MEAN
                For j = 0 To data.size() - 1
                    dj = data.get(j)
                    newMean += dj.getProb(i) * dj.val
                Next

                psi = data.nI(i)

                If psi = 0.0 Then
                    psi = 1.0E-50
                End If

                newMean /= psi
                components(i).Mean = newMean

                'STDEV
                For j = 0 To data.size() - 1
                    dj = data.get(j)
                    newStdev += dj.getProb(i) * std.Pow((dj.val - newMean), 2)
                Next
                newStdev /= psi
                newStdev = std.Sqrt(newStdev)
                components(i).Stdev = newStdev

                'WEIGHT
                components(i).Weight = psi / data.size()
            Next

        End Sub

        Public Overridable Function logLike() As Double
            Dim lLoglike = 0.0
            For i = 0 To data.size() - 1
                Dim sum = 0.0
                For j = 0 To components.Length - 1
                    Dim c = components(j)
                    Dim prob = data.get(i).getProb(j)
                    Dim val As Double = data.get(i).val
                    Dim gauss = gaussian(val, c.Mean, c.Stdev)
                    If gauss <= 0 Then
                        gauss = 1
                    End If
                    Dim inner = std.Log(gauss) + std.Log(c.Weight)
                    If Double.IsInfinity(inner) OrElse Double.IsNaN(inner) Then
                        Return 0
                    End If
                    sum += prob * inner
                Next
                lLoglike += sum
            Next
            Return lLoglike
        End Function

        Public Overridable Sub printStats(Optional dev As TextWriter = Nothing)
            dev = dev Or App.StdOut

            For Each c As Component In components
                Call dev.WriteLine($"C - mean: {c.Mean}{vbTab}stdev: {c.Stdev}{vbTab}weight: {c.Weight}")
            Next

            dev.Flush()
        End Sub

        ' 
        ' 	    The following two methods courtesy of Robert Sedgewick:
        ' 	    http://introcs.cs.princeton.edu/java/22library/Gaussian.java.html
        ' 	    Used to calculate the PDF of a gaussian distribution with mean=mu, stddev=sigma
        ' 	 
        Public Overridable Function standardGaussian(x As Double) As Double
            Return std.Exp(-x * x / 2) / std.Sqrt(2 * std.PI)
        End Function

        Public Overridable Function gaussian(x As Double, mu As Double, sigma As Double) As Double
            Return standardGaussian((x - mu) / sigma) / sigma
        End Function

    End Class
End Namespace