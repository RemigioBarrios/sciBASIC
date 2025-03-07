﻿Imports Microsoft.VisualBasic.Language.Java
Imports Microsoft.VisualBasic.MachineLearning.CNN.data
Imports std = System.Math

Namespace CNN.trainers

    ''' <summary>
    ''' Adaptive delta will look at the differences between the expected result and the current result to train the network.
    ''' 
    ''' @author Daniel Persson (mailto.woden@gmail.com)
    ''' </summary>
    Public Class AdaDeltaTrainer : Inherits TrainerAlgorithm

        Dim ro As Double = 0.95

        Public Sub New(batch_size As Integer, l2_decay As Single, Optional ro As Double = 0.95)
            MyBase.New(batch_size, l2_decay)
            Me.ro = ro
        End Sub

        Public Overrides Sub initTrainData(bpr As BackPropResult)
            Dim newXSumArr = New Double(bpr.Weights.Length - 1) {}

            Call newXSumArr.fill(0)
            Call xsum.Add(newXSumArr)
        End Sub

        Public Overrides Sub update(i As Integer, j As Integer, gij As Double, p As Double())
            Dim gsumi = gsum(i)
            Dim xsumi = xsum(i)
            Dim dx As Double

            gsumi(j) = ro * gsumi(j) + (1 - ro) * gij * gij
            dx = -std.Sqrt((xsumi(j) + eps) / (gsumi(j) + eps)) * gij
            ' yes, xsum lags behind gsum by 1.
            xsumi(j) = ro * xsumi(j) + (1 - ro) * dx * dx
            p(j) += dx
        End Sub

        Public Overrides Function ToString() As String
            Return $"ada_delta(ro:{ro})"
        End Function
    End Class

End Namespace
