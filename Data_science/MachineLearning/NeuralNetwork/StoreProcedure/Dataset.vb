﻿#Region "Microsoft.VisualBasic::0f08c40e05a4c9983db5703ac5514fcc, Data_science\MachineLearning\NeuralNetwork\StoreProcedure\Dataset.vb"

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

    '     Class Sample
    ' 
    '         Properties: ID, status, target
    ' 
    '         Constructor: (+2 Overloads) Sub New
    '         Function: ToString
    ' 
    '     Class DataSet
    ' 
    '         Properties: DataSamples, NormalizeMatrix
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Math.LinearAlgebra

Namespace NeuralNetwork.StoreProcedure

    ''' <summary>
    ''' The training dataset
    ''' </summary>
    Public Class Sample : Implements INamedValue

        ''' <summary>
        ''' 可选的数据集唯一标记信息
        ''' </summary>
        ''' <returns></returns>
        <XmlAttribute> Public Property ID As String Implements IKeyedEntity(Of String).Key

        ''' <summary>
        ''' Neuron network input parameters
        ''' </summary>
        ''' <returns></returns>
        Public Property status As Double()
        ''' <summary>
        ''' The network expected output values
        ''' </summary>
        ''' <returns></returns>
        Public Property target As Double()

        ''' <summary>
        ''' Create a new training dataset
        ''' </summary>
        ''' <param name="values">Neuron network input parameters</param>
        ''' <param name="targets">The network expected output values</param>
        Public Sub New(values#(), targets#())
            Me.status = values
            Me.target = targets
        End Sub

        ''' <summary>
        ''' Create a new empty training dataset
        ''' </summary>
        Sub New()
        End Sub

        Public Overrides Function ToString() As String
            Return $"{status.AsVector.ToString} => {target.AsVector.ToString}"
        End Function
    End Class

    ''' <summary>
    ''' A training dataset that stored in XML file.
    ''' </summary>
    Public Class DataSet : Inherits XmlDataModel

        Public Property DataSamples As Sample()
        Public Property NormalizeMatrix As NormalizeMatrix

    End Class
End Namespace
