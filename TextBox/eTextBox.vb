Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Resources
Imports System.Drawing

Namespace UIControls.TextBox
    <ToolboxBitmap(GetType(eTextBox), "Resources.textbox.bmp")> _
    Public Class eTextBox
        Inherits System.Windows.Forms.TextBox

#Region "Fields"
        Private firstTime As Boolean = True
        Private firstLoad As Boolean = True
        Private secondLoad As Boolean = False
        Private thirdLoad As Boolean = False
        Private lastText As String = ""
        Private keyType As String
        Private isRequired As Byte = 0
        Private vexit As Boolean = False
        Private WithEvents miForm As Form = MyBase.FindForm
        Private LETTERS As String = "AÁBCDEÉFGHIÍJKLMNÑOÓPQRSTUÚÜVWXYZ"
        Private NUMBERS As String = "1234567890"
        Protected _waterMarkText As String = "<Default>"
        Protected _waterMarkColor As Color
        Protected _waterMarkActiveColor As Color
        Private waterMarkContainer As Windows.Forms.Panel
        Private m_waterMarkFont As Font
        Private waterMarkBrush As SolidBrush
        Private llLinkLabel As LinkLabel
        Private ltLinkType As LinkTypes = LinkTypes.None
        Private bLinkClicked As Boolean
#End Region

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()
            InitializeComponent()
            _waterMarkColor = Color.LightGray
            _waterMarkActiveColor = Color.Gray
            m_waterMarkFont = Me.Font
            waterMarkBrush = New SolidBrush(_waterMarkActiveColor)
            waterMarkContainer = Nothing
            DrawWaterMark()
            AddHandler Me.Enter, New EventHandler(AddressOf ThisHasFocus)
            AddHandler Me.Leave, New EventHandler(AddressOf ThisWasLeaved)
            AddHandler Me.TextChanged, New EventHandler(AddressOf ThisTextChanged)
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private components As System.ComponentModel.IContainer
        Friend WithEvents erpShowError As System.Windows.Forms.ErrorProvider
        Friend WithEvents erpShowRequired As System.Windows.Forms.ErrorProvider
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(eTextBox))
            Me.erpShowError = New System.Windows.Forms.ErrorProvider
            Me.erpShowRequired = New System.Windows.Forms.ErrorProvider
            Me.erpShowError.Icon = CType(resources.GetObject("erpShowError.Icon"), System.Drawing.Icon)
            llLinkLabel = New LinkLabel()
            Me.Controls.Add(llLinkLabel)
            llLinkLabel.AutoSize = True
            llLinkLabel.Left = -1
            llLinkLabel.Top = 1
            AddHandler llLinkLabel.LinkClicked, AddressOf ll_LinkClicked
            llLinkLabel.Visible = True
            llLinkLabel.Text = Me.Text
            AddHandler llLinkLabel.GotFocus, AddressOf llLinkLabel_GotFocus
            AddHandler llLinkLabel.MouseDown, AddressOf llLinkLabel_MouseDown
        End Sub

#End Region

#Region " Attributes "

#Region " Fields "

        Private m_ShowErrorIcon As Boolean
        Private m_TextBox As String
        Private m_Required As Boolean
        Private m_ValidationMode As ValidationModes

#End Region

