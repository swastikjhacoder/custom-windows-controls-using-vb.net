
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel
Imports ESAR_Controls.UIControls.TabControl.Design

Namespace UIControls.TabControl
    <Designer(GetType(eTabStripItemDesigner))> _
    <ToolboxItem(False)> _
    <DefaultProperty("Title")> _
    <DefaultEvent("Changed")> _
    Public Class eTabStripItem
        Inherits Windows.Forms.Panel
#Region "Events"

        Public Event Changed As EventHandler

#End Region

#Region "Fields"

        'private DrawItemState drawState = DrawItemState.None;
        Private m_stripRect As RectangleF = Rectangle.Empty
        Private m_image As Image = Nothing
        Private m_canClose As Boolean = True
        Private m_selected As Boolean = False
        Private m_visible As Boolean = True
        Private m_isDrawn As Boolean = False
        Private m_title As String = String.Empty

#End Region

#Region "Props"

        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property Size() As Size
            Get
                Return MyBase.Size
            End Get
            Set(ByVal value As Size)
                MyBase.Size = value
            End Set
        End Property

        <DefaultValue(True)> _
        Public Shadows Property Visible() As Boolean
            Get
                Return m_visible
            End Get
            Set(ByVal value As Boolean)
                If m_visible = value Then
                    Return
                End If

                m_visible = value
                OnChanged()
            End Set
        End Property

        Friend Property StripRect() As RectangleF
            Get
                Return m_stripRect
            End Get
            Set(ByVal value As RectangleF)
                m_stripRect = value
            End Set
        End Property

        <Browsable(False)> _
        <DefaultValue(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Property IsDrawn() As Boolean
            Get
                Return m_isDrawn
            End Get
            Set(ByVal value As Boolean)
                If m_isDrawn = value Then
                    Return
                End If

                m_isDrawn = value
            End Set
        End Property

        Public Property Image() As Image
            Get
                Return m_image
            End Get
            Set(ByVal value As Image)
                m_image = value
            End Set
        End Property

        <DefaultValue(True)> _
        Public Property CanClose() As Boolean
            Get
                Return m_canClose
            End Get
            Set(ByVal value As Boolean)
                m_canClose = value
            End Set
        End Property

        <DefaultValue("Name")> _
        Public Property Title() As String
            Get
                Return m_title
            End Get
            Set(ByVal value As String)
                If m_title = value Then
                    Return
                End If

                m_title = value
                OnChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets and sets a value indicating if the page is selected.
        ''' </summary>
        <DefaultValue(False)> _
        <Browsable(False)> _
        Public Property Selected() As Boolean
            Get
                Return m_selected
            End Get
            Set(ByVal value As Boolean)
                If m_selected = value Then
                    Return
                End If

                m_selected = value
            End Set
        End Property

        <Browsable(False)> _
        Public ReadOnly Property Caption() As String
            Get
                Return Title
            End Get
        End Property

#End Region

#Region "Ctor"

        Public Sub New()
            Me.New(String.Empty, Nothing)
        End Sub

        Public Sub New(ByVal displayControl As Control)
            Me.New(String.Empty, displayControl)
        End Sub

        Public Sub New(ByVal caption As String, ByVal displayControl As Control)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.ContainerControl, True)

            m_selected = False
            Visible = True

            UpdateText(caption, displayControl)

            'Add to controls
            If displayControl IsNot Nothing Then
                Controls.Add(displayControl)
            End If
        End Sub

#End Region

#Region "IDisposable"

        ''' <summary>
        ''' Handles proper disposition of the tab page control.
        ''' </summary>
        ''' <param name="disposing"></param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)

            If disposing Then
                If m_image IsNot Nothing Then
                    m_image.Dispose()
                End If
            End If
        End Sub

#End Region

#Region "ShouldSerialize"

        Public Function ShouldSerializeIsDrawn() As Boolean
            Return False
        End Function

        Public Function ShouldSerializeDock() As Boolean
            Return False
        End Function

        Public Function ShouldSerializeControls() As Boolean
            Return Controls IsNot Nothing AndAlso Controls.Count > 0
        End Function

        Public Function ShouldSerializeVisible() As Boolean
            Return True
        End Function

#End Region

#Region "Methods"

        Private Sub UpdateText(ByVal caption As String, ByVal displayControl As Control)
            If displayControl IsNot Nothing AndAlso TypeOf displayControl Is ICaptionSupport Then
                Dim capControl As ICaptionSupport = TryCast(displayControl, ICaptionSupport)
                Title = capControl.Caption
            ElseIf caption.Length <= 0 AndAlso displayControl IsNot Nothing Then
                Title = displayControl.Text
            ElseIf caption IsNot Nothing Then
                Title = caption
            Else
                Title = String.Empty
            End If
        End Sub

        Public Sub Assign(ByVal item As eTabStripItem)
            Visible = item.Visible
            Text = item.Text
            CanClose = item.CanClose
            Tag = item.Tag
        End Sub

        Protected Friend Overridable Sub OnChanged()
            RaiseEvent Changed(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Return a string representation of page.
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            Return Caption
        End Function

#End Region
    End Class
End Namespace