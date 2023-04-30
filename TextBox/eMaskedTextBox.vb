Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing

Namespace UIControls.TextBox
    <ToolboxBitmap(GetType(eMaskedTextBox), "Resources.maskedtextbox.bmp")> _
    Public Class eMaskedTextBox
        Inherits System.Windows.Forms.TextBox

        Private mstrMask As String = ""
        Private mParams() As Char = {CChar("#"), CChar("&"), CChar("!")}
        Protected _waterMarkText As String = "Default Watermark..."
        Protected _waterMarkColor As Color
        Protected _waterMarkActiveColor As Color
        Private waterMarkContainer As Windows.Forms.Panel
        Private m_waterMarkFont As Font
        Private waterMarkBrush As SolidBrush

#Region " Component Designer generated code "
        Public Sub New(ByVal Container As System.ComponentModel.IContainer)
            MyClass.New()
            Container.Add(Me)
        End Sub

        Public Sub New()
            MyBase.New()
            InitializeComponent()
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
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container
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

#End Region

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

        Public ReadOnly Property UnformatedText() As String
            Get
                Dim i As Integer
                Dim strText As String = ""
                For i = 0 To mstrMask.Length - 1
                    If Array.IndexOf(mParams, mstrMask.Chars(i)) > -1 AndAlso Me.Text.Chars(i) <> "_" Then
                        strText += Me.Text.Chars(i)
                    End If
                Next
                Return strText
            End Get
        End Property

        Public Property Mask() As String
            Get
                Return (mstrMask)
            End Get
            Set(ByVal Value As String)
                'Use # for Digit only
                'Use & for Letter only
                'Use ! for Letter or Digit
                mstrMask = Value
                Me.Text = mstrMask
                Me.Text = Me.Text.Replace("#", "_").Replace("&", "_").Replace("!", "_")
            End Set
        End Property

        Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
            If e.KeyCode = Keys.Delete Then
                e.Handled = True
            End If
        End Sub

        Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            Dim chrKeyPressed As Char = e.KeyChar
            Dim intSelStart As Integer = Me.SelectionStart
            Dim intDelTo As Integer = Me.SelectionStart + Me.SelectionLength - 1

            Dim strText As String = Me.Text
            Dim bolDelete As Boolean = False
            Dim i As Integer
            e.Handled = True
            If chrKeyPressed = ControlChars.Back Then
                bolDelete = True
                If intSelStart > 0 And intDelTo < intSelStart Then
                    intSelStart -= 1
                End If
            End If
            For i = Me.SelectionStart To mstrMask.Length - 1
                If mstrMask.Chars(i) = "#" AndAlso Char.IsDigit(chrKeyPressed) _
                    OrElse mstrMask.Chars(i) = "&" AndAlso Char.IsLetter(chrKeyPressed) _
                    OrElse mstrMask.Chars(i) = "!" AndAlso Char.IsLetterOrDigit(chrKeyPressed) Then

                    strText = strText.Remove(i, 1).Insert(i, chrKeyPressed)
                    intSelStart = i + 1
                    bolDelete = True
                End If
                If Array.IndexOf(mParams, mstrMask.Chars(i)) > -1 Then
                    Exit For
                End If
            Next
            If bolDelete Then
                For i = intSelStart To intDelTo
                    If Array.IndexOf(mParams, mstrMask.Chars(i)) > -1 Then
                        strText = strText.Remove(i, 1).Insert(i, "_")
                    End If
                Next
                Me.Text = strText
                Me.SelectionStart = intSelStart
                Me.SelectionLength = 0
            End If
        End Sub

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
            DrawWaterMark()
        End Sub

        Protected Overrides Sub OnInvalidated(ByVal e As InvalidateEventArgs)
            MyBase.OnInvalidated(e)
            If waterMarkContainer IsNot Nothing Then
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
    End Class
End Namespace

