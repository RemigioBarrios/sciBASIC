﻿#Region "Microsoft.VisualBasic::e6efa560946179068e820fadc6c5e0e3, mime\application%rdf+xml\RDF.vb"

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

    ' Class RDF
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: (+2 Overloads) LoadDocument
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Xml.Serialization

''' <summary>
''' the xml file serilization model
''' </summary>
''' 
<XmlType("RDF", [Namespace]:=RDFEntity.XmlnsNamespace)>
Public MustInherit Class RDF(Of T As Description)

    <XmlNamespaceDeclarations()>
    Public xmlns As New XmlSerializerNamespaces

    <XmlElement("Description", [Namespace]:=RDFEntity.XmlnsNamespace)>
    Public Property Description As T

    Sub New()
        xmlns.Add("rdf", RDFEntity.XmlnsNamespace)
    End Sub
End Class
