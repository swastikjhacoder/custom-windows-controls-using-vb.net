Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles

Namespace UIControls.eForms
    Public Class eGlassEffect

        Public Event GlassEffectEnabled(ByVal sender As Object, ByVal e As EventArgs)
        Public Event GlassEffectDisabled(ByVal sender As Object, ByVal e As EventArgs)

        Private WithEvents WindowListener As New APIs.HookWindow
        Private IsGlassEnabled As Boolean = eGlassEffect.GlassEnabled

#Region "    Properties Declaration Section    "

        Private WithEvents mParentForm As Form
        Public Property ParentForm() As Form
            Get
                Return mParentForm
            End Get
            Set(ByVal value As Form)
                mParentForm = value
            End Set
        End Property

        Private mHeaderImage As Windows.Forms.PictureBox
        Public Property HeaderImage() As Windows.Forms.PictureBox
            Get
                Return mHeaderImage
            End Get
            Set(ByVal value As Windows.Forms.PictureBox)
                mHeaderImage = value
            End Set
        End Property

        Private mTopBarSize As Integer
        Public Property TopBarSize() As Integer
            Get
                Return mTopBarSize
            End Get
            Set(ByVal value As Integer)
                mTopBarSize = value
            End Set
        End Property

        Private mBottomBarSize As Integer
        Public Property BottomBarSize() As Integer
            Get
                Return mBottomBarSize
            End Get
            Set(ByVal value As Integer)
                mBottomBarSize = value
            End Set
        End Property

        Private mLeftBarSize As Integer
        Public Property LeftBarSize() As Integer
            Get
                Return mLeftBarSize
            End Get
            Set(ByVal value As Integer)
                mLeftBarSize = value
            End Set
        End Property

        Private mRightBarSize As Integer
        Public Property RightBarSize() As Integer
            Get
                Return mRightBarSize
            End Get
            Set(ByVal value As Integer)
                mRightBarSize = value
            End Set
        End Property

        Private mHeaderLabel As Windows.Forms.Label
        Public Property HeaderLabel() As Windows.Forms.Label
            Get
                Return mHeaderLabel
            End Get
            Set(ByVal value As Windows.Forms.Label)
                mHeaderLabel = value
            End Set
        End Property

        Private mUseHandCursorOnTitle As Boolean = True
        Property UseHandCursorOnTitle() As Boolean
            Get
                Return mUseHandCursorOnTitle
            End Get
            Set(ByVal value As Boolean)
                mUseHandCursorOnTitle = value
            End Set
        End Property

#End Region

#Region "    Public Methods of the Class       "

        Public Sub ShowEffect(ByVal Parent As Form, ByVal HeaderLabel As Windows.Forms.Label, ByVal HeaderImage As Windows.Forms.PictureBox)
            Me.ParentForm = Parent
            Me.HeaderLabel = HeaderLabel
            Me.HeaderImage = HeaderImage

            SetGlassEffect(Me.ParentForm, mTopBarSize, mRightBarSize, mBottomBarSize, mLeftBarSize)
        End Sub

        Public Sub ShowEffect(ByVal Parent As Form, ByVal HeaderLabel As Windows.Forms.Label)
            Me.ParentForm = Parent
            Me.HeaderLabel = HeaderLabel

            SetGlassEffect(Me.ParentForm, mTopBarSize, mRightBarSize, mBottomBarSize, mLeftBarSize)
        End Sub

        Public Sub ShowEffect(ByVal Parent As Form, ByVal HeaderImage As Windows.Forms.PictureBox)
            Me.ParentForm = Parent
            Me.HeaderImage = HeaderImage

            SetGlassEffect(Me.ParentForm, mTopBarSize, mRightBarSize, mBottomBarSize, mLeftBarSize)
        End Sub

        Public Sub ShowEffect(ByVal Parent As Form)
            Me.ParentForm = Parent
            SetGlassEffect(Me.ParentForm, mTopBarSize, mRightBarSize, mBottomBarSize, mLeftBarSize)
        End Sub

        Public Function SetGlassEffect(Optional ByVal fromTop As Integer = 0, Optional ByVal fromRight As Integer = 0, Optional ByVal fromBottom As Integer = 0, Optional ByVal fromLeft As Integer = 0) As Boolean
            SetGlassEffect(Me.ParentForm, fromTop, fromRight, fromBottom, fromLeft)
            Me.ParentForm.Invalidate()
        End Function

