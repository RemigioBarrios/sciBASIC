﻿#Region "Microsoft.VisualBasic::ed5c63b298fc2b051b5de076932172ae, Microsoft.VisualBasic.Core\Language\Language\UnixBash\Shell\rm.vb"

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

    '     Class FileDelete
    ' 
    '         Operators: -, <=, >=
    ' 
    '     Structure rmOption
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Language.UnixBash

    ''' <summary>
    ''' ``rm -rf /*``
    ''' </summary>
    Public Class FileDelete

        Public Shared Operator -(rm As FileDelete, opt As rmOption) As FileDelete
            Return rm
        End Operator

        Public Shared Operator <=(rm As FileDelete, file$) As Long
            Return 0
        End Operator

        Public Shared Operator >=(rm As FileDelete, file$) As Long
            Throw New NotSupportedException
        End Operator
    End Class

    Public Structure rmOption

    End Structure
End Namespace
