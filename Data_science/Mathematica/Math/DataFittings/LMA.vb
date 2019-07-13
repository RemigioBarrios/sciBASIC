﻿#Region "Microsoft.VisualBasic::c9911782e1be95ca40544e717be6f26a, Data_science\Mathematica\Math\DataFittings\LMA.vb"

    ' Author:
    ' 
    ' 
    ' 
    ' 
    ' 
    ' 
    ' 



    ' /********************************************************************************/

    ' Summaries:

    ' Module LMA
    ' 
    '     Function: NonLinearFit
    '     Structure FitInput
    ' 
    '         Function: ToString
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Serialization.JSON

''' <summary>
''' ### Levenberg–Marquardt algorithm
''' 
''' In mathematics and computing, the Levenberg–Marquardt algorithm 
''' (LMA or just LM), also known as the damped least-squares (DLS) 
''' method, is used to solve non-linear least squares problems. 
''' 
''' These minimization problems arise especially in least squares 
''' curve fitting.
''' </summary>
Public Module LMA

    Public Structure FitInput
        ''' <summary>
        ''' The matrix row
        ''' </summary>
        Dim factors As Dictionary(Of String, Double)
        ''' <summary>
        ''' The non-linear function result output 
        ''' </summary>
        Dim y As Double

        Public Overrides Function ToString() As String
            Return $"f(X) = {y} = f({factors.GetJson})"
        End Function
    End Structure

    <Extension>
    Public Function NonLinearFit(matrix As IEnumerable(Of FitInput), Optional iterations% = 2000)

        Throw New NotImplementedException
    End Function
End Module
