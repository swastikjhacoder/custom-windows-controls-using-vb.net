
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
' For UITypeEditor
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design
' For IWindowsFormsEditorService
Imports ESAR_Controls.UIControls.ProgressBar.Win32

Namespace UIControls.ProgressBar
#Region "Enum"

    Public Enum EasyProgressBarBorderStyle
        Flat
        Sunken
        Raised
    End Enum

#End Region

#Region "Struct"

    Public Structure ColorManipulation
#Region "Instance Members"

        Public Dark As Color
        Public Light As Color
        Public Darker As Color
        Public Lighter As Color
        Public BaseColor As Color

#End Region

#Region "Constructor"

        Public Sub New(ByVal baseColor As Color)
            Me.Dark = Shade(0.8F, baseColor)
            Me.Light = Smooth(0.6F, baseColor)
            Me.Darker = Shade(0.6F, baseColor)
            Me.Lighter = Smooth(0.3F, baseColor)
            Me.BaseColor = baseColor
        End Sub

#End Region

#Region "Helper Methods"

        Private Shared Function Shade(ByVal ratio As Single, ByVal baseColor As Color) As Color
            Return Manipule(ratio, baseColor, Color.Black)
        End Function

        Private Shared Function Smooth(ByVal ratio As Single, ByVal baseColor As Color) As Color
            Return Manipule(ratio, baseColor, Color.White)
        End Function

        Private Shared Function Manipule(ByVal ratio As Single, ByVal baseColor As Color, ByVal manipuleColor As Color) As Color
            Dim r As Integer = CInt(manipuleColor.R + ratio * (baseColor.R - manipuleColor.R))
            ' Red
            Dim g As Integer = CInt(manipuleColor.G + ratio * (baseColor.G - manipuleColor.G))
            ' Green
            Dim b As Integer = CInt(manipuleColor.B + ratio * (baseColor.B - manipuleColor.B))
            ' Blue
            Return Color.FromArgb(r, g, b)
        End Function

#End Region
    End Structure

#End Region

    <Designer(GetType(EasyProgressBarDesigner))> _
    <DefaultEvent("ValueChanged"), DefaultProperty("Value")> _
    Public Class eEasyProgressBar
        Inherits Control
        Implements IProgressBar
        Implements IFloatWindowBase
        'Implements ICloneable
