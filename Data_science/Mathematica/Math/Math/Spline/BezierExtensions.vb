﻿#Region "Microsoft.VisualBasic::af95f2df98c2e07fe8c2805e2aeec1ce, Data_science\Mathematica\Math\Math\Spline\BezierExtensions.vb"

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

    '     Module BezierExtensions
    ' 
    '         Function: __interpolation, BezierSmoothInterpolation
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq

Namespace Interpolation

    Public Module BezierExtensions

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="data"></param>
        ''' <param name="parallel">并行版本的</param>
        ''' <param name="windowSize">数据采样的窗口大小，默认大小是<paramref name="data"></paramref>的百分之1</param>
        ''' <returns></returns>
        ''' <remarks>先对数据进行采样，然后插值，最后返回插值后的平滑曲线数据以用于下一步分析</remarks>
        Public Function BezierSmoothInterpolation(data#(),
                                                  Optional windowSize% = -1,
                                                  Optional iteration% = 3,
                                                  Optional parallel As Boolean = False) As Double()

            If windowSize <= 0 Then
                windowSize = data.Length / 100
            End If

            If windowSize < 3 Then
                windowSize = 3 ' 最少需要3个点进行插值
            End If

            Dim LQuery As SeqValue(Of Double())()
            Dim slideWindows = data _
                .CreateSlideWindows(winSize:=windowSize,
                                    offset:=windowSize - 1)

            If parallel Then
                LQuery = LinqAPI.Exec(Of SeqValue(Of Double())) <=
 _
                    From win
                    In slideWindows.AsParallel
                    Let value = __interpolation(
                        win.Items, iteration)
                    Select x = New SeqValue(Of Double()) With {
                        .i = win.Index,
                        .value = value
                    }
                    Order By x.i Ascending
            Else
                LQuery = LinqAPI.Exec(Of SeqValue(Of Double())) <=
 _
                    From win As SlideWindow(Of Double)
                    In slideWindows
                    Let value = __interpolation(
                        win.Items, iteration)
                    Select x = New SeqValue(Of Double()) With {
                        .i = win.Index,
                        .value = value
                    }
                    Order By x.i Ascending
            End If

            Dim out#() = LQuery _
                .Select(Function(win) +win) _
                .IteratesALL _
                .ToArray

            Return out
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="X"></param>
        ''' <param name="iteration"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function __interpolation(X#(), iteration%) As Double()
            Dim data As Double() = New Double(2) {}

            data(0) = X(Scan0)
            data(1) = X(X.Length / 2)
            data(2) = X.Last

            Dim tmp As New BezierCurve(
                ctrl1:=New PointF(0, data(0)),
                ctrl2:=New PointF(1, data(1)),
                ctrl3:=New PointF(2, data(2)),
                iteration:=iteration
            )

            X = tmp.BezierPoints _
                .Select(Function(p) CDbl(p.Y)) _
                .ToArray

            Return X
        End Function
    End Module
End Namespace
