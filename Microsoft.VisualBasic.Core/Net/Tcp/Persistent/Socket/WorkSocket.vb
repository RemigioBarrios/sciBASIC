﻿#Region "Microsoft.VisualBasic::d2c9a815a23622c9e4141f970511d85b, Microsoft.VisualBasic.Core\Net\Tcp\Persistent\Socket\WorkSocket.vb"

    ' Author:
    ' 
    ' 
    ' 
    ' 
    ' 
    ' 
    ' 



    ' /********************************************************************************/

    ' Summaries:

    '     Class WorkSocket
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Sub: (+2 Overloads) PushMessage, ReadCallback
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Net.Sockets
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.ExceptionExtensions
Imports Microsoft.VisualBasic.Net.Abstract
Imports Microsoft.VisualBasic.Net.Protocols

Namespace Net.Tcp.Persistent.Socket

    ''' <summary>
    ''' 长连接之中只是进行消息的发送处理，并不保证数据能够被接收到
    ''' </summary>
    Public Class WorkSocket : Inherits StateObject

        Public ExceptionHandle As ExceptionHandler
        Public ForceCloseHandle As ForceCloseHandle
        Public TotalBytes As Double

        Public ReadOnly ConnectTime As Date = Now

        Sub New(Socket As StateObject)
            Me.ChunkBuffer = Socket.ChunkBuffer
            Me.readBuffer = Socket.readBuffer
            Me.workSocket = Socket.workSocket
        End Sub

        ''' <summary>
        ''' DO_NOTHING
        ''' </summary>
        ''' <param name="ar"></param>
        Public Sub ReadCallback(ar As IAsyncResult)
            ' DO_NOTHING
        End Sub 'ReadCallback

        ''' <summary>
        ''' Server send message to user client.
        ''' </summary>
        ''' <param name="request"></param>
        Public Sub PushMessage(request As RequestStream)
            Dim byteData As Byte() = request.Serialize
            Try
                Call Me.workSocket.Send(byteData, byteData.Length, SocketFlags.None)
            Catch ex As Exception
                Call ForceCloseHandle(Me)
            End Try
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub PushMessage(message As String)
            Call PushMessage(New RequestStream(message))
        End Sub
    End Class
End Namespace
