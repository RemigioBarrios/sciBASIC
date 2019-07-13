﻿#Region "Microsoft.VisualBasic::f55348a849147486e2689e6ff877cb26, Data_science\DataMining\DataMining\ComponentModel\Extensions.vb"

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

    '     Module Extensions
    ' 
    '         Function: ToEnumsTable
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Namespace ComponentModel

    Public Module Extensions

        <Extension>
        Public Function ToEnumsTable(Of T)(classes As IEnumerable(Of ColorClass)) As Dictionary(Of T, ColorClass)
            Return classes.ToDictionary(Function(c) DirectCast(CObj(c.enumInt), T))
        End Function
    End Module
End Namespace
