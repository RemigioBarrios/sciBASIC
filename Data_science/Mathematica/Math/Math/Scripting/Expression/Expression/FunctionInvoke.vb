﻿Namespace Scripting.MathExpression.Impl

    Public Class FunctionInvoke : Inherits Expression

        Public Property funcName As String
        Public Property parameters As Expression()

        Sub New(name As String, parameters As Expression())
            Me.funcName = name
            Me.parameters = parameters
        End Sub

        Public Overrides Function Evaluate(env As ExpressionEngine) As Double
            Dim func As Func(Of Double(), Double) = env.GetFunction(funcName)
            Dim parameters As Double() = Me.parameters.Select(Function(x) x.Evaluate(env)).ToArray
            Dim result As Double = func(parameters)

            Return result
        End Function

        Public Overrides Function ToString() As String
            Return $"{funcName}({parameters.JoinBy(", ")})"
        End Function
    End Class
End Namespace