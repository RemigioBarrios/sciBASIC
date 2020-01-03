﻿#Region "Microsoft.VisualBasic::ee4cb5a99ba86f8eb4dd39070e2837b2, gr\Microsoft.VisualBasic.Imaging\Drivers\Models\PSData.vb"

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

    '     Class PSData
    ' 
    '         Properties: Driver
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: (+2 Overloads) Save
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.IO

Namespace Driver

    Public Class PSData : Inherits GraphicsData

        Public Sub New(img As Object, size As Size)
            MyBase.New(img, size)
        End Sub

        Public Overrides ReadOnly Property Driver As Drivers

        Public Overrides Function Save(path As String) As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function Save(out As Stream) As Boolean
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
