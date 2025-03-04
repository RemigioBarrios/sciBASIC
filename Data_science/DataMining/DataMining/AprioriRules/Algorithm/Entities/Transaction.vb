﻿#Region "Microsoft.VisualBasic::3145582edf54a2b53a31cacd2168be8e, sciBASIC#\Data_science\DataMining\DataMining\AprioriRules\Algorithm\Entities\Transaction.vb"

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

    '   Total Lines: 40
    '    Code Lines: 33
    ' Comment Lines: 0
    '   Blank Lines: 7
    '     File Size: 1.40 KB


    '     Structure Transaction
    ' 
    '         Properties: Items, Name
    ' 
    '         Function: ToString
    ' 
    '     Module TransactionExtensions
    ' 
    '         Function: AllItems, BuildTransactions
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Linq

Namespace AprioriRules.Entities

    Public Structure Transaction

        Public Property Name As String
        Public Property Items As String()

        Public Overrides Function ToString() As String
            Return $"{Name} = {{ {Items.JoinBy(", ")} }}"
        End Function
    End Structure

    Public Module TransactionExtensions

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function BuildTransactions(data As IEnumerable(Of NamedValue(Of String()))) As IEnumerable(Of Transaction)
            Return data _
                .SafeQuery _
                .Select(Function(t)
                            Return New Transaction With {
                                .Name = t.Name,
                                .Items = t.Value
                            }
                        End Function)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function AllItems(transactions As IEnumerable(Of Transaction)) As IEnumerable(Of String)
            Return transactions _
                .Select(Function(t) t.Items) _
                .IteratesALL
        End Function
    End Module
End Namespace
