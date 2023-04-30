'***************************************************************************************************|
'/<summary>
'/ CollapseBox is the open/close box in the upper left corner of CollapseGroupBox.
'/</summary>
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

Namespace UIControls.GroupBox
    <ToolboxBitmap(GetType(eCollapseGroupBox), "Resources.collapse_groupbox.bmp")> _
    Public Class eCollapseBox
        Inherits eCollapseButton

        Private components As Container = Nothing
        Private boolIsPlus As Boolean

        '/<summary>
        '/IsPlus()returns the state of the CollapseBox
        '/</summary>
        Public Property IsPlus() As Boolean
            Get
                Return boolIsPlus
            End Get
            Set(ByVal Value As Boolean)
                boolIsPlus = Value
                Invalidate()
            End Set
        End Property

        Public Sub New()

            MyBase.New()

            'This call is required by the Component Designer.
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
#Region " Component Designer generated code "

        Private Sub InitializeComponent()
            ' 
            ' CollapseBox
            ' 
            AddHandler Click, AddressOf CollapseBox_Click
            AddHandler DoubleClick, AddressOf CollapseBox_DoubleClick
            AddHandler Paint, AddressOf CollapseBox_Paint

        End Sub
#End Region

        Private Sub CollapseBox_Click(ByVal sender As Object, ByVal e As EventArgs)

            IsPlus = Not IsPlus

        End Sub
        Private Sub CollapseBox_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)

            ' fast clicking registers as double-clicking, so map a double-click
            ' event into a single click.
            CollapseBox_Click(sender, e)

        End Sub
        Private Sub CollapseBox_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)

            Dim g As Graphics = e.Graphics

            If (m_ButtonState = ButtonState.TrackingInside) Then
                g.FillRectangle(Brushes.LightGray, ClientRectangle)
            Else
                g.FillRectangle(Brushes.White, ClientRectangle)
            End If

            ' get the client area coordinates
            Dim rec As Rectangle = New Rectangle()
            rec = ClientRectangle
            rec.Width = rec.Width - 1
            rec.Height = rec.Height - 1

            g.DrawRectangle(Pens.Black, rec)

            ' DrawLine(Pen, Integer, Integer, Integer, Integer)
            ' Create coordinates of points that define the box
            Dim x1 As Integer = rec.X + 2
            Dim y1 As Integer = rec.Y + (Height / 2)
            Dim x2 As Integer = rec.X + (Width - 3)
            Dim y2 As Integer = rec.Y + (Height / 2)

            ' draw horizontal line
            g.DrawLine(Pens.Black, x1, y1, x2, y2)

            If (IsPlus) Then
                ' draw bisecting verticalline
                x1 = rec.X + (Width / 2)
                y1 = rec.Y + 2
                x2 = rec.X + (Width / 2)
                y2 = rec.Y + (Height - 3)
                g.DrawLine(Pens.Black, x1, y1, x2, y2)
            End If

        End Sub
    End Class
End Namespace

