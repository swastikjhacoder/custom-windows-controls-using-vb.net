Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports PushButtonState = System.Windows.Forms.VisualStyles.PushButtonState

Namespace UIControls.Button
    ''' <summary> 
    ''' Represents a glass button control. 
    ''' </summary> 
    <ToolboxBitmap(GetType(Windows.Forms.Button)), ToolboxItem(True), ToolboxItemFilter("System.Windows.Forms"), Description("Raises an event when the user clicks it.")> _
    Partial Public Class eGlassButton
        Inherits Windows.Forms.Button

#Region " Constructors "
        Public Sub New()
            InitializeComponent()
            timer.Interval = animationLength \ framesCount
            MyBase.BackColor = Color.Transparent
            BackColor = Color.Black
            ForeColor = Color.White
            OuterBorderColor = Color.White
            InnerBorderColor = Color.Black
            ShineColor = Color.White
            GlowColor = Color.FromArgb(-7488001) 'unchecked((int)(0xFF8DBDFF))); 
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.Opaque, False)
        End Sub

#End Region

#Region " Fields and Properties "

        Private _backColor As Color
        ''' <summary> 
        ''' Gets or sets the background color of the control. 
        ''' </summary> 
        ''' <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns> 
        <DefaultValue(GetType(Color), "Black")> _
        Public Overridable Shadows Property BackColor() As Color
            Get
                Return _backColor
            End Get
            Set(ByVal value As Color)
                If Not _backColor.Equals(value) Then
                    _backColor = value
                    UseVisualStyleBackColor = False
                    CreateFrames()
                    OnBackColorChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary> 
        ''' Gets or sets the foreground color of the control. 
        ''' </summary> 
        ''' <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control.</returns> 
        <DefaultValue(GetType(Color), "White")> _
        Public Overridable Shadows Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        Private _innerBorderColor As Color
        ''' <summary> 
        ''' Gets or sets the inner border color of the control. 
        ''' </summary> 
        ''' <returns>A <see cref="T:System.Drawing.Color" /> value representing the color of the inner border.</returns> 
        <DefaultValue(GetType(Color), "Black"), Category("Appearance"), Description("The inner border color of the control.")> _
        Public Overridable Property InnerBorderColor() As Color
            Get
                Return _innerBorderColor
            End Get
            Set(ByVal value As Color)
                If _innerBorderColor <> value Then
                    _innerBorderColor = value
                    CreateFrames()
                    If IsHandleCreated Then
                        Invalidate()
                    End If
                    OnInnerBorderColorChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        Private _outerBorderColor As Color
        ''' <summary> 
        ''' Gets or sets the outer border color of the control. 
        ''' </summary> 
        ''' <returns>A <see cref="T:System.Drawing.Color" /> value representing the color of the outer border.</returns> 
        <DefaultValue(GetType(Color), "White"), Category("Appearance"), Description("The outer border color of the control.")> _
        Public Overridable Property OuterBorderColor() As Color
            Get
                Return _outerBorderColor
            End Get
            Set(ByVal value As Color)
                If _outerBorderColor <> value Then
                    _outerBorderColor = value
                    CreateFrames()
                    If IsHandleCreated Then
                        Invalidate()
                    End If
                    OnOuterBorderColorChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        Private _shineColor As Color
        ''' <summary> 
        ''' Gets or sets the shine color of the control. 
        ''' </summary> 
        ''' <returns>A <see cref="T:System.Drawing.Color" /> value representing the shine color.</returns> 
        <DefaultValue(GetType(Color), "White"), Category("Appearance"), Description("The shine color of the control.")> _
        Public Overridable Property ShineColor() As Color
            Get
                Return _shineColor
            End Get
            Set(ByVal value As Color)
                If _shineColor <> value Then
                    _shineColor = value
                    CreateFrames()
                    If IsHandleCreated Then
                        Invalidate()
                    End If
                    OnShineColorChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        Private _glowColor As Color
        ''' <summary> 
        ''' Gets or sets the glow color of the control. 
        ''' </summary> 
        ''' <returns>A <see cref="T:System.Drawing.Color" /> value representing the glow color.</returns> 
        <DefaultValue(GetType(Color), "255,141,189,255"), Category("Appearance"), Description("The glow color of the control.")> _
        Public Overridable Property GlowColor() As Color
            Get
                Return _glowColor
            End Get
            Set(ByVal value As Color)
                If _glowColor <> value Then
                    _glowColor = value
                    CreateFrames()
                    If IsHandleCreated Then
                        Invalidate()
                    End If
                    OnGlowColorChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        Private _fadeOnFocus As Boolean
        ''' <summary> 
        ''' Gets or sets a value indicating whether the button should fade in and fade out when it's getting and loosing the focus. 
        ''' </summary> 
        ''' <value><c>true</c> if fading with changing the focus; otherwise, <c>false</c>.</value> 
        <DefaultValue(False), Category("Appearance"), Description("Indicates whether the button should fade in and fade out when it is getting and loosing the focus.")> _
        Public Overridable Property FadeOnFocus() As Boolean
            Get
                Return _fadeOnFocus
            End Get
            Set(ByVal value As Boolean)
                If _fadeOnFocus <> value Then
                    _fadeOnFocus = value
                End If
            End Set
        End Property

        Private _isHovered As Boolean
        Private _isFocused As Boolean
        Private _isFocusedByKey As Boolean
        Private _isKeyDown As Boolean
        Private _isMouseDown As Boolean
        Private ReadOnly Property IsPressed() As Boolean
            Get
                Return _isKeyDown OrElse (_isMouseDown AndAlso _isHovered)
            End Get
        End Property

        ''' <summary> 
        ''' Gets the state of the button control. 
        ''' </summary> 
        ''' <value>The state of the button control.</value> 
        <Browsable(False)> _
        Public ReadOnly Property State() As PushButtonState
            Get
                If Not Enabled Then
                    Return PushButtonState.Disabled
                End If
                If IsPressed Then
                    Return PushButtonState.Pressed
                End If
                If _isHovered Then
                    Return PushButtonState.Hot
                End If
                If _isFocused OrElse IsDefault Then
                    Return PushButtonState.Default
                End If
                Return PushButtonState.Normal
            End Get
        End Property

