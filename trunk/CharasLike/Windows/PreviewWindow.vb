Public Class PreviewWindow

    Private RenderedImage As Bitmap

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Create the render
        RenderedImage = Canvas.RenderToTexture()
        PictureBox1.Image = RenderedImage
    End Sub

    Private Sub Form4_Move(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If EasyPosition.Visible = True Then Return

        'Dockable.CheckDocking()
    End Sub
End Class