﻿#Region "Microsoft.VisualBasic::92b077ee5953d923f97d83e5763ed99b, sciBASIC#\Microsoft.VisualBasic.Core\src\ComponentModel\Algorithm\BinaryTree\AVLTree\AVLTree.vb"

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

    '   Total Lines: 184
    '    Code Lines: 120
    ' Comment Lines: 33
    '   Blank Lines: 31
    '     File Size: 7.27 KB


    '     Delegate Sub
    ' 
    ' 
    '     Class AVLTree
    ' 
    '         Constructor: (+2 Overloads) Sub New
    ' 
    '         Function: Add, Find, Remove
    ' 
    '         Sub: (+2 Overloads) Add, appendLeft, appendRight, Remove, removeCurrent
    '              removeLeft, removeRight
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Namespace ComponentModel.Algorithm.BinaryTree

    Public Delegate Sub DuplicatedKeyHandler(Of K, V)(keyNode As BinaryTree(Of K, V), newValue As V)

    ''' <summary>
    ''' The AVL binary tree operator. the binbox key is <typeparamref name="K"/> and 
    ''' value data type is <typeparamref name="V"/>.
    ''' </summary>
    ''' <typeparam name="K"></typeparam>
    ''' <typeparam name="V"></typeparam>
    ''' <remarks>
    ''' http://www.cnblogs.com/huangxincheng/archive/2012/07/22/2603956.html
    ''' </remarks>
    Public Class AVLTree(Of K, V) : Inherits TreeBase(Of K, V)

        ''' <summary>
        ''' Create an instance of the AVL binary tree.
        ''' </summary>
        ''' <param name="compares">
        ''' ``compares(newKey, treeKey)``:
        ''' 
        ''' Compare between two keys. This comparison function should returns: 
        ''' 
        ''' + 0, means two keys are equals.
        ''' + 1, means a is greater than b.
        ''' + -1, means a is smaller than b.
        ''' </param>
        ''' <param name="views">Display the key as string</param>
        ''' <remarks>
        ''' the binbox key is <typeparamref name="K"/> and 
        ''' value data type is <typeparamref name="V"/>.
        ''' </remarks>
        Sub New(compares As Comparison(Of K), Optional views As Func(Of K, String) = Nothing)
            MyBase.New(compares, views)
        End Sub

        Sub New(compares As IComparer(Of K), Optional views As Func(Of K, String) = Nothing)
            MyBase.New(AddressOf compares.Compare, views)
        End Sub

        ''' <summary>
        ''' add key related value into current tree
        ''' </summary>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <param name="valueReplace"></param>
        ''' <remarks>
        ''' get cluster value collection via the tree node ``values`` data
        ''' </remarks>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub Add(key As K, value As V, Optional valueReplace As Boolean = True)
            _root = Add(key, value, _root, Sub(node, newValue)
                                               If valueReplace Then
                                                   node.Value = newValue
                                               End If
                                           End Sub)
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub Add(key As K, value As V, handleDuplicated As DuplicatedKeyHandler(Of K, V))
            _root = Add(key, value, _root, handleDuplicated)
        End Sub

        Private Function Add(key As K, value As V, tree As BinaryTree(Of K, V), handleDuplicatedKey As DuplicatedKeyHandler(Of K, V)) As BinaryTree(Of K, V)
            If tree Is Nothing Then
                ' 追加新的叶子节点
                tree = New BinaryTree(Of K, V)(key, value, Nothing, views)
                stack.Add(tree)
            Else
                Select Case compares(key, tree.Key)
                    Case < 0 : Call appendLeft(tree, key, value, handleDuplicatedKey)
                    Case > 0 : Call appendRight(tree, key, value, handleDuplicatedKey)
                    Case = 0

                        ' 将value追加到附加值中（也可对应重复元素）
                        Call handleDuplicatedKey(tree, value)

                        ' 2018.3.6
                        ' 如果是需要使用二叉树进行聚类操作，那么等于零的值可能都是同一个簇之中的
                        ' 在这里将这个member添加进来
                        Call DirectCast(tree!values, List(Of V)).Add(value)

                    Case Else
                        ' This will never happend!
                        Throw New Exception("????")
                End Select
            End If

            tree.PutHeight

            Return tree
        End Function

        Private Sub appendRight(ByRef tree As BinaryTree(Of K, V), key As K, value As V, handleDuplicatedKey As DuplicatedKeyHandler(Of K, V))
            tree.Right = Add(key, value, tree.Right, handleDuplicatedKey)

            If tree.Right.height - tree.Left.height = 2 Then
                If compares(key, tree.Right.Key) > 0 Then
                    tree = tree.RotateRR
                Else
                    tree = tree.RotateRL
                End If
            End If
        End Sub

        Private Sub appendLeft(ByRef tree As BinaryTree(Of K, V), key As K, value As V, handleDuplicatedKey As DuplicatedKeyHandler(Of K, V))
            tree.Left = Add(key, value, tree.Left, handleDuplicatedKey)

            If tree.Left.height - tree.Right.height = 2 Then
                If compares(key, tree.Left.Key) < 0 Then
                    tree = tree.RotateLL
                Else
                    tree = tree.RotateLR
                End If
            End If
        End Sub

        ''' <summary>
        ''' This function will returns nothing if <paramref name="term"/> not found in current tree.
        ''' </summary>
        ''' <param name="term"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function Find(term As K) As BinaryTree(Of K, V)
            Return root.Find(key:=term, compares:=compares)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub Remove(key As K)
            _root = Remove(key, _root)
        End Sub

        Public Function Remove(key As K, tree As BinaryTree(Of K, V)) As BinaryTree(Of K, V)
            If tree Is Nothing Then
                Return Nothing
            End If

            Select Case compares(key, tree.Key)
                Case < 0 : Call removeLeft(tree, key)
                Case > 0 : Call removeRight(tree, key)
                Case = 0 : Call removeCurrent(tree)
                Case Else
                    ' This will never happed!
                    Throw New Exception("This will never happed!")
            End Select

            If Not tree Is Nothing Then
                Call tree.PutHeight
            End If

            Return tree
        End Function

        Private Sub removeLeft(ByRef tree As BinaryTree(Of K, V), key As K)
            tree.Left = Remove(key, tree.Left)

            If tree.Left.height - tree.Right.height = 2 Then
                If compares(key, tree.Left.Key) < 0 Then
                    tree = tree.RotateLL
                Else
                    tree = tree.RotateLR
                End If
            End If
        End Sub

        Private Sub removeRight(ByRef tree As BinaryTree(Of K, V), key As K)
            tree.Right = Remove(key, tree.Right)

            If tree.Right.height - tree.Left.height = 2 Then
                If compares(key, tree.Right.Key) > 0 Then
                    tree = tree.RotateRR
                Else
                    tree = tree.RotateRL
                End If
            End If
        End Sub

        Private Sub removeCurrent(ByRef tree As BinaryTree(Of K, V))
            If Not tree.Left Is Nothing AndAlso Not tree.Right Is Nothing Then

                tree = New BinaryTree(Of K, V)(tree.Right.MinKey, tree.Value) With {
                    .Left = tree.Left,
                    .Right = tree.Right
                }
                tree.Right = Remove(tree.Key, tree.Right)

            Else
                tree = If(tree.Left Is Nothing, tree.Right, tree.Left)

                If tree Is Nothing Then
                    tree = Nothing
                End If
            End If
        End Sub
    End Class
End Namespace
