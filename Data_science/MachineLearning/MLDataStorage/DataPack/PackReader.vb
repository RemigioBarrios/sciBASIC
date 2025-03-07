﻿Imports System.IO
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.DataStorage.HDSPack
Imports Microsoft.VisualBasic.DataStorage.HDSPack.FileSystem
Imports Microsoft.VisualBasic.Math.Distributions
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Text.Xml.Models

Namespace DataPack

    Public Class PackReader : Implements IDisposable

        Dim disposedValue As Boolean
        Dim stream As StreamPack
        Dim labels As Dictionary(Of String, String())
        ''' <summary>
        ''' the width of the input sample data vector
        ''' </summary>
        Dim features As Integer
        ''' <summary>
        ''' the number of samples that contains in current dataset
        ''' </summary>
        Dim samples As Integer
        ''' <summary>
        ''' the vector dimension of the output labels
        ''' </summary>
        Dim label_size As Integer

        Public ReadOnly Property output_labels As String()
            Get
                Return labels!labels
            End Get
        End Property

        Sub New(stream As Stream)
            Me.stream = New StreamPack(stream, [readonly]:=True)
            Me.labels = Me.stream.ReadText("/.etc/attributes.json").LoadJSON(Of Dictionary(Of String, String()))

            With Me.stream.ReadText("/.etc/dimension.json").LoadJSON(Of Dictionary(Of String, Integer))
                features = !features
                samples = !samples
                label_size = !outputs
            End With
        End Sub

        Public Function GetMatrix() As NormalizeMatrix
            Dim features = LoadFeature().ToArray
            Dim norm As New NormalizeMatrix With {
                .matrix = New XmlList(Of SampleDistribution)() With {.items = features},
                .names = labels!feature_names
            }

            Return norm
        End Function

        Private Iterator Function LoadFeature() As IEnumerable(Of SampleDistribution)
            For Each name As String In labels!feature_names
                Dim path As String = $"/features/{name}.dat"
                Dim file As New BinaryDataReader(stream.OpenBlock(path), byteOrder:=ByteOrder.BigEndian)
                Dim v As Double() = file.ReadDoubles(11)

                Yield New SampleDistribution With {
                    .min = v(0),
                    .max = v(1),
                    .average = v(2),
                    .stdErr = v(3),
                    .size = v(4),
                    .mode = v(10),
                    .quantile = v.Skip(5).Take(5).ToArray
                }
            Next
        End Function

        Public Function ReadSample(id As String) As Sample
            Dim path As String = $"/samples/{id}.dat"
            Dim file As New BinaryDataReader(stream.OpenBlock(path), byteOrder:=ByteOrder.BigEndian)
            Dim label As String = file.ReadString(BinaryStringFormat.DwordLengthPrefix)
            Dim id2 As String = file.ReadString(BinaryStringFormat.DwordLengthPrefix)

            ' needs for check of the sample id?
            'If id <> id2 Then
            'End If

            Dim data_labels As Double() = file.ReadDoubles(label_size)
            Dim feature_vec As Double() = file.ReadDoubles(features)

            Return New Sample(feature_vec, data_labels, id2)
        End Function

        Public Iterator Function GetAllSamples() As IEnumerable(Of Sample)
            For Each id As String In labels!id
                Yield ReadSample(id)
            Next
        End Function

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 释放托管状态(托管对象)
                    Call stream.Dispose()
                End If

                ' TODO: 释放未托管的资源(未托管的对象)并重写终结器
                ' TODO: 将大型字段设置为 null
                disposedValue = True
            End If
        End Sub

        ' ' TODO: 仅当“Dispose(disposing As Boolean)”拥有用于释放未托管资源的代码时才替代终结器
        ' Protected Overrides Sub Finalize()
        '     ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
        '     Dispose(disposing:=False)
        '     MyBase.Finalize()
        ' End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace