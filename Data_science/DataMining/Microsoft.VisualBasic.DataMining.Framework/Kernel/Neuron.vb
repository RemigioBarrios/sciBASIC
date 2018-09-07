﻿#Region "Microsoft.VisualBasic::8246b597a3904d0388a575bdd6b8edce, Data_science\DataMining\Microsoft.VisualBasic.DataMining.Framework\Kernel\Neuron.vb"

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

    '     Class Neuron
    ' 
    '         Properties: Length, OutputFunction, W
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: Output, ToString, Train
    '         Class Entity
    ' 
    '             Properties: Properties, Width, Y
    ' 
    '             Function: ToString
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text
Imports System.Xml.Serialization

Namespace Kernel.Classifier

    Public Class Neuron

        Public Property W As Double()

        ''' <summary>
        ''' Weights, Entity, OutputValue
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property OutputFunction As System.Func(Of Generic.IEnumerable(Of Double), Generic.IEnumerable(Of Double), Double)

        Public Function Output(Entity As Double()) As Double
            Return Me.OutputFunction()(W, Entity)
        End Function

        Public ReadOnly Property Length As Integer
            Get
                Return W.Count
            End Get
        End Property

        Sub New(Length As Integer, Output As System.Func(Of Generic.IEnumerable(Of Double), Generic.IEnumerable(Of Double), Double))
            W = ComponentModel.Vector.Randomize(Length).ToArray
            Me.OutputFunction = Output
        End Sub

        Public Overrides Function ToString() As String
            Return W.ToString
        End Function

        Public Shared Function Train(Neuron As Neuron, Entities As Generic.IEnumerable(Of Entity), Optional Sigma As Double = 0.5, Optional Lambda As Double = 1.5) As Double()
            Dim ErrList As List(Of Double) = New List(Of Double)
            Do Until (From sgm In ErrList Where sgm <= Sigma Select 1).ToArray.Count / Entities.Count > 0.9
                For Each Entity In Entities
                    Dim y1 = Neuron.Output(Entity.Properties)
                    Dim d As Double = Entity.Y - y1
                    For i As Integer = 0 To Neuron.Length - 1
                        Neuron.W(i) = Neuron.W(i) + Lambda * d * Entity.Properties(i)
                    Next
                    Call ErrList.Add(d)
                Next
            Loop
            Return ErrList.ToArray
        End Function

        Public Class Entity

            <XmlAttribute> Public Property Properties As Double()
            <XmlAttribute> Public Property Y As Double

            Public Overrides Function ToString() As String
                Dim sBuilder As System.Text.StringBuilder = New StringBuilder(1024)
                For Each p As Integer In Properties
                    Call sBuilder.AppendFormat("{0}, ", p)
                Next
                Call sBuilder.Remove(sBuilder.Length - 1, length:=1)

                Return String.Format("<{0}> --> {1}", sBuilder.ToString, Y)
            End Function

            Default Public ReadOnly Property Item(Index As Integer) As Integer
                Get
                    Return Properties(Index)
                End Get
            End Property

            Public ReadOnly Property Width As Integer
                Get
                    Return Properties.Count
                End Get
            End Property

            Public Shared Widening Operator CType(properties As Double()) As Entity
                Return New Entity With {.Properties = properties}
            End Operator

            Public Shared Widening Operator CType(properties As Integer()) As Entity
                Return New Entity With {.Properties = (From n In properties Select CType(n, Double)).ToArray}
            End Operator
        End Class
    End Class
End Namespace
