﻿' 
'  PicoXLSX is a small .NET library to generate XLSX (Microsoft Excel 2007 or newer) files in an easy and native way
'  Copyright Raphael Stoeckli © 2023
'  This library is licensed under the MIT License.
'  You find a copy of the license in project folder or on: http://opensource.org/licenses/MIT
' 

Imports System.Globalization
Imports System.Reflection

Namespace XLSX.Writer

    ''' <summary>
    ''' Class representing the meta data of a workbook
    ''' </summary>
    Public Class Metadata
        ''' <summary>
        ''' Defines the applicationVersion
        ''' </summary>
        Private applicationVersionField As String

        ''' <summary>
        ''' Gets or sets the application which created the workbook. Default is PicoXLSX
        ''' </summary>
        Public Property Application As String

        ''' <summary>
        ''' Gets or sets the version of the creation application. Default is the library version of PicoXLSX. Use the format xxxxx.yyyyy (e.g. 1.0 or 55.9875) in case of a custom value.
        ''' </summary>
        Public Property ApplicationVersion As String
            Get
                Return applicationVersionField
            End Get
            Set(value As String)
                applicationVersionField = value
                CheckVersion()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the category of the document. There are no predefined values or restrictions about the content of this field
        ''' </summary>
        Public Property Category As String

        ''' <summary>
        ''' Gets or sets the company owning the document. This value is for organizational purpose. Add more than one manager by using the semicolon (;) between the words
        ''' </summary>
        Public Property Company As String

        ''' <summary>
        ''' Gets or sets the status of the document. There are no predefined values or restrictions about the content of this field
        ''' </summary>
        Public Property ContentStatus As String

        ''' <summary>
        ''' Gets or sets the creator of the workbook. Add more than one creator by using the semicolon (;) between the authors
        ''' </summary>
        Public Property Creator As String

        ''' <summary>
        ''' Gets or sets the description of the document or comment about it
        ''' </summary>
        Public Property Description As String

        ''' <summary>
        ''' Gets or sets the hyper-link base of the document.
        ''' </summary>
        Public Property HyperlinkBase As String

        ''' <summary>
        ''' Gets or sets the keywords of the workbook. Separate particular keywords with semicolons (;)
        ''' </summary>
        Public Property Keywords As String

        ''' <summary>
        ''' Gets or sets the responsible manager of the document. This value is for organizational purpose.
        ''' </summary>
        Public Property Manager As String

        ''' <summary>
        ''' Gets or sets the subject of the workbook
        ''' </summary>
        Public Property Subject As String

        ''' <summary>
        ''' Gets or sets the title of the workbook
        ''' </summary>
        Public Property Title As String

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Metadata"/> class
        ''' </summary>
        Public Sub New()
            Application = "PicoXLSX"
            Dim vi As Version = Assembly.GetExecutingAssembly().GetName().Version
            ApplicationVersion = ParseVersion(vi.Major, vi.Minor, vi.Revision, vi.Build)
        End Sub

        ''' <summary>
        ''' Checks the format of the passed version string. Allowed values are null, empty and fractions from 0.0  to 99999.99999 (max. number of digits before and after the period is 5)
        ''' </summary>
        Private Sub CheckVersion()
            If String.IsNullOrEmpty(applicationVersionField) Then
                Return
            End If
            Dim split = applicationVersionField.Split("."c)
            Dim state = True
            If split.Length <> 2 Then
                state = False
            Else
                If split(1).Length < 1 OrElse split(1).Length > 5 Then
                    state = False
                End If
                If split(0).Length < 1 OrElse split(0).Length > 5 Then
                    state = False
                End If
            End If
            If Not state Then
                Throw New FormatException("The format of the version in the meta data is wrong (" & applicationVersionField & "). Should be in the format and a range from '0.0' to '99999.99999'")
            End If
        End Sub

        ''' <summary>
        ''' Method to parse a common version (major.minor.revision.build) into the compatible format (major.minor). The minimum value is 0.0 and the maximum value is 99999.99999<br></br>
        ''' The minor, revision and build number are joined if possible. If the number is too long, the additional characters will be removed from the right side down to five characters (e.g. 785563 will be 78556)
        ''' </summary>
        ''' <param name="major">Major number from 0 to 99999.</param>
        ''' <param name="minor">Minor number.</param>
        ''' <param name="build">Build number.</param>
        ''' <param name="revision">Revision number.</param>
        ''' <returns>Formatted version number (e.g. 1.0 or 55.987).</returns>
        Public Shared Function ParseVersion(major As Integer, minor As Integer, build As Integer, revision As Integer) As String
            If major < 0 OrElse minor < 0 OrElse build < 0 OrElse revision < 0 Then
                Throw New FormatException("The format of the passed version is wrong. No negative number allowed.")
            End If
            If major > 99999 Then
                Throw New FormatException("The major number may not be bigger than 99999. The passed value is " & major.ToString())
            End If
            Dim culture = CultureInfo.InvariantCulture
            Dim leftPart = major.ToString("G", culture)
            Dim rightPart = minor.ToString("G", culture) & build.ToString("G", culture) & revision.ToString("G", culture)
            rightPart = rightPart.TrimEnd("0"c)
            If Equals(rightPart, "") Then
                rightPart = "0"
            Else
                If rightPart.Length > 5 Then
                    rightPart = rightPart.Substring(0, 5)
                End If
            End If
            Return leftPart & "." & rightPart
        End Function
    End Class
End Namespace