#Region "Events"

        ''' <summary>
        ''' Occurs when the currently docking style of the EasyProgressBar is changed.
        ''' </summary>
        <Description("Occurs when the currently docking style of the EasyProgressBar is changed")> _
        Public Event DockingModeChanged As EventHandler

        ''' <summary>
        ''' Event raised when the EasyProgressBar control is closed by the user, it's docking state should be float window for this change to take effect.
        ''' </summary>
        <Description("Event raised when the EasyProgressBar control is closed by the user, it's docking state should be float window for this change to take effect")> _
        Public Event EasyProgressBarClosed As EventHandler

        Public Delegate Sub EasyProgressBarClosingEventHandler(ByVal sender As Object, ByRef cancel As Boolean)
        ''' <summary>
        ''' Occurs when the docking state of the EasyProgressBar control is in the float mode and EasyProgressBar control is being closed by the user.
        ''' </summary>
        <Description("Occurs when the docking state of the EasyProgressBar control is in the float mode and EasyProgressBar control is being closed by the user")> _
        Public Event EasyProgressBarClosing As EasyProgressBarClosingEventHandler

#End Region

#Region "System Menu Constants"

        ' LEFTSIDE, RIGHTSIDE, DOCKIT, IS_OPACITY_ENABLED, SEEK_TO_OPACITY
        Private Shared ReadOnly SYSTEM_MENUITEMS As UInteger() = {&H100, &H101, &H102, &H103, &H104}

#End Region

#Region "Instance Members"

        Private m_minimum As Integer = 0
        ' Initializer
        Private m_maximum As Integer = 100
        ' Initializer
        Private progress As Integer = 20
        ' Initializer
        Private isDocked As Boolean = True
        ' Initializer
        Private m_isPaintBorder As Boolean = True
        ' Initializer
        Private m_isDigitDrawEnabled As Boolean = False
        ' Initializer
        Private m_isUserInteraction As Boolean = False
        ' Initializer
        Private m_showPercentage As Boolean = True
        ' Initializer
        Private m_displayFormat As String = "done"
        ' Initializer
        Private m_borderColor As Color = Color.DarkGray
        ' Initializer
        Private m_alphaMaker As Components.IAlphaMaker = Nothing
        ' Initializer
        Private parentContainer As Control = Nothing
        ' Initializer
        Private oldLocation As Point = Point.Empty
        ' Initializer
        Private m_controlBorderStyle As EasyProgressBarBorderStyle = EasyProgressBarBorderStyle.Flat
        ' Initializer
        Private m_progressGradient As GradientProgress
        Private m_backgroundGradient As GradientBackground
        Private m_digitBoxGradient As GradientDigitBox
        Private m_progressColorizer As ColorizerProgress

        Private progressRectangle As Rectangle
        Private backgroundRectangle As Rectangle
        Private infoBoxRectangle As RectangleF

#Region "IFloatWindowAlpha Members"

        Private m_isLayered As Boolean = False
        Private m_targetTransparency As Byte = 200
        Private m_currentTransparency As Byte = 255

#End Region

#End Region

#Region "Constructor"

        Public Sub New()
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)

            ' Object Initializer
            Me.Size = New Size(150, 40)

            m_progressGradient = New GradientProgress()
            AddHandler m_progressGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE

            m_backgroundGradient = New GradientBackground()
            AddHandler m_backgroundGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE

            m_progressColorizer = New ColorizerProgress()
            AddHandler m_progressColorizer.ProgressColorizerChanged, AddressOf CONTROL_INVALIDATE_UPDATE

            m_digitBoxGradient = New GradientDigitBox()
            AddHandler m_digitBoxGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE
        End Sub

#End Region

#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Property"

        ''' <summary>
        ''' Provides keyboard support to the user for EasyProgressBar operations[Home(Min value), End(Max value), Left(Decrease current value), Right(Increase current value), Enter(ReDocking), F1 Keys].
        ''' </summary>
        <Description("Provides keyboard support to the user for EasyProgressBar operations[Home(Min value), End(Max value), Left(Decrease current value), Right(Increase current value), Enter(ReDocking), F1 Keys]")> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        <IsCloneable(True)> _
        Public Property IsUserInteraction() As Boolean Implements IFloatWindowBase.DockUndockProgressBar
            Get
                Return m_isUserInteraction
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(m_isUserInteraction) Then
                    m_isUserInteraction = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets, this control's alpha maker component whose window has a alpha support or to be.
        ''' </summary>
        <Description("Gets or sets, this control's alpha maker component whose window has a alpha support or to be")> _
        <Browsable(True)> _
        Public Property AlphaMaker() As Components.IAlphaMaker
            Get
                Return m_alphaMaker
            End Get
            Set(ByVal value As Components.IAlphaMaker)
                Try
                    If Not value.Equals(m_alphaMaker) Then
                        m_alphaMaker = value

                        If Not isDocked Then
                            m_alphaMaker.IFloatWindowControl = Me
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    m_alphaMaker = Nothing
                End Try
            End Set
        End Property

        Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                If Not isDocked Then
                    cp.Style = cp.Style And (Not &H800000)
                    ' WS_BORDER
                    cp.Style = cp.Style And (Not &HC00000)
                    ' WS_CAPTION
                    ' WS_SYSMENU | WS_MINIMIZEBOX
                    cp.Style = cp.Style Or &H80000 Or &H20000
                End If

                Return cp
            End Get
        End Property

#End Region

#Region "Helper Methods"

        Private Sub CONTROL_INVALIDATE_UPDATE(ByVal sender As Object, ByVal e As EventArgs)
            Invalidate()
            Update()
        End Sub

        Private Sub GenerateProgressBar()
            ' Draws percent text value on the control and generates the width of the progress background and its progress indicator. 
            Dim percent As Integer = CInt(progress * 100 / m_maximum)
            Me.Text = [String].Format("{0}{1} {2}", If(percent >= 100, 100, percent), "%", m_displayFormat)

            backgroundRectangle = Me.ClientRectangle

            If m_isPaintBorder Then
                backgroundRectangle.Inflate(-1, -1)
            End If

            progressRectangle = backgroundRectangle

            Dim progressWidth As Integer, currentProgress As Integer = Value

            If Value >= Maximum Then
                progressWidth = backgroundRectangle.Width
            Else
                progressWidth = backgroundRectangle.Width * currentProgress / Maximum
            End If

            progressRectangle.Width = progressWidth
            progressRectangle.Height -= 1
        End Sub

        ' Resets the system menu to it's default.
        Private Sub ResetSystemMenu()
            User32.GetSystemMenu(Me.Handle, True)
        End Sub

        ' Creates extra menu items for docking and alpha operations.
        Private Sub CreateSystemMenu()
            Dim menuHandle As IntPtr = User32.GetSystemMenu(Me.Handle, False)

            ' Insert a separator item on top.
            User32.InsertMenu(menuHandle, 0, User32.MenuFlags.MF_SEPARATOR Or User32.MenuFlags.MF_BYPOSITION, 0, Nothing)
            ' Create and insert a popup menu for layout operations.
            Dim subMenu As IntPtr = User32.CreatePopupMenu()
            User32.AppendMenu(subMenu, If(m_digitBoxGradient.DigitalNumberSide <> GradientDigitBox.DigitalNumberLayout.LeftSide, User32.MenuFlags.MF_STRING, User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED Or User32.MenuFlags.MF_CHECKED), SYSTEM_MENUITEMS(0), "Left Side")
            User32.AppendMenu(subMenu, If(m_digitBoxGradient.DigitalNumberSide <> GradientDigitBox.DigitalNumberLayout.RightSide, User32.MenuFlags.MF_STRING, User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED Or User32.MenuFlags.MF_CHECKED), SYSTEM_MENUITEMS(1), "Right Side")
            User32.InsertMenu(menuHandle, 0, If(m_isDigitDrawEnabled, User32.MenuFlags.MF_POPUP Or User32.MenuFlags.MF_BYPOSITION, User32.MenuFlags.MF_POPUP Or User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED), CUInt(subMenu), "DigitBox Layouts")

            ' Insert a separator item by specified position.
            User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_SEPARATOR Or User32.MenuFlags.MF_BYPOSITION, 0, Nothing)
            User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING, SYSTEM_MENUITEMS(2), "Dock It!")

            ' If our alpha maker component is not null, we adds extra a separator and alpha menu items.
            If m_alphaMaker IsNot Nothing Then
                User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_SEPARATOR Or User32.MenuFlags.MF_BYPOSITION, 0, Nothing)
                User32.InsertMenu(menuHandle, 2, If(m_isLayered, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_CHECKED, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING), SYSTEM_MENUITEMS(3), [String].Format("Opacity {0}[{1}]", If(m_isLayered, "Enabled", "Disabled"), m_currentTransparency))
                User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING, SYSTEM_MENUITEMS(4), "Seek To Opacity")
            End If
        End Sub

#End Region

#Region "Virtual Methods"

        Protected Overridable Sub DrawText(ByVal gr As Graphics)
            If Not m_showPercentage Then
                Return
            End If

            Dim rct As RectangleF
            Dim wrap As Boolean = False
            If Not m_isDigitDrawEnabled OrElse backgroundRectangle.Height < 32 Then
                rct = backgroundRectangle
            Else
                wrap = True
                rct = infoBoxRectangle
            End If

            rct.Inflate(-1, -1)
            If rct.Width > 2 Then
                If wrap Then
                    Dim mode As SmoothingMode = gr.SmoothingMode
                    gr.SmoothingMode = SmoothingMode.AntiAlias
                    DrawDigitalHelper.RctWidth = rct.Width
                    DrawDigitalHelper.DrawNumbers(gr, progress, m_maximum, Me.ForeColor, infoBoxRectangle)
                    gr.SmoothingMode = mode
                Else
                    Using brush As New SolidBrush(Me.ForeColor)
                        Using textFormat As New StringFormat(StringFormatFlags.LineLimit)
                            textFormat.Alignment = StringAlignment.Center
                            textFormat.LineAlignment = StringAlignment.Center

                            textFormat.Trimming = StringTrimming.EllipsisCharacter
                            textFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide

                            gr.DrawString(Me.Text, Me.Font, brush, rct, textFormat)
                        End Using
                    End Using
                End If
            End If
        End Sub

        Protected Overridable Sub DrawBorder(ByVal gr As Graphics)
            If Not m_isPaintBorder Then
                Return
            End If

            Dim topLeft As Pen
            Dim bottomRight As Pen

            Select Case m_controlBorderStyle
                Case EasyProgressBarBorderStyle.Raised
                    topLeft = New Pen(New SolidBrush(SystemColors.ControlLightLight))
                    bottomRight = New Pen(New SolidBrush(SystemColors.ControlDark))
                    Exit Select
                Case EasyProgressBarBorderStyle.Sunken
                    topLeft = New Pen(New SolidBrush(SystemColors.ControlDark))
                    bottomRight = New Pen(New SolidBrush(SystemColors.ControlLightLight))
                    Exit Select
                Case Else
                    topLeft = New Pen(m_borderColor)
                    bottomRight = topLeft
                    Exit Select
            End Select

            gr.DrawLine(topLeft, 0, 0, Me.Width - 1, 0)
            ' Top
            gr.DrawLine(topLeft, 0, 0, 0, Me.Height - 1)
            ' Left
            gr.DrawLine(bottomRight, 0, Me.Height - 1, Me.Width - 1, Me.Height - 1)
            ' Bottom
            gr.DrawLine(bottomRight, Me.Width - 1, 0, Me.Width - 1, Me.Height - 1)
            ' Right
            topLeft.Dispose()
            bottomRight.Dispose()
        End Sub

        Protected Overridable Sub DrawProgress(ByVal gr As Graphics)
            If progressRectangle.Width < 2 Then
                Return
            End If

            Dim left As New Point(progressRectangle.X, progressRectangle.Y)
            Dim right As New Point(progressRectangle.Right, progressRectangle.Y)

            ' Create a new empty image for manipulations. If you use this constructor, you get a new Bitmap object that represents a bitmap in memory with a PixelFormat of Format32bppARGB.
            Using overlay As New Bitmap(progressRectangle.Width + 2, progressRectangle.Height + 2)
                ' Make an associated Graphics object.
                Using bmpGraphics As Graphics = Graphics.FromImage(overlay)
                    Using brush As New LinearGradientBrush(Point.Empty, New Point(0, progressRectangle.Height), m_progressGradient.ManipuleStart.BaseColor, m_progressGradient.ManipuleEnd.BaseColor)
                        If Not m_progressGradient.IsBlendedForProgress Then
                            bmpGraphics.FillRectangle(brush, progressRectangle)
                        Else
                            Dim bl As New Blend(2)
                            bl.Factors = New Single() {0.3F, 1.0F}
                            bl.Positions = New Single() {0.0F, 1.0F}
                            brush.Blend = bl
                            bmpGraphics.FillRectangle(brush, progressRectangle)
                        End If
                    End Using

                    Dim topInner As New LinearGradientBrush(left, right, m_progressGradient.ManipuleStart.Light, m_progressGradient.ManipuleEnd.Light)
                    Dim topOuter As New LinearGradientBrush(left, right, m_progressGradient.ManipuleStart.Lighter, m_progressGradient.ManipuleEnd.Lighter)
                    Dim bottomInner As New LinearGradientBrush(left, right, m_progressGradient.ManipuleStart.Dark, m_progressGradient.ManipuleEnd.Dark)
                    Dim bottomOuter As New LinearGradientBrush(left, right, m_progressGradient.ManipuleStart.Darker, m_progressGradient.ManipuleEnd.Darker)

                    ' Inner Top
                    Using pen As New Pen(topInner)
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Y + 1, progressRectangle.Right - 1, progressRectangle.Y + 1)
                    End Using

                    ' Inner Left
                    Using pen As New Pen(m_progressGradient.ManipuleStart.Light)
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Y + 1, progressRectangle.X + 1, progressRectangle.Bottom - 1)
                    End Using

                    ' Outer Top
                    Using pen As New Pen(topOuter)
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Y, progressRectangle.Right, progressRectangle.Y)
                    End Using

                    ' Outer Left
                    Using pen As New Pen(m_progressGradient.ManipuleStart.Lighter)
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Y, progressRectangle.X, progressRectangle.Bottom)
                    End Using

                    ' Inner Bottom
                    Using pen As New Pen(bottomInner)
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Bottom - 1, progressRectangle.Right - 1, progressRectangle.Bottom - 1)
                    End Using

                    ' Inner Right
                    Using pen As New Pen(m_progressGradient.ManipuleEnd.Dark)
                        bmpGraphics.DrawLine(pen, progressRectangle.Right - 1, progressRectangle.Y + 1, progressRectangle.Right - 1, progressRectangle.Bottom - 1)
                    End Using

                    ' Outer Bottom
                    Using pen As New Pen(bottomOuter)
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Bottom, progressRectangle.Right, progressRectangle.Bottom)
                    End Using

                    ' Outer Right
                    Using pen As New Pen(m_progressGradient.ManipuleEnd.Darker)
                        bmpGraphics.DrawLine(pen, progressRectangle.Right, progressRectangle.Y, progressRectangle.Right, progressRectangle.Bottom)
                    End Using

                    topInner.Dispose()
                    topOuter.Dispose()
                    bottomInner.Dispose()
                    bottomOuter.Dispose()
                End Using

                ' Create a new color matrix,
                '                   The value Alpha in row 4, column 4 specifies the alpha value 

                ' Red component   [from 0.0 to 1.0 increase red color component.]
                ' Green component [from 0.0 to 1.0 increase green color component.]
                ' Blue component  [from 0.0 to 1.0 increase blue color component.]
                ' Alpha component [from 1.0 to 0.0 increase transparency bitmap.]
                ' White component [0.0: goes to Original color, 1.0: goes to white for all color component(Red, Green, Blue.)]
                Dim jaggedMatrix As Single()() = New Single()() {New Single() {If(m_progressColorizer.IsColorizerEnabled, m_progressColorizer.Red / 255.0F, 1.0F), 0.0F, 0.0F, 0.0F, 0.0F}, New Single() {0.0F, If(m_progressColorizer.IsColorizerEnabled, m_progressColorizer.Green / 255.0F, 1.0F), 0.0F, 0.0F, 0.0F}, New Single() {0.0F, 0.0F, If(m_progressColorizer.IsColorizerEnabled, m_progressColorizer.Blue / 255.0F, 1.0F), 0.0F, 0.0F}, New Single() {0.0F, 0.0F, 0.0F, If(m_progressColorizer.IsTransparencyEnabled, m_progressColorizer.Alpha / 255.0F, 1.0F), 0.0F}, New Single() {If(m_progressColorizer.IsColorizerEnabled, 0.2F, 0.0F), If(m_progressColorizer.IsColorizerEnabled, 0.2F, 0.0F), If(m_progressColorizer.IsColorizerEnabled, 0.2F, 0.0F), 0.0F, 1.0F}}

                Dim colorMatrix As New ColorMatrix(jaggedMatrix)

                ' Create an ImageAttributes object and set its color matrix
                Using attributes As New ImageAttributes()
                    attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)

                    gr.DrawImage(overlay, New Rectangle(Point.Empty, New Size(overlay.Width, overlay.Height)), 0, 0, overlay.Width, overlay.Height, _
                     GraphicsUnit.Pixel, attributes)
                End Using
            End Using
        End Sub

        Protected Overridable Sub DrawBackground(ByVal gr As Graphics)
            If backgroundRectangle.Width < 2 Then
                Return
            End If

            Using brush As New LinearGradientBrush(Point.Empty, New Point(0, backgroundRectangle.Height), m_backgroundGradient.ColorStart, m_backgroundGradient.ColorEnd)
                If Not m_backgroundGradient.IsBlendedForBackground Then
                    gr.FillRectangle(brush, backgroundRectangle)
                Else
                    Dim bl As New Blend(2)
                    bl.Factors = New Single() {0.3F, 1.0F}
                    bl.Positions = New Single() {0.0F, 1.0F}
                    brush.Blend = bl
                    gr.FillRectangle(brush, backgroundRectangle)
                End If
            End Using
        End Sub

        Protected Overridable Sub DrawDigitBox(ByVal gr As Graphics)
            If Not m_isDigitDrawEnabled OrElse Not m_showPercentage Then
                Return
            End If

            Dim num As String = m_maximum.ToString()
            Dim startingWide As Single = 0
            Select Case num.Length - 1
                Case 0
                    startingWide = 0.8F
                    Exit Select
                Case 1
                    startingWide = 1.5F
                    Exit Select
                Case 2
                    startingWide = 2
                    Exit Select
                Case Else
                    startingWide = num.Length - 1.42F
                    Exit Select
            End Select

            Select Case m_digitBoxGradient.DigitalNumberSide
                Case GradientDigitBox.DigitalNumberLayout.LeftSide
                    infoBoxRectangle = backgroundRectangle
                    infoBoxRectangle.Width = backgroundRectangle.Height * startingWide
                    Exit Select
                Case GradientDigitBox.DigitalNumberLayout.RightSide
                    infoBoxRectangle = backgroundRectangle
                    infoBoxRectangle.X = backgroundRectangle.Right - backgroundRectangle.Height * startingWide
                    infoBoxRectangle.Width = backgroundRectangle.Height * startingWide
                    Exit Select
            End Select

            If infoBoxRectangle.Height >= 32 Then
                infoBoxRectangle.Inflate(-5, -5)
                Using pen As New Pen(m_digitBoxGradient.BorderColor)
                    gr.DrawRectangle(pen, infoBoxRectangle.X, infoBoxRectangle.Y, infoBoxRectangle.Width - 1, infoBoxRectangle.Height - 1)
                End Using

                infoBoxRectangle.Inflate(-1, -1)
                Using brush As New LinearGradientBrush(Point.Empty, New Point(0, backgroundRectangle.Height), m_digitBoxGradient.ColorStart, m_digitBoxGradient.ColorEnd)
                    If Not m_digitBoxGradient.IsBlendedForBackground Then
                        gr.FillRectangle(brush, infoBoxRectangle)
                    Else
                        Dim bl As New Blend(2)
                        bl.Factors = New Single() {0.3F, 1.0F}
                        bl.Positions = New Single() {0.0F, 1.0F}
                        brush.Blend = bl
                        gr.FillRectangle(brush, infoBoxRectangle)
                    End If
                End Using
            End If
        End Sub

        Protected Overridable Sub OnValueChanged()
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End Sub

        Protected Overridable Sub OnDockingModeChanged(ByVal e As EventArgs)
            RaiseEvent DockingModeChanged(Me, e)
        End Sub

        Protected Overridable Sub OnEasyProgressBarClosed(ByVal e As EventArgs)
            RaiseEvent EasyProgressBarClosed(Me, e)
        End Sub

        Protected Overridable Function OnEasyProgressBarClosing() As Boolean
            Dim result As Boolean = False
            RaiseEvent EasyProgressBarClosing(Me, result)

            Return result
        End Function

#End Region

#Region "Override Methods"

        Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
            DrawBackground(pe.Graphics)
            DrawProgress(pe.Graphics)
            DrawDigitBox(pe.Graphics)
            DrawText(pe.Graphics)
            DrawBorder(pe.Graphics)
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            MyBase.OnResize(e)
            GenerateProgressBar()
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)

            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                If Not DesignMode AndAlso Not isDocked Then
                    User32.ReleaseCapture()
                    User32.SendMessage(Me.Handle, CInt(User32.Msgs.WM_NCLBUTTONDOWN), CInt(User32._HT_CAPTION), 0)
                End If
            End If
        End Sub

        Protected Overrides Sub OnRightToLeftChanged(ByVal e As EventArgs)
            MyBase.OnRightToLeftChanged(e)

            If Not isDocked Then
                User32.SetParent(Handle, IntPtr.Zero)
                User32.SetWindowLong(Handle, CInt(User32.WindowExStyles.GWL_STYLE), User32.GetWindowLong(Handle, CInt(User32.WindowExStyles.GWL_STYLE)) Xor &H40000000)
                If m_alphaMaker IsNot Nothing Then
                    m_isLayered = Not m_isLayered
                    m_alphaMaker.IFloatWindowControl = Me
                    m_alphaMaker.SetLayered(m_isLayered)
                End If
            End If
        End Sub

        ''' <summary>
        ''' If IsUserInteraction property is enabled, executes this method operations.
        ''' </summary>
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If m_isUserInteraction Then
                Try
                    Select Case keyData
                        ' Changes to maximum value.
                        Case Keys.[End]
                            Me.Value = Me.Maximum
                            Exit Select
                            ' Changes to minimum value.
                        Case Keys.Home
                            Me.Value = Me.Minimum
                            Exit Select
                            ' Decrease the current value on the control.
                        Case Keys.Left
                            Me.Value -= 1
                            Exit Select
                            ' Increase the current value on the control.
                        Case Keys.Right
                            Me.Value += 1
                            Exit Select
                        Case Keys.[Return]
                            If MessageBox.Show("Do you want to re-docking this control?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                DockUndockProgressBar = Not DockUndockProgressBar
                            End If
                            Exit Select
                        Case Keys.F1
                            Exit Select
                    End Select
                Catch
                End Try
            End If

            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = CInt(User32.Msgs.WM_CLOSE) Then
                ' Fire a EasyProgressBarClosing Event.
                If OnEasyProgressBarClosing() Then
                    m.Result = New IntPtr(1)
                    Return
                End If

                ' Fire a EasyProgressBarClosed Event.
                OnEasyProgressBarClosed(EventArgs.Empty)
            End If

            MyBase.WndProc(m)
            If m.Msg = CInt(User32.Msgs.WM_SYSCOMMAND) Then
                Dim wParam As Integer = m.WParam.ToInt32()
                If wParam = SYSTEM_MENUITEMS(0) Then
                    m_digitBoxGradient.DigitalNumberSide = GradientDigitBox.DigitalNumberLayout.LeftSide

                    Dim menuHandle As IntPtr = User32.GetSystemMenu(Me.Handle, False)
                    User32.RemoveMenu(menuHandle, 0, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_REMOVE)

                    Dim subMenu As IntPtr = User32.CreatePopupMenu()
                    User32.AppendMenu(subMenu, If(m_digitBoxGradient.DigitalNumberSide <> GradientDigitBox.DigitalNumberLayout.LeftSide, User32.MenuFlags.MF_STRING, User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED Or User32.MenuFlags.MF_CHECKED), SYSTEM_MENUITEMS(0), "Left Side")
                    User32.AppendMenu(subMenu, If(m_digitBoxGradient.DigitalNumberSide <> GradientDigitBox.DigitalNumberLayout.RightSide, User32.MenuFlags.MF_STRING, User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED Or User32.MenuFlags.MF_CHECKED), SYSTEM_MENUITEMS(1), "Right Side")
                    User32.InsertMenu(menuHandle, 0, If(m_isDigitDrawEnabled, User32.MenuFlags.MF_POPUP Or User32.MenuFlags.MF_BYPOSITION, User32.MenuFlags.MF_POPUP Or User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED), CUInt(subMenu), "DigitBox Layouts")
                ElseIf wParam = SYSTEM_MENUITEMS(1) Then
                    m_digitBoxGradient.DigitalNumberSide = GradientDigitBox.DigitalNumberLayout.RightSide

                    Dim menuHandle As IntPtr = User32.GetSystemMenu(Me.Handle, False)
                    User32.RemoveMenu(menuHandle, 0, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_REMOVE)

                    Dim subMenu As IntPtr = User32.CreatePopupMenu()
                    User32.AppendMenu(subMenu, If(m_digitBoxGradient.DigitalNumberSide <> GradientDigitBox.DigitalNumberLayout.LeftSide, User32.MenuFlags.MF_STRING, User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED Or User32.MenuFlags.MF_CHECKED), SYSTEM_MENUITEMS(0), "Left Side")
                    User32.AppendMenu(subMenu, If(m_digitBoxGradient.DigitalNumberSide <> GradientDigitBox.DigitalNumberLayout.RightSide, User32.MenuFlags.MF_STRING, User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED Or User32.MenuFlags.MF_CHECKED), SYSTEM_MENUITEMS(1), "Right Side")
                    User32.InsertMenu(menuHandle, 0, If(m_isDigitDrawEnabled, User32.MenuFlags.MF_POPUP Or User32.MenuFlags.MF_BYPOSITION, User32.MenuFlags.MF_POPUP Or User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_GRAYED Or User32.MenuFlags.MF_DISABLED), CUInt(subMenu), "DigitBox Layouts")
                ElseIf wParam = SYSTEM_MENUITEMS(2) Then
                    DockUndockProgressBar = Not DockUndockProgressBar
                ElseIf wParam = SYSTEM_MENUITEMS(3) Then
                    If m_alphaMaker IsNot Nothing Then
                        m_alphaMaker.IFloatWindowControl = Me
                        m_alphaMaker.SetLayered(m_isLayered)

                        Dim menuHandle As IntPtr = User32.GetSystemMenu(Me.Handle, False)
                        User32.RemoveMenu(menuHandle, 3, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_REMOVE)
                        User32.InsertMenu(menuHandle, 3, If(m_isLayered, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_CHECKED, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING), SYSTEM_MENUITEMS(3), [String].Format("Opacity {0}[{1}]", If(m_isLayered, "Enabled", "Disabled"), m_currentTransparency))
                    End If
                ElseIf wParam = SYSTEM_MENUITEMS(4) Then
                    If m_alphaMaker IsNot Nothing Then
                        Dim [error] As String = ""
                        m_alphaMaker.IFloatWindowControl = Me
                        While True
                            Dim result As String = Microsoft.VisualBasic.Interaction.InputBox([String].Format("Please enter a {0} opacity value here, {1}", [error], "the value must be in the range of 0 to 255."), "Seek To Opacity", m_targetTransparency.ToString(), -1, -1)

                            If result.Equals([String].Empty) Then
                                Exit While
                            End If

                            Try
                                Dim value As Byte = Convert.ToByte(result)

                                If Not m_isLayered Then
                                    m_alphaMaker.SetLayered(m_isLayered)
                                End If

                                m_alphaMaker.SeekToOpacity(value)

                                Dim menuHandle As IntPtr = User32.GetSystemMenu(Me.Handle, False)
                                User32.RemoveMenu(menuHandle, 3, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_REMOVE)
                                User32.InsertMenu(menuHandle, 3, If(m_isLayered, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING Or User32.MenuFlags.MF_CHECKED, User32.MenuFlags.MF_BYPOSITION Or User32.MenuFlags.MF_STRING), SYSTEM_MENUITEMS(3), [String].Format("Opacity {0}[{1}]", If(m_isLayered, "Enabled", "Disabled"), m_targetTransparency))

                                Exit Try
                            Catch generatedExceptionName As Exception
                                [error] = "valid"
                            End Try
                        End While
                    End If
                End If
            End If
        End Sub

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                RemoveHandler m_progressGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                m_progressGradient.Dispose()

                RemoveHandler m_backgroundGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                m_backgroundGradient.Dispose()

                RemoveHandler m_progressColorizer.ProgressColorizerChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                m_progressColorizer.Dispose()

                RemoveHandler m_digitBoxGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                m_digitBoxGradient.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#End Region

#Region "IProgressBar Members"

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Property HoverGradient() As GradientHover Implements IProgressBar.HoverGradient
            Get
                Throw New NotImplementedException()
            End Get
            Set(ByVal value As GradientHover)
                Throw New NotImplementedException()
            End Set
        End Property
        Private Function ShouldSerializeHoverGradient() As Boolean
            Return False
        End Function

        ''' <summary>
        ''' You can change the progress appearance from here.
        ''' </summary>
        <Description("You can change the progress appearance from here")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        <IsCloneable(True)> _
        Public Property ProgressGradient() As GradientProgress Implements IProgressBar.ProgressGradient
            Get
                Return m_progressGradient
            End Get
            Set(ByVal value As GradientProgress)
                Try
                    If Not value.Equals(m_progressGradient) Then
                        RemoveHandler m_progressGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                        m_progressGradient = value
                        AddHandler m_progressGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE

                        Invalidate()
                        Update()
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' You can change the background appearance from here.
        ''' </summary>
        <Description("You can change the background appearance from here")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        <IsCloneable(True)> _
        Public Property BackgroundGradient() As GradientBackground Implements IProgressBar.BackgroundGradient
            Get
                Return m_backgroundGradient
            End Get
            Set(ByVal value As GradientBackground)
                Try
                    If Not value.Equals(m_backgroundGradient) Then
                        RemoveHandler m_backgroundGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                        m_backgroundGradient = value
                        AddHandler m_backgroundGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE

                        Invalidate()
                        Update()
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' You can change the color components of the progress bitmap[RGBA Colorizer for progress indicator].
        ''' </summary>
        <Description("You can change the color components of the progress bitmap[RGBA Colorizer for progress indicator]")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        <IsCloneable(True)> _
        Public Property ProgressColorizer() As ColorizerProgress Implements IProgressBar.ProgressColorizer
            Get
                Return m_progressColorizer
            End Get
            Set(ByVal value As ColorizerProgress)
                Try
                    If Not value.Equals(m_progressColorizer) Then
                        RemoveHandler m_progressColorizer.ProgressColorizerChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                        m_progressColorizer = value
                        AddHandler m_progressColorizer.ProgressColorizerChanged, AddressOf CONTROL_INVALIDATE_UPDATE

                        Invalidate()
                        Update()
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' You can change the background appearance of the DigitBox rectangle from here.
        ''' </summary>
        <Description("You can change the background appearance of the DigitBox rectangle from here")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        <IsCloneable(True)> _
        Public Property DigitBoxGradient() As GradientDigitBox Implements IProgressBar.DigitBoxGradient
            Get
                Return m_digitBoxGradient
            End Get
            Set(ByVal value As GradientDigitBox)
                Try
                    If Not value.Equals(m_backgroundGradient) Then
                        RemoveHandler m_digitBoxGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE
                        m_digitBoxGradient = value
                        AddHandler m_digitBoxGradient.GradientChanged, AddressOf CONTROL_INVALIDATE_UPDATE

                        If m_isDigitDrawEnabled Then
                            Invalidate()
                            Update()
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the current progress value of the control.
        ''' </summary>
        <Description("Gets or Sets, the current progress value of the control")> _
        <DefaultValue(20)> _
        <Browsable(True)> _
        <Category("Progress")> _
        <IsCloneable(True)> _
        Public Property Value() As Integer Implements IProgressBar.Value
            Get
                Return progress
            End Get
            Set(ByVal value As Integer)
                If Not value.Equals(progress) Then
                    If value > m_maximum Then
                        Throw New ArgumentException("Value must be smaller than or equal to Maximum.")
                    End If
                    If value < m_minimum Then
                        Throw New ArgumentException("Value must be greater than or equal to Minimum.")
                    End If

                    progress = value

                    GenerateProgressBar()

                    ' Trigger a ValueChanged event.
                    OnValueChanged()

                    Invalidate()
                    Update()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the minimum progress value of the control.
        ''' </summary>
        <Description("Gets or Sets, the minimum progress value of the control")> _
        <DefaultValue(0)> _
        <Browsable(True)> _
        <Category("Progress")> _
        <IsCloneable(True)> _
        Public Property Minimum() As Integer Implements IProgressBar.Minimum
            Get
                Return m_minimum
            End Get
            Set(ByVal value As Integer)
                If Not value.Equals(m_minimum) Then
                    If value >= m_maximum Then
                        Throw New ArgumentException("Minimum must be smaller than Maximum.")
                    ElseIf value < 0 Then
                        Throw New ArgumentException("Minimum must be positive integer.")
                    End If

                    m_minimum = value

                    GenerateProgressBar()

                    Invalidate()
                    Update()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the maximum progress value of the control.
        ''' </summary>
        <Description("Gets or Sets, the maximum progress value of the control")> _
        <DefaultValue(100)> _
        <Browsable(True)> _
        <Category("Progress")> _
        <IsCloneable(True)> _
        Public Property Maximum() As Integer Implements IProgressBar.Maximum
            Get
                Return m_maximum
            End Get
            Set(ByVal value As Integer)
                If Not value.Equals(m_maximum) Then
                    If value <= m_minimum Then
                        Throw New ArgumentException("Maximum must be greater than Minimum.")
                    End If

                    m_maximum = value

                    GenerateProgressBar()

                    Invalidate()
                    Update()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the control's border is draw or not.
        ''' </summary>
        <Description("Determines whether the control's border is draw or not")> _
        <RefreshProperties(RefreshProperties.All)> _
        <DefaultValue(True)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property IsPaintBorder() As Boolean Implements IProgressBar.IsPaintBorder
            Get
                Return m_isPaintBorder
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(m_isPaintBorder) Then
                    m_isPaintBorder = value

                    Invalidate()
                    Update()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the digital number drawing is enabled or not.
        ''' </summary>
        <Description("Determines whether the digital number drawing is enabled or not")> _
        <RefreshProperties(RefreshProperties.All)> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property IsDigitDrawEnabled() As Boolean Implements IProgressBar.IsDigitDrawEnabled
            Get
                Return m_isDigitDrawEnabled
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(m_isDigitDrawEnabled) Then
                    m_isDigitDrawEnabled = value

                    Invalidate()
                    Update()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the percentage text is show or hide.
        ''' </summary>
        <Description("Determines whether the percentage text is show or hide")> _
        <RefreshProperties(RefreshProperties.All)> _
        <DefaultValue(True)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property ShowPercentage() As Boolean Implements IProgressBar.ShowPercentage
            Get
                Return m_showPercentage
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(m_showPercentage) Then
                    m_showPercentage = value

                    Invalidate()
                    Update()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Display text formatting for progressbar value.
        ''' </summary>
        <Description("Display text formatting for progressbar value")> _
        <DefaultValue("done")> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property DisplayFormat() As String Implements IProgressBar.DisplayFormat
            Get
                Return m_displayFormat
            End Get
            Set(ByVal value As String)
                Try
                    If Not value.Equals(m_displayFormat) Then
                        m_displayFormat = value

                        ' Update text on the control.
                        Dim result As String() = Me.Text.Split(New Char() {"%"c, " "c}, 2)
                        Me.Text = [String].Format("{0}% {1}", result(0), m_displayFormat)

                        If m_showPercentage Then
                            Invalidate()
                            Update()
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value or empty string.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the control's border color from here.
        ''' </summary>
        <Description("Gets or Sets, the control's border color from here")> _
        <DefaultValue(GetType(Color), "DarkGray")> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property BorderColor() As Color Implements IProgressBar.BorderColor
            Get
                Return m_borderColor
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(m_borderColor) Then
                    m_borderColor = value

                    If m_isPaintBorder Then
                        Invalidate()
                        Update()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the current border style of the ProgressBar control.
        ''' </summary>
        <Description("Gets or Sets, the current border style of the ProgressBar control")> _
        <RefreshProperties(RefreshProperties.All)> _
        <DefaultValue(GetType(EasyProgressBarBorderStyle), "Flat")> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property ControlBorderStyle() As EasyProgressBarBorderStyle Implements IProgressBar.ControlBorderStyle
            Get
                Return m_controlBorderStyle
            End Get
            Set(ByVal value As EasyProgressBarBorderStyle)
                If Not value.Equals(m_controlBorderStyle) Then
                    m_controlBorderStyle = value

                    If m_isPaintBorder Then
                        Invalidate()
                        Update()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Occurs when the progress value changed of the control.
        ''' </summary>
        <Description("Occurs when the progress value changed of the control")> _
        Public Event ValueChanged As EventHandler Implements IProgressBar.ValueChanged

#End Region

#Region "IFloatWindowBase Members"

        Public Function SetFloatWindowTaskbarText(ByVal controlHandle As IntPtr, ByVal text As String) As Boolean Implements IFloatWindowBase.SetFloatWindowTaskbarText
            Return User32.SetWindowText(controlHandle, text)
        End Function

        <Browsable(False)> _
        Public Property DockUndockProgressBar() As Boolean
            Get
                Return isDocked
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(isDocked) Then
                    isDocked = value
                    If Not isDocked Then
                        ' Save the current location.
                        oldLocation = Me.Location

                        ' Top of the control container.
                        parentContainer = Me.Parent
                        UpdateStyles()
                        Parent = Nothing
                        User32.SetParent(Handle, IntPtr.Zero)
                        User32.SetWindowLong(Handle, CInt(User32.WindowExStyles.GWL_STYLE), User32.GetWindowLong(Handle, CInt(User32.WindowExStyles.GWL_STYLE)) Xor &H40000000)

                        If m_alphaMaker IsNot Nothing AndAlso Not m_isLayered Then
                            m_alphaMaker.IFloatWindowControl = Me
                            m_alphaMaker.SetLayered(m_isLayered)
                        End If

                        ' Create System Menu Items
                        CreateSystemMenu()
                    Else
                        ' Clear System Menu Items
                        ResetSystemMenu()

                        ' If our alpha component is not null, we clear transparency attributes.
                        If m_alphaMaker IsNot Nothing AndAlso m_isLayered Then
                            m_alphaMaker.IFloatWindowControl = Me
                            m_alphaMaker.SetLayered(m_isLayered)
                        End If

                        UpdateStyles()
                        User32.SetWindowLong(Handle, CInt(User32.WindowExStyles.GWL_STYLE), User32.GetWindowLong(Handle, CInt(User32.WindowExStyles.GWL_STYLE)) Xor &H40000000)
                        Parent = parentContainer

                        'Re-Set old location to the control.
                        Me.Location = oldLocation
                    End If

                    ' Fire a DockingModeChanged Event.
                    OnDockingModeChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        Private Function ShouldSerializeDockUndockProgressBar() As Boolean
            Return False
        End Function

        <Browsable(False)> _
        Public ReadOnly Property DockParentContainer() As Control Implements IFloatWindowBase.DockParentContainer
            Get
                Return parentContainer
            End Get
        End Property

#End Region

#Region "IFloatWindowAlpha Members"

        ''' <summary>
        ''' Gets, the handle of the float window, whose window has a alpha support or to be.
        ''' </summary>
        <Browsable(False)> _
        Public ReadOnly Property FloatWindowHandle() As IntPtr Implements IFloatWindowAlphaMembers.FloatWindowHandle
            Get
                Return Handle
            End Get
        End Property

        ''' <summary>
        ''' Gets, the float window control is marked to layered flag or not.
        ''' </summary>
        <Browsable(False)> _
        Public ReadOnly Property IsLayered() As Boolean Implements IFloatWindowAlphaMembers.IsLayered
            Get
                Return m_isLayered
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets, the target opacity value of the float window.
        ''' </summary>
        <Browsable(False)> _
        Public Property TargetTransparency() As Byte Implements IFloatWindowAlphaMembers.TargetTransparency
            Get
                Return m_targetTransparency
            End Get
            Set(ByVal value As Byte)
                If Not value.Equals(m_targetTransparency) Then
                    m_targetTransparency = value
                End If
            End Set
        End Property

        Private Function ShouldSerializeTargetTransparency() As Boolean
            Return False
        End Function

        ''' <summary>
        ''' Gets or sets, the currently opacity value of the float window.
        ''' </summary>
        <Browsable(False)> _
        Public Property CurrentTransparency() As Byte Implements IFloatWindowAlphaMembers.CurrentTransparency
            Get
                Return m_currentTransparency
            End Get
            Set(ByVal value As Byte)
                If Not value.Equals(m_currentTransparency) Then
                    m_currentTransparency = value
                End If
            End Set
        End Property

        Private Function ShouldSerializeCurrentTransparency() As Boolean
            Return False
        End Function

#End Region

#Region "ICloneable Members"

        Public Function Clone() As Object
            Dim progressBar As eEasyProgressBar = TryCast(CustomControlsLogic.GetMyClone(Me), eEasyProgressBar)
            ' Set its some base class properties.
            progressBar.Font = Me.Font
            progressBar.ForeColor = Me.ForeColor

            Return progressBar
        End Function

#End Region
    End Class

    Public Class EasyProgressBarDesigner
        Inherits ControlDesigner
#Region "Instance Members"

        Private m_actionLists As DesignerActionListCollection

#End Region

#Region "Constructor"

        Protected Sub New()
            MyBase.New()
        End Sub

#End Region

#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Property"

        Public Overrides ReadOnly Property ActionLists() As DesignerActionListCollection
            Get
                If m_actionLists Is Nothing Then
                    m_actionLists = New DesignerActionListCollection()
                    m_actionLists.Add(New EasyProgressBarActionList(DirectCast(Control, eEasyProgressBar)))
                End If

                Return m_actionLists
            End Get
        End Property

#End Region

#Region "Override Methods"

        '  As a general rule, always call the base method first in the PreFilterXxx() methods and last in the 
        '            PostFilterXxx() methods. This way, all designer classes are given the proper opportunity to apply their 
        '            changes. The ControlDesigner and ComponentDesigner use these methods to add properties like Visible, 
        '            Enabled, Name, and Locked. 


        ''' <summary>
        ''' Override this method to remove unused or inappropriate events.
        ''' </summary>
        ''' <param name="events">Events collection of the control.</param>
        Protected Overrides Sub PostFilterEvents(ByVal events As System.Collections.IDictionary)
            events.Remove("EnabledChanged")
            events.Remove("PaddingChanged")
            events.Remove("RightToLeftChanged")
            events.Remove("BackgroundImageChanged")
            events.Remove("BackgroundImageLayoutChanged")

            MyBase.PostFilterEvents(events)
        End Sub

        ''' <summary>
        ''' Override this method to add some properties to the control or change the properties attributes for a dynamic user interface.
        ''' </summary>
        ''' <param name="properties">Properties collection of the control before than add a new property to the collection by user.</param>
        Protected Overrides Sub PreFilterProperties(ByVal properties As System.Collections.IDictionary)
            MyBase.PreFilterProperties(properties)

            ' We don't want to show the "Text" property to the end-users at the design-time.
            properties("Text") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("Text"), PropertyDescriptor), BrowsableAttribute.No)

            ' After than, we don't want to see some properties at the design-time for general reasons(Dynamic property attributes). 

            Dim progressBar As eEasyProgressBar = TryCast(Control, eEasyProgressBar)

            If progressBar IsNot Nothing Then
                If progressBar.ControlBorderStyle <> EasyProgressBarBorderStyle.Flat Then
                    properties("BorderColor") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("BorderColor"), PropertyDescriptor), BrowsableAttribute.No)
                End If

                If Not progressBar.IsPaintBorder Then
                    properties("BorderColor") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("BorderColor"), PropertyDescriptor), BrowsableAttribute.No)
                    properties("ControlBorderStyle") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("ControlBorderStyle"), PropertyDescriptor), BrowsableAttribute.No)
                End If

                If Not progressBar.ShowPercentage Then
                    properties("Font") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("Font"), PropertyDescriptor), BrowsableAttribute.No)
                    properties("ForeColor") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("ForeColor"), PropertyDescriptor), BrowsableAttribute.No)
                    properties("DisplayFormat") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("DisplayFormat"), PropertyDescriptor), BrowsableAttribute.No)
                End If

                If Not progressBar.IsDigitDrawEnabled Then
                    properties("DigitBoxGradient") = TypeDescriptor.CreateProperty(GetType(eEasyProgressBar), DirectCast(properties("DigitBoxGradient"), PropertyDescriptor), BrowsableAttribute.No)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Override this method to remove unused or inappropriate properties.
        ''' </summary>
        ''' <param name="properties">Properties collection of the control.</param>
        Protected Overrides Sub PostFilterProperties(ByVal properties As System.Collections.IDictionary)
            properties.Remove("Enabled")
            properties.Remove("Padding")
            properties.Remove("RightToLeft")
            properties.Remove("BackgroundImage")
            properties.Remove("BackgroundImageLayout")

            MyBase.PostFilterProperties(properties)
        End Sub

