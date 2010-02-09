<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Canvas
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

    Private FinalClosing_ As Boolean = False
    Public Property FinalClosing() As Boolean
        Get
            Return FinalClosing_
        End Get
        Set(ByVal value As Boolean)
            FinalClosing_ = value
        End Set
    End Property

    <Security.Permissions.SecurityPermissionAttribute(Security.Permissions.SecurityAction.InheritanceDemand, Flags:=Security.Permissions.SecurityPermissionFlag.UnmanagedCode)> _
    <Security.Permissions.SecurityPermissionAttribute(Security.Permissions.SecurityAction.LinkDemand, Flags:=Security.Permissions.SecurityPermissionFlag.UnmanagedCode)> _
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case Is = &H10
                If AskToClose() Then
                    Return
                End If
                FinalClosing = True
        End Select

        MyBase.WndProc(m)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Canvas))
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton18 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton19 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton20 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton22 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton21 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton23 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SingleCharacterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImagePackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CharacterSetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviewSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntireSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FrameAnimationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetCameraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LayersToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetSheetCreatorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetGameMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton14 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton17 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton16 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.AutoScroll = True
        Me.ContentPanel.BackColor = System.Drawing.SystemColors.Window
        Me.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ContentPanel.Size = New System.Drawing.Size(584, 562)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AllowItemReorder = True
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton9, Me.ToolStripButton13, Me.ToolStripButton18, Me.ToolStripButton19, Me.ToolStripButton20, Me.ToolStripSeparator8, Me.ToolStripButton22, Me.ToolStripButton21, Me.ToolStripButton23, Me.ToolStripSeparator9, Me.ToolStripButton6, Me.ToolStripButton7, Me.ToolStripTextBox1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(289, 25)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 7
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = Global.CharasLike.My.Resources.Resources.NewDocumentHS
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton9.Text = "ToolStripButton1"
        Me.ToolStripButton9.ToolTipText = "New Character"
        '
        'ToolStripButton13
        '
        Me.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton13.Image = Global.CharasLike.My.Resources.Resources.openHS
        Me.ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton13.Name = "ToolStripButton13"
        Me.ToolStripButton13.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton13.Text = "ToolStripButton2"
        Me.ToolStripButton13.ToolTipText = "Open"
        '
        'ToolStripButton18
        '
        Me.ToolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton18.Image = Global.CharasLike.My.Resources.Resources.saveHS
        Me.ToolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton18.Name = "ToolStripButton18"
        Me.ToolStripButton18.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton18.Text = "ToolStripButton3"
        Me.ToolStripButton18.ToolTipText = "Save"
        '
        'ToolStripButton19
        '
        Me.ToolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton19.Image = CType(resources.GetObject("ToolStripButton19.Image"), System.Drawing.Image)
        Me.ToolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton19.Name = "ToolStripButton19"
        Me.ToolStripButton19.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton19.Text = "ToolStripButton4"
        Me.ToolStripButton19.ToolTipText = "Export"
        '
        'ToolStripButton20
        '
        Me.ToolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton20.Image = Global.CharasLike.My.Resources.Resources.PrintHS
        Me.ToolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton20.Name = "ToolStripButton20"
        Me.ToolStripButton20.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton20.Text = "ToolStripButton5"
        Me.ToolStripButton20.ToolTipText = "Print"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton22
        '
        Me.ToolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton22.Image = Global.CharasLike.My.Resources.Resources.preview
        Me.ToolStripButton22.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton22.Name = "ToolStripButton22"
        Me.ToolStripButton22.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton22.Text = "ToolStripButton10"
        Me.ToolStripButton22.ToolTipText = "Preview Sheet"
        '
        'ToolStripButton21
        '
        Me.ToolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton21.Image = Global.CharasLike.My.Resources.Resources.animation
        Me.ToolStripButton21.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton21.Name = "ToolStripButton21"
        Me.ToolStripButton21.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton21.Text = "ToolStripButton12"
        Me.ToolStripButton21.ToolTipText = "View Frame Mode"
        '
        'ToolStripButton23
        '
        Me.ToolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton23.Image = Global.CharasLike.My.Resources.Resources.sheetmode
        Me.ToolStripButton23.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton23.Name = "ToolStripButton23"
        Me.ToolStripButton23.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton23.Text = "ToolStripButton11"
        Me.ToolStripButton23.ToolTipText = "View Sheet Mode"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = Global.CharasLike.My.Resources.Resources.zoom_in
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton6.Text = "ToolStripButton6"
        Me.ToolStripButton6.ToolTipText = "Zoom In"
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = Global.CharasLike.My.Resources.Resources.zoom_out
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton7.Text = "ToolStripButton7"
        Me.ToolStripButton7.ToolTipText = "Zoom Out"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.ToolStripTextBox1.MaxLength = 5
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(40, 25)
        Me.ToolStripTextBox1.Text = "100%"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem, Me.ToolStripMenuItem7, Me.ToolStripMenuItem12, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(289, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem4, Me.ToolStripSeparator2, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem5, Me.ToolStripSeparator4, Me.ToolStripMenuItem6, Me.ToolStripMenuItem8, Me.ToolStripSeparator10, Me.ToolStripMenuItem11, Me.ToolStripMenuItem9, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SingleCharacterToolStripMenuItem, Me.ImagePackToolStripMenuItem, Me.CharacterSetToolStripMenuItem})
        Me.ToolStripMenuItem1.Image = Global.CharasLike.My.Resources.Resources.NewDocumentHS
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem1.Text = "New..."
        '
        'SingleCharacterToolStripMenuItem
        '
        Me.SingleCharacterToolStripMenuItem.Name = "SingleCharacterToolStripMenuItem"
        Me.SingleCharacterToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.SingleCharacterToolStripMenuItem.Text = "Character"
        '
        'ImagePackToolStripMenuItem
        '
        Me.ImagePackToolStripMenuItem.Name = "ImagePackToolStripMenuItem"
        Me.ImagePackToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ImagePackToolStripMenuItem.Text = "Image Pack..."
        '
        'CharacterSetToolStripMenuItem
        '
        Me.CharacterSetToolStripMenuItem.Name = "CharacterSetToolStripMenuItem"
        Me.CharacterSetToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.CharacterSetToolStripMenuItem.Text = "Character Sheet..."
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Image = Global.CharasLike.My.Resources.Resources.openHS
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem4.Text = "Open..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(151, 6)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.CharasLike.My.Resources.Resources.saveHS
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem2.Text = "Save..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem3.Text = "Save As..."
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem5.Text = "Export..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(151, 6)
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Image = Global.CharasLike.My.Resources.Resources.PrintHS
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem6.Text = "Print"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Image = Global.CharasLike.My.Resources.Resources.PrintPreviewHS
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem8.Text = "Print Preview"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(151, 6)
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Image = Global.CharasLike.My.Resources.Resources._112_RefreshArrow_Green_16x16_72
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem11.Text = "Refresh Images"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Image = Global.CharasLike.My.Resources.Resources._305_Close_16x16_72
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(154, 22)
        Me.ToolStripMenuItem9.Text = "Clear Settings"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(151, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PreviewSheetToolStripMenuItem, Me.ToolStripSeparator3, Me.ViewModeToolStripMenuItem, Me.ResetCameraToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'PreviewSheetToolStripMenuItem
        '
        Me.PreviewSheetToolStripMenuItem.Image = Global.CharasLike.My.Resources.Resources.preview
        Me.PreviewSheetToolStripMenuItem.Name = "PreviewSheetToolStripMenuItem"
        Me.PreviewSheetToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.PreviewSheetToolStripMenuItem.Text = "Preview Sheet"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(144, 6)
        '
        'ViewModeToolStripMenuItem
        '
        Me.ViewModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EntireSheetToolStripMenuItem, Me.FrameAnimationToolStripMenuItem})
        Me.ViewModeToolStripMenuItem.Name = "ViewModeToolStripMenuItem"
        Me.ViewModeToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.ViewModeToolStripMenuItem.Text = "View Mode"
        '
        'EntireSheetToolStripMenuItem
        '
        Me.EntireSheetToolStripMenuItem.Checked = True
        Me.EntireSheetToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EntireSheetToolStripMenuItem.Image = Global.CharasLike.My.Resources.Resources.sheetmode
        Me.EntireSheetToolStripMenuItem.Name = "EntireSheetToolStripMenuItem"
        Me.EntireSheetToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.EntireSheetToolStripMenuItem.Text = "Entire Sheet"
        '
        'FrameAnimationToolStripMenuItem
        '
        Me.FrameAnimationToolStripMenuItem.Image = CType(resources.GetObject("FrameAnimationToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FrameAnimationToolStripMenuItem.Name = "FrameAnimationToolStripMenuItem"
        Me.FrameAnimationToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.FrameAnimationToolStripMenuItem.Text = "Frame/Animation"
        '
        'ResetCameraToolStripMenuItem
        '
        Me.ResetCameraToolStripMenuItem.Image = Global.CharasLike.My.Resources.Resources.smCamera
        Me.ResetCameraToolStripMenuItem.Name = "ResetCameraToolStripMenuItem"
        Me.ResetCameraToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.ResetCameraToolStripMenuItem.Text = "Reset Camera"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LayersToolStripMenuItem2, Me.ItemsToolStripMenuItem})
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(68, 20)
        Me.ToolStripMenuItem7.Text = "Windows"
        '
        'LayersToolStripMenuItem2
        '
        Me.LayersToolStripMenuItem2.Name = "LayersToolStripMenuItem2"
        Me.LayersToolStripMenuItem2.Size = New System.Drawing.Size(107, 22)
        Me.LayersToolStripMenuItem2.Text = "Layers"
        '
        'ItemsToolStripMenuItem
        '
        Me.ItemsToolStripMenuItem.Name = "ItemsToolStripMenuItem"
        Me.ItemsToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.ItemsToolStripMenuItem.Text = "Items"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetSheetCreatorToolStripMenuItem, Me.ToolStripSeparator5, Me.ToolStripMenuItem10, Me.SetGameMenuItem})
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(48, 20)
        Me.ToolStripMenuItem12.Text = "Tools"
        '
        'SetSheetCreatorToolStripMenuItem
        '
        Me.SetSheetCreatorToolStripMenuItem.Name = "SetSheetCreatorToolStripMenuItem"
        Me.SetSheetCreatorToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.SetSheetCreatorToolStripMenuItem.Text = "Set Sheet Creator"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(161, 6)
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(164, 22)
        Me.ToolStripMenuItem10.Text = "Game Editor"
        '
        'SetGameMenuItem
        '
        Me.SetGameMenuItem.Name = "SetGameMenuItem"
        Me.SetGameMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.SetGameMenuItem.Text = "Set Game"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ContentsToolStripMenuItem.Text = "Contents"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.PictureBox1)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(289, 243)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(289, 317)
        Me.ToolStripContainer1.TabIndex = 8
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.MenuStrip1)
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip2)
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(289, 243)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton8, Me.ToolStripButton15, Me.ToolStripButton14, Me.ToolStripSeparator6, Me.ToolStripButton17, Me.ToolStripButton16, Me.ToolStripTextBox2, Me.ToolStripSeparator11, Me.ToolStripButton2, Me.ToolStripButton1, Me.ToolStripTextBox3})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 49)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(289, 25)
        Me.ToolStrip2.Stretch = True
        Me.ToolStrip2.TabIndex = 8
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = Global.CharasLike.My.Resources.Resources.playanim
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton8.Text = "ToolStripButton8"
        Me.ToolStripButton8.ToolTipText = "Animate/Pause"
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton15.Image = Global.CharasLike.My.Resources.Resources.prev_frame
        Me.ToolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton15.Name = "ToolStripButton15"
        Me.ToolStripButton15.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton15.Text = "ToolStripButton15"
        Me.ToolStripButton15.ToolTipText = "Step Prev Frame"
        '
        'ToolStripButton14
        '
        Me.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton14.Image = Global.CharasLike.My.Resources.Resources.next_frame
        Me.ToolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton14.Name = "ToolStripButton14"
        Me.ToolStripButton14.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton14.Text = "ToolStripButton14"
        Me.ToolStripButton14.ToolTipText = "Step Next Frame"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton17
        '
        Me.ToolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton17.Image = Global.CharasLike.My.Resources.Resources.inc_speed
        Me.ToolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton17.Name = "ToolStripButton17"
        Me.ToolStripButton17.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton17.Text = "ToolStripButton17"
        Me.ToolStripButton17.ToolTipText = "Decrease Animation Timer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Faster)"
        '
        'ToolStripButton16
        '
        Me.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton16.Image = Global.CharasLike.My.Resources.Resources.dec_speed
        Me.ToolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton16.Name = "ToolStripButton16"
        Me.ToolStripButton16.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton16.Text = "ToolStripButton16"
        Me.ToolStripButton16.ToolTipText = "Increase Animation Timer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Slower)"
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolStripTextBox2.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.ToolStripTextBox2.MaxLength = 6
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(60, 25)
        Me.ToolStripTextBox2.ToolTipText = "Animation Speed (in ms)." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Lower values are faster."
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.CharasLike.My.Resources.Resources.prevanim
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.ToolTipText = "Previous Animation"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.CharasLike.My.Resources.Resources.nextanim
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Next Animation"
        '
        'ToolStripTextBox3
        '
        Me.ToolStripTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolStripTextBox3.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.ToolStripTextBox3.MaxLength = 2
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        Me.ToolStripTextBox3.Size = New System.Drawing.Size(30, 25)
        Me.ToolStripTextBox3.ToolTipText = "Animation Number"
        '
        'Canvas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(289, 317)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(305, 355)
        Me.Name = "Canvas"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "OpenCharas"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton9 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton13 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton18 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton19 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton20 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton21 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton22 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton23 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SetGameMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreviewSheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ViewModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntireSheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FrameAnimationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResetCameraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LayersToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetSheetCreatorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton15 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton14 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton16 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton17 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripTextBox2 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripTextBox3 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents SingleCharacterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImagePackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CharacterSetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
