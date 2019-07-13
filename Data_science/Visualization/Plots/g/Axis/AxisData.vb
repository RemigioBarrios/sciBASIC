﻿#Region "Microsoft.VisualBasic::9277150f9916539941eca1b4d89b1948, Data_science\Visualization\Plots\g\Axis\AxisData.vb"

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

    '     Class AxisValue
    ' 
    '         Properties: Font, Range, Tick, Title
    ' 
    '         Function: ToString
    ' 
    '     Structure AxisData
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Graphic.Axis

    ''' <summary>
    ''' 手工指定Axis的数据
    ''' </summary>
    Public Class AxisValue

        ''' <summary>
        ''' 最大值和最小值
        ''' </summary>
        ''' <returns></returns>
        Public Property Range As DoubleRange
        ''' <summary>
        ''' 数值标签出现的间隔
        ''' </summary>
        ''' <returns></returns>
        Public Property Tick As Double
        ''' <summary>
        ''' 坐标轴的标题
        ''' </summary>
        ''' <returns></returns>
        Public Property Title As String
        Public Property Font As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class

    ''' <summary>
    ''' 横纵坐标轴的画图数据
    ''' </summary>
    Public Structure AxisData

        Dim X, Y As AxisValue

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Structure
End Namespace
