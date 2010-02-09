Imports System.ComponentModel

<DefaultEvent("DockStyleChanged")> _
Public Class FrmContainer

    Private evt As New EventArgs

    Private Sub HeaderWindow1_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeaderWindow1.Closing
        Me.Close()
    End Sub

#Region "Moving"

    Private loc, mloc, locBefore As Point
    Private WithEvents frm As New Form

    Private Sub HeaderWindow1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles HeaderWindow1.DoubleClick
        Me.OnHeaderDoubleClick(e)
    End Sub
    Private Sub HeaderWindow1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderWindow1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If Not Me.DockStyle = WindowsDockStyle.None Then
                loc = Me.Parent.PointToScreen(Me.Location)
            Else
                loc = Me.Location
            End If
            mloc = Windows.Forms.Cursor.Position
            locBefore = loc
            Me.VirtuaSize = Me.Size
            'ControlPaint.DrawReversibleFrame(New Rectangle(locBefore, Me.VirtuaSize), Color.Black, FrameStyle.Dashed)
            frm.Size = Me.Size
            Dim bmp As New Bitmap(Me.Width, Me.Height)
            Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
            frm.BackgroundImage = bmp
            frm.Show()
        End If
        Me.OnHeaderMouseDown(e)
    End Sub

    Private Sub HeaderWindow1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderWindow1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            'ControlPaint.DrawReversibleFrame(New Rectangle(locBefore, Me.VirtuaSize), Color.Black, FrameStyle.Dashed)
            With Windows.Forms.Cursor.Position
                locBefore = New Point(loc.X - (mloc.X - .X), loc.Y - (mloc.Y - .Y))
            End With
            frm.Location = locBefore
            'ControlPaint.DrawReversibleFrame(New Rectangle(locBefore, Me.VirtuaSize), Color.Black, FrameStyle.Dashed)
        End If

        Me.OnHeaderMovingFOrm(e)
    End Sub

    Private Sub HeaderWindow1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HeaderWindow1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = locBefore
            'ControlPaint.DrawReversibleFrame(New Rectangle(locBefore, Me.VirtuaSize), Color.Black, FrameStyle.Dashed)
            Me.Invalidate()
            Me.Width += 1
            Me.Width -= 1
        End If
        frm.Hide()
        Me.OnHeaderMouseUp(e)
    End Sub

#End Region

#Region "Public property"

    Private tb As New TabPage
    Public ReadOnly Property TabPage() As TabPage
        Get
            tb.Tag = Me
            Me.Tag = tb
            Return tb
        End Get
    End Property

    Public Shadows Property Text() As String
        Get
            Return Me.HeaderWindow1.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = ""
            Me.HeaderWindow1.Text = value
        End Set
    End Property

    Private prSz As Size
    Public Property PreferedSize() As Size
        Get
            Return prSz
        End Get
        Set(ByVal value As Size)
            prSz = value
            Me.OnPreferedSizeChanged(evt)
        End Set
    End Property

    Private Vsize As Size
    Public Property VirtuaSize() As Size
        Get
            Return Vsize
        End Get
        Set(ByVal value As Size)
            Vsize = value
            Me.OnVirtualSizeChanged(evt)
        End Set
    End Property


    Private dcs As DockStyle = Windows.Forms.DockStyle.None
    Public Property DockStyle() As WindowsDockStyle
        Get
            Return dcs
        End Get
        Set(ByVal value As WindowsDockStyle)
            dcs = value
            Me.OnDockStyleChanged(evt)
        End Set
    End Property

    Private lDock As WindowsDockStyle
    Public Property LastDockStyle() As WindowsDockStyle
        Get
            Return lDock
        End Get
        Set(ByVal value As WindowsDockStyle)
            If Not value = WindowsDockStyle.None Then
                lDock = value
            End If
        End Set
    End Property

    Private ac As Color = Drawing.SystemColors.ActiveCaption
    Public Property ActiveCaptionBackColor() As Color
        Get
            Return ac
        End Get
        Set(ByVal value As Color)
            ac = value
            If Me.DockStyle <> WindowsDockStyle.None Or Me.ContainsFocus Then
                Me.HeaderWindow1.BackColor = Me.ActiveCaptionBackColor
            End If
        End Set
    End Property

    Private iac As Color = Drawing.SystemColors.InactiveCaption
    Public Property InActiveCaptionBackColor() As Color
        Get
            Return iac
        End Get
        Set(ByVal value As Color)
            iac = value

            If Me.DockStyle = WindowsDockStyle.None And Not Me.ContainsFocus Then
                Me.HeaderWindow1.ForeColor = value
            End If

        End Set
    End Property

    Private afc As Color = Drawing.SystemColors.ActiveCaptionText
    Public Property ActiveCaptionForeColor() As Color
        Get
            Return afc
        End Get
        Set(ByVal value As Color)
            afc = value
            If Me.DockStyle <> WindowsDockStyle.None Or Me.ContainsFocus Then
                Me.HeaderWindow1.ForeColor = Me.ActiveCaptionForeColor
            End If
        End Set
    End Property

    Private iafc As Color = Drawing.SystemColors.InactiveCaption
    Public Property InActiveCaptionForeColor() As Color
        Get
            Return iafc
        End Get
        Set(ByVal value As Color)
            iafc = value
            If Me.DockStyle = WindowsDockStyle.None And Not Me.ContainsFocus Then
                Me.HeaderWindow1.ForeColor = value
            End If
        End Set
    End Property