#Region " Properties "

        <Description("Sets if error icon is showed."), Category("Validation")> _
        Public Property ShowErrorIcon() As Boolean
            Get
                Return m_ShowErrorIcon
            End Get
            Set(ByVal Value As Boolean)
                m_ShowErrorIcon = Value
            End Set
        End Property

        <Description("TextBox to use in validation."), Category("Validation")> _
        Public Property TextBox() As String
            Get
                Return m_TextBox
            End Get
            Set(ByVal Value As String)
                m_TextBox = Value
            End Set
        End Property

        <Description("Sets the field as required for validation."), Category("Validation")> _
        Public Property Required() As Boolean
            Get
                Return m_Required
            End Get
            Set(ByVal Value As Boolean)
                m_Required = Value
                If m_Required = True Then
                    Me.CausesValidation = True
                    Me.BackColor = System.Drawing.Color.White
                Else
                    Me.BackColor = System.Drawing.Color.White
                End If
            End Set
        End Property

        Enum ValidationModes
            None = 0
            ValidCharacters = 1
            InvalidCharacters = 2
            Letters = 3
            Numbers = 4
        End Enum

        <Description("Sets the validation mode to use."), Category("Validation"), RefreshProperties(RefreshProperties.All)> _
        Public Property ValidationMode() As ValidationModes
            Get
                Return m_ValidationMode
            End Get

            Set(ByVal Value As ValidationModes)
                m_ValidationMode = Value

                If m_ValidationMode = ValidationModes.None Then
                    Me.m_TextBox = ""
                End If

            End Set
        End Property

        <DefaultValue(LinkTypes.None)> _
        Public Property LinkType() As LinkTypes
            Get
                Return Me.ltLinkType
            End Get
            Set(ByVal value As LinkTypes)
                Me.ltLinkType = value
                If value = LinkTypes.None Then
                    SwitchToEditMode(True)
                Else
                    SwitchToEditMode(False)
                    FillLinkData()
                End If
            End Set
        End Property

#End Region

#End Region

