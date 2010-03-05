Public Class SetSheetCreator
    Private SetWidth As Integer, SetHeight As Integer
    Private SheetRows As Integer, SheetColumns As Integer
    Private SheetWidth As Integer, SheetHeight As Integer

    Private SetImages() As Bitmap

    Private Sub CreateNew()
        If SetValues.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CurrentSavePos = Nothing
            SetWidth = SetValues.NumericUpDown1.Value
            SetHeight = SetValues.NumericUpDown2.Value
            SheetRows = SetValues.NumericUpDown3.Value
            SheetColumns = SetValues.NumericUpDown4.Value

            SheetWidth = SetWidth * SheetRows
            SheetHeight = SetHeight * SheetColumns

            TreeView1.Nodes.Clear()

            For i As Integer = 1 To (SheetRows * SheetColumns)
                TreeView1.Nodes.Add("Set " + i.ToString())
            Next

            ReDim SetImages(SheetRows * SheetColumns)
        End If
        UpdateSet()
    End Sub

    Private Sub Form8_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        CreateNew()
    End Sub

    Private Sub UpdateSet()
        Dim StoredImage As Bitmap = Nothing
        If SplitContainer1.Panel1.Width And SplitContainer1.Panel1.Height Then
            StoredImage = New Bitmap(SplitContainer1.Panel1.Width, SplitContainer1.Panel1.Height, Imaging.PixelFormat.Format32bppArgb)
            Dim TempGfx As Graphics = Graphics.FromImage(StoredImage)

            Dim ContentWidth As Integer = CType(SplitContainer1.Panel1.Width, Integer)
            Dim ContentHeight As Integer = CType(SplitContainer1.Panel1.Height, Integer)
            TempGfx.DrawLine(Pens.Black, New PointF(ContentWidth \ 2, 0), New PointF(ContentWidth \ 2, ContentHeight))
            TempGfx.DrawLine(Pens.Black, New PointF(0, ContentHeight \ 2), New PointF(ContentWidth, ContentHeight \ 2))

            TempGfx.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

            If SheetWidth <> 0 And SheetHeight <> 0 Then
                Using EntireSheet As New Bitmap(SheetWidth + 1, SheetHeight + 1, Imaging.PixelFormat.Format32bppArgb)
                    Dim SheetGfx As Graphics = Graphics.FromImage(EntireSheet)

                    Dim What As Integer = 0
                    Dim TheY As Integer = 0
                    For y As Integer = 0 To SheetColumns - 1
                        Dim TheX As Integer = 0

                        For x As Integer = 0 To SheetRows - 1
                            If SetImages(What) IsNot Nothing Then
                                SheetGfx.DrawImage(SetImages(What), New Rectangle(TheX, TheY, SetWidth, SetHeight))
                            End If

                            What += 1
                            TheX += SetWidth
                        Next

                        TheY += SetHeight
                    Next

                    SheetGfx.Dispose()

                    Dim ZoomVal As Single = ZoomNumber / 100

                    ' Draw rectangles separately
                    TheY = (ContentHeight / 2) - (SheetHeight * ZoomVal) / 2
                    For y As Integer = 0 To SheetColumns - 1
                        Dim TheX As Integer = (ContentWidth / 2) - (SheetWidth * ZoomVal) / 2

                        For x As Integer = 0 To SheetRows - 1
                            TempGfx.DrawRectangle(Pens.Black, New Rectangle(TheX, TheY, SetWidth * ZoomVal, SetHeight * ZoomVal))
                            TheX += SetWidth * ZoomVal
                        Next

                        TheY += SetHeight * ZoomVal
                    Next

                    TempGfx.DrawImage(EntireSheet, New Rectangle((ContentWidth / 2) - (SheetWidth * ZoomVal) / 2, (ContentHeight / 2) - (SheetHeight * ZoomVal) / 2, SheetWidth * ZoomVal, SheetHeight * ZoomVal))
                End Using
            End If
            TempGfx.Dispose()
        End If

        PictureBox1.Image = StoredImage
    End Sub

    Private Sub Form8_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        UpdateSet()
    End Sub

    Public Sub DoImportImageToSet()
        If TreeView1.SelectedNode Is Nothing Then Return

        Using OpenDlg As New OpenFileDialog
            OpenDlg.AddExtension = True
            OpenDlg.Filter = "Image Files (.png;.jpg;.bmp)|*.png;*.jpg;*.bmp|All Files|*"
            OpenDlg.DefaultExt = "png"
            OpenDlg.FilterIndex = 1
            OpenDlg.RestoreDirectory = True
            Dim Result = OpenDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                Dim Path As String = OpenDlg.FileName
                Using TmpImage = Images.Convert8BPPTo32BPP(New Bitmap(Path))
                    Dim ReferenceImage As New Bitmap(TmpImage.Width, TmpImage.Height, Imaging.PixelFormat.Format32bppArgb)
                    Dim Gfx As Graphics = Graphics.FromImage(ReferenceImage)
                    Gfx.DrawImage(TmpImage, New Rectangle(0, 0, TmpImage.Width, TmpImage.Height))
                    Gfx.Dispose()

                    Using ImgFP As New FastPixel(ReferenceImage, True)
                        Using TempMapFP As New FastPixel(TmpImage, False)
                            Dim TransColor As Color = TempMapFP.GetPixel(0, 0)

                            For i As Integer = 0 To TmpImage.Width - 1
                                For z As Integer = 0 To TmpImage.Height - 1
                                    If (TempMapFP.GetPixel(i, z) = TransColor) Then
                                        ImgFP.SetPixel(i, z, Color.FromArgb(0, 0, 0, 0))
                                    Else
                                        ImgFP.SetPixel(i, z, TempMapFP.GetPixel(i, z))
                                    End If
                                Next
                            Next
                        End Using
                    End Using

                    SetImages(TreeView1.SelectedNode.Index) = ReferenceImage
                End Using
                UpdateSet()
            End If
        End Using
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        DoImportImageToSet()
    End Sub

    Private CurrentSavePos As String = Nothing
    Private CurrentSaveIs8Bit As Boolean = False

    Public Function RenderToTexture(ByVal ResetTrans As Boolean) As Bitmap
        Dim RenderedImage As Bitmap

        RenderedImage = New Bitmap(SheetWidth, SheetHeight, Imaging.PixelFormat.Format32bppArgb)
        Dim TempGfx As Graphics = Graphics.FromImage(RenderedImage)
        TempGfx.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

        ' Go through each layer drawing the sheet
        Dim What As Integer = 0
        Dim TheY As Integer = 0
        For y As Integer = 0 To SheetColumns - 1
            Dim TheX As Integer = 0

            For x As Integer = 0 To SheetRows - 1
                If SetImages(What) IsNot Nothing Then
                    TempGfx.DrawImage(SetImages(What), New Rectangle(TheX, TheY, SetWidth, SetHeight))
                End If

                What += 1
                TheX += SetWidth
            Next

            TheY += SetHeight
        Next

        TempGfx.Dispose()

        If ResetTrans = False Then
            Dim ColorList = Canvas.NumColorsInImage(RenderedImage)
            Dim EndedUpColor As Color = Canvas.SpecialColor

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

    Public Function RenderToTexture() As Bitmap
        Return RenderToTexture(False)
    End Function

    Public Sub DoSave()
        Dim Image As Bitmap = RenderToTexture(CurrentSaveIs8Bit = False)

        If CurrentSaveIs8Bit Then
            Dim Colors = Canvas.NumColorsInImage(Image)

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

                    Using fp As New FastPixel(Image, True)
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

        Dim ExtLoc As Integer = CurrentSavePos.LastIndexOf(".")
        If ExtLoc = -1 Then
            CurrentSavePos = CurrentSavePos.Insert(CurrentSavePos.Length, ".png")
            Image.Save(CurrentSavePos, Imaging.ImageFormat.Png)
        Else
            Dim Loc As String = CurrentSavePos.Substring(ExtLoc + 1)
            If String.Compare(Loc, "bmp", True) = 0 Then
                Image.Save(CurrentSavePos, Imaging.ImageFormat.Bmp)
            ElseIf String.Compare(Loc, "png", True) = 0 Then
                Image.Save(CurrentSavePos, Imaging.ImageFormat.Png)
            ElseIf String.Compare(Loc, "jpg", True) = 0 Then
                Image.Save(CurrentSavePos, Imaging.ImageFormat.Jpeg)
            End If
        End If
        Image.Dispose()
    End Sub

    Public Sub DoSaveAs()
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
                CurrentSavePos = SaveDlg.FileName
                CurrentSaveIs8Bit = (SaveDlg.FilterIndex = 1)
                DoSave()
            End If
        End Using
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        DoSaveAs()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        If CurrentSavePos = Nothing Then
            DoSaveAs()
        Else
            DoSave()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ImportImageToThisSetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportImageToThisSetToolStripMenuItem.Click
        DoImportImageToSet()
    End Sub

    Public Sub TreeView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TreeView1.MouseDown
        If e.Button = MouseButtons.Right Then
            TreeView1.SelectedNode = TreeView1.GetNodeAt(e.X, e.Y)
        End If
    End Sub

    Public Sub ClearCurrentSet()
        If TreeView1.SelectedNode Is Nothing Then Return

        SetImages(TreeView1.SelectedNode.Index) = Nothing
        UpdateSet()
    End Sub

    Private Sub ClearImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearImageToolStripMenuItem.Click
        ClearCurrentSet()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        ClearCurrentSet()
    End Sub

    Private ZoomNumber As Integer = 100

    Private Sub SetZoom(ByVal Num As Integer)
        If Num < 0 Then Num = 0
        If Num > 1000 Then Num = 1000

        ToolStripTextBox1.Text = Num.ToString() + "%"
        ZoomNumber = Num
    End Sub

    Public Sub ZoomByNumber(ByVal Addition As Integer)
        SetZoom(ZoomNumber + Addition)
        UpdateSet()
    End Sub

    Const ZoomInAmount = 25

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        ZoomByNumber(ZoomInAmount)
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        ZoomByNumber(-ZoomInAmount)
    End Sub

    Private Sub SetSheetCreator_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseWheel
        If e.Delta > 0 Then
            ZoomByNumber(ZoomInAmount)
        ElseIf e.Delta < 0 Then
            ZoomByNumber(-ZoomInAmount)
        End If
    End Sub

    Private Sub TreeView1_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TreeView1.MouseWheel
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

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        CreateNew()
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        CreateNew()
    End Sub

    Public Sub ExportSetToImage(ByVal Path As String, ByVal Indexed As Boolean)
        Static RandomNumGen As New System.Random
        Dim Image As New Bitmap(SetImages(TreeView1.SelectedNode.Index))

        If Indexed Then
            Dim ColorList = Canvas.NumColorsInImage(Image)
            Dim EndedUpColor As Color = Canvas.SpecialColor


            While ColorList.IndexOf(EndedUpColor) <> -1
                EndedUpColor = Color.FromArgb(RandomNumGen.Next(256), RandomNumGen.Next(256), RandomNumGen.Next(256))
            End While

            Using fp As New FastPixel(Image, True)
                For i As Integer = 0 To Image.Width - 1
                    For z As Integer = 0 To Image.Height - 1
                        If fp.GetPixel(i, z).A = 0 Then
                            fp.SetPixel(i, z, EndedUpColor)
                        End If
                    Next
                Next
            End Using
        End If

        If CurrentSaveIs8Bit Then
            Dim Colors = Canvas.NumColorsInImage(Image)

            If Colors.Count <= 256 Then
                Using IndexedImage As New Bitmap(Image.Width, Image.Height, Imaging.PixelFormat.Format8bppIndexed)

                    ' Create a palette
                    Dim Pal As Imaging.ColorPalette = IndexedImage.Palette
                    For i As Integer = 0 To 255
                        If i > Colors.Count - 1 Then
                            Pal.Entries(i) = Color.White
                        Else
                            Pal.Entries(i) = Colors(i)
                        End If
                    Next
                    IndexedImage.Palette = Pal

                    Dim bmd As Imaging.BitmapData = IndexedImage.LockBits(New Rectangle(0, 0, Image.Width, Image.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, IndexedImage.PixelFormat)

                    Using fp As New FastPixel(Image, False)
                        For y = 0 To bmd.Height - 1
                            For x = 0 To bmd.Width - 1
                                Runtime.InteropServices.Marshal.WriteByte(bmd.Scan0, (bmd.Stride * y) + (x), CByte(Colors.IndexOf(fp.GetPixel(x, y))))
                            Next
                        Next
                    End Using

                    IndexedImage.UnlockBits(bmd)

                    Image = IndexedImage.Clone()
                End Using
            Else
                MsgBox("Warning: Your image contains more than 256 colors! All this means is this program will not perform the conversion for you. You must manually convert this image to 8 bits per pixel if you wish to use this image in RPG Maker 2000/3.")
            End If
        End If

        Dim ExtLoc As Integer = Path.LastIndexOf(".")
        If ExtLoc = -1 Then
            Path = Path.Insert(Path.Length, ".png")
            Image.Save(Path, Imaging.ImageFormat.Png)
        Else
            Dim Loc As String = Path.Substring(ExtLoc + 1)
            If String.Compare(Loc, "bmp", True) = 0 Then
                Image.Save(Path, Imaging.ImageFormat.Bmp)
            ElseIf String.Compare(Loc, "png", True) = 0 Then
                Image.Save(Path, Imaging.ImageFormat.Png)
            ElseIf String.Compare(Loc, "jpg", True) = 0 Then
                Image.Save(Path, Imaging.ImageFormat.Jpeg)
            End If
        End If
        Image.Dispose()
    End Sub

    Public Sub DoExportSet()
        If TreeView1.SelectedNode Is Nothing Then Return
        If SetImages(TreeView1.SelectedNode.Index) Is Nothing Then Return

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
                ExportSetToImage(SaveDlg.FileName, SaveDlg.FilterIndex = 1)
            End If
        End Using
    End Sub

    Private Sub ExportSetToImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportSetToImageToolStripMenuItem.Click
        DoExportSet()
    End Sub

    Public Sub DoOpen()
        If MsgBox("Opening an existing sheet will erase this unsaved sheet. Do you wish to save first?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DoSaveAs()
            CurrentSavePos = ""
        End If

        Using OpenDlg As New OpenFileDialog
            OpenDlg.AddExtension = True
            OpenDlg.Filter = "Image Files (.png;.jpg;.bmp)|*.png;*.jpg;*.bmp|All Files|*"
            OpenDlg.DefaultExt = "png"
            OpenDlg.RestoreDirectory = True
            Dim Result = OpenDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                Dim Bmp As Bitmap = Images.Convert8BPPTo32BPP(New Bitmap(OpenDlg.FileName))

                Using ImgFP As New FastPixel(Bmp, True)
                    Dim TransColor As Color = ImgFP.GetPixel(0, 0)

                    For i As Integer = 0 To Bmp.Width - 1
                        For z As Integer = 0 To Bmp.Height - 1
                            If (ImgFP.GetPixel(i, z) = TransColor) Then
                                ImgFP.SetPixel(i, z, Color.FromArgb(0, 0, 0, 0))
                            End If
                        Next
                    Next
                End Using

                SetValues.NumericUpDown1.Enabled = False
                SetValues.NumericUpDown2.Enabled = False
                If SetValues.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    CurrentSavePos = Nothing
                    SheetRows = SetValues.NumericUpDown3.Value
                    SheetColumns = SetValues.NumericUpDown4.Value
                    SetWidth = Bmp.Width \ SheetRows
                    SetHeight = Bmp.Height \ SheetColumns

                    SheetWidth = Bmp.Width
                    SheetHeight = Bmp.Height

                    TreeView1.Nodes.Clear()

                    For i As Integer = 1 To (SheetRows * SheetColumns)
                        TreeView1.Nodes.Add("Set " + i.ToString())
                    Next

                    ReDim SetImages(SheetRows * SheetColumns)

                    Dim What As Integer = 0
                    Dim TheY As Integer = 0
                    For y As Integer = 0 To SheetColumns - 1
                        Dim TheX As Integer = 0

                        For x As Integer = 0 To SheetRows - 1
                            Dim NewBmpSheet As New Bitmap(SetWidth, SetHeight)
                            Dim GfxTmp = Graphics.FromImage(NewBmpSheet)

                            GfxTmp.DrawImage(Bmp, New Rectangle(0, 0, SetWidth, SetHeight), New Rectangle(TheX, TheY, SetWidth, SetHeight), GraphicsUnit.Pixel)
                            GfxTmp.Dispose()
                            SetImages(What) = NewBmpSheet
                            What += 1
                            TheX += SetWidth
                        Next

                        TheY += SetHeight
                    Next

                End If
                SetValues.NumericUpDown1.Enabled = True
                SetValues.NumericUpDown2.Enabled = True
                UpdateSet()
            End If
        End Using
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        DoOpen()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        DoExportSet()
    End Sub

    Private Sub ToolStripButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton18.Click
        If CurrentSavePos = Nothing Then
            DoSaveAs()
        Else
            DoSave()
        End If
    End Sub

    Private Sub ToolStripButton13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton13.Click
        DoOpen()
    End Sub
End Class