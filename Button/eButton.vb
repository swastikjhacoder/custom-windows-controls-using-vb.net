
#Region "Imports"

Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design
Imports System.Drawing
Imports System.Windows.Forms

#End Region

Namespace UIControls.Button

#Region "eButton Class"

    ''' <summary>
    ''' Custom Button Control with Gradient Colors and Extra Image (VB.NET)
    ''' </summary>
    ''' <remarks>v2.0</remarks>
    <ToolboxItem(True), ToolboxBitmap(GetType(eButton), "eButton.bmp")> _
    <DefaultEvent("ClickButtonArea")> _
    <Designer(GetType(eButtonDesigner))> _
    Public Class eButton
        Inherits Control
        Implements IButtonControl

#Region " IButtonControl Implementation "
        Private m_DialogResult As DialogResult
        Private m_IsDefault As Boolean

        <Browsable(False)> _
        Public ReadOnly Property IsDefault() As Boolean
            Get
                Return m_IsDefault
            End Get
        End Property

        <Category("Behavior"), DefaultValue(GetType(DialogResult), "None"), _
        Description("The dialog result produced in a modal form by clicking the button.")> _
        Public Property DialogResult() As DialogResult Implements IButtonControl.DialogResult
            Get
                Return m_DialogResult
            End Get
            Set(ByVal Value As DialogResult)
                If [Enum].IsDefined(GetType(DialogResult), Value) Then
                    m_DialogResult = Value
                End If
            End Set
        End Property

        Public Sub NotifyDefault(ByVal value As Boolean) Implements IButtonControl.NotifyDefault
            If m_IsDefault <> value Then
                m_IsDefault = value
            End If
            Invalidate()
        End Sub

        Public Sub PerformClick() Implements IButtonControl.PerformClick
            If CanSelect Then
                PerformClickButtonArea(Me, New MouseEventArgs( _
                        System.Windows.Forms.MouseButtons.Left, 1, _
                        CInt(ButtonArea.X), CInt(ButtonArea.Y), 0))
            End If
        End Sub

        Private Sub PerformClickButtonArea(ByVal Sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            If m_DialogResult <> System.Windows.Forms.DialogResult.None Then
                FindForm.DialogResult = m_DialogResult
            End If
            RaiseEvent ClickButtonArea(Sender, e)
        End Sub

#End Region

#Region "Declarations"

        Private MouseDrawState As eMouseDrawState = eMouseDrawState.Up
        Private PressedOffset As Integer
        Private Imagept As PointF
        Private ButtonArea As RectangleF
        Private TextArea As RectangleF
        Private ImageArea As RectangleF
        Private ImageSizeUse As Size

        Private _HoverColorBlend As Color()
        Private _ClickColorBlend As Color()
        Private _DisabledBlend As Color()
        Private _HoverColorSolid As Color
        Private _ClickColorSolid As Color
        Private _DisabledSolid As Color
        Private rectSideImage As Rectangle
        'Add a new Click event for only when the ButtonArea or SideImage is Clicked
        Public Event ClickButtonArea(ByVal Sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Public Event SideImageClicked(ByVal Sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        'API to allow Click through on the space around buttonarea
        Private Const WM_LBUTTONDBLCLK As Integer = 515
        Private Const WM_LBUTTONDOWN As Integer = 513
        Private Const WM_LBUTTONUP As Integer = 514
        Private Const WM_MBUTTONDBLCLK As Integer = 521
        Private Const WM_MBUTTONDOWN As Integer = 519
        Private Const WM_MBUTTONUP As Integer = 520
        Private Const WM_MOUSEMOVE As Integer = 512
        Private Const WM_RBUTTONDBLCLK As Integer = 518
        Private Const WM_RBUTTONDOWN As Integer = 516
        Private Const WM_RBUTTONUP As Integer = 517

        Private Declare Function SendMessage Lib "user32" _
            Alias "SendMessageA" _
            (ByVal hwnd As System.IntPtr, _
            ByVal wMsg As Long, _
            ByVal wParam As Long, _
            ByVal lParam As System.IntPtr) As Long
#End Region

#Region "New"

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                    ControlStyles.Selectable Or _
                    ControlStyles.DoubleBuffer Or _
                    ControlStyles.ResizeRedraw Or _
                    ControlStyles.SupportsTransparentBackColor Or _
                    ControlStyles.UserPaint, True)
            Size = New Size(90, 25)
            ForeColor = Color.White
            Font = New Font("Arial", 10, FontStyle.Bold)
            UpdateDimColors()
            UpdateDimBlends()
        End Sub
        Public Sub New(ByVal text As String)
            MyBase.New(text)
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                      ControlStyles.Selectable Or _
                  ControlStyles.DoubleBuffer Or _
                    ControlStyles.ResizeRedraw Or _
                    ControlStyles.SupportsTransparentBackColor Or _
                    ControlStyles.UserPaint, True)
            Size = New Size(90, 25)
            ForeColor = Color.White
            Font = New Font("Arial", 10, FontStyle.Bold)
            UpdateDimColors()
            UpdateDimBlends()

        End Sub
        Public Sub New(ByVal text As String, ByVal left As Integer, ByVal top As Integer, ByVal width As Integer, ByVal height As Integer)
            MyBase.New(text, left, top, width, height)
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.Selectable Or _
                   ControlStyles.DoubleBuffer Or _
                    ControlStyles.ResizeRedraw Or _
                    ControlStyles.SupportsTransparentBackColor Or _
                    ControlStyles.UserPaint, True)
            Size = New Size(90, 25)
            ForeColor = Color.White
            Font = New Font("Arial", 10, FontStyle.Bold)
            UpdateDimColors()
            UpdateDimBlends()

        End Sub
        Public Sub New(ByVal parent As Control, ByVal text As String)
            MyBase.New(parent, text)
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.Selectable Or _
                   ControlStyles.DoubleBuffer Or _
                    ControlStyles.ResizeRedraw Or _
                    ControlStyles.SupportsTransparentBackColor Or _
                    ControlStyles.UserPaint, True)
            Size = New Size(90, 25)
            ForeColor = Color.White
            Font = New Font("Arial", 10, FontStyle.Bold)
            UpdateDimColors()
            UpdateDimBlends()

        End Sub
        Public Sub New(ByVal parent As Control, ByVal text As String, ByVal left As Integer, ByVal top As Integer, ByVal width As Integer, ByVal height As Integer)
            MyBase.New(parent, text, left, top, width, height)
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.AllPaintingInWmPaint Or _
                     ControlStyles.Selectable Or _
                   ControlStyles.DoubleBuffer Or _
                    ControlStyles.ResizeRedraw Or _
                    ControlStyles.SupportsTransparentBackColor Or _
                    ControlStyles.UserPaint, True)
            Size = New Size(90, 25)
            ForeColor = Color.White
            Font = New Font("Arial", 10, FontStyle.Bold)
            UpdateDimColors()
            UpdateDimBlends()

        End Sub

        <DefaultValue(GetType(Font), "Arial, 10pt, style=Bold")> _
        Public Overrides Property Font() As Font
            Get
                Return (MyBase.Font)
            End Get
            Set(ByVal value As Font)
                MyBase.Font = value
            End Set
        End Property

        <DefaultValue(GetType(Color), "White")> _
        Public Overrides Property ForeColor() As Color
            Get
                Return (MyBase.ForeColor)
            End Get
            Set(ByVal value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

#End Region

#Region "Properties"

        Enum eMouseDrawState
            Over
            Down
            Up
        End Enum

#Region "Shape"

#Region "Corners Expandable Property"

        'Corners Property is defined in the Corners Converter Class
        'to use the ExpandableObjectConverter to simulate the Padding Property

        Private _Corners As CornersProperty = DefaultCorners()

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Category("Appearance eButton"), _
          Description("Get or Set the Corner Radii")> _
        Public Property Corners() As CornersProperty
            Get
                Return _Corners
            End Get
            Set(ByVal Value As CornersProperty)
                _Corners = Value
                Invalidate()
            End Set
        End Property

        Private Function DefaultCorners() As CornersProperty
            Return New CornersProperty(6)
        End Function

#Region "Corners Default Value"

        Public Sub ResetCorners()
            Corners = DefaultCorners()
        End Sub

        Private Function ShouldSerializeCorners() As Boolean
            Return Not (_Corners.Equals(DefaultCorners))
        End Function

#End Region

#End Region 'Corners Expandable Property

        Enum eShape
            Ellipse
            Rectangle
            TriangleUp
            TriangleDown
            TriangleLeft
            TriangleRight
        End Enum

        Private _Shape As eShape = eShape.Rectangle
        ''' <summary>
        ''' The Shape of the Button as eShape choices (Ellipse, Rectangle, and Triangles)
        ''' </summary>
        <Category("Appearance eButton")> _
        <Description("The Shape of the Button as eShape choices (Ellipse, Rectangle, and Triangles)")> _
        <DefaultValue(GetType(eShape), "Rectangle")> _
        Public Property Shape() As eShape
            Get
                Return _Shape
            End Get
            Set(ByVal value As eShape)
                _Shape = value
                Invalidate()
            End Set
        End Property

#End Region 'Shape

#Region "DimFactor"

        Private _DimFactorHover As Integer = 50
        ''' <summary>
        ''' Get or Set how much to dim the color on mouse rollover. Positive to Lighten and negative to Darken
        ''' </summary>
        <Category("Appearance eButton")> _
        <Description("Get or Set how much to dim the color on mouse rollover. Positive to Lighten and negative to Darken")> _
        <DefaultValue(50)> _
        Public Property DimFactorHover() As Integer
            Get
                Return _DimFactorHover
            End Get
            Set(ByVal Value As Integer)
                _DimFactorHover = Value
                UpdateDimBlends()
                UpdateDimColors()
                Invalidate()
            End Set
        End Property

        Private _DimFactorClick As Integer = -25
        ''' <summary>
        ''' Get or Set how much to dim the color on mouse down. Positive to Lighten and negative to Darken
        ''' </summary>
        <Category("Appearance eButton")> _
        <Description("Get or Set how much to dim the color on mouse down. Positive to Lighten and negative to Darken")> _
        <DefaultValue(-25)> _
        Public Property DimFactorClick() As Integer
            Get
                Return _DimFactorClick
            End Get
            Set(ByVal Value As Integer)
                _DimFactorClick = Value
                UpdateDimBlends()
                UpdateDimColors()
                Invalidate()
            End Set
        End Property

#End Region 'DimFactor

#Region "Border"

        Private _BorderColor As Color = Color.SteelBlue
        ''' <summary>
        ''' Get or Set the Border color
        ''' </summary>
        <Category("Appearance eButton")> _
       <Description("Get or Set to show the Border")> _
       <DefaultValue(GetType(Color), "SteelBlue")> _
        Public Property BorderColor() As Color
            Get
                Return _BorderColor
            End Get
            Set(ByVal Value As Color)
                _BorderColor = Value
                Invalidate()
            End Set
        End Property

        Private _BorderShow As Boolean = True
        ''' <summary>
        ''' Get or Set whether to show the Border
        ''' </summary>
        <Category("Appearance eButton")> _
       <Description("Get or Set whether to show the Border")> _
       <DefaultValue(True)> _
        Public Property BorderShow() As Boolean
            Get
                Return _BorderShow
            End Get
            Set(ByVal Value As Boolean)
                _BorderShow = Value
                Invalidate()
            End Set
        End Property

#End Region 'Border

#Region "Fill"

        Enum eFillType
            Solid
            GradientLinear
            GradientPath
        End Enum

        Private _FillType As eFillType = eFillType.GradientLinear
        ''' <summary>
        ''' The eFillType Fill Type to apply to the eButton
        ''' </summary>
        <Description("The Fill Type to apply to the eButton")> _
        <Category("Appearance eButton")> _
        <DefaultValue(GetType(eFillType), "GradientLinear")> _
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
        Category("Appearance eButton")> _
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

        Private _ColorFillSolid As Color = SystemColors.Control
        ''' <summary>
        ''' The Solid Color to fill the eButton
        ''' </summary>
        <Description("The Solid Color to fill the eButton"), _
        Category("Appearance eButton")> _
        <DefaultValue(GetType(Color), "Control")> _
        Public Property ColorFillSolid() As Color
            Get
                Return _ColorFillSolid
            End Get
            Set(ByVal value As Color)
                _ColorFillSolid = value
                UpdateDimColors()
                Invalidate()
            End Set
        End Property

        Private _ColorFillBlend As cBlendItems = DefaultColorFillBlend()
        ''' <summary>
        ''' The ColorBlend used to fill the eButton
        ''' </summary>
        <Description("The ColorBlend used to fill the eButton"), _
        Category("Appearance eButton"), _
        RefreshProperties(RefreshProperties.All), _
        Editor(GetType(BlendTypeEditor), GetType(UITypeEditor))> _
        Public Property ColorFillBlend() As cBlendItems
            Get
                Return _ColorFillBlend
            End Get
            Set(ByVal value As cBlendItems)
                If value.iColor.Length = value.iPoint.Length Then
                    value.iPoint(0) = 0
                    value.iPoint(value.iPoint.Length - 1) = 1
                    _ColorFillBlend = value
                Else
                    MsgBox("The number of colors must match the number of positions. The blend will be reset")
                    Exit Property
                End If
                UpdateDimBlends()
                Invalidate()
            End Set
        End Property

        Private Function DefaultColorFillBlend() As cBlendItems
            Return New cBlendItems(New Color() {Color.AliceBlue, Color.RoyalBlue, Color.Navy}, New Single() {0, 0.5, 1})
        End Function

#Region "ColorFillBlend Default Value"

        Public Sub ResetColorFillBlend()
            ColorFillBlend = DefaultColorFillBlend()
        End Sub

        Private Function ShouldSerializeColorFillBlend() As Boolean
            Return Not (_ColorFillBlend.Equals(DefaultColorFillBlend))
        End Function
#End Region

#End Region 'Fill

#Region "Text"

        Private _Text As String = "eButton"
        ''' <summary>
        ''' Get or Set the Button Text
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the Button Text"), _
       DefaultValue("eButton")> _
       Overrides Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
                Invalidate()
            End Set
        End Property

        Private _TextSmoothingMode As Drawing.Text.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        ''' <summary>
        ''' Get or Set the TextrenderingHint fot the button text
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set if the text is rendered with a Smoothing technique"), _
       DefaultValue(GetType(Drawing.Text.TextRenderingHint), "ClearTypeGridFit")> _
        Public Property TextSmoothingMode() As Drawing.Text.TextRenderingHint
            Get
                Return _TextSmoothingMode
            End Get
            Set(ByVal Value As Drawing.Text.TextRenderingHint)
                _TextSmoothingMode = Value
                Invalidate()
            End Set
        End Property

        Private _TextAlign As ContentAlignment = ContentAlignment.MiddleCenter
        ''' <summary>
        ''' Get or Set the alignment for the text
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the alignment for the text"), _
       DefaultValue(GetType(ContentAlignment), "MiddleCenter")> _
        Public Property TextAlign() As ContentAlignment
            Get
                Return _TextAlign
            End Get
            Set(ByVal Value As ContentAlignment)
                _TextAlign = Value
                Invalidate()
            End Set
        End Property

        Private _TextMargin As Padding = New Padding(2)
        ''' <summary>
        ''' Get or Set the margion between the text and the button edge
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the margion between the text and the button edge"), _
       DefaultValue(GetType(Padding), "2,2,2,2")> _
        Public Property TextMargin() As Padding
            Get
                Return _TextMargin
            End Get
            Set(ByVal Value As Padding)
                _TextMargin = Value
                Invalidate()
            End Set
        End Property

        Private _TextImageRelation As TextImageRelation = TextImageRelation.Overlay
        ''' <summary>
        ''' Get or Set the Relationship of the Text to the Image
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the Relationship of the Text to the Image"), _
       DefaultValue(GetType(TextImageRelation), "Overlay")> _
        Public Property TextImageRelation() As TextImageRelation
            Get
                Return _TextImageRelation
            End Get
            Set(ByVal Value As TextImageRelation)
                _TextImageRelation = Value
                Invalidate()
            End Set
        End Property

        Private _TextShadowShow As Boolean = True
        ''' <summary>
        ''' Get or Set if the Text has a shadow
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set if the Text has a shadow"), _
       DefaultValue(True)> _
        Public Property TextShadowShow() As Boolean
            Get
                Return _TextShadowShow
            End Get
            Set(ByVal Value As Boolean)
                _TextShadowShow = Value
                Invalidate()
            End Set
        End Property

        Private _TextShadow As Color = Color.MidnightBlue
        ''' <summary>
        ''' Get or Set the color of the Shadow Text
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the color of the Shadow Text"), _
       DefaultValue(GetType(Color), "MidnightBlue")> _
        Public Property TextShadow() As Color
            Get
                Return _TextShadow
            End Get
            Set(ByVal Value As Color)
                _TextShadow = Value
                Invalidate()
            End Set
        End Property

#End Region 'Text

#Region "Image"

        Private _Image As Image
        ''' <summary>
        ''' Get or Set the small Image next to text
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the small Image next to text")> _
       <DefaultValue(GetType(Image), "none")> _
        Public Property Image() As Image
            Get
                Return _Image
            End Get
            Set(ByVal Value As Image)
                _Image = Value
                Invalidate()
            End Set
        End Property

        Private _ImageAlign As ContentAlignment = ContentAlignment.MiddleCenter
        ''' <summary>
        ''' Get or Set the placement of the Image
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the placement of the Image"), _
       DefaultValue(GetType(ContentAlignment), "MiddleCenter")> _
        Public Property ImageAlign() As ContentAlignment
            Get
                Return _ImageAlign
            End Get
            Set(ByVal Value As ContentAlignment)
                _ImageAlign = Value
                Invalidate()
            End Set
        End Property

        Private _ImageSize As Size = New Size(16, 16)
        ''' <summary>
        ''' Get or Set the Size of the Image
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the Size of the Image"), _
       DefaultValue(GetType(Size), "16, 16")> _
        Public Property ImageSize() As Size
            Get
                Return _ImageSize
            End Get
            Set(ByVal Value As Size)
                _ImageSize = Value
                Invalidate()
            End Set
        End Property

        Private _Imagelist As New ImageList
        ''' <summary>
        ''' Get or Set the ImageList control
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the ImageList control")> _
        Public Property Imagelist() As ImageList
            Get
                Return _Imagelist
            End Get
            Set(ByVal Value As ImageList)
                _Imagelist = Value
                Invalidate()
            End Set
        End Property

        Private _ImageIndex As Integer
        ''' <summary>
        ''' Get or Set the ImageList control
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the ImageList control")> _
        Public Property ImageIndex() As Integer
            Get
                Return _ImageIndex
            End Get
            Set(ByVal Value As Integer)
                If Imagelist.Images.Count > 0 Then
                    If Value >= 0 And Value < Imagelist.Images.Count Then
                        _ImageIndex = Value
                        Image = Imagelist.Images.Item(Value)
                        Invalidate()
                    End If
                End If
            End Set
        End Property

#End Region 'Image

#Region "SideImage"

        Private _SideImage As Image
        ''' <summary>
        ''' Get or Set the Side Image
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the Side Image")> _
       <DefaultValue(GetType(Image), "none")> _
        Public Property SideImage() As Image
            Get
                Return _SideImage
            End Get
            Set(ByVal Value As Image)
                _SideImage = Value
                If Not Value Is Nothing Then
                    _SideImageSize = Value.Size
                End If
                Invalidate()
            End Set
        End Property

        Private _SideImageSize As Size = New Size(48, 48)
        ''' <summary>
        ''' Get or Set the Size of the Side Image
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the Size of the Side Image"), _
       DefaultValue(GetType(Size), "48, 48")> _
        Public Property SideImageSize() As Size
            Get
                Return _SideImageSize
            End Get
            Set(ByVal Value As Size)
                _SideImageSize = Value
                Invalidate()
            End Set
        End Property

        Private _SideImageIsClickable As Boolean
        ''' <summary>
        ''' Get or Set if the Side Image raises its own click event
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set if the Side Image raises its own click event"), _
       DefaultValue(False)> _
        Public Property SideImageIsClickable() As Boolean
            Get
                Return _SideImageIsClickable
            End Get
            Set(ByVal Value As Boolean)
                _SideImageIsClickable = Value
            End Set
        End Property

        Private _SideImageBehindText As Boolean = True
        ''' <summary>
        ''' Get or Set if the Side Image is in front or behind the Text
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set if the Side Image is in front or behind the Text"), _
       DefaultValue(True)> _
        Public Property SideImageBehindText() As Boolean
            Get
                Return _SideImageBehindText
            End Get
            Set(ByVal Value As Boolean)
                _SideImageBehindText = Value
                Invalidate()
            End Set
        End Property

        Private _SideImageAlign As ContentAlignment = ContentAlignment.MiddleLeft
        ''' <summary>
        ''' Get or Set the Side Image Alignment
        ''' </summary>
        <Category("Appearance eButton"), _
       Description("Get or Set the Side Image Alignment"), _
       DefaultValue(GetType(ContentAlignment), "MiddleLeft")> _
        Public Property SideImageAlign() As ContentAlignment
            Get
                Return _SideImageAlign
            End Get
            Set(ByVal Value As ContentAlignment)
                _SideImageAlign = Value
                Invalidate()
            End Set
        End Property

#End Region 'SideImage

#Region "FocalPoints"

        Private _FocalPoints As cFocalPoints = New cFocalPoints(0.5, 0.5, 0, 0)
        ''' <summary>
        ''' The CenterPoint and FocusScales for the ColorBlend
        ''' </summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Description("The CenterPoint and FocusScales for the ColorBlend"), _
        Category("Appearance eButton")> _
        Public Property FocalPoints() As cFocalPoints
            Get
                Return _FocalPoints
            End Get
            Set(ByVal value As cFocalPoints)
                _FocalPoints = value
                CenterPtTracker.TrackerRectangle = CenterPtTrackerRectangle()
                FocusPtTracker.TrackerRectangle = FocusPtTrackerRectangle()
                Invalidate()
            End Set
        End Property

        Public Sub ResetFocalPoints()
            FocalPoints = New cFocalPoints(0.5, 0.5, 0, 0)
        End Sub

        Private Function ShouldSerializeFocalPoints() As Boolean
            Return Not (_FocalPoints.Equals(New cFocalPoints(0.5, 0.5, 0, 0)))
        End Function

        ''' <summary>
        ''' This gets the Rectangle around the CenterPt for the DesignTime interaction
        ''' </summary>
        Private Function CenterPtTrackerRectangle() As RectangleF
            Return New RectangleF((FocalPoints.CenterPoint.X * Width) - 5, _
                        (FocalPoints.CenterPoint.Y * Height) - 5, 10, 10)
        End Function

        ''' <summary>
        ''' This gets the Rectangle around the FocusPt for the DesignTime interaction
        ''' </summary>
        Private Function FocusPtTrackerRectangle() As RectangleF
            Return New RectangleF((FocalPoints.FocusScales.X * Width) - 5, _
                            (FocalPoints.FocusScales.Y * Height) - 5, 10, 10)
        End Function

        Private _CenterPtTracker As New DesignerRectTracker

        <Browsable(False)> _
        <Category("Shape")> _
        Public Property CenterPtTracker() As DesignerRectTracker
            Get
                Return _CenterPtTracker
            End Get
            Set(ByVal value As DesignerRectTracker)
                _CenterPtTracker = value
            End Set
        End Property

        Private _FocusPtTracker As New DesignerRectTracker

        <Browsable(False)> _
        <Category("Shape")> _
        Public Property FocusPtTracker() As DesignerRectTracker
            Get
                Return _FocusPtTracker
            End Get
            Set(ByVal value As DesignerRectTracker)
                _FocusPtTracker = value
            End Set
        End Property

#End Region 'FocalPoints

#Region "Mneumonic"

        Private _UseMnemonic As System.Boolean = True
        ''' <summary>
        ''' If true, the first character proceeded by an ampersand will be used as the button's mnemonic key.
        ''' </summary>
        <Category("Appearance eButton"), _
        Description("If true, the first character proceeded by an ampersand (&) will be used as the button's mnemonic key."), _
        DefaultValue(True)> _
        Public Property UseMnemonic() As System.Boolean
            Get
                Return _UseMnemonic
            End Get
            Set(ByVal value As System.Boolean)
                _UseMnemonic = value
                Invalidate()
            End Set
        End Property

#End Region 'Mneumonic

#End Region 'Properties

#Region "Drawing"

        Protected Overrides Sub _
            OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.TextRenderingHint = _TextSmoothingMode
            'Gray the Text and Border Colors if Disabled
            Dim bColor, tColor, tsColor As Color
            If Enabled Then
                bColor = _BorderColor
                tColor = ForeColor
                tsColor = _TextShadow
            Else
                bColor = GrayTheColor(_BorderColor)
                tColor = GrayTheColor(ForeColor)
                tsColor = GrayTheColor(_TextShadow)
            End If

            Dim MyPen As New Pen(bColor)
            MyPen.Alignment = PenAlignment.Inset

            'Shrink the Area so the Border draws correctly, _
            'then trim off the Padding to get the button surface area
            ButtonArea = AdjustRect(New RectangleF(0, 0, _
                Size.Width - 1, Size.Height - 1), Padding)

            'Create the ButtonArea Path
            Dim gp As GraphicsPath = GetPath()

            If BackgroundImage Is Nothing Then

                'Color the ButtonArea with the right Brush
                Select Case FillType
                    Case eFillType.Solid
                        Using br As Brush = New SolidBrush(GetFill)
                            e.Graphics.FillPath(br, gp)
                        End Using

                    Case eFillType.GradientPath
                        Using br As PathGradientBrush = New PathGradientBrush(gp)
                            Dim cb As New ColorBlend
                            cb.Colors = GetFillBlend()
                            cb.Positions = ColorFillBlend.iPoint

                            br.FocusScales = FocalPoints.FocusScales
                            br.CenterPoint = New PointF( _
                                Width * FocalPoints.CenterPoint.X, _
                                Height * FocalPoints.CenterPoint.Y)
                            br.InterpolationColors = cb

                            e.Graphics.FillPath(br, gp)
                        End Using

                    Case eFillType.GradientLinear
                        Using br As LinearGradientBrush = New LinearGradientBrush( _
                          ButtonArea, Color.White, Color.White, FillTypeLinear)
                            Dim cb As New ColorBlend
                            cb.Colors = GetFillBlend()
                            cb.Positions = ColorFillBlend.iPoint
                            'MsgBox(cb.Colors.Length & cb.Positions.Length)
                            br.InterpolationColors = cb

                            e.Graphics.FillPath(br, gp)
                        End Using

                End Select
            End If

            If BorderShow Then
                e.Graphics.DrawPath(MyPen, gp)
            End If
            gp.Dispose()

            Dim ipt As PointF = ImageLocation(GetStringFormat(SideImageAlign), _
                Size, SideImageSize)

            rectSideImage = New Rectangle(CInt(ipt.X), CInt(ipt.Y), _
                SideImageSize.Width, SideImageSize.Height)

            'Put the SideImage behind the Text
            If SideImageBehindText AndAlso SideImage IsNot Nothing Then
                If Enabled Then
                    e.Graphics.SmoothingMode = SmoothingMode.None
                    e.Graphics.DrawImage(SideImage, ipt.X, ipt.Y, _
                        SideImageSize.Width, SideImageSize.Height)
                Else
                    ControlPaint.DrawImageDisabled(e.Graphics, _
                    New Bitmap(SideImage, SideImageSize.Width, _
                    SideImageSize.Height), _
                    CInt(ipt.X), CInt(ipt.Y), BackColor)
                End If
            End If

            'Layout the Text and Image on the button surface
            SetImageAndText(e.Graphics)

            If Not Image Is Nothing Then
                If Enabled Then
                    e.Graphics.DrawImage(Image, Imagept.X, Imagept.Y, _
                        ImageSize.Width, ImageSize.Height)
                Else
                    ControlPaint.DrawImageDisabled(e.Graphics, Image, _
                        CInt(Imagept.X), CInt(Imagept.Y), BackColor)
                End If
            End If

            'Draw the Text and Shadow
            If TextShadowShow Then
                TextArea.Offset(1, 1)
                e.Graphics.DrawString(Text, Font, _
                    New SolidBrush(tsColor), TextArea, GetStringFormat(TextAlign))
                TextArea.Offset(-1, -1)
            End If
            e.Graphics.DrawString(Text, Font, _
                New SolidBrush(tColor), TextArea, GetStringFormat(TextAlign))

            'Put the SideImage in front of the Text
            If Not SideImageBehindText AndAlso Not SideImage Is Nothing Then
                If Enabled Then
                    e.Graphics.SmoothingMode = SmoothingMode.None
                    e.Graphics.DrawImage(SideImage, ipt.X, ipt.Y, _
                        SideImageSize.Width, SideImageSize.Height)
                Else
                    ControlPaint.DrawImageDisabled(e.Graphics, _
                    New Bitmap(SideImage, _
                    SideImageSize.Width, SideImageSize.Height), _
                    CInt(ipt.X), CInt(ipt.Y), BackColor)
                End If
            End If

            MyPen.Dispose()
        End Sub

        Private Function GetPath() As GraphicsPath
            Dim gp As New GraphicsPath

            Select Case _Shape

                Case eShape.Ellipse
                    gp.AddEllipse(ButtonArea)

                Case eShape.Rectangle
                    gp = GetRoundedRectPath(ButtonArea)

                Case eShape.TriangleUp
                    Dim pts() As PointF = New PointF() { _
                        New PointF(CSng(ButtonArea.Width / 2), ButtonArea.Y), _
                        New PointF(ButtonArea.Width, ButtonArea.Y + ButtonArea.Height), _
                        New PointF(ButtonArea.X, ButtonArea.Y + ButtonArea.Height)}
                    gp.AddPolygon(pts)

                Case eShape.TriangleDown
                    Dim pts() As PointF = New PointF() { _
                        New PointF(ButtonArea.X, ButtonArea.Y), _
                        New PointF(CSng(ButtonArea.Width / 2), ButtonArea.Y + ButtonArea.Height), _
                        New PointF(ButtonArea.X + ButtonArea.Width, ButtonArea.Y)}
                    gp.AddPolygon(pts)

                Case eShape.TriangleLeft
                    Dim pts() As PointF = New PointF() { _
                        New PointF(ButtonArea.X, CSng(ButtonArea.Y + (ButtonArea.Height / 2))), _
                        New PointF(ButtonArea.Width, ButtonArea.Y), _
                        New PointF(ButtonArea.Width, ButtonArea.Y + ButtonArea.Height)}
                    gp.AddPolygon(pts)

                Case eShape.TriangleRight
                    Dim pts() As PointF = New PointF() { _
                        New PointF(ButtonArea.X, ButtonArea.Y), _
                        New PointF(ButtonArea.Width, CSng(ButtonArea.Y + (ButtonArea.Height / 2))), _
                        New PointF(ButtonArea.X, ButtonArea.Y + ButtonArea.Height)}
                    gp.AddPolygon(pts)

            End Select

            Return gp
        End Function

        Private Function GetStringFormat(ByVal ctrlalign As ContentAlignment) As StringFormat
            Dim strFormat As StringFormat = New StringFormat()
            Select Case ctrlalign
                Case ContentAlignment.MiddleCenter
                    strFormat.LineAlignment = StringAlignment.Center
                    strFormat.Alignment = StringAlignment.Center
                Case ContentAlignment.MiddleLeft
                    strFormat.LineAlignment = StringAlignment.Center
                    strFormat.Alignment = StringAlignment.Near
                Case ContentAlignment.MiddleRight
                    strFormat.LineAlignment = StringAlignment.Center
                    strFormat.Alignment = StringAlignment.Far
                Case ContentAlignment.TopCenter
                    strFormat.LineAlignment = StringAlignment.Near
                    strFormat.Alignment = StringAlignment.Center
                Case ContentAlignment.TopLeft
                    strFormat.LineAlignment = StringAlignment.Near
                    strFormat.Alignment = StringAlignment.Near
                Case ContentAlignment.TopRight
                    strFormat.LineAlignment = StringAlignment.Near
                    strFormat.Alignment = StringAlignment.Far
                Case ContentAlignment.BottomCenter
                    strFormat.LineAlignment = StringAlignment.Far
                    strFormat.Alignment = StringAlignment.Center
                Case ContentAlignment.BottomLeft
                    strFormat.LineAlignment = StringAlignment.Far
                    strFormat.Alignment = StringAlignment.Near
                Case ContentAlignment.BottomRight
                    strFormat.LineAlignment = StringAlignment.Far
                    strFormat.Alignment = StringAlignment.Far
            End Select
            If _UseMnemonic = True Then
                strFormat.HotkeyPrefix = Drawing.Text.HotkeyPrefix.Show
            Else
                strFormat.HotkeyPrefix = Drawing.Text.HotkeyPrefix.None
            End If
            Return strFormat
        End Function

        Private Sub SetImageAndText(ByVal g As Graphics)
            If MouseDrawState = eMouseDrawState.Down Then
                PressedOffset = 1
            Else
                PressedOffset = 0
            End If

            If Not Image Is Nothing Then
                ImageSizeUse = ImageSize
            Else
                ImageSizeUse = New Size(0, 0)
            End If

            Select Case TextImageRelation
                Case System.Windows.Forms.TextImageRelation.Overlay, System.Windows.Forms.TextImageRelation.ImageAboveText, System.Windows.Forms.TextImageRelation.TextAboveImage
                    TextArea = AdjustRect(ButtonArea, TextMargin)
                    ImageArea = ButtonArea
                    TextArea.Y += PressedOffset
                    Imagept = ImageLocation(GetStringFormat(ImageAlign), ButtonArea.Size, ImageSizeUse)
                    Imagept.X += ButtonArea.X
                Case System.Windows.Forms.TextImageRelation.ImageBeforeText
                    Dim TextSize As SizeF = g.MeasureString(Text, Font)
                    TextArea = AdjustRect(ButtonArea, TextMargin)
                    TextArea.Width -= ImageSizeUse.Width - 4
                    TextArea.Y += PressedOffset
                    ImageArea = New RectangleF(TextArea.X - ImageSizeUse.Width, ButtonArea.Y, ImageSizeUse.Width, ImageSizeUse.Height)
                    Imagept = ImageLocation(GetStringFormat(ImageAlign), ButtonArea.Size, ImageArea.Size)

                    Select Case GetStringFormat(TextAlign).Alignment
                        Case StringAlignment.Center
                            Imagept.X = ButtonArea.X + ((ButtonArea.Width - TextSize.Width - ImageSizeUse.Width) / 2)
                            TextArea.X = ButtonArea.X + ImageSizeUse.Width
                        Case StringAlignment.Near
                            Imagept.X = ButtonArea.X + 4
                            TextArea.X = ButtonArea.X + ImageSizeUse.Width + 4
                        Case StringAlignment.Far
                            Imagept.X = ButtonArea.X + TextArea.Width - TextSize.Width - 12
                            TextArea.X = ButtonArea.X + ImageSizeUse.Width - 8
                    End Select

                Case System.Windows.Forms.TextImageRelation.TextBeforeImage
                    Dim TextSize As SizeF = g.MeasureString(Text, Font)
                    TextArea = AdjustRect(ButtonArea, TextMargin)
                    TextArea.Width -= ImageSizeUse.Width - 8
                    TextArea.Y += PressedOffset
                    ImageArea = New RectangleF(TextArea.X, ButtonArea.Y, ImageSizeUse.Width, ImageSizeUse.Height)
                    Imagept = ImageLocation(GetStringFormat(ImageAlign), ButtonArea.Size, ImageArea.Size)

                    Select Case GetStringFormat(TextAlign).Alignment
                        Case StringAlignment.Center
                            Imagept.X = ((TextArea.Width - TextSize.Width) / 2) + TextSize.Width
                            TextArea.X = -4
                        Case StringAlignment.Near
                            Imagept.X = TextSize.Width + 8
                            TextArea.X = 4
                        Case StringAlignment.Far
                            Imagept.X = TextArea.Width - 12
                            TextArea.X = -16
                    End Select

            End Select
            Imagept.Y += PressedOffset + ButtonArea.Y

        End Sub

        Private Shared Function ImageLocation(ByVal sf As StringFormat, ByVal Area As SizeF, ByVal ImageArea As SizeF) As PointF
            Dim pt As PointF
            Select Case sf.Alignment
                Case StringAlignment.Center
                    pt.X = CSng((Area.Width - ImageArea.Width) / 2)
                Case StringAlignment.Near
                    pt.X = 2
                Case StringAlignment.Far
                    pt.X = Area.Width - ImageArea.Width - 2

            End Select

            Select Case sf.LineAlignment
                Case StringAlignment.Center
                    pt.Y = CSng((Area.Height - ImageArea.Height) / 2)
                Case StringAlignment.Near
                    pt.Y = 2
                Case StringAlignment.Far
                    pt.Y = Area.Height - ImageArea.Height - 2

            End Select

            Return pt
        End Function

        Private Sub UpdateDimBlends()
            _HoverColorBlend = CType(_ColorFillBlend.iColor.Clone, Color())
            _ClickColorBlend = CType(_ColorFillBlend.iColor.Clone, Color())
            _DisabledBlend = CType(_ColorFillBlend.iColor.Clone, Color())
            Dim c As Color
            For i As Integer = 0 To _ColorFillBlend.iColor.Length - 1
                c = _ColorFillBlend.iColor(i)
                _HoverColorBlend(i) = DimTheColor(c, _DimFactorHover)
                _ClickColorBlend(i) = DimTheColor(c, _DimFactorClick)
                _DisabledBlend(i) = GrayTheColor(c)
            Next
        End Sub

        Private Sub UpdateDimColors()
            _HoverColorSolid = DimTheColor(_ColorFillSolid, _DimFactorHover)
            _ClickColorSolid = DimTheColor(_ColorFillSolid, _DimFactorClick)
            _DisabledSolid = GrayTheColor(_ColorFillSolid)

        End Sub

        ''' <summary>
        ''' This function takes the given color and Lightens or Darkens it by the given value 
        ''' </summary>
        ''' <param name="DimColor">Base Color object to be changed</param>
        ''' <param name="DimDegree">Positive value to darken and negative value to lighten DimColor</param>
        Public Shared Function DimTheColor(ByVal DimColor As Color, ByVal DimDegree As Integer) As Color
            If DimColor = Color.Transparent Or DimDegree = 0 Then Return DimColor
            Dim ColorR As Integer = DimColor.R + DimDegree
            Dim ColorG As Integer = DimColor.G + DimDegree
            Dim ColorB As Integer = DimColor.B + DimDegree

            If ColorR > 255 Then ColorR = 255
            If ColorG > 255 Then ColorG = 255
            If ColorB > 255 Then ColorB = 255
            If ColorR < 0 Then ColorR = 0
            If ColorG < 0 Then ColorG = 0
            If ColorB < 0 Then ColorB = 0

            Return Color.FromArgb(ColorR, ColorG, ColorB)

        End Function

        ''' <summary>
        ''' This function takes the given color and returns its gray equivilant
        ''' </summary>
        ''' <param name="GrayColor">Color object to be grayed</param>
        Public Shared Function GrayTheColor(ByVal GrayColor As Color) As Color
            Dim gray As Integer = CInt(GrayColor.R * 0.3 + GrayColor.G * 0.59 + GrayColor.B * 0.11)
            Return Color.FromArgb(GrayColor.A, gray, gray, gray)
        End Function

        Private Function GetFillBlend() As Color()
            If Enabled Then
                If MouseDrawState = eMouseDrawState.Over Then
                    Return _HoverColorBlend
                ElseIf MouseDrawState = eMouseDrawState.Down Then
                    Return _ClickColorBlend
                Else
                    Return _ColorFillBlend.iColor
                End If
            Else
                Return _DisabledBlend
            End If

        End Function

        Private Function GetFill() As Color

            If Enabled Then
                If MouseDrawState = eMouseDrawState.Over Then
                    Return _HoverColorSolid
                ElseIf MouseDrawState = eMouseDrawState.Down Then
                    Return _ClickColorSolid
                Else
                    Return _ColorFillSolid
                End If
            Else
                Return _DisabledSolid
            End If

        End Function

        Private Shared Function AdjustRect(ByVal BaseRect As RectangleF, ByVal Pad As Padding) As RectangleF
            BaseRect.Width -= Pad.Horizontal
            BaseRect.Height -= Pad.Vertical
            BaseRect.Offset(Pad.Left, Pad.Top)
            Return BaseRect
        End Function

        Private Shared Function AdjustRect(ByVal BaseRect As Rectangle, ByVal Pad As Padding) As Rectangle
            BaseRect.Width -= Pad.Horizontal
            BaseRect.Height -= Pad.Vertical
            BaseRect.Offset(Pad.Left, Pad.Top)
            Return BaseRect
        End Function

        Private Function GetRoundedRectPath(ByVal BaseRect As RectangleF) As GraphicsPath

            Dim ArcRect As RectangleF
            Dim MyPath As New Drawing2D.GraphicsPath()
            If Corners.All = -1 Then
                With MyPath
                    ' top left arc
                    If Corners.UpperLeft = 0 Then
                        .AddLine(BaseRect.X, BaseRect.Y, BaseRect.X, BaseRect.Y)
                    Else
                        ArcRect = New RectangleF(BaseRect.Location, _
                            New SizeF(Corners.UpperLeft * 2, Corners.UpperLeft * 2))
                        .AddArc(ArcRect, 180, 90)
                    End If

                    ' top right arc
                    If Corners.UpperRight = 0 Then
                        .AddLine(BaseRect.X + (Corners.UpperLeft), BaseRect.Y, BaseRect.Right - (Corners.UpperRight), BaseRect.Top)
                    Else
                        ArcRect = New RectangleF(BaseRect.Location, _
                            New SizeF(Corners.UpperRight * 2, Corners.UpperRight * 2))
                        ArcRect.X = BaseRect.Right - (Corners.UpperRight * 2)
                        .AddArc(ArcRect, 270, 90)
                    End If

                    ' bottom right arc
                    If Corners.LowerRight = 0 Then
                        .AddLine(BaseRect.Right, BaseRect.Top + (Corners.UpperRight), BaseRect.Right, BaseRect.Bottom - (Corners.LowerRight))
                    Else
                        ArcRect = New RectangleF(BaseRect.Location, _
                            New SizeF(Corners.LowerRight * 2, Corners.LowerRight * 2))
                        ArcRect.Y = BaseRect.Bottom - (Corners.LowerRight * 2)
                        ArcRect.X = BaseRect.Right - (Corners.LowerRight * 2)
                        .AddArc(ArcRect, 0, 90)
                    End If

                    ' bottom left arc
                    If Corners.LowerLeft = 0 Then
                        .AddLine(BaseRect.Right - (Corners.LowerRight), BaseRect.Bottom, BaseRect.X - (Corners.LowerLeft), BaseRect.Bottom)
                    Else
                        ArcRect = New RectangleF(BaseRect.Location, _
                            New SizeF(Corners.LowerLeft * 2, Corners.LowerLeft * 2))
                        ArcRect.Y = BaseRect.Bottom - (Corners.LowerLeft * 2)
                        .AddArc(ArcRect, 90, 90)
                    End If

                    .CloseFigure()
                End With
            Else
                With MyPath
                    If Corners.All = 0 Then
                        .AddRectangle(BaseRect)
                    Else

                        ArcRect = New RectangleF(BaseRect.Location, _
                            New SizeF(Corners.All * 2, Corners.All * 2))
                        ' top left arc
                        .AddArc(ArcRect, 180, 90)

                        ' top right arc
                        ArcRect.X = BaseRect.Right - (Corners.All * 2)
                        .AddArc(ArcRect, 270, 90)

                        ' bottom right arc
                        ArcRect.Y = BaseRect.Bottom - (Corners.All * 2)
                        .AddArc(ArcRect, 0, 90)

                        ' bottom left arc
                        ArcRect.X = BaseRect.Left
                        .AddArc(ArcRect, 90, 90)

                    End If
                    .CloseFigure()
                End With
            End If
            Return MyPath
        End Function

#End Region 'Drawing

#Region "MouseEvents"

        Private Sub eButton_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
            If Not GetPath.IsVisible(e.X, e.Y) AndAlso Not rectSideImage.Contains(e.Location) Then
                Call SendMessage(Parent.Handle, WM_LBUTTONDBLCLK, 0, IntPtr.Zero)
            End If
        End Sub

        Private Sub eButton_MouseDown(ByVal sender As Object, _
          ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
            Focus()
            If e.Clicks = 2 Then Exit Sub
            If DesignMode Then
                'Because of the GetHitTest Override in the
                'Designer Manual Selection is needed
                Dim selectservice As ISelectionService = _
                     CType(GetService(GetType(ISelectionService)), ISelectionService)
                Dim selection As New ArrayList
                selection.Clear()
                selectservice.SetSelectedComponents(selection, SelectionTypes.Replace)
                selection.Add(Me)
                selectservice.SetSelectedComponents(selection, SelectionTypes.Add)

                'FocusPoints Reset
                If e.Button = System.Windows.Forms.MouseButtons.Right Then
                    If CenterPtTracker.IsActive Then
                        FocalPoints = New cFocalPoints( _
                            New PointF(0.5, 0.5), _
                            FocalPoints.FocusScales)
                        Invalidate()
                    ElseIf FocusPtTracker.IsActive Then
                        FocalPoints = New cFocalPoints( _
                            FocalPoints.CenterPoint, _
                            New PointF(0, 0))
                        Invalidate()
                    End If
                End If
            Else
                If Enabled Then
                    If GetPath.IsVisible(e.X, e.Y) Then
                        MouseDrawState = eMouseDrawState.Down
                        Invalidate(Rectangle.Round(ButtonArea))

                    ElseIf _SideImageIsClickable AndAlso rectSideImage.Contains(e.Location) Then
                        RaiseEvent SideImageClicked(Me, e)

                    Else
                        'Allow click through the non button area
                        If e.Button = System.Windows.Forms.MouseButtons.Left Then
                            Call SendMessage(Parent.Handle, WM_LBUTTONDOWN, 0, IntPtr.Zero)
                        Else
                            Call SendMessage(Parent.Handle, WM_RBUTTONDOWN, 0, IntPtr.Zero)
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub eButton_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
            If DesignMode Then
                If e.Button = System.Windows.Forms.MouseButtons.Left Then
                    If CenterPtTracker.IsActive Then
                        FocalPoints = New cFocalPoints( _
                            New PointF(CSng(e.X / Width), CSng(e.Y / Height)), _
                            FocalPoints.FocusScales)
                        Invalidate()
                    ElseIf FocusPtTracker.IsActive Then
                        FocalPoints = New cFocalPoints( _
                            FocalPoints.CenterPoint, _
                            New PointF(CSng(e.X / Width), CSng(e.Y / Height)))
                        Invalidate()
                    End If
                End If
            Else
                If GetPath.IsVisible(e.X, e.Y) Then
                    If Not MouseDrawState = eMouseDrawState.Down Then
                        MouseDrawState = eMouseDrawState.Over
                    End If
                    Invalidate(Rectangle.Round(ButtonArea))
                Else
                    If Not MouseDrawState = eMouseDrawState.Up Then
                        MouseDrawState = eMouseDrawState.Up
                        Invalidate(Rectangle.Round(ButtonArea))

                    End If
                End If
            End If
        End Sub

        Private Sub eButton_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
            If Not MouseDrawState = eMouseDrawState.Up Then
                MouseDrawState = eMouseDrawState.Up
                Invalidate(Rectangle.Round(ButtonArea))
            End If
        End Sub

        Private Sub eButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
            If MouseDrawState = eMouseDrawState.Down Then PerformClickButtonArea(Me, e)
            MouseDrawState = eMouseDrawState.Up
            Invalidate(Rectangle.Round(ButtonArea))
        End Sub

#End Region 'MouseEvents

#Region "Focus"

        Private Sub eButton_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
            MouseDrawState = eMouseDrawState.Over
            Invalidate(Rectangle.Round(ButtonArea))
        End Sub

        Private Sub eButton_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
            MouseDrawState = eMouseDrawState.Up
            Invalidate(Rectangle.Round(ButtonArea))
        End Sub

#End Region

#Region "Key Events"

        Private Sub eButton_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            If Enabled Then
                Select Case e.KeyCode
                    Case Keys.Enter, Keys.Space, Keys.Return
                        MouseDrawState = eMouseDrawState.Down
                        Invalidate(Rectangle.Round(ButtonArea))
                End Select
            End If
        End Sub

        Private Sub eButton_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Space, Keys.Return
                    e.Handled = True
                    If MouseDrawState = eMouseDrawState.Down Then
                        MouseDrawState = eMouseDrawState.Over
                        Invalidate(Rectangle.Round(ButtonArea))
                        PerformClickButtonArea(Me, _
                            New MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, _
                                CInt(ButtonArea.X), CInt(ButtonArea.Y), 0))
                    End If
            End Select
        End Sub
#End Region

#Region "Control Events"

        Private Sub eButton_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
            If DesignMode Then
                CenterPtTracker.TrackerRectangle = CenterPtTrackerRectangle()
                FocusPtTracker.TrackerRectangle = FocusPtTrackerRectangle()
            End If
        End Sub

        Protected Overrides Function ProcessMnemonic(ByVal charCode As Char) As Boolean
            If (CanSelect And IsMnemonic(charCode, Text)) Then
                Me.Select()
                PerformClickButtonArea(Me, New MouseEventArgs( _
                   System.Windows.Forms.MouseButtons.Left, 1, _
                    CInt(ButtonArea.X), CInt(ButtonArea.Y), 0))
                Return True
            End If
            Return False
        End Function

#End Region 'Control Events

    End Class

#End Region 'eButton Class

End Namespace


#Region "DesignerRectTracker"

Public Class DesignerRectTracker

    Private _TrackerRectangle As RectangleF
    Public Property TrackerRectangle() As RectangleF
        Get
            Return _TrackerRectangle
        End Get
        Set(ByVal value As RectangleF)
            _TrackerRectangle = value
        End Set
    End Property

    Private _IsActive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property

    Sub New()
        _TrackerRectangle = New RectangleF(0, 0, 10, 10)
        _IsActive = False
    End Sub

End Class

#End Region 'DesignerRectTracker

#Region "BlendTypeEditor - UITypeEditor"

#Region "cBlendItems"

<System.Diagnostics.DebuggerStepThrough()> _
<TypeConverter(GetType(BlendItemsConverter))> _
Public Class cBlendItems

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
Friend Class BlendItemsConverter : Inherits ExpandableObjectConverter

    Public Overrides Function GetCreateInstanceSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function CreateInstance(ByVal context As ITypeDescriptorContext, ByVal propertyValues As System.Collections.IDictionary) As Object
        Dim bItem As New cBlendItems
        bItem.iColor = CType(propertyValues("iColor"), Color())
        bItem.iPoint = CType(propertyValues("iPoint"), Single())
        Return bItem
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
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

End Class

#End Region 'BlendItemsConverter

<System.Diagnostics.DebuggerStepThrough()> _
Public Class BlendTypeEditor
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
                Using dropDownEditor As eDropdownColorBlender = New eDropdownColorBlender(editorService)
                    ' Pass the UI editor the current property values
                    Dim Instance As UIControls.Button.eButton
                    If context.Instance.GetType Is GetType(UIControls.Button.eButton) Then
                        'For PropertyGrid
                        Instance = CType(context.Instance, UIControls.Button.eButton)
                    Else
                        'For SmartTag
                        Instance = CType(CType(context.Instance, eButtonActionList).CurrControl, UIControls.Button.eButton)
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
                        ' Set current Corners values
                        .TheSample.Shape = Instance.Shape
                        .TheSample.FillType = Instance.FillType
                        .TheSample.FillTypeLinear = Instance.FillTypeLinear
                        .TheSample.ColorFillSolid = Instance.ColorFillSolid
                        .TheSample.BorderColor = Instance.BorderColor
                        '.TheSample.FocalPoints = Instance.FocalPoints
                        '.TheSample.ColorFillBlend = Instance.ColorFillBlend
                        '.TheSample.Corners = _
                        '    New CornersProperty(CShort(Instance.Corners.LowerLeft * ratio), _
                        '    CShort(Instance.Corners.LowerRight * ratio), _
                        '    CShort(Instance.Corners.UpperLeft * ratio), _
                        '    CShort(Instance.Corners.UpperRight * ratio))
                        .LoadABlend(Instance.ColorFillBlend)
                        .TheSample.TextMargin = _
                            New Padding(CInt(Instance.TextMargin.Left * ratio), _
                            CInt(Instance.TextMargin.Top * ratio), _
                            CInt(Instance.TextMargin.Right * ratio), _
                            CInt(Instance.TextMargin.Bottom * ratio))
                        .TheSample.Padding = _
                            New Padding(CInt(Instance.Padding.Left * ratio), _
                            CInt(Instance.Padding.Top * ratio), _
                            CInt(Instance.Padding.Right * ratio), _
                            CInt(Instance.Padding.Bottom * ratio))
                        .TheSample.Text = Instance.Text
                        .TheSample.ForeColor = Instance.ForeColor
                        .TheSample.TextAlign = Instance.TextAlign
                        .TheSample.Font = _
                            New Font(Instance.Font.FontFamily, _
                            Instance.Font.Size * ratio, _
                            Instance.Font.Style)
                        .TheSample.TextShadow = Instance.TextShadow
                        .TheSample.TextShadowShow = Instance.TextShadowShow
                    End With
                    ' Display the UI editor
                    editorService.DropDownControl(dropDownEditor)
                    ' Return the new property value from the editor
                    Return dropDownEditor.TheSample.ColorFillBlend
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
        Using br As LinearGradientBrush = _
            New LinearGradientBrush(e.Bounds, Color.Black, Color.Black, _
            LinearGradientMode.Horizontal)
            Dim cb As New ColorBlend
            cb.Colors = cblnd.iColor
            cb.Positions = cblnd.iPoint
            br.InterpolationColors = cb
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using
    End Sub
End Class
#End Region 'BlendTypeEditor - UITypeEditor

#Region "Expandable Focal Points Property Classes"

#Region "cFocalPoints"

<TypeConverter(GetType(FocalPointsConverter))> _
Public Class cFocalPoints

    Private _CenterPtX As Single = 0.5
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0.5)> _
        Public Property CenterPtX() As Single
        Get
            Return _CenterPtX
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _CenterPtX = value
        End Set
    End Property

    Private _CenterPtY As Single = 0.5
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0.5)> _
    Public Property CenterPtY() As Single
        Get
            Return _CenterPtY
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _CenterPtY = value
        End Set
    End Property

    Private _FocusPtX As Single = 0
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property FocusPtX() As Single
        Get
            Return _FocusPtX
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _FocusPtX = value
        End Set
    End Property

    Private _FocusPtY As Single = 0
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property FocusPtY() As Single
        Get
            Return _FocusPtY
        End Get
        Set(ByVal value As Single)
            If value < 0 Then value = 0
            If value > 1 Then value = 1
            _FocusPtY = value
        End Set
    End Property

    Public Function CenterPoint() As PointF
        Return New PointF(CenterPtX, CenterPtY)
    End Function

    Public Function FocusScales() As PointF
        Return New PointF(FocusPtX, FocusPtY)
    End Function

    Sub New()
        CenterPtX = 0.5
        CenterPtY = 0.5
        FocusPtX = 0
        FocusPtY = 0
    End Sub

    Sub New(ByVal Cx As Single, ByVal Cy As Single, ByVal Fx As Single, ByVal Fy As Single)
        CenterPtX = Cx
        CenterPtY = Cy
        FocusPtX = Fx
        FocusPtY = Fy
    End Sub

    Sub New(ByVal ptC As PointF, ByVal ptF As PointF)
        CenterPtX = ptC.X
        CenterPtY = ptC.Y
        FocusPtX = ptF.X
        FocusPtY = ptF.Y
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}, {3}", _CenterPtX, _CenterPtY, _FocusPtX, _FocusPtY)
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim eObj As cFocalPoints = CType(obj, cFocalPoints)

        Return CenterPtX = eObj.CenterPtX _
            AndAlso CenterPtY = eObj.CenterPtY _
            AndAlso FocusPtX = eObj.FocusPtX _
            AndAlso FocusPtY = eObj.FocusPtY

    End Function
