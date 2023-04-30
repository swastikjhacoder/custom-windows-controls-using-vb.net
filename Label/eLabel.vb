#Region " Imports "
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms.Design
Imports System.Windows.Forms
Imports System.Drawing

#End Region
<System.Diagnostics.DebuggerStepThrough()> _
    <ToolboxBitmap(GetType(eLabel), "Resources.label.bmp")> _
Public Class eLabel
    Inherits Label

#Region "Declarations"

    Private WithEvents Pulser As New Timer
    Private _MouseIsOver As Boolean

#End Region

#Region "New"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        ForeColor = Color.MediumBlue
        Font = New Font("Arial", 12, Font.Style)
        PulseSpeed = 25
        Size = New Size(75, 21)
        TextAlign = ContentAlignment.MiddleCenter
    End Sub

    <DefaultValue(GetType(Font), "Arial, 12pt")> _
    Public Overrides Property Font() As Font
        Get
            Return (MyBase.Font)
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
        End Set
    End Property

    <DefaultValue(GetType(Color), "MediumBlue")> _
    Public Overrides Property ForeColor() As Color
        Get
            Return (MyBase.ForeColor)
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = value
        End Set
    End Property

#End Region

#Region "Hidden"

    <Browsable(False)> _
<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property AutoSize() As Boolean
        Get
            Return MyBase.AutoSize
        End Get
        Set(ByVal value As Boolean)
        End Set
    End Property

    <Browsable(False)> _
