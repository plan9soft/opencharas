<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameEditor
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip()
        Me.MoveUpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GameToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(509, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GameToolStripMenuItem
        '
        Me.GameToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem5, Me.ToolStripMenuItem1, Me.ToolStripMenuItem3, Me.ToolStripMenuItem2, Me.ToolStripSeparator2, Me.ToolStripMenuItem4, Me.ToolStripSeparator1, Me.CloseEditorToolStripMenuItem})
        Me.GameToolStripMenuItem.Name = "GameToolStripMenuItem"
        Me.GameToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.GameToolStripMenuItem.Text = "Game"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(210, 22)
        Me.ToolStripMenuItem5.Text = "New"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(210, 22)
        Me.ToolStripMenuItem1.Text = "Save..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(210, 22)
        Me.ToolStripMenuItem3.Text = "Save As..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(210, 22)
        Me.ToolStripMenuItem2.Text = "Open..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(207, 6)
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(210, 22)
        Me.ToolStripMenuItem4.Text = "Import Reference Image..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(207, 6)
        '
        'CloseEditorToolStripMenuItem
        '
        Me.CloseEditorToolStripMenuItem.Name = "CloseEditorToolStripMenuItem"
        Me.CloseEditorToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.CloseEditorToolStripMenuItem.Text = "Close Editor"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(94, 19)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {32, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDown1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.NumericUpDown1, "Number of frames per row")
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Sheet Rows:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Sheet Columns:"
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(94, 45)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {32, 0, 0, 0})
        Me.NumericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDown2.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.NumericUpDown2, "Number of frames per column")
        Me.NumericUpDown2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown5)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 77)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(164, 158)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sheet Properties"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Preview Frame:"
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Location = New System.Drawing.Point(94, 119)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDown5.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.NumericUpDown5, "Frame used in preview windows.")
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(94, 99)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.CheckBox1, "If unchecked, default PNG save method" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "will use transparency, otherwise will use" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "an 8-bit color palette to determine" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "transparent chroma key.")
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "8-Bit:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Animations:"
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(94, 71)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDown3.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.NumericUpDown3, "Number of animations")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.ListBox1)
        Me.GroupBox2.Controls.Add(Me.ListBox2)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown4)
        Me.GroupBox2.Location = New System.Drawing.Point(207, 77)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(290, 221)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Animations"
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Image = Global.CharasLike.My.Resources.Resources.g_X
        Me.Button2.Location = New System.Drawing.Point(129, 127)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(32, 32)
        Me.Button2.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.Button2, "Removes frame from right.")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Image = Global.CharasLike.My.Resources.Resources.g_up
        Me.Button1.Location = New System.Drawing.Point(129, 89)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(32, 32)
        Me.Button1.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.Button1, "Adds frame on left to list on right.")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(9, 47)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(112, 160)
        Me.ListBox1.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.ListBox1, "List of frames that you can use." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Select frames here, then click" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the green arrow" & _
                " to add them.")
        '
        'ListBox2
        '
        Me.ListBox2.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(169, 21)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(112, 186)
        Me.ListBox2.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.ListBox2, "Frames that this animation will go through." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Duplicate frames can appear here." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "C" & _
                "lick on a frame here, then the red X to" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "remove it.")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveUpToolStripMenuItem, Me.MoveDownToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(139, 48)
        '
        'MoveUpToolStripMenuItem
        '
        Me.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem"
        Me.MoveUpToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.MoveUpToolStripMenuItem.Text = "Move Up"
        '
        'MoveDownToolStripMenuItem
        '
        Me.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem"
        Me.MoveDownToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.MoveDownToolStripMenuItem.Text = "Move Down"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Select Animation:"
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Location = New System.Drawing.Point(98, 19)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDown4.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.NumericUpDown4, "Animation to modify." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Note that 0 means none.")
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(13, 240)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(164, 26)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "Open Preview Window"
        Me.ToolTip1.SetToolTip(Me.Button3, "Opens preview window")
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Enabled = False
        Me.Button5.Location = New System.Drawing.Point(12, 272)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(87, 26)
        Me.Button5.TabIndex = 9
        Me.Button5.Text = "Test Animation"
        Me.ToolTip1.SetToolTip(Me.Button5, "Switch between animation and sheet mode")
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Name:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(54, 42)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(140, 20)
        Me.TextBox1.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.TextBox1, "Name of this game." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For ID purposes in save and" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Set Game menu.")
        '
        'GameEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 308)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "GameEditor"
        Me.Text = "Game Editor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents GameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MoveUpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown5 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
