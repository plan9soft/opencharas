Public Class ImagePacker

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Shared Function ContainsThisBox(ByVal Controls As Control.ControlCollection, ByVal Box As RPGPictureBox)
        If Controls Is Nothing Or Box Is Nothing Then Return False

        For Each Control In Controls
            Dim MyBox = CType(Control, RPGPictureBox)
            If MyBox.MyImage Is Box.MyImage Then Return True
        Next
        Return False
    End Function

    Private Sub TreeView1_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        ' Clear current box
        FlowLayoutPanel1.Controls.Clear()

        Dim Node As GameNodeNode = CType(e.Node, GameNodeNode)

        If Node.RPGNode.Images.Count Then
            For Each Image In Node.RPGNode.Images
                ' Add a box
                If ContainsThisBox(FlowLayoutPanel2.Controls, Image.Box) = False Then
                    FlowLayoutPanel1.Controls.Add(Image.Box.CreatePictureBoxCopy())
                End If
            Next
        End If
        TreeView1.SelectedNode = Node
    End Sub

    Private Sub FlowLayoutPanel2_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowLayoutPanel2.DragEnter
        If e.Data.GetDataPresent(GetType(RPGPictureBox)) = True Then
            Dim Box As RPGPictureBox = e.Data.GetData(GetType(RPGPictureBox))

            If ContainsThisBox(FlowLayoutPanel2.Controls, Box) Then
                e.Effect = DragDropEffects.None
            Else
                e.Effect = DragDropEffects.Copy
            End If
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub FlowLayoutPanel2_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowLayoutPanel2.DragDrop
        If e.Data.GetDataPresent(GetType(RPGPictureBox)) = True Then
            Dim Box As RPGPictureBox = e.Data.GetData(GetType(RPGPictureBox))

            FlowLayoutPanel2.Controls.Add(Box)
        End If
    End Sub

    Private Sub FlowLayoutPanel1_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowLayoutPanel1.DragEnter
        If e.Data.GetDataPresent(GetType(RPGPictureBox)) = True Then
            Dim Box As RPGPictureBox = e.Data.GetData(GetType(RPGPictureBox))

            If ContainsThisBox(FlowLayoutPanel1.Controls, Box) Then
                e.Effect = DragDropEffects.None
            Else
                e.Effect = DragDropEffects.Copy
            End If
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub FlowLayoutPanel1_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowLayoutPanel1.DragDrop
        If e.Data.GetDataPresent(GetType(RPGPictureBox)) = True Then
            Dim Box As RPGPictureBox = e.Data.GetData(GetType(RPGPictureBox))
            FlowLayoutPanel1.Controls.Add(Box)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub PackEverythingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PackEverythingToolStripMenuItem.Click
        FlowLayoutPanel2.Controls.Clear()

        For Each Node In Images.RPGNodes
            Node.RecurseNodeAndAddAll()
        Next

        FlowLayoutPanel1.Controls.Clear()
    End Sub

    Private Sub UnpackEverythingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnpackEverythingToolStripMenuItem.Click
        FlowLayoutPanel2.Controls.Clear()
    End Sub

    Private Sub PackAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PackAllToolStripMenuItem.Click
        While FlowLayoutPanel1.Controls.Count <> 0
            FlowLayoutPanel2.Controls.Add(FlowLayoutPanel1.Controls(0))
        End While
    End Sub

    Private Sub UnpackAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnpackAllToolStripMenuItem.Click
        FlowLayoutPanel2.Controls.Clear()
    End Sub
End Class