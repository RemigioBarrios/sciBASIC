﻿#Region "Microsoft.VisualBasic::71de434a5a97075a03cf49c40263e6ea, sciBASIC#\Data_science\Visualization\Plots-statistics\PCA\PCA2D.vb"

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
'    Code Lines: 18
' Comment Lines: 0
'   Blank Lines: 6
'     File Size: 746 B


'     Class PCA2D
' 
'         Constructor: (+1 Overloads) Sub New
'         Sub: PlotInternal
' 
' 
' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Canvas
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Math.Statistics.Hypothesis.ANOVA

Namespace PCA

    Public Class PCA2D : Inherits Plot

        ReadOnly pca As MultivariateAnalysisResult
        ReadOnly groups As String()

        Public Sub New(PCA As MultivariateAnalysisResult, groups As String(), theme As Theme)
            MyBase.New(theme)
            Me.pca = PCA
            Me.groups = groups
        End Sub

        Protected Overrides Sub PlotInternal(ByRef g As IGraphics, canvas As GraphicsRegion)

        End Sub
    End Class
End Namespace
