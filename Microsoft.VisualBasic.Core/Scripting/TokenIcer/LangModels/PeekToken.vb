﻿#Region "Microsoft.VisualBasic::e933c535623377918a8c6a0f6d714d80, Microsoft.VisualBasic.Core\Scripting\TokenIcer\LangModels\PeekToken.vb"

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

    '     Class PeekToken
    ' 
    '         Properties: TokenIndex, TokenPeek
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Scripting.TokenIcer

    ''' <summary>
    ''' A PeekToken object class, This defines the PeekToken object
    ''' </summary>
    ''' <remarks>
    ''' A PeekToken is a special pointer object that can be used to Peek() several
    ''' tokens ahead in the GetToken() queue.
    ''' </remarks>
    Public Class PeekToken(Of Tokens As IComparable)

        Public Property TokenIndex As Integer
        Public Property TokenPeek As Token(Of Tokens)

        Public Sub New(index As Integer, value As Token(Of Tokens))
            TokenIndex = index
            TokenPeek = value
        End Sub

        Public Overrides Function ToString() As String
            Return $"[{TokenIndex}]  {TokenPeek.ToString}"
        End Function
    End Class
End Namespace
