''///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'/////
'// StickyWindows
'// 
'// Copyright (c) 2004 Corneliu I. Tusnea
'//
'// This software is provided 'as-is', without any express or implied warranty.
'// In no event will the author be held liable for any damages arising from 
'// the use of this software.
'// Permission to use, copy, modify, distribute and sell this software for any 
'// purpose is hereby granted without fee, provided that the above copyright 
'// notice appear in all copies and that both that copyright notice and this 
'// permission notice appear in supporting documentation.
'//
'// Notice: Check CodeProject for details about using this class
'//
''///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'///'////

Imports System
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections
Imports CharasLike.Blue.Private.Win32Imports

#Region "Win32Imports"
Namespace Blue.Private.Win32Imports
    '/// <summary>
    '/// Win32 is just a placeholder for some Win32 imported definitions
    '/// </summary>
    Public Class Win32
        <DllImport("user32.dll", CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
        Public Shared Function GetAsyncKeyState(ByVal vKey As Integer) As Short
        End Function
        <DllImport("user32.dll", CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
        Public Shared Function GetDesktopWindow() As IntPtr
        End Function

        '/// <summary>
        '/// VK is just a placeholder for VK (VirtualKey) general definitions
        '/// </summary>
        Public Class VK
            Public Const VK_SHIFT = &H10
            Public Const VK_CONTROL = &H11
            Public Const VK_MENU = &H12
            Public Const VK_ESCAPE = &H1B

            Public Shared Function IsKeyPressed(ByVal KeyCode As Integer) As Boolean
                Return (GetAsyncKeyState(KeyCode) And &H800) = 0
            End Function
        End Class

        '/// <summary>
        '/// WM is just a placeholder class for WM (WindowMessage) definitions
        '/// </summary>
        Public Class WM
            Public Const WM_MOUSEMOVE = &H200
            Public Const WM_NCMOUSEMOVE = &HA0
            Public Const WM_NCLBUTTONDOWN = &HA1
            Public Const WM_NCLBUTTONUP = &HA2
            Public Const WM_NCLBUTTONDBLCLK = &HA3
            Public Const WM_LBUTTONDOWN = &H201
            Public Const WM_LBUTTONUP = &H202
            Public Const WM_KEYDOWN = &H100
        End Class

        '/// <summary>
        '/// HT is just a placeholder for HT (HitTest) definitions
        '/// </summary>
        Public Class HT
            Public Const HTERROR = (-2)
            Public Const HTTRANSPARENT = (-1)
            Public Const HTNOWHERE = 0
            Public Const HTCLIENT = 1
            Public Const HTCAPTION = 2
            Public Const HTSYSMENU = 3
            Public Const HTGROWBOX = 4
            Public Const HTSIZE = HTGROWBOX
            Public Const HTMENU = 5
            Public Const HTHSCROLL = 6
            Public Const HTVSCROLL = 7
            Public Const HTMINBUTTON = 8
            Public Const HTMAXBUTTON = 9
            Public Const HTLEFT = 10
            Public Const HTRIGHT = 11
            Public Const HTTOP = 12
            Public Const HTTOPLEFT = 13
            Public Const HTTOPRIGHT = 14
            Public Const HTBOTTOM = 15
            Public Const HTBOTTOMLEFT = 16
            Public Const HTBOTTOMRIGHT = 17
            Public Const HTBORDER = 18
            Public Const HTREDUCE = HTMINBUTTON
            Public Const HTZOOM = HTMAXBUTTON
            Public Const HTSIZEFIRST = HTLEFT
            Public Const HTSIZELAST = HTBOTTOMRIGHT

            Public Const HTOBJECT = 19
            Public Const HTCLOSE = 20
            Public Const HTHELP = 21
        End Class

        Public Class WordConverter
            <StructLayout(LayoutKind.Explicit)> _
            Private Structure DWord
                <FieldOffset(0)> Public LongValue As Integer
                <FieldOffset(0)> Public LoWord As Short
                <FieldOffset(2)> Public HiWord As Short
            End Structure

            Private Shared m_DWord As DWord

            Public Shared Function MakeLong( _
                ByVal LoWord As Short, _
                ByVal HiWord As Short _
            ) As Integer
                m_DWord.LoWord = LoWord
                m_DWord.HiWord = HiWord
                Return m_DWord.LongValue
            End Function

            Public Shared Function MakeLong( _
                ByVal LoWord As Integer, _
                ByVal HiWord As Integer _
            ) As Integer
                Return MakeLong(CShort(LoWord), CShort(HiWord))
            End Function

            Public Shared Function LoWord(ByVal LongValue As Integer) As Short
                m_DWord.LongValue = LongValue
                Return m_DWord.LoWord
            End Function

            Public Shared Function HiWord(ByVal LongValue As Integer) As Short
                m_DWord.LongValue = LongValue
                Return m_DWord.HiWord
            End Function
        End Class
    End Class
End Namespace
#End Region

Namespace Blue.Windows
    '/// <summary>
    '/// A windows that Sticks to other windows of the same type when moved or resized.
    '/// You get a nice way of organizing multiple top-level windows.
    '/// Quite similar with WinAmp 2.x style of sticking the windows
    '/// </summary>
    Public Class StickyWindow
        Inherits System.Windows.Forms.NativeWindow
        '/// <summary>
        '/// Global List of registered StickyWindows
        '/// </summary>
        Private Shared GlobalStickyWindows As New ArrayList()

#Region "ResizeDir"
        Private Enum ResizeDir
            Top = 2
            Bottom = 4
            Left = 8
            Right = 16
        End Enum
#End Region

#Region "Message Processor"
        '// Internal Message Processor
        Delegate Function ProcessMessage(ByRef m As Message) As Boolean
        Private MessageProcessor As ProcessMessage

        '// Messages processors based on type
        Private DefaultMessageProcessor As ProcessMessage
        Private MoveMessageProcessor As ProcessMessage
        Private ResizeMessageProcessor As ProcessMessage
#End Region

#Region "Internal properties"
        '// Move stuff
        Private movingForm As Boolean
        Private formOffsetPoint As Point '// calculated offset rect to be added !! (min distances in all directions!!)
        Private offsetPoint As Point '// primary offset

        '// Resize stuff
        Private resizingForm As Boolean
        Private resizeDirection As ResizeDir
        Private formOffsetRect As Rectangle '// calculated rect to fix the size
        Private mousePoint As Point '// mouse position

        '// General Stuff
        Private originalForm As Form '// the form
        Public ReadOnly Property GetOriginalForm() As Form
            Get
                Return originalForm
            End Get
        End Property
        Private formRect As Rectangle '// form bounds
        Private formOriginalRect As Rectangle '// bounds before last operation started
#End Region

        '// public properties
        Private Shared _stickGap As Integer = 15        '// distance to stick
        Private _stickOnResize As Boolean
        Private _stickOnMove As Boolean
        Private _stickToScreen As Boolean
        Private _stickToOther As Boolean
        Public _stuckTo As StickyWindow
        Public _stuckToMe As New ArrayList()
        Public _dockDifference As Point

#Region "Public operations and properties"
        '/// <summary>
        '/// Distance in pixels betwen two forms or a form and the screen where the sticking should start
        '/// Default value = 20
        '/// </summary>
        Public Property StickGap() As Integer
            Get
                Return _stickGap
            End Get
            Set(ByVal value As Integer)
                _stickGap = value
            End Set
        End Property
                '/// <summary>
                '/// Allow the form to stick while resizing
                '/// Default value = true
                '/// </summary>
        Public Property StickOnResize() As Boolean
            Get
                Return _stickOnResize
            End Get
            Set(ByVal value As Boolean)
                _stickOnResize = value
            End Set
        End Property

        '/// <summary>
        '/// Allow the form to stick while moving
        '/// Default value = true
        '/// </summary>
        Public Property StickOnMove() As Boolean
            Get
                Return _stickOnMove
            End Get
            Set(ByVal value As Boolean)
                _stickOnMove = value
            End Set
        End Property

        '/// <summary>
        '/// Allow sticking to Screen Margins
        '/// Default value = true
        '/// </summary>
        Public Property StickToScreen() As Boolean
            Get
                Return _stickToScreen
            End Get
            Set(ByVal value As Boolean)
                _stickToScreen = value
            End Set
        End Property

        '/// <summary>
        '/// Allow sticking to other StickWindows
        '/// Default value = true
        '/// </summary>
        Public Property StickToOther() As Boolean
            Get
                Return _stickToOther
            End Get
            Set(ByVal value As Boolean)
                _stickToOther = value
            End Set
        End Property

        '/// <summary>
        '/// Register a new form as an external reference form.
        '/// All Sticky windows will try to stick to the external references
        '/// Use this to register your MainFrame so the child windows try to stick to it, when your MainFrame is NOT a sticky window
        '/// </summary>
        '/// <param name="frmExternal">External window that will be used as reference</param>
        Public Shared Sub RegisterExternalReferenceForm(ByVal frmExternal As Form)
            GlobalStickyWindows.Add(frmExternal)
        End Sub

        '/// <summary>
        '/// Unregister a form from the external references.
        '/// <see cref="RegisterExternalReferenceForm"/>
        '/// </summary>
        '/// <param name="frmExternal">External window that will was used as reference</param>
        Public Shared Sub UnregisterExternalReferenceForm(ByVal frmExternal As Form)
            GlobalStickyWindows.Remove(frmExternal)
        End Sub

#End Region

#Region "StickyWindow Constructor"
        '/// <summary>
        '/// Make the form Sticky
        '/// </summary>
        '/// <param name="form">Form to be made sticky</param>
        Public Sub New(ByVal form As Form)
            resizingForm = False
            movingForm = False

            originalForm = form

            formRect = Rectangle.Empty
            formOffsetRect = Rectangle.Empty

            formOffsetPoint = Point.Empty
            offsetPoint = Point.Empty
            mousePoint = Point.Empty

            stickOnMove = True
            stickOnResize = True
            stickToScreen = True
            stickToOther = True

            DefaultMessageProcessor = New ProcessMessage(AddressOf DefaultMsgProcessor)
            MoveMessageProcessor = New ProcessMessage(AddressOf MoveMsgProcessor)
            ResizeMessageProcessor = New ProcessMessage(AddressOf ResizeMsgProcessor)
            MessageProcessor = DefaultMessageProcessor

            AssignHandle(originalForm.Handle)
        End Sub
#End Region

#Region "OnHandleChange"
        <System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")>
        Protected Overrides Sub OnHandleChange()
            If (CType(Handle, Integer) <> 0) Then
                GlobalStickyWindows.Add(Me)
            Else
                GlobalStickyWindows.Remove(Me)
            End If
        End Sub
#End Region

#Region "WndProc"
        <System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")>
        Protected Overrides Sub WndProc(ByRef m As Message)
            If (MessageProcessor(m) = False) Then MyBase.WndProc(m)
        End Sub
#End Region

#Region "DefaultMsgProcessor"
        '/// <summary>
        '/// Processes messages during normal operations (while the form is not resized or moved)
        '/// </summary>
        '/// <param name="m"></param>
        '/// <returns></returns>
        Private Function DefaultMsgProcessor(ByRef m As Message) As Boolean
            Select (m.Msg)
                Case Is = Win32.WM.WM_NCLBUTTONDOWN
                    originalForm.Activate()
                    mousePoint.X = CType(Win32.WordConverter.LoWord(m.LParam.ToInt32()), Short)
                    mousePoint.Y = CType(Win32.WordConverter.HiWord(m.LParam.ToInt32()), Short)
                    If (OnNCLButtonDown(CType(m.WParam, Integer), mousePoint)) Then
                        '//m.Result = new IntPtr ( (resizingForm || movingForm) ? 1 : 0 ) 
                        m.Result = CType((If(resizingForm OrElse movingForm, 1, 0)), IntPtr)
                        Return True
                    End If
            End Select

            Return False
        End Function
#End Region

#Region "OnNCLButtonDown"
        '/// <summary>
        '/// Checks where the click was in the NC area and starts move or resize operation
        '/// </summary>
        '/// <param name="iHitTest"></param>
        '/// <param name="point"></param>
        '/// <returns></returns>
        Private Function OnNCLButtonDown(ByVal HitTest As Integer, ByVal point As Point) As Boolean
            Dim rParent As Rectangle = originalForm.Bounds
            offsetPoint = point

            Select Case (HitTest)
                '// request for move
                Case Is = Win32.HT.HTCAPTION
                    If (StickOnMove) Then
                        offsetPoint.Offset(-rParent.Left, -rParent.Top)
                        StartMove()
                        Return True
                    Else
                        Return False    '// leave default processing
                    End If
                    '// requests for resize
                Case Win32.HT.HTTOPLEFT
                    Return StartResize(ResizeDir.Top Or ResizeDir.Left)
                Case Win32.HT.HTTOP
                    Return StartResize(ResizeDir.Top)
                Case Win32.HT.HTTOPRIGHT
                    Return StartResize(ResizeDir.Top Or ResizeDir.Right)
                Case Win32.HT.HTRIGHT
                    Return StartResize(ResizeDir.Right)
                Case Win32.HT.HTBOTTOMRIGHT
                    Return StartResize(ResizeDir.Bottom Or ResizeDir.Right)
                Case Win32.HT.HTBOTTOM
                    Return StartResize(ResizeDir.Bottom)
                Case Win32.HT.HTBOTTOMLEFT
                    Return StartResize(ResizeDir.Bottom Or ResizeDir.Left)
                Case Win32.HT.HTLEFT
                    Return StartResize(ResizeDir.Left)
            End Select
            Return False
        End Function
#End Region

#Region "ResizeOperations"
        Private Function StartResize(ByVal resDir As ResizeDir) As Boolean
            If (stickOnResize) Then
                resizeDirection = resDir
                formRect = originalForm.Bounds
                formOriginalRect = originalForm.Bounds  '// save the old bounds

                If (originalForm.Capture = False) Then    '// start capturing messages
                    originalForm.Capture = True

                    MessageProcessor = ResizeMessageProcessor

                    Return True     '// catch the message
                Else
                    Return False    '// leave default processing !
                End If
            End If
        End Function

        Private Function ResizeMsgProcessor(ByRef m As Message) As Boolean
            If (originalForm.Capture = False) Then
                Cancel()
                Return False
            End If

            Select (m.Msg)
                Case Win32.WM.WM_LBUTTONUP
                    '// ok, resize finished !!!
                    EndResize()
                Case Win32.WM.WM_MOUSEMOVE
                    mousePoint.X = CType(Win32.WordConverter.LoWord(m.LParam.ToInt32()), Short)
                    mousePoint.Y = CType(Win32.WordConverter.HiWord(m.LParam.ToInt32()), Short)
                    Resize(mousePoint)
                Case Win32.WM.WM_KEYDOWN
                    If (CType(m.WParam, Integer) = Win32.VK.VK_ESCAPE) Then
                        originalForm.Bounds = formOriginalRect  '// set back old size
                        Cancel()
                    End If
            End Select

            Return False
        End Function
        Private Sub EndResize()
            Cancel()
        End Sub
#End Region

#Region "Resize Computing"
        Private Sub Resize(ByVal p As Point)
            p = originalForm.PointToScreen(p)
            Dim activeScr As Screen = Screen.FromPoint(p)
            formRect = originalForm.Bounds

            Dim iRight As Integer = formRect.Right
            Dim iBottom As Integer = formRect.Bottom

            '// no normalize required
            '// first strech the window to the new position
            If ((resizeDirection And ResizeDir.Left) = ResizeDir.Left) Then
                formRect.Width = formRect.X - p.X + formRect.Width
                formRect.X = iRight - formRect.Width
            End If

            If ((resizeDirection And ResizeDir.Right) = ResizeDir.Right) Then
                formRect.Width = p.X - formRect.Left
            End If

            If ((resizeDirection And ResizeDir.Top) = ResizeDir.Top) Then
                formRect.Height = formRect.Height - p.Y + formRect.Top
                formRect.Y = iBottom - formRect.Height
            End If
            If ((resizeDirection And ResizeDir.Bottom) = ResizeDir.Bottom) Then
                formRect.Height = p.Y - formRect.Top
            End If

            '// this is the real new position
            '// now, try to snap it to different objects (first to screen)

            '// CARE !!! We use "Width" and "Height" as Bottom & Right!! (C++ style)
            '//formOffsetRect = new Rectangle ( stickGap + 1, stickGap + 1, 0, 0 ) 
            formOffsetRect.X = stickGap + 1
            formOffsetRect.Y = stickGap + 1
            formOffsetRect.Height = 0
            formOffsetRect.Width = 0

            If (stickToScreen) Then
                Resize_Stick(activeScr.WorkingArea, False)
            End If

            If (stickToOther) Then
                '// now try to stick to other forms
                For Each sw As StickyWindow In GlobalStickyWindows
                    If (sw.originalForm IsNot Me.originalForm) Then Resize_Stick(sw.originalForm.Bounds, True)
                Next
            End If

                    '// Fix (clear) the values that were not updated to stick
            If (formOffsetRect.X = stickGap + 1) Then
                formOffsetRect.X = 0
            End If
            If (formOffsetRect.Width = stickGap + 1) Then
                formOffsetRect.Width = 0
            End If
            If (formOffsetRect.Y = stickGap + 1) Then
                formOffsetRect.Y = 0
            End If
            If (formOffsetRect.Height = stickGap + 1) Then
                formOffsetRect.Height = 0
            End If

            '// compute the new form size
            If ((resizeDirection And ResizeDir.Left) = ResizeDir.Left) Then    '// left resize requires special handling of X & Width acording to MinSize and MinWindowTrackSize
                Dim iNewWidth As Integer = formRect.Width + formOffsetRect.Width + formOffsetRect.X

                If (originalForm.MaximumSize.Width <> 0) Then iNewWidth = Math.Min(iNewWidth, originalForm.MaximumSize.Width)

                iNewWidth = Math.Min(iNewWidth, SystemInformation.MaxWindowTrackSize.Width)
                iNewWidth = Math.Max(iNewWidth, originalForm.MinimumSize.Width)
                iNewWidth = Math.Max(iNewWidth, SystemInformation.MinWindowTrackSize.Width)

                formRect.X = iRight - iNewWidth
                formRect.Width = iNewWidth
            Else    '// other resizes
                formRect.Width += formOffsetRect.Width + formOffsetRect.X
            End If

            If ((resizeDirection And ResizeDir.Top) = ResizeDir.Top) Then
                Dim iNewHeight As Integer = formRect.Height + formOffsetRect.Height + formOffsetRect.Y

                If (originalForm.MaximumSize.Height <> 0) Then iNewHeight = Math.Min(iNewHeight, originalForm.MaximumSize.Height)

                iNewHeight = Math.Min(iNewHeight, SystemInformation.MaxWindowTrackSize.Height)
                iNewHeight = Math.Max(iNewHeight, originalForm.MinimumSize.Height)
                iNewHeight = Math.Max(iNewHeight, SystemInformation.MinWindowTrackSize.Height)

                formRect.Y = iBottom - iNewHeight
                formRect.Height = iNewHeight
            Else    '// all other resizing are fine 
                formRect.Height += formOffsetRect.Height + formOffsetRect.Y
            End If

            '// Done !!
            originalForm.Bounds = formRect
        End Sub

        Private Sub Resize_Stick(ByVal toRect As Rectangle, ByVal bInsideStick As Boolean)
            If ((resizeDirection & ResizeDir.Top) = ResizeDir.Top) Then
                If (Math.Abs(formRect.Top - toRect.Bottom) <= Math.Abs(formOffsetRect.Top) AndAlso bInsideStick) Then
                    formOffsetRect.Y = formRect.Top - toRect.Bottom     '// snap top to bottom
                ElseIf (Math.Abs(formRect.Top - toRect.Top) <= Math.Abs(formOffsetRect.Top)) Then
                    formOffsetRect.Y = formRect.Top - toRect.Top        '// snap top to top
                End If
            End If

            If ((resizeDirection & ResizeDir.Bottom) = ResizeDir.Bottom) Then
                If (Math.Abs(formRect.Bottom - toRect.Top) <= Math.Abs(formOffsetRect.Bottom) AndAlso bInsideStick) Then
                    formOffsetRect.Height = toRect.Top - formRect.Bottom    '// snap Bottom to top
                ElseIf (Math.Abs(formRect.Bottom - toRect.Bottom) <= Math.Abs(formOffsetRect.Bottom)) Then
                    formOffsetRect.Height = toRect.Bottom - formRect.Bottom     '// snap bottom to bottom
                End If
            End If

            If (formRect.Bottom >= (toRect.Top - stickGap) AndAlso formRect.Top <= (toRect.Bottom + stickGap)) Then
                If ((resizeDirection And ResizeDir.Right) = ResizeDir.Right) Then
                    If (Math.Abs(formRect.Right - toRect.Left) <= Math.Abs(formOffsetRect.Right) AndAlso bInsideStick) Then
                        formOffsetRect.Width = toRect.Left - formRect.Right         '// Stick right to left
                    ElseIf (Math.Abs(formRect.Right - toRect.Right) <= Math.Abs(formOffsetRect.Right)) Then
                        formOffsetRect.Width = toRect.Right - formRect.Right    '// Stick right to right
                    End If
                End If

                If ((resizeDirection And ResizeDir.Left) = ResizeDir.Left) Then
                    If (Math.Abs(formRect.Left - toRect.Right) <= Math.Abs(formOffsetRect.Left) AndAlso bInsideStick) Then
                        formOffsetRect.X = formRect.Left - toRect.Right         '// Stick left to right
                    ElseIf (Math.Abs(formRect.Left - toRect.Left) <= Math.Abs(formOffsetRect.Left)) Then
                        formOffsetRect.X = formRect.Left - toRect.Left      '// Stick left to left
                    End If
                End If
            End If
        End Sub
#End Region

#Region "Move Operations"
        Private Sub StartMove()
            formRect = originalForm.Bounds
            formOriginalRect = originalForm.Bounds   '// save original position

            '// start capturing messages
            If (originalForm.Capture = False) Then originalForm.Capture = True

            MessageProcessor = MoveMessageProcessor
        End Sub

        Private Function MoveMsgProcessor(ByRef m As Message) As Boolean	'// internal message loop
            If (originalForm.Capture = False) Then
                Cancel()
                Return False
            End If

            Select (m.Msg)
                Case Win32.WM.WM_LBUTTONUP '// ok, move finished !!!
                    EndMove()
                Case Win32.WM.WM_MOUSEMOVE
                    mousePoint.X = CType(Win32.WordConverter.LoWord(m.LParam.ToInt32()), Short)
                    mousePoint.Y = CType(Win32.WordConverter.HiWord(m.LParam.ToInt32()), Short)
                    Move(mousePoint)
                Case Win32.WM.WM_KEYDOWN
                    If (CType(m.WParam, Integer) = Win32.VK.VK_ESCAPE) Then
                        originalForm.Bounds = formOriginalRect  '// set back old size
                        Cancel()
                    End If
            End Select

            Return False
        End Function

        Private Sub EndMove()
            Cancel()
        End Sub
#End Region

#Region "Move Computing"
        Public Sub MoveAttached()
            For Each sw As StickyWindow In _stuckToMe
                sw.originalForm.Location = originalForm.Location - sw._dockDifference
                sw.MoveAttached()
            Next
        End Sub

        Public Function AttachedDownChain(ByVal sw As StickyWindow) As Boolean
            For Each subsw As StickyWindow In _stuckToMe
                If subsw Is sw Then Return True
                Return subsw.AttachedDownChain(sw)
            Next
            Return False
        End Function

        Private Sub Move(ByVal p As Point)
            p = originalForm.PointToScreen(p)
            Dim activeScr As Screen = Screen.FromPoint(p)   '// get the screen from the point !!

            If (activeScr.WorkingArea.Contains(p) = False) Then
                p.X = NormalizeInside(p.X, activeScr.WorkingArea.Left, activeScr.WorkingArea.Right)
                p.Y = NormalizeInside(p.Y, activeScr.WorkingArea.Top, activeScr.WorkingArea.Bottom)
            End If

            p.Offset(-offsetPoint.X, -offsetPoint.Y)

            '// p is the exact location of the frame - so we can play with it
            '// to detect the new position acording to different bounds
            formRect.Location = p   '// this is the new positon of the form

            formOffsetPoint.X = StickGap + 1  '// (more than) maximum gaps
            formOffsetPoint.Y = StickGap + 1

            If (StickToScreen) Then Move_Stick(activeScr.WorkingArea, False)

            '// Now try to snap to other windows
            If (StickToOther) Then
                For Each sw As StickyWindow In GlobalStickyWindows
                    If (sw.originalForm IsNot Me.originalForm) AndAlso (AttachedDownChain(sw) = False) Then
                        If Move_Stick(sw.originalForm.Bounds, True) Then
                            If _stuckTo Is Nothing Then
                                _stuckTo = sw
                                sw._stuckToMe.Add(Me)
                            End If
                            _dockDifference = _stuckTo.originalForm.Location - originalForm.Location
                        ElseIf sw Is _stuckTo Then
                            If _stuckTo IsNot Nothing Then _stuckTo._stuckToMe.Remove(Me)
                            _stuckTo = Nothing
                        End If
                    End If
                Next

                ' Move attached windows, if any
                MoveAttached()
            End If

            If (formOffsetPoint.X = StickGap + 1) Then formOffsetPoint.X = 0
            If (formOffsetPoint.Y = StickGap + 1) Then formOffsetPoint.Y = 0

            formRect.Offset(formOffsetPoint)

            originalForm.Bounds = formRect
        End Sub

        '/// <summary>
        '/// 
        '/// </summary>
        '/// <param name="calculatedOffset">Calculate positon of the offset (snap distance)</param>
        '/// <param name="toRect">Rect to try to snap to</param>
        '/// <param name="bInsideStick">Allow snapping on the inside (eg: window to screen)</param>
        Private Function Move_Stick(ByVal toRect As Rectangle, ByVal bInsideStick As Boolean) As Boolean
            Dim Snapped As Boolean = False
            '// compare distance from toRect to formRect
            '// and then with the found distances, compare the most closed position
            If (formRect.Bottom >= (toRect.Top - StickGap) AndAlso formRect.Top <= (toRect.Bottom + StickGap)) Then
                If (bInsideStick) Then
                    If ((Math.Abs(formRect.Left - toRect.Right) <= Math.Abs(formOffsetPoint.X))) Then '// left 2 right
                        formOffsetPoint.X = toRect.Right - formRect.Left
                        Snapped = True
                    End If

                    If ((Math.Abs(formRect.Left + formRect.Width - toRect.Left) <= Math.Abs(formOffsetPoint.X))) Then   '// right 2 left
                        formOffsetPoint.X = toRect.Left - formRect.Width - formRect.Left
                        Snapped = True
                    End If
                End If

                If (Math.Abs(formRect.Left - toRect.Left) <= Math.Abs(formOffsetPoint.X)) Then '// snap left 2 left
                    formOffsetPoint.X = toRect.Left - formRect.Left
                    Snapped = True
                End If

                If (Math.Abs(formRect.Left + formRect.Width - toRect.Left - toRect.Width) <= Math.Abs(formOffsetPoint.X)) Then  '// snap right 2 right
                    formOffsetPoint.X = toRect.Left + toRect.Width - formRect.Width - formRect.Left
                    Snapped = True
                End If
            End If

            If (formRect.Right >= (toRect.Left - StickGap) AndAlso formRect.Left <= (toRect.Right + StickGap)) Then
                If (bInsideStick) Then
                    If (Math.Abs(formRect.Top - toRect.Bottom) <= Math.Abs(formOffsetPoint.Y) AndAlso bInsideStick) Then    '// Stick Top to Bottom
                        formOffsetPoint.Y = toRect.Bottom - formRect.Top
                        Snapped = True
                    End If

                    If (Math.Abs(formRect.Top + formRect.Height - toRect.Top) <= Math.Abs(formOffsetPoint.Y) AndAlso bInsideStick) Then '// snap Bottom to Top
                        formOffsetPoint.Y = toRect.Top - formRect.Height - formRect.Top
                        Snapped = True
                    End If
                End If

                '// try to snap top 2 top also
                If (Math.Abs(formRect.Top - toRect.Top) <= Math.Abs(formOffsetPoint.Y)) Then '// top 2 top
                    formOffsetPoint.Y = toRect.Top - formRect.Top
                    Snapped = True
                End If

                If (Math.Abs(formRect.Top + formRect.Height - toRect.Top - toRect.Height) <= Math.Abs(formOffsetPoint.Y)) Then '// bottom 2 bottom
                    formOffsetPoint.Y = toRect.Top + toRect.Height - formRect.Height - formRect.Top
                    Snapped = True
                End If
            End If
            Return Snapped
        End Function
#End Region

#Region "Utilities"
        Private Function NormalizeInside(ByVal iP1 As Integer, ByVal iM1 As Integer, ByVal iM2 As Integer) As Integer
            If (iP1 <= iM1) Then
                Return iM1
            Else
                If (iP1 >= iM2) Then
                    Return iM2
                End If
                Return iP1
            End If
        End Function
#End Region

#Region "Cancel"
        Private Sub Cancel()
            originalForm.Capture = False
            movingForm = False
            resizingForm = False
            MessageProcessor = DefaultMessageProcessor
        End Sub
#End Region
    End Class
End Namespace
