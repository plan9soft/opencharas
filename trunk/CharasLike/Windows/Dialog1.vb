Imports System.Windows.Forms

Public Class DontAskDialog
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Overloads Function ShowDialog(ByVal Prompt As String, Optional ByVal Img As Image = Nothing, Optional ByVal Title As String = Nothing) As MsgBoxResult
        If Img IsNot Nothing Then
            PictureBox1.Image = Img
            PictureBox1.Visible = True
        Else
            PictureBox1.Visible = False
        End If

        If Title IsNot Nothing Then
            Me.Text = Title
        Else
            Me.Text = Application.ProductName
        End If

        Label1.Text = Prompt

        Return ShowDialog()
    End Function
End Class
