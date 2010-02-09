Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class QColorMatrix
    Dim PI As Single = 4.0F * Math.Atan(1.0)
    Dim Rad As Single = PI / 180.0F

    ' The luminance weight factors for the RGB color space.
    ' These values are actually preferable to the better known factors of
    ' Y = 0.30R + 0.59G + 0.11B, the formula which is used in color television technique.
    Const lumR As Single = 0.3086F
    Const lumG As Single = 0.6094F
    Const lumB As Single = 0.082F

    Shared Initialized As Boolean = False
    Shared PreHue As New QColorMatrix
    Shared PostHue As New QColorMatrix

    Private MyMatrix_ As New Imaging.ColorMatrix
    Public Property MyMatrix() As Imaging.ColorMatrix
        Get
            Return MyMatrix_
        End Get
        Set(ByVal value As Imaging.ColorMatrix)
            MyMatrix_ = value
        End Set
    End Property

    Private Sub Copy(ByVal CopyFrom As QColorMatrix)
        For x As Integer = 0 To 4
            For y As Integer = 0 To 4
                MyMatrix(x, y) = CopyFrom.MyMatrix(x, y)
            Next
        Next
    End Sub

    Sub Reset()
        For x As Integer = 0 To 4
            For y As Integer = 0 To 4
                If x = y Then
                    MyMatrix(x, y) = 1
                Else
                    MyMatrix(x, y) = 0
                End If
            Next
        Next
    End Sub

    Sub Multiply(ByVal Matrix As QColorMatrix, Optional ByVal Order As MatrixOrder = MatrixOrder.Prepend)
        ' NOTE: The last column is NOT calculated, because it is (or at least should be)
        ' always { 0, 0, 0, 0, 1 }.

        Dim A As Imaging.ColorMatrix
        Dim B As Imaging.ColorMatrix

        If Order = MatrixOrder.Append Then
            A = Matrix.MyMatrix
            B = MyMatrix
        Else
            A = MyMatrix
            B = Matrix.MyMatrix
        End If

        Dim Temp As New Imaging.ColorMatrix

        For y As Integer = 0 To 4
            For x As Integer = 0 To 3
                Dim T As Single = 0
                For i As Integer = 0 To 4
                    T += B(y, i) * A(i, x)
                Next
                Temp(y, x) = T
            Next
        Next

        For y As Integer = 0 To 4
            For x As Integer = 0 To 3
                MyMatrix(y, x) = Temp(y, x)
            Next
        Next
    End Sub

    ' Assumes that v points to (at least) four REALs.
    Sub TransformVector(ByRef V() As Single)
        Dim Temp(3) As Single

        For x As Integer = 0 To 3
            Temp(x) = MyMatrix.Item(4, x)
            For y As Integer = 0 To 3
                Temp(x) += V(y) * MyMatrix(y, x)
            Next
        Next

        For x As Integer = 0 To 3
            V(x) = Temp(x)
        Next
    End Sub

    Sub TransformColors(ByVal Colors As Collections.ObjectModel.Collection(Of Color))
        Dim P(3) As Single

        For i As Integer = 0 To Colors.Count - 1
            P(0) = Colors(i).R
            P(1) = Colors(i).G
            P(2) = Colors(i).B
            P(3) = Colors(i).A

            TransformVector(P)

            For j As Integer = 0 To 3
                If P(j) < 0 Then
                    P(j) = 0.0F
                ElseIf P(j) > 255.0F Then
                    P(j) = 255.0F
                End If
            Next

            Colors(i) = Color.FromArgb(P(0), P(1), P(2), P(3))
        Next
    End Sub

    ' phi is in degrees
    ' x and y are the indices of the value to receive the sin(phi) value
    Protected Sub RotateColor(ByVal Phi As Single, ByVal x As Integer, ByVal y As Integer, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Phi *= Rad

        Dim m As New QColorMatrix
        m.MyMatrix(x, x) = Math.Cos(Phi)
        m.MyMatrix(y, y) = Math.Cos(Phi)

        Dim s As Single = Math.Sin(Phi)
        m.MyMatrix(y, x) = s
        m.MyMatrix(x, y) = -s

        Multiply(m, order)
    End Sub

    Protected Sub ShearColor(ByVal x As Integer, ByVal y1 As Integer, ByVal d1 As Single, ByVal y2 As Integer, ByVal d2 As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Dim m As New QColorMatrix
        m.MyMatrix(y1, x) = d1
        m.MyMatrix(y2, x) = d2

        Multiply(m, order)
    End Sub

    Sub Scale(ByVal scaleRed As Single, ByVal scaleGreen As Single, ByVal scaleBlue As Single, ByVal scaleOpacity As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Dim m As New QColorMatrix
        m.MyMatrix(0, 0) = scaleRed
        m.MyMatrix(1, 1) = scaleGreen
        m.MyMatrix(2, 2) = scaleBlue
        m.MyMatrix(3, 3) = scaleOpacity

        Multiply(m, order)
    End Sub

    ' Scale just the three colors with the same amount, leave opacity unchanged.
    Sub ScaleColors(ByVal ScaleAmount As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Scale(ScaleAmount, ScaleAmount, ScaleAmount, 1.0F, order)
    End Sub

    ' Scale just the opacity, leave R, G and B unchanged.
    Sub ScaleOpacity(ByVal ScaleAmt As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Scale(1.0F, 1.0F, 1.0F, ScaleAmt, order)
    End Sub

    ' Rotate the matrix around one of the color axes. The color of the rotation
    ' axis is unchanged, the other two colors are rotated in color space.
    ' The angle phi is in degrees (-180.0f... 180.0f).
    Sub RotateRed(ByVal Phi As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        RotateColor(Phi, 2, 1, order)
    End Sub

    Sub RotateGreen(ByVal Phi As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        RotateColor(Phi, 0, 2, order)
    End Sub

    Sub RotateBlue(ByVal Phi As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        RotateColor(Phi, 1, 0, order)
    End Sub

    ' Shear the matrix in one of the color planes. The color of the color plane
    ' is influenced by the two other colors.
    Sub ShearRed(ByVal Green As Single, ByVal Blue As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        ShearColor(0, 1, Green, 2, Blue, order)
    End Sub

    Sub ShearGreen(ByVal Red As Single, ByVal Blue As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        ShearColor(1, 0, Red, 2, Blue, order)
    End Sub

    Sub ShearBlue(ByVal Red As Single, ByVal Green As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        ShearColor(2, 0, Red, 1, Green, order)
    End Sub

    Sub Translate(ByVal offsetRed As Single, ByVal offsetGreen As Single, ByVal offsetBlue As Single, ByVal offsetOpacity As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Dim m As New QColorMatrix
        m.MyMatrix(4, 0) = offsetRed
        m.MyMatrix(4, 1) = offsetGreen
        m.MyMatrix(4, 2) = offsetBlue
        m.MyMatrix(4, 3) = offsetOpacity

        Multiply(m, order)
    End Sub

    ' Translate just the three colors with the same amount, leave opacity unchanged.
    Sub TranslateColors(ByVal Offset As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Translate(Offset, Offset, Offset, 0.0, order)
    End Sub

    ' Translate just the opacity, leave R, G and B unchanged.
    Sub TranslateOpacity(ByVal OffsetOpacity As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        Translate(0.0F, 0.0F, 0.0F, OffsetOpacity, order)
    End Sub

    Sub SetSaturation(ByVal Saturation As Single, Optional ByVal order As MatrixOrder = MatrixOrder.Prepend)
        ' For the theory behind this, see the web sites at the top of this file.
        ' In short: if saturation is 1.0f, m becomes the identity matrix, and this matrix is
        ' unchanged. If saturation is 0.0f, each color is scaled by it's luminance weight.
        Dim SatCompl As Single = 1.0F - Saturation
        Dim SatComplR As Single = lumR * SatCompl
        Dim SatComplG As Single = lumG * SatCompl
        Dim SatComplB As Single = lumB * SatCompl

        Dim matrixValues As Single()() = { _
           New Single() {SatComplR + Saturation, SatComplR, SatComplR, 0, 0}, _
           New Single() {SatComplG, SatComplG + Saturation, SatComplG, 0, 0}, _
           New Single() {SatComplB, SatComplB, SatComplB + Saturation, 0, 0}, _
           New Single() {0, 0, 0, 1, 0}, _
           New Single() {0, 0, 0, 0, 1} _
        }

        Dim m As New QColorMatrix
        m.MyMatrix = New Imaging.ColorMatrix(matrixValues)

        Multiply(m, order)
    End Sub

    Sub RotateHue(ByVal Phi As Single)
        InitHue()

        ' Rotate the grey vector to the blue axis
        Multiply(PreHue, MatrixOrder.Append)

        ' Rotate around the blue axis
        RotateBlue(Phi, MatrixOrder.Append)

        Multiply(PostHue, MatrixOrder.Append)
    End Sub

    Private Shared Sub InitHue()
        Const greenRotation As Single = 35.0F

        ' NOTE: theoretically, greenRotation should have the value of 39.182655 degrees,
        ' being the angle for which the sine is 1/(sqrt(3)), and the cosine is sqrt(2/3).
        ' However, I found that using a slightly smaller angle works better.
        ' In particular, the greys in the image are not visibly affected with the smaller
        ' angle, while they deviate a little bit with the theoretical value.
        ' An explanation escapes me for now.
        ' If you rather stick with the theory, change the comments in the previous lines.


        If Initialized = False Then
            Initialized = True
            ' Rotating the hue of an image is a rather convoluted task, involving several matrix
            ' multiplications. For efficiency, we prepare two static matrices.
            ' This is by far the most complicated part of this class. For the background
            ' theory, refer to the sgi-sites mentioned at the top of this file.

            ' Prepare the preHue matrix.
            ' Rotate the grey vector in the green plane.
            PreHue.RotateRed(45.0F)

            ' Next, rotate it again in the green plane, so it coincides with the blue axis.
            PreHue.RotateGreen(-greenRotation, MatrixOrder.Append)

            ' Hue rotations keep the color luminations constant, so that only the hues change
            ' visible. To accomplish that, we shear the blue plane.
            Dim lum() As Single = {lumR, lumG, lumB, 1.0F}

            ' Transform the luminance vector.
            PreHue.TransformVector(lum)

            ' Calculate the shear factors for red and green.
            Dim red As Single = lum(0) / lum(2)
            Dim green As Single = lum(1) / lum(2)

            ' Shear the blue plane.
            PreHue.ShearBlue(red, green, MatrixOrder.Append)

            ' Prepare the postHue matrix. This holds the opposite transformations of the
            ' preHue matrix. In fact, postHue is the inversion of preHue.
            PostHue.ShearBlue(-red, -green)
            PostHue.RotateGreen(greenRotation, MatrixOrder.Append)
            PostHue.RotateRed(-45.0F, MatrixOrder.Append)
        End If
    End Sub

End Class