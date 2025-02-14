﻿#Region "Microsoft.VisualBasic::dc9dc729766920e14c2a726e3b9d4231, sciBASIC#\Data_science\MachineLearning\MachineLearning.Data.Extensions\Extensions.vb"

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

    '   Total Lines: 71
    '    Code Lines: 56
    ' Comment Lines: 5
    '   Blank Lines: 10
    '     File Size: 3.25 KB


    ' Module Extensions
    ' 
    '     Function: ExportQTable, GetInput
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.DataStorage.netCDF
Imports Microsoft.VisualBasic.DataStorage.netCDF.Components
Imports Microsoft.VisualBasic.DataStorage.netCDF.Data
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.ComponentModel.StoreProcedure
Imports Microsoft.VisualBasic.MachineLearning.QLearning
Imports Microsoft.VisualBasic.MachineLearning.QLearning.DataModel
Imports row = Microsoft.VisualBasic.Data.csv.IO.DataSet

Public Module Extensions

    ''' <summary>
    ''' 从csv文件数据之中读取和当前的数据集一样的元素顺序的向量用于预测分析
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Function GetInput(dataset As DataSet, data As row) As Double()
        Return dataset _
            .NormalizeMatrix _
            .names _
            .Select(Function(key) data(key)) _
            .ToArray
    End Function

    <Extension>
    Public Function ExportQTable(Q As IQTable, features As IQStateFeatureSet, file As Stream) As Boolean
        Using cdf As New CDFWriter(file)
            Dim attrs As New List(Of attribute)

            attrs.Add(New attribute With {.name = NameOf(Q.ActionRange), .type = CDFDataTypes.INT, .value = Q.ActionRange})
            attrs.Add(New attribute With {.name = NameOf(Q.ExplorationChance), .type = CDFDataTypes.FLOAT, .value = Q.ExplorationChance})
            attrs.Add(New attribute With {.name = NameOf(Q.GammaValue), .type = CDFDataTypes.FLOAT, .value = Q.GammaValue})
            attrs.Add(New attribute With {.name = NameOf(Q.LearningRate), .type = CDFDataTypes.FLOAT, .value = Q.LearningRate})
            attrs.Add(New attribute With {.name = "QTable_size", .type = CDFDataTypes.INT, .value = Q.Table.Count})

            cdf.GlobalAttributes(attrs.ToArray)

            Dim featureSet As NamedValue(Of List(Of Double))() = features.stateFeatures _
                .JoinIterates(features.QValueNames) _
                .Select(Function(name)
                            Return New NamedValue(Of List(Of Double))(name, New List(Of Double))
                        End Function) _
                .ToArray

            For Each stat As Object In features.AllQStates
                Dim vStat As Double() = features.ExtractStateVector(stat)
                Dim solve As Single() = Q.Table(stat.ToString).Qvalues
                Dim i As Integer = 0

                For i = 0 To vStat.Length - 1
                    featureSet(i).Value.Add(vStat(i))
                Next
                For i = 0 To solve.Length - 1
                    featureSet(i + vStat.Length).Value.Add(solve(i))
                Next
            Next

            For Each vec In featureSet
                Call cdf.AddVector(vec.Name, vec.Value, New Dimension With {.name = $"sizeof_{vec.Name}", .size = vec.Value.Count})
            Next
        End Using

        Return True
    End Function
End Module
