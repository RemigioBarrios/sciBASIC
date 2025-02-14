﻿#Region "Microsoft.VisualBasic::bd9a435cb869268b65c576cfd7270830, sciBASIC#\Data_science\MachineLearning\DeepLearning\CNN\ImageProcessor.vb"

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

    '   Total Lines: 54
    '    Code Lines: 42
    ' Comment Lines: 0
    '   Blank Lines: 12
    '     File Size: 1.96 KB


    '     Class ImageProcessor
    ' 
    ' 
    '         Enum ResizingMethod
    ' 
    '             Stretch, ZeroPad
    ' 
    ' 
    ' 
    '  
    ' 
    '     Function: resizeBitmap, Stretch, ZeroPad
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Drawing.Imaging

Namespace Convolutional

    Public Class ImageProcessor

        Public Enum ResizingMethod
            Stretch
            ZeroPad
        End Enum

        Private Shared Function Stretch(b As Bitmap, inputSize As Integer()) As Bitmap
            Dim resizedBmp As New Bitmap(inputSize(1), inputSize(0), PixelFormat.Format24bppRgb)

            Using gr As Graphics = Graphics.FromImage(resizedBmp)
                Call gr.DrawImage(b, 0, 0, inputSize(1), inputSize(0))
            End Using

            Return resizedBmp
        End Function

        Private Shared Function ZeroPad(b As Bitmap, inputSize As Integer())
            Dim resizedBmp As New Bitmap(inputSize(1), inputSize(0), PixelFormat.Format24bppRgb)

            Using gr As Graphics = Graphics.FromImage(resizedBmp)
                Dim inputAspRatio As Single = CSng(inputSize(0) / inputSize(1))
                Dim newHeight, newWidth As Integer
                Dim multiplier = CSng(b.Width) / b.Height

                If multiplier > inputAspRatio Then
                    multiplier = inputAspRatio / multiplier
                    newWidth = inputSize(1)
                    newHeight = CInt(newWidth * multiplier)
                Else
                    newHeight = inputSize(0)
                    newWidth = CInt(newHeight * multiplier)
                End If

                gr.DrawImage(b, (inputSize(1) - newWidth) / 2.0F, (inputSize(0) - newHeight) / 2.0F, newWidth, newHeight)
            End Using

            Return resizedBmp
        End Function

        Friend Shared Function resizeBitmap(b As Bitmap, resizingMethod As ResizingMethod, inputSize As Integer()) As Bitmap
            If resizingMethod = ResizingMethod.Stretch Then
                Return Stretch(b, inputSize)
            Else
                Return ZeroPad(b, inputSize)
            End If
        End Function
    End Class
End Namespace
