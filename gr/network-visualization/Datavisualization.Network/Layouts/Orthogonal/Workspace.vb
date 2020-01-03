﻿#Region "Microsoft.VisualBasic::30fc98f61ee17f034d4deeafae91aed9, gr\network-visualization\Datavisualization.Network\Layouts\Orthogonal\Workspace.vb"

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

    '     Class Workspace
    ' 
    '         Properties: totalEdgeLength, totalIntersections
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Imaging.Math2D

Namespace Layouts.Orthogonal

    Friend Class Workspace

        Public g As NetworkGraph
        Public grid As Grid
        Public V As Node()
        ''' <summary>
        ''' c
        ''' </summary>
        Public cellSize As Double
        Public delta As Double

        Public width As Dictionary(Of String, Double)
        Public height As Dictionary(Of String, Double)

        Public ReadOnly Property totalEdgeLength As Double
            Get
                Dim len As Double

                For Each edge As Edge In g.graphEdges
                    len += distance(edge.U, edge.V, cellSize, delta)
                Next

                Return len
            End Get
        End Property

        Public ReadOnly Property totalIntersections As Integer
            Get
                Dim n As Integer

                For Each i As Edge In g.graphEdges
                    Dim a = i.U.data.initialPostion
                    Dim b = i.V.data.initialPostion

                    For Each j As Edge In g.graphEdges
                        If i Is j Then
                            Continue For
                        End If

                        Dim c = j.U.data.initialPostion
                        Dim d = j.V.data.initialPostion

                        If GeometryMath.GetLineIntersection(a.x, a.y, b.x, b.y, c.x, c.y, d.x, d.y) = Intersections.Intersection Then
                            n += 1
                        End If
                    Next
                Next

                Return n
            End Get
        End Property

    End Class
End Namespace
