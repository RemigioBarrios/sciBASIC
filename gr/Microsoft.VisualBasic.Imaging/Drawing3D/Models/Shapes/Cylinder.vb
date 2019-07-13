﻿#Region "Microsoft.VisualBasic::b612bea5ab77372d66dde5ec825c93cf, gr\Microsoft.VisualBasic.Imaging\Drawing3D\Models\Shapes\Cylinder.vb"

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

    '     Class Cylinder
    ' 
    '         Constructor: (+2 Overloads) Sub New
    ' 
    '     Class Pie
    ' 
    '         Constructor: (+1 Overloads) Sub New
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Drawing3D.Models.Isometric.Shapes

    ''' <summary>
    ''' Created by fabianterhorst on 01.04.17.
    ''' </summary>
    Public Class Cylinder : Inherits Shape3D

        Public Sub New(origin As Point3D, vertices As Double, height As Double)
            Me.New(origin, 1, vertices, height)
        End Sub

        Public Sub New(origin As Point3D, radius#, vertices#, height#)
            Call MyBase.New()

            Dim circle As New Paths.Circle(origin, radius, vertices)
            Call Extrude(Me, circle, height)
        End Sub
    End Class

    Public Class Pie : Inherits Shape3D

        Public Sub New(origin As Point3D, radius#, startAngle#, sweepAngle#, vertices#, height#)
            Call MyBase.New

            Dim arc As New Paths.Arc(origin, radius, startAngle, sweepAngle, vertices)
            Call Extrude(Me, arc, height)
        End Sub
    End Class
End Namespace