End Class

#End Region 'cFocalPoints

#Region "FocalPointsConverter"

Friend Class FocalPointsConverter : Inherits ExpandableObjectConverter

    Public Overrides Function GetCreateInstanceSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function CreateInstance(ByVal context As ITypeDescriptorContext, ByVal propertyValues As System.Collections.IDictionary) As Object
        Dim fPt As New cFocalPoints
        fPt.CenterPtX = CType(propertyValues("CenterPtX"), Single)
        fPt.CenterPtY = CType(propertyValues("CenterPtY"), Single)
        fPt.FocusPtX = CType(propertyValues("FocusPtX"), Single)
        fPt.FocusPtY = CType(propertyValues("FocusPtY"), Single)
        Return fPt
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CType(value, String)
                Dim FocalPointsParts(4) As String
                FocalPointsParts = Split(s, ",")
                If Not IsNothing(FocalPointsParts) Then
                    If IsNothing(FocalPointsParts(0)) Then FocalPointsParts(0) = "0.5"
                    If IsNothing(FocalPointsParts(1)) Then FocalPointsParts(1) = "0.5"
                    If IsNothing(FocalPointsParts(2)) Then FocalPointsParts(2) = "0"
                    If IsNothing(FocalPointsParts(3)) Then FocalPointsParts(3) = "0"
                    Return New cFocalPoints(CSng(FocalPointsParts(0).Trim), _
                                            CSng(FocalPointsParts(1).Trim), _
                                            CSng(FocalPointsParts(2).Trim), _
                                            CSng(FocalPointsParts(3).Trim))
                End If
            Catch ex As Exception
                Throw New ArgumentException(String.Format("Can not convert '{0}' to type Corners", CStr(value)))
            End Try
        Else
            Return New CornersProperty()
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, _
      ByVal value As Object, ByVal destinationType As System.Type) As Object

        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is cFocalPoints) Then
            Dim _FocalPoints As cFocalPoints = CType(value, cFocalPoints)

            ' build the string as "UpperLeft,UpperRight,LowerLeft,LowerRight" 
            Return String.Format("{0}, {1}, {2}, {3}", _FocalPoints.CenterPtX, _FocalPoints.CenterPtY, _FocalPoints.FocusPtX, _FocalPoints.FocusPtY)
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)

    End Function

