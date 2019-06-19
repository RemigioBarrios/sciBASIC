﻿#Region "Microsoft.VisualBasic::820a1f34670a349d4a728bdce6f3ec81, gr\network-visualization\Datavisualization.Network\Layouts\BarycentricMethod.vb"

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

    '     Module BarycentricMethod
    ' 
    '         Function: __setLoci, ForceDirectedLayout
    ' 
    '         Sub: doLayout
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Math
Imports Microsoft.VisualBasic.Scripting.MetaData

Namespace Layouts

    <Package("Network.Layout.Barycentric", Publisher:="xie.guigang@gmail.com")>
    Public Module BarycentricMethod

        ''' <summary>
        ''' Applies the spring embedder.
        ''' </summary>
        ''' <param name="Network"></param>
        ''' <param name="iterations"></param>
        <ExportAPI("Layout.SpringEmbedder")>
        <Extension>
        Public Sub doLayout(Network As NetworkGraph, iterations As Integer, size As Size)
            Dim nodes As Node() = Network.connectedNodes
            Dim edges As Edge() = Network.graphEdges

            Dim k As Double = size.Width * size.Height / (nodes.Length * 1000)
            Dim c As Double = 3
            ' Repulsive forces between nodes that are further apart than this are ignored.
            Dim maxRepulsiveForceDistance As Double = 10

            ' For each iteration...
            For it As Integer = 0 To iterations - 1

                ' Calculate forces acting on nodes due to node-node repulsions...
                For Each NodeA As Node In nodes
                    For Each NodeB In nodes

                        If NodeA.Equals(NodeB) Then
                            Continue For
                        End If

                        Dim deltaX As Double = NodeB.data.initialPostion.x - NodeA.data.initialPostion.x
                        Dim deltaY As Double = NodeB.data.initialPostion.y - NodeA.data.initialPostion.y

                        Dim distanceSquared As Double = deltaX * deltaX + deltaY * deltaY

                        If distanceSquared < 0.01 Then
                            deltaX = (New Random(1)).NextDouble() / 10 + 0.1
                            deltaY = (New Random(2)).NextDouble() / 10 + 0.1
                            distanceSquared = deltaX * deltaX + deltaY * deltaY
                        End If

                        Dim distance As Double = System.Math.Sqrt(distanceSquared)

                        If distance < maxRepulsiveForceDistance Then
                            Dim repulsiveForce As Double = (k * k / distance)
                            Dim fa As Point = NodeA.data.Force
                            Dim fb As Point = NodeB.data.Force

                            fb.X = fb.X + (repulsiveForce * deltaX / distance)
                            fb.Y = fb.Y + (repulsiveForce * deltaY / distance)
                            fa.X = fa.X - (repulsiveForce * deltaX / distance)
                            fa.Y = fa.Y - (repulsiveForce * deltaY / distance)

                            NodeA.data.Force = fa
                            NodeB.data.Force = fb
                        End If
                    Next
                Next

                ' Calculate forces acting on nodes due to edge attractions.
                For e As Integer = 0 To edges.Length - 1
                    Dim edge As Edge = edges(e)
                    Dim nodeA As Node = edge.U
                    Dim nodeB As Node = edge.V

                    Dim deltaX As Double = nodeB.data.initialPostion.x - nodeA.data.initialPostion.x
                    Dim deltaY As Double = nodeB.data.initialPostion.y - nodeA.data.initialPostion.y

                    Dim distanceSquared As Double = deltaX * deltaX + deltaY * deltaY

                    ' Avoid division by zero error or Nodes flying off to
                    ' infinity.  Pretend there is an arbitrary distance between
                    ' the Nodes.
                    If distanceSquared < 0.01 Then
                        deltaX = (New Random(3)).NextDouble() / 10 + 0.1
                        deltaY = (New Random(4)).NextDouble() / 10 + 0.1
                        distanceSquared = deltaX * deltaX + deltaY * deltaY
                    End If

                    Dim distance As Double = System.Math.Sqrt(distanceSquared)

                    If distance > maxRepulsiveForceDistance Then
                        distance = maxRepulsiveForceDistance
                    End If

                    distanceSquared = distance * distance

                    Dim attractiveForce As Double = (distanceSquared - k * k) / k

                    ' Make edges stronger if people know each other.
                    Dim weight As Double = edge.data.weight

                    attractiveForce *= (System.Math.Log(weight) * 0.5) + 1

                    Dim fa As Point = nodeA.data.Force
                    Dim fb As Point = nodeB.data.Force

                    fb.X = nodeB.data.Force.X - attractiveForce * deltaX / distance
                    fb.Y = nodeB.data.Force.Y - attractiveForce * deltaY / distance
                    fa.X = nodeA.data.Force.X + attractiveForce * deltaX / distance
                    fa.Y = nodeA.data.Force.Y + attractiveForce * deltaY / distance

                    nodeA.data.Force = fa
                    nodeB.data.Force = fb
                Next

                ' Now move each node to its new location...
                For a As Integer = 0 To nodes.Length - 1
                    Dim node As Node = nodes(a)

                    Dim xMovement As Double = c * node.data.Force.X
                    Dim yMovement As Double = c * node.data.Force.Y

                    ' Limit movement values to stop nodes flying into oblivion.
                    Dim max As Double = 100
                    If xMovement > max Then
                        xMovement = max
                    ElseIf xMovement < -max Then
                        xMovement = -max
                    End If
                    If yMovement > max Then
                        yMovement = max
                    ElseIf yMovement < -max Then
                        yMovement = -max
                    End If

                    node.data.initialPostion.Point2D = New Point(node.data.initialPostion.x + xMovement, node.data.initialPostion.y + yMovement)
                    node.data.Force = New Point
                Next
            Next
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="network"></param>
        ''' <param name="cutoff"></param>
        ''' <returns></returns>
        ''' 
        <ExportAPI("Layout.ForceDirected")>
        Public Function ForceDirectedLayout(network As NetworkGraph, size As Size, Optional cutoff As Double = 100) As NetworkGraph
            Call network.vertex _
                .DoEach(Sub(node As Node)
                            Dim randl As New Point With {
                                .X = size.Width * seeds.NextDouble(),
                                .Y = size.Height * seeds.NextDouble()
                            }

                            node.data.initialPostion.Point2D = randl
                        End Sub)

            Call doLayout(network, 1, size)

            Return network
        End Function
    End Module
End Namespace
