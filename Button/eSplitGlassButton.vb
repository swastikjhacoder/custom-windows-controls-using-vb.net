#Region "   Imports"

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

#End Region
#Region "eSplitGlassButton UserControl Code"
Namespace UIControls.Button
    Public Class eSplitGlassButton
        Inherits System.Windows.Forms.Button

#Region "   Constructor"
        ''' <summary>
        ''' Required by the Form Designer Variable
        ''' </summary>
        Private Components As System.ComponentModel.IContainer
        ''' <summary>
        ''' Create New Object of eSplitGlassButton and Initialize it.
        ''' </summary>
        Public Sub New()
            InitializeComponent()
            Me.SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
            Me.SetStyle(ControlStyles.Opaque, False)
            Me.FlatAppearance.BorderSize = 0
            Me.FlatStyle = Windows.Forms.FlatStyle.Flat
            Me.BackColor = Color.Transparent
            Timer1.Interval = 5
            AddHandler Timer1.Tick, AddressOf Timer1_Tick
        End Sub

#Region "   Component Designer"
        ''' <summary>
        ''' Initialize the eSplitGlassButton Components
        ''' </summary>
        Public Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'eSplitGlassButton
            '
            Me.Name = "eSplitGlassButton"
            Me.Size = New System.Drawing.Size(112, 35)
            Me.ResumeLayout(False)
        End Sub

#End Region
        ''' <summary>
        ''' MBExplorer Overrides Dispose to CleanUp component list
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (Components Is Nothing) Then
                    Components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub
        ''' <summary>
        ''' Create Base for eSplitGlassButton
        ''' </summary>
        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            A0 = BaseColor.A
            R0 = BaseColor.R
            G0 = BaseColor.G
            B0 = BaseColor.B
            _colorStroke = _baseStroke
            Dim r As Rectangle = New Rectangle(New Point(-1, -1), New Size(Me.Width + _radius, Me.Height + _radius))
            If Me.Size <> Nothing Then
                Dim pathregion As GraphicsPath = New GraphicsPath
                DrawArc(r, pathregion)
                Me.Region = New Region(pathregion)
            End If
        End Sub

#End Region

#Region "   Image Settings Properties"

        Private _splitlocation As MB_SplitLocation
        ''' <summary>
        ''' Enum to Set Split Location for MBGlasButton
        ''' </summary>
        Public Enum MB_SplitLocation
            Bottom
            Right
            None
        End Enum
        ''' <summary>
        ''' Get or Set the Location to Split Button into Two Parts
        ''' </summary>
        <Category("eSplitGlassButton Appearance"), _
        Description("Split location for eSplitGlassButton"), _
        DefaultValue(MB_SplitLocation.Bottom), Browsable(True)> _
        Public Property SplitLocation() As MB_SplitLocation
            Get
                Return _splitlocation
            End Get
            Set(ByVal value As MB_SplitLocation)
                _splitlocation = value
                Me.Refresh()
            End Set
        End Property

        Private _ImageSize As Size = New Size(24, 24)
        ''' <summary>
        ''' Get or Set the Image Size for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Image Settings"), _
        Description("Set Image Size of eSplitGlassButton"), _
        DefaultValue("24,24"), Browsable(True)> _
        Public Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
            Set(ByVal value As Size)
                _ImageSize = value
            End Set
        End Property

#End Region

#Region "   Button Settings Properties"

        Private _imageoffset As Integer = 0
        Private _radius As Integer = 6
        Private _showbase As MB_ShowBase
        Private _tempshowbase As MB_ShowBase
        ''' <summary>
        ''' Enum for Base Visibility
        ''' </summary>
        Public Enum MB_ShowBase
            Yes
            No
        End Enum

        ''' <summary>
        ''' Get or Set Base Visibility for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Settings"), _
        Description("Display Glass Style Base for eSplitGlassButton"), _
        DefaultValue(MB_ShowBase.Yes), Browsable(True)> _
        Public Property ShowBase() As MB_ShowBase
            Get
                Return _showbase
            End Get
            Set(ByVal value As MB_ShowBase)
                _showbase = value
                Me.Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Get or Set Corner Radius for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Settings"), _
        Description("Radius of eSplitGlassButton Corners"), _
        DefaultValue(6), Browsable(True)> _
        Public Property Radius() As Integer
            Get
                Return _radius
            End Get
            Set(ByVal value As Integer)
                If _radius > 2 Then
                    _radius = value
                End If
                Me.Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Enum for Group Position of eSplitGlassButton
        ''' </summary>
        Public Enum MB_GroupPos
            None
            Left
            Center
            Right
            Top
            Bottom
        End Enum

        ''' <summary>
        ''' Get or Set Group Position for eSplitGlassButton
        ''' </summary>
        Private _grouppos As MB_GroupPos
        <Category("eSplitGlassButton Position"), _
        Description("Set the Group Position for eSplitGlassButton"), _
        DefaultValue(MB_GroupPos.None), Browsable(True)> _
        Public Property GroupPosition() As MB_GroupPos
            Get
                Return _grouppos
            End Get
            Set(ByVal value As MB_GroupPos)
                _grouppos = value
                Me.Refresh()
            End Set
        End Property

        Public Enum MB_Arrow
            None
            ToRight
            ToDown
        End Enum
        Private _arrow As MB_Arrow
        ''' <summary>
        ''' Get or Set Arrow visibility of eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Arrow"), _
        Description("Display Arrow on eSplitGlassButton"), _
        DefaultValue(MB_Arrow.None), Browsable(True)> _
        Public Property Arrow() As MB_Arrow
            Get
                Return _arrow
            End Get
            Set(ByVal value As MB_Arrow)
                _arrow = value
                Me.Refresh()
            End Set
        End Property

        Private _splitbutton As MB_SplitButton = MB_SplitButton.No
        Public Enum MB_SplitButton
            Yes
            No
        End Enum
        ''' <summary>
        ''' Get or Set Split Option for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Split Settings"), _
        Description("Split eSplitGlassButton into Two Parts"), _
        DefaultValue(MB_SplitButton.No), Browsable(True)> _
        Public Property SplitButton() As MB_SplitButton
            Get
                Return _splitbutton
            End Get
            Set(ByVal value As MB_SplitButton)
                _splitbutton = value
                Me.Refresh()
            End Set
        End Property

        Private _splitdistance As Integer = 0
        ''' <summary>
        ''' Get or Set Split Distance for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Split Settings"), _
        Description("Set Split Distance for eSplitGlassButton"), _
        DefaultValue(0), Browsable(True)> _
        Public Property SplitDistance() As Integer
            Get
                Return _splitdistance
            End Get
            Set(ByVal value As Integer)
                _splitdistance = value
                Me.Refresh()
            End Set
        End Property

        Private _keeppressed As Boolean = False
        ''' <summary>
        ''' Get or Set Button State of eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Appearance"), _
        Description("Keep eSplitGlassButton Pressed if Clicked"), _
        DefaultValue(False), Browsable(True)> _
        Public Property KeepPressed() As Boolean
            Get
                Return _keeppressed
            End Get
            Set(ByVal value As Boolean)
                _keeppressed = value
            End Set
        End Property

        Private _ispressed As Boolean = False
        ''' <summary>
        ''' Get or Set State of eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Appearance"), _
        Description("Keep eSplitGlassButton Selected if Focused"), _
        DefaultValue(False), Browsable(True)> _
        Public Property IsPressed() As Boolean
            Get
                Return _ispressed
            End Get
            Set(ByVal value As Boolean)
                _ispressed = value
            End Set
        End Property

#End Region

#Region "   Menu Position"
        Private _menupos As Point = New Point(0, 0)
        ''' <summary>
        ''' Get ot Set DropDown Menu Position of eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Menu"), _
        Description("Set MenuList Position for eSplitGlassButton"), _
        DefaultValue("0, 0"), Browsable(True)> _
        Public Property MenuListPosition() As Point
            Get
                Return _menupos
            End Get
            Set(ByVal value As Point)
                _menupos = value
            End Set
        End Property
#End Region

#Region "   Colors"

        Private _baseColor As Color = Color.FromArgb(211, 211, 211)
        Private _onColor As Color = Color.FromArgb(255, 214, 78)
        Private _pressColor As Color = Color.FromArgb(255, 128, 0)

        Private _baseStroke As Color = Color.FromArgb(192, 192, 192)
        Private _onStroke As Color = Color.FromArgb(196, 177, 118)
        Private _pressStroke As Color = Color.FromArgb(128, 64, 0)
        Private _colorStroke As Color = Color.FromArgb(255, 255, 255)
        Private A0 As Integer, R0 As Integer, G0 As Integer, B0 As Integer
        ''' <summary>
        ''' Get or Set Base Color for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Color"), _
        Description("Set Base Color of eSplitGlassButton"), _
        DefaultValue("186, 209, 240"), Browsable(True)> _
        Public Property BaseColor() As Color
            Get
                Return _baseColor
            End Get
            Set(ByVal value As Color)
                _baseColor = value
                R0 = _baseColor.R
                B0 = _baseColor.B
                G0 = _baseColor.G
                A0 = _baseColor.A
                Dim hsb As MBColor = New MBColor(_baseColor)
                If hsb.BC < 50 Then
                    hsb.SetBrightness(60)
                Else
                    hsb.SetBrightness(30)
                End If
                If _baseColor.A > 0 Then
                    _baseStroke = Color.FromArgb(100, hsb.GetColor())
                Else
                    _baseStroke = Color.FromArgb(0, hsb.GetColor())
                End If
                Me.Refresh()
            End Set
        End Property
        ''' <summary>
        ''' Get ot Set OnColor of eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Color"), _
        Description("Set Mouse On Color of eSplitGlassButton"), _
        DefaultValue("255, 214, 78"), Browsable(True)> _
        Public Property OnColor() As Color
            Get
                Return _onColor
            End Get
            Set(ByVal value As Color)
                _onColor = value

                Dim hsb As MBColor = New MBColor(_onColor)
                If (hsb.BC < 50) Then
                    hsb.SetBrightness(60)
                Else
                    hsb.SetBrightness(30)
                End If
                If (_baseStroke.A > 0) Then
                    _onStroke = Color.FromArgb(100, hsb.GetColor())
                Else
                    _onStroke = Color.FromArgb(0, hsb.GetColor())
                End If
                Me.Refresh()
            End Set
        End Property
        ''' <summary>
        ''' Get or Set Color when eSplitGlassButton is Pressed.
        ''' </summary>
        <Category("eSplitGlassButton Color"), _
        Description("Set Mouse Press Color of eSplitGlassButton"), _
        DefaultValue("255, 128, 0"), Browsable(True)> _
        Public Property PressColor() As Color
            Get
                Return _pressColor
            End Get
            Set(ByVal value As Color)
                _pressColor = value

                Dim hsb As MBColor = New MBColor(_pressColor)
                If (hsb.BC < 50) Then
                    hsb.SetBrightness(60)
                Else

                    hsb.SetBrightness(30)
                End If
                If _baseStroke.A > 0 Then
                    _pressStroke = Color.FromArgb(100, hsb.GetColor())
                Else
                    _pressStroke = Color.FromArgb(0, hsb.GetColor())
                End If
                Me.Refresh()
            End Set
        End Property
        ''' <summary>
        ''' Get or Set Base Stroke for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Color"), _
        Description("Set Base Stroke Color of eSplitGlassButton"), _
        DefaultValue("152, 187, 213"), Browsable(True)> _
        Public Property BaseStrokeColor() As Color
            Get
                Return _baseStroke
            End Get
            Set(ByVal value As Color)
                _baseStroke = value
            End Set
        End Property
        ''' <summary>
        ''' Get or Set OnStroke Color for eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Color"), _
        Description("Set On Stroke Color of eSplitGlassButton"), _
        DefaultValue("196, 177, 118"), Browsable(True)> _
        Public Property OnStrokeColor() As Color
            Get
                Return _onStroke
            End Get
            Set(ByVal value As Color)
                _onStroke = value
            End Set
        End Property
        ''' <summary>
        ''' Get or Set PressStroke Color of eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Color"), _
        Description("Set Press Stroke Color of eSplitGlassButton"), _
        DefaultValue("128, 64, 0"), Browsable(True)> _
        Public Property PressStrokeColor() As Color
            Get
                Return _pressStroke
            End Get
            Set(ByVal value As Color)
                _pressStroke = value
            End Set
        End Property
        ''' <summary>
        ''' Return Color with Increase Value
        ''' </summary>
        ''' <param name="color">The Value As System.Drawing.Color</param>
        ''' <param name="h">Hue Value As Integer</param>
        ''' <param name="s">Saturation Value As Integer</param>
        ''' <param name="b">Brightness Value As Integer</param>
        ''' <returns>Returns New Color</returns>
        Public Function GetColorIncreased(ByVal color As Color, ByVal h As Integer, ByVal s As Integer, ByVal b As Integer) As Color
            Dim _color As MBColor = New MBColor(color)
            Dim ss As Integer = _color.GetSaturation()
            Dim vc As Single = b + _color.GetBrightness()
            Dim hc As Single = h + _color.GetHue()
            Dim sc As Single = s + ss
            _color.VC = vc
            _color.HC = hc
            _color.SC = sc
            Return _color.GetColor()
        End Function
        ''' <summary>
        ''' Returns New Color From ARGB Values
        ''' </summary>
        ''' <param name="A">Alpha value as Integer</param>
        ''' <param name="R">Red value as integer</param>
        ''' <param name="G">Green value as Integer</param>
        ''' <param name="B">Blue value as Integer</param>
        ''' <returns>New Color</returns>
        Public Function GetColor(ByVal A As Integer, ByVal R As Integer, ByVal G As Integer, ByVal B As Integer)
            If (A + A0 > 255) Then
                A = 255
            Else
                A = A + A0
            End If
            If (R + R0 > 255) Then
                R = 255
            Else
                R = R + R0
            End If
            If (G + G0 > 255) Then
                G = 255
            Else
                G = G + G0
            End If
            If B + B0 > 255 Then
                B = 255
            Else
                B = B + B0
            End If
            Return Color.FromArgb(A, R, G, B)
        End Function

