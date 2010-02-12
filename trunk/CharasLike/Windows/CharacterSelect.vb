Public Class CharacterSelect

    Private Sub CharacterSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dockable.ForceDock(Canvas)
    End Sub

    Private Dockable As New WindowDocking(Me)
    Private Sub CharacterSelect_Move(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If Me.WindowState <> FormWindowState.Normal Then Return
        If Canvas.SkipSizeChanged Then Return

        Dockable.CheckDocking()
    End Sub

    Public Shared CharacterList As New RPGCharacterNodeList

    Public Sub ClearCharacters()
        TreeView1.Nodes.Clear()
        Dim Row = CharacterList.AddRow()
        Row.AddCharacter(True)
    End Sub

    Public Sub UpdateLayersTreeView_Recurse(ByVal Layer As RPGLayer)
        LayersWindow.TreeView1.Nodes.Add(Layer.Node)
        For Each SubLayer In Layer.SubLayers
            UpdateLayersTreeView_Recurse(SubLayer)
        Next
    End Sub

    Public Sub UpdateLayersTreeView()
        LayersWindow.TreeView1.Nodes.Clear()
        For Each Layer In CharacterList.CurrentCharacter.Character.Layers
            UpdateLayersTreeView_Recurse(Layer)
        Next
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim Node As TreeNode = e.Node

        If Node Is Nothing Then Return

        If Node.GetType() IsNot GetType(RPGCharacterNode) Then
            Return
        End If

        Dim MyNode As RPGCharacterNode = CType(Node, RPGCharacterNode)

        If CharacterList.CurrentCharacter IsNot MyNode Then
            CharacterList.CurrentCharacter = MyNode
            UpdateLayersTreeView()
            Canvas.UpdateDrawing()
        End If
    End Sub

    Public Function GetSelectedRow() As RPGCharacterRowNode
        Dim Node As TreeNode = TreeView1.SelectedNode
        Dim Row As RPGCharacterRowNode

        If Node.GetType() IsNot GetType(RPGCharacterRowNode) Then
            Row = Node.Parent
        Else
            Row = Node
        End If

        Return Row
    End Function

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim Row = GetSelectedRow()
        If Row Is Nothing Then Return
        Row.AddCharacter(True)
    End Sub

    Public Function RowsWithNodes() As Integer
        Dim NumWithNodes As Integer = 0
        For Each Row As TreeNode In TreeView1.Nodes
            If Row.Nodes.Count <> 0 Then NumWithNodes += 1
        Next
        Return NumWithNodes
    End Function

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim Node = CharacterList.CurrentCharacter
        Dim Row = GetSelectedRow()

        If Node Is Nothing Or Row Is Nothing Or TreeView1.SelectedNode.GetType() Is GetType(RPGCharacterRowNode) Then Return

        If RowsWithNodes() = 1 And Row.Nodes.Count = 1 Then
            ' special case: if we have only this character, just clear the data.
            Node.Character = New RPGCharacter
        ElseIf Row.Nodes.Count = 1 Then
            Row.Nodes.Remove(Node)

            ' Is there a row behind us with nodes?
            If Row.Index > 0 Then
                If Row.Parent.Nodes(Row.Index - 1).Nodes.Count > 0 Then
                    ' Good, select it
                    TreeView1.SelectedNode = Row.Parent.Nodes(Row.Index - 1).Nodes(Row.Parent.Nodes(Row.Index - 1).Nodes.Count - 1)
                End If
            ElseIf Row.Index = 0 Then
                ' Ahead of us?
                If Row.Parent.Nodes(Row.Index + 1).Nodes.Count > 0 Then
                    ' Good, select it
                    TreeView1.SelectedNode = Row.Parent.Nodes(Row.Index + 1).Nodes(Row.Parent.Nodes(Row.Index + 1).Nodes.Count - 1)
                End If
            End If
        Else
            Dim NewIndex As Integer = Node.Index
            Row.Nodes.Remove(Node)
            If NewIndex > Row.Nodes.Count - 1 Then NewIndex = Row.Nodes.Count - 1
            TreeView1.SelectedNode = Row.Nodes(NewIndex)
        End If

        If CharacterList.CurrentCharacter IsNot TreeView1.SelectedNode Then
            CharacterList.CurrentCharacter = TreeView1.SelectedNode
            UpdateLayersTreeView()
            Canvas.UpdateDrawing()
        End If
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        e.Node.BeginEdit()
    End Sub

    Public Sub MoveNode(ByVal IndexOffset As Integer)
        Dim Node = CharacterList.CurrentCharacter
        If Node Is Nothing Or TreeView1.SelectedNode.GetType() Is GetType(RPGCharacterRowNode) Then Return

        Dim Row As RPGCharacterRowNode = Node.Parent
  
        ' Can we even move?
        Dim WantedIndex As Integer = Node.Index + IndexOffset
        If ((WantedIndex > Node.Index) And Node.Index = Row.Nodes.Count - 1) Then
            ' Rows in front of us?
            If Row.Index <> TreeView1.Nodes.Count - 1 Then
                ' remove us from this row and push us back to the end
                Row.Nodes.Remove(Node)
                TreeView1.Nodes(Row.Index + 1).Nodes.Insert(0, Node)
                TreeView1.SelectedNode = Node
                Canvas.UpdateDrawing()
            End If
            Return
        End If
        If ((WantedIndex < Node.Index) And Node.Index = 0) Then
            ' We're at the end
            ' Are there any rows behind us?
            If Row.Index > 0 And TreeView1.Nodes.Count > (Row.Index) Then
                ' remove us from this row and push us back to the end
                Row.Nodes.Remove(Node)
                TreeView1.Nodes(Row.Index - 1).Nodes.Add(Node)
                TreeView1.SelectedNode = Node
                Canvas.UpdateDrawing()
            End If
            Return
        End If

        Row.Nodes.Remove(Node)
        Row.Nodes.Insert(WantedIndex, Node)
        TreeView1.SelectedNode = Node
        Canvas.UpdateDrawing()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        MoveNode(1)
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        MoveNode(-1)
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        CharacterList.AddRow()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        If TreeView1.SelectedNode.GetType() Is GetType(RPGCharacterRowNode) Then
            Dim Row As RPGCharacterRowNode = TreeView1.SelectedNode

            If RowsWithNodes() = 1 Then Return
            If Row.Nodes.Count > 0 Then
                If MsgBox("Deleting this row will delete all characters associated with it. Are you sure you want to do this?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then
                    Return
                End If
            End If

            TreeView1.Nodes.Remove(Row)
            Canvas.UpdateDrawing()
        End If
    End Sub

    Public Sub MoveNode(ByVal IndexOffset As Integer)
        Dim Node = CharacterList.CurrentCharacter
        If Node Is Nothing Or TreeView1.SelectedNode.GetType() Is GetType(RPGCharacterRowNode) Then Return

        Dim Row As RPGCharacterRowNode = Node.Parent

        If TreeView1.Nodes.Count = 1 Then Return

        ' Can we even move?
        Dim WantedIndex As Integer = Node.Index + IndexOffset

        Row.Nodes.Remove(Node)
        Row.Nodes.Insert(WantedIndex, Node)
        TreeView1.SelectedNode = Node
        Canvas.UpdateDrawing()
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click

    End Sub
End Class

Public Class RPGCharacterNode
    Inherits TreeNode
    Public Character As New RPGCharacter
End Class

Public Class RPGCharacterRowNode
    Inherits TreeNode
    Public Row As RPGCharacterNodeList

    Public Sub AddCharacter(Optional ByVal SwitchTo As Boolean = False)
        Dim NewCharacter As New RPGCharacterNode
        NewCharacter.Text = "Character " + (Nodes.Count + 1).ToString()

        Nodes.Add(NewCharacter)
        If SwitchTo Then
            CharacterSelect.TreeView1.SelectedNode = NewCharacter
            CharacterSelect.CharacterList.CurrentCharacter = NewCharacter
        End If
    End Sub
End Class

Public Class RPGCharacterNodeList
    Private CurrentSelectedNode_ As RPGCharacterNode
    Public Property CurrentCharacter() As RPGCharacterNode
        Get
            Return CurrentSelectedNode_
        End Get
        Set(ByVal value As RPGCharacterNode)
            CurrentSelectedNode_ = value
        End Set
    End Property
    Private List As New Collections.ObjectModel.Collection(Of RPGCharacterRowNode)

    Public Function AddRow() As RPGCharacterRowNode
        Dim NewRow As New RPGCharacterRowNode
        NewRow.Text = "Row " + (List.Count + 1).ToString()

        CharacterSelect.TreeView1.Nodes.Add(NewRow)
        List.Add(NewRow)

        Return NewRow
    End Function
End Class