#End Region

#Region " Events "

        ''' <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.InnerBorderColor" /> property changes.</summary> 
        <Description("Event raised when the value of the InnerBorderColor property is changed."), Category("Property Changed")> _
        Public Event InnerBorderColorChanged As EventHandler

        ''' <summary> 
        ''' Raises the <see cref="E:Glass.GlassButton.InnerBorderColorChanged" /> event. 
        ''' </summary> 
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
        Protected Overridable Sub OnInnerBorderColorChanged(ByVal e As EventArgs)
            RaiseEvent InnerBorderColorChanged(Me, e)
        End Sub

        ''' <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.OuterBorderColor" /> property changes.</summary> 
        <Description("Event raised when the value of the OuterBorderColor property is changed."), Category("Property Changed")> _
        Public Event OuterBorderColorChanged As EventHandler

        ''' <summary> 
        ''' Raises the <see cref="E:Glass.GlassButton.OuterBorderColorChanged" /> event. 
        ''' </summary> 
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
        Protected Overridable Sub OnOuterBorderColorChanged(ByVal e As EventArgs)
            RaiseEvent OuterBorderColorChanged(Me, e)
        End Sub

        ''' <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.ShineColor" /> property changes.</summary> 
        <Description("Event raised when the value of the ShineColor property is changed."), Category("Property Changed")> _
        Public Event ShineColorChanged As EventHandler

        ''' <summary> 
        ''' Raises the <see cref="E:Glass.GlassButton.ShineColorChanged" /> event. 
        ''' </summary> 
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
        Protected Overridable Sub OnShineColorChanged(ByVal e As EventArgs)
            RaiseEvent ShineColorChanged(Me, e)
        End Sub

        ''' <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.GlowColor" /> property changes.</summary> 
        <Description("Event raised when the value of the GlowColor property is changed."), Category("Property Changed")> _
        Public Event GlowColorChanged As EventHandler

        ''' <summary> 
        ''' Raises the <see cref="E:Glass.GlassButton.GlowColorChanged" /> event. 
        ''' </summary> 
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
        Protected Overridable Sub OnGlowColorChanged(ByVal e As EventArgs)
            RaiseEvent GlowColorChanged(Me, e)
        End Sub

#End Region

