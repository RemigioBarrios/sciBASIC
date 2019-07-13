﻿#Region "Microsoft.VisualBasic::569e94a0eee2f4b49b160f19eb302c50, Microsoft.VisualBasic.Core\ApplicationServices\Tools\Win32\HardwareInfo.vb"

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

    '     Module HardwareInfo
    ' 
    '         Function: CPU_Id, HarddriveInfo, SystemSerialNumber
    ' 
    '     Class HardDrive
    ' 
    '         Properties: Model, SerialNo, Type
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Management
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Win32

    ''' <summary>
    ''' Get mother board serial numbers and CPU IDs in Visual Basic .NET
    ''' </summary>
    Public Module HardwareInfo

        ''' <summary>
        ''' The following function gets a WMI object and then gets a collection of WMI_BaseBoard objects 
        ''' representing the system's mother boards. It loops through them getting their serial numbers. 
        ''' </summary>
        ''' <returns></returns>
        Public Function SystemSerialNumber() As String
            ' Get the Windows Management Instrumentation object.
            Dim wmi As Object = GetObject("WinMgmts:")
            ' Get the "base boards" (mother boards).
            Dim serial_numbers As New List(Of String)
            Dim mother_boards As Object = wmi.InstancesOf("Win32_BaseBoard")

            For Each board As Object In mother_boards
                serial_numbers += CStr(board.SerialNumber)
            Next

            Dim uid As String = String.Join("-", serial_numbers.ToArray)
            Return uid
        End Function

        ''' <summary>
        ''' The following code gets a WMI object and selects Win32_Processor objects. It loops through them getting their processor IDs. 
        ''' </summary>
        ''' <returns></returns>
        Public Function CPU_Id() As String
            Dim computer As String = "."
            Dim wmi As Object = GetObject("winmgmts:" & "{impersonationLevel=impersonate}!\\" & computer & "\root\cimv2")
            Dim processors As Object = wmi.ExecQuery("Select * from Win32_Processor")
            Dim cpu_ids As New List(Of String)

            For Each cpu As Object In processors
                cpu_ids += CStr(cpu.ProcessorId)
            Next

            Dim uid As String = String.Join("-", cpu_ids.ToArray)
            Return uid
        End Function

        ''' <summary>
        ''' How to Retrieve the REAL Hard Drive Serial Number.
        ''' 
        ''' Shows you how to obtain the hardware serial number set by the manufacturer and 
        ''' not the Volume Serial Number that changes after you format a hard drive.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' http://www.codeproject.com/Articles/6077/How-to-Retrieve-the-REAL-Hard-Drive-Serial-Number
        ''' </remarks>
        Public Function HarddriveInfo() As HardDrive()
            Dim hds As New List(Of HardDrive)
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
            Dim i As Pointer = 0

            For Each wmi_HD As ManagementObject In searcher.[Get]()
                Dim model As String = wmi_HD("Model").ToString()
                Dim type As String = wmi_HD("InterfaceType").ToString()

                hds += New HardDrive With {
                    .Model = model,
                    .Type = type
                }
            Next

            searcher = New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")

            For Each wmi_HD As ManagementObject In searcher.[Get]()
                ' get the hard drive from collection
                ' using index
                Dim hd As HardDrive = hds(++i)

                ' get the hardware serial no.
                If wmi_HD("SerialNumber") Is Nothing Then
                    hd.SerialNo = "None"
                Else
                    hd.SerialNo = wmi_HD("SerialNumber").ToString()
                End If
            Next

            Return hds
        End Function
    End Module

    Public Class HardDrive

        Public Property Model As String
        Public Property Type As String
        Public Property SerialNo As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
