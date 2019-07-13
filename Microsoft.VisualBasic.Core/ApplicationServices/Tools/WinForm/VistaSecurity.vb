﻿#Region "Microsoft.VisualBasic::7e3b7e103b4668634dc99b2fde490ce0, Microsoft.VisualBasic.Core\ApplicationServices\Tools\WinForm\VistaSecurity.vb"

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

    '     Module VistaSecurity
    ' 
    '         Function: IsAdmin, IsVistaOrHigher, SendMessage
    ' 
    '         Sub: AddShieldToButton, RestartElevated
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.Principal

Namespace ApplicationServices.Windows.Forms

    Public Module VistaSecurity

        <DllImport("user32")>
        Public Function SendMessage(hWnd As IntPtr, msg As UInt32, wParam As UInt32, lParam As UInt32) As UInt32
        End Function

        Friend Const BCM_FIRST As Integer = &H1600
        Friend Const BCM_SETSHIELD As Integer = (BCM_FIRST + &HC)

        Public Function IsVistaOrHigher() As Boolean
            Return Environment.OSVersion.Version.Major < 6
        End Function

        ''' <summary>
        ''' Checks if the process is elevated
        ''' </summary>
        ''' <returns>If is elevated</returns>
        Public Function IsAdmin() As Boolean
            Dim id As WindowsIdentity = WindowsIdentity.GetCurrent()
            Dim p As New WindowsPrincipal(id)
            Return p.IsInRole(WindowsBuiltInRole.Administrator)
        End Function

        ''' <summary>
        ''' Add a shield icon to a button
        ''' </summary>
        ''' <param name="b">The button</param>
        Public Sub AddShieldToButton(b As Button)
            b.FlatStyle = FlatStyle.System
            SendMessage(b.Handle, BCM_SETSHIELD, 0, &HFFFFFFFFUI)
        End Sub

        ''' <summary>
        ''' Restart the current process with administrator credentials.(以管理员的身份重启本应用程序)
        ''' </summary>
        Public Sub RestartElevated(Optional args$ = "")
            Dim startInfo As New ProcessStartInfo()
            startInfo.UseShellExecute = True
            startInfo.WorkingDirectory = Environment.CurrentDirectory
            startInfo.FileName = Application.ExecutablePath
            startInfo.Arguments = args
            startInfo.Verb = "runas"
            Try
                Dim p As Process = Process.Start(startInfo)
            Catch ex As Win32Exception
                'If cancelled, do nothing
                Return
            End Try

            Call App.Exit()
        End Sub
    End Module
End Namespace
