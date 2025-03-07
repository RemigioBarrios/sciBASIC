﻿Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Text

Public Module Debugger

    <Extension>
    Public Sub WritePlsResult(x As MultivariateAnalysisResult, sw As TextWriter)
        sw.WriteLine("Method" & ASCII.TAB & "PLS")
        sw.WriteLine("Optimized factor" & ASCII.TAB & x.OptimizedFactor.ToString())
        sw.WriteLine()
        sw.WriteLine("Cross validation N fold" & ASCII.TAB & x.NFold.ToString())
        sw.WriteLine("Component" & ASCII.TAB & "SSCV" & ASCII.TAB & "PRESS" & ASCII.TAB & "Q2" & ASCII.TAB & "Q2cum")
        For i = 0 To x.Presses.Count - 1
            sw.WriteLine((i + 1).ToString() & ASCII.TAB & x.SsCVs(i).ToString() & ASCII.TAB & x.Presses(i).ToString() & ASCII.TAB & x.Q2Values(i).ToString() & ASCII.TAB & x.Q2Cums(i).ToString())
        Next
        sw.WriteLine()

        Dim scoreSeq = New List(Of String)()
        Dim loadSeq = New List(Of String)()

        For i = 0 To x.OptimizedFactor - 1
            scoreSeq.Add("T" & (i + 1).ToString())
            loadSeq.Add("P" & (i + 1).ToString())
        Next

        scoreSeq.Add("Y experiment")
        scoreSeq.Add("Y predicted")
        loadSeq.Add("VIP")
        loadSeq.Add("Coefficients")

        Dim scoreSeqString = String.Join(ASCII.TAB, scoreSeq)
        Dim loadSeqString = String.Join(ASCII.TAB, loadSeq)

        'header set
        Dim tpredSize = x.TPreds.Count
        Dim toPredSize = x.ToPreds.Count
        Dim metSize = x.StatisticsObject.XIndexes.Count
        Dim fileSize = x.StatisticsObject.YIndexes.Count

        sw.WriteLine("Score" & ASCII.TAB & scoreSeqString)

        'Scores
        For i = 0 To fileSize - 1
            Dim tList = New List(Of Double)()
            For j = 0 To x.TPreds.Count - 1
                tList.Add(x.TPreds(j)(i))
            Next
            tList.Add(x.StatisticsObject.YVariables(i))
            tList.Add(x.PredictedYs(i))

            sw.WriteLine(x.StatisticsObject.YLabels(i) & ASCII.TAB & String.Join(ASCII.TAB, tList))
        Next
        sw.WriteLine()

        'Loadings
        sw.WriteLine("Loading" & ASCII.TAB & loadSeqString)
        For i = 0 To metSize - 1
            Dim pList = New List(Of Double)()
            For j = 0 To x.PPreds.Count - 1
                pList.Add(x.PPreds(j)(i))
            Next
            pList.Add(x.Vips(i))
            pList.Add(x.Coefficients(i))

            sw.WriteLine(x.StatisticsObject.XLabels(i) & ASCII.TAB & String.Join(ASCII.TAB, pList))
        Next
    End Sub

    <Extension>
    Public Sub WritePlsResult(x As MultivariateAnalysisResult, output As Stream)
        Using sw As New StreamWriter(output, Encoding.ASCII)
            Call x.WritePlsResult(sw)
        End Using
    End Sub

    <Extension>
    Public Sub WriteOplsResult(x As MultivariateAnalysisResult, sw As TextWriter)
        sw.WriteLine("Method" & ASCII.TAB & "OPLS")
        sw.WriteLine("Optimized biological factor" & ASCII.TAB & x.OptimizedFactor.ToString())
        sw.WriteLine("Optimized orthogonal factor" & ASCII.TAB & x.OptimizedOrthoFactor.ToString())
        sw.WriteLine()
        sw.WriteLine("Cross validation N fold" & ASCII.TAB & x.NFold.ToString())
        sw.WriteLine("Component" & ASCII.TAB & "SSCV" & ASCII.TAB & "PRESS" & ASCII.TAB & "Q2" & ASCII.TAB & "Q2cum")
        For i = 0 To x.Presses.Count - 1
            sw.WriteLine((i + 1).ToString() & ASCII.TAB & x.SsCVs(i).ToString() & ASCII.TAB & x.Presses(i).ToString() & ASCII.TAB & x.Q2Values(i).ToString() & ASCII.TAB & x.Q2Cums(i).ToString())
        Next
        sw.WriteLine()

        Dim scoreSeq = New List(Of String)()
        Dim loadSeq = New List(Of String)()

        For i = 0 To x.OptimizedFactor - 1
            scoreSeq.Add("T" & (i + 1).ToString())
            loadSeq.Add("P" & (i + 1).ToString())
        Next

        For i = 0 To x.OptimizedOrthoFactor - 1
            scoreSeq.Add("To" & (i + 1).ToString())
            loadSeq.Add("Po" & (i + 1).ToString())
        Next

        scoreSeq.Add("Y experiment")
        scoreSeq.Add("Y predicted")
        loadSeq.Add("VIP")
        loadSeq.Add("Coefficients")

        Dim scoreSeqString = String.Join(ASCII.TAB, scoreSeq)
        Dim loadSeqString = String.Join(ASCII.TAB, loadSeq)

        'header set
        Dim tpredSize = x.TPreds.Count
        Dim toPredSize = x.ToPreds.Count
        Dim metSize = x.StatisticsObject.XIndexes.Count
        Dim fileSize = x.StatisticsObject.YIndexes.Count

        sw.WriteLine("Score" & ASCII.TAB & scoreSeqString)

        'Scores
        For i = 0 To fileSize - 1
            Dim tList = New List(Of Double)()
            For j = 0 To x.TPreds.Count - 1
                tList.Add(x.TPreds(j)(i))
            Next
            For j = 0 To x.ToPreds.Count - 1
                tList.Add(x.ToPreds(j)(i))
            Next
            tList.Add(x.StatisticsObject.YVariables(i))
            tList.Add(x.PredictedYs(i))

            sw.WriteLine(x.StatisticsObject.YLabels(i) & ASCII.TAB & String.Join(ASCII.TAB, tList))
        Next
        sw.WriteLine()

        'Loadings
        sw.WriteLine("Loading" & ASCII.TAB & loadSeqString)
        For i = 0 To metSize - 1
            Dim pList = New List(Of Double)()
            For j = 0 To x.PPreds.Count - 1
                pList.Add(x.PPreds(j)(i))
            Next
            For j = 0 To x.PoPreds.Count - 1
                pList.Add(x.PoPreds(j)(i))
            Next
            pList.Add(x.Vips(i))
            pList.Add(x.Coefficients(i))

            sw.WriteLine(x.StatisticsObject.XLabels(i) & ASCII.TAB & String.Join(ASCII.TAB, pList))
        Next
    End Sub

    <Extension>
    Public Sub WriteOplsResult(x As MultivariateAnalysisResult, output As Stream)
        Using sw As New StreamWriter(output, Encoding.ASCII)
            Call x.WriteOplsResult(sw)
        End Using
    End Sub

    <Extension>
    Public Sub WritePcaResult(x As MultivariateAnalysisResult, sw As TextWriter)
        'header set
        sw.WriteLine("Contribution")
        For i = 0 To x.Contributions.Count - 1
            sw.WriteLine((i + 1).ToString() & ASCII.TAB & x.Contributions(i).ToString())
        Next
        sw.WriteLine()

        Dim compSize = x.Contributions.Count
        Dim filesize = x.StatisticsObject.YLabels.Count
        Dim metsize = x.StatisticsObject.XLabels.Count
        Dim compSequence = New List(Of Integer)()
        For i = 0 To compSize - 1
            compSequence.Add(i + 1)
        Next
        Dim compSeqString = String.Join(ASCII.TAB, compSequence)

        'header set
        sw.WriteLine("Score" & ASCII.TAB & compSeqString)

        For i = 0 To filesize - 1
            Dim tList = New List(Of Double)()
            For j = 0 To compSize - 1
                tList.Add(x.TPreds(j)(i))
            Next
            sw.WriteLine(x.StatisticsObject.YLabels(i) & ASCII.TAB & String.Join(ASCII.TAB, tList))
        Next

        sw.WriteLine()

        'header set
        sw.WriteLine("Loading" & ASCII.TAB & compSeqString)

        For i = 0 To metsize - 1
            Dim pList = New List(Of Double)()
            For j = 0 To compSize - 1
                pList.Add(x.PPreds(j)(i))
            Next
            sw.WriteLine(x.StatisticsObject.XLabels(i) & ASCII.TAB & String.Join(ASCII.TAB, pList))
        Next
    End Sub

    <Extension>
    Public Sub WritePcaResult(x As MultivariateAnalysisResult, output As Stream)
        Using sw As New StreamWriter(output, Encoding.ASCII)
            Call x.WritePcaResult(sw)
        End Using
    End Sub
End Module
