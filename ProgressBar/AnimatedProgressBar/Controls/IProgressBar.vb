
Imports System.Drawing

Namespace UIControls.ProgressBar
    Public Interface IProgressBar

        Property HoverGradient() As GradientHover

        Property ProgressGradient() As GradientProgress

        Property BackgroundGradient() As GradientBackground

        Property ProgressColorizer() As ColorizerProgress

        Property DigitBoxGradient() As GradientDigitBox

        Property Value() As Integer

        Property Minimum() As Integer

        Property Maximum() As Integer

        Property IsPaintBorder() As Boolean

        Property IsDigitDrawEnabled() As Boolean

        Property ShowPercentage() As Boolean

        Property DisplayFormat() As String

        Property BorderColor() As Color

        Property ControlBorderStyle() As EasyProgressBarBorderStyle

        Event ValueChanged As EventHandler
    End Interface
End Namespace
