﻿#Region "Microsoft.VisualBasic::8deb7022a32ace0845aa7b4055e175a9, sciBASIC#\mime\application%pdf\PdfReader\Parser\ParseObjectBase.vb"

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

    '   Total Lines: 7
    '    Code Lines: 7
    ' Comment Lines: 0
    '   Blank Lines: 0
    '     File Size: 325 B


    '     Class ParseObjectBase
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace PdfReader
    Public MustInherit Class ParseObjectBase
        Public Shared ReadOnly [True] As ParseBoolean = New ParseBoolean(True)
        Public Shared ReadOnly [False] As ParseBoolean = New ParseBoolean(False)
        Public Shared ReadOnly Null As ParseNull = New ParseNull()
    End Class
End Namespace
