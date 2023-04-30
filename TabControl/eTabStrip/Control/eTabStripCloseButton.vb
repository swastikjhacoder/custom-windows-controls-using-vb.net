
Imports System.Drawing
Imports System.Windows.Forms

Namespace UIControls.TabControl
    Friend Class eTabStripCloseButton
#Region "Fields"

        Private crossRect As Rectangle = Rectangle.Empty
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
                Return crossRect
            End Get
            Set(ByVal value As Rectangle)
                crossRect = value
            End Set
        End Property

#End Region

#Region "Ctor"

        Friend Sub New(ByVal renderer As ToolStripProfessionalRenderer)
            Me.renderer = renderer
        End Sub

#End Region

#Region "Methods"

        Public Sub DrawCross(ByVal g As Graphics)
            If m_isMouseOver Then
                Dim fill As Color = renderer.ColorTable.ButtonSelectedHighlight

                g.FillRectangle(New SolidBrush(fill), crossRect)

                Dim borderRect As Rectangle = crossRect

                borderRect.Width -= 1
                borderRect.Height -= 1

                g.DrawRectangle(SystemPens.Highlight, borderRect)
            End If

            Using pen As New Pen(Color.Black, 1.6F)
                g.DrawLine(pen, crossRect.Left + 3, crossRect.Top + 3, crossRect.Right - 5, crossRect.Bottom - 4)

                g.DrawLine(pen, crossRect.Right - 5, crossRect.Top + 3, crossRect.Left + 3, crossRect.Bottom - 4)
            End Using
        End Sub

#End Region
    End Class
End Namespace