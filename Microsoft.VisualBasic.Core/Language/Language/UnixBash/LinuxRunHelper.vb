﻿#Region "Microsoft.VisualBasic::bed11e068fc896e1e7f8d9a91311ec0a, Microsoft.VisualBasic.Core\Language\Language\UnixBash\LinuxRunHelper.vb"

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

    '     Module LinuxRunHelper
    ' 
    '         Function: BashRun, BashShell, MonoRun, PerlShell, ScriptMe
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Reflection
Imports System.Text
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CommandLine

Namespace Language.UnixBash

    ''' <summary>
    ''' mono shortcuts
    ''' </summary>
    Public Module LinuxRunHelper

        ''' <summary>
        ''' perl ./<see cref="Assembly"/> @ARGV
        ''' </summary>
        ''' <returns></returns>
        Public Function ScriptMe() As String
            Dim cmd As String = Assembly.GetEntryAssembly.Location
            Dim perl As String =
$"#!/usr/bin/perl

use strict;
use warnings;
use File::Basename;
use File::Spec;

my $cli = join "" "", @ARGV;

print ""{App.AssemblyName} << $cli\n"";
system(""mono {cmd.CLIPath} $cli"");
"
            Return perl
        End Function

        ''' <summary>
        ''' Run from bash shell
        ''' </summary>
        ''' <returns></returns>
        Public Function BashRun() As String
            Dim cmd As String = Assembly.GetEntryAssembly.Location
            Dim bash As String =
$"#!/bin/sh

cli=""$@"";

echo ""{App.AssemblyName} <<< $@"";
mono ""{cmd}"" $cli
"
            Return bash.Replace(vbCr, "")
        End Function

        ''' <summary>
        ''' 这里比perl脚本掉调用有一个缺点，在运行前还需要使用命令修改为可执行权限
        ''' 'sudo chmod 777 cmd.sh'
        ''' </summary>
        ''' <returns></returns>
        Public Function BashShell() As Integer
            Dim path As String = Assembly.GetEntryAssembly.Location.TrimSuffix
            Return BashRun.SaveTo(path, Encoding.ASCII)
        End Function

        ''' <summary>
        ''' Execute command using perl script
        ''' </summary>
        ''' <returns></returns>
        Public Function PerlShell() As Integer
            Dim path As String = Assembly.GetEntryAssembly.Location.TrimSuffix & ".pl"
            Return ScriptMe.SaveTo(path)
        End Function

        Public Function MonoRun(app As String, CLI As String) As ProcessEx
            Dim proc As New ProcessEx With {
                .Bin = "mono",
                .CLIArguments = app.GetFullPath.CLIPath & " " & CLI
            }
            Return proc
        End Function
    End Module
End Namespace
