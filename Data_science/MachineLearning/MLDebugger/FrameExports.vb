﻿Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.IO.netCDF
Imports Microsoft.VisualBasic.Data.IO.netCDF.Components
Imports Microsoft.VisualBasic.DataMining.ComponentModel.Normalizer
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.NeuralNetwork.StoreProcedure
Imports Basic = Microsoft.VisualBasic.Language.Runtime
Imports DataFrame = Microsoft.VisualBasic.Data.csv.IO.DataFrame
Imports Excel = Microsoft.VisualBasic.Data.csv.IO.DataSet

Public Module FrameExports

    <Extension>
    Public Function NormalizeSample(samples As DataSet, method As Methods) As Excel()
        Dim matrix As Sample() = samples.PopulateNormalizedSamples(method).ToArray
        Dim names = samples.NormalizeMatrix.names.SeqIterator.ToArray
        Dim dataset = matrix _
            .Select(Function(r)
                        Return New Excel With {
                            .ID = r.ID,
                            .Properties = names.ToDictionary(
                                Function(name) name.value,
                                Function(name)
                                    Return r.status(name)
                                End Function)
                        }
                    End Function) _
            .ToArray

        Return dataset
    End Function

    ''' <summary>
    ''' 导出误差率曲线数据
    ''' </summary>
    ''' <returns></returns>
    Public Function ExportErrorCurve(cdf As netCDFReader) As DataFrame
        Dim errors = cdf.getDataVariable("fitness").numerics
        Dim index = cdf.getDataVariable("iterations").integers

        With New Basic
            Return New DataFrame(!iterations = index, !fitness = errors)
        End With
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function GetTimeIndex(cdf As netCDFReader) As String()
        Return cdf.getDataVariable("iterations") _
            .integers _
            .Select(Function(i) $"T{i}") _
            .ToArray
    End Function

    Public Iterator Function ExportValueFrames(cdf As netCDFReader) As IEnumerable(Of Excel)
        Dim nodes As variable() = cdf.variables _
            .Where(Function(var)
                       Dim isANeuron As attribute = var _
                           .attributes _
                           .FirstOrDefault(Function(a)
                                               Return a.name = "type" AndAlso a.value = "neuron"
                                           End Function)

                       Return Not isANeuron Is Nothing
                   End Function) _
            .ToArray
        Dim times = FrameExports.GetTimeIndex(cdf) _
            .SeqIterator _
            .ToArray
        Dim row As Excel
        Dim nodeValue As Double()

        For Each node As variable In nodes
            nodeValue = cdf.getDataVariable(node).numerics
            row = New Excel With {
                .ID = node.name,
                .Properties = times.ToDictionary(
                    Function(tag) tag.value,
                    Function(i)
                        Return nodeValue.ElementAtOrDefault(i, [default]:=Double.NaN)
                    End Function)
            }

            Yield row
        Next
    End Function
End Module
