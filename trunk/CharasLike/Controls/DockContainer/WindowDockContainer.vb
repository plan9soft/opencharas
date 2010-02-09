
Imports System.ComponentModel
<System.ComponentModel.DefaultEvent("ActiveFormChanged")> Public Class WindowDockContainer
    Private WithEvents pnlTop, pnlBottom, pnlLeft, pnlRight, pnlMidle As New Panel
    Private WithEvents TabTop, TabBottom, TabLeft, TabRight, TabMidle As New TabControl
    Private WithEvents splTop, splBottom, splLeft, splRight As New Splitter
    Private WithEvents curForm As New FrmContainer
    Private WithEvents fShadows As New Form

    Private lstF As New Collections.ObjectModel.Collection(Of FrmContainer)

#Region "Property"
    <EditorBrowsable(EditorBrowsableState.Never), ToolboxItem(False)> _
    Public Property ActiveForm() As FrmContainer
        Get
            Return curForm
        End Get
        Set(ByVal value As FrmContainer)
            With value.TabPage
                If Not IsNothing(.Parent) Then
                    CType(.Parent, TabControl).SelectedTab = value.TabPage
                End If
            End With
        End Set
    End Property

    Private ac As Color = Drawing.SystemColors.ActiveCaption
    Public Property ActiveCaptionBackColor() As Color
        Get
            Return ac
        End Get
        Set(ByVal value As Color)
            ac = value
            SetHeaderFormColor()
        End Set
    End Property

    Private iac As Color = Drawing.SystemColors.InactiveCaption
    Public Property InActiveCaptionBackColor() As Color
        Get
            Return iac
        End Get
        Set(ByVal value As Color)
            iac = value
            SetHeaderFormColor()
        End Set
    End Property

    Private afc As Color = Drawing.SystemColors.ActiveCaptionText
    Public Property ActiveCaptionForeColor() As Color
        Get
            Return afc
        End Get
        Set(ByVal value As Color)
            afc = value
            SetHeaderFormColor()
        End Set
    End Property

    Private iafc As Color = Drawing.SystemColors.InactiveCaption
    Public Property InActiveCaptionForeColor() As Color
        Get
            Return iafc
        End Get
        Set(ByVal value As Color)
            iafc = value
            SetHeaderFormColor()
        End Set
    End Property

#End Region

