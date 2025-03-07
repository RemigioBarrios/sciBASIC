﻿#Region "Microsoft.VisualBasic::4c3f1ceef48c007c13d4e4ff9e6446f6, sciBASIC#\Microsoft.VisualBasic.Core\src\ComponentModel\File\XmlAssembly\XmlDataModel.vb"

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

'   Total Lines: 96
'    Code Lines: 57
' Comment Lines: 28
'   Blank Lines: 11
'     File Size: 3.75 KB


'     Class XmlDataModel
' 
'         Properties: Stylesheet, TypeComment
' 
'         Function: CreateTypeReferenceComment, (+2 Overloads) GetTypeReferenceComment
' 
'         Sub: SaveTypeComment
'         Interface IXmlType
' 
'             Properties: TypeComment
' 
' 
' 
' 
' /********************************************************************************/

#End Region

#If NET48 Then
Imports System.Web.Script.Serialization
#Else
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
#End If

Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.SecurityString

Namespace ComponentModel

    ''' <summary>
    ''' 这个基类型对象主要是用来生成类型全称注释方便编写XML文件加载代码功能的
    ''' </summary>
    Public MustInherit Class XmlDataModel : Implements IXmlType

        ''' <summary>
        ''' 只适合最外层面的容器类型的对象来实现
        ''' </summary>
        Public Interface IXmlType
            Property TypeComment As XmlComment
        End Interface

        ''' <summary>
        ''' The external css/xsl file name for styling current xml file
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' This property is only works for the root element type
        ''' </remarks>
        <XmlIgnore>
        Public Property Stylesheet As XmlStyleProcessor

        ''' <summary>
        ''' ReadOnly, Data model type tracking use Xml Comment.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' JSON存储的时候,这个属性会被自动忽略掉
        ''' </remarks>
        <DataMember>
        <IgnoreDataMember>
        <SoapIgnore>
        <XmlAnyElement>
        Public Property TypeComment As XmlComment Implements IXmlType.TypeComment
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return GetTypeReferenceComment()
            End Get
            Set(value As XmlComment)
                ' Do Nothing
                ' 2018-6-5 this xml comment node cause bug 
                ' when using xml deserialization
            End Set
        End Property

        Private Function GetTypeReferenceComment() As XmlComment
            Return New XmlDocument().CreateComment(GetTypeReferenceComment(Me.GetType))
        End Function

        Public Shared Function CreateTypeReferenceComment(type As Type) As XmlComment
            Return New XmlDocument().CreateComment(GetTypeReferenceComment(type))
        End Function

        Public Shared Sub SaveTypeComment(model As IXmlType)
            model.TypeComment = New XmlDocument().CreateComment(GetTypeReferenceComment(model.GetType))
        End Sub

        ''' <summary>
        ''' 生成的注释信息是默认空了四个空格的
        ''' </summary>
        ''' <param name="modelType"></param>
        ''' <returns></returns>
        Public Shared Function GetTypeReferenceComment(modelType As Type, Optional indent% = 4) As String
            Dim fullName$ = modelType.FullName
            Dim devtools = modelType.Assembly.FromAssembly
            Dim assembly$ = modelType.Assembly.FullName
            Dim md5$ = modelType.Assembly.Location.GetFileMd5
            Dim indentBlank As New String(" "c, indent)
            Dim traceInfo$ = vbCrLf &
                $"{indentBlank} model:     " & fullName & vbCrLf &
                $"{indentBlank} assembly:  " & assembly & vbCrLf &
                $"{indentBlank} version:   " & devtools.AssemblyVersion & vbCrLf &
                $"{indentBlank} built:     " & devtools.BuiltTime.ToString & vbCrLf &
                $"{indentBlank} md5:       " & md5 & vbCrLf &
                $"{indentBlank} timestamp: " & Now.ToString & vbCrLf &
                "  "

            Return traceInfo
        End Function
    End Class
End Namespace
