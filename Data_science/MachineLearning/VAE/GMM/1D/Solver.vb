﻿Imports System.IO
Imports Microsoft.VisualBasic.DataMining.KMeans
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.MachineLearning.VariationalAutoencoder.GMM.EMGaussianMixtureModel
Imports std = System.Math

Namespace GMM

    ''' <summary>
    ''' GMM problem solver
    ''' </summary>
    Public Class Solver

        Public Shared Function Predicts(ds As IEnumerable(Of ClusterEntity),
                                        Optional components As Integer = 3,
                                        Optional threshold As Double = 0.00000001,
                                        Optional max_iteration As Integer = 1000,
                                        Optional strict As Boolean = True,
                                        Optional abs As Boolean = False) As GaussianMixtureModel

            Dim matrix = ds.ToArray
            Dim gmm As New GaussianMixtureModel(matrix, strict, abs)
            Dim componentList As ClusterEntity() = matrix.Shuffles.Take(components).ToArray

            Return Training(gmm, componentList, max_iteration, threshold)
        End Function

        Private Shared Function Training(mix As GaussianMixtureModel,
                                         componentList As ClusterEntity(),
                                         max_iteration As Integer,
                                         threshold As Double) As GaussianMixtureModel

            Return mix.FitGMM(
                maxIterations:=max_iteration,
                convergenceCriteria:=threshold,
                estimatedCompCenters:=componentList.Select(Function(c) c.entityVector).AsList
            )
        End Function

        Private Shared Function Training(mix As Mixture, threshold As Double, verbose As Boolean) As Mixture
            Dim dev As TextWriter = App.StdOut

            If verbose Then
                Call mix.printStats(dev)
            End If

            Dim oldLog As Double = mix.logLike()
            Dim newLog = oldLog - 100.0
            Dim i As i32 = 1

            Do
                oldLog = newLog
                mix.Expectation()
                mix.Maximization()
                newLog = mix.logLike()

                If verbose Then
                    Call dev.WriteLine($" [{vbTab}{++i}]{vbTab}new-loglike: {newLog}")
                    Call dev.Flush()
                End If
            Loop While newLog <> 0 AndAlso std.Abs(newLog - oldLog) > threshold

            If verbose Then
                Call mix.printStats(dev)
            End If

            Return mix
        End Function

        Public Shared Function Predicts(x As IEnumerable(Of Double),
                                        Optional components As Integer = 3,
                                        Optional threshold As Double = 0.00000001,
                                        Optional verbose As Boolean = False) As Mixture

            Return Training(New Mixture(New DatumList(x, components)), threshold, verbose)
        End Function
    End Class
End Namespace