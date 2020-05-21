﻿#Region "Microsoft.VisualBasic::a1896556223b05f7cf4b0d5b5aa9c32a, gr\Microsoft.VisualBasic.Imaging\Drivers\Models\WmfData.vb"

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

    '     Class WmfData
    ' 
    '         Properties: Driver
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: (+2 Overloads) Save, wmfTmp
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic.MIME.Markup.HTML.CSS

Namespace Driver

    Public Class WmfData : Inherits GraphicsData

        Public Overrides ReadOnly Property Driver As Drivers
            Get
                Return Drivers.WMF
            End Get
        End Property

        ReadOnly tempfile As String

        Public Sub New(img As Object, size As Size, padding As Padding)
            MyBase.New(img, size, padding)

            ' the wmf metafile temp file
            ' which its file path is generated from function 
            ' wmfTmp
            ' in graphics plot helper api
            If Not TypeOf img Is String Then
                Throw New InvalidDataException("The input img data should be a temporary wmf meta file path!")
            Else
                tempfile = img
            End If

            If tempfile.FileLength <= 0 Then
                Throw New InvalidDataException("The input img data is nothing or file unavailable currently!")
            End If
        End Sub

        Public Overrides Function Save(path As String) As Boolean
            Return tempfile.FileCopy(path)
        End Function

        Public Overrides Function Save(out As Stream) As Boolean
            Using reader As FileStream = tempfile.Open(FileMode.Open, doClear:=False)
                Call out.Seek(Scan0, SeekOrigin.Begin)
                Call reader.CopyTo(out)
            End Using

            Return True
        End Function

        Friend Shared Function wmfTmp() As String
            Return App.GetAppSysTempFile(".wmf", App.PID, RandomASCIIString(10, skipSymbols:=True))
        End Function
    End Class
End Namespace
