﻿#Region "Microsoft.VisualBasic::a7a9735ff4cbb6fb1f4c96060daedf4a, gr\network-visualization\Datavisualization.Network\Layouts\ForceDirected\Layout\Layout\ForceDirected2D.vb"

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

    '     Class ForceDirected2D
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: GetBoundingBox, GetPoint
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Data.visualize.Network.Layouts.Interfaces

Namespace Layouts

    ''' <summary>
    ''' Layout provider engine for the 2D network graphics.
    ''' </summary>
    Public Class ForceDirected2D
        Inherits ForceDirected(Of FDGVector2)

        Public Sub New(iGraph As NetworkGraph, iStiffness As Single, iRepulsion As Single, iDamping As Single)
            MyBase.New(iGraph, iStiffness, iRepulsion, iDamping)
        End Sub

        Public Overrides Function GetPoint(iNode As Node) As LayoutPoint
            Dim iniPosition As FDGVector2

            If Not (nodePoints.ContainsKey(iNode.Label)) Then
                iniPosition = TryCast(iNode.data.initialPostion, FDGVector2)
                If iniPosition Is Nothing Then
                    iniPosition = TryCast(FDGVector2.Random(), FDGVector2)
                End If

                nodePoints(iNode.Label) = New LayoutPoint(iniPosition, FDGVector2.Zero(), FDGVector2.Zero(), iNode)
            End If

            Return nodePoints(iNode.Label)
        End Function

        Public Overrides Function GetBoundingBox() As BoundingBox
            Dim boundingBox As New BoundingBox()
            Dim bottomLeft As FDGVector2 = TryCast(FDGVector2.Identity().Multiply(BoundingBox.defaultBB * -1.0F), FDGVector2)
            Dim topRight As FDGVector2 = TryCast(FDGVector2.Identity().Multiply(BoundingBox.defaultBB), FDGVector2)

            For Each n As Node In graph.vertex
                Dim position As FDGVector2 = TryCast(GetPoint(n).position, FDGVector2)

                If position.x < bottomLeft.x Then
                    bottomLeft.x = position.x
                End If
                If position.y < bottomLeft.y Then
                    bottomLeft.y = position.y
                End If
                If position.x > topRight.x Then
                    topRight.x = position.x
                End If
                If position.y > topRight.y Then
                    topRight.y = position.y
                End If
            Next

            Dim padding As AbstractVector = (topRight - bottomLeft).Multiply(BoundingBox.defaultPadding)
            boundingBox.bottomLeftFront = bottomLeft.Subtract(padding)
            boundingBox.topRightBack = topRight.Add(padding)
            Return boundingBox
        End Function
    End Class
End Namespace
