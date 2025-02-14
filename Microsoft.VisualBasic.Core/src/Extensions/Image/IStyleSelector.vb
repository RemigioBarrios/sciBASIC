﻿#Region "Microsoft.VisualBasic::1ef590590551a27f0a9b66df7eb0264b, sciBASIC#\Microsoft.VisualBasic.Core\src\Extensions\Image\IStyleSelector.vb"

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
    '     File Size: 280 B


    '     Interface IStyleSelector
    ' 
    '         Function: GetElementById, GetElementsByClassName, GetElementsByName
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Imaging

    Public Interface IStyleSelector(Of T)

        Function GetElementById(id As String) As T
        Function GetElementsByClassName(classname As String) As IEnumerable(Of T)
        Function GetElementsByName(name As String) As T()

    End Interface
End Namespace