#End Region
    End Class

    Public Class EasyProgressBarActionList
        Inherits DesignerActionList
#Region "Instance Members"

        Private linkedControl As eEasyProgressBar
        Private designerService As DesignerActionUIService

#End Region

#Region "Constructor"

        ' The constructor associates the control to the smart tag action list.
        Public Sub New(ByVal control As eEasyProgressBar)
            MyBase.New(control)
            linkedControl = control
            designerService = DirectCast(GetService(GetType(DesignerActionUIService)), DesignerActionUIService)

            ' When this control will be added to the design area, the smart tag panel will open automatically.
            Me.AutoShow = True
        End Sub

#End Region

#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Property - DesignerActionPropertyItem"

        ' Property that is target of a DesignerActionPropertyItem. 


        Public Property Value() As Integer
            Get
                Return linkedControl.Value
            End Get
            Set(ByVal value As Integer)
                GetPropertyByName("Value", False).SetValue(linkedControl, value)
            End Set
        End Property

#End Region

#Region "Override Methods"

        ' Implementation of this abstract method creates smart tag 
        '           items, associates their targets, and collects into list. 


        Public Overrides Function GetSortedActionItems() As DesignerActionItemCollection
            Dim items As New DesignerActionItemCollection()
            Try
                ' Creating the action list static headers.
                items.Add(New DesignerActionHeaderItem("Progress"))
                items.Add(New DesignerActionHeaderItem("Appearance"))

                items.Add(New DesignerActionPropertyItem("Value", "Value", "Progress", "Sets, the current progress value of the control."))

                items.Add(New DesignerActionMethodItem(Me, "IsColorizerEnabled", "Is Colorizer Enabled: " + (If(linkedControl.ProgressColorizer.IsColorizerEnabled, "ON", "OFF")), "Appearance", "Determines whether the colorizer effect is enable or not for progress bitmap.", False))

                items.Add(New DesignerActionMethodItem(Me, "IsTransparencyEnabled", "Is Transparency Enabled: " + (If(linkedControl.ProgressColorizer.IsTransparencyEnabled, "ON", "OFF")), "Appearance", "Determines whether the transparency effect is visible or not for progress bitmap.", False))

                ' Add a new static header and its items.
                items.Add(New DesignerActionHeaderItem("Information"))
                items.Add(New DesignerActionTextItem("X: " + linkedControl.Location.X + ", " + "Y: " + linkedControl.Location.Y, "Information"))
                items.Add(New DesignerActionTextItem("Width: " + linkedControl.Size.Width + ", " + "Height: " + linkedControl.Size.Height, "Information"))
            Catch ex As Exception
                MessageBox.Show("Exception while generating the action list panel for this EasyProgressBar, " + ex.Message)
            End Try

            Return items
        End Function

