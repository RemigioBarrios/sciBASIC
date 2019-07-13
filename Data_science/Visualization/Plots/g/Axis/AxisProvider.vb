﻿#Region "Microsoft.VisualBasic::b0e77ee920d5056fb476e96a75cd4343, Data_science\Visualization\Plots\g\Axis\AxisProvider.vb"

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

    '     Class AxisProvider
    ' 
    '         Properties: n, Range, Tick
    ' 
    '         Constructor: (+2 Overloads) Sub New
    '         Function: AxisTicks, ToString, TryParse
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Scripting.Runtime
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Graphic.Axis

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' ```vbnet
    ''' Dim axis$ = "(min,max),tick=steps,n=parts"
    ''' 
    ''' ' example as
    ''' Dim axis$ = "(0,100),tick=10"
    ''' Dim axis$ = "(100,1000),n=10"
    ''' ```
    ''' </remarks>
    Public Class AxisProvider

        Public Property Range As DoubleRange
        Public Property n As Integer
        Public Property Tick As Double

        Sub New()
        End Sub

        Sub New(data#())
            Range = New DoubleRange(data.Min, data.Max)
            Tick = data(1) - data(0)
        End Sub

        Public Function AxisTicks() As Double()
            If n = 0 Then
                Return Range.GetAxisByTick(Tick)
            Else
                Return Range.GetAxisByTick(tick:=Range.Length / n)
            End If
        End Function

        Public Overrides Function ToString() As String
            Return AxisTicks().GetJson
        End Function

        ''' <summary>
        ''' (min,max), tick=xxx, n=xxx
        ''' </summary>
        ''' <param name="s"></param>
        ''' <returns></returns>
        Public Shared Function TryParse(s As String) As AxisProvider
            Dim range As String = Regex.Match(s, "\(.+?\)").Value
            Dim tick$ = Regex.Match(s, "tick=[^,]+", RegexICSng).Value
            Dim n$ = Regex.Match(s, "n=[^,]+", RegexICSng).Value

            If range.StringEmpty Then
                ' 默认值
                range = s
                tick = -1
                n = 10
            Else
                n = n.GetTagValue("=", trim:=True).Value
                tick = tick.GetTagValue("=", trim:=True).Value
            End If

            Return New AxisProvider With {
                .n = n.ParseInteger,
                .Range = DoubleRange.op_Implicit(range),
                .Tick = tick.ParseNumeric
            }
        End Function

        Public Shared Widening Operator CType(expression$) As AxisProvider
            Return TryParse(expression)
        End Operator

        Public Shared Narrowing Operator CType(axis As AxisProvider) As Double()
            Return axis.AxisTicks
        End Operator
    End Class
End Namespace
