Public Enum EFlipFlag
    FlipNone = 0
    FlipHorizontal = 1
    FlipVertical = 2
End Enum

Public Class RPGLayer
    Private Offset_ As New Point
    Public Property Offset() As Point
        Get
            Return Offset_
        End Get
        Set(ByVal value As Point)
            Offset_ = value
        End Set
    End Property

    Private Image_ As RPGImage
    Public Property Image() As RPGImage
        Get
            Return Image_
        End Get
        Set(ByVal value As RPGImage)
            Image_ = value
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

    Private Matrix_ As New QColorMatrix
    Public Property Matrix() As QColorMatrix
        Get
            Return Matrix_
        End Get
        Set(ByVal value As QColorMatrix)
            Matrix_ = value
        End Set
    End Property

    Private MatrixValues_(3) As Single
    Public Sub SetMatrixValue(ByVal index As Integer, ByVal value As Single)
        MatrixValues_(index) = value
    End Sub
    Public Function GetMatrixValue(ByVal index As Integer)
        Return MatrixValues_(index)
    End Function

    Private Alpha_ As Integer
    Public Property Alpha() As Integer
        Get
            Return Alpha_
        End Get
        Set(ByVal value As Integer)
            Alpha_ = value
        End Set
    End Property

    Private SubLayers_ As New Collections.ObjectModel.Collection(Of RPGLayer)
    Public ReadOnly Property SubLayers() As Collections.ObjectModel.Collection(Of RPGLayer)
        Get
            Return SubLayers_
        End Get
    End Property

    Private Parent_ As RPGLayer
    Public Property Parent() As RPGLayer
        Get
            Return Parent_
        End Get
        Set(ByVal value As RPGLayer)
            Parent_ = value
        End Set
    End Property

    Private OwnedCharacter_ As RPGCharacter
    Public Property OwnedCharacter() As RPGCharacter
        Get
            Return OwnedCharacter_
        End Get
        Set(ByVal value As RPGCharacter)
            OwnedCharacter_ = value
        End Set
    End Property

    Private FlipFlags_ As Integer
    Public Property FlipFlags() As Integer
        Get
            Return FlipFlags_
        End Get
        Set(ByVal value As Integer)
            FlipFlags_ = value
        End Set
    End Property

    Private FlippedImage_ As Bitmap
    Public Property FlippedImage() As Bitmap
        Get
            Return FlippedImage_
        End Get
        Set(ByVal value As Bitmap)
            FlippedImage_ = value
        End Set
    End Property

    Public Sub New()
        Offset = Point.Empty
        Image = Nothing
        Name = ""
        Parent = Nothing
        Matrix.Reset()
        Alpha = 255
        FlipFlags = EFlipFlag.FlipNone
        FlippedImage = Nothing

        SetMatrixValue(0, 0)
        SetMatrixValue(1, 1)
        SetMatrixValue(2, 1)
        SetMatrixValue(3, 0)
    End Sub

    Public Sub SetImage(ByVal Img As RPGPictureBox)
        If Img Is Nothing Then
            Image = Nothing
            LayersWindow.PictureBox1.Image = Nothing
            FlippedImage = Nothing
            Return
        End If

        Image = Img.MainImage
        LayersWindow.PictureBox1.Image = Img.MyImage
        UpdateFlippedImage()
    End Sub

    Public Sub UpdateFlippedImage()
        FlippedImage = Image.RawBitmap.Clone()
        If (FlipFlags And (EFlipFlag.FlipHorizontal Or EFlipFlag.FlipVertical)) = (EFlipFlag.FlipHorizontal Or EFlipFlag.FlipVertical) Then
            FlippedImage.RotateFlip(RotateFlipType.RotateNoneFlipXY)
        ElseIf (FlipFlags And EFlipFlag.FlipHorizontal) = EFlipFlag.FlipHorizontal Then
            FlippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
        ElseIf (FlipFlags And EFlipFlag.FlipVertical) = EFlipFlag.FlipVertical Then
            FlippedImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
        End If
    End Sub

    Public Sub Save(ByVal File As IO.BinaryWriter, ByVal ImagesToo As Boolean)
        File.Write(Offset.X)
        File.Write(Offset.Y)
        File.Write(Name)
        File.Write(Image IsNot Nothing)
        If Image IsNot Nothing Then
            File.Write(Image.Path)

            If ImagesToo Then
                File.Write(Image.OriginalBitmap.Width)
                File.Write(Image.OriginalBitmap.Height)

                Canvas.WriteBitmapToFile(File, Image.OriginalBitmap)
            End If

        End If

        For x As Integer = 0 To 4
            For y As Integer = 0 To 4
                File.Write(Matrix.MyMatrix(x, y))
            Next
        Next
        For i As Integer = 0 To 3
            File.Write(MatrixValues_(i))
        Next
        File.Write(Alpha)
        File.Write(FlipFlags)

        ' Parent is not saved/loaded, it is re-created on run-time
        File.Write(SubLayers.Count)
        For Each SubLayer In SubLayers
            SubLayer.Save(File, ImagesToo)
        Next
    End Sub

    Public Sub Load(ByVal File As IO.BinaryReader, ByVal ImagesToo As Boolean)
        Offset = New Point(File.ReadInt32(), File.ReadInt32())
        Name = File.ReadString()
        If File.ReadBoolean() = True Then
            Dim Img As String = File.ReadString()
            Image = Images.FindImage(Img)
            If Image IsNot Nothing Then FlippedImage = Image.RawBitmap

            If ImagesToo Then
                Dim Width As Integer = File.ReadInt32(), Height As Integer = File.ReadInt32()

                Using BMP As New Bitmap(Width, Height, Imaging.PixelFormat.Format32bppArgb)
                    Canvas.ReadBitmapFromFile(File, Width, Height, BMP)
                    ' Do we have this image?
                    If IO.File.Exists(Img) Then
                        If Images.BitmapCompare(BMP, Images.FindImage(Img).OriginalBitmap) = False Then
                            ' Message the user
                            If MsgBox("The image " + Img + " already exists in your image folder, but yours differs from the one in the file. Would you like to overwrite with this one?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                BMP.Save(Img)
                                Canvas.ImagesAcquired.Add(Img)
                            End If
                        End If
                    ElseIf IO.File.Exists(Img) = False Then
                        IO.Directory.CreateDirectory(Img.Substring(0, Img.LastIndexOf("\")))

                        ' Save the bitmap
                        BMP.Save(Img)
                        Canvas.ImagesAcquired.Add(Img)

                        ' Set our image to a temporary image for later re-loading
                        Image = New RPGImage
                        Image.Path = Img
                    End If
                End Using
            Else
                If Image Is Nothing Then
                    Canvas.PackImagesNotFound.Add(Img)
                End If
            End If
        End If

        For x As Integer = 0 To 4
            For y As Integer = 0 To 4
                Matrix.MyMatrix(x, y) = File.ReadSingle()
            Next
        Next
        For i As Integer = 0 To 3
            MatrixValues_(i) = File.ReadSingle()
        Next
        Alpha = File.ReadInt32()
        FlipFlags = File.ReadInt32()

        Dim SubLayerNum As Integer = File.ReadInt32()
        For i As Integer = 0 To SubLayerNum - 1
            Dim SubLayer As New RPGLayer
            SubLayer.Load(File, ImagesToo)
            SubLayer.Parent = Me
            SubLayers.Add(SubLayer)
        Next
    End Sub

    Public Function GenerateColorMatrix()
        Dim Attr As New Imaging.ImageAttributes
        Attr.SetColorMatrix(Matrix.MyMatrix, Imaging.ColorMatrixFlag.Default, Imaging.ColorAdjustType.Bitmap)
        Return Attr
    End Function

    Public Sub ReloadImagesSubLayers(ByVal Restore As Boolean)
        If Restore = False Then
            For Each SubLayer In SubLayers
                If SubLayer.Image IsNot Nothing Then
                    Dim Img As RPGImage = SubLayer.Image

                    SubLayer.Image = New RPGImage
                    SubLayer.Image.Path = Img.Path
                End If
                SubLayer.ReloadImagesSubLayers(Restore)
            Next
        Else
            For Each SubLayer In SubLayers
                If SubLayer.Image IsNot Nothing Then
                    SubLayer.Image = Images.FindImage(SubLayer.Image.Path)
                    SubLayer.UpdateFlippedImage()
                End If
                SubLayer.ReloadImagesSubLayers(Restore)
            Next
        End If
    End Sub

    Public Sub SetColorMatrix()
        Matrix.Reset()
        Matrix.ScaleColors(Single.Parse(LayersWindow.TextBox4.Text))
        Matrix.TranslateColors(Single.Parse(LayersWindow.TextBox6.Text))
        Matrix.SetSaturation(Single.Parse(LayersWindow.TextBox3.Text))
        Matrix.RotateHue(Integer.Parse(LayersWindow.TextBox2.Text))
        Matrix.ScaleOpacity(Alpha / 255)
        Canvas.UpdateDrawing()
    End Sub

    ' CANVAS RENDERING
    Public Sub RenderCanvasLayer(ByVal gfx As Graphics)
        Dim Zoom As Single = CType(Canvas.ZoomNumber, Single) / 100
        Dim ContentWidth As Integer = CType(Canvas.ToolStripContainer1.ContentPanel.Width / 2, Integer)
        Dim ContentHeight As Integer = CType(Canvas.ToolStripContainer1.ContentPanel.Height / 2, Integer)
        Dim bmp As Bitmap = FlippedImage
        Dim destRect As Rectangle
        Dim srcRect As Rectangle
        Dim Attr As Imaging.ImageAttributes = GenerateColorMatrix()

        ' Frame mode
        If Canvas.FrameAnimationToolStripMenuItem.Checked Then
            Dim BmpWidth As Integer = (((bmp.Width \ Image.GameFile.SheetRows) * Zoom) / 2)
            Dim BmpHeight As Integer = (((bmp.Height \ Image.GameFile.SheetColumns) * Zoom) / 2)
            Dim DrawX As Integer = (ContentWidth - BmpWidth) + (Offset.X * Zoom)
            Dim DrawY As Integer = (ContentHeight - BmpHeight) + (Offset.Y * Zoom)
            Dim Frame As Integer

            If Canvas.AnimationIndex = 0 Then
                Frame = 0
            Else
                Frame = Images.CurrentGameFile.Animations(Canvas.AnimationIndex - 1).GetFrame(Canvas.AnimationFrameIndex)
            End If

            destRect = New Rectangle(DrawX, DrawY, (bmp.Width \ Image.GameFile.SheetRows) * Zoom, (bmp.Height \ Image.GameFile.SheetColumns) * Zoom)
            srcRect = Character.GetRectangleForBitmapFrame(bmp, Frame, Image.GameFile.SheetRows, Image.GameFile.SheetColumns)
            gfx.DrawImage(bmp, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, Attr)
        Else ' Sheet mode
            Dim BmpWidth As Integer = ((bmp.Width * Zoom) / 2)
            Dim BmpHeight As Integer = ((bmp.Height * Zoom) / 2)
            Dim DrawX As Integer = (ContentWidth - BmpWidth) + (Offset.X * Zoom) + Canvas.CanvasCamera.X
            Dim DrawY As Integer = (ContentHeight - BmpHeight) + (Offset.Y * Zoom) + Canvas.CanvasCamera.Y

            destRect = New Rectangle(New Point(DrawX, DrawY), New Size(bmp.Width * Zoom, bmp.Height * Zoom))
            srcRect = New Rectangle(0, 0, bmp.Width, bmp.Height)

            gfx.DrawImage(bmp, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, Attr)
        End If
    End Sub

    Public Sub DrawCanvasLayers(ByVal e As Graphics)
        If Image IsNot Nothing Then RenderCanvasLayer(e)

        For Each SubLayer In SubLayers
            SubLayer.DrawCanvasLayers(e)
        Next
    End Sub

    Public Sub RenderLayerToGraphics(ByVal gfx As Graphics)
        Dim WImg = OwnedCharacter.SizeOfOutput
        Dim ContentSize As New Size(WImg.Width / 2, WImg.Height / 2)
        Dim BmpSize As New Size(FlippedImage.Width / 2, FlippedImage.Height / 2)
        Dim DrawLocation As Point = (New Point(ContentSize - BmpSize)) + Offset

        gfx.DrawImage(FlippedImage, New Rectangle(DrawLocation, FlippedImage.Size), 0, 0, FlippedImage.Width, FlippedImage.Height, GraphicsUnit.Pixel, GenerateColorMatrix())
    End Sub

    Public Sub RenderLayerToGraphicsRecursion(ByVal e As Graphics)
        If Image IsNot Nothing Then RenderLayerToGraphics(e)
        For Each SubLayer In SubLayers
            SubLayer.RenderLayerToGraphicsRecursion(e)
        Next
    End Sub
End Class

Public Class RPGCharacter
    Private Name_ As String ' Unique ID for character file (for multi-sheet)
    Public Property Name() As String
        Get
            Return Name_
        End Get
        Set(ByVal value As String)
            Name_ = value
        End Set
    End Property

    Private LastSavePath_ As String ' Last place we saved this specific character file
    Public Property LastSavePath() As String
        Get
            Return LastSavePath_
        End Get
        Set(ByVal value As String)
            LastSavePath_ = value
        End Set
    End Property

    Private Layers_ As New Collections.ObjectModel.Collection(Of RPGLayer)
    Public ReadOnly Property Layers() As Collections.ObjectModel.Collection(Of RPGLayer)
        Get
            Return Layers_
        End Get
    End Property

    Public Sub Save(ByVal Writer As IO.BinaryWriter, ByVal ImagesToo As Boolean)
        Writer.Write(Layers.Count)
        For Each Layer In Layers
            Layer.Save(Writer, ImagesToo)
        Next
    End Sub

    Public Sub Load(ByVal Reader As IO.BinaryReader, ByVal ImagesToo As Boolean)
        Dim Count As Integer = Reader.ReadInt32()

        For i As Integer = 0 To Count - 1
            Dim NewLayer As New RPGLayer
            NewLayer.Load(Reader, ImagesToo)
            Layers.Add(NewLayer)
        Next

        For Each Layer In Layers
            Dim Node As New RPGNode
            Node.Layer = Layer
            Node.Text = Layer.Name

            LayersWindow.TreeView1.Nodes.Add(Node)

            Node.RecurseAndLoadSubNodes()
        Next
    End Sub

    Private Function SizeRecursion(ByVal LayerCollection As Collections.ObjectModel.Collection(Of RPGLayer), ByVal CurrentSize As Size)
        For Each Layer In LayerCollection
            CurrentSize = SizeRecursion(Layer.SubLayers, CurrentSize)
            If Layer.FlippedImage Is Nothing Then Continue For
            If Layer.FlippedImage.Size.Width > CurrentSize.Width And Layer.FlippedImage.Size.Height > CurrentSize.Height Then CurrentSize = Layer.FlippedImage.Size
        Next

        Return CurrentSize
    End Function

    Public ReadOnly Property SizeOfOutput() As Size
        Get
            Dim Width As Integer, Height As Integer
            If Layers.Count = 0 Then
                Width = 1
                Height = 1
            Else
                Dim LargestSize As New Size(1, 1)
                Return SizeRecursion(Layers, LargestSize)
            End If
            Return New Size(Width, Height)
        End Get
    End Property

    Public Function CreateLayer(ByVal Node As TreeNode)
        Dim Layer As New RPGLayer

        If Node IsNot Nothing And Node.Parent IsNot Nothing And (Node.Parent IsNot LayersWindow.SplitContainer1.Panel1) Then
            ' Use our parent layer
            Layer.Parent = CType(Node.Parent, RPGNode).Layer
            Layer.OwnedCharacter = Layer.Parent.OwnedCharacter
            Layer.Parent.SubLayers.Add(Layer)
        Else
            Layer.Parent = Nothing
            Layer.OwnedCharacter = Me
            Layers.Add(Layer)
        End If

        Layer.Name = Node.Text
        Return Layer
    End Function

    Public Sub RemoveLayer(ByVal Layer As RPGLayer)
        ' Do we have a parent?
        If Layer.Parent IsNot Nothing Then
            ' Remove us from sublayers
            Layer.Parent.SubLayers.Remove(Layer)
        Else
            Layers.Remove(Layer)
        End If
    End Sub

    Public Sub MoveLayer(ByVal Layer As RPGLayer, ByVal Down As Boolean)
        If Layer.Parent IsNot Nothing Then
            Dim OldIndex As Integer = Layer.Parent.SubLayers.IndexOf(Layer)
            Layer.Parent.SubLayers.Remove(Layer)
            If (Down) Then
                Layer.Parent.SubLayers.Insert(OldIndex + 1, Layer)
            Else
                Layer.Parent.SubLayers.Insert(OldIndex - 1, Layer)
            End If
        Else
            Dim OldIndex As Integer = Character.RPGCharacterFiles(0).Layers.IndexOf(Layer)
            Layers.Remove(Layer)
            If (Down) Then
                Layers.Insert(OldIndex + 1, Layer)
            Else
                Layers.Insert(OldIndex - 1, Layer)
            End If
        End If
        Canvas.UpdateDrawing()
    End Sub
End Class

Public Class Character
    Public Shared RPGCharacterFiles As New Collections.ObjectModel.Collection(Of RPGCharacter)

    Public Shared Function GetRectangleForBitmapFrame(ByVal bmp As Image, ByVal FrameNumber As Integer, ByVal NumRows As Integer, ByVal NumColumns As Integer)
        If bmp Is Nothing Then Return New Rectangle(0, 0, 0, 0)
        Dim SpriteWidth As Integer = bmp.Width \ NumRows, SpriteHeight As Integer = bmp.Height \ NumColumns
        Dim FramesPerRow As Integer = bmp.Width \ SpriteWidth

        Return New Rectangle(((FrameNumber Mod FramesPerRow) * SpriteWidth), ((FrameNumber \ FramesPerRow) * SpriteHeight), SpriteWidth, SpriteHeight)
    End Function
End Class
