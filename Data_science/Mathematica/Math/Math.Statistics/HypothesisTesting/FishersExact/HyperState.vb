﻿#Region "Microsoft.VisualBasic::b8e8ae2facd37bb0a13e2e7560feffbf, Data_science\Mathematica\Math\Math.Statistics\HypothesisTesting\FishersExact\HyperState.vb"

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

    '     Class HyperState
    ' 
    '         Properties: n, n_1, n1_, n11, prob
    '                     valid
    ' 
    '         Function: ToString
    ' 
    '     Class FishersExactPvalues
    ' 
    '         Properties: greater_pvalue, hyper_state, less_pvalue, two_tail_pvalue
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Hypothesis.FishersExact

    Public Class HyperState
        Public Property n11 As Integer
        Public Property n1_ As Integer
        Public Property n_1 As Integer
        Public Property n As Integer
        Public Property prob As Double
        Public Property valid As Boolean

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class

    ''' <summary>
    ''' `FishersExactPvalues` holds the pvalues calculated by the `fishers_exact` function.
    ''' </summary>
    Public Class FishersExactPvalues

        ''' <summary>
        ''' pvalue for the two-tailed test. Use this when there Is no prior alternative.
        ''' </summary>
        ''' <returns></returns>
        Public Property two_tail_pvalue As Double
        ''' <summary>
        ''' pvalue for the "left" Or "lesser" tail. Use this when the alternative to
        ''' independence Is that there Is negative association between the variables.
        ''' That Is, the observations tend to lie in lower left And upper right.
        ''' </summary>
        ''' <returns></returns>
        Public Property less_pvalue As Double
        ''' <summary>
        ''' Use this when the alternative to independence Is that there Is positive
        ''' association between the variables. That Is, the observations tend to lie
        ''' in upper left And lower right.
        ''' </summary>
        ''' <returns></returns>
        Public Property greater_pvalue As Double

        Public Property hyper_state As HyperState

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
