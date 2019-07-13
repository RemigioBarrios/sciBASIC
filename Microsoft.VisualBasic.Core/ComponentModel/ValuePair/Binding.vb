﻿#Region "Microsoft.VisualBasic::00ff821a099908e75295b177c069fae6, Microsoft.VisualBasic.Core\ComponentModel\ValuePair\Binding.vb"

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

    '     Structure Binding
    ' 
    '         Properties: IsEmpty
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: ToString, Tuple, ValueTuple
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language.Default

Namespace ComponentModel

    ''' <summary>
    ''' Functioning the same as the <see cref="KeyValuePair(Of T, K)"/>, but with more specific on the name. 
    ''' <see cref="KeyValuePair(Of T, K)"/> its name is too generic.
    ''' (作用与<see cref="KeyValuePair(Of T, K)"/>类似，只不过类型的名称更加符合绑定的描述)
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <typeparam name="K"></typeparam>
    Public Structure Binding(Of T, K)
        Implements IsEmpty

        Dim Bind As T
        Dim Target As K

        ''' <summary>
        ''' If the field <see cref="Bind"/> and <see cref="Target"/> are both nothing, then this binding is empty.
        ''' (当<see cref="Bind"/>以及<see cref="Target"/>都同时为空值的时候这个参数才会为真)
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsEmpty As Boolean Implements IsEmpty.IsEmpty
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return Bind Is Nothing AndAlso Target Is Nothing
            End Get
        End Property

        Sub New(source As T, target As K)
            Me.Bind = source
            Me.Target = target
        End Sub

        Public Overrides Function ToString() As String
            If IsEmpty Then
                Return "No binding"
            Else
                Return Bind.ToString & " --> " & Target.ToString
            End If
        End Function

        ''' <summary>
        ''' Convert this binding to tuple
        ''' </summary>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function Tuple() As Tuple(Of T, K)
            Return Me
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function ValueTuple() As (bind As T, target As K)
            Return (Bind, Target)
        End Function

        ''' <summary>
        ''' Implicit convert this binding as the <see cref="System.Tuple(Of T, K)"/>
        ''' </summary>
        ''' <param name="b"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Narrowing Operator CType(b As Binding(Of T, K)) As Tuple(Of T, K)
            Return New Tuple(Of T, K)(b.Bind, b.Target)
        End Operator
    End Structure
End Namespace
