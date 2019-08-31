﻿#Region "Microsoft.VisualBasic::eee79e6b0c909dec273d1f4b24eca635, mime\application%json\Javascript\JsonElement.vb"

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

    '     Class JsonElement
    ' 
    '         Function: [As], ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Namespace Javascript

    ''' <summary>
    ''' The abstract javascript object model
    ''' </summary>
    Public MustInherit Class JsonElement

        Public MustOverride Function BuildJsonString() As String

        Public Overrides Function ToString() As String
            Return "base::json"
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function [As](Of T As JsonElement)() As T
            Return DirectCast(Me, T)
        End Function
    End Class
End Namespace
