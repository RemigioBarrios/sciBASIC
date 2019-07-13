﻿#Region "Microsoft.VisualBasic::2cc647a87c1072853036d6f9bc3cba98, Data_science\Mathematica\Math\Math\test\SplineTest.vb"

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

    ' Module SplineTest
    ' 
    '     Sub: Main
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports Microsoft.VisualBasic.Math.Interpolation
Imports Microsoft.VisualBasic.FileIO

Module SplineTest

    Sub Main()
        Dim points = {
            New PointF(10, 10),
            New PointF(20, 20),
            New PointF(25, 30),
            New PointF(30, 60),
            New PointF(40, 50),
            New PointF(50, 10),
            New PointF(60, 1),
            New PointF(70, -5)
        }

        Dim spline = PolynomialNewton.NewtonPolynomial(points).ToArray

        Call points.DumpSerial("./in.csv")
        Call spline.DumpSerial("./out.csv")
    End Sub
End Module
