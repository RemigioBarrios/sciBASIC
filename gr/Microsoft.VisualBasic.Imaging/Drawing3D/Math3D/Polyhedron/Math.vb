﻿Imports System.Math

Namespace Drawing3D.Math3D

    Module PolyhedronMath

        ''' <summary>
        ''' Sign of the vertex P with respect to O, as defined above.
        ''' </summary>
        ''' <param name="P"></param>
        ''' <param name="O"></param>
        ''' <returns></returns>
        Public Function vertex_sign(P As Double(), O As Double()) As Integer
            Dim result = Sign(P(0) - O(0)) Or Sign(P(1) - O(1)) Or Sign(P(2) - O(2))

            If Not result Then
                Throw New InvalidConstraintException("vertex coincides with origin")
            End If

            Return result
        End Function

        ''' <summary>
        ''' Sign of the edge PQ with respect to O, as defined above.
        ''' </summary>
        ''' <param name="P"></param>
        ''' <param name="Q"></param>
        ''' <param name="O"></param>
        ''' <returns></returns>
        Public Function edge_sign(P As Double(), Q As Double(), O As Double()) As Integer
            Dim result = (
                Sign((P(1) - O(1)) * (Q(0) - O(0)) - (P(0) - O(0)) * (Q(1) - O(1))) Or
                Sign((P(2) - O(2)) * (Q(0) - O(0)) - (P(0) - O(0)) * (Q(2) - O(2))) Or
                Sign((P(2) - O(2)) * (Q(1) - O(1)) - (P(1) - O(1)) * (Q(2) - O(2)))
            )

            If Not result Then
                Throw New InvalidConstraintException("vertices collinear with origin")
            End If

            Return result
        End Function

        ''' <summary>
        ''' Sign of the triangle PQR with respect to O, as defined above.
        ''' </summary>
        ''' <param name="P"></param>
        ''' <param name="Q"></param>
        ''' <param name="R"></param>
        ''' <param name="O"></param>
        ''' <returns></returns>
        Public Function triangle_sign(P As Double(), Q As Double(), R As Double(), O As Double()) As Integer
            Dim m1_0 = P(0) - O(0)
            Dim m1_1 = P(1) - O(1)
            Dim m2_0 = Q(0) - O(0)
            Dim m2_1 = Q(1) - O(1)
            Dim m3_0 = R(0) - O(0)
            Dim m3_1 = R(1) - O(1)
            Dim result = Sign(
                (m1_0 * m2_1 - m1_1 * m2_0) * (R(2) - O(2)) +
                (m2_0 * m3_1 - m2_1 * m3_0) * (P(2) - O(2)) +
                (m3_0 * m1_1 - m3_1 * m1_0) * (Q(2) - O(2))
            )

            If Not result Then
                Throw New InvalidConstraintException("vertices coplanar with origin")
            End If

            Return result
        End Function

        ''' <summary>
        ''' Return the contribution of this triangle to the winding number.
        ''' Raise ValueError If the face contains the origin.
        ''' </summary>
        ''' <param name="v1"></param>
        ''' <param name="v2"></param>
        ''' <param name="v3"></param>
        ''' <param name="origin"></param>
        ''' <returns></returns>
        Public Function triangle_chain(v1 As Double(), v2 As Double(), v3 As Double(), origin As Double()) As Integer
            Dim v1sign = vertex_sign(v1, origin)
            Dim v2sign = vertex_sign(v2, origin)
            Dim v3sign = vertex_sign(v3, origin)
            Dim face_boundary = 0

            If v1sign <> v2sign Then face_boundary += edge_sign(v1, v2, origin)
            If v2sign <> v3sign Then face_boundary += edge_sign(v2, v3, origin)
            If v3sign <> v1sign Then face_boundary += edge_sign(v3, v1, origin)

            If Not face_boundary Then
                Return 0
            End If

            Return triangle_sign(v1, v2, v3, origin)
        End Function
    End Module
End Namespace