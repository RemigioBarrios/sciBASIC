﻿#Region "Microsoft.VisualBasic::cb7c12209490c29b0ba650281109540a, Data_science\DataMining\DataMining\ComponentModel\Class.vb"

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

    '     Class ColorClass
    ' 
    '         Properties: color, enumInt, name
    ' 
    '         Function: FromEnums, ToString
    '         Operators: <>, =
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Linq

Namespace ComponentModel

    ''' <summary>
    ''' Object entity classification class
    ''' </summary>
    Public Class ColorClass

        ''' <summary>
        ''' Using for the data visualization.(RGB表达式, html颜色值或者名称)
        ''' </summary>
        ''' <returns></returns>
        Public Property color As String
        ''' <summary>
        ''' <see cref="Integer"/> encoding for this class.(即枚举类型)
        ''' </summary>
        ''' <returns></returns>
        Public Property enumInt As Integer
        ''' <summary>
        ''' Class Name
        ''' </summary>
        ''' <returns></returns>
        Public Property name As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="colors$">Using the user custom colors</param>
        ''' <returns></returns>
        Public Shared Function FromEnums(Of T As Structure)(Optional colors$() = Nothing) As ColorClass()
            Dim values As T() = Enums(Of T)()

            If colors.IsNullOrEmpty OrElse colors.Length < values.Length Then
                colors$ = Imaging _
                    .ChartColors _
                    .Select(AddressOf Imaging.ToHtmlColor) _
                    .ToArray
            End If

            Dim out As ColorClass() = values _
                .SeqIterator _
                .Select(Function(v)
                            Return New ColorClass With {
                                .enumInt = CInt(DirectCast(+v, Object)),
                                .color = colors(v),
                                .name = DirectCast(CObj((+v)), [Enum]).Description
                            }
                        End Function) _
                .ToArray

            Return out
        End Function

        Public Shared Operator =(a As ColorClass, b As ColorClass) As Boolean
            Return a.color = b.color AndAlso a.enumInt = b.enumInt AndAlso a.name = b.name
        End Operator

        Public Shared Operator <>(a As ColorClass, b As ColorClass) As Boolean
            Return Not a = b
        End Operator
    End Class
End Namespace
