<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOtherContainer
    Inherits FrmContainer

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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'HeaderWindow1
        '
        Me.HeaderWindow1.Size = New System.Drawing.Size(238, 20)
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(238, 185)
        Me.Panel1.TabIndex = 2
        '
        'FrmOtherContainer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(238, 205)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.Name = "FrmOtherContainer"
        Me.Controls.SetChildIndex(Me.HeaderWindow1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Panel1 As System.Windows.Forms.Panel

End Class
