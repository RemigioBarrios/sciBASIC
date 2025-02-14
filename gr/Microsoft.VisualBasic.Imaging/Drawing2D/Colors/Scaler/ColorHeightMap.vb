﻿Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq

Namespace Drawing2D.Colors.Scaler

    ''' <summary>
    ''' Map a color to a single numeric value
    ''' </summary>
    ''' <remarks>
    ''' Extract numeric value from a raster data
    ''' </remarks>
    Public Class ColorHeightMap : Implements IBucketVector

        Dim ruler As Color()

        Public ReadOnly Property Levels As Integer
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return ruler.Length
            End Get
        End Property

        Sub New(ParamArray ruler As Color())
            Me.ruler = ruler
        End Sub

        ''' <summary>
        ''' scale the scalar color palette to a specific level.
        ''' </summary>
        ''' <param name="level"></param>
        ''' <returns></returns>
        Public Function ScaleLevels(level As Integer) As ColorHeightMap
            ruler = Designer.CubicSpline(ruler, level, interpolate:=True)
            Return Me
        End Function

        ''' <summary>
        ''' mapping color to level
        ''' </summary>
        ''' <param name="c"></param>
        ''' <returns></returns>
        Public Function GetScale(c As Color) As Single
            Dim v As Double() = New Double(ruler.Length - 1) {}

            For i As Integer = 0 To v.Length - 1
                v(i) = ruler(i).EuclideanDistance(c)
            Next

            Return CSng(which.Min(v))
        End Function

        ''' <summary>
        ''' use for html view in R# scripting
        ''' </summary>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetVector() As IEnumerable Implements IBucketVector.GetVector
            Return ruler.Select(Function(c) c.ToHtmlColor)
        End Function
    End Class
End Namespace