﻿#Region "Microsoft.VisualBasic::d8dd6a59c3b2a1e7dc1fa15d9ec356a7, Data_science\MachineLearning\Bootstrapping\Darwinism\GAF\Abstract\ParameterVector.vb"

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

    '     Class ParameterVector
    ' 
    '         Function: ChromosomeMutate, Clone, (+2 Overloads) Crossover, Mutate, Yield
    ' 
    '         Sub: Put
    ' 
    '     Class FFFF
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.MachineLearning.Darwinism
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.Models

Namespace Darwinism.GAF.Abstract

    Public Class ParameterVector(Of T As ParameterVector(Of T))
        Implements Chromosome(Of ParameterVector(Of T)), ICloneable
        Implements IIndividual

        Public Sub Put(i As Integer, value As Double) Implements IIndividual.Put
            Throw New NotImplementedException()
        End Sub

        Public Function Yield(i As Integer) As Double Implements IIndividual.Yield
            Throw New NotImplementedException()
        End Function

        Private Function Crossover(anotherChromosome As IIndividual) As IEnumerable(Of IIndividual) Implements Chromosome(Of IIndividual).Crossover
            Return Crossover(TryCast(anotherChromosome, ParameterVector(Of T)))
        End Function

        Public Function Crossover(anotherChromosome As ParameterVector(Of T)) As IEnumerable(Of ParameterVector(Of T)) Implements Chromosome(Of ParameterVector(Of T)).Crossover
            Throw New NotImplementedException()
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Private Function Mutate() As IIndividual Implements Chromosome(Of IIndividual).Mutate
            Return ChromosomeMutate()
        End Function

        Public Overridable Function Clone() As Object Implements ICloneable.Clone
            Throw New NotImplementedException()
        End Function

        Public Overridable Function ChromosomeMutate() As ParameterVector(Of T) Implements Chromosome(Of ParameterVector(Of T)).Mutate
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class FFFF : Inherits ParameterVector(Of FFFF)


    End Class
End Namespace
