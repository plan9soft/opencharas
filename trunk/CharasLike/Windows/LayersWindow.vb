Public Class LayersWindow
    ' Loading/Closing
    Private Dockable As Blue.Windows.StickyWindow
    Private Sub LayersWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TreeView1.Nodes.Clear()

        SetSliderColors()

        'If String.IsNullOrEmpty(My.Settings.DockString) Then Dockable.ForceDock(ItemsWindow)
        Dockable = New Blue.Windows.StickyWindow(Me)
    End Sub

    Private Sub LayersWindow_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Canvas.FinalClosing Then Return

        e.Cancel = True
        Me.Hide()
    End Sub

    Private CurrentNode_ As RPGNode
    Public Property CurrentNode() As RPGNode
        Get
            Return CurrentNode_
        End Get
        Set(ByVal value As RPGNode)
            CurrentNode_ = value
        End Set
    End Property

    Public Sub ChangeStatesSub(ByVal Ctrl As Control, ByVal Enabled As Boolean)
        For Each SubCtrl As Control In Ctrl.Controls
            SubCtrl.Enabled = Enabled
            ChangeStatesSub(SubCtrl, Enabled)
        Next
    End Sub

    Public Sub ChangeStates(ByVal Enabled As Boolean)
        For Each Ctrl As Control In SplitContainer1.Panel2.Controls
            Ctrl.Enabled = Enabled
            ChangeStatesSub(Ctrl, Enabled)
        Next

        If Enabled = False Then
            NumericUpDown1.Value = 0
            NumericUpDown2.Value = 0

            TrackBar1.Value = 0
            TrackBar2.Value = 100
            TrackBar3.Value = 100
            TrackBar5.Value = 0
            TrackBar6.Value = 255
            CheckBox3.Checked = False
            SetSliderColors()
            PictureBox1.Image = Nothing
        End If
    End Sub

    Private Sub LayersWindow_Move(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If Canvas.SkipSizeChanged Then Return
    End Sub

    Public Sub SetSliderColors()
        TextBox2.Text = TrackBar1.Value
        TextBox3.Text = TrackBar2.Value / 100
        TextBox4.Text = TrackBar3.Value / 100
        TextBox6.Text = TrackBar5.Value / 100
        TextBox7.Text = TrackBar6.Value
    End Sub

    Public Sub SliderColorsToLayerColors()
        TextBox2.Text = CurrentNode.Layer.GetMatrixValue(0)
        TextBox3.Text = CurrentNode.Layer.GetMatrixValue(1)
        TextBox4.Text = CurrentNode.Layer.GetMatrixValue(2)
        TextBox6.Text = CurrentNode.Layer.GetMatrixValue(3)
        TextBox7.Text = CurrentNode.Layer.Alpha
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        Dim Node As New RPGNode
        Node.Text = "Layer " + (TreeView1.Nodes.Count() + 1).ToString()
        Node.Layer = CharacterSelect.CharacterList.CurrentCharacter.Character.CreateLayer(Node)
        Node.Layer.Node = Node
        TreeView1.Nodes.Add(Node)
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click
        If TreeView1.SelectedNode Is Nothing Then Return

        Dim Node As RPGNode = CType(TreeView1.SelectedNode, RPGNode)

        Dim NewNode As RPGNode = New RPGNode
        NewNode.Text = "Sub-Layer " + (Node.Nodes.Count() + 1).ToString()
        NewNode.Layer = CharacterSelect.CharacterList.CurrentCharacter.Character.CreateLayer(NewNode)
        NewNode.Layer.Node = NewNode
        Node.Nodes.Add(NewNode)
        Node.Expand()
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        Dim Node As RPGNode = CType(TreeView1.SelectedNode, RPGNode)

        If Node IsNot Nothing Then
            CharacterSelect.CharacterList.CurrentCharacter.Character.RemoveLayer(Node.Layer)
            TreeView1.Nodes.Remove(Node)

            If TreeView1.SelectedNode IsNot Nothing Then
                DoSelectNode(TreeView1.SelectedNode)
            Else
                ChangeStates(False)
            End If
            Canvas.UpdateDrawing()
        End If
    End Sub

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click
        Dim Node As RPGNode = CType(TreeView1.SelectedNode, RPGNode)

        Dim NodeCont As TreeNodeCollection
        If Node.Parent Is Nothing Then NodeCont = TreeView1.Nodes Else NodeCont = Node.Parent.Nodes
        If NodeCont.Count = 1 Or NodeCont.IndexOf(Node) = 0 Then Return

        Dim OldIndex As Integer = NodeCont.IndexOf(Node)
        NodeCont.Remove(Node)
        NodeCont.Insert(OldIndex - 1, Node)
        TreeView1.SelectedNode = Node

        CharacterSelect.CharacterList.CurrentCharacter.Character.MoveLayer(Node.Layer, False)
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim Node As RPGNode = CType(TreeView1.SelectedNode, RPGNode)

        Dim NodeCont As TreeNodeCollection
        If Node.Parent Is Nothing Then NodeCont = TreeView1.Nodes Else NodeCont = Node.Parent.Nodes
        If NodeCont.Count = 1 Or NodeCont.IndexOf(Node) = NodeCont.Count - 1 Then Return

        Dim OldIndex As Integer = NodeCont.IndexOf(Node)
        NodeCont.Remove(Node)
        NodeCont.Insert(OldIndex + 1, Node)
        TreeView1.SelectedNode = Node

        CharacterSelect.CharacterList.CurrentCharacter.Character.MoveLayer(Node.Layer, True)
    End Sub

    Private Sub TreeView1_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
    End Sub

    Private SkipChanged As Boolean = False

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        ItemsWindow.Show()
        ItemsWindow.BringToFront()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If CurrentNode IsNot Nothing Then
            CurrentNode.Layer.Offset = New Point(NumericUpDown1.Value, CurrentNode.Layer.Offset.Y)
            Canvas.UpdateDrawing()
        End If
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        If CurrentNode IsNot Nothing Then
            CurrentNode.Layer.Offset = New Point(CurrentNode.Layer.Offset.X, NumericUpDown2.Value)
            Canvas.UpdateDrawing()
        End If
    End Sub

    Private Sub LayersWindow_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
    End Sub

    Public Sub SetTextToTrackBar(ByVal Box As TextBox, ByVal Value As Single, ByVal Index As Integer)
        If SkipChanged Then Return

        SkipChanged = True
        Box.Text = Value
        SkipChanged = False
        CurrentNode.Layer.SetColorMatrix()
        CurrentNode.Layer.SetMatrixValue(Index, Value)
    End Sub

    Public Sub SetTrackBarToText(ByVal TrackB As TrackBar, ByVal Box As TextBox, ByVal LayerIndex As Integer, ByVal IsSingle As Boolean)
        If CurrentNode Is Nothing Then Return
        If SkipChanged Then Return

        Dim Val As Single
        If IsSingle = False Then
            If Integer.TryParse(Box.Text, Val) = False Then
                Box.Text = TrackB.Value
                Return
            End If
        Else
            If Single.TryParse(Box.Text, Val) = False Then
                Box.Text = TrackB.Value / 100
                Return
            End If
        End If

        If IsSingle Then
            Val *= 100
        End If

        If Val > TrackB.Maximum Then
            Val = TrackB.Maximum
        ElseIf Val < TrackB.Minimum Then
            Val = TrackB.Minimum
        End If

        SkipChanged = True
        TrackB.Value = Val
        If IsSingle Then
            Box.Text = TrackB.Value / 100
        Else
            Box.Text = TrackB.Value
        End If
        SkipChanged = False

        CurrentNode.Layer.SetMatrixValue(LayerIndex, Single.Parse(Box.Text))
        CurrentNode.Layer.SetColorMatrix()
    End Sub

    Public Sub SetTrackBarToText(ByVal TrackB As TrackBar, ByVal Box As TextBox, ByVal LayerIndex As Integer)
        SetTrackBarToText(TrackB, Box, LayerIndex, False)
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        SetTextToTrackBar(TextBox2, TrackBar1.Value, 0)
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        SetTextToTrackBar(TextBox3, TrackBar2.Value / 100, 1)
    End Sub

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        SetTextToTrackBar(TextBox4, TrackBar3.Value / 100, 2)
    End Sub

    Private Sub TrackBar5_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar5.Scroll
        SetTextToTrackBar(TextBox6, TrackBar5.Value / 100, 3)
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        SetTrackBarToText(TrackBar1, TextBox2, 0)
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        SetTrackBarToText(TrackBar2, TextBox3, 1, True)
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        SetTrackBarToText(TrackBar3, TextBox4, 2, True)
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        SetTrackBarToText(TrackBar5, TextBox6, 3, True)
    End Sub

    Private Sub TrackBar6_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar6.Scroll
        If CurrentNode Is Nothing Then Return

        TextBox7.Text = TrackBar6.Value
        CurrentNode.Layer.Alpha = TrackBar6.Value
        CurrentNode.Layer.SetColorMatrix()
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        If CurrentNode Is Nothing Then Return

        Dim Val As Integer
        If Integer.TryParse(TextBox7.Text, Val) = False Then
            TextBox7.Text = TrackBar6.Value
            Return
        End If

        If Val > TrackBar6.Maximum Then
            Val = TrackBar6.Maximum
        ElseIf Val < TrackBar6.Minimum Then
            Val = TrackBar6.Minimum
        End If

        TrackBar6.Value = Val
        TextBox7.Text = Val
        CurrentNode.Layer.Alpha = Val
        CurrentNode.Layer.SetColorMatrix()
    End Sub

    Private Sub TreeView1_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView1.AfterLabelEdit
        CType(e.Node, RPGNode).Layer.Name = e.Node.Text
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CurrentNode Is Nothing Then Return

        If CheckBox1.Checked Then
            CurrentNode.Layer.FlipFlags = CurrentNode.Layer.FlipFlags Or EFlipFlag.FlipHorizontal
        Else
            CurrentNode.Layer.FlipFlags = CurrentNode.Layer.FlipFlags And (Not EFlipFlag.FlipHorizontal)
        End If

        CurrentNode.Layer.UpdateFlippedImage()
        Canvas.UpdateDrawing()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CurrentNode Is Nothing Then Return

        If CheckBox2.Checked Then
            CurrentNode.Layer.FlipFlags = CurrentNode.Layer.FlipFlags Or EFlipFlag.FlipVertical
        Else
            CurrentNode.Layer.FlipFlags = CurrentNode.Layer.FlipFlags And (Not EFlipFlag.FlipVertical)
        End If

        CurrentNode.Layer.UpdateFlippedImage()
        Canvas.UpdateDrawing()
    End Sub

    Public Sub DoSelectNode(ByVal Node As TreeNode)
        CurrentNode = Node
        SliderColorsToLayerColors()
        ChangeStates(True)

        'TextBox1.Text = CurrentNode.Layer.Name
        NumericUpDown1.Value = CurrentNode.Layer.Offset.X
        NumericUpDown2.Value = CurrentNode.Layer.Offset.Y

        If CurrentNode.Layer.Image IsNot Nothing Then
            PictureBox1.Image = CurrentNode.Layer.Image.Box.MyImage
        Else
            PictureBox1.Image = Nothing
        End If

        CheckBox1.Checked = False
        CheckBox2.Checked = False

        If (CurrentNode.Layer.FlipFlags And (EFlipFlag.FlipHorizontal Or EFlipFlag.FlipVertical)) = (EFlipFlag.FlipHorizontal Or EFlipFlag.FlipVertical) Then
            CheckBox1.Checked = True
            CheckBox2.Checked = True
        ElseIf (CurrentNode.Layer.FlipFlags And EFlipFlag.FlipHorizontal) = EFlipFlag.FlipHorizontal Then
            CheckBox1.Checked = True
        ElseIf (CurrentNode.Layer.FlipFlags And EFlipFlag.FlipVertical) = EFlipFlag.FlipVertical Then
            CheckBox2.Checked = True
        End If

        CheckBox3.Checked = CurrentNode.Layer.Inverted
        TreeView1.SelectedNode = Node
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Dim Node As RPGNode = CType(e.Node, RPGNode)

        If Node IsNot Nothing Then
            DoSelectNode(Node)
        Else
            ChangeStates(False)
        End If
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        e.Node.BeginEdit()
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CurrentNode Is Nothing Then Return

        CurrentNode.Layer.Inverted = CheckBox3.Checked
        CurrentNode.Layer.SetColorMatrix()
        Canvas.UpdateDrawing()
    End Sub
End Class

Public Class RPGNode
    Inherits TreeNode
    Private Layer_ As RPGLayer
    Public Property Layer() As RPGLayer
        Get
            Return Layer_
        End Get
        Set(ByVal value As RPGLayer)
            Layer_ = value
        End Set
    End Property

    Public Sub RecurseAndLoadSubNodes()
        For Each SubLayer In Layer.SubLayers
            Dim SubNode As New RPGNode
            SubNode.Text = SubLayer.Name
            SubNode.Layer = SubLayer
            SubNode.Layer.Node = SubNode

            Nodes.Add(SubNode)
            SubNode.RecurseAndLoadSubNodes()
        Next
    End Sub
End Class