#End Region

#Region "   eSplitGlassButton Methods"
        ''' <summary>
        ''' Handles eSplitGlassButton Paint
        ''' </summary>
        Protected Overrides Sub OnPaint(ByVal pevent As System.Windows.Forms.PaintEventArgs)
            Dim g As Graphics = pevent.Graphics
            g.SmoothingMode = SmoothingMode.HighQuality
            g.InterpolationMode = InterpolationMode.High
            Dim r As Rectangle = New Rectangle(New Point(-1, -1), New Size(Me.Width + _radius, Me.Height + _radius))
            Dim path As GraphicsPath = New GraphicsPath
            Dim rp As Rectangle = New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1))
            DrawArc(rp, path)
            FillGradients(g, path)
            DrawImage(g)
            DrawText(g)
            DrawArrow(g)
        End Sub
        ''' <summary>
        ''' Handles eSplitGlassButton Resize
        ''' </summary>
        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            Dim r As Rectangle = New Rectangle(New Point(-1, -1), New Size(Me.Width + _radius, Me.Height + _radius))
            If (Me.Size <> Nothing) Then
                Dim pathregion As GraphicsPath = New GraphicsPath()
                DrawArc(r, pathregion)
                Me.Region = New Region(pathregion)
            End If
            MyBase.OnResize(e)
        End Sub
        ''' <summary>
        ''' Hnadles ContaxtMenuStrip Rendering
        ''' </summary>
        Private Sub eSplitGlassButton_ContextMenuStripChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ContextMenuStripChanged
            If Not (Me.ContextMenuStrip Is Nothing) Then Me.ContextMenuStrip.Renderer = New MBMenuRendere
        End Sub

#End Region

#Region "   Paint Methods"
        ''' <summary>
        ''' Handles Glowing of eSplitGlassButton
        ''' </summary>
        Public Sub FillGradients(ByVal gr As Graphics, ByVal pa As GraphicsPath)
            Dim _origin As Integer = Me.Height / 3
            Dim _end As Integer = Me.Height
            Dim _oe As Integer = (_end - _origin) / 2
            Dim lgbrush As LinearGradientBrush
            Dim rect As Rectangle
            If _showbase = MB_ShowBase.Yes Then
                rect = New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1))
                pa = New GraphicsPath()
                DrawArc(rect, pa)
                lgbrush = New LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical)
                Dim pos As Single() = New Single(3) {0.0F, 0.3F, 0.35F, 1.0F}
                Dim colors As Color()
                If i_mode = 0 Then
                    colors = New Color(3) {GetColor(0, 35, 24, 9), GetColor(0, 13, 8, 3), Color.FromArgb(A0, R0, G0, B0), GetColor(0, 28, 29, 14)}
                Else
                    colors = New Color(3) {GetColor(0, 0, 50, 100), GetColor(0, 0, 0, 30), Color.FromArgb(A0, R0, G0, B0), GetColor(0, 0, 50, 100)}
                End If
                Dim mix As ColorBlend = New ColorBlend()
                mix.Colors = colors
                mix.Positions = pos
                lgbrush.InterpolationColors = mix
                gr.FillPath(lgbrush, pa)
                rect = New Rectangle(New Point(0, 0), New Size(Me.Width, Me.Height / 3))
                pa = New GraphicsPath()
                Dim _rtemp As Integer = _radius
                _radius = _rtemp - 1
                DrawArc(rect, pa)
                If (A0 > 80) Then
                    gr.FillPath(New SolidBrush(Color.FromArgb(60, 255, 255, 255)), pa)
                End If
                _radius = _rtemp
                If (_splitbutton = MB_SplitButton.Yes And mouse) Then
                    FillSplitPart(gr)
                End If
                If (i_mode = 2) Then
                    rect = New Rectangle(1, 1, Me.Width - 2, Me.Height)
                    pa = New GraphicsPath()
                    DrawShadow(rect, pa)
                    gr.DrawPath(New Pen(Color.FromArgb(50, 20, 20, 20), 2.0F), pa)
                Else
                    rect = New Rectangle(1, 1, Me.Width - 2, Me.Height - 1)
                    pa = New GraphicsPath()
                    DrawShadow(rect, pa)
                    If (A0 > 80) Then
                        gr.DrawPath(New Pen(Color.FromArgb(100, 250, 250, 250), 3.0F), pa)
                    End If
                End If
                If (_splitbutton = MB_SplitButton.Yes) Then
                    If (_splitlocation = MB_SplitLocation.Bottom) Then
                        Select Case i_mode
                            Case 1
                                gr.DrawLine(New Pen(_onStroke), New Point(1, Me.Height - _splitdistance), New Point(Me.Width - 1, Me.Height - _splitdistance))
                            Case 2
                                gr.DrawLine(New Pen(_pressStroke), New Point(1, Me.Height - _splitdistance), New Point(Me.Width - 1, Me.Height - _splitdistance))
                        End Select
                    ElseIf (_splitlocation = MB_SplitLocation.Right) Then
                        Select Case i_mode
                            Case 1
                                gr.DrawLine(New Pen(_onStroke), New Point(Me.Width - _splitdistance, 0), New Point(Me.Width - _splitdistance, Me.Height))
                            Case 2
                                gr.DrawLine(New Pen(_pressStroke), New Point(Me.Width - _splitdistance, 0), New Point(Me.Width - _splitdistance, Me.Height))
                        End Select
                    End If
                End If
                rect = New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1))
                pa = New GraphicsPath()
                DrawArc(rect, pa)
                gr.DrawPath(New Pen(_colorStroke, 0.9F), pa)
                pa.Dispose()
                lgbrush.Dispose()
            End If
        End Sub
        Dim offsetx As Integer = 0, offsety As Integer = 0, imageheight As Integer = 0, imagewidth As Integer = 0
        Public Sub DrawImage(ByVal gr As Graphics)
            If Me.Image Is Nothing Then Return
            Dim r As Rectangle = New Rectangle(8, 8, Me.ImageSize.Width, Me.ImageSize.Height)
            Select Case Me.ImageAlign
                Case ContentAlignment.TopCenter
                    r = New Rectangle(Me.Width / 2 - Me.ImageSize.Width / 2, 8, Me.ImageSize.Width, Me.ImageSize.Height)
                Case ContentAlignment.TopRight
                    r = New Rectangle(Me.Width - 8 - Me.ImageSize.Width, 8, Me.ImageSize.Width, Me.ImageSize.Height)
                Case ContentAlignment.MiddleLeft
                    r = New Rectangle(8, Me.Height / 2 - Me.ImageSize.Height / 2, Me.ImageSize.Width, Me.ImageSize.Height)
                Case ContentAlignment.MiddleCenter
                    r = New Rectangle(Me.Width / 2 - Me.ImageSize.Width / 2, Me.Height / 2 - Me.ImageSize.Height / 2, Me.ImageSize.Width, Me.ImageSize.Height)
                Case ContentAlignment.MiddleRight
                    r = New Rectangle(Me.Width - 8 - Me.ImageSize.Width, Me.Height / 2 - Me.ImageSize.Height / 2, Me.ImageSize.Width, Me.ImageSize.Height)
                Case ContentAlignment.BottomLeft
                    r = New Rectangle(8, Me.Height - 8 - Me.ImageSize.Height, Me.ImageSize.Width, Me.ImageSize.Height)
                Case ContentAlignment.BottomCenter
                    r = New Rectangle(Me.Width / 2 - Me.ImageSize.Width / 2, Me.Height - 8 - Me.ImageSize.Height, Me.ImageSize.Width, Me.ImageSize.Height)
                Case ContentAlignment.BottomRight
                    r = New Rectangle(Me.Width - 8 - Me.ImageSize.Width, Me.Height - 8 - Me.ImageSize.Height, Me.ImageSize.Width, Me.ImageSize.Height)
            End Select
            gr.DrawImage(Me.Image, r)
        End Sub
        ''' <summary>
        ''' Draw Text on eSplitGlassButton
        ''' </summary>
        ''' <param name="g">G as Graphics</param>
        Public Sub DrawText(ByVal g As Graphics)
            Dim sf As StringFormat = TextAlignment(Me.TextAlign)
            Dim r As Rectangle
            If SplitButton = MB_SplitButton.Yes And SplitLocation = MB_SplitLocation.Right Then
                r = New Rectangle(8, 8, Me.Width - 17 - _splitdistance, Me.Height - 17)
            Else
                r = New Rectangle(8, 8, Me.Width - 17, Me.Height - 17)
            End If
            g.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), r, sf)
        End Sub
        ''' <summary>
        ''' Text Alignment of eSplitGlassButton
        ''' </summary>
        ''' <param name="textalign">TextAlignment Value</param>
        ''' <returns>String Format</returns>
        Private Function TextAlignment(ByVal textalign As ContentAlignment)
            Dim sf As StringFormat = New StringFormat()
            Select Case textalign
                Case ContentAlignment.TopLeft
                    sf.LineAlignment = StringAlignment.Near
                Case ContentAlignment.TopCenter
                    sf.LineAlignment = StringAlignment.Near
                Case ContentAlignment.TopRight
                    sf.LineAlignment = StringAlignment.Near
                Case ContentAlignment.MiddleLeft
                    sf.LineAlignment = StringAlignment.Center
                Case ContentAlignment.MiddleCenter
                    sf.LineAlignment = StringAlignment.Center
                Case ContentAlignment.MiddleRight
                    sf.LineAlignment = StringAlignment.Center
                Case ContentAlignment.BottomLeft
                    sf.LineAlignment = StringAlignment.Far
                Case ContentAlignment.BottomCenter
                    sf.LineAlignment = StringAlignment.Far
                Case ContentAlignment.BottomRight
                    sf.LineAlignment = StringAlignment.Far
            End Select
            Select Case textalign
                Case ContentAlignment.TopLeft
                    sf.Alignment = StringAlignment.Near
                Case ContentAlignment.MiddleLeft
                    sf.Alignment = StringAlignment.Near
                Case ContentAlignment.BottomLeft
                    sf.Alignment = StringAlignment.Near
                Case ContentAlignment.TopCenter
                    sf.Alignment = StringAlignment.Center
                Case ContentAlignment.MiddleCenter
                    sf.Alignment = StringAlignment.Center
                Case ContentAlignment.BottomCenter
                    sf.Alignment = StringAlignment.Center
                Case ContentAlignment.TopRight
                    sf.Alignment = StringAlignment.Far
                Case ContentAlignment.MiddleRight
                    sf.Alignment = StringAlignment.Far
                Case ContentAlignment.BottomRight
                    sf.Alignment = StringAlignment.Far
            End Select
            Return sf
        End Function
        ''' <summary>
        ''' Draw arc for eSplitGlassButton
        ''' </summary>
        ''' <param name="re">R As Rectangle</param>
        ''' <param name="pa">Path As Graphics Path</param>
        Public Sub DrawArc(ByVal re As Rectangle, ByVal pa As GraphicsPath)
            If _radius < 2 Then _radius = 2
            Dim _radiusX0Y0 As Integer = _radius, _radiusXFY0 As Integer = _radius, _radiusX0YF As Integer = _radius, _radiusXFYF As Integer = _radius
            Select Case _grouppos
                Case MB_GroupPos.Left
                    _radiusXFY0 = 1
                    _radiusXFYF = 1
                Case MB_GroupPos.Center
                    _radiusX0Y0 = 1
                    _radiusX0YF = 1
                    _radiusXFY0 = 1
                    _radiusXFYF = 1
                Case MB_GroupPos.Right
                    _radiusX0Y0 = 1
                    _radiusX0YF = 1
                Case MB_GroupPos.Top
                    _radiusX0YF = 1
                    _radiusXFYF = 1
                Case MB_GroupPos.Bottom
                    _radiusX0Y0 = 1
                    _radiusXFY0 = 1
            End Select
            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90)
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90)
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90)
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90)
            pa.CloseFigure()
        End Sub
        ''' <summary>
        ''' Draw Shadow for eSplitGlassButton
        ''' </summary>
        ''' <param name="re">R As Rectangle</param>
        ''' <param name="pa">Path As Graphics Path</param>
        Public Sub DrawShadow(ByVal re As Rectangle, ByVal pa As GraphicsPath)
            Dim _radiusX0Y0 As Integer = _radius, _radiusXFY0 As Integer = _radius, _radiusX0YF As Integer = _radius, _radiusXFYF As Integer = _radius
            Select Case _grouppos
                Case MB_GroupPos.Left
                    _radiusXFY0 = 1
                    _radiusXFYF = 1
                Case MB_GroupPos.Center
                    _radiusX0Y0 = 1
                    _radiusX0YF = 1
                    _radiusXFY0 = 1
                    _radiusXFYF = 1
                Case MB_GroupPos.Right
                    _radiusX0Y0 = 1
                    _radiusX0YF = 1
                Case MB_GroupPos.Top
                    _radiusX0YF = 1
                    _radiusXFYF = 1
                Case MB_GroupPos.Bottom
                    _radiusX0Y0 = 1
                    _radiusXFY0 = 1
            End Select
            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90)
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90)
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90)
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90)
            pa.CloseFigure()

        End Sub
        ''' <summary>
        ''' Draw Arrow on eSplitGlassButton
        ''' </summary>
        ''' <param name="gr">graphics As Graphics</param>
        ''' <remarks></remarks>
        Public Sub DrawArrow(ByVal gr As Graphics)
            Dim _size As Integer = 1

            Dim __color As MBColor = New MBColor(Color.FromArgb(R0, G0, B0))
            Dim forecolor As MBColor = New MBColor(Me.ForeColor)
            Dim _forecolor As Color
            If (__color.GetBrightness() > 50) Then
                forecolor.BC = 1
                forecolor.SC = 80
            Else
                forecolor.BC = 99
                forecolor.SC = 20
            End If
            _forecolor = forecolor.GetColor()
            Select Case _arrow
                Case MB_Arrow.ToDown
                    If (_splitlocation = MB_SplitLocation.Right) Then
                        Dim arrowxpos As Integer = Me.Width - _splitdistance - 4
                        If _splitdistance = 0 Then arrowxpos = arrowxpos - 10
                        Dim points As Point() = New Point(2) {New Point((_splitdistance / 2) + arrowxpos + 8 * _size - _imageoffset, Me.Height / 2 - _size / 2), New Point((_splitdistance / 2) + arrowxpos + 0.5 * _size - _imageoffset, Me.Height / 2 - _size / 2), New Point((_splitdistance / 2) + arrowxpos + 4 * _size - _imageoffset, Me.Height / 2 + _size * 2)}
                        gr.FillPolygon(New SolidBrush(_forecolor), points)
                    ElseIf (_splitlocation = MB_SplitLocation.Bottom) Then
                        Dim points As Point() = New Point(2) {New Point(Me.Width / 2 + 5 * _size - _imageoffset, Me.Height - _imageoffset - 5 * _size), New Point(Me.Width / 2 - 4 * _size - _imageoffset, Me.Height - _imageoffset - 5 * _size), New Point(Me.Width / 2 + 1 * _size - _imageoffset, Me.Height - _imageoffset - 2 * _size)}
                        gr.FillPolygon(New SolidBrush(_forecolor), points)
                    End If
                Case MB_Arrow.ToRight
                    If (_splitlocation = MB_SplitLocation.Right) Then
                        Dim arrowxpos As Integer = Me.Width - _splitdistance + 4 * _imageoffset
                        If _splitdistance = 0 Then arrowxpos = arrowxpos - 15
                        Dim points As Point() = New Point(2) {New Point(arrowxpos + 4, Me.Height / 2 - 4 * _size), New Point(arrowxpos + 8, Me.Height / 2), New Point(arrowxpos + 4, Me.Height / 2 + 4 * _size)}
                        gr.FillPolygon(New SolidBrush(_forecolor), points)
                    End If
            End Select
        End Sub

        Public Sub FillSplitPart(ByVal g As Graphics)
            Dim _tranp As Color = Color.FromArgb(200, 255, 255, 255)
            Dim x1 As Int32 = Me.Width - _splitdistance
            Dim x2 As Int32 = 0
            Dim y1 As Int32 = Me.Height - _splitdistance, y2 As Int32 = 0
            Dim btransp As SolidBrush = New SolidBrush(_tranp)
            If _splitlocation = MB_SplitLocation.Right Then
                If xmouse < Me.Width - _splitdistance And mouse Then
                    Dim _r As Rectangle = New Rectangle(x1 + 1, 1, Me.Width - 2, Me.Height - 1)
                    Dim p As GraphicsPath = New GraphicsPath()
                    Dim _rtemp As Int32 = _radius
                    _radius = 4
                    DrawArc(_r, p)
                    _radius = _rtemp
                    g.FillPath(btransp, p)
                ElseIf mouse Then
                    Dim _r As Rectangle = New Rectangle(x2 + 1, 1, Me.Width - _splitdistance - 1, Me.Height - 1)
                    Dim p As GraphicsPath = New GraphicsPath()
                    Dim _rtemp As Int32 = _radius
                    _radius = 4
                    DrawArc(_r, p)
                    _radius = _rtemp
                    g.FillPath(btransp, p)
                End If
            ElseIf _splitlocation = MB_SplitLocation.Bottom Then
                If (ymouse < Me.Height - _splitdistance And mouse) Then
                    Dim _r As Rectangle = New Rectangle(1, y1 + 1, Me.Width - 1, Me.Height - 1)
                    Dim p As GraphicsPath = New GraphicsPath()
                    Dim _rtemp As Int32 = _radius
                    _radius = 4
                    DrawArc(_r, p)
                    _radius = _rtemp
                    g.FillPath(btransp, p)
                ElseIf mouse Then
                    Dim _r As Rectangle = New Rectangle(1, y2 + 1, Me.Width - 1, Me.Height - _splitdistance - 1)
                    Dim p As GraphicsPath = New GraphicsPath()
                    Dim _rtemp As Int32 = _radius
                    _radius = 4
                    DrawArc(_r, p)
                    _radius = _rtemp
                    g.FillPath(btransp, p)
                End If
            End If
            btransp.Dispose()
        End Sub

