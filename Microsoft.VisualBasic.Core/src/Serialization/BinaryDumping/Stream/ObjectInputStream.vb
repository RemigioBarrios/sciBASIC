﻿Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.ValueTypes
Imports TypeInfo = Microsoft.VisualBasic.Scripting.MetaData.TypeInfo

Namespace Serialization.BinaryDumping

    Public Class ObjectInputStream : Implements IDisposable

        Dim disposedValue As Boolean
        Dim stream As BinaryReader
        Dim network As New NetworkByteOrderBuffer

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Sub New(s As Stream)
            stream = New BinaryReader(s)
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Sub New(rd As BinaryReader)
            stream = rd
        End Sub

        Public Function ReadObject() As Object
            Dim flag As Integer = stream.ReadInt32

            If flag <= 0 Then
                Return Nothing
            Else
                Call stream.BaseStream.Seek(-4, SeekOrigin.Current)
            End If

            Dim info As TypeInfo = Encoding.ASCII.GetString(Buffer.Parse(stream).buffer).LoadJSON(Of TypeInfo)
            Dim obj As Object = Activator.CreateInstance(info.GetType(knownFirst:=True))
            Dim nsize As Integer = stream.ReadInt32
            Dim value As Object
            Dim fields As Dictionary(Of String, FieldInfo) = ObjectVisitor _
                .GetAllFields(obj.GetType) _
                .Distinct _
                .ToDictionary(Function(f)
                                  Return f.Name
                              End Function)
            Dim buf As Buffer
            Dim code As TypeCode

            For i As Integer = 0 To nsize - 1
                Dim name As String = Encoding.ASCII.GetString(Buffer.Parse(stream).buffer)
                Dim field As FieldInfo = fields.TryGetValue(name)

                If field Is Nothing Then
                    ' just can not ignores the missing data field at here
                    ' due to the reason of the data decoder required of the field data type
                    ' to read the binary data
                    Throw New Exception($"the data record('{name}') inside the binary data file is not required in target object?")
                End If

                If DataFramework.IsPrimitive(field.FieldType) Then
                    buf = Buffer.Parse(stream)

                    Select Case field.FieldType
                        Case GetType(Integer) : value = BitConverter.ToInt32(buf.buffer, Scan0)
                        Case GetType(Double) : value = network.ToDouble(buf.buffer)
                        Case GetType(String) : value = If(buf.Length = 0, Nothing, Encoding.UTF8.GetString(buf.buffer))
                        Case GetType(Single) : value = network.ToFloat(buf.buffer)
                        Case GetType(Long) : value = BitConverter.ToInt64(buf.buffer, Scan0)
                        Case GetType(Short) : value = BitConverter.ToInt16(buf.buffer, Scan0)
                        Case GetType(Byte) : value = buf.buffer(0)
                        Case GetType(Boolean) : value = If(buf.buffer(0) > 0, True, False)
                        Case GetType(Date) : value = DateTimeHelper.FromUnixTimeStamp(network.ToDouble(buf.buffer))
                        Case Else
                            Throw New NotImplementedException($"{field.Name}: {field.FieldType.Name}")
                    End Select
                ElseIf field.FieldType.IsArray Then
                    If DataFramework.IsPrimitive(field.FieldType.GetElementType) Then
                        buf = Buffer.Parse(stream)
                        code = stream.ReadInt32
                        value = RawStream.GetData(buf.buffer, code)
                    Else
                        Dim nlen As Integer = stream.ReadInt32
                        Dim array As Array = Array.CreateInstance(field.FieldType.GetElementType, nlen)

                        For j As Integer = 0 To nlen - 1
                            array.SetValue(Me.ReadObject, j)
                        Next

                        value = array
                    End If
                Else
                    value = Me.ReadObject
                End If

                Call field.SetValue(obj, value)
            Next

            Return obj
        End Function

        Public Sub Close()
            stream.Close()
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 释放托管状态(托管对象)
                    Try
                        Call stream.Close()
                    Catch ex As Exception

                    End Try
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