<Assembly: CLSCompliant(True)> 
Public Class Canvas
    Public Sub New()
        InitializeComponent()
        WindowGeometry.FromString(My.Settings.WindowGeometry_Canvas, Me)
    End Sub

    Private SkipSizeChanged_ As Boolean
    Public Property SkipSizeChanged() As Boolean
        Get
            Return SkipSizeChanged_
        End Get
        Set(ByVal value As Boolean)
            SkipSizeChanged_ = value
        End Set
    End Property

    Public Sub SetCurrentSavePosition(ByVal file As String)
        SavePos = file
        Me.Text = Application.ProductName

        If String.IsNullOrEmpty(file) = False Then Me.Text += " - " + SavePos.Substring(SavePos.LastIndexOf("\") + 1)
    End Sub

    ' Loading code
    Private Sub Canvas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' TEMP
        Character.RPGCharacterFiles.Add(New RPGCharacter)

        Images.LoadImages()
        AnimTimer = New Timer
        AnimTimer.Interval = 350
        AddHandler AnimTimer.Tick, AddressOf AnimTimerUp
        ToolStripTextBox2.Text = AnimTimer.Interval

        UpdateDrawing()

        UpdateAnimNum(0)

        SkipSizeChanged = True
        CharacterSelect.Show(Me)
        LayersWindow.Show(Me)
        ItemsWindow.Show(Me)
        SkipSizeChanged = False

        If String.IsNullOrEmpty(My.Settings.WindowGeometry_Canvas) Then
            Me.Location = New Point((Screen.PrimaryScreen.WorkingArea.X + (Screen.PrimaryScreen.WorkingArea.Width / 2)) - (Me.Size.Width / 2) - (LayersWindow.Size.Width / 2), (Screen.PrimaryScreen.WorkingArea.Y + (Screen.PrimaryScreen.WorkingArea.Height / 2)) - (Me.Size.Height / 2) + (LayersWindow.Size.Height / 3.5))
        End If

        If String.IsNullOrEmpty(My.Settings.WindowGeometry_Items) Then
            ItemsWindow.Location = New Point(Me.Location.X + Me.Size.Width, Me.Location.Y + Me.Size.Height - ItemsWindow.Size.Height)
        Else
            WindowGeometry.FromString(My.Settings.WindowGeometry_Items, ItemsWindow)
        End If

        If String.IsNullOrEmpty(My.Settings.WindowGeometry_Layers) Then
            LayersWindow.Location = New Point(Me.Location.X + Me.Size.Width, Me.Location.Y - LayersWindow.Size.Height + (Me.Size.Height - LayersWindow.Size.Height) + (LayersWindow.Size.Height - ItemsWindow.Size.Height))
        Else
            WindowGeometry.FromString(My.Settings.WindowGeometry_Layers, LayersWindow)
        End If

        If String.IsNullOrEmpty(My.Settings.WindowGeometry_CharSelect) Then
            CharacterSelect.Location = New Point(Me.Location.X - CharacterSelect.Size.Width, Me.Location.Y + Me.Size.Height - CharacterSelect.Size.Height)
        Else
            WindowGeometry.FromString(My.Settings.WindowGeometry_CharSelect, CharacterSelect)
        End If

        If My.Settings.FirstRun Then
            My.Settings.FirstRun = False
            My.Settings.Save()
        End If

        ToolStripManager.LoadSettings(Me)
        ToolStripManager.LoadSettings(LayersWindow)

        ' Do we have any games?
        While Images.RPGGames.Count = 0
            MsgBox("Charas.EXtended notices that you don't have any games set up." + vbNewLine + vbNewLine + "To use this application, you must create at least one game.", MsgBoxStyle.OkOnly)

            GameEditor.ShowDialog()
            ' Re-load images and games
            Images.LoadImages()
        End While
    End Sub

    Public Function AskToClose() As Boolean
        Dim Result = MsgBox("You may have unsaved work. Would you like to save your changes before exiting?", MsgBoxStyle.YesNoCancel)
        If Result = MsgBoxResult.Yes Then
            DoSaveAs()
        ElseIf Result = MsgBoxResult.Cancel Then
            Return True
        End If
        Return False
    End Function

    ' Closing Code
    Private Sub Canvas_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.WindowGeometry_Canvas = WindowGeometry.ToString(Me)
        My.Settings.WindowGeometry_Items = WindowGeometry.ToString(ItemsWindow)
        My.Settings.WindowGeometry_Layers = WindowGeometry.ToString(LayersWindow)
    End Sub

    Private Sub Canvas_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        ToolStripManager.SaveSettings(Me)
        ToolStripManager.SaveSettings(LayersWindow)

        My.Settings.Save()
    End Sub

    Private Dockable As New WindowDocking(Me)
    Private Sub Canvas_Move(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If Me.WindowState <> FormWindowState.Normal Then Return

        Dockable.CheckDocking()
    End Sub

    Dim WasMinimized As Boolean = False
    Private Sub Canvas_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        If WasMinimized Then
            Me.Focus()
            WasMinimized = False
        End If
    End Sub

    Private Sub Canvas_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then Return
        UpdateDrawing()
    End Sub

    ' Open Layers
    Private Sub LayersToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LayersToolStripMenuItem2.Click
        If LayersWindow.Visible Then Return

        LayersWindow.Show(Me)
    End Sub

    ' Open Items
    Private Sub ItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsToolStripMenuItem.Click
        If ItemsWindow.Visible Then Return

        ItemsWindow.Show(Me)
    End Sub

    ' Drawing Code
    Dim StoredImage As Bitmap

    Public Sub UpdateDrawing()
        If ToolStripContainer1.ContentPanel.Width And ToolStripContainer1.ContentPanel.Height Then
            StoredImage = New Bitmap(ToolStripContainer1.ContentPanel.Width, ToolStripContainer1.ContentPanel.Height, Imaging.PixelFormat.Format32bppArgb)
            Dim TempGfx As Graphics = Graphics.FromImage(StoredImage)

            Dim ContentWidth As Integer = CType(ToolStripContainer1.ContentPanel.Width, Integer)
            Dim ContentHeight As Integer = CType(ToolStripContainer1.ContentPanel.Height, Integer)
            TempGfx.DrawLine(Pens.Black, New PointF(ContentWidth / 2, 0), New PointF(ContentWidth / 2, ContentHeight))
            TempGfx.DrawLine(Pens.Black, New PointF(0, ContentHeight / 2), New PointF(ContentWidth, ContentHeight / 2))

            Dim Cont As Drawing2D.GraphicsContainer = TempGfx.BeginContainer()

            TempGfx.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

            ' Go through each layer drawing the sheet
            If Character.RPGCharacterFiles.Count > 0 Then
                For Each Layer In Character.RPGCharacterFiles(0).Layers
                    Layer.DrawCanvasLayers(TempGfx)
                Next
            End If

            TempGfx.EndContainer(Cont)
            TempGfx.Dispose()
        End If

        PictureBox1.Image = StoredImage
    End Sub

    ' Switch to Frame Mode
    Public Sub SwitchToFrameMode()
        FrameAnimationToolStripMenuItem.Checked = True
        EntireSheetToolStripMenuItem.Checked = False
        UpdateDrawing()
    End Sub

    Private Sub FrameAnimationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrameAnimationToolStripMenuItem.Click
        SwitchToFrameMode()
    End Sub

    Private Sub ToolStripButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton21.Click
        SwitchToFrameMode()
    End Sub

    ' Switch to Sheet Mode
    Public Sub SwitchToSheetMode()
        FrameAnimationToolStripMenuItem.Checked = False
        EntireSheetToolStripMenuItem.Checked = True
        UpdateDrawing()
    End Sub

    Private Sub EntireSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntireSheetToolStripMenuItem.Click
        SwitchToSheetMode()
    End Sub

    Private Sub ToolStripButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton23.Click
        SwitchToSheetMode()
    End Sub

    ' Zooming code
    Private ZoomNumber_ As Integer = 100
    Public Property ZoomNumber() As Integer
        Get
            Return ZoomNumber_
        End Get
        Set(ByVal value As Integer)
            ZoomNumber_ = value
        End Set
    End Property

    Private Sub SetZoom(ByVal Num As Integer)
        If Num < 0 Then Num = 0
        If Num > 1000 Then Num = 1000

        ToolStripTextBox1.Text = Num.ToString() + "%"
        ZoomNumber = Num
    End Sub

    Public Sub ZoomByNumber(ByVal Addition As Integer)
        SetZoom(ZoomNumber + Addition)
        UpdateDrawing()
    End Sub

    Const ZoomInAmount = 25

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        ZoomByNumber(ZoomInAmount)
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        ZoomByNumber(-ZoomInAmount)
    End Sub

    Private Sub Canvas_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseWheel
        If e.Delta > 0 Then
            ZoomByNumber(ZoomInAmount)
        ElseIf e.Delta < 0 Then
            ZoomByNumber(-ZoomInAmount)
        End If
    End Sub

    Private Sub ToolStripTextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ToolStripTextBox1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Or e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            e.Handled = True
            Dim EndResult As Boolean
            Dim EndValue As Integer
            EndResult = Integer.TryParse(ToolStripTextBox1.Text, EndValue)

            If EndResult = False Then Return

            SetZoom(EndValue)
        End If
    End Sub

    ' Animation player
    Dim AnimTimer As Timer
    Private AnimationFrame As Integer = 0
    Public Property AnimationFrameIndex() As Integer
        Get
            Return AnimationFrame
        End Get
        Set(ByVal value As Integer)
            AnimationFrame = value
        End Set
    End Property

    Private Sub AnimTimerUp(ByVal sender As Object, ByVal e As EventArgs)
        If AnimationIndexVal = 0 Then Return

        AnimationFrame = AnimationFrame + 1
        If AnimationFrame > Images.CurrentGameFile.Animations(AnimationIndexVal - 1).Frames.Count - 1 Then AnimationFrame = 0
        UpdateDrawing()
    End Sub

    Private PlayingAnimation As Boolean = False
    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click
        PlayingAnimation = (PlayingAnimation = False)

        If PlayingAnimation Then
            ToolStripButton8.Image = My.Resources.stop_anim
            AnimTimer.Start()
        Else
            ToolStripButton8.Image = My.Resources.playanim
            AnimTimer.Stop()
        End If

        UpdateDrawing()
    End Sub

    Private Sub ToolStripButton14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton14.Click
        AnimationFrame = AnimationFrame + 1
        If AnimationFrame > 3 Then AnimationFrame = 0
        UpdateDrawing()
    End Sub

    Private Sub ToolStripButton15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton15.Click
        AnimationFrame = AnimationFrame - 1
        If AnimationFrame < 0 Then AnimationFrame = 3
        UpdateDrawing()
    End Sub

    ' Animation speed setting
    Public Sub SetSpeed(ByVal Speed As Integer)
        If Speed < 1 Then Speed = 1
        AnimTimer.Interval = Speed
        ToolStripTextBox2.Text = Speed.ToString()
    End Sub

    Private Sub ToolStripTextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ToolStripTextBox2.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Or e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            e.Handled = True
            Dim EndResult As Boolean
            Dim EndValue As Integer
            EndResult = Integer.TryParse(ToolStripTextBox2.Text, EndValue)

            If EndResult = False Then Return

            SetSpeed(EndValue)
        End If

    End Sub

    Private Sub ToolStripButton16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton16.Click
        SetSpeed(AnimTimer.Interval + 25)
    End Sub

    Private Sub ToolStripButton17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton17.Click
        SetSpeed(AnimTimer.Interval - 25)
    End Sub

    ' Preview sheet
    Private Sub DoPreviewSheet()
        If Character.RPGCharacterFiles(0).Layers.Count = 0 Then
            MsgBox("You need some layers first!")
            Return
        End If
        If PreviewWindow.Visible Then
            PreviewWindow.Close()
            Return
        End If
        PreviewWindow.StartPosition = FormStartPosition.Manual
        Dim ImgWidth = Character.RPGCharacterFiles(0).SizeOfOutput
        PreviewWindow.Size = New Size(ImgWidth.Width + GameEditor.Diff.Width, ImgWidth.Height + GameEditor.Diff.Height)
        PreviewWindow.Location = Me.Location + New Size(Me.Size.Width - PreviewWindow.Size.Width, -PreviewWindow.Size.Height)
        PreviewWindow.Show()
    End Sub

    Private Sub ToolStripButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton22.Click
        DoPreviewSheet()
    End Sub

    Private Sub PreviewSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewSheetToolStripMenuItem.Click
        DoPreviewSheet()
    End Sub

    ' Rendering: for getting the finalized image for saving
    Public Shared SpecialColor As Color = Color.FromArgb(32, 156, 0)

    Public Function RenderToTexture(ByVal ResetTrans) As Bitmap
        Dim ImgSize As Size = Character.RPGCharacterFiles(0).SizeOfOutput

        Dim RenderedImage = New Bitmap(ImgSize.Width, ImgSize.Height, Imaging.PixelFormat.Format32bppArgb)
        Using TempGfx As Graphics = Graphics.FromImage(RenderedImage)
            TempGfx.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

            ' Go through each layer drawing the sheet
            For Each Layer In Character.RPGCharacterFiles(0).Layers
                Layer.RenderLayerToGraphicsRecursion(TempGfx)
            Next
        End Using

        If ResetTrans = False Then
            Dim ColorList = NumColorsInImage(RenderedImage)
            Dim EndedUpColor As Color = SpecialColor

            Static RandomNumGen As New System.Random

            While ColorList.IndexOf(EndedUpColor) <> -1
                EndedUpColor = Color.FromArgb(RandomNumGen.Next(256), RandomNumGen.Next(256), RandomNumGen.Next(256))
            End While

            Using fp As New FastPixel(RenderedImage, True)
                For i As Integer = 0 To RenderedImage.Width - 1
                    For z As Integer = 0 To RenderedImage.Height - 1
                        If fp.GetPixel(i, z).A = 0 Then
                            fp.SetPixel(i, z, EndedUpColor)
                        End If
                    Next
                Next
            End Using
        End If

        Return RenderedImage
    End Function

    Public Function RenderToTexture()
        Return RenderToTexture(False)
    End Function

    ' Saving/Loading
    ' Bitmap utility
    Shared Sub WriteBitmapToFile(ByVal File As IO.BinaryWriter, ByVal Bmp As Bitmap)
        If File Is Nothing Or Bmp Is Nothing Then Return
        Using fp As New FastPixel(Bmp, False)
            For x As Integer = 0 To Bmp.Width - 1
                For y As Integer = 0 To Bmp.Height - 1
                    Dim Col As Color = fp.GetPixel(x, y)
                    ' Write it
                    File.Write(Col.A)
                    File.Write(Col.R)
                    File.Write(Col.G)
                    File.Write(Col.B)
                Next
            Next
        End Using
    End Sub

    Shared Sub ReadBitmapFromFile(ByVal File As IO.BinaryReader, ByVal Width As Integer, ByVal Height As Integer, ByVal Bmp As Bitmap)
        If File Is Nothing Then Return

        Dim fp As FastPixel = Nothing

        If Bmp IsNot Nothing Then
            fp = New FastPixel(Bmp, True)
        End If

        For x As Integer = 0 To Width - 1
            For y As Integer = 0 To Height - 1
                Dim clr As Color = Color.FromArgb(File.ReadByte(), File.ReadByte(), File.ReadByte(), File.ReadByte())
                ' Set this pixel
                If Bmp IsNot Nothing Then
                    fp.SetPixel(x, y, clr)
                End If
            Next
        Next

        If Bmp IsNot Nothing Then fp.Dispose()
    End Sub

    Dim SavePos As String = ""

    Shared Sub WriteHeader(ByVal Writer As IO.BinaryWriter, ByVal Header As EHeaderValue)
        If Writer Is Nothing Then Return

        If Header = EHeaderValue.HeaderCharacterNoImages Then
            Writer.Write("chz0")
        ElseIf Header = EHeaderValue.HeaderCharacterImages Then
            Writer.Write("chz1")
        ElseIf Header = EHeaderValue.HeaderImagePack Then
            Writer.Write("chz2")
        End If
    End Sub

    Shared Sub PerformSave(ByVal Path As String, ByVal Header As EHeaderValue)
        Using MemStrm As New IO.MemoryStream
            Using Writer As New IO.BinaryWriter(MemStrm)

                If Writer IsNot Nothing Then
                    WriteHeader(Writer, Header)

                    ' Write the game file (this is to ensure that receiving ends have the game)
                    Images.CurrentGameFile.Save(Writer)
                    ' Write the path
                    Writer.Write(Images.CurrentGameFile.FilePath)

                    ' Write every layer
                    Character.RPGCharacterFiles(0).Save(Writer, (Header = EHeaderValue.HeaderCharacterImages))
                End If

                Using outFile As IO.FileStream = IO.File.Create(Path)
                    Using Compress As IO.Compression.GZipStream = _
                        New IO.Compression.GZipStream(outFile, IO.Compression.CompressionMode.Compress)

                        ' Copy the source file into the compression stream.
                        MemStrm.Seek(0, IO.SeekOrigin.Begin)
                        Compress.Write(MemStrm.ToArray(), 0, MemStrm.Length())
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Public Shared Function Percent(ByVal CurrVal As Long, ByVal MaxVal As Long) As Integer
        Return Int(CurrVal / MaxVal * 100 + 0.5)
    End Function

    Shared Sub PerformImagePackCreate(ByVal Path As String)
        ImagePacker.TreeView1.Nodes.Clear()
        ImagePacker.FlowLayoutPanel1.Controls.Clear()
        ImagePacker.FlowLayoutPanel2.Controls.Clear()
        For Each Node In Images.RPGNodes
            Images.AddAndRecurseNodes(ImagePacker.TreeView1.Nodes, Node)
        Next

        Dim Result = ImagePacker.ShowDialog()
        If Result = Windows.Forms.DialogResult.OK And ImagePacker.FlowLayoutPanel2.Controls.Count <> 0 Then
            ' Regular compression
            Using MemStrm As New IO.MemoryStream
                Using File As New IO.BinaryWriter(MemStrm)
                    Dim GameFilesInUse As New List(Of RPGGameFile)

                    WriteHeader(File, EHeaderValue.HeaderImagePack)

                    File.Write(ImagePacker.FlowLayoutPanel2.Controls.Count)
                    For Each Control In ImagePacker.FlowLayoutPanel2.Controls
                        Dim Box As RPGPictureBox = Control
                        Dim Image As RPGImage = Box.MainImage

                        File.Write(Image IsNot Nothing)
                        If Image IsNot Nothing Then
                            File.Write(Image.Path)

                            File.Write(Image.OriginalBitmap.Width)
                            File.Write(Image.OriginalBitmap.Height)

                            WriteBitmapToFile(File, Image.OriginalBitmap)

                            If GameFilesInUse.Contains(Image.GameFile) = False Then GameFilesInUse.Add(Image.GameFile)
                        End If
                    Next

                    File.Write(GameFilesInUse.Count)
                    For Each Game In GameFilesInUse
                        Game.Save(File)
                        File.Write(Game.FilePath)
                    Next

                    ' Create the compressed file.
                    Using outFile As IO.FileStream = IO.File.Create(Path)
                        Using Compress As IO.Compression.GZipStream = _
                            New IO.Compression.GZipStream(outFile, IO.Compression.CompressionMode.Compress)

                            ' Copy the source file into the compression stream.
                            MemStrm.Seek(0, IO.SeekOrigin.Begin)
                            Compress.Write(MemStrm.ToArray(), 0, MemStrm.Length())
                        End Using
                    End Using

                End Using
            End Using
        End If
    End Sub

    Public Sub DoSaveAs()
        Using SaveDlg As New SaveFileDialog
            SaveDlg.AddExtension = True
            SaveDlg.Filter = "CHZ Files (.chz)|*.chz|CHX Files (.chx)|*.chx|Image Pack (.chx)|*.chx|All Files|*"
            SaveDlg.DefaultExt = "chz"
            SaveDlg.RestoreDirectory = True
            Dim Result = SaveDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                If SavePos.EndsWith(".chz") Then
                    SetCurrentSavePosition(SaveDlg.FileName)
                    PerformSave(SavePos, EHeaderValue.HeaderCharacterNoImages)
                ElseIf SavePos.EndsWith(".chx") Then
                    If SaveDlg.FilterIndex = 3 Then
                        PerformImagePackCreate(SaveDlg.FileName)
                    Else
                        SetCurrentSavePosition(SaveDlg.FileName)
                        PerformSave(SavePos, EHeaderValue.HeaderCharacterImages)
                    End If
                End If
            End If
        End Using
    End Sub

    Public Sub DoSave()
        If SavePos = "" Then
            DoSaveAs()
        Else
            PerformSave(SavePos, EHeaderValue.HeaderCharacterNoImages)
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        DoSave()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        DoSaveAs()
    End Sub

    Private Sub ToolStripButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton18.Click
        DoSave()
    End Sub

    ' Loading portion
    Shared Function ReadHeader(ByVal Reader As IO.BinaryReader) As EHeaderValue
        If Reader Is Nothing Then Return "???"

        Dim Header As String = Reader.ReadString()

        If Header = "chz0" Then
            Return EHeaderValue.HeaderCharacterNoImages
        ElseIf Header = "chz1" Then
            Return EHeaderValue.HeaderCharacterImages
        ElseIf Header = "chz2" Then
            Return EHeaderValue.HeaderImagePack
        End If
        Return EHeaderValue.HeaderUnknown
    End Function

    Private ImagesNotFound As New Collections.ObjectModel.Collection(Of String)
    Public ReadOnly Property PackImagesNotFound() As Collections.ObjectModel.Collection(Of String)
        Get
            Return ImagesNotFound
        End Get
    End Property

    Private ImagesAcquired_ As New Collections.ObjectModel.Collection(Of String)
    Public ReadOnly Property ImagesAcquired() As Collections.ObjectModel.Collection(Of String)
        Get
            Return ImagesAcquired_
        End Get
    End Property

    Public Sub PerformOpen(ByVal Path As String)
        Using DecompressedFile As New IO.MemoryStream
            Using InFile = IO.File.Open(Path, IO.FileMode.Open)
                Using FileToBeDcmp As New IO.Compression.GZipStream(InFile, IO.Compression.CompressionMode.Decompress)
                    Dim buffer As Byte() = New Byte(4096) {}
                    Dim numRead As Integer
                    numRead = FileToBeDcmp.Read(buffer, 0, buffer.Length)
                    Do While numRead <> 0
                        DecompressedFile.Write(buffer, 0, numRead)
                        numRead = FileToBeDcmp.Read(buffer, 0, buffer.Length)
                    Loop
                End Using

                DecompressedFile.Position = 0
                Dim Reader As New IO.BinaryReader(DecompressedFile)

                If Reader IsNot Nothing Then
                    Dim Header As EHeaderValue = ReadHeader(Reader)
                    If Header = EHeaderValue.HeaderUnknown Then
                        MsgBox("Error opening " + Path + ": invalid header.", MsgBoxStyle.OkOnly)
                        Reader.Close()
                        Return
                    ElseIf Header = EHeaderValue.HeaderCharacterNoImages Or Header = EHeaderValue.HeaderCharacterImages Then
                        ' Read the game
                        Dim CheckGameFile As New RPGGameFile
                        CheckGameFile.Load(Reader)
                        CheckGameFile.FilePath = Reader.ReadString()

                        Dim FoundGameFile As RPGGameFile = Images.FindGame(CheckGameFile)

                        If FoundGameFile IsNot Nothing Then
                            Images.CurrentGameFile.MenuItem.Checked = False
                            Images.CurrentGameFile = FoundGameFile
                            FoundGameFile.MenuItem.Checked = True
                        Else
                            IO.Directory.CreateDirectory(CheckGameFile.FilePath.Substring(0, CheckGameFile.FilePath.LastIndexOf("\")))
                            Using File As New IO.BinaryWriter(IO.File.Open(CheckGameFile.FilePath, IO.FileMode.Create))
                                CheckGameFile.Save(File)
                            End Using
                            Images.RPGGames.Add(CheckGameFile)
                            Images.CurrentGameFile = CheckGameFile

                            Dim MenuItem As GameModeMenuItem = New GameModeMenuItem
                            MenuItem.GameFile = CheckGameFile
                            CheckGameFile.MenuItem = MenuItem
                            SetGameMenuItem.DropDownItems.Add(MenuItem)
                        End If

                        Character.RPGCharacterFiles(0).Load(Reader, (Header = EHeaderValue.HeaderCharacterImages))
                        If Header = EHeaderValue.HeaderCharacterImages Then Images.ReloadImages()

                        If ImagesNotFound.Count Then
                            Dim Prompt As String = "The character file loaded with the following errors:" + vbNewLine + vbNewLine
                            For Each MissingImg In ImagesNotFound
                                Prompt += "The image " + MissingImg + " was not found." + vbNewLine
                            Next

                            MsgBox(Prompt)
                            ImagesNotFound.Clear()
                        End If
                        If ImagesAcquired.Count Then
                            Dim ResultStr As String = (ImagesAcquired.Count.ToString() + " images were added to your list:" + vbNewLine + vbNewLine)
                            For Each Stri In ImagesAcquired
                                ResultStr += Stri + vbNewLine
                            Next
                            MsgBox(ResultStr)
                            ImagesAcquired.Clear()
                        End If
                        SetCurrentSavePosition(Path)
                    ElseIf Header = EHeaderValue.HeaderImagePack Then
                        Dim Count As Integer = Reader.ReadInt32()
                        Dim StrList As New List(Of String)

                        For i As Integer = 0 To Count - 1
                            Dim DoRead = Reader.ReadBoolean()

                            If DoRead Then
                                Dim Img As String = Reader.ReadString()
                                Dim Image = Images.FindImage(Img)
                                Dim Width As Integer = Reader.ReadInt32(), Height As Integer = Reader.ReadInt32()
                                Using BMP As New Bitmap(Width, Height, Imaging.PixelFormat.Format32bppArgb)
                                    ReadBitmapFromFile(Reader, BMP.Width, BMP.Height, BMP)

                                    If IO.File.Exists(Img) Then
                                        If Images.BitmapCompare(BMP, Images.FindImage(Img).OriginalBitmap) = False Then
                                            ' Message the user
                                            If MsgBox("The image " + Img + " already exists in your image folder, but yours differs from the one in the pack. Would you like to overwrite with this one?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                                BMP.Save(Img)
                                                StrList.Add(Img)
                                            End If
                                        End If
                                    ElseIf IO.File.Exists(Img) = False Then
                                        IO.Directory.CreateDirectory(Img.Substring(0, Img.LastIndexOf("\")))

                                        ' Save the bitmap
                                        BMP.Save(Img)

                                        ' Set our image to a temporary image for later re-loading
                                        Image = New RPGImage
                                        Image.Path = Img
                                        StrList.Add(Img)
                                    End If
                                End Using
                            End If
                        Next

                        Count = Reader.ReadInt32()
                        For i As Integer = 0 To Count - 1
                            Dim CheckGameFile As New RPGGameFile

                            CheckGameFile.Load(Reader)
                            CheckGameFile.FilePath = Reader.ReadString()

                            Dim FoundGameFile As RPGGameFile = Images.FindGame(CheckGameFile)

                            If FoundGameFile IsNot Nothing Then
                                Images.CurrentGameFile.MenuItem.Checked = False
                                Images.CurrentGameFile = FoundGameFile
                                FoundGameFile.MenuItem.Checked = True
                            Else
                                IO.Directory.CreateDirectory(CheckGameFile.FilePath.Substring(0, CheckGameFile.FilePath.LastIndexOf("\")))
                                Using File As New IO.BinaryWriter(IO.File.Open(CheckGameFile.FilePath, IO.FileMode.Create))
                                    CheckGameFile.Save(File)
                                End Using
                                Images.RPGGames.Add(CheckGameFile)
                                Images.CurrentGameFile = CheckGameFile

                                Dim MenuItem As GameModeMenuItem = New GameModeMenuItem
                                MenuItem.GameFile = CheckGameFile
                                CheckGameFile.MenuItem = MenuItem
                                SetGameMenuItem.DropDownItems.Add(MenuItem)
                            End If
                        Next

                        Dim ResultStr As String = (StrList.Count.ToString() + " images were added to your list:" + vbNewLine + vbNewLine)
                        For Each Stri In StrList
                            ResultStr += Stri + vbNewLine
                        Next
                        MsgBox(ResultStr)
                    End If

                    Reader.Close()
                End If
            End Using
        End Using

        Images.ReloadImages()
        UpdateDrawing()
    End Sub

    Public Sub HitOpen()
        If MsgBox("Opening a new character file will erase this unsaved character. Do you wish to save first?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DoSaveAs()
            SetCurrentSavePosition("")
        End If

        ResetEverything()

        Using OpenDlg As New OpenFileDialog
            OpenDlg.AddExtension = True
            OpenDlg.Filter = "Character Files (.chz;.chx)|*.chz;*.chx|All Files|*"
            OpenDlg.DefaultExt = "chz"
            OpenDlg.RestoreDirectory = True
            Dim Result = OpenDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                PerformOpen(OpenDlg.FileName)
            End If
        End Using
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        HitOpen()
    End Sub

    Private Sub ToolStripButton13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton13.Click
        HitOpen()
    End Sub

    ' Export sprite to image
    Shared Function NumColorsInImage(ByVal img As Bitmap) As Collections.ObjectModel.Collection(Of Color)
        If img Is Nothing Then Return Nothing

        Dim ColorsInImage As New Collections.ObjectModel.Collection(Of Color)

        Using fp As New FastPixel(img, False)
            For i As Integer = 0 To img.Width - 1
                For z As Integer = 0 To img.Height - 1
                    If ColorsInImage.Contains(fp.GetPixel(i, z)) Then
                        Continue For
                    Else
                        ColorsInImage.Add(fp.GetPixel(i, z))
                    End If
                Next
            Next
        End Using

        Return ColorsInImage
    End Function

    Public Sub ExportSprite()
        Using SaveDlg As New SaveFileDialog
            SaveDlg.AddExtension = True
            SaveDlg.Filter = "8-Bit PNG Files (RPG2000/3) (.png)|*.png|32-Bit PNG Files (.png)|*.png|JPG Files (.jpg)|*.jpg|BMP Files (.bmp)|*.bmp|All Files|*"
            SaveDlg.DefaultExt = "png"
            SaveDlg.RestoreDirectory = True
            If Images.CurrentGameFile.Is8Bit Then
                SaveDlg.FilterIndex = 1
            Else
                SaveDlg.FilterIndex = 2
            End If
            Dim Result = SaveDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                Dim Image As Bitmap = RenderToTexture(SaveDlg.FilterIndex = 2)

                If SaveDlg.FilterIndex = 1 Then
                    Dim Colors = NumColorsInImage(Image)

                    If Colors.Count <= 256 Then
                        Using Indexed As New Bitmap(Image.Width, Image.Height, Imaging.PixelFormat.Format8bppIndexed)
                            ' Create a palette
                            Dim Pal As Imaging.ColorPalette = Indexed.Palette
                            For i As Integer = 0 To 255
                                If i > Colors.Count - 1 Then
                                    Pal.Entries(i) = Color.White
                                Else
                                    Pal.Entries(i) = Colors(i)
                                End If
                            Next
                            Indexed.Palette = Pal

                            Dim bmd As Imaging.BitmapData = Indexed.LockBits(New Rectangle(0, 0, Image.Width, Image.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, Indexed.PixelFormat)

                            Using fp As New FastPixel(Image, False)
                                For y = 0 To bmd.Height - 1
                                    For x = 0 To bmd.Width - 1
                                        Runtime.InteropServices.Marshal.WriteByte(bmd.Scan0, (bmd.Stride * y) + (x), CByte(Colors.IndexOf(fp.GetPixel(x, y))))
                                    Next
                                Next
                            End Using

                            Indexed.UnlockBits(bmd)
                            Image = Indexed.Clone()
                        End Using
                    Else
                        MsgBox("Warning: Your image contains more than 256 colors! All this means is this program will not perform the conversion for you. You must manually convert this image to 8 bits per pixel if you wish to use this image in RPG Maker 2000/3.")
                    End If
                End If

                Dim ExtLoc As Integer = SaveDlg.FileName.LastIndexOf(".")
                If ExtLoc = -1 Then
                    SaveDlg.FileName = SaveDlg.FileName.Insert(SaveDlg.FileName.Length, ".png")
                    Image.Save(SaveDlg.FileName, Imaging.ImageFormat.Png)
                Else
                    Dim Loc As String = SaveDlg.FileName.Substring(ExtLoc + 1)
                    If String.Compare(Loc, "bmp", True) = 0 Then
                        Image.Save(SaveDlg.FileName, Imaging.ImageFormat.Bmp)
                    ElseIf String.Compare(Loc, "png", True) = 0 Then
                        Image.Save(SaveDlg.FileName, Imaging.ImageFormat.Png)
                    ElseIf String.Compare(Loc, "jpg", True) = 0 Then
                        Image.Save(SaveDlg.FileName, Imaging.ImageFormat.Jpeg)
                    End If
                End If
            End If
        End Using
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        ExportSprite()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton19.Click
        ExportSprite()
    End Sub

    ' New image
    Public Sub ResetEverything()
        ' Clear the layers list
        Character.RPGCharacterFiles(0).Layers.Clear()
        LayersWindow.TreeView1.Nodes.Clear()
        LayersWindow.CurrentNode = Nothing
        LayersWindow.ChangeStates(False)

        SetCurrentSavePosition("")
        Me.Text = Application.ProductName
        UpdateDrawing()
    End Sub

    Public Sub NewCharacter()
        Dim Result = MsgBox("You may have unsaved work. Would you like to save your changes before creating a new character?", MsgBoxStyle.YesNoCancel)
        If Result = MsgBoxResult.Yes Then
            DoSaveAs()
        ElseIf Result = MsgBoxResult.Cancel Then
            Return
        End If

        ResetEverything()
    End Sub

    Private Sub SingleCharacterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SingleCharacterToolStripMenuItem.Click
        NewCharacter()
    End Sub

    Private Sub ImagePackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImagePackToolStripMenuItem.Click
        Using SaveDlg As New SaveFileDialog
            SaveDlg.AddExtension = True
            SaveDlg.Filter = "Image Pack (.chx)|*.chx|All Files|*"
            SaveDlg.DefaultExt = "chx"
            SaveDlg.RestoreDirectory = True
            Dim Result = SaveDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                PerformImagePackCreate(SaveDlg.FileName)
            End If
        End Using
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        NewCharacter()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    ' Camera code
    Private MovingCamera As Boolean = False
    Private MouseDelta As New Point(0, 0)
    Private OldMousePos As New Point(0, 0)
    Private CameraPosition As New Point(0, 0)
    Public Property CanvasCamera() As Point
        Get
            Return CameraPosition
        End Get
        Set(ByVal value As Point)
            CameraPosition = value
        End Set
    End Property

    Private Sub PictureBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        MovingCamera = True
        OldMousePos.X = e.X
        OldMousePos.Y = e.Y
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        MovingCamera = False
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If MovingCamera Then
            MouseDelta = New Point(e.X, e.Y) - OldMousePos
            OldMousePos = New Point(e.X, e.Y)
            CameraPosition += MouseDelta
            UpdateDrawing()
        End If
    End Sub

    ' Reset camera
    Private Sub ResetCameraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetCameraToolStripMenuItem.Click
        CameraPosition = Point.Empty
        UpdateDrawing()
    End Sub

    ' Printing code
    Private WithEvents docToPrint As New Printing.PrintDocument

    ' The PrintDialog will print the document
    ' by handling the document's PrintPage event.
    Private Sub document_PrintPage(ByVal sender As Object, _
       ByVal e As System.Drawing.Printing.PrintPageEventArgs) _
           Handles docToPrint.PrintPage

        ' Insert code to render the page here.
        ' This code will be called when the control is drawn.
        Dim Render As Bitmap = RenderToTexture()

        e.Graphics.DrawImage(Render, 0, 0)
    End Sub

    Public Sub DoPrint()
        Using PrintDialog1 As New PrintDialog
            ' Allow the user to choose the page range he or she would
            ' like to print.
            PrintDialog1.AllowSomePages = True

            ' Show the help button.
            PrintDialog1.ShowHelp = True

            ' Set the Document property to the PrintDocument for 
            ' which the PrintPage Event has been handled. To display the
            ' dialog, either this property or the PrinterSettings property 
            ' must be set 
            PrintDialog1.Document = docToPrint

            Dim result As DialogResult = PrintDialog1.ShowDialog()

            ' If the result is OK then print the document.
            If (result = DialogResult.OK) Then
                docToPrint.Print()
            End If
        End Using
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        DoPrint()
    End Sub

    ' Print Preview
    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        Using Prev As New PrintPreviewDialog
            'Set the size, location, and name.
            Prev.ClientSize = New System.Drawing.Size(400, 300)
            Prev.Location = New System.Drawing.Point(29, 29)
            Prev.Name = "Print Preview"

            ' Set the minimum size the dialog can be resized to.
            Prev.MinimumSize = New System.Drawing.Size(375, 250)
            Prev.Document = docToPrint

            Prev.ShowDialog()
        End Using
    End Sub

    ' About
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        About.ShowDialog()
    End Sub

    ' Right-click for easy positioning tool
    Private Sub PictureBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            EasyPosition.Show()
            EasyPosition.Location = New Point(MousePosition.X, MousePosition.Y)
            EasyPosition.TopMost = True
        End If
    End Sub

    ' Clear Settings
    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        My.Settings.Reset()
    End Sub

    ' Game Editor
    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        GameEditor.ShowDialog()
    End Sub

    ' Animation number
    Private AnimationIndexVal As Integer = 0
    Public Property AnimationIndex() As Integer
        Get
            Return AnimationIndexVal
        End Get
        Set(ByVal value As Integer)
            AnimationIndexVal = value
        End Set
    End Property

    Public Sub UpdateAnimNum(ByVal Wanted As Integer)
        If Images.CurrentGameFile Is Nothing Then
            Wanted = 0
            ToolStripTextBox3.Text = "0"
            Return
        End If
        If Wanted > Images.CurrentGameFile.Animations.Count Then
            Wanted = Images.CurrentGameFile.Animations.Count
        ElseIf Wanted < 0 Then
            Wanted = 0
        End If

        If Wanted = 0 Then
            AnimationIndexVal = 0
            ToolStripTextBox3.Text = AnimationIndexVal.ToString()
            Return
        End If

        AnimationIndexVal = Wanted
        ToolStripTextBox3.Text = AnimationIndexVal.ToString()
        AnimationFrame = 0

        If AnimTimer.Enabled Then
            AnimTimer.Stop()
            AnimTimer.Start()
        End If
    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        UpdateAnimNum(AnimationIndexVal + 1)
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        UpdateAnimNum(AnimationIndexVal - 1)
    End Sub

    Private Sub ToolStripTextBox3_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles ToolStripTextBox3.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Or e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            e.Handled = True
            Dim EndResult As Boolean
            Dim EndValue As Integer
            EndResult = Integer.TryParse(ToolStripTextBox3.Text, EndValue)

            If EndResult = False Then Return

            UpdateAnimNum(EndValue)
        End If
    End Sub

    ' Reload Images
    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        Images.ReloadImages()
    End Sub

    ' Sheet Creator
    Private Sub SetSheetCreatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetSheetCreatorToolStripMenuItem.Click
        SetSheetCreator.ShowDialog()
    End Sub
End Class

Public Structure DockReturnValues
    Private WindowPositionVal As Point
    Private IsDockedVal As Boolean
    Private DockDifferenceVal As Point

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If (TypeOf (obj) Is DockReturnValues) = False Then Return False

        Return Equals(CType(obj, DockReturnValues))
    End Function

    Public Overloads Function Equals(ByVal PasObj As DockReturnValues) As Boolean
        Return (WindowPositionVal = PasObj.WindowPositionVal) And (IsDockedVal = PasObj.IsDockedVal) And (DockDifferenceVal = PasObj.DockDifferenceVal)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return IsDocked ^ (DockDifferenceVal.X ^ DockDifferenceVal.Y)
    End Function

    Shared Operator =(ByVal Left As DockReturnValues, ByVal Right As DockReturnValues)
        Return Left.Equals(Right)
    End Operator

    Shared Operator <>(ByVal Left As DockReturnValues, ByVal Right As DockReturnValues)
        Return (Left.Equals(Right) = False)
    End Operator

    Public Property WindowPosition() As Point
        Get
            Return WindowPositionVal
        End Get
        Set(ByVal value As Point)
            WindowPositionVal = value
        End Set
    End Property

    Public Property IsDocked() As Boolean
        Get
            Return IsDockedVal
        End Get
        Set(ByVal value As Boolean)
            IsDockedVal = value
        End Set
    End Property

    Public Property DockDifference() As Point
        Get
            Return DockDifferenceVal
        End Get
        Set(ByVal value As Point)
            DockDifferenceVal = value
        End Set
    End Property
End Structure

Public Structure DockStoreValues
    Private IsDockedVal As Boolean
    Private DockDifferenceVal As Point

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If (TypeOf (obj) Is DockStoreValues) = False Then Return False

        Return Equals(CType(obj, DockStoreValues))
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return IsDocked ^ (DockDifferenceVal.X ^ DockDifferenceVal.Y)
    End Function

    Public Overloads Function Equals(ByVal PasObj As DockStoreValues) As Boolean
        Return (IsDockedVal = PasObj.IsDockedVal) And (DockDifferenceVal = PasObj.DockDifferenceVal)
    End Function

    Shared Operator =(ByVal Left As DockStoreValues, ByVal Right As DockStoreValues)
        Return Left.Equals(Right)
    End Operator

    Shared Operator <>(ByVal Left As DockStoreValues, ByVal Right As DockStoreValues)
        Return (Left.Equals(Right) = False)
    End Operator

    Public Property IsDocked() As Boolean
        Get
            Return IsDockedVal
        End Get
        Set(ByVal value As Boolean)
            IsDockedVal = value
        End Set
    End Property

    Public Property DockDifference() As Point
        Get
            Return DockDifferenceVal
        End Get
        Set(ByVal value As Point)
            DockDifferenceVal = value
        End Set
    End Property

    Sub New(ByVal Right As DockReturnValues)
        IsDocked = Right.IsDocked
        DockDifference = Right.DockDifference
    End Sub
End Structure

Public Enum EHeaderValue
    HeaderCharacterNoImages
    HeaderCharacterImages
    HeaderImagePack
    HeaderUnknown
End Enum

' Position/size saving code
Public NotInheritable Class WindowGeometry
    Private Sub New()

    End Sub

    Shared Sub FromString(ByVal thisWindowGeometry As String, ByVal formIn As Form)
        If String.IsNullOrEmpty(thisWindowGeometry) Then Return
        If formIn Is Nothing Then Return

        Dim numbers As String() = thisWindowGeometry.Split("|")
        Dim windowString As String = numbers(4)

        If (windowString = "Normal") Then
            Dim windowPoint As Point = New Point(Integer.Parse(numbers(0)), Integer.Parse(numbers(1)))
            Dim windowSize As Size = New Size(Integer.Parse(numbers(2)), Integer.Parse(numbers(3)))

            Dim locOkay As Boolean = InBizarreLocation(windowPoint, windowSize)
            Dim sizeOkay As Boolean = IsBizarreSize(windowSize)

            If (locOkay = True And sizeOkay = True) Then
                formIn.Location = windowPoint
                formIn.Size = windowSize
                formIn.StartPosition = FormStartPosition.Manual
                formIn.WindowState = FormWindowState.Normal
            ElseIf (sizeOkay = True) Then
                formIn.Size = windowSize
            End If
        ElseIf (windowString = "Maximized") Then
            formIn.Location = New Point(100, 100)
            formIn.StartPosition = FormStartPosition.Manual
            formIn.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Shared Function InBizarreLocation(ByVal Loc As Point, ByVal Size As Size) As Boolean
        Dim locOkay As Boolean

        If (Loc.X < 0 Or Loc.Y < 0) Then
            locOkay = False
        ElseIf (Loc.X + Size.Width > Screen.PrimaryScreen.WorkingArea.Width) Then
            locOkay = False
        ElseIf (Loc.Y + Size.Height > Screen.PrimaryScreen.WorkingArea.Height) Then
            locOkay = False
        Else
            locOkay = True
        End If

        Return locOkay
    End Function

    Shared Function IsBizarreSize(ByVal Size As Size) As Boolean
        Return (Size.Height <= Screen.PrimaryScreen.WorkingArea.Height And Size.Width <= Screen.PrimaryScreen.WorkingArea.Width)
    End Function

    Overloads Shared Function ToString(ByVal mainForm As Form) As String
        If mainForm Is Nothing Then Return ""
        Return mainForm.Location.X.ToString() + "|" + mainForm.Location.Y.ToString() + "|" + mainForm.Size.Width.ToString() + "|" + mainForm.Size.Height.ToString() + "|" + mainForm.WindowState.ToString()
    End Function
End Class