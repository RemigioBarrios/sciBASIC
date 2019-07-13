﻿#Region "Microsoft.VisualBasic::308f84f37b5f91bed95321b3962e4c31, www\Microsoft.VisualBasic.Webservices.Bing\WebResult.vb"

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

    ' Class WebResult
    ' 
    '     Function: TryParse
    ' 
    ' Class SearchResult
    ' 
    '     Properties: [Next], CurrentPage, HaveNext, Results, Title
    ' 
    '     Function: NextPage, ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Net
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Text.Parser.HtmlParser

''' <summary>
''' 一个结果条目
''' </summary>
Public Class WebResult : Inherits Http.WebResult

    Public Shared Function TryParse(html As String) As WebResult
        Dim tokens As String() = Strings.Split(html, "</h2>", -1, CompareMethod.Text)
        Dim Title As String = tokens(Scan0)
        Dim URL As String = Title.href
        Title = Regex.Match(Title, """>.+</a>").Value
        Title = Title.GetValue.Trim
        html = tokens(1)
        html = Regex.Replace(html, "<cite>.+?</cite>", "", RegexICSng)
        html = Regex.Replace(html, "<.+?>", "", RegexICSng)

        Dim [date] As String = Regex.Match(New String(html.Reverse.ToArray), "\d+-\d+-\d+", RegexICSng).Value
        [date] = New String([date].Reverse.ToArray)

        Dim BriefText As String = html
        If Not String.IsNullOrEmpty([date]) Then
            BriefText = BriefText.Replace([date], "")
        End If

        Return New WebResult With {
            .Title = Title,
            .URL = URL,
            .BriefText = BriefText,
            .Update = [date]
        }
    End Function
End Class

Public Class SearchResult

    Public Property Title As String
    ''' <summary>
    ''' 总的结果数目
    ''' </summary>
    ''' <returns></returns>
    Public Property Results As Integer
    Public Property CurrentPage As WebResult()
    Public Property [Next] As String

    Public Function NextPage() As SearchResult
        Return SearchEngineProvider.DownloadResult([Next])
    End Function

    ''' <summary>
    ''' Is there have next page?
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HaveNext As Boolean
        Get
            Return Not String.IsNullOrEmpty([Next])
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function
End Class
