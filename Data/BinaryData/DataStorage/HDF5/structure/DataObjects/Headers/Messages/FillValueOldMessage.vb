﻿#Region "Microsoft.VisualBasic::acce3629429ee02077f2ca899c17e5f3, Data\BinaryData\DataStorage\HDF5\structure\DataObjects\Headers\Messages\FillValueOldMessage.vb"

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

'     Class FillValueOldMessage
' 
'         Properties: size, value
' 
'         Constructor: (+1 Overloads) Sub New
'         Sub: printValues
' 
' 
' /********************************************************************************/

#End Region

'
' * Mostly copied from NETCDF4 source code.
' * refer : http://www.unidata.ucar.edu
' * 
' * Modified by iychoi@email.arizona.edu
' 

Imports System.IO
Imports BinaryReader = Microsoft.VisualBasic.Data.IO.HDF5.device.BinaryReader

Namespace HDF5.struct.messages

    ''' <summary>
    ''' The Data Storage - Fill Value (Old) Message
    ''' 
    ''' The fill value message stores a single data value which is returned to 
    ''' the application when an uninitialized data element is read from a dataset. 
    ''' The fill value is interpreted with the same datatype as the dataset. 
    ''' If no fill value message is present then a fill value of all zero bytes 
    ''' is assumed.
    '''
    ''' This fill value message Is deprecated In favor Of the "new" fill value message 
    ''' (Message Type 0x0005) And Is only written To the file For forward compatibility 
    ''' With versions Of the HDF5 Library before the 1.6.0 version. Additionally, 
    ''' it only appears For datasets With a user-defined fill value (As opposed To the 
    ''' library Default fill value Or an explicitly Set “undefined” fill value).
    ''' </summary>
    Public Class FillValueOldMessage : Inherits Message

        Public ReadOnly Property size As Integer
        Public ReadOnly Property value As Byte()

        Public Sub New([in] As BinaryReader, sb As Superblock, address As Long)
            Call MyBase.New(address)

            [in].offset = address

            Me.size = [in].readInt()
            Me.value = [in].readBytes(Me.size)
        End Sub

        Protected Friend Overrides Sub printValues(console As TextWriter)
            console.WriteLine("FillValueOldMessage >>>")
            console.WriteLine("address : " & Me.m_address)
            console.WriteLine("size : " & Me.size)

            console.WriteLine("FillValueOldMessage <<<")
        End Sub
    End Class

End Namespace