#End Region

#Region "   Fading Properties"
        Private Timer1 As Timer = New Timer
        Dim i_factor As Int32 = 35
        ''' <summary>
        ''' Get or Set Fading Speeed of eSplitGlassButton
        ''' </summary>
        <Category("eSplitGlassButton Appearance"), _
        Description("Set Fading Speed of eSplitGlassButton"), _
        DefaultValue(35), Browsable(True)> _
        Public Property GlowingingSpeed() As Int32
            Get
                Return i_factor
            End Get
            Set(ByVal value As Int32)
                If value > -1 Then i_factor = value
            End Set
        End Property

        Dim i_fR As Int32 = 1, i_fG As Int32 = 1, i_fB As Int32 = 1, i_fA As Int32 = 1
        Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
            If i_mode = 1 Then
                If (System.Math.Abs(OnColor.R - R0) > i_factor) Then
                    i_fR = i_factor
                Else
                    i_fR = 1
                End If
                If System.Math.Abs(OnColor.G - G0) > i_factor Then
                    i_fG = i_factor
                Else
                    i_fG = 1
                End If
                If System.Math.Abs(OnColor.B - B0) > i_factor Then
                    i_fB = i_factor
                Else
                    i_fB = 1
                End If
                If (OnColor.R < R0) Then
                    R0 -= i_fR
                ElseIf (OnColor.R > R0) Then
                    R0 += i_fR
                End If
                If (OnColor.G < G0) Then
                    G0 -= i_fG
                ElseIf (OnColor.G > G0) Then
                    G0 += i_fG
                End If
                If (OnColor.B < B0) Then
                    B0 -= i_fB
                ElseIf (OnColor.B > B0) Then
                    B0 += i_fB
                End If
                If OnColor = Color.FromArgb(R0, G0, B0) Then
                    Timer1.Stop()
                Else
                    Me.Refresh()
                End If
            End If
            If i_mode = 0 Then
                If (System.Math.Abs(BaseColor.R - R0) < i_factor) Then
                    i_fR = 1
                Else
                    i_fR = i_factor
                End If
                If (System.Math.Abs(BaseColor.G - G0) < i_factor) Then
                    i_fG = 1
                Else
                    i_fG = i_factor
                End If
                If (System.Math.Abs(BaseColor.B - B0) < i_factor) Then
                    i_fB = 1
                Else
                    i_fB = i_factor
                End If
                If (System.Math.Abs(BaseColor.A - A0) < i_factor) Then
                    i_fA = 1
                Else
                    i_fA = i_factor
                End If

                If (BaseColor.R < R0) Then
                    R0 -= i_fR
                ElseIf (BaseColor.R > R0) Then
                    R0 += i_fR
                End If
                If (BaseColor.G < G0) Then
                    G0 -= i_fG
                ElseIf (BaseColor.G > G0) Then
                    G0 += i_fG
                End If
                If (BaseColor.B < B0) Then
                    B0 -= i_fB
                ElseIf (BaseColor.B > B0) Then
                    B0 += i_fB
                End If
                If (BaseColor.A < A0) Then
                    A0 -= i_fA
                ElseIf (BaseColor.A > A0) Then
                    A0 += i_fA
                End If
                If (BaseColor = Color.FromArgb(A0, R0, G0, B0)) Then
                    Timer1.Stop()
                Else
                    Me.Refresh()
                End If
            End If
            Me.Refresh()
        End Sub
#End Region

#Region "   Mouse Events"
        Dim i_mode As Integer = 0
        Dim mouse As Boolean = False
        Dim xmouse As Int32 = 0, ymouse As Int32 = 0
        ''' <summary>
        ''' Handles Mouse Enter for eSplitGlassButton
        ''' </summary>
        Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
            MyBase.OnMouseEnter(e)
            _colorStroke = OnStrokeColor
            _tempshowbase = _showbase
            _showbase = MB_ShowBase.Yes
            i_mode = 1
            xmouse = PointToClient(Cursor.Position).X
            mouse = True
            A0 = 200
            If i_factor = 0 Then
                R0 = _onColor.R
                G0 = _onColor.G
                B0 = _onColor.B
            End If
            Timer1.Start()
        End Sub
        ''' <summary>
        ''' Handles Mouse Leave for eSplitGlassButton
        ''' </summary>
        Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
            MyBase.OnMouseLeave(e)
            UpdateMouseLeave()
        End Sub
        ''' <summary>
        ''' Handles Mouse Leave for eSplitGlassButton
        ''' </summary>
        Public Sub UpdateMouseLeave()
            If _keeppressed = False Or (_keeppressed = True And _ispressed = False) Then
                _colorStroke = BaseStrokeColor
                _showbase = _tempshowbase
                i_mode = 0
                mouse = False
                If i_factor = 0 Then
                    R0 = _baseColor.R
                    G0 = _baseColor.G
                    B0 = _baseColor.B
                    Me.Refresh()
                Else
                    Timer1.Stop()
                    Timer1.Start()
                End If
            End If
        End Sub
        ''' <summary>
        ''' Handles Mouse Down for eSplitGlassButton
        ''' </summary>
        Protected Overrides Sub OnMouseDown(ByVal mevent As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseDown(mevent)
            R0 = PressColor.R
            G0 = PressColor.G
            B0 = PressColor.B
            _colorStroke = PressStrokeColor
            _showbase = MB_ShowBase.Yes
            i_mode = 2
            xmouse = PointToClient(Cursor.Position).X
            ymouse = PointToClient(Cursor.Position).Y
            mouse = True
        End Sub
        ''' <summary>
        ''' Handles Mouse Up for eSplitGlassButton
        ''' </summary>
        Protected Overrides Sub OnMouseUp(ByVal mevent As System.Windows.Forms.MouseEventArgs)
            R0 = OnColor.R
            G0 = OnColor.G
            B0 = OnColor.B
            _colorStroke = OnStrokeColor
            _showbase = MB_ShowBase.Yes
            i_mode = 1
            mouse = True
            If _splitlocation = MB_SplitLocation.Right And xmouse > Me.Width - _splitdistance And _splitbutton = MB_SplitButton.Yes Then
                If (_arrow = MB_Arrow.ToDown) Then
                    If Not (Me.ContextMenuStrip Is Nothing) Then
                        Me.ContextMenuStrip.Opacity = 1.0
                        Me.ContextMenuStrip.Width = Me.Width
                        Me.ContextMenuStrip.Show(Me, 0, Me.Height)
                    End If
                ElseIf (_arrow = MB_Arrow.ToRight) Then
                    If Not (Me.ContextMenuStrip Is Nothing) Then
                        Dim menu As ContextMenuStrip = Me.ContextMenuStrip
                        menu.Width = Me.Width
                        Me.ContextMenuStrip.Opacity = 1.0
                        If (Me.MenuListPosition.Y = 0) Then
                            Me.ContextMenuStrip.Show(Me, Me.Width + 2, -Me.Height)
                        Else
                            Me.ContextMenuStrip.Show(Me, Me.Width + 2, Me.MenuListPosition.Y)
                        End If
                    End If
                End If
            ElseIf _splitlocation = MB_SplitLocation.Bottom And ymouse > Me.Height - _splitdistance And _splitbutton = MB_SplitButton.Yes Then
                If _arrow = MB_Arrow.ToDown Then
                    If Not (Me.ContextMenuStrip Is Nothing) Then
                        Me.ContextMenuStrip.Width = Me.Width
                        Me.ContextMenuStrip.Show(Me, 0, Me.Height)
                    End If

                End If
            Else
                MyBase.OnMouseUp(mevent)
                If (_keeppressed) Then
                    _ispressed = True
                    For Each _control As Control In Me.Parent.Controls
                        If _control.Name <> Me.Name And TypeOf (_control) Is eSplitGlassButton = True Then
                            CType(_control, eSplitGlassButton)._ispressed = False
                            CType(_control, eSplitGlassButton).UpdateMouseLeave()
                        End If
                    Next
                End If
            End If
        End Sub
        ''' <summary>
        ''' Handles Mouse Move for eSplitGlassButton
        ''' </summary>
        Protected Overrides Sub OnMouseMove(ByVal mevent As System.Windows.Forms.MouseEventArgs)
            If mouse And Me.SplitButton = MB_SplitButton.Yes Then
                xmouse = PointToClient(Cursor.Position).X
                ymouse = PointToClient(Cursor.Position).Y
                Me.Refresh()
            End If
            MyBase.OnMouseMove(mevent)
        End Sub
#End Region

    End Class
    Public Class MBColor

#Region "   Constructor"
        ''' <summary>
        ''' Constructor for MBColor
        ''' </summary>
        ''' <param name="color">Color Value</param>
        Public Sub New(ByVal color As Color)
            _rc = color.R
            _gc = color.G
            _bc = color.B
            _ac = color.A
            HSV()
        End Sub
        ''' <summary>
        ''' Constructor for MBColor
        ''' </summary>
        ''' <param name="a">Aplph value</param>
        ''' <param name="r">Red value</param>
        ''' <param name="g">Green value</param>
        ''' <param name="b">Blue value</param>
        Public Sub New(ByVal a As UInteger, ByVal r As Integer, ByVal g As Integer, ByVal b As Integer)
            _hc = r
            _gc = g
            _vc = b
            _ac = a
            GetColor()
        End Sub

