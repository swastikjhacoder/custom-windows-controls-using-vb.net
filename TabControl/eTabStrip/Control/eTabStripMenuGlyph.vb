
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace UIControls.TabControl
    Friend Class eTabStripMenuGlyph
#Region "Fields"

        Private glyphRect As Rectangle = Rectangle.Empty
        Private m_isMouseOver As Boolean = False
        Private renderer As ToolStripProfessionalRenderer

#End Region

#Region "Props"

        Public Property IsMouseOver() As Boolean
            Get
                Return m_isMouseOver
            End Get
            Set(ByVal value As Boolean)
                m_isMouseOver = value
            End Set
        End Property

        Public Property Bounds() As Rectangle
            Get
                Return glyphRect
            End Get
            Set(ByVal value As Rectangle)
                glyphRect = value
            End Set
        End Property

#End Region

#Region "Ctor"

        Friend Sub New(ByVal renderer As ToolStripProfessionalRenderer)
            Me.renderer = renderer
        End Sub

#End Region

#Region "Methods"

        Public Sub DrawGlyph(ByVal g As Graphics)
            If m_isMouseOver Then
                Dim fill As Color = renderer.ColorTable.ButtonSelectedHighlight
                'Color.FromArgb(35, SystemColors.Highlight);
                g.FillRectangle(New SolidBrush(fill), glyphRect)
                Dim borderRect As Rectangle = glyphRect

                borderRect.Width -= 1
                borderRect.Height -= 1

                g.DrawRectangle(SystemPens.Highlight, borderRect)
            End If

            Dim bak As SmoothingMode = g.SmoothingMode

            g.SmoothingMode = SmoothingMode.[Default]

            Using pen As New Pen(Color.Black)
                pen.Width = 2

                g.DrawLine(pen, New Point(glyphRect.Left + (glyphRect.Width / 3) - 2, glyphRect.Height / 2 - 1), New Point(glyphRect.Right - (glyphRect.Width / 3), glyphRect.Height / 2 - 1))
            End Using

            g.FillPolygon(Brushes.Black, New Point() {New Point(glyphRect.Left + (glyphRect.Width / 3) - 2, glyphRect.Height / 2 + 2), New Point(glyphRect.Right - (glyphRect.Width / 3), glyphRect.Height / 2 + 2), New Point(glyphRect.Left + glyphRect.Width / 2 - 1, glyphRect.Bottom - 4)})

            g.SmoothingMode = bak
        End Sub

#End Region
    End Class
End Namespace