#Region " Methods "
        Private Sub eventTextChanged_TextBox(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.TextChanged
            If vexit = True Then
                vexit = False
                Exit Sub
            End If
            If secondLoad = True Then
                Me.lastText = Me.Text
                secondLoad = False
                thirdLoad = True
            End If
            If firstLoad = True Then
                Me.secondLoad = True
                firstLoad = False
            End If
            Me.erpShowRequired.SetError(Me, "")
            If Me.m_ValidationMode = ValidationModes.None Then
                Me.erpShowError.SetError(Me, "")
            End If

            Dim i As Integer
            Dim j As Integer
            Dim check As Boolean = False
            Dim valid As Boolean = True
            Dim InvalidCharacters As New Collection
            Select Case m_ValidationMode
                Case ValidationModes.ValidCharacters
                    If firstTime = True Then
                        Dim validString As String = Me.m_TextBox
                        check = False
                        If Not Me.Text = "" And Not validString = "" Then
                            firstTime = False
                            Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                            For i = 0 To Me.Text.Length - 1
                                For j = 0 To validString.Length - 1
                                    If Me.Text.Chars(i) = validString.Chars(j) Then
                                        check = True
                                    End If
                                Next
                                If j = validString.Length And check = False Then
                                    valid = False
                                    InvalidCharacters.Add(Me.Text.Chars(i))
                                End If
                                check = False
                            Next
                            If valid = False Then
                                If Me.m_ShowErrorIcon = True Then
                                    showError(InvalidCharacters)
                                End If
                                Me.Text = lastText
                                Me.SelectionStart = Me.Text.Length
                            Else
                                Me.erpShowError.SetError(Me, "")
                            End If
                            firstTime = True
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        lastText = Me.Text
                    End If
                Case ValidationModes.InvalidCharacters
                    If firstTime = True Then
                        Dim validString As String = Me.m_TextBox
                        check = False
                        If Not Me.Text = "" And Not validString = "" Then
                            firstTime = False
                            Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                            For i = 0 To Me.Text.Length - 1
                                For j = 0 To validString.Length - 1
                                    If Me.Text.Chars(i) = validString.Chars(j) Then
                                        check = True
                                    End If
                                Next
                                If j = validString.Length And check = True Then
                                    valid = False
                                    InvalidCharacters.Add(Me.Text.Chars(i))
                                End If
                                check = False
                            Next
                            If valid = False Then
                                If Me.m_ShowErrorIcon = True Then
                                    showError(InvalidCharacters)
                                End If
                                Me.Text = lastText
                                Me.SelectionStart = Me.Text.Length
                            Else
                                Me.erpShowError.SetError(Me, "")
                            End If
                            firstTime = True
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        lastText = Me.Text
                    End If
                Case ValidationModes.Letters
                    If firstTime = True Then
                        Dim validString As String = "AÁBCDEÉFGHIÍJKLMNÑOÓPQRSTUÚÜVWXYZaábcdeéfghiíjklmnñoópqrstuúüvwxyz ,;.'"
                        check = False
                        If Not Me.Text = "" Then
                            firstTime = False
                            Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                            For i = 0 To Me.Text.Length - 1
                                For j = 0 To validString.Length - 1
                                    If Me.Text.Chars(i) = validString.Chars(j) Then
                                        check = True
                                    End If
                                Next
                                If j = validString.Length And check = False Then
                                    valid = False
                                    InvalidCharacters.Add(Me.Text.Chars(i))
                                End If
                                check = False
                            Next
                            If valid = False Then
                                If Me.m_ShowErrorIcon = True Then
                                    showError(InvalidCharacters)
                                End If
                                Me.Text = lastText
                                Me.SelectionStart = Me.Text.Length
                            Else
                                Me.erpShowError.SetError(Me, "")
                            End If
                            firstTime = True
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        lastText = Me.Text
                    End If
                Case ValidationModes.Numbers
                    If firstTime = True Then
                        Dim validString As String = "1234567890."
                        check = False
                        If Not Me.Text = "" Then
                            firstTime = False
                            Dim lastCharacter As String = Mid(Me.Text, Me.Text.Length, 1)
                            For i = 0 To Me.Text.Length - 1
                                For j = 0 To validString.Length - 1
                                    If Me.Text.Chars(i) = validString.Chars(j) Then
                                        check = True
                                    End If
                                Next
                                If j = validString.Length And check = False Then
                                    valid = False
                                    InvalidCharacters.Add(Me.Text.Chars(i))
                                End If
                                check = False
                            Next
                            If valid = False Then
                                If Me.m_ShowErrorIcon = True Then
                                    showError(InvalidCharacters)
                                End If
                                Me.Text = lastText
                                Me.SelectionStart = Me.Text.Length
                            Else
                                Me.erpShowError.SetError(Me, "")
                            End If
                            firstTime = True
                        Else
                            Me.erpShowError.SetError(Me, "")
                        End If
                        lastText = Me.Text
                    End If
            End Select
            If Not Me.thirdLoad = True Then
                Me.lastText = Me.Text
            Else
                Me.thirdLoad = False
            End If
        End Sub

        Public Sub eventKeyPress_TextBox(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
            Select Case e.KeyChar
                Case Microsoft.VisualBasic.ChrW(Keys.Back)
                    keyType = "BackSP"
                Case Else
                    keyType = "Other"
            End Select
        End Sub

        Public Sub eventValidating_TextBox(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Validating
            If Me.m_Required = True And isRequired = 0 Then
                If Me.Text = "" Then
                    e.Cancel = True
                    If Me.m_ShowErrorIcon = True Then
                        Me.erpShowError.SetError(Me, "")
                        Me.erpShowRequired.SetError(Me, "Field value is required!")
                    End If
                End If
            End If
        End Sub

        Public Sub eventValidated_TextBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Validated
            Me.erpShowRequired.SetError(Me, "")
        End Sub

        Public Sub eventoEnter_TextBox(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
            Dim i As Integer
            For i = 0 To Me.Text.Length - 1
                If Me.Text.Chars(i) = "_" Then
                    Exit For
                End If
            Next
            Me.SelectionStart = i
        End Sub

        Private Sub showError(ByVal pInvalidCharacters As Collection)
            If pInvalidCharacters.Count > 1 Then
                Dim invalidCharactersList As String = ""
                Dim n As String
                For Each n In pInvalidCharacters
                    invalidCharactersList &= "'" & n & "', "
                Next
                Me.erpShowError.SetError(Me, "Characters (" & invalidCharactersList.Trim.Remove(invalidCharactersList.Length - 2, 1) & ") are not allowed!")
            Else
                Me.erpShowError.SetError(Me, "Character ('" & CStr(pInvalidCharacters.Item(1)) & "') is not allowed!")
            End If
        End Sub

        Private Sub mmsRequired()
            Me.erpShowError.SetError(Me, "")
            Me.erpShowRequired.SetError(Me, "Field value is Required!")
        End Sub

        Public Shared Sub cancelrequiredFieldsCheck(ByVal pForm As Windows.Forms.Form)
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer
            Dim l As Integer
            For i = 0 To pForm.Controls.Count - 1
                If pForm.Controls(i).GetType.ToString = "glblControls.TextBox" Then
                    CType(pForm.Controls(i), eTextBox).isRequired = 1
                    CType(pForm.Controls(i), eTextBox).erpShowError.SetError(CType(pForm.Controls(i), eTextBox), "")
                    CType(pForm.Controls(i), eTextBox).erpShowRequired.SetError(CType(pForm.Controls(i), eTextBox), "")
                End If
                For j = 0 To pForm.Controls(i).Controls.Count - 1
                    If pForm.Controls(i).Controls(j).GetType.ToString = "glblControls.TextBox" Then
                        CType(pForm.Controls(i).Controls(j), eTextBox).isRequired = 1
                        CType(pForm.Controls(i).Controls(j), eTextBox).erpShowError.SetError(CType(pForm.Controls(i).Controls(j), eTextBox), "")
                        CType(pForm.Controls(i).Controls(j), eTextBox).erpShowRequired.SetError(CType(pForm.Controls(i).Controls(j), eTextBox), "")
                    End If
                    For k = 0 To pForm.Controls(i).Controls(j).Controls.Count - 1
                        If pForm.Controls(i).Controls(j).Controls(k).GetType.ToString = "glblControls.TextBox" Then
                            CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox).isRequired = 1
                            CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox).erpShowError.SetError(CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox), "")
                            CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox).erpShowRequired.SetError(CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox), "")
                        End If
                        For l = 0 To pForm.Controls(i).Controls(j).Controls(k).Controls.Count - 1
                            If pForm.Controls(i).Controls(j).Controls(k).Controls(l).GetType.ToString = "glblControls.TextBox" Then
                                CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox).isRequired = 1
                                CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox).erpShowError.SetError(CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox), "")
                                CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox).erpShowRequired.SetError(CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox), "")
                            End If
                        Next
                    Next
                Next
            Next
        End Sub

        Public Shared Function requiredFieldsCheck(ByVal pForm As Windows.Forms.Form) As Boolean
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer
            Dim l As Integer
            Dim bolRequired As Boolean = False
            For i = 0 To pForm.Controls.Count - 1
                If pForm.Controls(i).GetType.ToString = "glblControls.TextBox" Then
                    If CType(pForm.Controls(i), eTextBox).Text = "" And CType(pForm.Controls(i), eTextBox).Required = True Then
                        CType(pForm.Controls(i), eTextBox).mmsRequired()
                        bolRequired = True
                    End If
                End If
                For j = 0 To pForm.Controls(i).Controls.Count - 1
                    If pForm.Controls(i).Controls(j).GetType.ToString = "glblControls.TextBox" Then
                        If CType(pForm.Controls(i).Controls(j), eTextBox).Text = "" And CType(pForm.Controls(i).Controls(j), eTextBox).Required = True Then
                            CType(pForm.Controls(i).Controls(j), eTextBox).mmsRequired()
                            bolRequired = True
                        End If
                    End If
                    For k = 0 To pForm.Controls(i).Controls(j).Controls.Count - 1
                        If pForm.Controls(i).Controls(j).Controls(k).GetType.ToString = "glblControls.TextBox" Then
                            If CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox).Text = "" And CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox).Required = True Then
                                CType(pForm.Controls(i).Controls(j).Controls(k), eTextBox).mmsRequired()
                                bolRequired = True
                            End If
                        End If
                        For l = 0 To pForm.Controls(i).Controls(j).Controls(k).Controls.Count - 1
                            If pForm.Controls(i).Controls(j).Controls(k).Controls(l).GetType.ToString = "glblControls.TextBox" Then
                                If CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox).Text = "" And CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox).Required = True Then
                                    CType(pForm.Controls(i).Controls(j).Controls(k).Controls(l), eTextBox).mmsRequired()
                                    bolRequired = True
                                End If
                            End If
                        Next
                    Next
                Next
            Next
            If bolRequired = True Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub eventoLeave_TextBox(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            Me.erpShowError.SetError(Me, "")
        End Sub

        Private Sub eventLoad_TextBoxForm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miForm.Load
            If isRequired = 1 Then
                isRequired = 0
            End If
        End Sub

        Private Sub RemoveWaterMark()
            If waterMarkContainer IsNot Nothing Then
                Me.Controls.Remove(waterMarkContainer)
                waterMarkContainer = Nothing
            End If
        End Sub

        Private Sub DrawWaterMark()
            If Me.waterMarkContainer Is Nothing AndAlso Me.TextLength <= 0 Then
                waterMarkContainer = New Windows.Forms.Panel()
                AddHandler waterMarkContainer.Paint, New PaintEventHandler(AddressOf waterMarkContainer_Paint)
                waterMarkContainer.Invalidate()
                AddHandler waterMarkContainer.Click, New EventHandler(AddressOf waterMarkContainer_Click)
                Me.Controls.Add(waterMarkContainer)
            End If
        End Sub

#End Region

#Region "Eventhandlers"

#Region "WaterMark Events"

        Private Sub waterMarkContainer_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.Focus()
        End Sub

        Private Sub waterMarkContainer_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
            waterMarkContainer.Location = New System.Drawing.Point(2, 0)
            waterMarkContainer.Height = Me.Height
            waterMarkContainer.Width = Me.Width
            waterMarkContainer.Anchor = AnchorStyles.Left Or AnchorStyles.Right
            If Me.ContainsFocus Then
                waterMarkBrush = New SolidBrush(Me._waterMarkActiveColor)
            Else
                waterMarkBrush = New SolidBrush(Me._waterMarkColor)
            End If
            Dim g As Graphics = e.Graphics
            g.DrawString(Me._waterMarkText, m_waterMarkFont, waterMarkBrush, New PointF(-2.0F, 1.0F))
        End Sub

#End Region

#Region "CTextBox Events"

        Private Sub ThisHasFocus(ByVal sender As Object, ByVal e As EventArgs)
            waterMarkBrush = New SolidBrush(Me._waterMarkActiveColor)
            If Me.TextLength <= 0 Then
                RemoveWaterMark()
                DrawWaterMark()
            End If
        End Sub

        Private Sub ThisWasLeaved(ByVal sender As Object, ByVal e As EventArgs)
            If Me.TextLength > 0 Then
                RemoveWaterMark()
            Else
                Me.Invalidate()
            End If
        End Sub

        Private Sub ThisTextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Me.TextLength > 0 Then
                RemoveWaterMark()
            Else
                DrawWaterMark()
            End If
        End Sub

#Region "Overrided Events"

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            'Draw the watermark even in design time
            DrawWaterMark()
        End Sub

        Protected Overrides Sub OnInvalidated(ByVal e As InvalidateEventArgs)
            MyBase.OnInvalidated(e)
            'Check if there is a watermark
            If waterMarkContainer IsNot Nothing Then
                'if there is a watermark it should also be invalidated();
                waterMarkContainer.Invalidate()
            End If
        End Sub

#End Region

#End Region

#End Region

#Region "Properties"
        <Category("Watermark attribtues")> _
        <Description("Sets the text of the watermark")> _
        Public Property WaterMark() As String
            Get
                Return Me._waterMarkText
            End Get
            Set(ByVal value As String)
                Me._waterMarkText = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Watermark attribtues")> _
        <Description("When the control gaines focus, this color will be used as the watermark's forecolor")> _
        Public Property WaterMarkActiveForeColor() As Color
            Get
                Return Me._waterMarkActiveColor
            End Get

            Set(ByVal value As Color)
                Me._waterMarkActiveColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Watermark attribtues")> _
        <Description("When the control looses focus, this color will be used as the watermark's forecolor")> _
        Public Property WaterMarkForeColor() As Color
            Get
                Return Me._waterMarkColor
            End Get

            Set(ByVal value As Color)
                Me._waterMarkColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Watermark attribtues")> _
        <Description("The font used on the watermark. Default is the same as the control")> _
        Public Property WaterMarkFont() As Font
            Get
                Return Me.m_waterMarkFont
            End Get

            Set(ByVal value As Font)
                Me.m_waterMarkFont = value
                Me.Invalidate()
            End Set
        End Property

#End Region

#Region "Focus overrides"

        Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)

            MyBase.OnGotFocus(e)
            If ltLinkType <> LinkTypes.None Then
                Me.SwitchToEditMode(True)
            End If

        End Sub

        Protected Overrides Sub OnLostFocus(ByVal e As System.EventArgs)

            MyBase.OnLostFocus(e)
            If ltLinkType <> LinkTypes.None Then
                Me.SwitchToEditMode(False)
            End If

        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)

            MyBase.OnTextChanged(e)
            If ltLinkType <> LinkTypes.None Then
                FillLinkData()
            End If

        End Sub


