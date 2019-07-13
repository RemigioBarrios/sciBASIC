﻿#Region "Microsoft.VisualBasic::caee8921de8aef7e1ccec0d478fa11a3, Data\BinaryData\BinaryData\Extensions\FixLengthString.vb"

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

    ' Class FixLengthString
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: GetBytes, ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text

Public Class FixLengthString

    ReadOnly encoding As Encoding

    Sub New(encoding As Encoding)
        Me.encoding = encoding
    End Sub

    Public Function GetBytes(text$, bytLen%) As Byte()
        Dim bytes As Byte() = encoding.GetBytes(text)

        If bytes.Length > bytLen Then
            Return bytes.Take(bytLen).ToArray
        ElseIf bytes.Length < bytLen Then
            ReDim Preserve bytes(bytLen - 1)
            Return bytes
        Else
            Return bytes
        End If
    End Function

    Public Overrides Function ToString() As String
        Return encoding.ToString
    End Function
End Class