#Region "Functions and Subs"

    Public Function Forms() As Collections.ObjectModel.Collection(Of FrmContainer)
        Return lstF
    End Function

    Public Sub AddForm(ByVal frm As FrmContainer, Optional ByVal dockStyleWanted As WindowsDockStyle = WindowsDockStyle.Fill) ', Optional ByVal ActiveCaptionBackColor As Color = SystemColors.ActiveCaption, Optional ByVal InActiveCaptionBackColor As Color = SystemColors.InactiveCaption, Optional ByVal ActiveCaptionForeColor As Color = SystemColors.ActiveCaptionText, Optional ByVal InActiveCaptionForeColor As Color = SystemColors.InactiveCaption)
        With Forms()
            With frm
                .ActiveCaptionBackColor = Me.ActiveCaptionBackColor
                .ActiveCaptionForeColor = Me.ActiveCaptionForeColor
                .InActiveCaptionBackColor = Me.InActiveCaptionBackColor
                .InActiveCaptionForeColor = Me.InActiveCaptionForeColor
            End With

            AddHandler frm.Activated, AddressOf frmActivated
            AddHandler frm.GotFocus, AddressOf frmActivated
            AddHandler frm.Enter, AddressOf frmActivated
            AddHandler frm.FormClosing, AddressOf FrmClosing
            AddHandler frm.VisibleChanged, AddressOf FrmVisibleChanged
            AddHandler frm.DockStyleChanged, AddressOf curForm_DockStyleChanged
            AddHandler frm.HeaderMouseUp, AddressOf sender_HeaderMouseUp
            AddHandler frm.HeaderMovingForm, AddressOf curForm_HeaderMovingForm

            frm.Owner = Me.FindForm
            .Add(frm)
            frm.DockStyle = dockStyleWanted
            frm.Visible = True
        End With
    End Sub

    Public Sub AddForm(ByVal frm As Form, Optional ByVal dockStyleWanted As WindowsDockStyle = WindowsDockStyle.Fill)
        Dim fr As New FrmFormContainer
        fr.SetForm(frm)
        Me.AddForm(fr, dockStyleWanted)
        frm.Visible = True
    End Sub

    Private Sub SetHeaderFormColor()
        For Each f As FrmContainer In Me.Forms
            With f
                .ActiveCaptionBackColor = Me.ActiveCaptionBackColor
                .ActiveCaptionForeColor = Me.ActiveCaptionForeColor
                .InActiveCaptionBackColor = Me.InActiveCaptionBackColor
                .InActiveCaptionForeColor = Me.InActiveCaptionForeColor
            End With
        Next
    End Sub

    Private Sub tabGotFocust(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabBottom.SelectedIndexChanged, TabTop.SelectedIndexChanged, TabLeft.SelectedIndexChanged, TabRight.SelectedIndexChanged, TabMidle.SelectedIndexChanged
        With CType(sender, TabControl)
            If .SelectedIndex > -1 Then
                CType(.SelectedTab.Tag, Form).Activate()
                'Me.FindForm.Text = sender.text
            End If
        End With
    End Sub

    Private Sub TabResize(ByVal sender As Object, ByVal e As System.EventArgs)
        With CType(sender.tag, FrmContainer)
            .Size = sender.size
            .Location = New Point(0, 0)
        End With
    End Sub

    Private Sub FrmClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
        Try
            CType(sender.Tag.parent, TabControl).TabPages.Remove(sender.Tag)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmVisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            sender.Tag.visible = sender.visible
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmActivated(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.curForm = sender
        Try
            If sender.Tag.parent IsNot Nothing Then
                CType(sender.Tag.parent, TabControl).SelectedTab = sender.Tag
            End If
        Catch ex As Exception
        End Try
        'Me.FindForm.Text = sender.text
        Me.OnActiveFormChanged(e)
    End Sub

#End Region

#Region "Event"

    Public Event ActiveFormChanged As EventHandler

    Protected Overridable Sub OnActiveFormChanged(ByVal e As System.EventArgs)
        RaiseEvent ActiveFormChanged(Me, e)
    End Sub

#End Region

#Region "Win32 Function"

    Private Sub SetParent(ByVal frm As FrmContainer)
        If frm.DockStyle <> WindowsDockStyle.None Then
            frm.PreferedSize = frm.Size
        End If

        If frm.DockStyle <> WindowsDockStyle.None Then
            frm.Owner = Me.FindForm
            frm.TopLevel = False
            frm.Visible = True
            frm.Dock = DockStyle.Fill
            frm.TabPage.Controls.Add(frm)
            frm.TabPage.Width += 1
        End If

        Select Case frm.DockStyle
            Case Is = WindowsDockStyle.Bottom
                TabBottom.TabPages.Add(frm.TabPage)
                TabBottom.SelectedTab = frm.TabPage
            Case Is = WindowsDockStyle.Fill
                TabMidle.TabPages.Add(frm.TabPage)
                TabMidle.SelectedTab = frm.TabPage
            Case Is = WindowsDockStyle.Left
                TabLeft.TabPages.Add(frm.TabPage)
                TabLeft.SelectedTab = frm.Tag
            Case Is = WindowsDockStyle.Right
                TabRight.TabPages.Add(frm.TabPage)
                TabRight.SelectedTab = frm.TabPage
            Case Is = WindowsDockStyle.None
                If Not IsNothing(frm.TabPage.Parent) Then
                    CType(frm.TabPage.Parent, TabControl).TabPages.Remove(frm.TabPage)
                End If
                frm.TabPage.Controls.Remove(frm)
                frm.Dock = DockStyle.None
                frm.Size = frm.PreferedSize
                frm.TopLevel = True
                frm.Owner = Nothing
                frm.Parent = Nothing
                frm.MdiParent = Nothing
                frm.Owner = Me.FindForm
            Case Is = WindowsDockStyle.Top
                TabTop.TabPages.Add(frm.TabPage)
                TabTop.SelectedTab = frm.TabPage
        End Select
        If Not frm.DockStyle = WindowsDockStyle.None Then
            frm.FormBorderStyle = FormBorderStyle.None
            frm.Location = New Point(0, 0)
            frm.Dock = DockStyle.Fill
            Me.FindForm.Width += 1
            Me.FindForm.Width -= 1
            Me.FindForm.BringToFront()
        Else
            frm.FormBorderStyle = FormBorderStyle.SizableToolWindow
            frm.Visible = True
            frm.Owner = Me.FindForm
        End If
        fShadows.Hide()
    End Sub

#End Region

    Private Sub WindowDockContainer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '-------------- Top
            With TabTop
                .ItemSize = New Size(.ItemSize.Width, .Font.Height + 2)
                .Alignment = TabAlignment.Top
                .Dock = DockStyle.Fill
                .Name = "Top"
                .Visible = False
            End With

            With pnlTop
                .Height = 10
                .Dock = DockStyle.Top
                .Controls.Add(TabTop)
            End With

            With splTop
                .Height = 5
                .Dock = DockStyle.Top
                .Visible = False
            End With

            '-------------- Bottom
            With TabBottom
                .ItemSize = New Size(.ItemSize.Width, .Font.Height + 2)
                .Alignment = TabAlignment.Bottom
                .Dock = DockStyle.Fill
                .Name = "Bottom"
                .Visible = False
            End With

            With pnlBottom
                .Height = 10
                .Dock = DockStyle.Bottom
                .Controls.Add(TabBottom)
            End With

            With splBottom
                .Height = 5
                .Dock = DockStyle.Bottom
                .Visible = False
            End With

            '-------------- Right
            With TabRight
                .ItemSize = New Size(.ItemSize.Width, .Font.Height + 2)
                .Alignment = TabAlignment.Top
                .Dock = DockStyle.Fill
                .Name = "Right"
                .Visible = False
            End With

            With pnlRight
                .Width = 10
                .Dock = DockStyle.Right
                .Controls.Add(TabRight)
            End With

            With splRight
                .Width = 5
                .Dock = DockStyle.Right
                .Visible = False
            End With

            '-------------- Left
            With TabLeft
                .ItemSize = New Size(.ItemSize.Width, .Font.Height + 2)
                .Alignment = TabAlignment.Top
                .Dock = DockStyle.Fill
                .Name = "Left"
                .Visible = False
            End With

            With pnlLeft
                .Width = 10
                .Dock = DockStyle.Left
                .Controls.Add(TabLeft)
            End With

            With splLeft
                .Width = 5
                .Dock = DockStyle.Left
                .Visible = False
            End With

            '-------------- Midle
            With TabMidle
                .ItemSize = New Size(.ItemSize.Width, .Font.Height + 2)
                .Alignment = TabAlignment.Top
                .Dock = DockStyle.Fill
                .Name = "Midle"
                .Visible = False
            End With

            With pnlMidle
                .Height = 10
                .Dock = DockStyle.Fill
                .Controls.Add(TabMidle)
            End With

            '-------------- Me
            With Me
                With .Controls
                    .Add(pnlMidle)
                    '---------------
                    .Add(splLeft)
                    .Add(pnlLeft)
                    '---------------
                    .Add(splRight)
                    .Add(pnlRight)
                    '---------------
                    .Add(splTop)
                    .Add(pnlTop)
                    '---------------
                    .Add(splBottom)
                    .Add(pnlBottom)
                End With

            End With
        Catch ex As Exception
        End Try

        With fShadows
            .Owner = Me.FindForm
            .FormBorderStyle = FormBorderStyle.None
            .ShowInTaskbar = False
            .Opacity = 0.6
            .BackColor = Color.Black
        End With

        top = New Size(100, 100)
        bottom = New Size(100, 100)
        left = New Size(200, 100)
        right = New Size(200, 100)
    End Sub

    Private Sub curForm_DockStyleChanged(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles curForm.DockStyleChanged
        SetParent(sender)
        If CType(sender, Form).TopLevel Then
            sender.TopLevel = True
        End If
    End Sub

    Private Sub sender_HeaderMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) 'Handles sender.HeaderMouseUp
        fShadows.Visible = False
        fShadows.Owner = Me.FindForm
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim p As Point = Me.PointToClient(Windows.Forms.Cursor.Position)

            With pnlTop
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not sender.DockStyle = WindowsDockStyle.Top Then
                        sender.DockStyle = WindowsDockStyle.Top
                    Else
                        sender.Location = New Point(0, 0)
                        sender.Size = sender.Tag.size
                    End If
                    Exit Sub
                End If
            End With

            With pnlBottom
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not sender.DockStyle = WindowsDockStyle.Bottom Then
                        sender.DockStyle = WindowsDockStyle.Bottom
                    Else
                        sender.Location = New Point(0, 0)
                        sender.Size = sender.Tag.size
                    End If
                    Exit Sub
                End If
            End With

            With pnlLeft
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not sender.DockStyle = WindowsDockStyle.Left Then
                        sender.DockStyle = WindowsDockStyle.Left
                    Else
                        sender.Location = New Point(0, 0)
                        sender.Size = sender.Tag.size
                    End If
                    Exit Sub
                End If
            End With

            With pnlRight
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not sender.DockStyle = WindowsDockStyle.Right Then
                        sender.DockStyle = WindowsDockStyle.Right
                    Else
                        sender.Location = New Point(0, 0)
                        sender.Size = sender.Tag.size
                    End If
                    Exit Sub
                End If
            End With

            With pnlMidle
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not sender.DockStyle = WindowsDockStyle.Fill Then
                        sender.DockStyle = WindowsDockStyle.Fill
                    Else
                        sender.Location = New Point(0, 0)
                        sender.Size = sender.Tag.size
                    End If
                    Exit Sub
                End If
            End With

            If Not sender.DockStyle = WindowsDockStyle.None Then
                sender.DockStyle = WindowsDockStyle.None
                'sender.owner = Me.FindForm
                'sender.TopMost = True
            End If
        End If
        'Me.sender_DockStyleChanged(sender, e)
    End Sub

    Private Sub TabBottom_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        For Each t As TabPage In TabTop.TabPages
            Me.TabResize(t, e)
        Next
        pnlTop.Width = Me.Width
        If pnlTop.Visible = False Then
            Me.pnlTop.Height = 10
        End If

        For Each t As TabPage In TabBottom.TabPages
            Me.TabResize(t, e)
        Next
        pnlBottom.Width = Me.Width
        If Me.pnlBottom.Visible = False Then
            Me.pnlBottom.Height = 10
        End If

        For Each t As TabPage In TabLeft.TabPages
            Me.TabResize(t, e)
        Next
        pnlLeft.Height = Me.Height
        If Me.pnlLeft.Visible = False Then
            pnlLeft.Width = 10
        End If

        For Each t As TabPage In TabRight.TabPages
            Me.TabResize(t, e)
        Next
        pnlRight.Height = Me.Height
        If Me.pnlRight.Visible = False Then
            Me.pnlRight.Width = 10
        End If

        For Each t As TabPage In TabMidle.TabPages
            Me.TabResize(t, e)
        Next

        pnlTop.Dock = DockStyle.Top
        pnlBottom.Dock = DockStyle.Bottom
        pnlLeft.Dock = DockStyle.Left
        pnlRight.Dock = DockStyle.Right
    End Sub


    Private Shadows top, bottom, right, left As Size

    Private Sub TabBottom_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles TabBottom.ControlAdded, TabLeft.ControlAdded, TabRight.ControlAdded, TabTop.ControlAdded, TabMidle.ControlAdded
        With CType(sender, TabControl)
            Select Case .Name
                Case Is = TabTop.Name
                    pnlTop.Height = top.Height
                    splTop.Visible = True
                    'pnlTop.Visible = True
                Case Is = TabBottom.Name
                    pnlBottom.Height = bottom.Height
                    splBottom.Visible = True
                    'pnlBottom.Visible = True
                Case Is = TabRight.Name
                    pnlRight.Width = right.Width
                    splRight.Visible = True
                    'pnlRight.Visible = True
                Case Is = TabLeft.Name
                    pnlLeft.Width = left.Width
                    splLeft.Visible = True
                    'pnlLeft.Visible = True
            End Select
            .Visible = True
        End With
    End Sub

    Private Sub TabBottom_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles TabBottom.ControlRemoved, TabLeft.ControlRemoved, TabRight.ControlRemoved, TabTop.ControlRemoved, TabMidle.ControlRemoved
        With CType(sender, TabControl)
            If .TabPages.Count = 1 Then
                Select Case .Name
                    Case Is = TabTop.Name
                        top = pnlTop.Size
                        pnlTop.Height = 2
                        splTop.Visible = False
                        'pnlTop.Visible = False
                    Case Is = TabBottom.Name
                        bottom = pnlBottom.Size
                        pnlBottom.Height = 2
                        splBottom.Visible = False
                        'pnlBottom.Visible = False
                    Case Is = TabRight.Name
                        right = pnlRight.Size
                        pnlRight.Width = 2
                        splRight.Visible = False
                        'pnlRight.Visible = False
                    Case Is = TabLeft.Name
                        left = pnlLeft.Size
                        pnlLeft.Width = 2
                        splLeft.Visible = False
                        'pnlLeft.Visible = False
                End Select
                .Visible = False
            End If
        End With
    End Sub

    Private Sub curForm_HeaderMovingForm(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles curForm.HeaderMovingForm
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim p As Point = Me.PointToClient(Windows.Forms.Cursor.Position)

            With pnlTop
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not curForm.DockStyle = WindowsDockStyle.Top Then
                        fShadows.Location = Me.PointToScreen(t)
                        fShadows.Size = .Size
                        fShadows.Show()
                        fShadows.Invalidate()
                    End If
                    Exit Sub
                End If
            End With

            With pnlBottom
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not curForm.DockStyle = WindowsDockStyle.Bottom Then
                        fShadows.Location = Me.PointToScreen(t)
                        fShadows.Size = .Size
                        fShadows.Show()
                        fShadows.Invalidate()
                    End If
                    Exit Sub
                End If
            End With

            With pnlLeft
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not curForm.DockStyle = WindowsDockStyle.Left Then
                        fShadows.Location = Me.PointToScreen(t)
                        fShadows.Size = .Size
                        fShadows.Show()
                        fShadows.Invalidate()
                    End If
                    Exit Sub
                End If
            End With

            With pnlRight
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not curForm.DockStyle = WindowsDockStyle.Right Then
                        fShadows.Location = Me.PointToScreen(t)
                        fShadows.Size = .Size
                        fShadows.Show()
                        fShadows.Invalidate()
                    End If
                    Exit Sub
                End If
            End With

            With pnlMidle
                Dim t As Point = .Location
                If t.X <= p.X And t.X + .Width > p.X And t.Y <= p.Y And t.Y + .Height > p.Y Then
                    If Not curForm.DockStyle = WindowsDockStyle.Fill Then
                        fShadows.Location = Me.PointToScreen(t)
                        fShadows.Size = .Size
                        fShadows.Show()
                    End If
                    Exit Sub
                End If
            End With

            fShadows.Hide()
        End If
    End Sub

    Private Sub fShadows_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fShadows.VisibleChanged
        If sender.visible = False Then
            Me.FindForm.BringToFront()
        End If
    End Sub
End Class

