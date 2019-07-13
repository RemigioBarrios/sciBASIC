﻿#Region "Microsoft.VisualBasic::d8e3e0b0588161caa6ec7f26db80a1b2, Data\BinaryData\BinaryData\Stream\SeekTask.vb"

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

    ' Class SeekTask
    ' 
    '     Properties: PreviousPosition, Stream
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Sub: Dispose
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO

''' <summary>
''' Represents a temporary seek to another position which is undone after the task has been disposed.
''' </summary>
Public Class SeekTask : Implements IDisposable

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SeekTask"/> class to temporarily seek the given
    ''' <see cref="Stream"/> to the specified position. The <see cref="System.IO.Stream"/> is rewound to its
    ''' previous position after the task is disposed.
    ''' </summary>
    ''' <param name="stream">A <see cref="System.IO.Stream"/> to temporarily seek.</param>
    ''' <param name="offset">A byte offset relative to the origin parameter.</param>
    ''' <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain
    ''' the new position.</param>
    Public Sub New(stream As Stream, offset As Long, origin As SeekOrigin)
        Me.Stream = stream
        PreviousPosition = stream.Position
        Me.Stream.Seek(offset, origin)
    End Sub

    ''' <summary>
    ''' Gets the <see cref="Stream"/> which is temporarily sought to another position.
    ''' </summary>
    Public Property Stream() As Stream
    ''' <summary>
    ''' Gets the absolute position to which the <see cref="Stream"/> will be rewound after this task is disposed.
    ''' </summary>
    Public Property PreviousPosition() As Long

    ''' <summary>
    ''' Rewinds the <see cref="Stream"/> to its previous position.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
		Stream.Seek(PreviousPosition, SeekOrigin.Begin)
	End Sub
End Class
