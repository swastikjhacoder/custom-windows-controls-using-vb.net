
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles

Namespace UIControls.TabControl.BaseClasses
    <ToolboxItem(False)> _
    Public Class BaseStyledPanel
        Inherits ContainerControl
#Region "Fields"

        Private Shared renderer As ToolStripProfessionalRenderer

#End Region

#Region "Events"

        Public Event ThemeChanged As EventHandler

#End Region

#Region "Ctor"

        Shared Sub New()
            renderer = New ToolStripProfessionalRenderer()
        End Sub

        Public Sub New()
            ' Set painting style for better performance.
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.UserPaint, True)
        End Sub

#End Region

#Region "Methods"

        Protected Overrides Sub OnSystemColorsChanged(ByVal e As EventArgs)
            MyBase.OnSystemColorsChanged(e)
            UpdateRenderer()
            Invalidate()
        End Sub

        Protected Overridable Sub OnThemeChanged(ByVal e As EventArgs)
            RaiseEvent ThemeChanged(Me, e)
        End Sub

        Private Sub UpdateRenderer()
            If Not UseThemes Then
                renderer.ColorTable.UseSystemColors = True
            Else
                renderer.ColorTable.UseSystemColors = False
            End If
        End Sub

#End Region

#Region "Props"

        <Browsable(False)> _
        Public ReadOnly Property ToolStripRenderer() As ToolStripProfessionalRenderer
            Get
                Return renderer
            End Get
        End Property

        <DefaultValue(True)> _
        <Browsable(False)> _
        Public ReadOnly Property UseThemes() As Boolean
            Get
                Return VisualStyleRenderer.IsSupported AndAlso VisualStyleInformation.IsSupportedByOS AndAlso Application.RenderWithVisualStyles
            End Get
        End Property

#End Region
    End Class
End Namespace