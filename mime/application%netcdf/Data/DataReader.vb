﻿#Region "Microsoft.VisualBasic::aff62437cfdf76073823ca2399e70e63, mime\application%netcdf\DataReader.vb"

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

    ' Module DataReader
    ' 
    '     Function: nonRecord, record
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.MIME.application.netCDF.Components

''' <summary>
''' Data reader methods for a given variable data value.
''' (在这个模块之中读取<see cref="variable.value"/>数据变量的值)
''' </summary>
Module DataReader

    ''' <summary>
    ''' Read data for the given non-record variable
    ''' </summary>
    ''' <param name="buffer">Buffer for the file data</param>
    ''' <param name="variable">Variable metadata</param>
    ''' <returns>Data of the element</returns>
    Public Function nonRecord(buffer As BinaryDataReader, variable As variable) As Object()
        ' variable type
        Dim type = TypeExtensions.str2num(variable.type)
        ' size of the data
        Dim size = variable.size / sizeof(type)
        ' iterates over the data
        Dim data As Object() = New Object(size - 1) {}

        ' 读取的结果是一个T()数组
        For i As Integer = 0 To size - 1
            data(i) = TypeExtensions.readType(buffer, type, 1)
        Next

        Return data
    End Function

    ''' <summary>
    ''' Read data for the given record variable
    ''' </summary>
    ''' <param name="buffer">Buffer for the file data</param>
    ''' <param name="variable">Variable metadata</param>
    ''' <param name="recordDimension">Record dimension metadata</param>
    ''' <returns>Data of the element</returns>
    Public Function record(buffer As BinaryDataReader, variable As variable, recordDimension As recordDimension) As Object()
        ' variable type
        Dim type As CDFDataTypes = TypeExtensions.str2num(variable.type)
        Dim width% = If(variable.size, variable.size / sizeof(type), 1)

        ' size of the data
        ' TODO streaming data
        Dim size = recordDimension.length

        ' iterates over the data
        Dim data As Object() = New Object(size - 1) {}
        Dim [step] = recordDimension.recordStep

        ' 读取的结果可能是一个T()()矩阵或者T()数组
        For i As Integer = 0 To size - 1
            Dim currentOffset& = buffer.Position
            Dim nextOffset = currentOffset + [step]

            If buffer.EndOfStream Then
                data(i) = Nothing
            Else
                data(i) = TypeExtensions.readType(buffer, type, width)
                buffer.Seek(nextOffset, SeekOrigin.Begin)
            End If
        Next

        Return data
    End Function
End Module
