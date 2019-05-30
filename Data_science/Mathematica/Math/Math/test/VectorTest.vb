﻿#Region "Microsoft.VisualBasic::0aac4bbf953ccee74dd29e697483e3a8, Data_science\Mathematica\Math\Math\test\VectorTest.vb"

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
'     Sub: Main
' 
' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Serialization.JSON
Imports numpy = Microsoft.VisualBasic.Math.NumpyExtensions

Module VectorTest

    Sub Main()
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