End Class 'CornerConverter Code

#End Region 'FocalPointsConverter

#End Region 'Expandable Focal Points Property Class

#Region "Expandable Border Corners Property Class"

#Region "CornersProperty"

<TypeConverter(GetType(CornerConverter))> _
Public Class CornersProperty

    Private _All As Short = -1
    Private _UpperLeft As Short = 0
    Private _UpperRight As Short = 0
    Private _LowerLeft As Short = 0
    Private _LowerRight As Short = 0

    Public Sub New(ByVal LowerLeft As Short, ByVal LowerRight As Short, _
      ByVal UpperLeft As Short, ByVal UpperRight As Short)
        Me.LowerLeft = LowerLeft
        Me.LowerRight = LowerRight
        Me.UpperLeft = UpperLeft
        Me.UpperRight = UpperRight
    End Sub

    Public Sub New(ByVal All As Short)
        Me.All = All
    End Sub

    Public Sub New()
        LowerLeft = 0
        LowerRight = 0
        UpperLeft = 0
        UpperRight = 0
    End Sub

    Private Sub CheckForAll(ByVal val As Short)
        If val = LowerLeft AndAlso _
           val = LowerRight AndAlso _
           val = UpperLeft AndAlso _
           val = UpperRight Then
            If _All <> val Then _All = val
        Else
            If All <> -1 Then All = -1
        End If
    End Sub

    <DescriptionAttribute("Set the Radius of the All four Corners the same")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(-1)> _
    Public Property All() As Short
        Get
            Return _All
        End Get
        Set(ByVal Value As Short)
            _All = Value
            If Value > -1 Then
                _LowerLeft = Value
                _LowerRight = Value
                _UpperLeft = Value
                _UpperRight = Value
            End If
        End Set

    End Property

    <DescriptionAttribute("Set the Radius of the Upper Left Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property UpperLeft() As Short
        Get
            Return _UpperLeft
        End Get
        Set(ByVal Value As Short)
            _UpperLeft = Value

            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Upper Right Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property UpperRight() As Short
        Get
            Return _UpperRight
        End Get
        Set(ByVal Value As Short)
            _UpperRight = Value
            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Lower Left Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property LowerLeft() As Short
        Get
            Return _LowerLeft
        End Get
        Set(ByVal Value As Short)
            _LowerLeft = Value
            CheckForAll(Value)
        End Set
    End Property

    <DescriptionAttribute("Set the Radius of the Lower Right Corner")> _
    <RefreshProperties(RefreshProperties.Repaint)> _
    <NotifyParentProperty(True)> _
    <DefaultValue(0)> _
    Public Property LowerRight() As Short
        Get
            Return _LowerRight
        End Get
        Set(ByVal Value As Short)
            _LowerRight = Value
            CheckForAll(Value)
        End Set
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean

        Dim eObj As CornersProperty = CType(obj, CornersProperty)

        Return All = eObj.All _
            AndAlso LowerLeft = eObj.LowerLeft _
            AndAlso LowerRight = eObj.LowerRight _
            AndAlso UpperLeft = eObj.UpperLeft _
            AndAlso UpperRight = eObj.UpperRight

    End Function

End Class 'Corners Properties

#End Region 'CornersProperty

#Region "CornerConverter"

Friend Class CornerConverter : Inherits ExpandableObjectConverter

    Public Overrides Function GetCreateInstanceSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function CreateInstance(ByVal context As ITypeDescriptorContext, ByVal propertyValues As System.Collections.IDictionary) As Object
        Dim crn As New CornersProperty
        Dim AL As Short = CType(propertyValues("All"), Short)
        Dim LL As Short = CType(propertyValues("LowerLeft"), Short)
        Dim LR As Short = CType(propertyValues("LowerRight"), Short)
        Dim UL As Short = CType(propertyValues("UpperLeft"), Short)
        Dim UR As Short = CType(propertyValues("UpperRight"), Short)


        Dim oAll As Short = CType(CType(context.Instance, UIControls.Button.eButton).Corners, CornersProperty).All

        If oAll <> AL And AL > -1 Then
            crn.All = AL
        Else
            crn.LowerLeft = LL
            crn.LowerRight = LR
            crn.UpperLeft = UL
            crn.UpperRight = UR

        End If

        Return crn
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, _
      ByVal sourceType As System.Type) As Boolean

        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CType(value, String)
                Dim cornerParts(4) As String
                cornerParts = Split(s, ",")
                If Not IsNothing(cornerParts) Then
                    If IsNothing(cornerParts(0)) Then cornerParts(0) = "0"
                    If IsNothing(cornerParts(1)) Then cornerParts(1) = "0"
                    If IsNothing(cornerParts(2)) Then cornerParts(2) = "0"
                    If IsNothing(cornerParts(3)) Then cornerParts(3) = "0"
                    Return New CornersProperty(CShort(cornerParts(0).Trim), _
                                               CShort(cornerParts(1).Trim), _
                                               CShort(cornerParts(2).Trim), _
                                               CShort(cornerParts(3).Trim))
                End If
            Catch ex As Exception
                Throw New ArgumentException(String.Format("Can not convert '{0}' to type Corners", CStr(value)))
            End Try
        Else
            Return New CornersProperty()
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, _
      ByVal culture As System.Globalization.CultureInfo, _
      ByVal value As Object, ByVal destinationType As System.Type) As Object

        Dim _Corners As CornersProperty = CType(value, CornersProperty)
        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is CornersProperty) Then
            ' build the string as "UpperLeft, UpperRight, LowerLeft, LowerRight" 
            Return String.Format("{0}, {1}, {2}, {3}", _
                _Corners.LowerLeft, _
                _Corners.LowerRight, _
                _Corners.UpperLeft, _
                _Corners.UpperRight)
        Else
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End If

    End Function

End Class 'CornerConverter Code

#End Region 'CornerConverter

#End Region 'Expandable Border Corners Property Class

