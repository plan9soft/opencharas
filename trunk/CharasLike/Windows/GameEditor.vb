Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class GameEditor
    Private GameFile As RPGGameFile
    Private OldNumFrames As Integer = 0
    Private SavedPath As String = ""
    Private ReferenceImage As Bitmap

    Public Sub UpdateFrameBox()
        Dim WantedValue As Integer = NumericUpDown1.Value * NumericUpDown2.Value

        ListBox1.Items.Clear()
        For i As Integer = 1 To WantedValue
            ListBox1.Items.Add(i)
        Next

        If WantedValue < OldNumFrames Then
            For Each Anim In GameFile.Animations
                For i As Integer = WantedValue + 1 To OldNumFrames

                    If GameFile.Animations.IndexOf(Anim) = NumericUpDown4.Value - 1 Then
                        While True
                            Dim index As Integer = ListBox2.Items.IndexOf(i)
                            If index = -1 Then Exit While
                            ListBox2.Items.RemoveAt(index)
                        End While
                    End If
                    While True
                        Dim index As Integer = Anim.Frames.IndexOf(i)
                        If index = -1 Then Exit While
                        Anim.Frames.RemoveAt(index)
                    End While
                Next
            Next
        End If
        OldNumFrames = WantedValue
    End Sub

    Private DontUpdateList As Boolean = False
    Public Sub UpdateFramesList()
        If GameFile Is Nothing Then Return
        If DontUpdateList Then Return

        GameFile.SheetRows = NumericUpDown1.Value
        GameFile.SheetColumns = NumericUpDown2.Value
        NumericUpDown5.Maximum = (GameFile.SheetRows * GameFile.SheetColumns) - 1
        If NumericUpDown5.Value > NumericUpDown5.Maximum Then NumericUpDown5.Value = NumericUpDown5.Maximum
        UpdateFrameBox()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        UpdateFramesList()
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        UpdateFramesList()
    End Sub

    Private Sub UpdateAnimations()
        NumericUpDown4.Maximum = NumericUpDown3.Value
        If NumericUpDown4.Value > NumericUpDown3.Value Then NumericUpDown4.Value = NumericUpDown3.Value

        Dim WantedValue As Integer = NumericUpDown3.Value

        If GameFile.Animations.Count = WantedValue - 1 Then
            GameFile.Animations.Add(New RPGGameFileAnimation)
        ElseIf GameFile.Animations.Count = WantedValue + 1 Then
            GameFile.Animations.RemoveAt(WantedValue)
        Else
            If GameFile.Animations.Count < WantedValue Then
                For i As Integer = GameFile.Animations.Count + 1 To WantedValue
                    GameFile.Animations.Add(New RPGGameFileAnimation)
                Next
            ElseIf GameFile.Animations.Count > WantedValue Then
                Dim i As Integer = (GameFile.Animations.Count - WantedValue)
                While i > 0
                    i -= 1
                    GameFile.Animations.RemoveAt(WantedValue)
                End While
            End If
        End If
    End Sub

    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        UpdateAnimations()
    End Sub

    Public Sub GrayOutAnimStuff()
        ListBox2.Enabled = False
        ListBox2.Items.Clear()
        Button1.Enabled = False
        Button2.Enabled = False
    End Sub

    Public Sub EnableAnimStuff()
        ListBox2.Enabled = True
        Button1.Enabled = True
        Button2.Enabled = True

        For Each Frame In GameFile.Animations(NumericUpDown4.Value - 1).Frames
            ListBox2.Items.Add(Frame)
        Next
    End Sub

    Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        ListBox2.Items.Clear()

        If NumericUpDown4.Value = 0 Then
            GrayOutAnimStuff()
            Return
        End If

        EnableAnimStuff()
    End Sub

    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' TODO: load current game, if applicable
        SavedPath = ""
        GameFile = New RPGGameFile
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        GameFile.GameName = TextBox1.Text
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex = -1 Then
            MoveUpToolStripMenuItem.Enabled = False
            MoveDownToolStripMenuItem.Enabled = False
            Return
        End If

        MoveUpToolStripMenuItem.Enabled = True
        MoveDownToolStripMenuItem.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex = -1 Then Return
        ListBox2.Items.Add(ListBox1.SelectedItem)
        GameFile.Animations(NumericUpDown4.Value - 1).Frames.Add(ListBox1.SelectedItem)

        ListBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox2.SelectedIndex = -1 Then Return
        Dim Indx As Integer = ListBox2.SelectedIndex
        ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        GameFile.Animations(NumericUpDown4.Value - 1).Frames.RemoveAt(Indx)
        If ListBox2.Items.Count > 0 Then
            If (Indx - 1) = -1 Then Indx = 1
            ListBox2.SelectedIndex = Indx - 1
        End If

        ListBox1.Focus()
    End Sub

    Private Sub MoveUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveUpToolStripMenuItem.Click
        If ListBox2.SelectedIndex = -1 Or ListBox2.SelectedIndex = 0 Then Return

        Dim index As Integer = ListBox2.SelectedIndex
        Dim val As Integer = ListBox2.Items(index)
        ListBox2.Items.RemoveAt(index)
        ListBox2.Items.Insert(index - 1, val)
        GameFile.Animations(NumericUpDown4.Value - 1).Frames.RemoveAt(index)
        GameFile.Animations(NumericUpDown4.Value - 1).Frames.Insert(index - 1, val)
        ListBox2.SelectedIndex = index - 1
    End Sub

    Private Sub MoveDownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveDownToolStripMenuItem.Click
        If ListBox2.SelectedIndex = -1 Or ListBox2.SelectedIndex = ListBox2.Items.Count - 1 Then Return

        Dim index As Integer = ListBox2.SelectedIndex
        Dim val As Integer = ListBox2.Items(index)
        ListBox2.Items.RemoveAt(index)
        ListBox2.Items.Insert(index + 1, val)
        GameFile.Animations(NumericUpDown4.Value - 1).Frames.RemoveAt(index)
        GameFile.Animations(NumericUpDown4.Value - 1).Frames.Insert(index + 1, val)
        ListBox2.SelectedIndex = index + 1
    End Sub

    Public Sub DoASaveAs()
        Using SaveDlg As New SaveFileDialog
            SaveDlg.AddExtension = True
            SaveDlg.Filter = "GAM Files (.gam)|*.gam|All Files|*"
            SaveDlg.DefaultExt = "gam"
            SaveDlg.RestoreDirectory = True
            Dim Result = SaveDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                SavedPath = SaveDlg.FileName
                Using outFile = IO.File.Open(SavedPath, IO.FileMode.Create)
                    Using Writer As New IO.BinaryWriter(outFile)
                        If Writer IsNot Nothing Then
                            GameFile.Save(Writer)
                        End If
                    End Using
                End Using
            End If
        End Using
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If GameFile.GameName = "" Or GameFile.GameName = Nothing Then
            MsgBox("You might want to give this game mode a name!")
            Return
        End If

        If SavedPath <> "" Then
            Using outfile = IO.File.Open(SavedPath, IO.FileMode.Create)
                Using Writer As New IO.BinaryWriter(outfile)
                    If Writer IsNot Nothing Then
                        GameFile.Save(Writer)
                    End If
                End Using
            End Using
        Else
            DoASaveAs()
        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        DoASaveAs()
    End Sub

    Public Sub CreateNew()
        GameFile.Clear()
        TextBox1.Clear()
        NumericUpDown1.Value = 1
        NumericUpDown2.Value = 1
        NumericUpDown3.Value = 0
        NumericUpDown4.Value = 0
        ListBox1.Items.Clear()
        GrayOutAnimStuff()
        ReferenceImage = Nothing
        CheckBox1.Checked = False
        NumericUpDown5.Value = 0
        AnimMode = False
        FrameIndex = 0

        UpdateFramesList()
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Using OpenDlg As New OpenFileDialog
            OpenDlg.AddExtension = True
            OpenDlg.Filter = "GAM Files (.gam)|*.gam|All Files|*"
            OpenDlg.DefaultExt = "gam"
            OpenDlg.FilterIndex = 1
            OpenDlg.RestoreDirectory = True
            Dim Result = OpenDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                Dim Path As String = OpenDlg.FileName
                Using inFile = IO.File.Open(Path, IO.FileMode.Open)
                    Using Reader As New IO.BinaryReader(inFile)
                        If Reader IsNot Nothing Then
                            CreateNew()
                            GameFile.Load(Reader)

                            TextBox1.Text = GameFile.GameName
                            DontUpdateList = True
                            NumericUpDown1.Value = GameFile.SheetRows
                            NumericUpDown2.Value = GameFile.SheetColumns
                            DontUpdateList = False
                            NumericUpDown3.Value = GameFile.Animations.Count
                            NumericUpDown5.Maximum = (GameFile.SheetRows * GameFile.SheetColumns) - 1
                            CheckBox1.Checked = GameFile.Is8Bit
                            NumericUpDown5.Value = GameFile.PreviewFrame

                            UpdateFramesList()
                        End If
                    End Using
                End Using
            End If
        End Using
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        CreateNew()
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Using OpenDlg As New OpenFileDialog
            OpenDlg.AddExtension = True
            OpenDlg.Filter = "Image Files (.png;.jpg;.bmp)|*.png;*.jpg;*.bmp|All Files|*"
            OpenDlg.DefaultExt = "png"
            OpenDlg.FilterIndex = 1
            OpenDlg.RestoreDirectory = True
            Dim Result = OpenDlg.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then
                Dim Path As String = OpenDlg.FileName
                Using TmpImage As New Bitmap(Path)
                    ReferenceImage = New Bitmap(TmpImage.Width, TmpImage.Height, Imaging.PixelFormat.Format32bppArgb)
                    Dim Gfx As Graphics = Graphics.FromImage(ReferenceImage)
                    Gfx.DrawImage(TmpImage, New Rectangle(0, 0, TmpImage.Width, TmpImage.Height))
                    Gfx.Dispose()
                End Using

                Button3.Enabled = True
                Button5.Enabled = True
            End If
        End Using
    End Sub

    Private Sub CloseEditorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseEditorToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private FrameIndex As Integer = 0
    Public Sub UpdateMyPictureBox()
        If ReferenceImage Is Nothing Then Return

        Dim MyBMP As Bitmap
        Dim Gfx As Graphics

        If AnimMode = False Then
            MyBMP = New Bitmap(ReferenceImage.Width, ReferenceImage.Height, Imaging.PixelFormat.Format32bppArgb)
            Gfx = Graphics.FromImage(MyBMP)
            Gfx.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            Gfx.DrawImage(ReferenceImage, New Rectangle(0, 0, ReferenceImage.Width, ReferenceImage.Height))

            ' Draw lines
            Dim WidthMagic As Integer = ReferenceImage.Width \ GameFile.SheetRows
            Dim HeightMagic As Integer = ReferenceImage.Height \ GameFile.SheetColumns

            Dim Val As Integer = 0
            For i As Integer = 0 To GameFile.SheetRows
                Gfx.DrawLine(Pens.Black, New Point(Val, 0), New Point(Val, ReferenceImage.Height))
                Val += WidthMagic
            Next

            Val = 0
            For i As Integer = 0 To GameFile.SheetRows
                Gfx.DrawLine(Pens.Black, New Point(0, Val), New Point(ReferenceImage.Width, Val))
                Val += HeightMagic
            Next
        Else
            MyBMP = New Bitmap(ReferenceImage.Width \ GameFile.SheetRows, ReferenceImage.Height \ GameFile.SheetColumns, Imaging.PixelFormat.Format32bppArgb)
            Gfx = Graphics.FromImage(MyBMP)
            Gfx.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            Dim destRect = New Rectangle(0, 0, ReferenceImage.Width \ GameFile.SheetRows, ReferenceImage.Height \ GameFile.SheetColumns)
            Dim Frame As Integer
            If NumericUpDown4.Value = 0 Then Frame = GameFile.PreviewFrame Else Frame = GameFile.Animations(NumericUpDown4.Value - 1).GetFrame(FrameIndex)
            Dim srcRect = Character.GetRectangleForBitmapFrame(ReferenceImage, Frame, GameFile.SheetRows, GameFile.SheetColumns)
            Gfx.DrawImage(ReferenceImage, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel)
        End If

        Gfx.Dispose()

        PreviewWindow.PictureBox1.Image = MyBMP
    End Sub

    Public Shared Diff As Size = (New Size(88, 162) - New Size(72, 128))
    Dim TheTimer As New Timer
    Public Sub TickTimer(ByVal sender As Object, ByVal e As EventArgs)
        If PreviewWindow.Visible = False Then
            TheTimer.Stop()
            Return
        End If
        If NumericUpDown4.Value = 0 Then
            FrameIndex = GameFile.PreviewFrame
            Return
        End If

        If GameFile.Animations.Count = 0 Then
            FrameIndex = 0
            Return
        End If

        If (GameFile.Animations.Count - 1 < NumericUpDown4.Value - 1) Or GameFile.Animations(NumericUpDown4.Value - 1).Frames.Count = 0 Then
            FrameIndex = 0
            Return
        End If

        FrameIndex += 1
        If FrameIndex > GameFile.Animations(NumericUpDown4.Value - 1).Frames.Count - 1 Then
            FrameIndex = 0
        End If
        UpdateMyPictureBox()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If GameFile.SheetRows = 0 And GameFile.SheetColumns = 0 Then Return
        If PreviewWindow.Visible Then PreviewWindow.Close()

        PreviewWindow.Show(Me)

        PreviewWindow.StartPosition = FormStartPosition.Manual
        PreviewWindow.Location = Me.Location + New Point(Me.Size.Width, 0)
        PreviewWindow.Size = New Size(ReferenceImage.Width + Diff.Width, ReferenceImage.Height + Diff.Height)
        TheTimer.Interval = 350
        TheTimer.Start()
        AddHandler TheTimer.Tick, AddressOf TickTimer

        UpdateMyPictureBox()
    End Sub

    Private AnimMode_ As Boolean = False
    Public Property AnimMode() As Boolean
        Get
            Return AnimMode_
        End Get
        Set(ByVal value As Boolean)
            AnimMode_ = value
        End Set
    End Property

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        AnimMode = (AnimMode = False)

        If AnimMode Then
            PreviewWindow.Size = New Size((ReferenceImage.Width \ GameFile.SheetRows) + Diff.Width, (ReferenceImage.Height \ GameFile.SheetColumns) + Diff.Height)
        Else
            PreviewWindow.Size = New Size(ReferenceImage.Width + Diff.Width, ReferenceImage.Height + Diff.Height)
        End If
        UpdateMyPictureBox()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        GameFile.Is8Bit = CheckBox1.Checked
    End Sub

    Private Sub Form6_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        TheTimer.Stop()
        CreateNew()

        GrayOutAnimStuff()
    End Sub

    Private Sub NumericUpDown5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        GameFile.PreviewFrame = NumericUpDown5.Value

        If ReferenceImage IsNot Nothing Then UpdateMyPictureBox()
    End Sub

    Private Sub GameEditor_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        UpdateFramesList()
    End Sub
