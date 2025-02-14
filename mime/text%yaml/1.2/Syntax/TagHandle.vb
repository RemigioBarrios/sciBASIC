﻿#Region "Microsoft.VisualBasic::3be8c22ae75013087ba1f8baa2338ab4, sciBASIC#\mime\text%yaml\1.2\Syntax\TagHandle.vb"

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

    '   Total Lines: 23
    '    Code Lines: 17
    ' Comment Lines: 0
    '   Blank Lines: 6
    '     File Size: 478.00 B


    '     Class TagHandle
    ' 
    ' 
    ' 
    '     Class PrimaryTagHandle
    ' 
    ' 
    ' 
    '     Class SecondaryTagHandle
    ' 
    ' 
    ' 
    '     Class NamedTagHandle
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Syntax

    Public Class TagHandle
    End Class

    Public Class PrimaryTagHandle
        Inherits TagHandle
    End Class

    Public Class SecondaryTagHandle
        Inherits TagHandle
    End Class

    Public Class NamedTagHandle
        Inherits TagHandle

        Public Name As New List(Of Char)()

        Public Overrides Function ToString() As String
            Return Name.CharString
        End Function
    End Class
End Namespace
