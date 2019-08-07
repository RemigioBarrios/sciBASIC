﻿#Region "Microsoft.VisualBasic::d8c29cc86662446cb5362b6bc3bd5b25, Data_science\Mathematica\Math\Math\test\VectorTest.vb"

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

' Module VectorTest
' 
'     Sub: Main, numpyTest
' 
' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ApplicationServices.Debugging
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Serialization.JSON
Imports numpy = Microsoft.VisualBasic.Math.NumpyExtensions

Module VectorTest

    Sub memoryTest()

        Dim testList As New List(Of Double) From {0.2333}

        Dim sizeTest = HeapSizeOf.MeasureSize(testList)

        Dim largeVector As New Vector(Replicate(0.0, 100000).JoinIterates(1))
        Dim sizeOfFull As Long = HeapSizeOf.MeasureSize(largeVector)

        Dim compactVector As New SparseVector(largeVector)
        Dim sizeOfCompact As Long = HeapSizeOf.MeasureSize(compactVector)

        Dim foldChange = sizeOfFull / sizeOfCompact

        Pause()
    End Sub


    Sub SparseVectorTest()

        SparseVector.Precision = 10

        Dim vector As Vector = Vector.rand(1000, -1000, 5).AsList + Repeats(0.0, 50)
        Dim spVector As New SparseVector(vector)

        Call Console.WriteLine($"memory of the normal vector: {HeapSizeOf.MeasureSize(vector)} bytes")
        Call Console.WriteLine($"memory of the compact vector: {HeapSizeOf.MeasureSize(spVector)} bytes")


        Dim avg1 = vector.Average
        Dim avg2 = spVector.Average

        Call Console.WriteLine($"average should be equals: {avg1} = {avg2} ? ({SparseVector.Equals(avg1, avg2)})")

        Dim x1 = vector + 1
        Dim x2 = spVector + 1

        avg1 = x1.Average
        avg2 = x2.Average

        Call Console.WriteLine($"average should be equals: {avg1} = {avg2} ? ({SparseVector.Equals(avg1, avg2)})")

        Call Console.WriteLine($"memory of the normal vector: {HeapSizeOf.MeasureSize(x1)} bytes")
        Call Console.WriteLine($"memory of the compact vector: {HeapSizeOf.MeasureSize(x2)} bytes")

        Dim s1 = vector.Sum
        Dim s2 = spVector.Sum

        Call Console.WriteLine($"Sum should be equals: {s1} = {s2} ? ({SparseVector.Equals(s1, s2)})")


        Call Console.WriteLine(vector.ToString)
        Call Console.WriteLine(spVector.ToString)

        Pause()
    End Sub

    Sub Main()

        Call SparseVectorTest()
        Call memoryTest()

        Dim aa As Vector = {0, 0, 0, 0}
        Dim taa = 1 / aa
        Dim NaN = aa * taa

        Dim inf = 1 / 0
        Dim nanQ = 0 * inf

        Pause()

        Dim x As Double() = {423, 4, 2, 4, 24, 2, 3, 423, 4, 2, 3, 4, 23, 4, 2, 4, 2, 3, 4, 2, 4, 2}
        Dim y As Vector = Vector.Call(Of Double)(New Func(Of Double, Double, Double)(AddressOf Math.Log), x, 2).ToArray
        Dim z As Vector = Vector.Call(Function(a, b) a / b, x, 1000000)

        Call numpyTest()

        Pause()
    End Sub

    Sub numpyTest()
        Dim a = {New Vector({1.0#, 2.0#}), New Vector({3.0#, 4.0#})}

        Console.WriteLine("mean")

        Console.WriteLine(numpy.Mean(a).ToString)
        Console.WriteLine(numpy.Mean(a, axis:=0).ToString)
        Console.WriteLine(numpy.Mean(a, axis:=1).ToString)

        Console.WriteLine("std")

        Console.WriteLine(numpy.Std(a).ToString)
        Console.WriteLine(numpy.Std(a, axis:=0).ToString)
        Console.WriteLine(numpy.Std(a, axis:=1).ToString)

        a = {New Vector({0#, 1.0#}), New Vector({0#, 5.0#})}

        Console.WriteLine("sum")

        Console.WriteLine(numpy.Sum(a).ToString)
        Console.WriteLine(numpy.Sum(a, axis:=0).ToString)
        Console.WriteLine(numpy.Sum(a, axis:=1).ToString)

        a = {New Vector({1.0#, 4.0#}), New Vector({3.0#, 1.0#})}

        Console.WriteLine("sort")

        Console.WriteLine(numpy.Sort(a).ToMatrix.ToVectorList.GetJson(indent:=True))
        Console.WriteLine(numpy.Sort(a, axis:=Nothing).ToMatrix.ToVectorList.GetJson)
        Console.WriteLine(numpy.Sort(a, axis:=0).ToMatrix.ToVectorList.GetJson(indent:=True))

        Pause()
    End Sub
End Module