<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
Public Shadows Property Image() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(ByVal value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
Public Shadows Property BorderStyle() As BorderStyle
        Get
            Return BorderStyle.None 'always false 
        End Get
        Set(ByVal value As BorderStyle) 'None 
        End Set
    End Property

    <Browsable(False)> _
<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
Public Shadows Property ImageAlign() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(ByVal value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
Public Shadows Property ImageIndex() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(ByVal value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
Public Shadows Property ImageKey() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(ByVal value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
Public Shadows Property ImageList() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(ByVal value As Boolean) 'empty 
        End Set
    End Property

#End Region

#Region "Properties"

    Private _MouseOver As Boolean
    ''' <summary>
    ''' Get or Set if the eLabel will Glow when the mouse is over it
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the eLabel will Glow when the mouse is over it")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <DefaultValue(False)> _
    Public Property MouseOver() As Boolean
        Get
            Return _MouseOver
        End Get
        Set(ByVal value As Boolean)
            _MouseOver = value
        End Set
    End Property

    Private _MouseOverColor As Color = Color.Crimson
    ''' <summary>
    ''' Get or Set what color the eLabel will Glow when the mouse is over it
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set what color the eLabel will Glow when the mouse is over it")> _
    <DefaultValue(GetType(Color), "Crimson")> _
    Public Property MouseOverColor() As Color
        Get
            Return _MouseOverColor
        End Get
        Set(ByVal value As Color)
            _MouseOverColor = value
            Invalidate()
        End Set
    End Property

    Private _MouseOverForeColor As Color = Color.MediumBlue
    ''' <summary>
    ''' Get or Set what Forecolor the eLabel will be when the mouse is over it
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set what Forecolor the eLabel will be when the mouse is over it")> _
    <DefaultValue(GetType(Color), "MediumBlue")> _
    Public Property MouseOverForeColor() As Color
        Get
            Return _MouseOverForeColor
        End Get
        Set(ByVal value As Color)
            _MouseOverForeColor = value
            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean
    ''' <summary>
    ''' Get or Set the Checked status
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set the Checked status")> _
    <DefaultValue(False)> _
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            If _CheckedType = eCheckedType.Radio Then
                If _Checked = False Then

                    If value Then

                        For Each ctrl As Control In Parent.Controls
                            If ctrl.GetType Is GetType(eLabel) Then
                                Dim teLabel As eLabel = CType(ctrl, eLabel)
                                If teLabel.CheckedType = eCheckedType.Radio AndAlso teLabel.Checked Then
                                    teLabel.Checked = False
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            If _GlowMatchChecked Then
                _GlowState = value
            End If
            _Checked = value
            Invalidate()
        End Set
    End Property

    Public Sub ToggleChecked()
        Checked = Not Checked
    End Sub

    Public Enum eCheckedType
        Label
        Check
        Radio
    End Enum
    Private _CheckedType As eCheckedType = eCheckedType.Label
    ''' <summary>
    ''' Get or Set the Checked behavior
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set the Checked behavior")> _
    <DefaultValue(GetType(eCheckedType), "Label")> _
    Public Property CheckedType() As eCheckedType
        Get
            Return _CheckedType
        End Get
        Set(ByVal value As eCheckedType)
            _CheckedType = value
            Invalidate()
        End Set
    End Property

    Private _CheckedColor As Color = Color.Crimson
    ''' <summary>
    ''' Get or Set what color the eLabel will Glow when the mouse is over it
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set what color the eLabel will Glow when the mouse is over it")> _
    <DefaultValue(GetType(Color), "Crimson")> _
    Public Property CheckedColor() As Color
        Get
            Return _CheckedColor
        End Get
        Set(ByVal value As Color)
            _CheckedColor = value
            Invalidate()
        End Set
    End Property

    Private _FeatherState As Boolean = True
    ''' <summary>
    ''' Get or Set if the text is glowing
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the glow is feathered")> _
    <DefaultValue(True)> _
    Public Property FeatherState() As Boolean
        Get
            Return _FeatherState
        End Get
        Set(ByVal value As Boolean)
            _FeatherState = value
            Invalidate()
        End Set
    End Property

    Private _Feather As Integer = 100
    ''' <summary>
    ''' Get or Set the level of feathering (blurring) of the Outer Glow
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set the level of feathering (blurring) of the Outer Glow")> _
    <DefaultValue(100)> _
    Public Property Feather() As Integer
        Get
            Return _Feather
        End Get
        Set(ByVal value As Integer)
            _Feather = value
            If _Feather > 255 Then _Feather = 255
            If _Feather < 0 Then _Feather = 0
            Invalidate()
            PulseDirection = _Feather \ 25
            If PulseDirection < 0 Then PulseDirection = 1
        End Set
    End Property

    Private _PulseAdj As Integer
    Private _Pulse As Boolean
    ''' <summary>
    ''' Get or Set if the eLabel should be Pulsing or not
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the eLabel should be Pulsing or not")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <DefaultValue(False)> _
    Public Property Pulse() As Boolean
        Get
            Return _Pulse
        End Get
        Set(ByVal value As Boolean)
            _Pulse = value
            If value Then
                _PulseAdj = 0
                Pulser.Start()
            Else
                Pulser.Stop()
                _PulseAdj = 0
                Invalidate()
                'PulseDirection = _Feather \ 25
                'If PulseDirection < 0 Then PulseDirection = 1
            End If
        End Set
    End Property

    ''' <summary>
    ''' Get or Set how fast to pulse the eLabel
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set how fast to pulse the eLabel")> _
     <DefaultValue(25)> _
   Public Property PulseSpeed() As Integer
        Get
            Return Pulser.Interval

        End Get
        Set(ByVal value As Integer)
            Pulser.Interval = value
        End Set
    End Property

    Private _ShadowOffset As Point = New Point(5, 5)
    ''' <summary>
    ''' Get or Set how far to offset the shadow from the text
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set how far to offset the shadow from the text")> _
    <DefaultValue(GetType(Point), "5,5")> _
    Public Property ShadowOffset() As Point
        Get
            Return _ShadowOffset
        End Get
        Set(ByVal value As Point)
            _ShadowOffset = value
            Invalidate()
        End Set
    End Property

    Private _ShadowState As Boolean
    ''' <summary>
    ''' Get or Set if the eLabel displays the shadow text
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the eLabel displays the shadow text")> _
    <DefaultValue(False)> _
    Public Property ShadowState() As Boolean
        Get
            Return _ShadowState
        End Get
        Set(ByVal value As Boolean)
            _ShadowState = value
            Invalidate()
        End Set
    End Property

    Private _ShadowColor As Color = Color.Gray
    ''' <summary>
    ''' Get or Set what color to use for the shadow text
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set what color to use for the shadow text")> _
    <DefaultValue(GetType(Color), "Gray")> _
    Public Property ShadowColor() As Color
        Get
            Return _ShadowColor
        End Get
        Set(ByVal value As Color)
            _ShadowColor = value
            Invalidate()
        End Set
    End Property

    Private _GlowMatchChecked As Boolean = True
    ''' <summary>
    ''' Get or Set if the text is glowing
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the GlowState auto matches the Checked Value")> _
    <DefaultValue(True)> _
    Public Property GlowMatchChecked() As Boolean
        Get
            Return _GlowMatchChecked
        End Get
        Set(ByVal value As Boolean)
            _GlowMatchChecked = value
            Invalidate()
        End Set
    End Property

    Private _GlowState As Boolean = True
    ''' <summary>
    ''' Get or Set if the text is glowing
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the text is glowing")> _
    <DefaultValue(True)> _
    Public Property GlowState() As Boolean
        Get
            Return _GlowState
        End Get
        Set(ByVal value As Boolean)
            _GlowState = value
            Invalidate()
        End Set
    End Property

    Private _Glow As Integer = 8
    ''' <summary>
    ''' Get or Set how far out the text glows
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set how far out the text glows")> _
    <DefaultValue(8)> _
    Public Property Glow() As Integer
        Get
            Return _Glow
        End Get
        Set(ByVal value As Integer)
            _Glow = value
            Invalidate()
        End Set
    End Property

    Private _GlowColor As Color = Color.CornflowerBlue
    ''' <summary>
    ''' Get or Set what color to use for the Glow
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set what color to use for the Glow")> _
    <DefaultValue(GetType(Color), "CornflowerBlue")> _
    Public Property GlowColor() As Color
        Get
            Return _GlowColor
        End Get
        Set(ByVal value As Color)
            _GlowColor = value
            Invalidate()
        End Set
    End Property

    Enum eBorder
        All
        Top
        Bottom

    End Enum

    Private _Border As AnchorStyles = AnchorStyles.None
    ''' <summary>
    ''' Get or Set to show or not show the border
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set to show or not show the border")> _
    <DefaultValue(GetType(AnchorStyles), "None")> _
    Public Property Border() As AnchorStyles
        Get
            Return _Border
        End Get
        Set(ByVal value As AnchorStyles)
            _Border = value
            Invalidate()
        End Set
    End Property

    Private _BorderColor As Color = Color.Black
    ''' <summary>
    ''' Get or Set what Color to draw the border
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set what Color to draw the border")> _
    <DefaultValue(GetType(Color), "Black")> _
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Private _BorderWidth As Single = 1
    ''' <summary>
    ''' Get or Set what Width to draw the border
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set what Width to draw the border")> _
    <DefaultValue(GetType(Single), "1")> _
    Public Property BorderWidth() As Single
        Get
            Return _BorderWidth
        End Get
        Set(ByVal value As Single)
            _BorderWidth = value
            Invalidate()
        End Set
    End Property

    Private _AutoFit As Boolean
    ''' <summary>
    ''' Get or Set if the text is fitted to the DisplayRectangle or uses Font Size 
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the text is fitted to the DisplayRectangle or uses Font Size")> _
    <DefaultValue(False)> _
    Public Property AutoFit() As Boolean
        Get
            Return _AutoFit
        End Get
        Set(ByVal value As Boolean)
            _AutoFit = value
            Invalidate()
        End Set
    End Property

    Private _textWordWrap As Boolean = True
    ''' <summary>
    ''' Get or Set if the text wraps inside the DisplayRectangle 
    ''' </summary>
    <Category("Appearance")> _
    <Description("Get or Set if the text wraps inside the DisplayRectangle")> _
    <DefaultValue(True)> _
    Public Property TextWordWrap() As Boolean
        Get
            Return _textWordWrap
        End Get
        Set(ByVal Value As Boolean)
            _textWordWrap = Value
            If Value Then
                sf.FormatFlags = Nothing
            Else
                sf.FormatFlags = StringFormatFlags.NoWrap
            End If
            Invalidate()
        End Set
    End Property


    ''' <summary>
    '''  Get or Set if the text adds an Ellipsis (...) when the text exceeds the width
    ''' </summary>
    Public Shadows Property AutoEllipsis() As Boolean
        Get
            Return MyBase.AutoEllipsis
        End Get
        Set(ByVal Value As Boolean)
            MyBase.AutoEllipsis = Value
            If Value Then
                sf.Trimming = StringTrimming.EllipsisCharacter
            Else
                sf.Trimming = StringTrimming.None
            End If
            Invalidate()
        End Set
    End Property

    Private ReadOnly sf As StringFormat = New StringFormat()
    <DefaultValue(GetType(ContentAlignment), "MiddleCenter")> _
    Public Overrides Property TextAlign() As ContentAlignment
        Get
            Return MyBase.TextAlign
        End Get
        Set(ByVal value As ContentAlignment)
            MyBase.TextAlign = value
            Select Case TextAlign
                Case ContentAlignment.BottomCenter, ContentAlignment.BottomLeft, ContentAlignment.BottomRight
                    sf.LineAlignment = StringAlignment.Far
                Case ContentAlignment.MiddleCenter, ContentAlignment.MiddleLeft, ContentAlignment.MiddleRight
                    sf.LineAlignment = StringAlignment.Center
                Case ContentAlignment.TopCenter, ContentAlignment.TopLeft, ContentAlignment.TopRight
                    sf.LineAlignment = StringAlignment.Near
            End Select
            Select Case TextAlign
                Case ContentAlignment.BottomRight, ContentAlignment.MiddleRight, ContentAlignment.TopRight
                    sf.Alignment = StringAlignment.Far
                Case ContentAlignment.BottomCenter, ContentAlignment.MiddleCenter, ContentAlignment.TopCenter
                    sf.Alignment = StringAlignment.Center
                Case ContentAlignment.BottomLeft, ContentAlignment.MiddleLeft, ContentAlignment.TopLeft
                    sf.Alignment = StringAlignment.Near
            End Select

        End Set
    End Property

    Enum eFillType
        Solid
        GradientLinear
    End Enum

    Private _FillType As eFillType = eFillType.Solid
    ''' <summary>
    ''' The eFillType Fill Type to apply to the eLabel
    ''' </summary>
    <Description("The Fill Type to apply to the eLabel")> _
    <Category("Appearance")> _
    <DefaultValue(GetType(eFillType), "Solid")> _
    Public Property FillType() As eFillType
        Get
            Return _FillType
        End Get
        Set(ByVal value As eFillType)
            _FillType = value
            Invalidate()
        End Set
    End Property

    Private _FillTypeLinear As LinearGradientMode = LinearGradientMode.Vertical
    ''' <summary>
    ''' The Linear Blend type
    ''' </summary>
    <Description("The Linear Blend type"), _
    Category("Appearance")> _
    <DefaultValue(GetType(LinearGradientMode), "Vertical")> _
    Public Property FillTypeLinear() As LinearGradientMode
        Get
            Return _FillTypeLinear
        End Get
        Set(ByVal value As LinearGradientMode)
            _FillTypeLinear = value
            Invalidate()
        End Set
    End Property

    Private _ForeColorBlend As cBlendItems = DefaultColorFillBlend()
    ''' <summary>
    ''' The ColorBlend used to fill the eLabel
    ''' </summary>
    <Description("The ColorBlend used to fill the eLabel"), _
    Category("Appearance"), _
    RefreshProperties(RefreshProperties.All), _
    Editor(GetType(BlendTypeEditor), GetType(UITypeEditor))> _
    Public Property ForeColorBlend() As cBlendItems
        Get
            Return _ForeColorBlend
        End Get
        Set(ByVal value As cBlendItems)
            _ForeColorBlend = value
            Invalidate()
        End Set
    End Property

    Private Function DefaultColorFillBlend() As cBlendItems
        Return New cBlendItems(New Color() {Color.AliceBlue, Color.RoyalBlue, Color.Navy}, New Single() {0, 0.5, 1})
    End Function

#Region "ForeColorBlend DefaultValue Settings"
    'The standard <DefaultValue(XXX)> attribute
    'will not work correctly for custom Types
    'These two Methods are needed to set the Default Value Correctly
    Public Sub ResetForeColorBlend()
        ForeColorBlend = DefaultColorFillBlend()
    End Sub

    Private Function ShouldSerializeForeColorBlend() As Boolean
        Return Not (_ForeColorBlend.Equals(DefaultColorFillBlend))
    End Function
#End Region

#End Region

#Region "Overrides"

    Protected Overrides Sub OnPaintBackground( _
        ByVal pevent As PaintEventArgs)
        If BackColor = Color.Transparent Then
            MyBase.OnPaintBackground(pevent)
        Else
            pevent.Graphics.Clear(BackColor)
            'pevent.Graphics.Clear(EnabledColor(BackColor))
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        'Setup Graphics
        Dim g As Graphics = e.Graphics

        g.SmoothingMode = SmoothingMode.AntiAlias
        'g.InterpolationMode = InterpolationMode.HighQualityBicubic

        'Paint Border if Requested
        If _Border <> AnchorStyles.None Then
            Using pn As New Pen(EnabledColor(_BorderColor), (_BorderWidth * 2))
                g.ResetTransform()
                If _Border = CType(AnchorStyles.Bottom Or AnchorStyles.Top Or _
                    AnchorStyles.Left Or AnchorStyles.Right, AnchorStyles) Then
                    g.DrawRectangle(pn, 0, 0, Width - 1, Height - 1)
                Else
                    If _Border = CType(_Border Or AnchorStyles.Bottom, AnchorStyles) Then
                        g.DrawLine(pn, 0, Height - 1, Width - 1, Height - 1)
                    End If
                    If _Border = CType(_Border Or AnchorStyles.Top, AnchorStyles) Then
                        g.DrawLine(pn, 0, 0, Width - 1, 0)
                    End If
                    If _Border = CType(_Border Or AnchorStyles.Left, AnchorStyles) Then
                        g.DrawLine(pn, 0, 0, 0, Height - 1)
                    End If
                    If _Border = CType(_Border Or AnchorStyles.Right, AnchorStyles) Then
                        g.DrawLine(pn, Width - 1, 0, Width - 1, Height - 1)
                    End If

                End If

            End Using

        End If
        'Paint Shadow if Requested
        If _ShadowState Then
            Using shadowpath As New GraphicsPath

                MakeTextPath(shadowpath, g)

                'Offset the Shadow
                Using mx As New Matrix
                    If _AutoFit Then
                        mx.Translate(_ShadowOffset.X - 2, _ShadowOffset.Y - 2)
                    Else
                        mx.Translate(_ShadowOffset.X, _ShadowOffset.Y)
                    End If
                    shadowpath.Transform(mx)
                End Using

                'Blur the edge a bit
                Dim x As Integer = CInt(Fix(Height / 30) + 1)
                For i As Integer = 1 To x
                    Using pen As Pen = New Pen(EnabledColor( _
                      Color.FromArgb(CInt(200 - ((200 / x) * i)), _ShadowColor)), i)
                        pen.LineJoin = LineJoin.Round
                        g.DrawPath(pen, shadowpath)
                    End Using
                Next i
                g.FillPath(New SolidBrush(EnabledColor(_ShadowColor)), shadowpath)
            End Using
        End If

        Using path As New GraphicsPath

            MakeTextPath(path, g)

            'Paint Glow if Requested
            Dim gColor As Color = _GlowColor
            Dim gForeColor As Color
            If _Checked Then
                gForeColor = _CheckedColor
            Else
                gForeColor = ForeColor
            End If

            If _GlowState Or (_MouseOver And _MouseIsOver) Then
                If _MouseOver And _MouseIsOver Then
                    gColor = _MouseOverColor
                    If Not _Checked Then gForeColor = _MouseOverForeColor
                End If

                If _FeatherState Then
                    For i As Integer = 1 To _Glow Step 2
                        Dim aGlow As Integer = CInt((_Feather - _PulseAdj) - _
                            (((_Feather - _PulseAdj) / _Glow) * i))
                        Using pen As Pen = New Pen(Color.FromArgb( _
                            aGlow, EnabledColor(gColor)), i)
                            pen.LineJoin = LineJoin.Round
                            g.DrawPath(pen, path)
                        End Using
                    Next i
                Else
                    Using pen As Pen = New Pen(Color.FromArgb( _
                        _Feather - _PulseAdj, EnabledColor(gColor)), _Glow)
                        pen.LineJoin = LineJoin.Round
                        g.DrawPath(pen, path)
                    End Using
                End If
            End If

            'Paint Label Text
            Select Case _FillType
                Case eFillType.Solid
                    path.FillMode = FillMode.Winding
                    g.FillPath(New SolidBrush(EnabledColor(gForeColor)), path)

                Case eFillType.GradientLinear
                    Using br As LinearGradientBrush = New LinearGradientBrush( _
                      New RectangleF(path.GetBounds.X - 1, path.GetBounds.Y - 1, _
                      path.GetBounds.Width + 2, path.GetBounds.Height + 2), _
                      Color.White, Color.White, FillTypeLinear)
                        Dim cb As New ColorBlend
                        cb.Colors = EnableBlends(_ForeColorBlend.iColor)
                        cb.Positions = _ForeColorBlend.iPoint

                        br.InterpolationColors = cb

                        g.FillPath(br, path)
                    End Using

            End Select
        End Using


    End Sub

    Private Sub MakeTextPath(ByRef path As GraphicsPath, ByRef g As Graphics)

        If _AutoFit Then
            Try
                'Determine the outer margin for the text so there
                'is enough room for the glow and shadow
                Dim pad As Integer = 2
                If GlowState Then
                    pad += _
                    CInt(_Glow - (_Glow / 2) + Fix(_Glow / 30) * 3)
                End If
                If Border <> AnchorStyles.None Then
                    pad += CInt(BorderWidth + 1)
                End If

                'Add Padding
                Dim TextMargin As Padding = _
                    Windows.Forms.Padding.Add(Padding, New Padding(pad))

                If ShadowState Then
                    With _ShadowOffset
                        TextMargin.Right += CInt(.X)
                        TextMargin.Bottom += CInt(.Y + ((.Y) / 3))
                    End With
                End If

                'Get a rectangle for the area to paint the text
                Dim target As New Rectangle( _
                    TextMargin.Left, _
                    TextMargin.Top, _
                    Math.Max(5, ClientSize.Width - TextMargin.Horizontal), _
                    Math.Max(5, ClientSize.Height - TextMargin.Vertical))

                'Get the points for the corners of the area
                Dim target_pts() As PointF = { _
                    New PointF(target.Left, target.Top), _
                    New PointF(target.Right, target.Top), _
                    New PointF(target.Left, target.Bottom) _
                    }

                'Make a GraphicsPath of the Text String 
                'close to the size it needs to be
                path.AddString(Text, _
                    Font.FontFamily, Font.Style, _
                     target.Height, New PointF(0, 0), sf)

                'Get a rectangle for the path of the text
                Dim text_rectf As RectangleF = path.GetBounds()

                'Transform the Graphics Object with the Matrix
                'to fit the path rectangle inside the target rectangle
                g.Transform = New Matrix(text_rectf, target_pts)

            Catch ex As Exception

            End Try

        Else
            'create a GraphicsPath of the text
            'Because the GraphicsPath does not match exactly with
            'Drawing a String normally, multiply the font Size by
            '1.26 to get a pretty close representation of the size.
            path.AddString(Text, Font.FontFamily, Font.Style, _
                CInt(Font.Size * 1.26), _
                New Rectangle( _
                    ClientRectangle.X + Padding.Left, _
                    ClientRectangle.Y + Padding.Top, _
                    ClientRectangle.Width - Padding.Horizontal, _
                    ClientRectangle.Height - Padding.Vertical), _
                sf)

        End If

    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        If MouseOver Then
            _MouseIsOver = True
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseleave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        If MouseOver Then
            _MouseIsOver = False
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        MyBase.OnClick(e)
        If CheckedType <> eCheckedType.Label Then
            If Not (CheckedType = eCheckedType.Radio And _Checked) Then
                ToggleChecked()
            End If
        End If
    End Sub

#End Region

#Region "EnabledColor"

    ''' <summary>
    ''' Convert color to gray if Disabled else return origional color
    ''' </summary>
    ''' <param name="ColorIn">Color to Check</param>
    Private Function EnabledColor(ByVal ColorIn As Color) As Color
        If Enabled Then
            Return ColorIn
        Else
            Dim gray As Integer = _
          CInt(ColorIn.R * 0.3 + ColorIn.G * 0.59 + ColorIn.B * 0.11)
            Return Color.FromArgb(ColorIn.A, gray, gray, gray)
        End If
    End Function

    ''' <summary>
    ''' Convert colorblend to grayscale if Disabled else return origional colorblend
    ''' </summary>
    ''' <param name="ColorIn">Colorblend to Check</param>
    Private Function EnableBlends(ByVal ColorIn As Color()) As Color()

        If Enabled Then
            Return ColorIn
        Else
            Dim tcolor As New List(Of Color)
            For Each c As Color In ColorIn
                Dim gray As Integer = CInt(c.R * 0.3 + c.G * 0.59 + c.B * 0.11)
                tcolor.Add(Color.FromArgb(c.A, gray, gray, gray))
            Next
            Return tcolor.ToArray
        End If
    End Function

#End Region

#Region "PulseTimer"

    Private PulseDirection As Integer = 1
    Private Sub Pulser_Tick(ByVal sender As Object, _
        ByVal e As EventArgs) Handles Pulser.Tick
        _PulseAdj += PulseDirection
        If _Feather - _PulseAdj < 10 _
          OrElse _Feather - _PulseAdj > _Feather Then
            PulseDirection *= -1
            _PulseAdj += PulseDirection
        End If
        Invalidate()
    End Sub

#End Region

End Class

#Region "BlendTypeEditor - UITypeEditor"

#Region "cBlendItems"

<System.Diagnostics.DebuggerStepThrough()> _
<TypeConverter(GetType(BlendItemsConverter))> _
Public Class eBlendItems

    Sub New()

    End Sub

    Sub New(ByVal Color As Color(), ByVal Pt As Single())
        iColor = Color
        iPoint = Pt
    End Sub

    Private _iColor As Color()
    <Description("The Color for the Point"), _
       Category("Appearance")> _
    Public Property iColor() As Color()
        Get
            Return _iColor
        End Get
        Set(ByVal value As Color())
            _iColor = value
        End Set
    End Property

    Private _iPoint As Single()
    <Description("The Color for the Point"), _
       Category("Appearance")> _
    Public Property iPoint() As Single()
        Get
            Return _iPoint
        End Get
        Set(ByVal value As Single())
            _iPoint = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        ' build the string as "Color1;Color2;Color3|Pt1;Pt2;Pt3" 
        Dim bColors As New ArrayList
        Dim bPoints As New ArrayList
        For Each bColor As Color In _iColor
            If bColor.IsNamedColor Then
                bColors.Add(bColor.Name)
            Else
                bColors.Add(String.Format("{0},{1},{2},{3}", bColor.A, bColor.R, bColor.G, bColor.B))
            End If
        Next
        For Each bPoint As Single In _iPoint
            bPoints.Add(bPoint.ToString)
        Next

        Return String.Format("{0}|{1}", Join(bColors.ToArray, ";"), Join(bPoints.ToArray, ";"))
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim eObj As cBlendItems = CType(obj, cBlendItems)
        If iColor.Length <> eObj.iColor.Length _
            OrElse iPoint.Length <> eObj.iPoint.Length Then
            Return False
        Else
            For i As Integer = 0 To iColor.Length - 1
                If iColor(i) <> eObj.iColor(i) OrElse iPoint(i) <> eObj.iPoint(i) Then
                    Return False
                End If
            Next
            Return True
        End If

    End Function

End Class

#End Region 'cBlendItems

#Region "BlendItemsConverter"

<System.Diagnostics.DebuggerStepThrough()> _
Friend Class eBlendItemsConverter : Inherits ExpandableObjectConverter

    Public Overrides Function GetCreateInstanceSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function CreateInstance(ByVal context As ITypeDescriptorContext, ByVal propertyValues As IDictionary) As Object
        Dim bItem As New cBlendItems
        bItem.iColor = CType(propertyValues("iColor"), Color())
        bItem.iPoint = CType(propertyValues("iPoint"), Single())
        Return bItem
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object


        If TypeOf value Is String Then
            Try
                Dim s As String() = Split(CType(value, String), "|")
                Dim bColors As New List(Of Color)
                Dim bPoints As New List(Of Single)

                For Each cstring As String In Split(s(0), ";")
                    bColors.Add(CType(TypeDescriptor.GetConverter( _
                    GetType(Color)).ConvertFromString(cstring), Color))
                Next
                For Each cstring As String In Split(s(1), ";")
                    bPoints.Add(CType(TypeDescriptor.GetConverter( _
                    GetType(Single)).ConvertFromString(cstring), Single))
                Next

                If Not IsNothing(bColors) AndAlso Not IsNothing(bPoints) Then
                    If bColors.Count <> bPoints.Count Then Throw New ArgumentException(String.Format("Can not convert '{0}' to type cBlendItem", CStr(value)))

                    Return New cBlendItems(bColors.ToArray, bPoints.ToArray)
                End If
            Catch ex As Exception
                Throw New ArgumentException(String.Format("Can not convert '{0}' to type cBlendItem", CStr(value)))
            End Try
        Else
            Return New cBlendItems()
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, _
      ByVal value As Object, ByVal destinationType As Type) As Object

        If (destinationType Is GetType(String) AndAlso TypeOf value Is cBlendItems) Then
            Dim _BlendItems As cBlendItems = CType(value, cBlendItems)

            ' build the string as "Color1;Color2;Color3|Pt1;Pt2;Pt3" 
            Dim bColors As New ArrayList
            Dim bPoints As New ArrayList
            For Each bColor As Color In _BlendItems.iColor
                If bColor.IsNamedColor Then
                    bColors.Add(bColor.Name)
                Else
                    bColors.Add(String.Format("{0},{1},{2},{3}", bColor.A, bColor.R, bColor.G, bColor.B))
                End If

            Next
            For Each bPoint As Single In _BlendItems.iPoint
                bPoints.Add(bPoint.ToString)
            Next

            Return String.Format("{0}|{1}", Join(bColors.ToArray, ";"), Join(bPoints.ToArray, ";"))
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)

    End Function

End Class 'CornerConverter Code

#End Region 'BlendItemsConverter

#Region "BlendTypeEditor"

<System.Diagnostics.DebuggerStepThrough()> _
Public Class eBlendTypeEditor
    Inherits UITypeEditor

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If Not context Is Nothing Then
            Return UITypeEditorEditStyle.DropDown
        End If
        Return (MyBase.GetEditStyle(context))
    End Function

    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing) And (Not provider Is Nothing) Then
            ' Access the property browser's UI display service, IWindowsFormsEditorService
            Dim editorService As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If Not editorService Is Nothing Then
                ' Create an instance of the UI editor, passing a reference to the editor service
                Using dropDownEditor As DropdownColorBlender = New DropdownColorBlender(editorService)
                    ' Pass the UI editor the current property values
                    Dim Instance As New eLabel()
                    If context.Instance.GetType Is GetType(eLabel) Then
                        'For PropertyGrid
                        Instance = CType(context.Instance, eLabel)
                        'Else
                        'For SmartTag To be added later
                        'Instance = CType(CType(context.Instance, eLabelActionList).CurrControl, eLabel)
                    End If
                    'Update The Sample with the Current Instance's Properties
                    With dropDownEditor
                        Dim ratio As Single
                        If Instance.Width > Instance.Height Then
                            .TheSample.Height = CInt(.TheSample.Width * (Instance.Height / Instance.Width))
                            .TheSample.Top = CInt((.panSampleHolder.Height - .TheSample.Height) / 2)
                            ratio = CSng(.TheSample.Height / Instance.Height)
                        Else
                            .TheSample.Width = CInt(.TheSample.Height * (Instance.Width / Instance.Height))
                            .TheSample.Left = CInt((.panSampleHolder.Width - .TheSample.Width) / 2)
                            ratio = CSng(.TheSample.Width / Instance.Width)
                        End If
                        .TheSample.BackColor = Instance.BackColor
                        .TheSample.Border = Instance.Border
                        .TheSample.BorderColor = Instance.BorderColor
                        .TheSample.Feather = Instance.Feather
                        .TheSample.FillType = eLabel.eFillType.GradientLinear
                        .TheSample.FillTypeLinear = Instance.FillTypeLinear
                        .TheSample.Font = New Font(Instance.Font.FontFamily, Instance.Font.Size * ratio, Instance.Font.Style)
                        .TheSample.ForeColor = Instance.ForeColor
                        .TheSample.BorderColor = Instance.BorderColor
                        .TheSample.ForeColorBlend = Instance.ForeColorBlend
                        .LoadABlend(Instance.ForeColorBlend)
                        .TheSample.Glow = Instance.Glow
                        .TheSample.GlowColor = Instance.GlowColor
                        .TheSample.GlowState = Instance.GlowState
                        .TheSample.MouseOver = Instance.MouseOver
                        .TheSample.MouseOverColor = Instance.MouseOverColor
                        .TheSample.Pulse = Instance.Pulse
                        .TheSample.PulseSpeed = Instance.PulseSpeed
                        .TheSample.ShadowColor = Instance.ShadowColor
                        .TheSample.ShadowOffset = Instance.ShadowOffset
                        .TheSample.ShadowState = Instance.ShadowState
                        .TheSample.Text = Instance.Text
                        .TheSample.TextAlign = Instance.TextAlign
                        .TheSample.AutoFit = Instance.AutoFit
                    End With
                    ' Display the UI editor
                    editorService.DropDownControl(dropDownEditor)
                    ' Return the new property value from the editor
                    Return dropDownEditor.TheSample.ForeColorBlend
                End Using
            End If
        End If
        Return MyBase.EditValue(context, provider, value)
    End Function

    ' Indicate that we draw values in the Properties window.
    Public Overrides Function GetPaintValueSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    ' Draw a BorderStyles value.
    Public Overrides Sub PaintValue(ByVal e As PaintValueEventArgs)
        ' Erase the area.
        e.Graphics.FillRectangle(Brushes.White, e.Bounds)

        ' Draw the sample.
        Dim cblnd As cBlendItems = DirectCast(e.Value, cBlendItems)
        Using br As LinearGradientBrush = New LinearGradientBrush(e.Bounds, Color.Black, Color.Black, LinearGradientMode.Horizontal)
            Dim cb As New ColorBlend
            cb.Colors = cblnd.iColor
            cb.Positions = cblnd.iPoint
            br.InterpolationColors = cb
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using
    End Sub
End Class
#End Region

#End Region 'BlendTypeEditor - UITypeEditor

