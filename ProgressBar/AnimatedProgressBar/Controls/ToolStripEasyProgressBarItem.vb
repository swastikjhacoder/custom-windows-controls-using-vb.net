
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.ComponentModel

Namespace UIControls.ProgressBar
    ''' <summary>
    ''' Represents a custom progressbar control for all strip controls.
    ''' </summary>
    <ToolboxBitmap(GetType(System.Windows.Forms.ProgressBar))> _
    <ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)> _
    Public Class ToolStripEasyProgressBarItem
        Inherits ToolStripControlHost
        Implements IProgressBar
        'Implements ICloneable
#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the ToolStripEasyProgressBarItem class. 
        ''' </summary>
        Public Sub New()
            MyBase.New(CreateControlInstance())
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

        ''' <summary>
        ''' Gets the EasyProgressBar control that this ToolStripControlHost is hosting.
        ''' </summary>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public ReadOnly Property ProgressBar() As eEasyProgressBar
            Get
                Return TryCast(MyBase.Control, eEasyProgressBar)
            End Get
        End Property

#Region "Previous Hided Members"

        ''' <summary>
        ''' Gets or sets the text displayed on the <see cref="ToolStripEasyProgressBarItem"/>.
        ''' </summary>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property Text() As String
            Get
                Return Me.ProgressBar.Text
            End Get
            Set(ByVal value As String)
                Me.ProgressBar.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the foreground color of the hosted control.
        ''' </summary>
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Overrides Property ForeColor() As Color
            Get
                Return Me.ProgressBar.ForeColor
            End Get
            Set(ByVal value As Color)
                Me.ProgressBar.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the font to be used on the hosted control.
        ''' </summary>
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Overrides Property Font() As Font
            Get
                Return Me.ProgressBar.Font
            End Get
            Set(ByVal value As Font)
                Me.ProgressBar.Font = value
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property BackgroundImage() As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(ByVal value As Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property BackgroundImageLayout() As ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(ByVal value As ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        ''' <summary>
        ''' Gets the height and width of the ToolStripEasyProgressBarItem in pixels.
        ''' </summary>
        ''' <value>
        ''' Type: <see cref="System.Drawing.Size"/>
        ''' A Point value representing the height and width.
        ''' </value>
        Protected Overrides ReadOnly Property DefaultSize() As Size
            Get
                Return New Size(100, 20)
            End Get
        End Property

        ''' <summary>
        ''' Gets the spacing between the <see cref="ToolStripEasyProgressBarItem"/> and adjacent items.
        ''' </summary>
        Protected Overrides ReadOnly Property DefaultMargin() As Padding
            Get
                If (MyBase.Owner IsNot Nothing) AndAlso (TypeOf MyBase.Owner Is StatusStrip) Then
                    Return New Padding(3, 3, 3, 3)
                End If

                Return New Padding(1, 1, 1, 2)
            End Get
        End Property

#End Region

#Region "Helper Methods"

        Private Shared Function CreateControlInstance() As Control
            Return New eEasyProgressBar()
        End Function

        Private Sub OwnerRendererChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim toolsTripRenderer As ToolStripRenderer = MyBase.Owner.Renderer
            If toolsTripRenderer IsNot Nothing Then
                If TypeOf toolsTripRenderer Is ToolStripProfessionalRenderer Then
                    Dim renderer As ToolStripProfessionalRenderer = TryCast(toolsTripRenderer, ToolStripProfessionalRenderer)
                    If renderer IsNot Nothing Then
                        Me.BorderColor = renderer.ColorTable.ToolStripBorder
                    End If
                    If MyBase.Owner.[GetType]() IsNot GetType(StatusStrip) Then
                        Me.Margin = New Padding(1, 1, 1, 3)
                    End If
                Else
                    Me.Margin = DefaultMargin
                End If
            End If
        End Sub

#End Region

#Region "Virtual Methods"

        Protected Overridable Sub OnValueChanged()
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End Sub

#End Region

#Region "Override Methods"

        ''' <summary>
        ''' Raises the OwnerChanged event. 
        ''' </summary>
        ''' <param name="e">An EventArgs that contains the event data.</param>
        Protected Overrides Sub OnOwnerChanged(ByVal e As EventArgs)
            MyBase.OnOwnerChanged(e)

            If MyBase.Owner IsNot Nothing Then
                AddHandler MyBase.Owner.RendererChanged, AddressOf OwnerRendererChanged
            End If
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
                Return Me.ProgressBar.ProgressGradient
            End Get
            Set(ByVal value As GradientProgress)
                Me.ProgressBar.ProgressGradient = value
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
                Return Me.ProgressBar.BackgroundGradient
            End Get
            Set(ByVal value As GradientBackground)
                Me.ProgressBar.BackgroundGradient = value
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
                Return Me.ProgressBar.ProgressColorizer
            End Get
            Set(ByVal value As ColorizerProgress)
                Me.ProgressBar.ProgressColorizer = value
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
                Return Me.ProgressBar.DigitBoxGradient
            End Get
            Set(ByVal value As GradientDigitBox)
                Me.ProgressBar.DigitBoxGradient = value
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
                Return Me.ProgressBar.Value
            End Get
            Set(ByVal value As Integer)
                Me.ProgressBar.Value = value
                OnValueChanged()
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
                Return Me.ProgressBar.Minimum
            End Get
            Set(ByVal value As Integer)
                Me.ProgressBar.Minimum = value
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
                Return Me.ProgressBar.Maximum
            End Get
            Set(ByVal value As Integer)
                Me.ProgressBar.Maximum = value
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the control's border is draw or not.
        ''' </summary>
        <Description("Determines whether the control's border is draw or not")> _
        <DefaultValue(True)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property IsPaintBorder() As Boolean Implements IProgressBar.IsPaintBorder
            Get
                Return Me.ProgressBar.IsPaintBorder
            End Get
            Set(ByVal value As Boolean)
                Me.ProgressBar.IsPaintBorder = value
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the digital number drawing is enabled or not.
        ''' </summary>
        <Description("Determines whether the digital number drawing is enabled or not")> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property IsDigitDrawEnabled() As Boolean Implements IProgressBar.IsDigitDrawEnabled
            Get
                Return Me.ProgressBar.IsDigitDrawEnabled
            End Get
            Set(ByVal value As Boolean)
                Me.ProgressBar.IsDigitDrawEnabled = value
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the percentage text is show or hide.
        ''' </summary>
        <Description("Determines whether the percentage text is show or hide")> _
        <DefaultValue(True)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property ShowPercentage() As Boolean Implements IProgressBar.ShowPercentage
            Get
                Return Me.ProgressBar.ShowPercentage
            End Get
            Set(ByVal value As Boolean)
                Me.ProgressBar.ShowPercentage = value
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
                Return Me.ProgressBar.DisplayFormat
            End Get
            Set(ByVal value As String)
                Me.ProgressBar.DisplayFormat = value
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
                Return Me.ProgressBar.BorderColor
            End Get
            Set(ByVal value As Color)
                Me.ProgressBar.BorderColor = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the current border style of the ProgressBar control.
        ''' </summary>
        <Description("Gets or Sets, the current border style of the ProgressBar control")> _
        <DefaultValue(GetType(EasyProgressBarBorderStyle), "Flat")> _
        <Browsable(True)> _
        <Category("Appearance")> _
        <IsCloneable(True)> _
        Public Property ControlBorderStyle() As EasyProgressBarBorderStyle Implements IProgressBar.ControlBorderStyle
            Get
                Return Me.ProgressBar.ControlBorderStyle
            End Get
            Set(ByVal value As EasyProgressBarBorderStyle)
                Me.ProgressBar.ControlBorderStyle = value
            End Set
        End Property

        ''' <summary>
        ''' Occurs when the progress value changed of the control.
        ''' </summary>
        <Description("Occurs when the progress value changed of the control")> _
        Public Event ValueChanged As EventHandler Implements IProgressBar.ValueChanged

#End Region

#Region "ICloneable Members"

        Public Function Clone() As Object
            Dim toolStripItem As ToolStripEasyProgressBarItem = TryCast(CustomControlsLogic.GetMyClone(Me), ToolStripEasyProgressBarItem)
            Return toolStripItem
        End Function

#End Region
    End Class
End Namespace