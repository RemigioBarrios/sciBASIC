﻿#Region "Microsoft.VisualBasic::d7c99e5b968ed7d376f2595fd713a959, Data\BinaryData\DataStorage\HDF5\dataset\filters\filters.vb"

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

    '     Class DeflatePipelineFilter
    ' 
    '         Properties: id, name
    ' 
    '         Function: decode
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports Microsoft.VisualBasic.Net.Http

Namespace HDF5.dataset.filters

    ''' <summary>
    ''' GZip
    ''' </summary>
    Public Class DeflatePipelineFilter : Implements IFilter

        Public ReadOnly Property id As Integer Implements IFilter.id
        Public ReadOnly Property name As String Implements IFilter.name

        Public Function decode(encodedData() As Byte, filterData() As Integer) As Byte() Implements IFilter.decode
            Return encodedData.UnZipStream(noMagic:=True).ToArray
        End Function
    End Class
End Namespace
