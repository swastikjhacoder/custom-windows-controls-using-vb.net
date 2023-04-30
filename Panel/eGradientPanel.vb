Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.ComponentModel

Namespace UIControls.Panels
    ''' <summary>
    ''' Enables you to group control in a panel with a gradient background.
    ''' </summary>
    <ToolboxItem(True), Description("Enables you to group control in a panel with a gradient background."), ToolboxBitmap(GetType(eGradientPanel), "Resources.gradient_panel.bmp")> _
    Public Class eGradientPanel
        Inherits Windows.Forms.Panel
#Region "Properties"

        Private _startColor As Color = Color.Black
        ''' <summary>
        ''' Gets or sets the left/upper color for the gradient panel
        ''' </summary>
        <Category("Appearance"), Description("The left/upper color for the gradient panel.")> _
        Public Property StartColor() As Color
            Get
                Return _startColor
            End Get
            Set(ByVal value As Color)
                _startColor = value
                Me.Refresh()
            End Set
        End Property

        Private _endColor As Color = Color.White
        ''' <summary>
        ''' Gets or sets the right/lower color for the gradient panel
        ''' </summary>
        <Category("Appearance"), Description("The right/lower color for the gradient panel.")> _
        Public Property EndColor() As Color
            Get
                Return _endColor
            End Get
            Set(ByVal value As Color)
                _endColor = value
                Me.Refresh()
            End Set
        End Property

        Private _gradientMode As LinearGradientMode = LinearGradientMode.BackwardDiagonal

        ''' <summary>
        ''' Gets or sets the direction of the linear gradient
        ''' </summary>
        <Category("Appearance"), Description("The direction of the linear gradient.")> _
        Public Property GradientMode() As LinearGradientMode
            Get
                Return _gradientMode
            End Get
            Set(ByVal value As LinearGradientMode)
                _gradientMode = value
                Me.Refresh()
            End Set
        End Property

#End Region

#Region "Ctor"

        'Public Sub New()
        '    MyBase.New()
        'End Sub

#End Region

#Region "Paints overrides"

        Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
            Dim brush As New LinearGradientBrush(pevent.ClipRectangle, _startColor, _endColor, _gradientMode)
            pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle)
            brush.Dispose()
        End Sub
#End Region
    End Class
End Namespace