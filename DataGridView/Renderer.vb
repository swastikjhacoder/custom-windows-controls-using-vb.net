Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Namespace UIControls.DataGridView
    Public Class Renderer

        Public Enum RenderingMode
            Office2007OrangeHover
            Office2007OrangeSelected
            Office2007GrayHover
        End Enum
        Public Sub Fill3DRectangle(ByVal r As Rectangle, ByVal rm As RenderingMode, ByVal g As Graphics)
            Dim r1, r2 As Rectangle
            Dim h As Integer = r.Height
            Dim w As Integer = r.Width
            Dim h1 As Integer = (h * 40) / 100
            Dim lb1 As LinearGradientBrush
            Dim lb2 As LinearGradientBrush
            Dim c1, c2, c3, c4, bc As Color
            Select Case rm
                Case RenderingMode.Office2007OrangeHover
                    r1 = New Rectangle(r.X, r.Y, r.Width, h)
                    r2 = New Rectangle(r.X, r.Y + h1, r.Width, h - h1 - 1)
                    c1 = Color.FromArgb(255, 253, 236)
                    c2 = Color.FromArgb(255, 238, 177)
                    c3 = Color.FromArgb(255, 214, 101)
                    c4 = Color.FromArgb(255, 226, 137)
                    bc = Color.FromArgb(219, 206, 153)
                    lb1 = New LinearGradientBrush(r1, c1, c2, Drawing2D.LinearGradientMode.Vertical)
                    lb2 = New LinearGradientBrush(r2, c3, c4, Drawing2D.LinearGradientMode.Vertical)

                Case RenderingMode.Office2007OrangeSelected
                    r1 = New Rectangle(r.X, r.Y, r.Width, h)
                    r2 = New Rectangle(r.X, r.Y + h1, r.Width, h - h1 - 1)
                    c1 = Color.FromArgb(251, 209, 163)
                    c2 = Color.FromArgb(253, 182, 104)
                    c3 = Color.FromArgb(250, 155, 52)
                    c4 = Color.FromArgb(253, 230, 158)
                    bc = Color.FromArgb(254, 208, 53)
                    lb1 = New LinearGradientBrush(r1, c1, c2, Drawing2D.LinearGradientMode.Vertical)
                    lb2 = New LinearGradientBrush(r2, c3, c4, Drawing2D.LinearGradientMode.Vertical)
            End Select
            'g.FillRectangle(lb1, r1)
            'g.FillRectangle(lb2, r2)
            'r.Inflate(-1, -1)
            g.DrawRectangle(New Pen(bc), r)
        End Sub

        Public Sub FillGradientRectangle(ByVal r As Rectangle, ByVal rm As RenderingMode, ByVal g As Graphics)
            Dim r1 As Rectangle
            Dim h As Integer = r.Height
            Dim w As Integer = r.Width
            Dim lb1 As LinearGradientBrush
            Dim c1, c2, bc As Color
            Select Case rm
                Case RenderingMode.Office2007OrangeHover
                    r1 = New Rectangle(r.X, r.Y, r.Width, h)
                    c1 = Color.FromArgb(255, 253, 236)
                    c2 = Color.FromArgb(255, 226, 137)
                    bc = Color.FromArgb(219, 206, 153)
                    lb1 = New LinearGradientBrush(r1, c1, c2, Drawing2D.LinearGradientMode.Vertical)

                Case RenderingMode.Office2007OrangeSelected
                    r1 = New Rectangle(r.X, r.Y, r.Width, h)
                    c1 = Color.FromArgb(251, 209, 163)
                    c2 = Color.FromArgb(253, 230, 158)
                    bc = Color.FromArgb(254, 208, 53)
                    lb1 = New LinearGradientBrush(r1, c1, c2, Drawing2D.LinearGradientMode.Vertical)

                Case RenderingMode.Office2007GrayHover
                    r1 = New Rectangle(r.X, r.Y, r.Width, h)
                    c1 = Color.FromArgb(245, 249, 251)
                    c2 = Color.FromArgb(212, 220, 233)
                    bc = Color.FromArgb(135, 169, 213)
                    lb1 = New LinearGradientBrush(r1, c1, c2, Drawing2D.LinearGradientMode.Vertical)
            End Select
            'g.FillRectangle(lb1, r1)
            'r.Inflate(-1, -1)
            g.DrawRectangle(New Pen(bc), r)
        End Sub
    End Class
End Namespace
