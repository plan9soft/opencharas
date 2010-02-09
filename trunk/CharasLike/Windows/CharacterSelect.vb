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
        Row.AddCharacter()
        Row.AddCharacter()
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
End Class

Public Class RPGCharacterNode
    Inherits TreeNode
    Public Character As New RPGCharacter
End Class

Public Class RPGCharacterRowNode
    Inherits TreeNode
    Public Row As RPGCharacterNodeList

    Public Sub AddCharacter()
        Dim NewCharacter As New RPGCharacterNode
        NewCharacter.Text = "Character " + (Nodes.Count + 1).ToString()

        Nodes.Add(NewCharacter)
        If Nodes.Count = 1 Then
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