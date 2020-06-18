﻿Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Language
Imports names = Microsoft.VisualBasic.Data.visualize.Network.FileStream.Generic.NamesOf

Namespace FileStream

    Public Module TabularCreator

        ''' <summary>
        ''' 将<see cref="NetworkGraph"/>保存到csv文件之中
        ''' </summary>
        ''' <param name="g"></param>
        ''' <param name="properties">
        ''' The data property names of nodes and edges.
        ''' </param>
        ''' <returns></returns>
        <Extension>
        Public Function Tabular(g As NetworkGraph, Optional properties$() = Nothing, Optional is2D As Boolean = True) As NetworkTables
            Dim nodes As Node() = g.createNodesTable(properties, is2D).ToArray
            Dim edges As New List(Of NetworkEdge)

            For Each l As Edge In g.graphEdges
                edges += New NetworkEdge With {
                    .fromNode = l.U.label,
                    .toNode = l.V.label,
                    .interaction = l.data(names.REFLECTION_ID_MAPPING_INTERACTION_TYPE),
                    .value = l.weight,
                    .Properties = New Dictionary(Of String, String) From {
                        {NameOf(EdgeData.label), l.data.label},
                        {names.REFLECTION_ID_MAPPING_EDGE_GUID, l.ID}
                    }
                }

                With edges.Last
                    If Not properties Is Nothing Then
                        For Each key As String In properties.Where(Function(p) l.data.HasProperty(p))
                            .ItemValue(key) = l.data(key)
                        Next
                    End If
                End With
            Next

            Return New NetworkTables With {
                .edges = edges,
                .nodes = nodes
            }
        End Function

        <Extension>
        Private Iterator Function createNodesTable(g As NetworkGraph, properties$(), is2Dlayout As Boolean) As IEnumerable(Of Node)
            Dim data As Dictionary(Of String, String)

            For Each n As Graph.Node In g.vertex
                If n.data Is Nothing Then
                    n.data = New NodeData
                End If

                data = New Dictionary(Of String, String) From {
                    {"weight", n.data.mass}
                }

                If Not n.data.initialPostion Is Nothing Then
                    ' skip coordination information when no layout data.
                    data("x") = n.data.initialPostion.x
                    data("y") = n.data.initialPostion.y

                    If Not is2Dlayout Then
                        data("z") = n.data.initialPostion.z
                    End If
                End If

                If Not n.data.color Is Nothing AndAlso n.data.color.GetType Is GetType(SolidBrush) Then
                    data(names.REFLECTION_ID_MAPPING_NODECOLOR) = DirectCast(n.data.color, SolidBrush).Color.ToHtmlColor
                End If

                If Not properties Is Nothing Then
                    For Each key As String In properties.Where(Function(p) n.data.HasProperty(p))
                        data(key) = n.data(key)
                    Next
                End If

                For Each key As String In {
                    names.REFLECTION_ID_MAPPING_DEGREE,
                    names.REFLECTION_ID_MAPPING_DEGREE_IN,
                    names.REFLECTION_ID_MAPPING_DEGREE_OUT,
                    names.REFLECTION_ID_MAPPING_BETWEENESS_CENTRALITY
                }.Where(Function(p) n.data.HasProperty(p))

                    data(key) = n.data(key)
                Next

                ' 20191022
                ' name 会和cytoscape之中的name属性产生冲突
                ' 所以在这里修改为label
                If Not data.ContainsKey("label") Then
                    data.Add("label", n.data.label)
                End If

                Yield New Node With {
                    .ID = n.label,
                    .NodeType = n.data(names.REFLECTION_ID_MAPPING_NODETYPE),
                    .Properties = data
                }
            Next
        End Function
    End Module
End Namespace