#End Region

#Region "Helper Methods"

        ' This helper method to retrieve control properties.
        '           GetProperties ensures undo and menu updates to work properly. 


        Private Function GetPropertyByName(ByVal propName As [String], ByVal isChildren As Boolean) As PropertyDescriptor
            If propName IsNot Nothing Then
                Dim prop As PropertyDescriptor = If(isChildren, TypeDescriptor.GetProperties(linkedControl.ProgressColorizer)(propName), TypeDescriptor.GetProperties(linkedControl)(propName))

                If prop IsNot Nothing Then
                    Return prop
                Else
                    Throw New ArgumentException("Property name not found!", propName)
                End If
            Else
                Throw New ArgumentNullException("Property name cannot be null!")
            End If
        End Function

#End Region

#Region "General Methods - DesignerActionMethodItem"

        ' Methods that are targets of DesignerActionMethodItem entries. 


        Public Sub IsColorizerEnabled()
            Try
                GetPropertyByName("IsColorizerEnabled", True).SetValue(linkedControl.ProgressColorizer, Not linkedControl.ProgressColorizer.IsColorizerEnabled)
                designerService.Refresh(Component)
            Catch ex As Exception
                MessageBox.Show("Exception while changing the value of the IsColorizerEnabled property, " + ex.Message)
            End Try
        End Sub

        Public Sub IsTransparencyEnabled()
            Try
                GetPropertyByName("IsTransparencyEnabled", True).SetValue(linkedControl.ProgressColorizer, Not linkedControl.ProgressColorizer.IsTransparencyEnabled)
                designerService.Refresh(Component)
            Catch ex As Exception
                MessageBox.Show("Exception while changing the value of the IsTransparencyEnabled property, " + ex.Message)
            End Try
        End Sub

#End Region
    End Class
End Namespace
