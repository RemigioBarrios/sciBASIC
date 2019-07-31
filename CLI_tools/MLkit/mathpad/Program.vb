﻿#Region "Microsoft.VisualBasic::b4d82f54970726f4abaea67353f4c006, CLI_tools\MLkit\mathpad\Program.vb"

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

    ' Module Program
    ' 
    '     Function: runMathpad
    ' 
    '     Sub: Main
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Math.Scripting

Module Program

    Sub Main()
        Call runMathpad()
    End Sub

    Private Function runMathpad() As Integer
        Dim ans As String
        Dim cmdl As String = String.Empty

        '(log(max(sinh(((1-2-3+4+5+6+7+8+9)-20)^0.5)+5,rnd(-10, 100)))!%5)^3!

        Do While cmdl <> ".quit"
            Console.Write("# ")

            cmdl = Console.ReadLine
            ans = ScriptEngine.Shell(cmdl)

            If Not String.IsNullOrEmpty(ans) Then
                Console.WriteLine("  = {0}", ans)
            End If
        Loop

        Return 0
    End Function
End Module

