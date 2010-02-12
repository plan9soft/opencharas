﻿Imports System.Windows.Forms

Public Class CrashReport

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CrashReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBox1.Image = SystemIcons.Error.ToBitmap()
    End Sub

    Public Folder_Path As String
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If String.IsNullOrEmpty(TextBox1.Text) Then
            MsgBox("You need to enter a name!")
            Return
        End If

        Process.Start("explorer.exe", Folder_Path.Substring(0, Folder_Path.LastIndexOf("\")))
        Process.Start("http://www.glennfamily.us/Paril/Uploader/?name=" + System.Web.HttpUtility.UrlEncode(TextBox1.Text) + "&email=" + System.Web.HttpUtility.UrlEncode(TextBox2.Text))
    End Sub
End Class