#End Region

#Region "   Alpha"
        Private _ac As UInteger = 0
        Public Property AC() As UInteger
            Get
                Return _ac
            End Get
            Set(ByVal value As UInteger)
                System.Math.Min(value, 255)
            End Set
        End Property
#End Region

#Region "   RGB"

        Private _rc As Int32 = 0, _gc As Int32 = 0, _bc As Int32 = 0

        Public Property RC() As Int32
            Get
                Return _rc
            End Get
            Set(ByVal value As Int32)
                _rc = System.Math.Min(value, 255)
            End Set
        End Property

        Public Property GC() As Int32
            Get
                Return _gc
            End Get
            Set(ByVal value As Int32)
                _gc = System.Math.Min(value, 255)
            End Set
        End Property

        Public Property BC() As Int32
            Get
                Return _bc
            End Get
            Set(ByVal value As Int32)
                _bc = System.Math.Min(value, 255)
            End Set
        End Property

        Public Function GetColor() As Color
            Dim conv As Integer
            Dim hue As Double, sat As Double, val As Double
            Dim basis As Integer

            hue = CType(_hc / 100.0F, Single)
            sat = CType(_sc / 100.0F, Single)
            val = CType(_vc / 100.0F, Single)
            If _sc = 0 Then
                conv = (255.0F * val)
                _rc = _gc = _bc = conv
                Return Color.FromArgb(_rc, _gc, _bc)
            End If
            basis = 255.0F * (1.0 - sat) * val
            Select Case _hc / 60.0F
                Case 0
                    RC = 255.0F * val
                    GC = (255.0F * val - basis) * (HC / 60.0F) + basis
                    BC = basis
                Case 1
                    RC = ((255.0F * val - basis) * (1.0F - ((HC Mod 60) / 60.0F)) + basis)
                    GC = 255.0F * val
                    BC = basis
                Case 2
                    RC = basis
                    GC = (255.0F * val)
                    BC = (255.0F * val - basis) * ((HC Mod 60) / 60.0F) + basis
                Case 3
                    RC = basis
                    GC = (255.0F * val - basis) * (1.0F - ((HC Mod 60) / 60.0F)) + basis
                    BC = (255.0F * val)
                Case 4
                    RC = ((255.0F * val - basis) * ((HC Mod 60) / 60.0F) + basis)
                    GC = basis
                    BC = (255.0F * val)
                Case 5
                    RC = (255.0F * val)
                    GC = basis
                    BC = ((255.0F * val - basis) * (1.0F - ((HC Mod 60) / 60.0F)) + basis)
            End Select
            Return Color.FromArgb(_ac, _rc, _gc, _bc)
        End Function

