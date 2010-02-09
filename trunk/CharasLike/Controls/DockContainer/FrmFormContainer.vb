Public Class FrmFormContainer

    Private WithEvents frm As Form
    Public Sub SetForm(ByVal f As Form)
        frm = f
        With frm
            .TopLevel = False
            Me.Size = .Size
            Me.Location = .Location
            .FormBorderStyle = Windows.Forms.FormBorderStyle.None
            .Location = New Point(0, 0)
            .Size = Me.Panel1.Size
            Me.Text = .Text
            Me.TabPage.Text = .Text
            .Dock = Windows.Forms.DockStyle.Fill
        End With

        Me.Panel1.Controls.Add(frm)
    End Sub

    Private Sub FrmContainer_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frm.Activated, frm.GotFocus
        Me.HeaderWindow1.BackColor = Me.ActiveCaptionBackColor
        Me.HeaderWindow1.ForeColor = Me.ActiveCaptionForeColor
    End Sub

    Private Sub FrmContainer_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles frm.Deactivate, frm.LostFocus
        Me.HeaderWindow1.BackColor = Me.InActiveCaptionBackColor
        Me.HeaderWindow1.ForeColor = Me.InActiveCaptionForeColor
    End Sub

    Private Sub frm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles frm.TextChanged
        Me.Text = frm.Text
        Me.TabPage.Text = frm.Text
    End Sub

    Private Sub FrmFormContainer_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        Me.frm.Visible = Me.Visible
    End Sub
End Class
