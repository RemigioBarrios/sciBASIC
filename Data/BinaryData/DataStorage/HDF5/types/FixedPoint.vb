﻿#Region "Microsoft.VisualBasic::ae3a101c7ebb4f37ea4f7eec26b458c0, Data\BinaryData\DataStorage\HDF5\types\FixedPoint.vb"

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

    '     Class FixedPoint
    ' 
    '         Properties: bitOffset, bitPrecision, byteOrder, highPadding, lowPadding
    '                     signed, typeInfo
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Numerics

Namespace HDF5.type

    Public Class FixedPoint : Inherits DataType

        Public Property byteOrder As ByteOrder
        Public Property lowPadding As Boolean
        Public Property highPadding As Boolean
        Public Property signed As Boolean
        Public Property bitOffset As Short
        Public Property bitPrecision As Short

        Public Overrides ReadOnly Property typeInfo As System.Type
            Get
                If signed Then
                    Select Case bitPrecision
                        Case 8 : Return GetType(SByte)
                        Case 16 : Return GetType(Short)
                        Case 32 : Return GetType(Integer)
                        Case 64 : Return GetType(Long)
                        Case Else
                            Throw New NotSupportedException
                    End Select
                Else
                    Select Case bitPrecision
                        Case 8, 16
                            Return GetType(Integer)
                        Case 32
                            Return GetType(Long)
                        Case 64
                            Return GetType(BigInteger)
                        Case Else
                            Throw New NotSupportedException
                    End Select
                End If
            End Get
        End Property
    End Class
End Namespace
