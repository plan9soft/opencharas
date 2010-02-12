Public Class WindowDocking
    Private DockForm_ As Form ' Form to be docked
    Public Property DockForm() As Form
        Get
            Return DockForm_
        End Get
        Set(ByVal value As Form)
            DockForm_ = value
        End Set
    End Property

    Private FormsDockedToMe_ As New Collections.ObjectModel.Collection(Of WindowDocking)
    Public ReadOnly Property FormsDockedToMe() As Collections.ObjectModel.Collection(Of WindowDocking)
        Get
            Return FormsDockedToMe_
        End Get
    End Property

    Private DockedTo_ As WindowDocking ' Current form we are docked to
    Public Property DockedTo() As WindowDocking
        Get
            Return DockedTo_
        End Get
        Set(ByVal value As WindowDocking)
            DockedTo_ = value
        End Set
    End Property

    Private SkipLocationCheck As Boolean
    Private DockDifference As Point ' The difference in last dock positions

    Public Sub New(ByVal frm As Form)
        DockForm = frm
        WindowDockingHandler.Handler.DockableWindows.Add(Me)
    End Sub

    Public Function IsDocked() As Boolean
        Return DockedTo IsNot Nothing
    End Function

    Public Sub ForceDock(ByVal DockToMe As Form)
        DockedTo = WindowDockingHandler.Handler.DockFromForm(DockToMe)
        DockDifference = DockForm.Location - DockedTo.DockForm.Location

        DockedTo.FormsDockedToMe.Add(Me)
    End Sub

    Shared Function SingleValueWithin(ByVal Value As Integer, ByVal Low As Integer, ByVal High As Integer) As Boolean
        Return (Value < High And Value > Low)
    End Function

    Shared Function AttachCheck(ByVal DockWindowRectangle As Rectangle, ByVal ParentWindowRectangle As Rectangle, ByRef StorePoint As Point) As Boolean
        Dim IsAttached As Boolean = False

        If (((DockWindowRectangle.X + DockWindowRectangle.Width) - ParentWindowRectangle.X) < 15 And ((DockWindowRectangle.X + DockWindowRectangle.Width) - ParentWindowRectangle.X) > -15) And _
            (New Rectangle(DockWindowRectangle.X + DockWindowRectangle.Width - 15, DockWindowRectangle.Y, 30, DockWindowRectangle.Height).IntersectsWith(New Rectangle(ParentWindowRectangle.X - 15, ParentWindowRectangle.Y, 30, ParentWindowRectangle.Height))) Then
            StorePoint = New Point((ParentWindowRectangle.X - DockWindowRectangle.Width), DockWindowRectangle.Y)
            IsAttached = True
        End If

        Dim ParentWindowRightSide As Point = ParentWindowRectangle.Location + New Point(ParentWindowRectangle.Width, 0)

        If ((DockWindowRectangle.X - ParentWindowRightSide.X) < 15 And (DockWindowRectangle.X - ParentWindowRightSide.X) > -15) And _
            (New Rectangle(DockWindowRectangle.X - 15, DockWindowRectangle.Y, 30, DockWindowRectangle.Height).IntersectsWith(New Rectangle(ParentWindowRectangle.X + ParentWindowRectangle.Width - 15, ParentWindowRectangle.Y, 30, ParentWindowRectangle.Height))) Then
            StorePoint = New Point(ParentWindowRightSide.X, DockWindowRectangle.Y)
            IsAttached = True
        End If

        If (((DockWindowRectangle.Y + DockWindowRectangle.Height) - ParentWindowRectangle.Y) < 15 And ((DockWindowRectangle.Y + DockWindowRectangle.Height) - ParentWindowRectangle.Y) > -15) And _
            (New Rectangle(DockWindowRectangle.X, DockWindowRectangle.Y + DockWindowRectangle.Height - 15, DockWindowRectangle.Width, 30).IntersectsWith(New Rectangle(ParentWindowRectangle.X, ParentWindowRectangle.Y - 15, ParentWindowRectangle.Width, 30))) Then
            StorePoint = New Point(DockWindowRectangle.X, ParentWindowRectangle.Y - DockWindowRectangle.Height)
            IsAttached = True
        End If

        If ((DockWindowRectangle.Y - (ParentWindowRectangle.Y + ParentWindowRectangle.Height)) < 15 And (DockWindowRectangle.Y - (ParentWindowRectangle.Y + ParentWindowRectangle.Height)) > -15) And _
            (New Rectangle(DockWindowRectangle.X, DockWindowRectangle.Y - 15, DockWindowRectangle.Width, 30).IntersectsWith(New Rectangle(ParentWindowRectangle.X, ParentWindowRectangle.Y + ParentWindowRectangle.Height - 15, ParentWindowRectangle.Width, 30))) Then
            StorePoint = New Point(DockWindowRectangle.X, ParentWindowRectangle.Y + ParentWindowRectangle.Height)
            IsAttached = True
        End If

        Return IsAttached
    End Function

    Public Function AttachedTo(ByVal frm As Form) As Boolean
        Dim FoundLocation As Point = DockForm.Location
        Dim DockWindowRectangle As New Rectangle(FoundLocation, DockForm.Size)
        Dim ParentWindowRectangle As New Rectangle(frm.Location, frm.Size)
        Dim IsAttached As Boolean = AttachCheck(DockWindowRectangle, ParentWindowRectangle, FoundLocation)

        DockDifference = FoundLocation - ParentWindowRectangle.Location
        DockForm.Location = FoundLocation
        Return IsAttached
    End Function

    Public Function NotDockedSomewhereDownTheChain(ByVal checkFrm As WindowDocking) As Boolean
        If checkFrm Is Nothing Then Return False

        ' Go through each form that is docked to this one 
        For Each Docked In FormsDockedToMe
            If Docked Is checkFrm Then Return True

            If Docked.NotDockedSomewhereDownTheChain(checkFrm) Then Return True
        Next

        Return False
    End Function

    Public Sub CheckDocking()
        If SkipLocationCheck Then Return

        Dim FoundDock As WindowDocking = Nothing
        Dim FoundLocation As Point = DockForm.Location, FoundDifference As Point

        'MoveWithDock
        For Each Dockable In WindowDockingHandler.Handler.DockableWindows
            ' Special case: don't try on a form that Me is docked to
            If FormsDockedToMe.Contains(Dockable) Then Continue For

            Dim DockWindowRectangle As New Rectangle(FoundLocation, DockForm.Size)
            Dim ParentWindowRectangle As New Rectangle(Dockable.DockForm.Location, Dockable.DockForm.Size)

            If AttachCheck(DockWindowRectangle, ParentWindowRectangle, FoundLocation) Then
                FoundDock = Dockable
            End If

            FoundDifference = FoundLocation - ParentWindowRectangle.Location
        Next

        Dim DockedDownChain As Boolean = False
        If FoundDock IsNot Nothing Then DockedDownChain = NotDockedSomewhereDownTheChain(FoundDock)
        ' Did we find a dock?
        ' If we were already docked, we can just switch and go away.
        If DockedTo IsNot Nothing Then AttachedTo(DockedTo.DockForm)
        If (DockedTo Is Nothing And FormsDockedToMe.Count = 0) Or (FoundDock IsNot DockedTo And DockedTo IsNot Nothing) Or
            (FoundDock IsNot Nothing And DockedTo Is Nothing And DockedDownChain = False) Then
            If DockedTo IsNot Nothing Then DockedTo.FormsDockedToMe.Remove(Me)

            DockedTo = FoundDock
            If DockedTo IsNot Nothing Then DockedTo.FormsDockedToMe.Add(Me)
            DockDifference = FoundDifference

            SkipLocationCheck = True
            DockForm.Location = FoundLocation
            SkipLocationCheck = False
        ElseIf FormsDockedToMe.Count > 0 Then
            For Each Docked In FormsDockedToMe
                Docked.MoveWithDock()
            Next
        End If
    End Sub

    Public Sub MoveWithDock()
        SkipLocationCheck = True
        DockForm.Location = DockedTo.DockForm.Location + DockDifference
        For Each Docked In FormsDockedToMe
            Docked.MoveWithDock()
        Next
        SkipLocationCheck = False
    End Sub
End Class

Public Class WindowDockingHandler
    Private DockableWindows_ As New Collections.ObjectModel.Collection(Of WindowDocking)
    Public ReadOnly Property DockableWindows() As Collections.ObjectModel.Collection(Of WindowDocking)
        Get
            Return DockableWindows_
        End Get
    End Property

    Public Shared Handler As New WindowDockingHandler

    Public Function DockFromForm(ByVal frm As Form) As WindowDocking
        For Each Dockable In DockableWindows
            If Dockable.DockForm Is frm Then Return Dockable
        Next
        Return Nothing
    End Function
End Class