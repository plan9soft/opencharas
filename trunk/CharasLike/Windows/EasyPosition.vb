Public Class EasyPosition

    Private Sub Form5_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        If Me.Visible() Then
            Me.Close()
        End If
    End Sub

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If LayersWindow.CurrentNode Is Nothing Then
            Label1.Text = ""
            Return
        End If

        Label1.SendToBack()
        Label1.Text = LayersWindow.CurrentNode.Layer.Offset.X.ToString + "," + LayersWindow.CurrentNode.Layer.Offset.Y.ToString()
    End Sub

    Private Sub Form5_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If LayersWindow.CurrentNode Is Nothing Then Return
        LayersWindow.CurrentNode.Layer.Offset = New Point(0, 0)
        Canvas.UpdateDrawing()
        Label1.Text = LayersWindow.CurrentNode.Layer.Offset.X.ToString + "," + LayersWindow.CurrentNode.Layer.Offset.Y.ToString()
    End Sub

    Public Sub OffsetIt(ByVal Pnt As Point)
        If LayersWindow.CurrentNode Is Nothing Then Return
        LayersWindow.CurrentNode.Layer.Offset += Pnt
        Canvas.UpdateDrawing()
        Label1.Text = LayersWindow.CurrentNode.Layer.Offset.X.ToString + "," + LayersWindow.CurrentNode.Layer.Offset.Y.ToString()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OffsetIt(New Point(0, -1))
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        OffsetIt(New Point(1, -1))
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        OffsetIt(New Point(1, 0))
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        OffsetIt(New Point(1, 1))
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OffsetIt(New Point(0, 1))
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        OffsetIt(New Point(-1, 1))
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OffsetIt(New Point(-1, 0))
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        OffsetIt(New Point(-1, -1))
    End Sub
End Class