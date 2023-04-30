
Imports System.ComponentModel
Imports System.Drawing
Imports System.Security.Permissions
Imports System.Windows.Forms

Namespace UIControls.TextBox
    <Designer(GetType(Design.SearchTextBoxDesigner))> _
    <DefaultEvent("TextChanged")> _
    <DefaultProperty("Text")> _
    <ToolboxBitmap(GetType(eSearchTextBox), "Resources.searchtextbox.bmp")> _
    Partial Public Class eSearchTextBox
        Inherits Control
        Private Const DefaultInactiveText As String = "Search"

        Private _active As Boolean

        Private _hoverButtonColor As Color
        Private _activeBackColor As Color
        Private _activeForeColor As Color
        Private _inactiveBackColor As Color
        Private _inactiveForeColor As Color

        Private _inactiveFont As Font

        Private _inactiveText As String

#Region "Events"

        Public Shadows Custom Event TextChanged As EventHandler
            AddHandler(ByVal value As EventHandler)
                'searchText.TextChanged += value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                'searchText.TextChanged -= value
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)

            End RaiseEvent
        End Event

#End Region

#Region "Properties"

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "GradientInactiveCaption")> _
        Public Property HoverButtonColor() As Color
            Get
                Return _hoverButtonColor
            End Get
            Set(ByVal value As Color)
                _hoverButtonColor = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "WindowText")> _
        Public Property ActiveForeColor() As Color
            Get
                Return _activeForeColor
            End Get
            Set(ByVal value As Color)
                _activeForeColor = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "Window")> _
        Public Property ActiveBackColor() As Color
            Get
                Return _activeBackColor
            End Get
            Set(ByVal value As Color)
                _activeBackColor = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "GrayText")> _
        Public Property InactiveForeColor() As Color
            Get
                Return _inactiveForeColor
            End Get
            Set(ByVal value As Color)
                _inactiveForeColor = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "Window")> _
        Public Property InactiveBackColor() As Color
            Get
                Return _inactiveBackColor
            End Get
            Set(ByVal value As Color)
                _inactiveBackColor = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Cursor), "IBeam")> _
        Public Overrides Property Cursor() As Cursor
            Get
                Return MyBase.Cursor
            End Get
            Set(ByVal value As Cursor)
                MyBase.Cursor = value
            End Set
        End Property

        <Browsable(False)> _
        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        <Browsable(False)> _
        Public Overrides Property BackColor() As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As Color)
                MyBase.BackColor = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(DefaultInactiveText)> _
        Public Property InactiveText() As String
            Get
                Return _inactiveText
            End Get
            Set(ByVal value As String)
                _inactiveText = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Font), "Verdana, 8.25pt")> _
        Public Property ActiveFont() As Font
            Get
                Return MyBase.Font
            End Get
            Set(ByVal value As Font)
                MyBase.Font = value
            End Set
        End Property

        <Category("Appearance")> _
        <DefaultValue(GetType(Font), "Verdana, 8.25pt, style=Bold, Normal")> _
        Public Property InactiveFont() As Font
            Get
                Return _inactiveFont
            End Get
            Set(ByVal value As Font)
                _inactiveFont = value
            End Set
        End Property

        <Browsable(False)> _
        Public Overrides Property Font() As Font
            Get
                Return MyBase.Font
            End Get
            Set(ByVal value As Font)
                MyBase.Font = value
            End Set
        End Property

        <Category("Appearance")> _
        Public Overrides Property Text() As String
            Get
                Return searchText.Text
            End Get
            Set(ByVal value As String)
                searchText.Text = value
            End Set
        End Property

        Protected ReadOnly Property TextEntered() As Boolean
            Get
                Return Not [String].IsNullOrEmpty(searchText.Text)
            End Get
        End Property

