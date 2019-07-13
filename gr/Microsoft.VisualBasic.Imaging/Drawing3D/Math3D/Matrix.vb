﻿#Region "Microsoft.VisualBasic::7d7e8fe7e37bc25e396b1dc1792e0bd6, gr\Microsoft.VisualBasic.Imaging\Drawing3D\Math3D\Matrix.vb"

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

    '     Class Matrix
    ' 
    '         Properties: Matrix
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: GetEnumerator, IEnumerable_GetEnumerator, TranslateBuffer
    '         Structure SurfaceVector
    ' 
    ' 
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Math.LinearAlgebra

Namespace Drawing3D.Math3D

    Public Class Matrix : Implements IEnumerable(Of Surface)

        Public ReadOnly Property Matrix As Vector3D

        ReadOnly surfaces As New List(Of SurfaceVector)

        Public Structure SurfaceVector
            Public brush As Brush
            Public vertices As Integer()
        End Structure

        Sub New(surface As IEnumerable(Of Surface))
            Dim i As VBInteger = 0
            Dim x As New List(Of Double)
            Dim y As New List(Of Double)
            Dim z As New List(Of Double)

            For Each s As Surface In surface
                Dim v As New List(Of Integer)

                For Each p3D As Point3D In s.vertices
                    v += ++i  ' 这个自增变量是为了生成后面的顶点坐标所使用的

                    With p3D
                        x += .X
                        y += .Y
                        z += .Z
                    End With
                Next

                surfaces += New SurfaceVector With {
                    .brush = s.brush,
                    .vertices = v
                }
            Next

            Matrix = New Vector3D(New Vector(x), New Vector(y), New Vector(z))
        End Sub

        ''' <summary>
        ''' 投影
        ''' </summary>
        ''' <param name="v">经过转换之后的向量，例如旋转或者位移，在这个函数值中会利用摄像机进行投影</param>
        ''' <returns></returns>
        Public Function TranslateBuffer(camera As Camera, v As Vector3D, illumination As Boolean) As IEnumerable(Of Polygon)
            Dim surfaces As New List(Of Surface)

            For Each s As SurfaceVector In Me.surfaces
                surfaces += New Surface With {
                    .vertices = v(s.vertices),
                    .brush = s.brush
                }
            Next

            Return camera.PainterBuffer(surfaces, illumination)
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of Surface) Implements IEnumerable(Of Surface).GetEnumerator
            For Each s As SurfaceVector In surfaces
                Yield New Surface With {
                    .vertices = Matrix(s.vertices),
                    .brush = s.brush
                }
            Next
        End Function
    End Class
End Namespace
