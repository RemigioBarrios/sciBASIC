﻿Imports MathLambda
Imports Microsoft.VisualBasic.MIME.application.xml.MathML

Module Module1

    Sub Main()
        Dim exp As LambdaExpression = LambdaExpression.FromMathML("D:\GCModeller\src\runtime\sciBASIC#\Data_science\Mathematica\Math\MathLambda\mathML.xml".ReadAllText)
        Dim func = Compiler.CreateLambda(exp)
        Dim del As Func(Of Double, Double, Double, Double, Double, Double, Double) = func.Compile()

        Console.WriteLine(func)
        Console.WriteLine(del(1, 0, 0.1, 0.01, 99, 10))

        Pause()
    End Sub

End Module
