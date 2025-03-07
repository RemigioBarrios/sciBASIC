﻿#Region "Microsoft.VisualBasic::c66aba300e4d57f62a82db671b3377c5, sciBASIC#\Data_science\MachineLearning\DeepLearning\CNN\Layers\ReLU.vb"

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

    '   Total Lines: 51
    '    Code Lines: 37
    ' Comment Lines: 0
    '   Blank Lines: 14
    '     File Size: 1.36 KB


    '     Class ReLU
    ' 
    '         Properties: type
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: layerFeedNext
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Convolutional

    Friend Class ReLU : Inherits Layer

        Public Overrides ReadOnly Property type As CNN.LayerTypes
            Get
                Return CNN.LayerTypes.ReLU
            End Get
        End Property

        Public Sub New(inputTensorDims As Integer())
            Call MyBase.New(inputTensorDims)
        End Sub

        Protected Overrides Function layerFeedNext() As Layer
            Dim inputHeight = inputTensorDims(0)
            Dim inputWidth = inputTensorDims(1)
            Dim channelCount = inputTensorDims(2)
            Dim f As Single
            Dim inputInd = New Integer() {0, 0, 0}

            While inputInd(0) < inputHeight
                inputInd(1) = 0

                While inputInd(1) < inputWidth
                    inputInd(2) = 0

                    While inputInd(2) < channelCount
                        f = inputTensor(inputInd)

                        If f < 0 Then
                            f = 0
                        End If

                        Call writeNextLayerInput(inputInd, f)

                        inputInd(2) += 1
                    End While

                    inputInd(1) += 1
                End While

                inputInd(0) += 1
            End While

            Call disposeInputTensor()

            Return Me
        End Function
    End Class
End Namespace
