﻿#Region "Microsoft.VisualBasic::3de1dcd3c85a4ff5f4aa502544b62980, sciBASIC#\mime\text%html\Parser\Escaping.vb"

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


' Code Statistics:

'   Total Lines: 10
'    Code Lines: 7
' Comment Lines: 0
'   Blank Lines: 3
'     File Size: 182 B


'     Class Escaping
' 
' 
' 
' 
' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Text.Parser

Namespace Language

    Friend Class Escaping

        Public [string] As Boolean
        Public quote As Char
        Public tagOpen As Boolean
        ''' <summary>
        ''' the html script tag is a special tag 
        ''' </summary>
        Public scriptOpen As Boolean

        Public checkScriptEnd As New CharBuffer

    End Class
End Namespace
