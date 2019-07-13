﻿#Region "Microsoft.VisualBasic::0adbc52120f4399bc4bd23ef4b26a98e, Data_science\Mathematica\Math\Math\Quantile\X.vb"

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

    '     Class X
    ' 
    '         Properties: delta, g, value
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

'
'   Copyright 2012 Andrew Wang (andrew@umbrant.com)
'
'   Licensed under the Apache License, Version 2.0 (the "License");
'   you may not use this file except in compliance with the License.
'   You may obtain a copy of the License at
'
'       http://www.apache.org/licenses/LICENSE-2.0
'
'   Unless required by applicable law or agreed to in writing, software
'   distributed under the License is distributed on an "AS IS" BASIS,
'   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'   See the License for the specific language governing permissions and
'   limitations under the License.
'

Namespace Quantile

    ''' <summary>
    ''' Data variables
    ''' </summary>
    Public Class X

        Public ReadOnly Property value As Double
        Public Property g As Integer
        Public ReadOnly Property delta As Integer

        Public Sub New(value#, lower_delta%, delta%)
            Me.value = value
            Me.g = lower_delta
            Me.delta = delta
        End Sub

        Public Overrides Function ToString() As String
            Return $"{value.ToString("F2")}@[delta={delta.ToString("F2")}, lower_delta={g.ToString("F2")}]"
        End Function
    End Class
End Namespace
