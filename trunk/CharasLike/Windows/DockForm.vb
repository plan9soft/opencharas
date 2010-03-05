Public Class DockForm

    Public FrmCont As New WindowDockContainer
    Private Sub DockForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(FrmCont)
        FrmCont.Dock = DockStyle.Fill

        With FrmCont
            .AddForm(Canvas, WindowsDockStyle.Fill)
            .AddForm(ItemsWindow, WindowsDockStyle.Bottom)
            .AddForm(LayersWindow, WindowsDockStyle.Top)
            .AddForm(CharacterSelect, WindowsDockStyle.Left)
        End With
    End Sub

    Private Sub DockForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Canvas.Close()

        'If Canvas.FinalClosing = False Then e.Cancel = True
    End Sub
End Class