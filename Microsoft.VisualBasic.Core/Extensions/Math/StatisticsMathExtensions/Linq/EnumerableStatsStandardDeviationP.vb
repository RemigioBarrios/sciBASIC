﻿#Region "Microsoft.VisualBasic::d977f07e7fc1a94b17764fb2c3698c7f, Microsoft.VisualBasic.Core\Extensions\Math\StatisticsMathExtensions\Linq\EnumerableStatsStandardDeviationP.vb"

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

    '     Module EnumerableStatsStandardDeviationP
    ' 
    '         Function: (+20 Overloads) StandardDeviationP
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports sys = System.Math

Namespace Math.Statistics.Linq

    Public Module EnumerableStatsStandardDeviationP
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Decimal values.
        '
        ' Parameters:
        '   source:
        '     A sequence of nullable System.Decimal values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Decimal.MaxValue.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Decimal?)) As Decimal
            Dim values As IEnumerable(Of Decimal) = source.Coalesce()
            If values.Any() Then
                Return values.StandardDeviationP()
            End If

            Return Nothing
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Decimal values.
        '
        ' Parameters:
        '   source:
        '     A sequence of System.Decimal values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Decimal.MaxValue.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Decimal)) As Decimal
            Return CDec(source.[Select](Function(x) CDbl(x)).StandardDeviationP())
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Double values.
        '
        ' Parameters:
        '   source:
        '     A sequence of nullable System.Double values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Double?)) As Double
            Dim values As IEnumerable(Of Double) = source.Coalesce()
            If values.Any() Then
                Return values.StandardDeviationP()
            End If

            Return Nothing
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Double values.
        '
        ' Parameters:
        '   source:
        '     A sequence of System.Double values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Double)) As Double
            Return sys.Sqrt(source.VarianceP())
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Single values.
        '
        ' Parameters:
        '   source:
        '     A sequence of nullable System.Single values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Single?)) As Single
            Dim values As IEnumerable(Of Single) = source.Coalesce()
            If values.Any() Then
                Return values.StandardDeviationP()
            End If

            Return Nothing
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Single values.
        '
        ' Parameters:
        '   source:
        '     A sequence of System.Single values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Single)) As Single
            Return CSng(source.[Select](Function(x) CDbl(x)).StandardDeviationP())
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Int32 values.
        '
        ' Parameters:
        '   source:
        '     A sequence of nullable System.Int32values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Int64.MaxValue.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Integer?)) As Double
            Dim values As IEnumerable(Of Integer) = source.Coalesce()
            If values.Any() Then
                Return values.StandardDeviationP()
            End If

            Return Nothing
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Int32 values.
        '
        ' Parameters:
        '   source:
        '     A sequence of System.Int32 values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Int64.MaxValue.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Integer)) As Double
            Return source.[Select](Function(x) CDbl(x)).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Int64 values.
        '
        ' Parameters:
        '   source:
        '     A sequence of nullable System.Int64 values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Int64.MaxValue.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Long?)) As Double
            Dim values As IEnumerable(Of Long) = source.Coalesce()
            If values.Any() Then
                Return values.StandardDeviationP()
            End If

            Return Nothing
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Int64 values.
        '
        ' Parameters:
        '   source:
        '     A sequence of System.Int64 values to calculate the StandardDeviationP of.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Int64.MaxValue.
        <Extension>
        Public Function StandardDeviationP(source As IEnumerable(Of Long)) As Double
            Return source.[Select](Function(x) CDbl(x)).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Decimal values that
        '     are obtained by invoking a transform function on each element of the input
        '     sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Decimal.MaxValue.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Decimal?)) As Decimal
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Decimal values that are obtained
        '     by invoking a transform function on each element of the input sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values that are used to calculate an StandardDeviationP.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Decimal.MaxValue.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Decimal)) As Decimal
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Double values that
        '     are obtained by invoking a transform function on each element of the input
        '     sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Double?)) As Double
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Double values that are obtained
        '     by invoking a transform function on each element of the input sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Double)) As Double
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Single values that
        '     are obtained by invoking a transform function on each element of the input
        '     sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Single?)) As Single
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Single values that are obtained
        '     by invoking a transform function on each element of the input sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Single)) As Single
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Int32 values that are
        '     obtained by invoking a transform function on each element of the input sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Int64.MaxValue.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Integer?)) As Double
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Int32 values that are obtained
        '     by invoking a transform function on each element of the input sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Int64.MaxValue.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Integer)) As Double
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of nullable System.Int64 values that are
        '     obtained by invoking a transform function on each element of the input sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values, or null if the source sequence is
        '     empty or contains only values that are null.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Long?)) As Double
            Return source.[Select](selector).StandardDeviationP()
        End Function
        '
        ' Summary:
        '     Computes the StandardDeviationP of a sequence of System.Int64 values that are obtained
        '     by invoking a transform function on each element of the input sequence.
        '
        ' Parameters:
        '   source:
        '     A sequence of values to calculate the StandardDeviationP of.
        '
        '   selector:
        '     A transform function to apply to each element.
        '
        ' Type parameters:
        '   TSource:
        '     The type of the elements of source.
        '
        ' Returns:
        '     The StandardDeviationP of the sequence of values.
        '
        ' Exceptions:
        '   System.ArgumentNullException:
        '     source or selector is null.
        '
        '   System.InvalidOperationException:
        '     source contains no elements.
        '
        '   System.OverflowException:
        '     The sum of the elements in the sequence is larger than System.Int64.MaxValue.
        <Extension>
        Public Function StandardDeviationP(Of TSource)(source As IEnumerable(Of TSource), selector As Func(Of TSource, Long)) As Double
            Return source.[Select](selector).StandardDeviationP()
        End Function
    End Module
End Namespace
