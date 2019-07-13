﻿#Region "Microsoft.VisualBasic::f77cfba2f205b0f6ca5674da03cba129, tutorials\core.test\toStringTest.vb"

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

    ' Module toStringTest
    ' 
    '     Function: populatestrings
    ' 
    '     Sub: greekTokens, Main, reflectionTest
    '     Class NoCStr
    ' 
    '         Function: ToString
    ' 
    '     Class hasCStr
    ' 
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Threading
Imports Microsoft.VisualBasic.Scripting.Runtime
Imports Microsoft.VisualBasic.Text

Module toStringTest

    Sub greekTokens()

        ' GreekAlphabets.[New]()

        Dim test$ = "&alpha;--------&beta;---------------&yyyyyy;gdfgdasderrewrwerw&gamma;."
        Dim unescaped = test.AlphabetUnescape(("&", ";"))

        Call test.__DEBUG_ECHO
        Call unescaped.__DEBUG_ECHO

        Pause()
    End Sub


    Sub Main()

        Call greekTokens()
        ' Call reflectionTest()

        Dim list1 = populatestrings(True)
        Dim list2 = populatestrings(False)
        Dim strings$()

        Call BENCHMARK(Sub() strings = list1.Select(AddressOf Scripting.ToString).ToArray)
        Call BENCHMARK(Sub() strings = list2.Select(AddressOf Scripting.ToString).ToArray)
        Call BENCHMARK(Sub() strings = list1.Select(Function(x) x.ToString).ToArray)


        '   Call BENCHMARK(Sub() CStr(list1(0)))
        Call BENCHMARK(Sub() strings = {CStr(DirectCast(list2(0), hasCStr))})

        '   Call BENCHMARK(Sub() CType(list1(0), String))
        Call BENCHMARK(Sub() strings = {CType(DirectCast(list2(0), hasCStr), String)})

        Pause()
    End Sub

    Private Sub reflectionTest()

        Dim type = GetType(NoCStr)
        Dim type2 = GetType(hasCStr)


        Dim op = type.GetNarrowingOperator(Of String)

        '  Console.WriteLine(op(New NoCStr))

        op = type2.GetNarrowingOperator(Of String)

        Console.WriteLine(op(New hasCStr))

        Pause()
    End Sub

    Public Iterator Function populatestrings(no As Boolean) As IEnumerable(Of NoCStr)
        For i As Integer = 0 To 2000
            If no Then
                Yield New NoCStr
            Else
                Yield New hasCStr
            End If

            ' Thread.Sleep(10)
        Next
    End Function

    Public Class NoCStr

        Dim s As String = RandomASCIIString(5, skipSymbols:=True)

        Public Overrides Function ToString() As String
            Return s
        End Function
    End Class

    Public Class hasCStr : Inherits NoCStr

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Narrowing Operator CType(h As hasCStr) As String
            Return h.ToString
        End Operator
    End Class
End Module
