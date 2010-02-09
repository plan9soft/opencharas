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

    Public Sub UpdateCharacterTreeView()

    End Sub

    Public Sub ClearCharacters()
        Character.RPGCharacterFiles.Clear()
        Character.RPGCharacterFiles.Add(New RPGCharacter)

        UpdateCharacterTreeView()
    End Sub
End Class