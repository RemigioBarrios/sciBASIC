﻿#Region "Microsoft.VisualBasic::b4c004b0b0e8f696931c474bd074ddf1, sciBASIC#\gr\Microsoft.VisualBasic.Imaging\Drawing2D\HeatMap\Pixel.vb"

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

    '   Total Lines: 67
    '    Code Lines: 35
    ' Comment Lines: 20
    '   Blank Lines: 12
    '     File Size: 1.71 KB


    '     Interface Pixel
    ' 
    '         Properties: Scale, X, Y
    ' 
    '     Structure PixelData
    ' 
    '         Properties: isEmpty, Scale, X, Y
    ' 
    '         Constructor: (+3 Overloads) Sub New
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization
Imports Microsoft.VisualBasic.Serialization.BinaryDumping

Namespace Drawing2D.HeatMap

    Public Interface IRasterGrayscaleHeatmap
        Function GetRasterPixels() As IEnumerable(Of Pixel)
    End Interface

    ''' <summary>
    ''' A pixel spot object associate [x,y] with intensity scale data
    ''' </summary>
    Public Structure PixelData : Implements Pixel

        Public Property X As Integer Implements Pixel.X
        Public Property Y As Integer Implements Pixel.Y
        Public Property Scale As Double Implements Pixel.Scale

        Public ReadOnly Property isEmpty As Boolean
            Get
                Return X = 0 AndAlso Y = 0 AndAlso Scale = 0.0
            End Get
        End Property

        Sub New(raster As RasterPixel, data As Double)
            Call Me.New(raster.X, raster.Y, data)
        End Sub

        Sub New(p As Point, data As Double)
            X = p.X
            Y = p.Y
            Scale = data
        End Sub

        Sub New(x As Integer, y As Integer, scale As Double)
            Me.X = x
            Me.Y = y
            Me.Scale = scale
        End Sub

        ''' <summary>
        ''' spot data is zero
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        Sub New(x As Integer, y As Integer)
            Me.X = x
            Me.Y = y
        End Sub

        Public Overrides Function ToString() As String
            Return $"[{X},{Y} = {Scale.ToString("G4")}]"
        End Function

        Public Shared Function ParseStream(data As Byte()) As IEnumerable(Of PixelData)
            Using ms As New MemoryStream(data)
                ms.Seek(Scan0, SeekOrigin.Begin)
                Return ParseStream(ms)
            End Using
        End Function

        Public Shared Iterator Function ParseStream(data As Stream) As IEnumerable(Of PixelData)
            Dim bytes As Byte() = New Byte(RawStream.INT32 - 1) {}
            data.Read(bytes, Scan0, bytes.Length)
            Dim n As Integer = BitConverter.ToInt32(bytes, Scan0)
            Dim x = New Byte((n * RawStream.INT32) - 1) {}
            data.Read(x, Scan0, x.Length)
            Dim y = New Byte((n * RawStream.INT32) - 1) {}
            data.Read(y, Scan0, y.Length)
            bytes = New Byte((n * RawStream.DblFloat) - 1) {}
            data.Read(bytes, Scan0, bytes.Length)
            Dim z As Double() = New NetworkByteOrderBuffer().decode(bytes)
            Dim xbytes = New Byte(RawStream.INT32 - 1) {}
            Dim ybytes = New Byte(RawStream.INT32 - 1) {}

            For i As Integer = 0 To n - 1
                Array.ConstrainedCopy(x, i * RawStream.INT32, xbytes, Scan0, RawStream.INT32)
                Array.ConstrainedCopy(y, i * RawStream.INT32, ybytes, Scan0, RawStream.INT32)

                Yield New PixelData(BitConverter.ToInt32(x, Scan0), BitConverter.ToInt32(y, Scan0), z(i))
            Next
        End Function

        Public Shared Function CreateStream(pixels As IEnumerable(Of PixelData)) As MemoryStream
            Dim layer As PixelData() = pixels.ToArray
            Dim x As Byte() = layer.Select(Function(p) BitConverter.GetBytes(p.X)).IteratesALL.ToArray
            Dim y As Byte() = layer.Select(Function(p) BitConverter.GetBytes(p.Y)).IteratesALL.ToArray
            Dim z As Double() = layer.Select(Function(p) p.Scale).ToArray
            Dim buf As New MemoryStream
            Dim bytes As Byte()
            Dim encode As New NetworkByteOrderBuffer

            bytes = BitConverter.GetBytes(layer.Length)
            Call buf.Write(bytes, Scan0, bytes.Length)
            Call buf.Write(x, Scan0, x.Length)
            Call buf.Write(y, Scan0, y.Length)

            bytes = encode.encode(z)
            Call buf.Write(bytes, Scan0, bytes.Length)
            Call buf.Flush()

            Return buf
        End Function

    End Structure
End Namespace
