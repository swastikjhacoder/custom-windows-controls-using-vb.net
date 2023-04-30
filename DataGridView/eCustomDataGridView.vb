Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms

Namespace UIControls.DataGridView
    Public Class eCustomDataGridView
        Inherits Windows.Forms.DataGridView

        Private Sub CustomGrid_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
            e.Handled = True
            If e.RowIndex < 0 Then
                DrawColumnHeader(e)
            Else
                DrawCell(e)
            End If
            e.PaintContent(e.CellBounds)
        End Sub

        Protected Sub DrawColumnHeader(ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)
            Dim h As Integer = e.CellBounds.Height
            Dim w As Integer = e.CellBounds.Width
            Dim h1 As Integer = h * 0.4
            Dim ct As New eOfficeColorTable
            Dim r1 As Rectangle = New Rectangle(e.CellBounds.X, e.CellBounds.Y, w, h1)
            Dim r2 As Rectangle = New Rectangle(e.CellBounds.X, h1, w, h - h1 + 1)
            Dim lb1 As LinearGradientBrush = New LinearGradientBrush(r1, ct.ColumnHeaderStartColor, ct.ColumnHeaderMidColor1, Drawing2D.LinearGradientMode.Vertical)
            Dim lb2 As LinearGradientBrush = New LinearGradientBrush(r2, ct.ColumnHeaderMidColor2, ct.ColumnHeaderEndColor, Drawing2D.LinearGradientMode.Vertical)
            Dim p As Pen = New Pen(ct.GridColor, 1)
            Dim frmt As StringFormat = New StringFormat
            frmt.Alignment = StringAlignment.Center
            frmt.FormatFlags = StringFormatFlags.DisplayFormatControl
            frmt.LineAlignment = StringAlignment.Center
            With e.Graphics
                .FillRectangle(lb1, r1)
                .FillRectangle(lb2, r2)
                .DrawRectangle(p, e.CellBounds)
            End With
        End Sub

        Protected Sub DrawCell(ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)
            Dim r1 As Rectangle = New Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height)
            Dim r2 As Rectangle = New Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height)
            Dim ct As New eOfficeColorTable
            Dim cellColor As Color
            Dim borderColor As Color
            Dim p As Pen
            If e.State = 97 Or e.State = 105 Then
                borderColor = ct.GridColor
                cellColor = ct.ActiveCellColor
                p = New Pen(borderColor, 1)
            Else
                borderColor = ct.GridColor
                p = New Pen(borderColor, 1)
                If e.State = 109 Then
                    cellColor = ct.ReadonlyCellColor
                Else
                    cellColor = ct.DefaultCellColor
                End If

            End If
            If e.ColumnIndex < 0 Then
                cellColor = ct.ColumnHeaderMidColor2
            End If
            With e.Graphics
                .FillRectangle(New SolidBrush(cellColor), e.CellBounds)
                Dim rnd As New Renderer
                If e.State = 97 Then
                    rnd.Fill3DRectangle(e.CellBounds, Renderer.RenderingMode.Office2007OrangeHover, e.Graphics)
                End If
                If e.ColumnIndex < 0 Then
                    If e.State = 97 Then
                        rnd.FillGradientRectangle(e.CellBounds, Renderer.RenderingMode.Office2007OrangeHover, e.Graphics)
                    Else
                        rnd.FillGradientRectangle(e.CellBounds, Renderer.RenderingMode.Office2007GrayHover, e.Graphics)
                    End If
                End If
                .DrawRectangle(p, e.CellBounds)
                .DrawRectangle(p, New Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, Me.Height))
            End With

        End Sub

        Protected Overrides Sub OnCellClick(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
            Me.BeginEdit(False)
        End Sub

        Private Sub CustomGrid_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            Dim h As Integer = Me.RowTemplate.Height
            Dim rh As Integer = Me.ColumnHeadersHeight + 1
            Dim lh As Integer = Me.GetRowDisplayRectangle(RowCount - 1, False).Bottom
            Dim r As Rectangle
            Dim ct As New eOfficeColorTable
            If lh < Height Then
                For i As Integer = lh + rh To Me.Height Step h
                    With e.Graphics
                        r = New Rectangle(0, i, Me.Width, h)
                        .DrawRectangle(New Pen(ct.GridColor), r)
                    End With
                Next
            End If
        End Sub

        Public Sub New()
            'SET DEFAULT SETTINGS
            Me.BackgroundColor = Color.WhiteSmoke
            Me.DefaultCellStyle.SelectionForeColor = Color.Black
            Me.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.RowHeadersWidth = 25
        End Sub
    End Class
End Namespace