#End Region

#Region "   HSV"
        Private _hc As Int32 = 0, _sc As Int32 = 0, _vc As Int32 = 0

        Public Property HC() As Single
            Get
                Return _hc
            End Get
            Set(ByVal value As Single)
                _hc = System.Math.Min(value, 310)
                _hc = System.Math.Max(HC, 0)
            End Set
        End Property

        Public Property SC() As Single
            Get
                Return _sc
            End Get
            Set(ByVal value As Single)
                _sc = System.Math.Min(value, 100)
                _sc = System.Math.Max(SC, 0)
            End Set
        End Property

        Public Property VC() As Single
            Get
                Return _vc
            End Get
            Set(ByVal value As Single)
                _vc = System.Math.Min(value, 100)
                _vc = System.Math.Max(VC, 0)
            End Set
        End Property

        Public Enum MB_Color
            RED
            GREEN
            BLUE
            NONE
        End Enum

        Private maxval As Integer = 0, minval As Integer = 0
        Private CompMax As MB_Color, CompMain As MB_Color

        Private Sub HSV()
            _hc = Me.GetHue()
            _sc = Me.GetSaturation()
            _vc = Me.GetBrightness()
        End Sub

        Public Sub ColorMax()
            If _rc > _gc Then
                If _rc < _bc Then
                    maxval = _bc
                    CompMax = MB_Color.BLUE
                Else
                    maxval = _rc
                    CompMax = MB_Color.RED
                End If
            Else
                If _gc < _bc Then
                    maxval = _bc
                    CompMax = MB_Color.BLUE
                Else
                    maxval = _gc
                    CompMax = MB_Color.GREEN
                End If
            End If
        End Sub

        Public Sub ColorMin()
            If _rc < _gc Then
                If _rc > _bc Then
                    maxval = _bc
                    CompMax = MB_Color.BLUE
                Else
                    maxval = _rc
                    CompMax = MB_Color.RED
                End If
            Else
                If _gc > _bc Then
                    maxval = _bc
                    CompMax = MB_Color.BLUE
                Else
                    maxval = _gc
                    CompMax = MB_Color.GREEN
                End If
            End If
        End Sub

        Public Function GetBrightness()
            ColorMax()
            Return 100 * maxval / 255
        End Function

        Public Function GetSaturation()
            ColorMax()
            ColorMin()
            If CompMax = MB_Color.NONE Then
                Return 0
            ElseIf maxval <> minval Then
                Dim d_sat As Double = Decimal.Divide(minval, maxval)
                d_sat = Decimal.Subtract(1, d_sat)
                d_sat = Decimal.Multiply(d_sat, 100)
                Return Convert.ToUInt16(d_sat)
            Else
                Return 0
            End If
        End Function

        Public Function GetHue() As UInt16
            ColorMax()
            ColorMin()
            If maxval = minval Then
                Return 0
            ElseIf CompMax = MB_Color.RED Then
                If GC >= BC Then
                    Dim d1 As Decimal = Decimal.Divide((GC - BC), (maxval - minval))
                    Return Convert.ToUInt16(60 * d1)
                Else
                    Dim d1 As Decimal = Decimal.Divide((BC - GC), (maxval - minval))
                    d1 = 60 * d1
                    Return Convert.ToUInt16(360 - d1)
                End If
            ElseIf CompMax = MB_Color.GREEN Then
                If (BC >= RC) Then
                    Dim d1 As Decimal = Decimal.Divide((BC - RC), (maxval - minval))
                    d1 = 60 * d1
                    Return Convert.ToUInt16(120 + d1)
                Else
                    Dim d1 As Decimal = Decimal.Divide((RC - BC), (maxval - minval))
                    d1 = 60 * d1
                    Return Convert.ToUInt16(120 - d1)
                End If
            ElseIf CompMax = MB_Color.BLUE Then
                If (RC >= GC) Then
                    Dim d1 As Decimal = Decimal.Divide((RC - GC), (maxval - minval))
                    d1 = 60 * d1
                    Return Convert.ToUInt16(240 + d1)
                Else
                    Dim d1 As Decimal = Decimal.Divide((GC - RC), (maxval - minval))
                    d1 = 60 * d1
                    Return Convert.ToUInt16(240 - d1)
                End If
            Else
                Return 0
            End If
        End Function

#End Region

#Region "   Methods"

        Public Sub SetBrightness(ByVal val As Integer)
            Me.VC = val
        End Sub

#End Region

    End Class
    Public Class MBMenuRendere
        Inherits ToolStripProfessionalRenderer
        Dim R0 As Int32 = 255, G0 As Int32 = 214, B0 As Int32 = 78
        Dim StrokeColor As Color = Color.FromArgb(196, 177, 118)
        ''' <summary>
        ''' Handles MenuItemBackground for eSplitGlassButton ContaxtMenuStrip
        ''' </summary>
        Protected Overrides Sub OnRenderMenuItemBackground(ByVal e As System.Windows.Forms.ToolStripItemRenderEventArgs)
            If e.Item.Selected Then
                Dim g As Graphics = e.Graphics
                g.SmoothingMode = SmoothingMode.HighQuality
                Dim pa As GraphicsPath = New GraphicsPath()
                Dim rect As Rectangle = New Rectangle(2, 1, e.Item.Size.Width - 2, e.Item.Size.Height - 1)
                DrawArc(rect, pa)
                Dim lgbrush As LinearGradientBrush = New LinearGradientBrush(rect, Color.White, Color.White, LinearGradientMode.Vertical)
                Dim pos As Single() = New Single(3) {0.0F, 0.4F, 0.45F, 1.0F}
                Dim colors As Color() = New Color(3) {GetRendererColor(0, 50, 100), GetRendererColor(0, 0, 30), Color.FromArgb(R0, G0, B0), GetRendererColor(0, 50, 100)}
                Dim mix As ColorBlend = New ColorBlend()
                mix.Colors = colors
                mix.Positions = pos
                lgbrush.InterpolationColors = mix
                g.FillPath(lgbrush, pa)
                g.DrawPath(New Pen(StrokeColor), pa)
                lgbrush.Dispose()
            Else
                MyBase.OnRenderMenuItemBackground(e)
            End If
        End Sub

        Dim offsetx As Int32 = 3, offsety As Int32 = 2, imageheight As Int32 = 0, imagewidth As Int32 = 0
        ''' <summary>
        ''' Handles RenderItemImage for eSplitGlassButton ContaxtMenuStrip
        ''' </summary>
        Protected Overrides Sub OnRenderItemImage(ByVal e As System.Windows.Forms.ToolStripItemImageRenderEventArgs)
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality
            If Not (e.Image Is Nothing) Then
                imageheight = e.Item.Height - offsety * 2
                imagewidth = ((Convert.ToDouble(imageheight) / e.Image.Height) * e.Image.Width)
            End If
            e.Graphics.DrawImage(e.Image, New Rectangle(offsetx, offsety, imagewidth, imageheight))
        End Sub

