﻿#Region "Microsoft.VisualBasic::facb61ba50747eb1b37f5551b426d508, Data_science\Mathematica\Math\Math.Statistics\HypothesisTesting\T-test\TestResult.vb"

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

    '     Class TtestResult
    ' 
    '         Properties: alpha, Alternative, DegreeFreedom, Mean, Pvalue
    '                     StdErr, TestValue
    ' 
    '         Function: ToString, Valid
    ' 
    '     Class TwoSampleResult
    ' 
    '         Properties: MeanX, MeanY
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Hypothesis

    Public Class TtestResult

        ''' <summary>
        ''' the degrees of freedom for the t-statistic.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Welch–Satterthwaite equation 的计算结果为小数</remarks>
        Public Property DegreeFreedom As Double
        ''' <summary>
        ''' the p-value For the test.
        ''' </summary>
        ''' <returns></returns>
        Public Property Pvalue As Double
        ''' <summary>
        ''' the value of the t-statistic.
        ''' </summary>
        ''' <returns></returns>
        Public Property TestValue As Double
        ''' <summary>
        ''' the alternative hypothesis.
        ''' </summary>
        ''' <returns></returns>
        Public Property Alternative As Hypothesis
        Public Property alpha As Double
        ''' <summary>
        ''' Sample mean
        ''' </summary>
        ''' <returns></returns>
        Public Property Mean As Double
        Public Property StdErr As Double

        ''' <summary>
        ''' Alternative hypothesis result
        ''' </summary>
        ''' <returns></returns>
        Public Function Valid() As Boolean
            Return Pvalue >= alpha
        End Function

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class

    Public Class TwoSampleResult : Inherits TtestResult

        Public Property MeanX As Double
        Public Property MeanY As Double

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
