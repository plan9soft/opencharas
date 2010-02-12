Public Class RPGImage
    Private RawBitmap_ As Bitmap
    Public Property RawBitmap() As Bitmap
        Get
            Return RawBitmap_
        End Get
        Set(ByVal value As Bitmap)
            RawBitmap_ = value
        End Set
    End Property

    Private OriginalBitmap_ As Bitmap
    Public Property OriginalBitmap() As Bitmap
        Get
            Return OriginalBitmap_
        End Get
        Set(ByVal value As Bitmap)
            OriginalBitmap_ = value
        End Set
    End Property

    Private Box_ As RPGPictureBox
    Public Property Box() As RPGPictureBox
        Get
            Return Box_
        End Get
        Set(ByVal value As RPGPictureBox)
            Box_ = value
        End Set
    End Property

    Private Path_ As String
    Public Property Path() As String
        Get
            Return Path_
        End Get
        Set(ByVal value As String)
            Path_ = value
        End Set
    End Property

    Private Name_ As String
    Public Property Name() As String
        Get
            Return Name_
        End Get
        Set(ByVal value As String)
            Name_ = value
        End Set
    End Property

    Private GameFile_ As RPGGameFile
    Public Property GameFile() As RPGGameFile
        Get
            Return GameFile_
        End Get
        Set(ByVal value As RPGGameFile)
            GameFile_ = value
        End Set
    End Property
End Class

Public Class GameNodeEntry
    Private Subdirs_ As New Collections.ObjectModel.Collection(Of GameNodeEntry)
    Public ReadOnly Property Subdirs() As Collections.ObjectModel.Collection(Of GameNodeEntry)
        Get
            Return Subdirs_
        End Get
    End Property

    Private Images_ As New Collections.ObjectModel.Collection(Of RPGImage)
    Public ReadOnly Property Images() As Collections.ObjectModel.Collection(Of RPGImage)
        Get
            Return Images_
        End Get
    End Property

    Private NodeName_ As String
    Public Property NodeName() As String
        Get
            Return NodeName_
        End Get
        Set(ByVal value As String)
            NodeName_ = value
        End Set
    End Property

    Private Path_ As String
    Public Property Path() As String
        Get
            Return Path_
        End Get
        Set(ByVal value As String)
            Path_ = value
        End Set
    End Property

    Private GameFile_ As RPGGameFile
    Public Property GameFile() As RPGGameFile
        Get
            Return GameFile_
        End Get
        Set(ByVal value As RPGGameFile)
            GameFile_ = value
        End Set
    End Property

    Public Sub RecurseNodeAndAddAll()
        For Each Image In Images
            ImagePacker.FlowLayoutPanel2.Controls.Add(Image.Box.CreatePictureBoxCopy())
        Next

        For Each Subdir In Subdirs
            Subdir.RecurseNodeAndAddAll()
        Next
    End Sub
End Class

Public Class GameNodeNode
    Inherits TreeNode
    Private RPGNode_ As GameNodeEntry
    Public Property RPGNode() As GameNodeEntry
        Get
            Return RPGNode_
        End Get
        Set(ByVal value As GameNodeEntry)
            RPGNode_ = value
        End Set
    End Property
End Class

Public Class GameModeMenuItem
    Inherits ToolStripMenuItem
    Private GameFile_ As RPGGameFile
    Public Property GameFile() As RPGGameFile
        Get
            Return GameFile_
        End Get
        Set(ByVal value As RPGGameFile)
            GameFile_ = value
        End Set
    End Property
End Class

