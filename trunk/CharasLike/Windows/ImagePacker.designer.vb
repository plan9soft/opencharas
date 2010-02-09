<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImagePacker
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip()
        Me.PackEverythingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnpackEverythingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip()
        Me.PackAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip()
        Me.UnpackAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(557, 192)
        Me.SplitContainer1.SplitterDistance = 128
        Me.SplitContainer1.TabIndex = 0
        '
        'TreeView1
        '
        Me.TreeView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(128, 192)
        Me.TreeView1.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PackEverythingToolStripMenuItem, Me.UnpackEverythingToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(174, 48)
        '
        'PackEverythingToolStripMenuItem
        '
        Me.PackEverythingToolStripMenuItem.Name = "PackEverythingToolStripMenuItem"
        Me.PackEverythingToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.PackEverythingToolStripMenuItem.Text = "Pack Everything"
        '
        'UnpackEverythingToolStripMenuItem
        '
        Me.UnpackEverythingToolStripMenuItem.Name = "UnpackEverythingToolStripMenuItem"
        Me.UnpackEverythingToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.UnpackEverythingToolStripMenuItem.Text = "Unpack Everything"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.FlowLayoutPanel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.FlowLayoutPanel2)
        Me.SplitContainer2.Size = New System.Drawing.Size(425, 192)
        Me.SplitContainer2.SplitterDistance = 208
        Me.SplitContainer2.TabIndex = 0
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AllowDrop = True
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.ContextMenuStrip = Me.ContextMenuStrip2
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(204, 188)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PackAllToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(117, 26)
        '
        'PackAllToolStripMenuItem
        '
        Me.PackAllToolStripMenuItem.Name = "PackAllToolStripMenuItem"
        Me.PackAllToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.PackAllToolStripMenuItem.Text = "Pack All"
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.AllowDrop = True
        Me.FlowLayoutPanel2.AutoScroll = True
        Me.FlowLayoutPanel2.ContextMenuStrip = Me.ContextMenuStrip3
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(209, 188)
        Me.FlowLayoutPanel2.TabIndex = 0
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UnpackAllToolStripMenuItem})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(132, 26)
        '
        'UnpackAllToolStripMenuItem
        '
        Me.UnpackAllToolStripMenuItem.Name = "UnpackAllToolStripMenuItem"
        Me.UnpackAllToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.UnpackAllToolStripMenuItem.Text = "Unpack All"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 5000
        Me.ToolTip1.AutoPopDelay = 3000
        Me.ToolTip1.InitialDelay = 100
        Me.ToolTip1.ReshowDelay = 0
        Me.ToolTip1.ShowAlways = True
        Me.ToolTip1.UseAnimation = False
        Me.ToolTip1.UseFading = False
        '
        'SplitContainer3
        '
        Me.SplitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer3.Size = New System.Drawing.Size(559, 282)
        Me.SplitContainer3.SplitterDistance = 194
        Me.SplitContainer3.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(462, 44)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(84, 26)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(462, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(84, 26)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Done"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ImagePacker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 282)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(416, 263)
        Me.Name = "ImagePacker"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Image Packer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PackEverythingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnpackEverythingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PackAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip3 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UnpackAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
