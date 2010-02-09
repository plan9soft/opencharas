Public Class DockForm

    Public FrmCont As New WindowDockContainer
    Private Sub DockForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(FrmCont)
        FrmCont.Dock = DockStyle.Fill

        With FrmCont
            .AddForm(Canvas, WindowsDockStyle.Fill)
            .AddForm(ItemsWindow, WindowsDockStyle.Bottom)
            .AddForm(LayersWindow, WindowsDockStyle.Top)
        End With
    End Sub
End Class