Public Class Images
    Public Shared RPGNodes As New Collections.ObjectModel.Collection(Of GameNodeEntry)
    Public Shared RPGGames As New Collections.ObjectModel.Collection(Of RPGGameFile)

    Shared Function CompareGameFiles(ByVal Left As RPGGameFile, ByVal Right As RPGGameFile) As Boolean
        If Left Is Nothing Or Right Is Nothing Then Return False

        If Left.SheetRows <> Right.SheetRows Then Return False
        If Left.SheetColumns <> Right.SheetColumns Then Return False
        If Left.PreviewFrame <> Right.PreviewFrame Then Return False
        If Left.Is8Bit <> Right.Is8Bit Then Return False
        If Left.GameName <> Right.GameName Then Return False
        If Left.Animations.Count <> Right.Animations.Count Then Return False

        For i As Integer = 0 To Left.Animations.Count - 1
            If Left.Animations(i).Frames.Count <> Right.Animations(i).Frames.Count Then Return False

            For z As Integer = 0 To Left.Animations(i).Frames.Count - 1
                If Left.Animations(i).Frames(z) <> Right.Animations(i).Frames(z) Then Return False
            Next
        Next

        Return True
    End Function

    Shared Function FindGame(ByVal CheckGame As RPGGameFile) As RPGGameFile
        If CheckGame Is Nothing Then Return Nothing

        For Each GameFile In RPGGames
            If String.Compare(GameFile.FilePath, CheckGame.FilePath, True) = 0 Then
                ' Check game file
                If CompareGameFiles(CheckGame, GameFile) = True Then
                    Return GameFile ' Gotcha
                Else
                    If MsgBox("The required game file (" + CheckGame.FilePath + ") varies from your game file version. In order for this to work properly, you must replace your game file with this one." + vbNewLine + vbNewLine + "A backup will be made at " + CheckGame.FilePath + ".bak. Continue?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                        ' FIXME: do it
                        IO.File.Copy(GameFile.FilePath, GameFile.FilePath + ".bak")

                        Using File As New IO.BinaryWriter(IO.File.Open(GameFile.FilePath, IO.FileMode.Truncate))
                            CheckGame.Save(File)
                        End Using

                        Return GameFile
                    Else
                        Return Nothing
                    End If
                End If
            End If
        Next
        Return Nothing
    End Function

    Shared Function FindGameByPath(ByVal Path As String) As RPGGameFile
        For Each GameFile In RPGGames
            If String.Compare(GameFile.Path, Path, True) = 0 Then
                Return GameFile
            End If
        Next
        Return Nothing
    End Function

    Shared Function SearchImage(ByVal List As Collections.ObjectModel.Collection(Of GameNodeEntry), ByVal Paths As String(), ByVal CurIndex As Integer) As RPGImage
        For Each Node In List
            If String.Compare(Node.NodeName, Paths(CurIndex), True) = 0 Then
                CurIndex += 1

                ' Are we at the end of the list now?
                If CurIndex = Paths.Length - 1 Then
                    ' See if we have said image
                    For Each Img In Node.Images
                        If String.Compare(Img.Name, Paths(CurIndex), True) = 0 Then
                            Return Img
                        End If
                    Next
                    Return Nothing
                Else
                    ' Keep searching from here
                    Return SearchImage(Node.Subdirs, Paths, CurIndex)
                End If
            End If
        Next

        Return Nothing
    End Function

    Shared Function FindImage(ByVal Path As String) As RPGImage
        Dim Main As String = Path.Substring(5)
        Dim Paths() As String = Main.Split(New String() {"\"}, StringSplitOptions.None)

        Return SearchImage(RPGNodes, Paths, 0)
    End Function

    Shared Function BitmapCompare(ByVal Bmp1 As Bitmap, ByVal Bmp2 As Bitmap) As Boolean
        If Bmp1.Size <> Bmp2.Size Then Return False

        Using fpBmp1 As New FastPixel(Bmp1, True), fpBmp2 As New FastPixel(Bmp2, True)
            For y As Integer = 0 To fpBmp1.Height - 1
                For x As Integer = 0 To fpBmp1.Width - 1
                    If fpBmp1.GetPixel(x, y) <> fpBmp2.GetPixel(x, y) Then Return False
                Next
            Next
        End Using

        Return True ' Same
    End Function

    Shared Function Convert8BPPTo32BPP(ByVal Convert As Image) As Bitmap
        If Convert Is Nothing Then Return Nothing

        Dim TempMap = New Bitmap(Convert.Width, Convert.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim Gfx As Graphics = Graphics.FromImage(TempMap)
        Gfx.DrawImage(Convert, New Rectangle(0, 0, Convert.Width, Convert.Height))
        Gfx.Dispose()

        Return TempMap
    End Function

    Shared Sub LoadRPGImage(ByVal Node As GameNodeEntry, ByVal Path As String)
        If Node Is Nothing Or Path Is Nothing Then Return

        Dim Img As New RPGImage
        Using TmpImage As New Bitmap(Path)
            Dim TempMap As Bitmap = Convert8BPPTo32BPP(TmpImage)

            Img.RawBitmap = New Bitmap(TempMap.Width, TempMap.Height, Imaging.PixelFormat.Format32bppArgb)
            Img.OriginalBitmap = New Bitmap(TmpImage)
            Img.GameFile = Node.GameFile

            Using ImgFP As New FastPixel(Img.RawBitmap, True)
                Using TempMapFP As New FastPixel(TempMap, True)
                    Dim TransColor As Color = TempMapFP.GetPixel(0, 0)

                    For i As Integer = 0 To TempMap.Width - 1
                        For z As Integer = 0 To TempMap.Height - 1
                            If (TempMapFP.GetPixel(i, z) = TransColor) Then
                                ImgFP.SetPixel(i, z, Color.FromArgb(0, 0, 0, 0))
                            Else
                                ImgFP.SetPixel(i, z, TempMapFP.GetPixel(i, z))
                            End If
                        Next
                    Next
                End Using
            End Using
        End Using

        Img.Path = Path
        Img.Name = Path.Substring(Path.LastIndexOf("\") + 1)
        Img.Box = RPGPictureBox.Create(Img)

        Node.Images.Add(Img)
    End Sub

    Shared Sub AddAndRecurseNodes(ByVal Nodes As TreeNodeCollection, ByVal GameNode As GameNodeEntry)
        If Nodes Is Nothing Or GameNode Is Nothing Then Return

        ' Insert me
        Dim Node As New GameNodeNode
        Node.RPGNode = GameNode
        Node.Text = GameNode.NodeName

        Nodes.Add(Node)

        For Each SubNode In GameNode.Subdirs
            AddAndRecurseNodes(Node.Nodes, SubNode)
        Next
    End Sub

    Shared Sub RecurseSubdirectories(ByVal Node As GameNodeEntry)
        If Node Is Nothing Then Return

        Dim entries As String() = IO.Directory.GetDirectories(Node.Path)

        For Each directory In entries
            If directory.Substring(directory.LastIndexOf("\") + 1).StartsWith(".") Then Continue For

            Dim Entry As New GameNodeEntry
            Entry.NodeName = directory.Substring(directory.LastIndexOf("\") + 1)
            Entry.Path = directory
            Entry.GameFile = CurrentGameFile

            Dim Files As New List(Of String)
            Files.AddRange(IO.Directory.GetFiles(directory, "*.png"))
            Files.AddRange(IO.Directory.GetFiles(directory, "*.bmp"))
            Files.AddRange(IO.Directory.GetFiles(directory, "*.jpg"))

            ' See if path has any images
            For Each Img In Files
                LoadRPGImage(Entry, Img)
            Next

            RecurseSubdirectories(Entry)

            Node.Subdirs.Add(Entry)
        Next
    End Sub

    Public Shared CurrentGameFile As RPGGameFile

    Shared Sub GameMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Item As GameModeMenuItem = CType(sender, GameModeMenuItem)

        If Item.GameFile Is CurrentGameFile Then Return

        ' Set game
        CurrentGameFile.MenuItem.Checked = False
        CurrentGameFile = Item.GameFile
        Item.Checked = True

        My.Settings.CurrentGame = CurrentGameFile.Path
    End Sub

    Shared Sub ClearImagesSubDirs(ByVal Node As GameNodeEntry)
        If Node Is Nothing Then Return

        For Each SubNode In Node.Subdirs
            SubNode.Images.Clear()
            ClearImagesSubDirs(SubNode)
        Next
    End Sub

    Shared Sub ClearImages()
        RPGNodes.Clear()
        ItemsWindow.FlowLayoutPanel1.Controls.Clear()
        ItemsWindow.FlowLayoutPanel1.Controls.Add(RPGPictureBox.EmptyBox)
        ItemsWindow.TreeView1.Nodes.Clear()
    End Sub

    ' Loads images and game files
    Shared Sub LoadImages()
        Dim subdirectoryEntries As String() = IO.Directory.GetDirectories("data\")

        Dim WillBeCurrentGame As RPGGameFile = Nothing
        For Each Game In subdirectoryEntries
            If Game.Substring(Game.LastIndexOf("\") + 1).StartsWith(".") Then Continue For
            Dim fileEntries As String() = IO.Directory.GetFiles(Game)

            Dim Entry As New GameNodeEntry
            Entry.NodeName = Game.Substring(Game.LastIndexOf("\") + 1)
            Entry.Path = Game

            ' Is there a .gam file in here?
            For Each File In fileEntries
                If File.EndsWith(".gam") Then
                    Entry.GameFile = New RPGGameFile
                    Using infile = IO.File.Open(File, IO.FileMode.Open)
                        Using Reader As New IO.BinaryReader(infile)
                            Entry.GameFile.FilePath = File

                            If Reader IsNot Nothing Then
                                Entry.GameFile.Load(Reader)
                                Entry.GameFile.Path = Entry.Path
                            End If

                            RPGGames.Add(Entry.GameFile)

                            CurrentGameFile = Entry.GameFile
                            If My.Settings.CurrentGame = CurrentGameFile.Path Then WillBeCurrentGame = CurrentGameFile
                            Exit For
                        End Using
                    End Using
                End If
            Next

            RecurseSubdirectories(Entry)

            RPGNodes.Add(Entry)
        Next

        If WillBeCurrentGame IsNot Nothing Then
            CurrentGameFile = WillBeCurrentGame
        Else
            CurrentGameFile = Nothing
        End If

        ' Fill in the menu
        For Each Game In RPGGames
            Dim MenuItem As GameModeMenuItem = New GameModeMenuItem
            MenuItem.GameFile = Game
            Game.MenuItem = MenuItem
            MenuItem.Text = Game.GameName
            AddHandler MenuItem.Click, AddressOf GameMenuItemClick

            If CurrentGameFile IsNot Nothing Then
                If Game Is CurrentGameFile Then MenuItem.Checked = True
            ElseIf My.Settings.FirstRun Or My.Settings.CurrentGame = "" Or CurrentGameFile Is Nothing Then
                CurrentGameFile = Game
                My.Settings.CurrentGame = CurrentGameFile.Path
                MenuItem.Checked = True
            End If

            Canvas.SetGameMenuItem.DropDownItems.Add(MenuItem)
        Next

        ' Fill the tree view in ItemsWindow
        For Each Node In RPGNodes
            AddAndRecurseNodes(ItemsWindow.TreeView1.Nodes, Node)
        Next
    End Sub

    ' Call this when you want to refresh the image list.
    Shared Sub ReloadImages()
        Dim Backup As String = CurrentGameFile.Path

        ' Back up all image data in the layers
        ' FIXME: do all characters
        For Each Layer In CharacterSelect.CharacterList.CurrentCharacter().Character.Layers
            If Layer.Image IsNot Nothing Then
                Dim Img As RPGImage = Layer.Image

                Layer.Image = New RPGImage
                Layer.Image.Path = Img.Path
            End If
            Layer.ReloadImagesSubLayers(False)
        Next

        ' Clear nodes
        ClearImages()

        ' Clear games
        Canvas.SetGameMenuItem.DropDownItems.Clear()
        RPGGames.Clear()

        LoadImages()

        ' Reset game
        CurrentGameFile = FindGameByPath(Backup)

        ' Fix layer data
        ' FIXME2: above
        For Each Layer In CharacterSelect.CharacterList.CurrentCharacter().Character.Layers
            If Layer.Image IsNot Nothing Then
                Layer.Image = FindImage(Layer.Image.Path)
                Layer.UpdateFlippedImage()
            End If
            Layer.ReloadImagesSubLayers(True)
        Next

        ' Re-draw incase we changed
        Canvas.UpdateDrawing()
    End Sub
End Class