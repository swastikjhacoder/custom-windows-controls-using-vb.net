Imports System.Windows.Forms
Imports System.Drawing

Namespace UIControls.Panel
    <Global.System.ComponentModel.Designer(GetType(Global.System.Windows.Forms.Design.ParentControlDesigner))> _
    <ToolboxBitmap(GetType(eRoundedPanel), "Resources.rounded_panel.bmp")> _
    Public Class eRoundedPanel
        Inherits System.Windows.Forms.UserControl
#Region " Windows Form Designer generated code "
        Public Sub New()
            MyBase.New()
            'This call is required by the Windows Form Designer.
            InitializeComponent()
            'Add any initialization after the InitializeComponent() call
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.DoubleBuffer Or ControlStyles.UserPaint, True)
        End Sub
        'UserControl overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub
        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer
        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container
        End Sub
#End Region
        Private m_BorderRadius As Int32 = 32
        Public Property BorderRadius() As Int32
            Get
                Return m_BorderRadius
            End Get
            Set(ByVal Value As Int32)
                m_BorderRadius = Value
                Me.Invalidate()
            End Set
        End Property
        Protected Overrides ReadOnly Property DefaultSize() As System.Drawing.Size
            Get
                Return New Size(200, 100)
            End Get
        End Property
        Protected Overrides Sub OnMove(ByVal e As System.EventArgs)
            MyBase.OnMove(e)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            MyBase.OnResize(e)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
            'Draw the Parent onto our Control to give pseudo transparency.
            'The BeginContainer and EndContainer calls stop incorrect painting of 
            'child controls when both container and child have BackColor set to Transparent.
            'This only happens as a result of the TranslateTransform() call.
            Dim g As System.Drawing.Drawing2D.GraphicsContainer = pevent.Graphics.BeginContainer()
            Dim translateRect As Rectangle = Me.Bounds
            pevent.Graphics.TranslateTransform(-Me.Left, -Me.Top)
            Dim pe As PaintEventArgs = New PaintEventArgs(pevent.Graphics, translateRect)
            Me.InvokePaintBackground(Me.Parent, pe)
            Me.InvokePaint(Me.Parent, pe)
            pevent.Graphics.ResetTransform()
            pevent.Graphics.EndContainer(g)
            'Define the custom Border Region, Brush and Pen.
            Dim border As System.Drawing.Drawing2D.GraphicsPath
            Dim paintBrush As New SolidBrush(Me.BackColor)
            Dim borderPen As New Pen(Me.ForeColor)
            Dim r As Rectangle = Me.ClientRectangle
            'Set the Region of the Control
            Me.Region = New Region(RoundRegion(r))
            r.Inflate(-1, -1)
            border = RoundRegion(r)
            'Fill The Region with the Controls BackColor
            pevent.Graphics.FillPath(paintBrush, border)
            'Paint any BackgroundImage that might have been set
            If Not (Me.BackgroundImage Is Nothing) Then
                Dim br As Brush = New TextureBrush(Me.BackgroundImage)
                pevent.Graphics.FillPath(br, border)
                br.Dispose()
            End If
            'Draw the Region
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            pevent.Graphics.DrawPath(borderPen, border)
            'Clean Up
            borderPen.Dispose()
            paintBrush.Dispose()
            border.Dispose()
        End Sub
        Private Function RoundRegion(ByVal r As Rectangle) As System.Drawing.Drawing2D.GraphicsPath
            'Scale the radius if it's too large to fit.
            Dim radius As Int32 = m_BorderRadius
            If (radius > (r.Width)) Then radius = r.Width
            If (radius > (r.Height)) Then radius = r.Height
            Dim path As New System.Drawing.Drawing2D.GraphicsPath
            If radius <= 0 Then
                path.AddRectangle(r)
            Else
                path.AddArc(r.Left, r.Top, radius, radius, 180, 90)
                path.AddArc(r.Right - radius, r.Top, radius, radius, 270, 90)
                path.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90)
                path.AddArc(r.Left, r.Bottom - radius, radius, radius, 90, 90)
                path.CloseFigure()
            End If
            Return path
        End Function
    End Class
End Namespace
