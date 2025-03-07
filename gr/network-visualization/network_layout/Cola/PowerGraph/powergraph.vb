﻿#Region "Microsoft.VisualBasic::e2bfba5c1186c74bc739c4ba0c6cfb21, sciBASIC#\gr\network-visualization\Datavisualization.Network\Layouts\Cola\PowerGraph\powergraph.vb"

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

    '   Total Lines: 21
    '    Code Lines: 15
    ' Comment Lines: 0
    '   Blank Lines: 6
    '     File Size: 514 B


    '     Interface network
    ' 
    '         Properties: links, nodes
    ' 
    '     Class PowerGraph
    ' 
    '         Properties: groups, powerEdges
    ' 
    '     Class LayoutGraph
    ' 
    '         Properties: cola, powerGraph
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language

Namespace Cola

    Public Interface network

        Property nodes() As Node()
        Property links() As Link(Of Node)()

    End Interface

    Public Class PowerGraph
        Public Property groups As List(Of Node)
        Public Property powerEdges As List(Of PowerEdge(Of Node))
    End Class

    Public Class LayoutGraph
        Public Property cola As Layout
        Public Property powerGraph As PowerGraph
    End Class
End Namespace
