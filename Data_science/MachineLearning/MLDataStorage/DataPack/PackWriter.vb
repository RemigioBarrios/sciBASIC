Imports System.IO
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.DataStorage.HDSPack.FileSystem

Public Class PackWriter : Implements IDisposable

    Dim disposedValue As Boolean
    Dim stream As StreamPack

    Sub New(stream As Stream)
        Me.stream = New StreamPack(stream, meta_size:=32 * 1024 * 1024, [readonly]:=False)
    End Sub

    Public Sub AddSamples(samples As IEnumerable(Of Sample))
        Dim allSamples As New List(Of Sample)

        For Each sample As Sample In samples
            Using file As Stream = stream.OpenBlock($"/samples/{sample.ID}.dat")
                Dim buf As New BinaryDataWriter(file, byteOrder:=ByteOrder.BigEndian)

                Call buf.Write(sample.label, BinaryStringFormat.DwordLengthPrefix)
                Call buf.Write(sample.ID, BinaryStringFormat.DwordLengthPrefix)
                Call buf.Write(sample.target)
                Call buf.Write(sample.vector)
            End Using
        Next
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: �ͷ��й�״̬(�йܶ���)
                Call stream.Dispose()
            End If

            ' TODO: �ͷ�δ�йܵ���Դ(δ�йܵĶ���)����д�ս���
            ' TODO: �������ֶ�����Ϊ null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: ������Dispose(disposing As Boolean)��ӵ�������ͷ�δ�й���Դ�Ĵ���ʱ������ս���
    ' Protected Overrides Sub Finalize()
    '     ' ��Ҫ���Ĵ˴��롣�뽫���������롰Dispose(disposing As Boolean)��������
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' ��Ҫ���Ĵ˴��롣�뽫���������롰Dispose(disposing As Boolean)��������
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