#End Region

#Region "Public Event"

    Public Event PreferedSizeChanged As EventHandler
    Public Event VirtuaSizeChanged As EventHandler
    Public Event DockStyleChanged As EventHandler
    Public Event HeaderMouseDown As MouseEventHandler
    Public Event HeaderMouseUp As MouseEventHandler
    Public Event HeaderDoubleClick As EventHandler
    Public Event HeaderMovingForm As MouseEventHandler

    Protected Overridable Sub OnHeaderMovingForm(ByVal e As MouseEventArgs)
        RaiseEvent HeaderMovingForm(Me, e)
    End Sub

    Protected Overridable Sub OnHeaderMouseDown(ByVal e As MouseEventArgs)
        RaiseEvent HeaderMouseDown(Me, e)
    End Sub

    Protected Overridable Sub OnHeaderMouseUp(ByVal e As MouseEventArgs)
        RaiseEvent HeaderMouseUp(Me, e)
    End Sub

    Protected Overridable Sub OnHeaderDoubleClick(ByVal e As System.EventArgs)
        RaiseEvent HeaderDoubleClick(Me, e)
    End Sub

    Protected Overridable Sub OnDockStyleChanged(ByVal e As System.EventArgs)
        RaiseEvent DockStyleChanged(Me, e)
        Me.HeaderWindow1.BackColor = Me.ActiveCaptionBackColor
        Me.HeaderWindow1.ForeColor = Me.ActiveCaptionForeColor
    End Sub

    Protected Overridable Sub OnPreferedSizeChanged(ByVal e As System.EventArgs)
        RaiseEvent PreferedSizeChanged(Me, e)
    End Sub

    Protected Overridable Sub OnVirtualSizeChanged(ByVal e As System.EventArgs)
        RaiseEvent VirtuaSizeChanged(Me, e)
    End Sub

#End Region

    Private Sub FrmContainer_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated, Me.GotFocus
        Me.HeaderWindow1.BackColor = ActiveCaptionBackColor
        Me.HeaderWindow1.ForeColor = ActiveCaptionForeColor
    End Sub

    Private Sub FrmContainer_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate, Me.LostFocus
        Me.HeaderWindow1.BackColor = InActiveCaptionBackColor
        Me.HeaderWindow1.ForeColor = InActiveCaptionForeColor
        If Me.DockStyle <> WindowsDockStyle.None Then
            Me.HeaderWindow1.BackColor = Me.ActiveCaptionBackColor
            Me.HeaderWindow1.ForeColor = Me.ActiveCaptionForeColor
        End If
    End Sub

    Private Sub FrmContainer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With frm
            .ShowInTaskbar = False
            .FormBorderStyle = Windows.Forms.FormBorderStyle.None
            .Opacity = 0.7
            .Owner = Me

        End With
    End Sub

    Private Sub frm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles frm.Click
        sender.visible = False
    End Sub

    Private Sub FrmContainer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        Me.TabPage.Text = Me.Text
    End Sub
End Class

Public Class DoubleBufferPanel
    Inherits Panel

    Public Sub New()
        MyBase.New()

        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
    End Sub
End Class

