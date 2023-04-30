
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports ESAR_Controls.UIControls.ProgressBar.Components

Namespace UIControls.ProgressBar
    ''' <summary>
    ''' Represents a custom animated progressbar control for all strip controls.
    ''' </summary>
    <ToolboxBitmap(GetType(System.Windows.Forms.ProgressBar))> _
    <ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)> _
    Public Class ToolStripAnimatedEasyProgressBarItem
        Inherits ToolStripControlHost
        'Implements ICloneable
#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the ToolStripAnimatedEasyProgressBarItem class. 
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
        ''' Gets the AnimatedEasyProgressControl that this ToolStripControlHost is hosting.
        ''' </summary>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public ReadOnly Property AnimatedProgressBar() As eAnimatedEasyProgressControl
            Get
                Return TryCast(MyBase.Control, eAnimatedEasyProgressControl)
            End Get
        End Property

        ''' <summary>
        ''' Gets or Sets the task manager for painting this control.
        ''' </summary>
        <Description("Gets or Sets the task manager for painting this control")> _
        Public Property EasyProgressTaskManager() As eAniEasyProgressTaskManager
            Get
                Return Me.AnimatedProgressBar.EasyProgressTaskManager
            End Get
            Set(ByVal value As eAniEasyProgressTaskManager)
                Me.AnimatedProgressBar.EasyProgressTaskManager = value
            End Set
        End Property

#Region "Previous Hided Members"

        ''' <summary>
        ''' Gets or sets the text displayed on the <see cref="ToolStripAnimatedEasyProgressBarItem"/>.
        ''' </summary>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property Text() As String
            Get
                Return Me.AnimatedProgressBar.Text
            End Get
            Set(ByVal value As String)
                Me.AnimatedProgressBar.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the foreground color of the hosted control.
        ''' </summary>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property ForeColor() As Color
            Get
                Return Me.AnimatedProgressBar.ForeColor
            End Get
            Set(ByVal value As Color)
                Me.AnimatedProgressBar.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the font to be used on the hosted control.
        ''' </summary>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property Font() As Font
            Get
                Return Me.AnimatedProgressBar.Font
            End Get
            Set(ByVal value As Font)
                Me.AnimatedProgressBar.Font = value
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
        ''' Gets the height and width of the ToolStripAnimatedEasyProgressBarItem in pixels.
        ''' </summary>
        ''' <value>
        ''' Type: <see cref="System.Drawing.Size"/>
        ''' A Point value representing the height and width.
        ''' </value>
        Protected Overrides ReadOnly Property DefaultSize() As Size
            Get
                Return New Size(32, 32)
            End Get
        End Property

        ''' <summary>
        ''' Gets the spacing between the <see cref="ToolStripAnimatedEasyProgressBarItem"/> and adjacent items.
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
            Return New eAnimatedEasyProgressControl()
        End Function

        Private Sub OwnerRendererChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim toolsTripRenderer As ToolStripRenderer = MyBase.Owner.Renderer
            If toolsTripRenderer IsNot Nothing Then
                If TypeOf toolsTripRenderer Is ToolStripProfessionalRenderer Then
                    If MyBase.Owner.[GetType]() IsNot GetType(StatusStrip) Then
                        Me.Margin = New Padding(1, 1, 1, 3)
                    End If
                Else
                    Me.Margin = DefaultMargin
                End If
            End If
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

#Region "ICloneable Members"

        Public Function Clone() As Object
            Dim toolStripItem As ToolStripAnimatedEasyProgressBarItem = TryCast(CustomControlsLogic.GetMyClone(Me), ToolStripAnimatedEasyProgressBarItem)
            Return toolStripItem
        End Function

#End Region
    End Class
End Namespace