#Region " Overrided Methods "

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event. 
        ''' </summary> 
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            CreateFrames()
            MyBase.OnSizeChanged(e)
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event. 
        ''' </summary> 
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param> 
        Protected Overloads Overrides Sub OnClick(ByVal e As EventArgs)
            _isKeyDown = False
            _isMouseDown = False
            MyBase.OnClick(e)
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event. 
        ''' </summary> 
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnEnter(ByVal e As EventArgs)
            _isFocused = True
            _isFocusedByKey = True
            MyBase.OnEnter(e)
            If _fadeOnFocus Then
                FadeIn()
            End If
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event. 
        ''' </summary> 
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnLeave(ByVal e As EventArgs)
            MyBase.OnLeave(e)
            _isFocused = False
            _isFocusedByKey = False
            _isKeyDown = False
            _isMouseDown = False
            Invalidate()
            If _fadeOnFocus Then
                FadeOut()
            End If
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event. 
        ''' </summary> 
        ''' <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
            If e.KeyCode = Keys.Space Then
                _isKeyDown = True
                Invalidate()
            End If
            MyBase.OnKeyDown(e)
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event. 
        ''' </summary> 
        ''' <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnKeyUp(ByVal e As KeyEventArgs)
            If _isKeyDown AndAlso e.KeyCode = Keys.Space Then
                _isKeyDown = False
                Invalidate()
            End If
            MyBase.OnKeyUp(e)
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event. 
        ''' </summary> 
        ''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            If Not _isMouseDown AndAlso e.Button = MouseButtons.Left Then
                _isMouseDown = True
                _isFocusedByKey = False
                Invalidate()
            End If
            MyBase.OnMouseDown(e)
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event. 
        ''' </summary> 
        ''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            If _isMouseDown Then
                _isMouseDown = False
                Invalidate()
            End If
            MyBase.OnMouseUp(e)
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event. 
        ''' </summary> 
        ''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            If e.Button <> MouseButtons.None Then
                If Not ClientRectangle.Contains(e.X, e.Y) Then
                    If _isHovered Then
                        _isHovered = False
                        Invalidate()
                    End If
                ElseIf Not _isHovered Then
                    _isHovered = True
                    Invalidate()
                End If
            End If
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event. 
        ''' </summary> 
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param> 
        Protected Overloads Overrides Sub OnMouseEnter(ByVal e As EventArgs)
            _isHovered = True
            FadeIn()
            Invalidate()
            MyBase.OnMouseEnter(e)
        End Sub

        ''' <summary> 
        ''' Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event. 
        ''' </summary> 
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param> 
        Protected Overloads Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            _isHovered = False
            If Not (Me.FadeOnFocus AndAlso _isFocusedByKey) Then FadeOut()
            Invalidate()
            MyBase.OnMouseLeave(e)
        End Sub

#End Region

#Region " Painting "

        ''' <summary> 
        ''' Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event. 
        ''' </summary> 
        ''' <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param> 
        Protected Overloads Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            DrawButtonBackgroundFromBuffer(e.Graphics)
            DrawForegroundFromButton(e)
            DrawButtonForeground(e.Graphics)

            RaiseEvent Paint(Me, e)
        End Sub

        ''' <summary> 
        ''' Occurs when the control is redrawn. 
        ''' </summary> 
        Public Shadows Event Paint As PaintEventHandler

        Private Sub DrawButtonBackgroundFromBuffer(ByVal graphics As Graphics)
            Dim frame As Integer
            If Not Enabled Then
                frame = FRAME_DISABLED
            ElseIf IsPressed Then
                frame = FRAME_PRESSED
            ElseIf Not IsAnimating AndAlso _currentFrame = 0 Then
                frame = FRAME_NORMAL
            Else
                If Not HasAnimationFrames Then
                    CreateFrames(True)
                End If
                frame = FRAME_ANIMATED + _currentFrame
            End If
            If _frames Is Nothing OrElse _frames.Count = 0 Then
                CreateFrames()
            End If
            graphics.DrawImage(_frames(frame), Point.Empty)
        End Sub

        Private Function CreateBackgroundFrame(ByVal pressed As Boolean, ByVal hovered As Boolean, ByVal animating As Boolean, ByVal enabled As Boolean, ByVal glowOpacity As Single) As Image
            Dim rect As Rectangle = ClientRectangle
            If rect.Width <= 0 Then
                rect.Width = 1
            End If
            If rect.Height <= 0 Then
                rect.Height = 1
            End If
            Dim img As Image = New Bitmap(rect.Width, rect.Height)
            Using g As Graphics = Graphics.FromImage(img)
                g.Clear(Color.Transparent)
                DrawButtonBackground(g, rect, pressed, hovered, animating, enabled, _
                _outerBorderColor, _backColor, _glowColor, _shineColor, _innerBorderColor, glowOpacity)
            End Using
            Return img
        End Function

        Private Shared Sub DrawButtonBackground(ByVal g As Graphics, ByVal rectangle As Rectangle, ByVal pressed As Boolean, ByVal hovered As Boolean, ByVal animating As Boolean, ByVal enabled As Boolean, _
        ByVal outerBorderColor As Color, ByVal backColor As Color, ByVal glowColor As Color, ByVal shineColor As Color, ByVal innerBorderColor As Color, ByVal glowOpacity As Single)
            Dim sm As SmoothingMode = g.SmoothingMode
            g.SmoothingMode = SmoothingMode.AntiAlias

            ' white border
            Dim rect As Rectangle = rectangle
            rect.Width -= 1
            rect.Height -= 1
            Using bw As GraphicsPath = CreateRoundRectangle(rect, 4)
                Using p As New Pen(outerBorderColor)
                    g.DrawPath(p, bw)
                End Using
            End Using

            rect.X += 1
            rect.Y += 1
            rect.Width -= 2
            rect.Height -= 2
            Dim rect2 As Rectangle = rect
            rect2.Height >>= 1

            ' content
            Using bb As GraphicsPath = CreateRoundRectangle(rect, 2)
                Dim opacity As Integer = [If](Of Integer)(pressed, &HCC, &H7F)
                Using br As Brush = New SolidBrush(Color.FromArgb(opacity, backColor))
                    g.FillPath(br, bb)
                End Using
            End Using

            ' glow
            If (hovered OrElse animating) AndAlso Not pressed Then
                Using clip As GraphicsPath = CreateRoundRectangle(rect, 2)
                    g.SetClip(clip, CombineMode.Intersect)
                    Using brad As GraphicsPath = CreateBottomRadialPath(rect)
                        Using pgr As New PathGradientBrush(brad)
                            Dim opacity As Integer = Convert.ToInt32(&HB2 * glowOpacity + 0.5F)
                            Dim bounds As RectangleF = brad.GetBounds()
                            pgr.CenterPoint = New PointF((bounds.Left + bounds.Right) / 2.0F, (bounds.Top + bounds.Bottom) / 2.0F)
                            pgr.CenterColor = Color.FromArgb(opacity, glowColor)
                            pgr.SurroundColors = New Color() {Color.FromArgb(0, glowColor)}
                            g.FillPath(pgr, brad)
                        End Using
                    End Using
                    g.ResetClip()
                End Using
            End If

            ' shine
            If rect2.Width > 0 AndAlso rect2.Height > 0 Then
                rect2.Height += 1
                Using bh As GraphicsPath = CreateTopRoundRectangle(rect2, 2)
                    rect2.Height += 1
                    Dim opacity As Integer = &H99
                    If pressed Or Not enabled Then
                        opacity = Convert.ToInt32(0.4F * opacity + 0.5F)
                    End If
                    Using br As New LinearGradientBrush(rect2, Color.FromArgb(opacity, shineColor), Color.FromArgb(opacity \ 3, shineColor), LinearGradientMode.Vertical)
                        g.FillPath(br, bh)
                    End Using
                End Using
                rect2.Height -= 2
            End If

            ' black border
            Using bb As GraphicsPath = CreateRoundRectangle(rect, 3)
                Using p As New Pen(innerBorderColor)
                    g.DrawPath(p, bb)
                End Using
            End Using

            g.SmoothingMode = sm
        End Sub

        Private Sub DrawButtonForeground(ByVal g As Graphics)
            If Focused AndAlso ShowFocusCues Then
                ' && isFocusedByKey 
                Dim rect As Rectangle = ClientRectangle
                rect.Inflate(-4, -4)
                ControlPaint.DrawFocusRectangle(g, rect)
            End If
        End Sub

        Private _imageButton As Windows.Forms.Button
        Private Sub DrawForegroundFromButton(ByVal pevent As PaintEventArgs)
            If _imageButton Is Nothing Then
                _imageButton = New Windows.Forms.Button()
                _imageButton.Parent = New TransparentControl()
                _imageButton.SuspendLayout()
                _imageButton.BackColor = Color.Transparent
                _imageButton.FlatAppearance.BorderSize = 0
                _imageButton.FlatStyle = FlatStyle.Flat
            Else
                _imageButton.SuspendLayout()
            End If
            _imageButton.AutoEllipsis = AutoEllipsis
            If Enabled Then
                _imageButton.ForeColor = ForeColor
            Else
                _imageButton.ForeColor = Color.FromArgb((3 * ForeColor.R + _backColor.R) >> 2, (3 * ForeColor.G + _backColor.G) >> 2, (3 * ForeColor.B + _backColor.B) >> 2)
            End If
            _imageButton.Font = Font
            _imageButton.RightToLeft = RightToLeft
            _imageButton.Image = Image
            If Image IsNot Nothing AndAlso Not Enabled Then
                Dim size As Size = Image.Size
                Dim newColorMatrix As Single()() = New Single(4)() {}
                newColorMatrix(0) = New Single() {0.2125F, 0.2125F, 0.2125F, 0.0F, 0.0F}
                newColorMatrix(1) = New Single() {0.2577F, 0.2577F, 0.2577F, 0.0F, 0.0F}
                newColorMatrix(2) = New Single() {0.0361F, 0.0361F, 0.0361F, 0.0F, 0.0F}
                Dim arr As Single() = New Single(4) {}
                arr(3) = 1.0F
                newColorMatrix(3) = arr
                newColorMatrix(4) = New Single() {0.38F, 0.38F, 0.38F, 0.0F, 1.0F}
                Dim matrix As New System.Drawing.Imaging.ColorMatrix(newColorMatrix)
                Dim disabledImageAttr As New System.Drawing.Imaging.ImageAttributes()
                disabledImageAttr.ClearColorKey()
                disabledImageAttr.SetColorMatrix(matrix)
                _imageButton.Image = New Bitmap(Image.Width, Image.Height)
                Using gr As Graphics = Graphics.FromImage(_imageButton.Image)
                    gr.DrawImage(Image, New Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, disabledImageAttr)
                End Using
            End If
            _imageButton.ImageAlign = ImageAlign
            _imageButton.ImageIndex = ImageIndex
            _imageButton.ImageKey = ImageKey
            _imageButton.ImageList = ImageList
            _imageButton.Padding = Padding
            _imageButton.Size = Size
            _imageButton.Text = Text
            _imageButton.TextAlign = TextAlign
            _imageButton.TextImageRelation = TextImageRelation
            _imageButton.UseCompatibleTextRendering = UseCompatibleTextRendering
            _imageButton.UseMnemonic = UseMnemonic
            _imageButton.ResumeLayout()
            InvokePaint(_imageButton, pevent)
            If _imageButton.Image IsNot Nothing AndAlso _imageButton.Image IsNot Image Then
                _imageButton.Image.Dispose()
                _imageButton.Image = Nothing
            End If
        End Sub

        Private Class TransparentControl
            Inherits Control
            Protected Overloads Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
            End Sub
            Protected Overloads Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            End Sub
        End Class

        Private Shared Function CreateRoundRectangle(ByVal rectangle As Rectangle, ByVal radius As Integer) As GraphicsPath
            Dim path As New GraphicsPath()
            Dim l As Integer = rectangle.Left
            Dim t As Integer = rectangle.Top
            Dim w As Integer = rectangle.Width
            Dim h As Integer = rectangle.Height
            Dim d As Integer = radius << 1
            path.AddArc(l, t, d, d, 180, 90) ' topleft
            path.AddLine(l + radius, t, l + w - radius, t) ' top
            path.AddArc(l + w - d, t, d, d, 270, 90) ' topright
            path.AddLine(l + w, t + radius, l + w, t + h - radius) ' right
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90) ' bottomright
            path.AddLine(l + w - radius, t + h, l + radius, t + h) ' bottom
            path.AddArc(l, t + h - d, d, d, 90, 90) ' bottomleft
            path.AddLine(l, t + h - radius, l, t + radius) ' left
            path.CloseFigure()
            Return path
        End Function

        Private Shared Function CreateTopRoundRectangle(ByVal rectangle As Rectangle, ByVal radius As Integer) As GraphicsPath
            Dim path As New GraphicsPath()
            Dim l As Integer = rectangle.Left
            Dim t As Integer = rectangle.Top
            Dim w As Integer = rectangle.Width
            Dim h As Integer = rectangle.Height
            Dim d As Integer = radius << 1
            path.AddArc(l, t, d, d, 180, 90) ' topleft
            path.AddLine(l + radius, t, l + w - radius, t) ' top
            path.AddArc(l + w - d, t, d, d, 270, 90) ' topright
            path.AddLine(l + w, t + radius, l + w, t + h) ' right
            path.AddLine(l + w, t + h, l, t + h) ' bottom
            path.AddLine(l, t + h, l, t + radius) ' left
            path.CloseFigure()
            Return path
        End Function

        Private Shared Function CreateBottomRadialPath(ByVal rectangle As Rectangle) As GraphicsPath
            Dim path As New GraphicsPath()
            Dim rect As RectangleF = rectangle
            rect.X -= rect.Width * 0.35F
            rect.Y -= rect.Height * 0.15F
            rect.Width *= 1.7F
            rect.Height *= 2.3F
            path.AddEllipse(rect)
            path.CloseFigure()
            Return path
        End Function

