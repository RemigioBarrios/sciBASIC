﻿#Region "Microsoft.VisualBasic::f4dee8da94c256c13687cb02a395f237, www\WWW.Google\News\XML.vb"

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

    '     Class Channel
    ' 
    '         Properties: copyright, description, generator, image, item
    '                     language, lastBuildDate, link, pubDate, title
    '                     ttl, webMaster
    ' 
    '         Function: ToString
    ' 
    '     Class image
    ' 
    '         Properties: link, title, url
    ' 
    '     Class item
    ' 
    '         Properties: description, guid, link, pubDate, title
    ' 
    '     Class guid
    ' 
    '         Properties: isPermaLink, text
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace News

    Public Class Channel

        Public Property generator As String
        Public Property title As String
        Public Property link As String
        Public Property language As String
        Public Property webMaster As String
        Public Property copyright As String
        Public Property pubDate As String
        Public Property lastBuildDate As String
        Public Property description As String
        Public Property ttl As String
        <XmlElement> Public Property image As image()
        <XmlElement> Public Property item As item()

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class

    Public Class image
        Public Property title As String
        Public Property url As String
        Public Property link As String
    End Class

    Public Class item
        Public Property title As String
        Public Property link As String
        Public Property guid As guid
        Public Property pubDate As String
        Public Property description As String
    End Class

    Public Class guid
        <XmlAttribute> Public Property isPermaLink As Boolean
        <XmlText> Public Property text As String
    End Class
End Namespace