#End Region

#Region "    Public Shared Methods             "

        Public Shared ReadOnly Property GlassEnabled() As Boolean
            Get
                Dim VistaOrAbove As Boolean = (Environment.OSVersion.Version.Major >= 6)

                If VistaOrAbove Then
                    Dim Enabled As Boolean
                    APIs.DwmIsCompositionEnabled(Enabled)

                    Return Enabled
                Else
                    Return False
                End If

            End Get
        End Property

        Public Shared Function SetGlassEffect(ByVal Frm As Form, Optional ByVal fromTop As Integer = 0, Optional ByVal fromRight As Integer = 0, Optional ByVal fromBottom As Integer = 0, Optional ByVal fromLeft As Integer = 0) As Boolean

            If eGlassEffect.GlassEnabled AndAlso Frm IsNot Nothing Then
                Dim m As New APIs.MARGINS

                m.Top = fromTop
                m.Right = fromRight
                m.Left = fromLeft
                m.Bottom = fromBottom

                APIs.DwmExtendFrameIntoClientArea(Frm.Handle, m)
                Frm.Invalidate()
            End If

        End Function

        Public Shared Sub DrawTextGlow(ByVal Graphics As Graphics, ByVal text As String, ByVal fnt As Font, ByVal bounds As Rectangle, ByVal Clr As Color, ByVal flags As TextFormatFlags)

            ' Variables used later.
            Dim SavedBitmap As IntPtr = IntPtr.Zero
            Dim SavedFont As IntPtr = IntPtr.Zero
            Dim MainHDC As IntPtr = Graphics.GetHdc
            Dim MemHDC As IntPtr = APIs.CreateCompatibleDC(MainHDC)
            Dim BtmInfo As New APIs.BITMAPINFO
            Dim TextRect As New APIs.RECT(0, 0, bounds.Right - bounds.Left + 2 * 15, bounds.Bottom - bounds.Top + 2 * 15)
            Dim ScreenRect As New APIs.RECT(bounds.Left - 15, bounds.Top - 15, bounds.Right + 15, bounds.Bottom + 15)
            Dim hFont As IntPtr = fnt.ToHfont

            Try
                Dim Renderer As VisualStyleRenderer = New VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active)

                ' Memory bitmap to hold the drawn glowed text.
                BtmInfo.bmiHeader.biSize = Marshal.SizeOf(BtmInfo.bmiHeader)

                With BtmInfo
                    .bmiHeader.biWidth = bounds.Width + 30
                    .bmiHeader.biHeight = -bounds.Height - 30
                    .bmiHeader.biPlanes = 1
                    .bmiHeader.biBitCount = 32
                End With

                ' Create a DIB Section for this bitmap from the graphics object.
                Dim dibSection As IntPtr = APIs.CreateDIBSection(MainHDC, BtmInfo, 0, 0, IntPtr.Zero, 0)

                ' Save the current handles temporarily.
                SavedBitmap = APIs.SelectObject(MemHDC, dibSection)
                SavedFont = APIs.SelectObject(MemHDC, hFont)

                ' Holds the properties of the text (size and color , ...etc).
                Dim TextOptions As APIs.S_DTTOPTS = New APIs.S_DTTOPTS

                With TextOptions
                    .dwSize = Marshal.SizeOf(TextOptions)
                    .dwFlags = APIs.DTT_COMPOSITED Or APIs.DTT_GLOWSIZE Or APIs.DTT_TEXTCOLOR
                    .crText = ColorTranslator.ToWin32(Clr)
                    .iGlowSize = 18
                End With

                ' Draw The text on the memory surface.
                APIs.DrawThemeTextEx(Renderer.Handle, MemHDC, 0, 0, text, -1, flags, TextRect, TextOptions)

                ' Reflecting the image on the primary surface of the graphics object.
                With ScreenRect
                    APIs.BitBlt(MainHDC, .Left, .Top, .Right - .Left, .Bottom - .Top, MemHDC, 0, 0, APIs.SRCCOPY)
                End With

                ' Resources Cleaning.
                APIs.SelectObject(MemHDC, SavedFont)
                APIs.SelectObject(MemHDC, SavedBitmap)

                APIs.DeleteDC(MemHDC)
                APIs.DeleteObject(hFont)
                APIs.DeleteObject(dibSection)

                Graphics.ReleaseHdc(MainHDC)
            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "    APIs Declaration Section          "

        Public Class APIs

            Public Declare Function CreateDIBSection Lib "gdi32.dll" (ByVal hdc As IntPtr, ByRef pbmi As BITMAPINFO, ByVal iUsage As UInt32, ByVal ppvBits As Integer, ByVal hSection As IntPtr, ByVal dwOffset As UInt32) As IntPtr
            Public Declare Function CreateCompatibleDC Lib "gdi32.dll" (ByVal hDC As IntPtr) As IntPtr
            Public Declare Function SelectObject Lib "gdi32.dll" (ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
            Public Declare Function DeleteObject Lib "gdi32.dll" (ByVal hObject As IntPtr) As Boolean
            Public Declare Function DeleteDC Lib "gdi32.dll" (ByVal hdc As IntPtr) As Boolean
            Public Declare Function BitBlt Lib "gdi32.dll" (ByVal hdc As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr, ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As Int32) As Boolean

            Public Declare Function DwmExtendFrameIntoClientArea Lib "dwmapi.dll" (ByVal hWnd As IntPtr, ByRef margins As MARGINS) As Integer
            Public Declare Sub DwmIsCompositionEnabled Lib "dwmapi.dll" (ByRef IsIt As Boolean)
            <DllImport("UxTheme.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)> Shared Function DrawThemeTextEx(ByVal hTheme As IntPtr, ByVal hdc As IntPtr, ByVal iPartId As Integer, ByVal iStateId As Integer, ByVal text As String, ByVal iCharCount As Integer, ByVal dwFlags As Integer, ByRef pRect As RECT, ByRef pOptions As S_DTTOPTS) As Integer
            End Function

            Public Const DTT_COMPOSITED As Integer = 8192
            Public Const DTT_GLOWSIZE As Integer = 2048
            Public Const DTT_TEXTCOLOR As Integer = 1
            Public Const SRCCOPY As Integer = &HCC0020
            Public Const WM_SYSCOLORCHANGE As Int32 = &H15

            <StructLayout(LayoutKind.Sequential)> _
            Public Structure MARGINS
                Public Left As Integer
                Public Right As Integer
                Public Top As Integer
                Public Bottom As Integer
            End Structure

            Public Structure RECT

                Public Sub New(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer)
                    Left = iLeft
                    Top = iTop
                    Right = iRight
                    Bottom = iBottom
                End Sub

                Public Left As Integer
                Public Top As Integer
                Public Right As Integer
                Public Bottom As Integer
            End Structure

            Public Structure BITMAPINFOHEADER
                Dim biSize As Integer
                Dim biWidth As Integer
                Dim biHeight As Integer
                Dim biPlanes As Short
                Dim biBitCount As Short
                Dim biCompression As Integer
                Dim biSizeImage As Integer
                Dim biXPelsPerMeter As Integer
                Dim biYPelsPerMeter As Integer
                Dim biClrUsed As Integer
                Dim biClrImportant As Integer
            End Structure

            Public Structure RGBQUAD
                Dim rgbBlue As Byte
                Dim rgbGreen As Byte
                Dim rgbRed As Byte
                Dim rgbReserved As Byte
            End Structure

            Public Structure BITMAPINFO
                Dim bmiHeader As BITMAPINFOHEADER
                Dim bmiColors As RGBQUAD
            End Structure

            Public Structure S_DTTOPTS
                Dim dwSize As Integer
                Dim dwFlags As Integer
                Dim crText As Integer
                Dim crBorder As Integer
                Dim crShadow As Integer
                Dim iTextShadowType As Integer
                Dim ptShadowOffset As Point
                Dim iBorderSize As Integer
                Dim iFontPropId As Integer
                Dim iColorPropId As Integer
                Dim iStateId As Integer
                Dim fApplyOverlay As Boolean
                Dim iGlowSize As Integer
                Dim pfnDrawTextCallback As Integer
                Dim lParam As IntPtr
            End Structure

            Public Class HookWindow
                Inherits NativeWindow

                Sub New()
                    Dim cp As New CreateParams()
                    Me.CreateHandle(cp)
                End Sub

                Public Event MessageArrived(ByVal sender As Object, ByVal e As EventArgs)

                Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

                    If m.Msg = APIs.WM_SYSCOLORCHANGE Then
                        RaiseEvent MessageArrived(Me, New EventArgs)
                    End If

                    MyBase.WndProc(m)

                End Sub

                Protected Overrides Sub Finalize()
                    Me.DestroyHandle()
                    MyBase.Finalize()
                End Sub
            End Class

        End Class

#End Region

#Region "    Event Handlers of the Parent Form "

        Private Sub Parent_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles mParentForm.Paint

            Try

                If GlassEnabled Then

                    If mTopBarSize > 0 Then
                        e.Graphics.FillRectangle(Brushes.Black, New Rectangle(0, 0, Me.ParentForm.ClientSize.Width, mTopBarSize))
                    End If

                    If mBottomBarSize > 0 Then
                        e.Graphics.FillRectangle(Brushes.Black, New Rectangle(0, Me.ParentForm.ClientSize.Height - mBottomBarSize, Me.ParentForm.ClientSize.Width, mBottomBarSize))
                    End If

                    If mRightBarSize > 0 Then
                        e.Graphics.FillRectangle(Brushes.Black, New Rectangle(Me.ParentForm.ClientSize.Width - mRightBarSize, 0, mRightBarSize, Me.ParentForm.ClientSize.Height))
                    End If

                    If mLeftBarSize > 0 Then
                        e.Graphics.FillRectangle(Brushes.Black, New Rectangle(0, 0, mLeftBarSize, Me.ParentForm.ClientSize.Height))
                    End If

                    If (HeaderLabel IsNot Nothing) AndAlso (HeaderLabel.Text.Length > 0) Then
                        HeaderLabel.Visible = False
                        eGlassEffect.DrawTextGlow(e.Graphics, HeaderLabel.Text, HeaderLabel.Font, HeaderLabel.Bounds, HeaderLabel.ForeColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine)
                    End If

                    If (Me.HeaderImage IsNot Nothing) AndAlso (Me.HeaderImage.Image IsNot Nothing) Then
                        HeaderImage.Visible = False
                        e.Graphics.DrawImage(mHeaderImage.Image, mHeaderImage.Bounds)
                    End If

                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Last As Point = Point.Empty
        Private Sub Parent_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mParentForm.MouseDown
            If e.Location.Y <= mTopBarSize Then
                Last = e.Location
            Else
                Last = Point.Empty
            End If
        End Sub

        Private Sub Parent_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mParentForm.MouseMove
            If (Not Last.Equals(Point.Empty)) AndAlso (e.Button = Windows.Forms.MouseButtons.Left) Then
                ParentForm.Location = New Point(ParentForm.Left + e.Location.X - Last.X, ParentForm.Top + e.Location.Y - Last.Y)
            End If

            If mUseHandCursorOnTitle Then
                If (e.Location.Y < mTopBarSize) Then
                    If Not ParentForm.Cursor.Equals(Cursors.Hand) Then ParentForm.Cursor = Cursors.Hand
                ElseIf Not ParentForm.Cursor.Equals(Cursors.Default) Then
                    ParentForm.Cursor = Cursors.Default
                End If
            End If
        End Sub

        Private Sub ParentForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles mParentForm.Resize
            ParentForm.Invalidate()
        End Sub

        Private Sub WindowListener_MessageArrived(ByVal sender As Object, ByVal e As System.EventArgs) Handles WindowListener.MessageArrived
            Dim State As Boolean = eGlassEffect.GlassEnabled

            If State AndAlso Not IsGlassEnabled Then
                RaiseEvent GlassEffectEnabled(Me, New EventArgs)
            ElseIf Not State AndAlso IsGlassEnabled Then
                RaiseEvent GlassEffectDisabled(Me, New EventArgs)
            End If

            IsGlassEnabled = State
        End Sub

#End Region


    End Class
End Namespace