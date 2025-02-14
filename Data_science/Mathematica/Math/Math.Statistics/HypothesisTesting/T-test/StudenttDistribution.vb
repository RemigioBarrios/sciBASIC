﻿#Region "Microsoft.VisualBasic::70f4c0919f05e3c0800416fc40688f81, sciBASIC#\Data_science\Mathematica\Math\Math.Statistics\HypothesisTesting\T-test\StudenttDistribution.vb"

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

    '   Total Lines: 47
    '    Code Lines: 34
    ' Comment Lines: 3
    '   Blank Lines: 10
    '     File Size: 1.96 KB


    '     Class StudenttDistribution
    ' 
    '         Properties: DegreeOfFreedom, pdf_const
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: cdf, inv, pdf
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports stdNum = System.Math
Imports cephes = Microsoft.VisualBasic.Math.Statistics.SpecialFunctions

Namespace Hypothesis

    Public Class StudenttDistribution

        Public ReadOnly Property DegreeOfFreedom As Integer
        Public ReadOnly Property pdf_const As Double

        Sub New(df As Integer)
            DegreeOfFreedom = df
            ' Math.exp(cephes.lgam((df + 1) / 2) - cephes.lgam(df / 2)) / Math.sqrt(this._df * Math.PI)
            pdf_const = stdNum.Exp(cephes.gammaln((df + 1) / 2) - cephes.gammaln(df / 2)) / stdNum.Sqrt(df * stdNum.PI)
        End Sub

        Public Function pdf(x As Double) As Double
            Return pdf_const / stdNum.Pow(1 + ((x * x) / DegreeOfFreedom), (DegreeOfFreedom + 1) / 2)
        End Function

        Public Function cdf(x As Double) As Double
            Return t.Tcdf(x, DegreeOfFreedom)
        End Function

        Public Function inv(p As Double) As Double
            If (p <= 0) Then Return Double.NegativeInfinity
            If (p >= 1) Then Return Double.PositiveInfinity
            If (p = 0.5) Then Return 0

            If (p > 0.25 AndAlso p < 0.75) Then
                Dim phat = 1 - 2 * p
                Dim z = cephes.RegularizedIncompleteBetaFunction(0.5, 0.5 * DegreeOfFreedom, stdNum.Abs(phat))
                ' Dim z = cephes.incbi(0.5, 0.5 * DegreeOfFreedom, stdNum.Abs(phat))
                Dim t = stdNum.Sqrt(DegreeOfFreedom * z / (1 - z))

                Return If(p < 0.5, -t, t)
            Else
                Dim phat = If(p >= 0.5, 1 - p, p)
                Dim z = cephes.RegularizedIncompleteBetaFunction(0.5 * DegreeOfFreedom, 0.5, 2 * phat)
                ' Dim z = cephes.incbi(0.5 * DegreeOfFreedom, 0.5, 2 * phat)
                Dim t = stdNum.Sqrt(DegreeOfFreedom / z - DegreeOfFreedom)

                Return If(p < 0.5, -t, t)
            End If
        End Function
    End Class
End Namespace
