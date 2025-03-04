﻿#Region "Microsoft.VisualBasic::ee360feb76cb8d6958eafb4643ab55c5, sciBASIC#\Microsoft.VisualBasic.Core\src\ApplicationServices\Terminal\TablePrinter\ConsoleColorNullable.vb"

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

    '   Total Lines: 24
    '    Code Lines: 20
    ' Comment Lines: 0
    '   Blank Lines: 4
    '     File Size: 857 B


    '     Class ConsoleColorNullable
    ' 
    '         Properties: BackgroundColor, ForegroundColor
    ' 
    '         Constructor: (+3 Overloads) Sub New
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace ApplicationServices.Terminal.TablePrinter

    Public Class ConsoleColorNullable

        Public Property ForegroundColor As ConsoleColor?
        Public Property BackgroundColor As ConsoleColor?

        Public Sub New()
        End Sub

        Public Sub New(foregroundColor As ConsoleColor)
            Me.ForegroundColor = foregroundColor
        End Sub

        Public Sub New(foregroundColor As ConsoleColor, backgroundColor As ConsoleColor)
            Me.ForegroundColor = foregroundColor
            Me.BackgroundColor = backgroundColor
        End Sub
    End Class
End Namespace