#End Region

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)> _
            Get
                Dim createParams__1 As CreateParams = MyBase.CreateParams
                createParams__1.ExStyle = createParams__1.ExStyle Or NativeConstants.WS_EX_CONTROLPARENT
                createParams__1.ExStyle = createParams__1.ExStyle And Not NativeConstants.WS_EX_CLIENTEDGE

                createParams__1.Style = createParams__1.Style Or NativeConstants.WS_BORDER

                Return createParams__1
            End Get
        End Property


        Public Sub New()
            _hoverButtonColor = SystemColors.GradientInactiveCaption
            _activeBackColor = SystemColors.Window
            _activeForeColor = SystemColors.WindowText
            _inactiveBackColor = SystemColors.Window
            _inactiveForeColor = SystemColors.GrayText

            _inactiveFont = New Font(Me.Font, FontStyle.Italic Or FontStyle.Bold)

            _inactiveText = DefaultInactiveText

            InitializeComponent()

            BackColor = InactiveBackColor
            ForeColor = InactiveForeColor

            searchOverlayLabel.Font = InactiveFont
            searchOverlayLabel.ForeColor = InactiveForeColor
            searchOverlayLabel.BackColor = InactiveBackColor
            searchOverlayLabel.Text = InactiveText

            searchText.Font = Font
            searchText.ForeColor = ActiveForeColor
            searchText.BackColor = InactiveBackColor

            _active = False

            SetTextActive(False)
            SetActive(False)
        End Sub

#Region "Methods"

        Private Sub SetActive(ByVal value As Boolean)
            If TextEntered Then
                value = True
            End If

            If _active = value Then
                Return
            End If

            Me.BackColor = If(value, ActiveBackColor, InactiveBackColor)
            Me.ForeColor = If(value, ActiveForeColor, InactiveForeColor)

            _active = value
        End Sub

        Private Sub SetTextActive(ByVal value As Boolean)
            Dim active As Boolean = value OrElse TextEntered

            Me.searchOverlayLabel.Visible = Not active
            Me.searchText.Visible = active

            If value AndAlso Not searchText.Focused Then
                Me.searchText.[Select]()
            End If
        End Sub

#End Region

#Region "Event Methods"

        Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
            SetTextActive(True)
            SetActive(True)

            MyBase.OnGotFocus(e)
        End Sub

        Protected Overrides Sub OnLostFocus(ByVal e As EventArgs)
            If Me.searchText.Focused Then
                Return
            End If

            SetTextActive(False)
            SetActive(False)

            MyBase.OnLostFocus(e)
        End Sub

        Protected Overrides Sub OnClick(ByVal e As EventArgs)
            Me.[Select]()

            MyBase.OnClick(e)
        End Sub

        Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
            Me.searchText.ForeColor = Me.ForeColor

            MyBase.OnForeColorChanged(e)
        End Sub

        Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
            Me.searchOverlayLabel.BackColor = Me.BackColor
            Me.searchText.BackColor = Me.BackColor

            MyBase.OnBackColorChanged(e)
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            searchImage.Image = If(TextEntered, Global.ESAR_Controls.My.Resources.active_search, Global.ESAR_Controls.My.Resources.inactive_search)

            MyBase.OnTextChanged(e)
        End Sub

        Private Sub searchImage_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            If e.X < 0 OrElse e.X > searchImage.Width OrElse e.Y < 0 OrElse e.Y > searchImage.Height Then
                NativeMethods.StopMouseCapture()
                searchImage.BackColor = Color.Empty
            Else
                NativeMethods.StartMouseCapture(searchImage.Handle)

                If TextEntered Then
                    searchImage.BackColor = HoverButtonColor
                End If
            End If
        End Sub

        Private Sub searchImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If TextEntered Then
                Me.searchText.ResetText()
                OnLostFocus(EventArgs.Empty)
            End If
        End Sub

        Private Sub searchText_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnTextChanged(e)
        End Sub

        Private Sub searchText_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
            OnLostFocus(e)
        End Sub

        Private Sub searchText_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
            OnGotFocus(e)
        End Sub

#End Region
    End Class
End Namespace