#End Region

#Region "Click Handling"

        Protected Sub SwitchToEditMode(ByVal _bEditMode As Boolean)
            llLinkLabel.Visible = Not _bEditMode

        End Sub

        Private Sub FillLinkData()
            llLinkLabel.Text = Me.Text
            Dim sLinkType As String = ""

            Select Case ltLinkType
                Case LinkTypes.Http
                    If Me.Text.ToLower().IndexOf("http://") < 0 AndAlso Me.Text.ToLower().IndexOf("https://") < 0 Then
                        sLinkType = "http://"
                    End If
                    Exit Select
                Case LinkTypes.Ftp
                    If Me.Text.ToLower().IndexOf("ftp://") < 0 Then
                        sLinkType = "ftp://"
                    End If
                    Exit Select
                Case LinkTypes.Email
                    If Me.Text.ToLower().IndexOf("mailto:") < 0 Then
                        sLinkType = "mailto:"
                    End If
                    Exit Select
            End Select

            llLinkLabel.Links.Clear()
            llLinkLabel.Links.Add(0, llLinkLabel.Text.Length, sLinkType & Convert.ToString(Me.Text))

        End Sub

        Private Sub UseHyperlink()

            Try

                If llLinkLabel.Links.Count > 0 Then
                    Dim sLink As String = llLinkLabel.Links(0).LinkData.ToString()
                    System.Diagnostics.Process.Start(sLink)

                End If
            Catch ex As Exception


                Throw New ArgumentException("Link error!", ex)
            End Try

        End Sub

#End Region

#Region "Link Activation"

        Private Sub ll_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
            If ltLinkType <> LinkTypes.None Then
                UseHyperlink()
            End If
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            If ltLinkType <> LinkTypes.None Then
                If e.Button = MouseButtons.Left AndAlso (Control.ModifierKeys And Keys.Control) = Keys.Control Then
                    UseHyperlink()
                Else
                    MyBase.OnMouseDown(e)
                End If
            Else
                MyBase.OnMouseDown(e)
            End If
        End Sub

#End Region

#Region "Focus Handling"

        Private Sub llLinkLabel_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            If Not bLinkClicked Then
                Me.Focus()
                bLinkClicked = False
            End If
        End Sub

        Private Sub llLinkLabel_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
            bLinkClicked = True
        End Sub


#End Region

    End Class

#Region "LinkTypes Enum"

    Public Enum LinkTypes
        None
        Http
        Ftp
        Email
    End Enum

#End Region

End Namespace