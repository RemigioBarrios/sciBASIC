﻿Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection.Deque
Imports Microsoft.VisualBasic.Data.visualize.Network.Analysis.Model
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph

Namespace Analysis

    ''' <summary>
    ''' Kosaraju's algorithm is a linear time algorithm to find 
    ''' the strongly connected components of a directed graph.
    ''' 
    ''' Kosaraju's algorithm works as follows:
    '''
    ''' + Let G be a directed graph And S be an empty stack.
    ''' + While S does Not contain all vertices:
    '''    + Choose an arbitrary vertex ''v'' not in S. 
    '''    + Perform a depth first search starting at ''v''. 
    '''    + Each time that depth-first search finishes expanding a vertex ''u'', push ''u'' onto S.
    ''' + Reverse the directions Of all arcs To obtain the transpose graph.
    ''' + While S Is nonempty:
    '''    + Pop the top vertex ''v'' from S. 
    '''    + Perform a depth-first search starting at ''v'' in the transpose graph. 
    '''    + The set of visited vertices will give the strongly connected component containing ''v''; 
    '''    + record this and remove all these vertices from the graph G and the stack S. 
    '''    + Equivalently, breadth-first search (BFS) can be used instead of depth-first search.
    '''  
    ''' > https://github.com/awadalaa/kosaraju
    ''' </summary>
    Public NotInheritable Class Kosaraju

        Dim t As Integer
        ''' <summary>
        ''' the strong connected components
        ''' </summary>
        Dim scc As New List(Of Integer?)()
        Dim pass As Integer = 0
        Dim deque As New Deque(Of Node)

        Shared ReadOnly FORWARD_TRAVERSAL As New ForwardTraversal()
        Shared ReadOnly BACKWARD_TRAVERSAL As New BackwardTraversal()

        Private Class NodeCompares : Implements IComparer(Of Node)

            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Public Function Compare(v1 As Node, v2 As Node) As Integer Implements IComparer(Of Node).Compare
                Return v2.data.mass.CompareTo(v1.data.mass)
            End Function
        End Class

        Public Shared Function StronglyConnectedComponents(gr As NetworkGraph, Optional forwardTraversal As Boolean = True) As Kosaraju
            Dim search As New Kosaraju
            search.loop(gr, If(forwardTraversal, FORWARD_TRAVERSAL, BACKWARD_TRAVERSAL))
            Return search
        End Function

        Private Sub [loop](ByVal gr As NetworkGraph, tp As EdgeTraversalPolicy)
            Dim vs As ICollection(Of Node)

            If pass = 0 Then
                ' 这里是按照id降序
                vs = gr.vertex _
                    .OrderByDescending(Function(a) a.ID) _
                    .ToArray
            Else
                ' 这里是按照结果值升序
                vs = New SortedSet(Of Node)(gr.vertex, New NodeCompares)
            End If

            For Each v As Node In vs
                If Not v.visited Then
                    v.visited = True
                    deque.AddHead(v)

                    While Not deque.Empty
                        v = deque.Peek()
                        Call depthFirstSearch(tp, v)
                    End While

                    If pass = 1 Then
                        scc.Add(t)
                        t = 0
                    End If
                End If
            Next

            pass += 1
        End Sub

        Private Sub depthFirstSearch(tp As EdgeTraversalPolicy, v As Node)
            For Each edge As Edge In tp.edges(v.directedVertex)
                Dim [next] As Node = tp.vertex(edge)

                If Not [next].visited Then
                    [next].visited = True
                    deque.AddHead([next])
                    Return
                End If
            Next

            t += 1

            If pass = 0 Then
                v.data.mass = t
            End If

            deque.RemoveHead()
        End Sub
    End Class
End Namespace