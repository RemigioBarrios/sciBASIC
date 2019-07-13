﻿#Region "Microsoft.VisualBasic::6e209f6e9b16a9f7d548e3d0e98f638f, Microsoft.VisualBasic.Core\ApplicationServices\VBDev\Signature\LicenseInfo.vb"

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

    '     Class LicenseInfo
    ' 
    '         Properties: Authors, Brief, Copyright, Title
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Text.Xml.Models

Namespace ApplicationServices.Development

    ' Here is the example:

#Region "b77a5c561934e089; ./Tools/SoftwareToolkits/LicenseMgr.vb"

    ' Author:                                                                         <-- authors
    '
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    '
    ' Copyright (c) 2016 amethyst.asuka@gcmodeller.org                                <-- copyright
    '
    '
    ' The MIT License (MIT)                                                           <-- title
    '
    ' Permission is hereby granted, free of charge, to any person obtaining a copy    <-- biref
    ' of this software and associated documentation files (the "Software"), to deal
    ' in the Software without restriction, including without limitation the rights
    ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ' copies of the Software, and to permit persons to whom the Software is
    ' furnished to do so, subject to the following conditions:
    '
    ' The above copyright notice and this permission notice shall be included in
    ' all copies or substantial portions of the Software.
    '
    ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    ' THE SOFTWARE.

#End Region

    Public Class LicenseInfo

        Public Property Authors As NamedValue()
        Public Property Title As String
        Public Property Copyright As String

        <XmlText>
        Public Property Brief As String

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function ToString() As String
            Return Brief
        End Function
    End Class
End Namespace
