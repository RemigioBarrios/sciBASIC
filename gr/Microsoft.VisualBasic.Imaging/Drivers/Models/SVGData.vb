﻿#Region "Microsoft.VisualBasic::057737d63152e061dc6781e29e831296, gr\Microsoft.VisualBasic.Imaging\Drivers\Models\SVGData.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xie (genetics@smrucc.org)
'       xieguigang (xie.guigang@live.com)
' 
' Copyright (c) 2018 GPL3 Licensed
' 
' 
' GNU GENERAL PUBLIC LICENSE (GPL3)
' 
' 
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with this program. If not, see <http://www.gnu.org/licenses/>.



' /********************************************************************************/

' Summaries:

'     Class SVGData
' 
'         Properties: Driver, SVG, title, XmlComment
' 
'         Constructor: (+2 Overloads) Sub New
'         Function: Render, (+2 Overloads) Save
' 
' 
' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.SVG
Imports Microsoft.VisualBasic.MIME.Markup.HTML.CSS

Namespace Driver

    ''' <summary>
    ''' SVG graphic data
    ''' </summary>
    Public Class SVGData : Inherits GraphicsData

        Public ReadOnly Property SVG As SVGDataLayers
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return engine.__svgData
            End Get
        End Property

        Dim engine As GraphicsSVG

        ''' <summary>
        ''' <paramref name="img"/> parameter is <see cref="GraphicsSVG"/>
        ''' </summary>
        ''' <param name="img"></param>
        ''' <param name="size"></param>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub New(img As Object, size As Size, padding As Padding)
            Call MyBase.New(img, size, padding)

            Me.engine = DirectCast(img, GraphicsSVG)
        End Sub

        Sub New(canvas As GraphicsSVG, padding As Padding)
            Call Me.New(canvas, canvas.Size, padding)
        End Sub

        Public Overrides ReadOnly Property Driver As Drivers
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return Drivers.SVG
            End Get
        End Property

        Public Property XmlComment As String
        Public Property title As String

        Const InvalidSuffix$ = "The SVG image file save path: {0} not ending with *.svg file extension suffix!"

        ''' <summary>
        ''' Save the image as svg file.
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        Public Overrides Function Save(path As String) As Boolean
            If Not path.ExtensionSuffix.TextEquals("svg") Then
                Call String.Format(InvalidSuffix, path.ToFileURL).Warning
            End If

            With Layout.Size
                Dim sz$ = $"{ .Width},{ .Height}"
                Return engine.WriteSVG(path, sz, XmlComment, title:=title)
            End With
        End Function

        Public Overrides Function Save(out As Stream) As Boolean
            With Layout.Size
                Dim sz$ = $"{ .Width},{ .Height}"
                Return engine.WriteSVG(out, size:=sz, comments:=XmlComment, title:=title)
            End With
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function Render() As Drawing.Image
            Return Renderer.DrawImage(Me)
        End Function
    End Class
End Namespace