#End Region

#Region " Unused Properties & Events "

        ''' <summary>This property is not relevant for this class.</summary> 
        ''' <returns>This property is not relevant for this class.</returns> 
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows ReadOnly Property FlatAppearance() As FlatButtonAppearance
            Get
                Return MyBase.FlatAppearance
            End Get
        End Property

        ''' <summary>This property is not relevant for this class.</summary> 
        ''' <returns>This property is not relevant for this class.</returns> 
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property FlatStyle() As FlatStyle
            Get
                Return MyBase.FlatStyle
            End Get
            Set(ByVal value As FlatStyle)
                MyBase.FlatStyle = value
            End Set
        End Property

        ''' <summary>This property is not relevant for this class.</summary> 
        ''' <returns>This property is not relevant for this class.</returns> 
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property UseVisualStyleBackColor() As Boolean
            Get
                Return MyBase.UseVisualStyleBackColor
            End Get
            Set(ByVal value As Boolean)
                MyBase.UseVisualStyleBackColor = value
            End Set
        End Property

#End Region

#Region " Animation Support "

        Private _frames As List(Of Image)

        Private Const FRAME_DISABLED As Integer = 0
        Private Const FRAME_PRESSED As Integer = 1
        Private Const FRAME_NORMAL As Integer = 2
        Private Const FRAME_ANIMATED As Integer = 3

        Private ReadOnly Property HasAnimationFrames() As Boolean
            Get
                Return _frames IsNot Nothing AndAlso _frames.Count > FRAME_ANIMATED
            End Get
        End Property

        Private Sub CreateFrames()
            CreateFrames(False)
        End Sub

        Private Sub CreateFrames(ByVal withAnimationFrames As Boolean)
            DestroyFrames()
            If Not IsHandleCreated Then
                Return
            End If
            If _frames Is Nothing Then
                _frames = New List(Of Image)()
            End If
            _frames.Add(CreateBackgroundFrame(False, False, False, False, 0))
            _frames.Add(CreateBackgroundFrame(True, True, False, True, 0))
            _frames.Add(CreateBackgroundFrame(False, False, False, True, 0))
            If Not withAnimationFrames Then
                Return
            End If
            For i As Integer = 0 To framesCount - 1
                _frames.Add(CreateBackgroundFrame(False, True, True, True, Convert.ToSingle(i) / (framesCount - 1.0F)))
            Next
        End Sub

        Private Sub DestroyFrames()
            If _frames IsNot Nothing Then
                While _frames.Count > 0
                    _frames(_frames.Count - 1).Dispose()
                    _frames.RemoveAt(_frames.Count - 1)
                End While
            End If
        End Sub

        Private Const animationLength As Integer = 300
        Private Const framesCount As Integer = 10
        Private _currentFrame As Integer
        Private _direction As Integer

        Private ReadOnly Property IsAnimating() As Boolean
            Get
                Return _direction <> 0
            End Get
        End Property

        Private Sub FadeIn()
            _direction = 1
            timer.Enabled = True
        End Sub

        Private Sub FadeOut()
            _direction = -1
            timer.Enabled = True
        End Sub

        Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer.Tick
            If Not timer.Enabled Then
                Return
            End If
            Refresh()
            _currentFrame += _direction
            If _currentFrame = -1 Then
                _currentFrame = 0
                timer.Enabled = False
                _direction = 0
                Return
            End If
            If _currentFrame = framesCount Then
                _currentFrame = framesCount - 1
                timer.Enabled = False
                _direction = 0
            End If
        End Sub

#End Region

#Region " Misc "
        Private Shared Function [If](Of T)(ByVal condition As Boolean, ByVal obj1 As T, ByVal obj2 As T) As T
            If condition Then Return obj1
            Return obj2
        End Function
#End Region

    End Class
End Namespace