<DefaultEvent("Closing")> _
Public Class HeaderWindow
    Inherits UserControl

    Public Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)

        With lblCaption
            .TextAlign = ContentAlignment.MiddleCenter
            .AutoSize = False
            .BackColor = Color.Transparent
            .Dock = DockStyle.Fill
        End With

        With lblClose
            .TextAlign = ContentAlignment.MiddleCenter
            .AutoSize = False
            .BackColor = Color.Transparent
            .Text = "r"
            .Font = New Font("Webdings", 10, FontStyle.Regular)
            .Dock = DockStyle.Right
            .Width = 20
        End With

        Me.Controls.Add(lblClose)
        Me.Controls.Add(lblCaption)
    End Sub

    Private _color1 As Color = Color.Transparent
    Private _color2 As Color = Color.White
    Private _enableParentMoving As Boolean = True
    Private _useGlassStyle As Boolean = True
    Private WithEvents lblCaption, lblClose As New Label

    Public Property EnableParentMoving() As Boolean
        Get
            Return _enableParentMoving
        End Get
        Set(ByVal value As Boolean)
            _enableParentMoving = value
        End Set
    End Property

    Public Property UseGlassStyle() As Boolean
        Get
            Return _useGlassStyle
        End Get
        Set(ByVal value As Boolean)
            _useGlassStyle = value
            Me.Invalidate()
        End Set
    End Property

    Public Property Color1() As Color
        Get
            Return _color1
        End Get
        Set(ByVal value As Color)
            _color1 = value
            ResetBrush()
            Me.Invalidate()
        End Set
    End Property

    Public Property Color2() As Color
        Get
            Return _color2
        End Get
        Set(ByVal value As Color)
            _color2 = value
            ResetBrush()
            Me.Invalidate()
        End Set
    End Property

    Public Shadows Property ForeColor() As Color
        Get
            Return Me.lblCaption.ForeColor
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = MyBase.ForeColor
            Me.lblCaption.ForeColor = value
            Me.lblClose.ForeColor = value
        End Set
    End Property

    Public Shadows Property Text() As String
        Get
            Return lblCaption.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            lblCaption.Text = value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return lblCaption.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            lblCaption.Text = value
        End Set
    End Property

    Private Sub ResetBrush()
        brs = New Drawing2D.LinearGradientBrush(New Rectangle(0, 0, 1, Me.Height), Color1, Color2, Drawing2D.LinearGradientMode.Vertical)
    End Sub

    Private loc, mloc As Point

    Private Sub lblCaption_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCaption.Click
        MyBase.OnClick(e)
    End Sub

    Private Sub lblCaption_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCaption.DoubleClick
        MyBase.OnDoubleClick(e)
    End Sub

    Private Sub lblCaption_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblCaption.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left And EnableParentMoving Then
            mloc = Windows.Forms.Cursor.Position
            loc = Me.FindForm.Location
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Private Sub lblCaption_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCaption.MouseEnter
        MyBase.OnMouseEnter(e)
    End Sub

    Private Sub lblCaption_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCaption.MouseHover
        MyBase.OnMouseHover(e)
    End Sub

    Private Sub lblCaption_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCaption.MouseLeave
        MyBase.OnMouseLeave(e)
    End Sub

    Private Sub lblCaption_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblCaption.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left And EnableParentMoving Then
            With Windows.Forms.Cursor.Position
                Me.Parent.Location = New Point(loc.X - (mloc.X - .X), loc.Y - (mloc.Y - .Y))
            End With
        End If
        MyBase.OnMouseMove(e)
    End Sub

    Private Sub lblCaption_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblCaption.MouseUp
        MyBase.OnMouseUp(e)
    End Sub

    Private Sub lblCaption_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblCaption.MouseWheel
        MyBase.OnMouseWheel(e)
    End Sub

    Private Sub lblClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblClose.Click
        Me.OnClosing(e)
    End Sub

    Private Sub lblClose_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblClose.MouseEnter
        Me.lblClose.ForeColor = Color.Red
    End Sub

    Private Sub lblClose_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblClose.MouseLeave
        Me.lblClose.ForeColor = Me.lblCaption.ForeColor
    End Sub

    Private brs As New Drawing2D.LinearGradientBrush(New Rectangle(0, 0, 1, 15), Color.Transparent, Color.White, Drawing2D.LinearGradientMode.Vertical)
    Private Sub HeaderWindow_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        With e.Graphics
            .SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            .FillRectangle(brs, 1, 1, Me.Width - 2, Me.Height - 2)
            'ControlPaint.DrawGrabHandle(e.Graphics, New Rectangle(1, 1, Me.Height - 2, Me.Height - 2), False, True)
            Using strf As New StringFormat(StringFormatFlags.NoWrap), font As New Font("Times New Roman", 10, FontStyle.Bold)
                ControlPaint.DrawStringDisabled(e.Graphics, "|||", font, Color.Transparent, New Rectangle(1, 1, Me.Height - 2, Me.Height - 2), strf) ', False, True)
            End Using
        End With
    End Sub

    Private Sub HeaderWindow_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ResetBrush()
    End Sub

    Public Event Closing As EventHandler
    Protected Overridable Sub OnClosing(ByVal e As System.EventArgs)
        RaiseEvent Closing(Me, e)
    End Sub

End Class

Public Enum WindowsDockStyle
    None
    Top
    Bottom
    Left
    Right
    Fill
End Enum
