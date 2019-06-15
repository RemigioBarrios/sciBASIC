﻿#Region "Microsoft.VisualBasic::3d96b82f7862b8756cb1d5c0e9624981, Data_science\MachineLearning\MachineLearning\test\FileTest.vb"

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

    ' Module FileTest
    ' 
    '     Sub: Main
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.MachineLearning.NeuralNetwork.StoreProcedure
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Text.Xml.Models
Imports Microsoft.VisualBasic.MachineLearning.NeuralNetwork
Imports Microsoft.VisualBasic.Serialization.JSON

Module FileTest
    Sub Main()

        Dim samples As New List(Of Sample)
        Dim id As VBInteger = 0

        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 0, 1, 1}}, .target = {0, 0, 0, 0}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 1}}, .target = {1, 1, 1, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 0}}, .target = {0, 0, 0, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 0}}, .target = {0, 0, 0, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 0}}, .target = {0, 0, 0, 1}}
        samples += New Sample With {.ID = ++id, .status = New NumericVector With {.vector = {1, 1, 1, 1, 0}}, .target = {0, 0, 0, 1}}

        Dim trainer As New TrainingUtils(5, {10, 100, 30, 50}, 4)

        Helpers.MaxEpochs = 1000

        Dim snapshot As New Snapshot(trainer.NeuronNetwork)

        Call snapshot.UpdateSnapshot(1000).WriteIntegralXML("./test_before.XML")

        Call samples.DoEach(Sub(dset) trainer.Add(dset))
        Call trainer.Train()

        Call snapshot.UpdateSnapshot(trainer.MinError).WriteIntegralXML("./test_after.XML")


        Call trainer.TakeSnapshot.GetXml.SaveTo("./format1.Xml")
        Call trainer.TakeSnapshot.GetJson.SaveTo("./format2.json")

        Call trainer.TakeSnapshot.ScatteredStore("./scatters/")

        Dim model1 = Scattered.ScatteredLoader("./scatters/").LoadModel
        Dim model2 = "./format1.Xml".LoadXml(Of StoreProcedure.NeuralNetwork).LoadModel

        Dim predict1 = model1.Compute(1, 1, 1, 1, 0)
        Dim predict2 = model2.Compute(1, 1, 1, 1, 0)

        Call StoreProcedure.NeuralNetwork.Snapshot(model1).GetXml.SaveTo("./scatterLoaded.Xml")
        Call StoreProcedure.NeuralNetwork.Snapshot(model2).GetXml.SaveTo("./interalLoaded.Xml")

        Pause()
    End Sub
End Module