#Region "Paint Method"

        Private _radius As Int32 = 6
        ''' <summary>
        ''' Draw Arc for ContaxtMenuStripRenderer
        ''' </summary>
        Public Sub DrawArc(ByVal re As Rectangle, ByVal pa As GraphicsPath)
            Dim _radiusX0Y0 As Int32 = _radius, _radiusXFY0 As Int32 = _radius, _radiusX0YF As Int32 = _radius, _radiusXFYF As Int32 = _radius
            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90)
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90)
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90)
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90)
            pa.CloseFigure()
        End Sub
        ''' <summary>
        ''' Returns New Color for eSplitGlassButton ContextMenuStrip Renderer
        ''' </summary>
        ''' <param name="R">Red value as Integer</param>
        ''' <param name="G">Green value as Integer</param>
        ''' <param name="B">Blue value as Integer</param>
        ''' <returns>New Color</returns>
        Public Function GetRendererColor(ByVal R As Int32, ByVal G As Int32, ByVal B As Int32) As Color
            If (R + R0 > 255) Then R = 255 Else R = R + R0
            If (G + G0 > 255) Then G = 255 Else G = G + G0
            If (B + B0 > 255) Then B = 255 Else B = B + B0
            Return Color.FromArgb(R, G, B)
        End Function

#End Region

    End Class
End Namespace

#End Region


