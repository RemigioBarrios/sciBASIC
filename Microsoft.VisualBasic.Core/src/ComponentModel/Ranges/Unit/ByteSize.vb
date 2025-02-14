﻿#Region "Microsoft.VisualBasic::6c731ac798c808c1feac3177c81413d5, sciBASIC#\Microsoft.VisualBasic.Core\src\ComponentModel\Ranges\Unit\ByteSize.vb"

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

'   Total Lines: 10
'    Code Lines: 9
' Comment Lines: 0
'   Blank Lines: 1
'     File Size: 207 B


'     Enum ByteSize
' 
' 
'  
' 
' 
' 
' 
' /********************************************************************************/

#End Region

Namespace ComponentModel.Ranges.Unit

    Public Enum ByteSize As Long
        B = 1
        KB = 1024
        MB = KB * 1024
        GB = MB * 1024
        TB = GB * 1024
    End Enum

    <HideModuleName>
    Public Module ParserExtensions

        ''' <summary>
        ''' Parse the byte size in bytes
        ''' </summary>
        ''' <param name="desc">
        ''' 1111B means 1111 byte
        ''' 1KB means 1024 byte
        ''' 1MB means 1024*1024 byte
        ''' etc
        ''' </param>
        ''' <returns></returns>
        Public Function ParseByteSize(desc As String) As Long
            desc = Strings.Trim(desc)

            If desc = "" Then
                Return 0
            ElseIf desc.IsSimpleNumber Then
                Return CLng(Val(desc))
            End If

            Dim num As String = desc.Match(SimpleNumberPattern)
            Dim unit As String = desc.Replace(num, "").Trim.ToUpper
            Dim unitFlag As ByteSize = [Enum].Parse(GetType(ByteSize), unit)
            Dim bytes As Long = CLng(Val(num) * CLng(unitFlag))

            Return bytes
        End Function
    End Module
End Namespace
