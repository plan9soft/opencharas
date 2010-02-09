<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmContainer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.HeaderWindow1 = New HeaderWindow
        Me.SuspendLayout()
        '
        'HeaderWindow1
        '
        Me.HeaderWindow1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.HeaderWindow1.Caption = ""
        Me.HeaderWindow1.Color1 = System.Drawing.Color.Transparent
        Me.HeaderWindow1.Color2 = System.Drawing.Color.White
        Me.HeaderWindow1.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderWindow1.EnableParentMoving = False
        Me.HeaderWindow1.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.HeaderWindow1.Location = New System.Drawing.Point(0, 0)
        Me.HeaderWindow1.Name = "HeaderWindow1"
        Me.HeaderWindow1.Size = New System.Drawing.Size(367, 20)
        Me.HeaderWindow1.TabIndex = 0
        Me.HeaderWindow1.UseGlassStyle = True
        '
        'FrmContainer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(367, 264)
        Me.Controls.Add(Me.HeaderWindow1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "FrmContainer"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents HeaderWindow1 As HeaderWindow

End Class
