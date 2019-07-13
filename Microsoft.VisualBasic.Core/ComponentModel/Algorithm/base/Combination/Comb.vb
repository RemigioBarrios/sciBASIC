﻿#Region "Microsoft.VisualBasic::1696f908c778b7346677fded1a4d746b, Microsoft.VisualBasic.Core\ComponentModel\Algorithm\base\Combination\Comb.vb"

    ' Author:
    ' 
    ' 
    ' 
    ' 
    ' 
    ' 
    ' 



    ' /********************************************************************************/

    ' Summaries:

    '     Class Comb
    ' 
    '         Properties: CombList, EOL, NewLine
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: __internalQuery, CreateCompleteObjectPairs, CreateObject, GetObjectPair, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq

Namespace ComponentModel.Algorithm.base

    ''' <summary>
    ''' 对象类型的组合输出工具，即目标类型的集合之中的元素两两组合配对
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Comb(Of T)

        Dim source As List(Of T)
        Dim p As Integer = 1

        ''' <summary>
        ''' 对象列表是否已经完全组合输出
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property EOL As Boolean
            Get
                Return Not source.Count >= 2
            End Get
        End Property

        ''' <summary>
        ''' 是否已经开始读取新的一行数据
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property NewLine As Boolean = True

        Public ReadOnly Property CombList As Tuple(Of T, T)()()
            Get
                Dim out As Tuple(Of T, T)()() = __internalQuery.ToArray
                Return out
            End Get
        End Property

        Private Function __internalQuery() As IEnumerable(Of Tuple(Of T, T)())
            Return From index As Integer
                   In source.Sequence _
                       .Take(source.Count - 1) _
                       .AsParallel
                   Let combTuples = {
 _
                       From offset As Integer
                       In source.Skip(index + 1).Sequence
                       Let b = index + 1
                       Select New Tuple(Of T, T)(source(index), source(b + offset))
                   }
                   Select array = combTuples _
                       .IteratesALL _
                       .ToArray
                   Order By array.Length Descending
        End Function

        Public Function GetObjectPair() As (T, T)
            If source.Count = 1 Then
                Return Nothing
            End If

            If p < source.Count Then
                Dim o As T = source(p)
                _NewLine = False
                p += 1
                Return (source(0), o)
            Else
                source.RemoveAt(0)
                p = 1
                Dim pair As (T, T) = GetObjectPair()
                _NewLine = True

                Return pair
            End If
        End Function

        Friend Sub New()
        End Sub

        Public Shared Function CreateObject(source As IEnumerable(Of T)) As Comb(Of T)
            Return New Comb(Of T) With {
                .source = source.AsList
            }
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("Comb(Of {0})::There is {1} object last.", GetType(T).FullName, source.Count)
        End Function

        Public Shared Widening Operator CType(source As T()) As Comb(Of T)
            Return New Comb(Of T) With {
                .source = source.AsList
            }
        End Operator

        Public Shared Widening Operator CType(source As List(Of T)) As Comb(Of T)
            Return New Comb(Of T) With {
                .source = source
            }
        End Operator

        ''' <summary>
        ''' Creates the completely combination of the elements in the target input collection source.
        ''' (创建完完全全的两两配对)
        ''' </summary>
        ''' <param name="source"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Iterator Function CreateCompleteObjectPairs(source As IEnumerable(Of T)) As IEnumerable(Of Tuple(Of T, T)())
            Dim array As T() = source.ToArray

            For Each i As T In array
                Dim tmp As New List(Of Tuple(Of T, T))

                For Each j As T In array
                    tmp += New Tuple(Of T, T)(i, j)
                Next

                Yield tmp.ToArray
            Next
        End Function
    End Class
End Namespace
