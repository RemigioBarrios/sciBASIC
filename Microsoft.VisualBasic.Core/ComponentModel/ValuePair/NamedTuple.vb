﻿#Region "Microsoft.VisualBasic::7fcc9d024b07357353ddd681a28a6080, Microsoft.VisualBasic.Core\ComponentModel\ValuePair\NamedTuple.vb"

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

    '     Class NamedTuple
    ' 
    '         Properties: Item1, Item2, Name
    ' 
    '         Constructor: (+3 Overloads) Sub New
    '         Function: AsTuple, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository

Namespace ComponentModel

    Public Class NamedTuple(Of T) : Implements INamedValue

        Public Property Name As String Implements IKeyedEntity(Of String).Key
        Public Property Item1 As T
        Public Property Item2 As T

        Sub New()
        End Sub

        Sub New(item1 As T, item2 As T)
            Me.Item1 = item1
            Me.Item2 = item2
        End Sub

        Sub New(name$, item1 As T, item2 As T)
            Me.New(item1, item2)
            Me.Name = name
        End Sub

        Public Function AsTuple() As NamedValue(Of (T, T))
            Return New NamedValue(Of (T, T)) With {
                .Name = Name,
                .Value = (Item1, Item2)
            }
        End Function

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
