Public Class ItemsWindow
    ' Loading/Closing
    Private Dockable As Blue.Windows.StickyWindow

    Private Sub ItemsWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FlowLayoutPanel1.Controls.Add(RPGPictureBox.EmptyBox)

        Dockable = New Blue.Windows.StickyWindow(Me)
        'If String.IsNullOrEmpty(My.Settings.DockString) Then Dockable.ForceDock(Canvas)
    End Sub

    Private Sub ItemsWindow_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Canvas.FinalClosing Then Return

        e.Cancel = True
        Me.Hide()
    End Sub

    ' Move/resize code

    Private Sub ItemsWindow_Move(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If Canvas.SkipSizeChanged Then Return
        'Dockable.CheckDocking()
    End Sub

    Private Sub ItemsWindow_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
    End Sub

    ' Box code
    Private Sub TreeView1_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        ' Clear current box
        FlowLayoutPanel1.Controls.Clear()

        Dim Node As GameNodeNode = CType(e.Node, GameNodeNode)

        FlowLayoutPanel1.Controls.Add(RPGPictureBox.EmptyBox)
        If Node.RPGNode.Images.Count Then
            For Each Image In Node.RPGNode.Images
                ' Add a box
                FlowLayoutPanel1.Controls.Add(Image.Box)
            Next
        End If
        TreeView1.SelectedNode = Node
    End Sub
End Class

Public Class RPGPictureBox
    Inherits PictureBox
    Private MainImage_ As RPGImage
    Public Property MainImage() As RPGImage
        Get
            Return MainImage_
        End Get
        Set(ByVal value As RPGImage)
            MainImage_ = value
        End Set
    End Property

    Private MyImage_ As Bitmap
    Public Property MyImage() As Bitmap
        Get
            Return MyImage_
        End Get
        Set(ByVal value As Bitmap)
            MyImage_ = value
        End Set
    End Property

    Shared EmptyBoxVal As PictureBox = RPGPictureBox.Create(Nothing)
    Shared ReadOnly Property EmptyBox() As PictureBox
        Get
            Return EmptyBoxVal
        End Get
    End Property

    Shared Sub PictureBoxClick(ByVal sender As Object, ByVal e As EventArgs)
        If ImagePacker.Visible Then Return

        Dim Box As RPGPictureBox = CType(sender, RPGPictureBox)

        If Box.MainImage IsNot Nothing Then
            If Images.CurrentGameFile IsNot Box.MainImage.GameFile And My.Settings.DontAskGameChange = False Then
                Dim result As MsgBoxResult = DontAskDialog.ShowDialog("You are about to use an image associated with" + vbNewLine + "another game. This may result in problems with" + vbNewLine + "exporting and animation." + vbNewLine + vbNewLine + "Are you sure you want to do this?", SystemIcons.Question.ToBitmap())
                My.Settings.DontAskGameChange = DontAskDialog.CheckBox1.Checked
                If result = MsgBoxResult.Cancel Then Return
            End If
        End If

        ' Are we modifying a layer?
        If LayersWindow.CurrentNode IsNot Nothing Then
            If Box.MainImage IsNot Nothing Then
                LayersWindow.CurrentNode.Layer.SetImage(Box)
            Else
                LayersWindow.CurrentNode.Layer.SetImage(Nothing)
            End If
            Canvas.UpdateDrawing()
        End If
    End Sub

    Shared Sub PictureBoxDragStart(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim Box As RPGPictureBox = sender
        If ImagePacker.ContainsThisBox(ImagePacker.FlowLayoutPanel1.Controls, Box) Then
            ImagePacker.FlowLayoutPanel2.Controls.Add(Box)
            Return
        ElseIf ImagePacker.ContainsThisBox(ImagePacker.FlowLayoutPanel2.Controls, Box) Then
            ImagePacker.FlowLayoutPanel1.Controls.Add(Box)
            Return
        End If
        Return
    End Sub

    Shared Sub PictureBoxQueryThing(ByVal sender As Object, ByVal e As QueryContinueDragEventArgs)
        ' If Form7.Visible = False Then Return
    End Sub

    Public Function CreatePictureBoxCopy() As RPGPictureBox
        Dim Box As New RPGPictureBox

        Box.Size = New Size(48, 48)
        Box.SizeMode = PictureBoxSizeMode.CenterImage
        Box.MainImage = MainImage
        Box.MyImage = MyImage
        Box.Image = Image
        Box.BorderStyle = BorderStyle.Fixed3D
        AddHandler Box.Click, AddressOf PictureBoxClick
        AddHandler Box.MouseDown, AddressOf PictureBoxDragStart
        ImagePacker.ToolTip1.SetToolTip(Box, MainImage.Name)

        Return Box
    End Function

    Shared Function Create(ByVal MainImage As RPGImage) As RPGPictureBox
        Dim Box As New RPGPictureBox

        Box.Size = New Size(48, 48)
        Box.SizeMode = PictureBoxSizeMode.CenterImage
        Box.MainImage = MainImage
        Box.BorderStyle = BorderStyle.Fixed3D
        AddHandler Box.DoubleClick, AddressOf PictureBoxClick
        'AddHandler Box.MouseDown, AddressOf PictureBoxDragStart
        'AddHandler Box.QueryContinueDrag, AddressOf PictureBoxQueryThing

        ' Create the preview image
        If MainImage IsNot Nothing Then
            If MainImage.GameFile IsNot Nothing Then
                Dim FrameSize As Size = New Size(MainImage.RawBitmap.Width \ MainImage.GameFile.SheetRows, MainImage.RawBitmap.Height \ MainImage.GameFile.SheetColumns)
                Box.MyImage = New Bitmap(FrameSize.Width, FrameSize.Height, Imaging.PixelFormat.Format32bppArgb)

                Using MyImageFP As New FastPixel(Box.MyImage, True)
                    Using RawImageFP As New FastPixel(Box.MainImage.RawBitmap, False)
                        Dim Rect As Rectangle = Character.GetRectangleForBitmapFrame(Box.MainImage.RawBitmap, MainImage.GameFile.PreviewFrame, MainImage.GameFile.SheetRows, MainImage.GameFile.SheetColumns)
                        For i As Integer = 0 To FrameSize.Width - 1
                            For z As Integer = 0 To FrameSize.Height - 1
                                MyImageFP.SetPixel(i, z, RawImageFP.GetPixel(Rect.X + i, Rect.Y + z))
                            Next
                        Next
                    End Using
                End Using

                ItemsWindow.ToolTip1.SetToolTip(Box, MainImage.Name + vbNewLine + "Double-click to set current layer image to this.")
                Box.Image = Box.MyImage

                If FrameSize.Width > Box.Size.Width And FrameSize.Height > Box.Size.Height Then Box.SizeMode = PictureBoxSizeMode.StretchImage
            Else
                Box.MyImage = Nothing
                Box.Image = Nothing
            End If
        Else
            Box.MyImage = Nothing
            Box.Image = Nothing
        End If

        Return Box
    End Function
End Class
