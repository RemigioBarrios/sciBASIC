﻿#Region "Microsoft.VisualBasic::ac24885edf64e343051027db8519ca9a, sciBASIC#\Data_science\MachineLearning\MachineLearning\SVM\SVMPrediction.vb"

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

    '   Total Lines: 17
    '    Code Lines: 12
    ' Comment Lines: 0
    '   Blank Lines: 5
    '     File Size: 425 B


    '     Structure SVMPrediction
    ' 
    '         Properties: [class], score, unifyValue, vote
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Serialization.JSON

Namespace SVM

    Public Structure SVMPrediction

        Public Property [class] As Integer
        Public Property score As Double
        Public Property unifyValue As Double
        Public Property vote As Double()

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Structure
End Namespace
