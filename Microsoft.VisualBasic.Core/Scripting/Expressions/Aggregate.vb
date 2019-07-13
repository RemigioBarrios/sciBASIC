﻿#Region "Microsoft.VisualBasic::10c538a5029327a24db621be937c1762, Microsoft.VisualBasic.Core\Scripting\Expressions\Aggregate.vb"

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

    '     Module Aggregate
    ' 
    '         Function: GetAggregateFunction
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Namespace Scripting.Expressions

    Public Module Aggregate

        ''' <summary>
        ''' Get ``Aggregate`` function by term.
        ''' </summary>
        ''' <param name="name$">
        ''' + max
        ''' + min
        ''' + average
        ''' </param>
        ''' <returns></returns>
        ''' 
        <Extension> Public Function GetAggregateFunction(name$) As Func(Of IEnumerable(Of Double), Double)
            Select Case LCase(name)
                Case "max"
                    Return Function(x) x.Max
                Case "min"
                    Return Function(x) x.Min
                Case "average", "avg", "mean"
                    Return Function(x) x.Average

                Case Else
                    Throw New NotImplementedException(name)
            End Select
        End Function
    End Module
End Namespace