End Class

Public Class RPGGameFileAnimation
    Private Frames_ As New Collections.ObjectModel.Collection(Of Integer)
    Public ReadOnly Property Frames() As Collections.ObjectModel.Collection(Of Integer)
        Get
            Return Frames_
        End Get
    End Property

    Public Sub Save(ByVal Writer As IO.BinaryWriter)
        Writer.Write(Frames.Count)

        For Each Frame In Frames
            Writer.Write(Frame)
        Next
    End Sub

    Public Sub Load(ByVal Reader As IO.BinaryReader)
        Dim NumFrames As Integer = Reader.ReadInt32()

        For i As Integer = 0 To NumFrames - 1
            Frames.Add(Reader.ReadInt32())
        Next
    End Sub

    Public Function GetFrame(ByVal Index As Integer) As Integer
        Return Frames(Index) - 1
    End Function
End Class

Public Class RPGGameFile
    Private Path_ As String
    Public Property Path() As String
        Get
            Return Path_
        End Get
        Set(ByVal value As String)
            Path_ = value
        End Set
    End Property

    Private FilePath_ As String
    Public Property FilePath() As String
        Get
            Return FilePath_
        End Get
        Set(ByVal value As String)
            FilePath_ = value
        End Set
    End Property

    Private GameName_ As String
    Public Property GameName() As String
        Get
            Return GameName_
        End Get
        Set(ByVal value As String)
            GameName_ = value
        End Set
    End Property

    Private SheetRows_ As Integer
    Public Property SheetRows() As Integer
        Get
            Return SheetRows_
        End Get
        Set(ByVal value As Integer)
            SheetRows_ = value
        End Set
    End Property

    Private SheetColumns_ As Integer
    Public Property SheetColumns() As Integer
        Get
            Return SheetColumns_
        End Get
        Set(ByVal value As Integer)
            SheetColumns_ = value
        End Set
    End Property

    Private Is8Bit_ As Boolean
    Public Property Is8Bit() As Boolean
        Get
            Return Is8Bit_
        End Get
        Set(ByVal value As Boolean)
            Is8Bit_ = value
        End Set
    End Property

    Private PreviewFrame_ As Integer
    Public Property PreviewFrame() As Integer
        Get
            Return PreviewFrame_
        End Get
        Set(ByVal value As Integer)
            PreviewFrame_ = value
        End Set
    End Property

    Private Animations_ As New Collections.ObjectModel.Collection(Of RPGGameFileAnimation)
    Public ReadOnly Property Animations() As Collections.ObjectModel.Collection(Of RPGGameFileAnimation)
        Get
            Return Animations_
        End Get
    End Property

    Private MenuItem_ As GameModeMenuItem
    Public Property MenuItem() As GameModeMenuItem
        Get
            Return MenuItem_
        End Get
        Set(ByVal value As GameModeMenuItem)
            MenuItem_ = value
        End Set
    End Property

    Public Sub New()
        SheetRows = 1
        SheetColumns = 1
    End Sub

    Public Sub Clear()
        GameName = ""
        SheetRows = 1
        SheetColumns = 1
        Is8Bit = False
        Animations.Clear()
        PreviewFrame = 0
    End Sub

    Public Sub Save(ByVal Writer As IO.BinaryWriter)
        Writer.Write(GameName)
        Writer.Write(SheetRows)
        Writer.Write(SheetColumns)
        Writer.Write(Is8Bit)
        Writer.Write(PreviewFrame)
        Writer.Write(Animations.Count)

        For Each Anim In Animations
            Anim.Save(Writer)
        Next
    End Sub

    Public Sub Load(ByVal Reader As IO.BinaryReader)
        GameName = Reader.ReadString()
        SheetRows = Reader.ReadInt32()
        SheetColumns = Reader.ReadInt32()
        Is8Bit = Reader.ReadBoolean()
        PreviewFrame = Reader.ReadInt32()
        Dim AnimCount As Integer = Reader.ReadInt32()

        For i As Integer = 0 To AnimCount - 1
            Dim NewAnim As New RPGGameFileAnimation
            NewAnim.Load(Reader)
            Animations.Add(NewAnim)
        Next
    End Sub
End Class