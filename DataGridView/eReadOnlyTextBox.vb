
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms

Namespace UIControls.DataGridView
    Partial Public Class eReadOnlyTextBox
        Inherits Control

        Private format As StringFormat
        Public Sub New()
            InitializeComponent()

            format = New StringFormat(StringFormatFlags.NoWrap Or StringFormatFlags.FitBlackBox Or StringFormatFlags.MeasureTrailingSpaces)
            format.LineAlignment = StringAlignment.Center

            Me.Height = 10
            Me.Width = 10

            Me.Padding = New Padding(2)
        End Sub

        Public Sub New(ByVal container As IContainer)
            container.Add(Me)
            InitializeComponent()

            AddHandler Me.TextChanged, AddressOf ReadOnlyTextBox_TextChanged
        End Sub

        Private Sub ReadOnlyTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Not String.IsNullOrEmpty(m_formatString) AndAlso Not String.IsNullOrEmpty(Text) Then
                Text = String.Format(m_formatString, Text)
            End If
        End Sub

        Private m_borderColor As Color = Color.Black

        Private m_isSummary As Boolean
        Public Property IsSummary() As Boolean
            Get
                Return m_isSummary
            End Get
            Set(ByVal value As Boolean)
                m_isSummary = value
            End Set
        End Property

        Private m_isLastColumn As Boolean
        Public Property IsLastColumn() As Boolean
            Get
                Return m_isLastColumn
            End Get
            Set(ByVal value As Boolean)
                m_isLastColumn = value
            End Set
        End Property

        Private m_formatString As String
        Public Property FormatString() As String
            Get
                Return m_formatString
            End Get
            Set(ByVal value As String)
                m_formatString = value
            End Set
        End Property


        Private m_textAlign As HorizontalAlignment = HorizontalAlignment.Left
        <DefaultValue(HorizontalAlignment.Left)> _
        Public Property TextAlign() As HorizontalAlignment
            Get
                Return m_textAlign
            End Get
            Set(ByVal value As HorizontalAlignment)
                m_textAlign = value
                setFormatFlags()
            End Set
        End Property

        Private m_trimming As StringTrimming = StringTrimming.None
        <DefaultValue(StringTrimming.None)> _
        Public Property Trimming() As StringTrimming
            Get
                Return m_trimming
            End Get
            Set(ByVal value As StringTrimming)
                m_trimming = value
                setFormatFlags()
            End Set
        End Property

        Private Sub setFormatFlags()
            format.Alignment = TextHelper.TranslateAligment(TextAlign)
            format.Trimming = m_trimming
        End Sub

        Public Property BorderColor() As Color
            Get
                Return m_borderColor
            End Get
            Set(ByVal value As Color)
                m_borderColor = value
            End Set
        End Property

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim subWidth As Integer = 0
            Dim textBounds As Rectangle

            If Not String.IsNullOrEmpty(m_formatString) AndAlso Not String.IsNullOrEmpty(Text) Then
                Text = [String].Format((Convert.ToString("{0:") & m_formatString) + "}", Convert.ToDecimal(Text))
            End If

            textBounds = New Rectangle(Me.ClientRectangle.X + 2, Me.ClientRectangle.Y + 2, Me.ClientRectangle.Width - 2, Me.ClientRectangle.Height - 2)
            Using pen As New Pen(m_borderColor)
                If m_isLastColumn Then
                    subWidth = 1
                End If

                e.Graphics.FillRectangle(New SolidBrush(Me.BackColor), Me.ClientRectangle)
                e.Graphics.DrawRectangle(pen, Me.ClientRectangle.X, Me.ClientRectangle.Y, Me.ClientRectangle.Width - subWidth, Me.ClientRectangle.Height - 1)
                e.Graphics.DrawString(Text, Font, Brushes.Black, textBounds, format)
            End Using
        End Sub
    End Class
End Namespace
