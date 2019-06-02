﻿#Region "Microsoft.VisualBasic::787fba7abb48e5c3818586e8aa186fe8, Data_science\MachineLearning\MachineLearning\NeuralNetwork\TrainingUtils.vb"

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

    '     Class TrainingUtils
    ' 
    '         Properties: MinError, NeuronNetwork, Selective, TrainingSet, TrainingType
    '                     Truncate, XP
    ' 
    '         Constructor: (+2 Overloads) Sub New
    ' 
    '         Function: CalculateError, TakeSnapshot, trainingImpl
    ' 
    '         Sub: (+2 Overloads) Add, (+2 Overloads) Corrects, RemoveLast, (+3 Overloads) Train
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.MachineLearning.NeuralNetwork.Activations
Imports Microsoft.VisualBasic.MachineLearning.NeuralNetwork.StoreProcedure
Imports Microsoft.VisualBasic.Terminal.ProgressBar
Imports Microsoft.VisualBasic.Text

Namespace NeuralNetwork

    ''' <summary>
    ''' Tools for training the neuron network
    ''' </summary>
    Public Class TrainingUtils : Inherits IterationReporter(Of Network)

        Public Property TrainingType As TrainingType = TrainingType.Epoch
        Public Property MinError As Double = Helpers.MinimumError
        ''' <summary>
        ''' 对<see cref="Neuron.Gradient"/>的剪裁限制阈值，小于等于零表示不进行剪裁，默认不剪裁
        ''' </summary>
        ''' <returns></returns>
        Public Property Truncate As Double = -1
        ''' <summary>
        ''' 是否对训练样本数据集进行选择性的训练，假若目标样本在当前所训练的模型上面
        ''' 所计算得到的预测结果和其真实结果的误差足够小的话，目标样本将不会再进行训练
        ''' </summary>
        ''' <returns></returns>
        Public Property Selective As Boolean = True

        ''' <summary>
        ''' 最终得到的训练结果神经网络
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NeuronNetwork As Network
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return network
            End Get
        End Property

        Public ReadOnly Property TrainingSet As Sample()
            Get
                Return _dataSets.ToArray
            End Get
        End Property

        ReadOnly _dataSets As New List(Of Sample)
        ReadOnly network As Network

        ''' <summary>
        ''' 模型当前的训练误差
        ''' </summary>
        Dim errors#

        ''' <summary>
        ''' 训练所使用到的经验数量,即数据集的大小s
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property XP As Integer
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return _dataSets.Count
            End Get
        End Property

        Sub New(net As Network)
            network = net
        End Sub

        ''' <summary>
        ''' 以指定的网络规模参数进行人工神经网络模型的构建
        ''' </summary>
        ''' <param name="inputSize"></param>
        ''' <param name="hiddenSize"></param>
        ''' <param name="outputSize"></param>
        ''' <param name="learnRate"></param>
        ''' <param name="momentum"></param>
        ''' <param name="active"></param>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub New(inputSize As Integer, hiddenSize As Integer(), outputSize As Integer,
                       Optional learnRate As Double = 0.1,
                       Optional momentum As Double = 0.9,
                       Optional active As LayerActives = Nothing)
            Call Me.New(New Network(inputSize, hiddenSize, outputSize, learnRate, momentum, active))
        End Sub

        ''' <summary>
        ''' 将训练的成果状态进行快照,转换为可以保存的XML文件对象
        ''' </summary>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function TakeSnapshot() As StoreProcedure.NeuralNetwork
            Return StoreProcedure.NeuralNetwork.Snapshot(network, errors)
        End Function

        Public Sub RemoveLast()
            If Not _dataSets.Count = 0 Then
                Call _dataSets.RemoveLast
            End If
        End Sub

        ''' <summary>
        ''' 在这里添加训练使用的数据集
        ''' (请注意,因为ANN的output结果向量只输出``[0,1]``之间的结果,所以在训练的时候,output应该是被编码为0或者1的;
        ''' input可以接受任意实数的向量,但是这个要求所有属性都应该在同一个scale区间内,所以最好也归一化编码为0到1之间的小数)
        ''' </summary>
        ''' <param name="input"></param>
        ''' <param name="output"></param>
        Public Sub Add(input As Double(), output As Double())
            Call _dataSets.Add(New Sample(input, output))
        End Sub

        Public Sub Add(x As Sample)
            Call _dataSets.Add(x)
        End Sub

        ''' <summary>
        ''' 开始进行训练
        ''' </summary>
        ''' <param name="parallel">
        ''' 小型的人工神经网络的训练,并不建议使用并行化
        ''' </param>
        Public Overrides Sub Train(Optional parallel As Boolean = False)
            Dim trainingDataSet As Sample() = _dataSets.ToArray

            If TrainingType = TrainingType.Epoch Then
                Call Train(trainingDataSet, Helpers.MaxEpochs, parallel)
            Else
                Call Train(trainingDataSet, minimumError:=Helpers.MinimumError, parallel:=parallel)
            End If
        End Sub

#Region "-- Training --"
        Public Overloads Sub Train(dataSets As Sample(), numEpochs As Integer, Optional parallel As Boolean = False)
            Using progress As New ProgressBar("Training ANN...")
                Dim tick As New ProgressProvider(numEpochs)
                Dim msg$
                Dim ETA$

                For i As Integer = 0 To numEpochs - 1
                    errors = trainingImpl(dataSets, parallel, Selective)
                    ETA = $"ETA: {tick.ETA(progress.ElapsedMilliseconds).FormatTime}"
                    msg = $"Iterations: [{i}/{numEpochs}], errors={errors}{vbTab}learn_rate={network.LearnRate} {ETA}"
                    progress.SetProgress(tick.StepProgress, msg)

                    If errors < 0.0001 Then
                        Selective = False
                    End If

                    If Not reporter Is Nothing Then
                        Call reporter(i, errors, network)
                    End If
                Next
            End Using
        End Sub

        Private Function trainingImpl(dataSets As Sample(), parallel As Boolean, selective As Boolean) As Double
            Dim errors As New List(Of Double)()
            Dim err#
            Dim outputSize% = dataSets(Scan0).target.Length

            For Each dataSet As Sample In dataSets
                If selective Then
                    ' 如果是只针对误差较大的训练样本进行训练的话,则在这里会首先计算
                    ' 当前的训练样本的误差,如果当前的训练样本误差比较小的话,就
                    ' 不再进行当前的样本的训练了
                    ' sum
                    err = CalculateError(network, dataSet.target)
                    ' means
                    If err / outputSize <= 0.05 Then
                        ' skip current sample
                        Call errors.Add(err)
                        Continue For
                    End If
                End If

                ' 下面的两步代码调用完成一个样本的训练操作:
                ' 首先根据当前样本进行计算
                ' 然后根据误差调整响应节点的权重
                Call network.ForwardPropagate(dataSet.status, parallel)
                Call network.BackPropagate(dataSet.target, Truncate, parallel)
                Call errors.Add(CalculateError(network, dataSet.target))
            Next

            Return errors.Average
        End Function

        Public Overloads Sub Train(dataSets As Sample(), minimumError As Double, Optional parallel As Boolean = False)
            Dim [error] = 1.0
            Dim numEpochs = 0
            Dim progress$

            While [error] > minimumError AndAlso numEpochs < Integer.MaxValue
                [error] = trainingImpl(dataSets, parallel, True)
                numEpochs += 1
                progress = ((minimumError / [error]) * 100).ToString("F2")

                Call $"{numEpochs}{ASCII.TAB}Error:={[error]}{ASCII.TAB}progress:={progress}%".__DEBUG_ECHO

                If Not reporter Is Nothing Then
                    Call reporter(numEpochs, [error], network)
                End If
            End While
        End Sub

        ''' <summary>
        ''' 预测值与目标值之间的误差的绝对值之和
        ''' </summary>
        ''' <param name="neuronNetwork"></param>
        ''' <param name="targets"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Friend Shared Function CalculateError(neuronNetwork As Network, targets As Double()) As Double
            Return neuronNetwork.OutputLayer _
                .Neurons _
                .Select(Function(n, i)
                            Return Math.Abs(n.CalculateError(targets(i)))
                        End Function) _
                .Sum()
        End Function
#End Region

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="input">The inputs data</param>
        ''' <param name="convertedResults">The error outputs</param>
        ''' <param name="expectedResults">The corrects output</param>
        Public Sub Corrects(input As Double(), convertedResults As Double(), expectedResults As Double(),
                            Optional train As Boolean = True,
                            Optional parallel As Boolean = False)

            Dim offendingDataSet As Sample = _dataSets _
                .FirstOrDefault(Function(x)
                                    Return x.status.SequenceEqual(input) AndAlso x.target.SequenceEqual(convertedResults)
                                End Function)
            _dataSets.Remove(offendingDataSet)

            If Not _dataSets.Exists(Function(x) x.status.SequenceEqual(input) AndAlso x.target.SequenceEqual(expectedResults)) Then
                Call _dataSets.Add(New Sample(input, expectedResults))
            End If

            If train Then
                Call Me.Train(parallel)
            End If
        End Sub

        Public Sub Corrects(dataset As Sample, expectedResults As Double(),
                            Optional train As Boolean = True,
                            Optional parallel As Boolean = False)
            Call Corrects(dataset.status, dataset.target, expectedResults, train, parallel)
        End Sub
    End Class
End Namespace
