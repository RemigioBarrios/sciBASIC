﻿#Region "Microsoft.VisualBasic::4fb4eefea535d7445616c338383477b9, gr\network-visualization\Datavisualization.Network\Graph\Model\Graph.vb"

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

'     Class NetworkGraph
' 
'         Properties: connectedNodes, edges, nodes
' 
'         Constructor: (+1 Overloads) Sub New
' 
'         Function: (+2 Overloads) AddEdge, AddNode, Clone, Copy, (+2 Overloads) CreateEdge
'                   (+2 Overloads) CreateNode, GetEdge, (+2 Overloads) GetEdges, GetElementByID, GetNode
'                   ToString
' 
'         Sub: AddGraphListener, Clear, (+2 Overloads) CreateEdges, (+2 Overloads) CreateNodes, DetachNode
'              FilterEdges, FilterNodes, Merge, notify, RemoveEdge
'              RemoveNode
' 
' 
' /********************************************************************************/

#End Region

'! 
'@file Graph.cs
'@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
'		<http://github.com/juhgiyo/epForceDirectedGraph.cs>
'@date August 08, 2013
'@brief Graph Interface
'@version 1.0
'
'@section LICENSE
'
'The MIT License (MIT)
'
'Copyright (c) 2013 Woong Gyu La <juhgiyo@gmail.com>
'
'Permission is hereby granted, free of charge, to any person obtaining a copy
'of this software and associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
'copies of the Software, and to permit persons to whom the Software is
'furnished to do so, subject to the following conditions:
'
'The above copyright notice and this permission notice shall be included in
'all copies or substantial portions of the Software.
'
'THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
'IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
'LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
'OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
'THE SOFTWARE.
'
'@section DESCRIPTION
'
'An Interface for the Graph Class.
'
'

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.visualize.Network.Layouts
Imports Microsoft.VisualBasic.Data.visualize.Network.Layouts.Interfaces
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq

Namespace Graph

    ''' <summary>
    ''' The network graph object model, corresponding network csv table data model is <see cref="FileStream.NetworkTables"/> 
    ''' </summary>
    Public Class NetworkGraph : Inherits GraphTheory.Network.NetworkGraph(Of Node, Edge)
        Implements ICloneable

        ''' <summary>
        ''' Returns the set of all Nodes that have emanating Edges.
        ''' This therefore returns all Nodes that will be visible in the drawing.
        ''' (这个属性之中是没有任何孤立的节点的)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 对于<see cref="vertex"/>属性而言，其则是所有的节点的集合，
        ''' 包括当前的这个<see cref="connectedNodes"/>和孤立点的总集合
        ''' </remarks>
        Public ReadOnly Property connectedNodes() As Node()
            Get
                Return graphEdges _
                    .Select(Function(d) d.Iterate2Nodes) _
                    .IteratesALL _
                    .Distinct _
                    .ToArray
            End Get
        End Property

        ''' <summary>
        ''' <see cref="Node.Label"/>为键名
        ''' </summary>
        Private _nodeSet As Dictionary(Of String, Node)
        Private _adjacencySet As Dictionary(Of String, Dictionary(Of String, List(Of Edge)))

        Private _nextNodeId As Integer = 0
        Private _nextEdgeId As Integer = 0
        Private _eventListeners As List(Of IGraphEventListener)

        Public Sub New()
            _nodeSet = New Dictionary(Of String, Node)()
            _eventListeners = New List(Of IGraphEventListener)()
            _adjacencySet = New Dictionary(Of String, Dictionary(Of String, List(Of Edge)))()
        End Sub

        Public Sub Clear()
            Call vertices.Clear()
            Call edges.Clear()
            Call _adjacencySet.Clear()
        End Sub

        ''' <summary>
        ''' 添加节点然后返回这个新添加的节点
        ''' </summary>
        ''' <param name="iNode"></param>
        ''' <returns></returns>
        Public Function AddNode(iNode As Node) As Node
            If Not _nodeSet.ContainsKey(iNode.Label) Then
                Call vertices.Add(iNode)
            End If

            _nodeSet(iNode.Label) = iNode
            notify()
            Return iNode
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overloads Function AddEdge(i%, j%) As Edge
            Return CreateEdge(GetElementByID(i), GetElementByID(j))
        End Function

        Public Function GetElementByID(id As Integer) As Node
            Return vertex.Where(Function(n) n.ID = id).FirstOrDefault
        End Function

        Public Overloads Function AddEdge(iEdge As Edge) As Edge
            If Not edges.ContainsKey(iEdge.ID) Then
                Call edges.Add(iEdge.ID, iEdge)
            End If

            If Not (_adjacencySet.ContainsKey(iEdge.U.Label)) Then
                _adjacencySet(iEdge.U.Label) = New Dictionary(Of String, List(Of Edge))()
            End If
            If Not (_adjacencySet(iEdge.U.Label).ContainsKey(iEdge.V.Label)) Then
                _adjacencySet(iEdge.U.Label)(iEdge.V.Label) = New List(Of Edge)()
            End If

            If Not _adjacencySet(iEdge.U.Label)(iEdge.V.Label).Contains(iEdge) Then
                _adjacencySet(iEdge.U.Label)(iEdge.V.Label).Add(iEdge)
            End If

            Call notify()

            Return iEdge
        End Function

        Public Sub CreateNodes(iDataList As List(Of NodeData))
            For listTrav As Integer = 0 To iDataList.Count - 1
                CreateNode(iDataList(listTrav))
            Next
        End Sub

        Public Sub CreateNodes(iNameList As List(Of String))
            For listTrav As Integer = 0 To iNameList.Count - 1
                CreateNode(iNameList(listTrav))
            Next
        End Sub

        Public Sub CreateEdges(iDataList As List(Of Triple(Of String, String, EdgeData)))
            For listTrav As Integer = 0 To iDataList.Count - 1
                If Not _nodeSet.ContainsKey(iDataList(listTrav).first) Then
                    Return
                End If
                If Not _nodeSet.ContainsKey(iDataList(listTrav).second) Then
                    Return
                End If
                Dim node1 As Node = _nodeSet(iDataList(listTrav).first)
                Dim node2 As Node = _nodeSet(iDataList(listTrav).second)

                CreateEdge(node1, node2, iDataList(listTrav).third)
            Next
        End Sub

        Public Sub CreateEdges(iDataList As List(Of KeyValuePair(Of String, String)))
            For listTrav As Integer = 0 To iDataList.Count - 1
                If Not _nodeSet.ContainsKey(iDataList(listTrav).Key) Then
                    Return
                End If
                If Not _nodeSet.ContainsKey(iDataList(listTrav).Value) Then
                    Return
                End If
                Dim node1 As Node = _nodeSet(iDataList(listTrav).Key)
                Dim node2 As Node = _nodeSet(iDataList(listTrav).Value)

                CreateEdge(node1, node2)
            Next
        End Sub

        Public Function CreateNode(data As NodeData) As Node
            Dim tNewNode As New Node(_nextNodeId.ToString(), data)
            _nextNodeId += 1
            AddNode(tNewNode)
            Return tNewNode
        End Function

        ''' <summary>
        ''' 使用节点的标签创建一个新的节点对象，将这个节点对象添加进入网络模型之后将新创建的节点对象返回给用户
        ''' </summary>
        ''' <param name="label"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 使用这个函数所构建的节点对象的<see cref="Node.ID"/>是自增的，<paramref name="label"/>则会赋值给<see cref="Node.Label"/>属性
        ''' </remarks>
        Public Function CreateNode(label As String) As Node
            Dim data As New NodeData With {.label = label}
            Dim tNewNode As New Node(label, data) With {
                .ID = _nextNodeId
            }
            _nextNodeId += 1
            AddNode(tNewNode)
            Return tNewNode
        End Function

        ''' <summary>
        ''' 使用两个节点对象创建一条边连接之后，将所创建的边连接对象添加进入当前的图模型之中，最后将边对象返回给用户
        ''' </summary>
        ''' <param name="iSource"></param>
        ''' <param name="iTarget"></param>
        ''' <param name="iData"></param>
        ''' <returns></returns>
        Public Overloads Function CreateEdge(iSource As Node, iTarget As Node, Optional iData As EdgeData = Nothing) As Edge
            If iSource Is Nothing OrElse iTarget Is Nothing Then
                Return Nothing
            End If

            Dim tNewEdge As New Edge(_nextEdgeId.ToString(), iSource, iTarget, iData)
            _nextEdgeId += 1
            AddEdge(tNewEdge)
            Return tNewEdge
        End Function

        Public Overloads Function CreateEdge(iSource As String, iTarget As String, Optional iData As EdgeData = Nothing) As Edge
            If Not _nodeSet.ContainsKey(iSource) Then
                Return Nothing
            End If
            If Not _nodeSet.ContainsKey(iTarget) Then
                Return Nothing
            End If
            Dim node1 As Node = _nodeSet(iSource)
            Dim node2 As Node = _nodeSet(iTarget)
            Return CreateEdge(node1, node2, iData)
        End Function

        Public Function GetEdges(iNode1 As Node, iNode2 As Node) As List(Of Edge)
            If _adjacencySet.ContainsKey(iNode1.Label) AndAlso _adjacencySet(iNode1.Label).ContainsKey(iNode2.Label) Then
                Return _adjacencySet(iNode1.Label)(iNode2.Label)
            End If
            Return Nothing
        End Function

        Public Function GetEdges(iNode As Node) As List(Of Edge)
            Dim retEdgeList As New List(Of Edge)()
            If _adjacencySet.ContainsKey(iNode.Label) Then
                For Each keyPair As KeyValuePair(Of String, List(Of Edge)) In _adjacencySet(iNode.Label)
                    For Each e As Edge In keyPair.Value
                        retEdgeList.Add(e)
                    Next
                Next
            End If

            For Each keyValuePair As KeyValuePair(Of String, Dictionary(Of String, List(Of Edge))) In _adjacencySet
                If keyValuePair.Key <> iNode.Label Then
                    For Each keyPair As KeyValuePair(Of String, List(Of Edge)) In _adjacencySet(keyValuePair.Key)
                        For Each e As Edge In keyPair.Value
                            retEdgeList.Add(e)
                        Next

                    Next
                End If
            Next
            Return retEdgeList
        End Function

        Public Sub RemoveNode(iNode As Node)
            If _nodeSet.ContainsKey(iNode.Label) Then
                _nodeSet.Remove(iNode.Label)
            End If
            vertices.Remove(iNode)
            DetachNode(iNode)
        End Sub

        Public Sub DetachNode(iNode As Node)
            Call graphEdges.ToArray _
                .DoEach(Sub(e As Edge)
                            If e.U.Label = iNode.Label OrElse e.V.Label = iNode.Label Then
                                Call RemoveEdge(e)
                            End If
                        End Sub)
            notify()
        End Sub

        Public Sub RemoveEdge(iEdge As Edge)
            Call edges.Remove(iEdge.ID)

            For Each x As KeyValuePair(Of String, Dictionary(Of String, List(Of Edge))) In _adjacencySet
                For Each y As KeyValuePair(Of String, List(Of Edge)) In x.Value
                    Dim tEdges As List(Of Edge) = y.Value
                    tEdges.Remove(iEdge)
                    If tEdges.Count = 0 Then
                        _adjacencySet(x.Key).Remove(y.Key)
                        Exit For
                    End If
                Next
                If x.Value.Count = 0 Then
                    _adjacencySet.Remove(x.Key)
                    Exit For
                End If
            Next

            Call notify()
        End Sub

        ''' <summary>
        ''' 根据node节点的label来查找
        ''' </summary>
        ''' <param name="label"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetNode(label As String) As Node
            Return vertex _
                .Where(Function(n) n.data.label = label) _
                .FirstOrDefault
        End Function

        ''' <summary>
        ''' Find edge by label data
        ''' </summary>
        ''' <param name="label"></param>
        ''' <returns></returns>
        Public Function GetEdge(label As String) As Edge
            Dim retEdge As Edge = graphEdges.FirstOrDefault(Function(e) e.data.label = label)
            Return retEdge
        End Function

        Public Sub Merge(iMergeGraph As NetworkGraph)
            For Each n As Node In iMergeGraph.vertex
                Dim mergeNode As New Node(_nextNodeId.ToString(), n.data)
                AddNode(mergeNode)
                _nextNodeId += 1
                mergeNode.data.origID = n.Label
            Next

            For Each e As Edge In iMergeGraph.graphEdges
                Dim fromNode As Node = vertex.FirstOrDefault(Function(n) e.U.Label = n.data.origID)
                Dim toNode As Node = vertex.FirstOrDefault(Function(n) e.V.Label = n.data.origID)

                Dim tNewEdge As Edge = AddEdge(New Edge(_nextEdgeId.ToString(), fromNode, toNode, e.data))
                _nextEdgeId += 1
            Next
        End Sub

        Public Sub FilterNodes(match As Predicate(Of Node))
            For Each n As Node In vertex
                If Not match(n) Then
                    RemoveNode(n)
                End If
            Next
        End Sub

        Public Sub FilterEdges(match As Predicate(Of Edge))
            For Each e As Edge In graphEdges
                If Not match(e) Then
                    RemoveEdge(e)
                End If
            Next
        End Sub

        Public Sub AddGraphListener(iListener As IGraphEventListener)
            _eventListeners.Add(iListener)
        End Sub

        Private Sub notify()
            For Each listener As IGraphEventListener In _eventListeners
                listener.GraphChanged()
            Next
        End Sub

        Public Overrides Function ToString() As String
            Return $"Network graph have {vertices.Count} nodes and {graphEdges.Count} edges."
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' graphEdges和edges这两个元素集合应该都是等长的
        ''' </remarks>
        Private Function Clone() As Object Implements ICloneable.Clone
            Dim copy As New NetworkGraph With {
                .vertices = New Dictionary(Of Node)(vertex),
                .edges = New Dictionary(Of Edge)(edges.Values)
            }
            Return copy
        End Function

        Public Function Copy() As NetworkGraph
            Return DirectCast(Clone(), NetworkGraph)
        End Function
    End Class
End Namespace
