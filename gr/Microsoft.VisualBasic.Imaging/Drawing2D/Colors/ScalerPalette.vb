﻿#Region "Microsoft.VisualBasic::b539378823b3b27f0809dbcc63dc646b, sciBASIC#\gr\Microsoft.VisualBasic.Imaging\Drawing2D\Colors\ScalerPalette.vb"

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

'   Total Lines: 62
'    Code Lines: 26
' Comment Lines: 27
'   Blank Lines: 9
'     File Size: 1.67 KB


'     Enum ScalerPalette
' 
'         Autumn, Cool, FlexImaging, Gray, Hot
'         Rainbow, Spring, Summer, Winter
' 
'  
' 
' 
' 
' 
' /********************************************************************************/

#End Region

Imports System.ComponentModel

Namespace Drawing2D.Colors

    ''' <summary>
    ''' color set for visualize linear scale data color mapping
    ''' </summary>
    Public Enum ScalerPalette

        ''' <summary>
        ''' <see cref="ColorMap.PatternJet"/>
        ''' </summary>
        Jet = 0

        ''' <summary>
        ''' <see cref="ColorMap.PatternAutumn"/>
        ''' </summary>
        Autumn
        ''' <summary>
        ''' <see cref="ColorMap.PatternCool"/>
        ''' </summary>
        Cool
        ''' <summary>
        ''' <see cref="ColorMap.PatternGray"/>
        ''' </summary>
        Gray
        ''' <summary>
        ''' <see cref="ColorMap.PatternHot"/>
        ''' </summary>
        Hot
        ''' <summary>
        ''' <see cref="ColorMap.PatternSpring"/>
        ''' </summary>
        Spring
        ''' <summary>
        ''' <see cref="ColorMap.PatternSummer"/>
        ''' </summary>
        Summer
        ''' <summary>
        ''' <see cref="ColorMap.PatternWinter"/>
        ''' </summary>
        Winter

        <Description("red_channel")> Red
        <Description("green_channel")> Green
        <Description("blue_channel")> Blue

        Rainbow
        FlexImaging
        Typhoon

        <Description("viridis")> viridis
        <Description("viridis:magma")> magma
        <Description("viridis:inferno")> inferno
        <Description("viridis:plasma")> plasma
        <Description("viridis:cividis")> cividis
        <Description("viridis:mako")> mako
        <Description("viridis:rocket")> rocket
        <Description("viridis:turbo")> turbo

    End Enum

End Namespace
