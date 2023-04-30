Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace UIControls.DataGridView
    Public Class eProgressColumn
        Inherits DataGridViewImageColumn
        Dim pShineColor As Color = Color.SkyBlue
        Dim pBarColor As Color = Color.SteelBlue
        Dim pBarBGColor As Color = Color.FromArgb(190, 190, 190)
        Sub redrawColumn()
            If Me.DataGridView IsNot Nothing Then
                Me.DataGridView.InvalidateColumn(Me.Index)
            End If
        End Sub
        Public Property BarShineColor() As Color
            Get
                Return pShineColor
            End Get
            Set(ByVal value As Color)
                pShineColor = value
                redrawColumn()
            End Set
        End Property
        Public Property BarBGColor() As Color
            Get
                Return pBarBGColor
            End Get
            Set(ByVal value As Color)
                pBarBGColor = value
                redrawColumn()
            End Set
        End Property
        Public Property BarColor() As Color
            Get
                Return pBarColor
            End Get
            Set(ByVal value As Color)
                pBarColor = value
                redrawColumn()
            End Set
        End Property
        Public Sub New()
            Me.CellTemplate = New DataGridViewProgressCell
        End Sub
    End Class

    Public Class DataGridViewProgressCell
        Inherits DataGridViewImageCell
        Sub New()
            ValueType = Type.GetType("Integer")
            Value = -1
        End Sub
        Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, ByVal valueTypeConverter As TypeConverter, ByVal formattedValueTypeConverter As TypeConverter, ByVal context As DataGridViewDataErrorContexts) As Object
            Static emptyImage As Bitmap = New Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            GetFormattedValue = emptyImage
        End Function
        Protected Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal clipBounds As System.Drawing.Rectangle, ByVal cellBounds As System.Drawing.Rectangle, ByVal rowIndex As Integer, ByVal cellState As System.Windows.Forms.DataGridViewElementStates, ByVal value As Object, ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal advancedBorderStyle As System.Windows.Forms.DataGridViewAdvancedBorderStyle, ByVal paintParts As System.Windows.Forms.DataGridViewPaintParts)
            MyBase.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)
            Dim tmpColumn As eProgressColumn = CType(Me.DataGridView.Columns(Me.ColumnIndex), eProgressColumn)
            Dim progressVal As Integer = Math.Min(value, 100)
            Dim sFormat As New StringFormat()
            Dim brLinear As LinearGradientBrush
            sFormat.LineAlignment = StringAlignment.Center
            sFormat.Alignment = StringAlignment.Center
            sFormat.FormatFlags = StringFormatFlags.NoWrap
            If progressVal > -1 Then

                g.FillRectangle(New SolidBrush(tmpColumn.BarBGColor), cellBounds.X + 3, cellBounds.Y + 3, cellBounds.Width - 6, cellBounds.Height - 6)

                brLinear = New LinearGradientBrush(New Point(0, cellBounds.Bottom), New Point(0, CInt(cellBounds.Y - 3 + cellBounds.Height / 2)), Color.FromArgb(188, 255, 255, 255), Color.FromArgb(0, 255, 255, 255))
                g.FillRectangle(brLinear, cellBounds.X + 3, CInt(cellBounds.Y + cellBounds.Height / 2), cellBounds.Width - 6, CInt((cellBounds.Height - 6) / 2))

                g.FillRectangle(New SolidBrush(tmpColumn.BarColor), cellBounds.X + 3, cellBounds.Y + 3, CInt((progressVal / 100 * cellBounds.Width - 6)), cellBounds.Height - 6)
                brLinear = New LinearGradientBrush(New Point(0, cellBounds.Bottom), New Point(0, CInt(cellBounds.Y - 3 + cellBounds.Height / 2)), tmpColumn.BarShineColor, Color.FromArgb(0, 255, 255, 255))
                g.FillRectangle(brLinear, cellBounds.X + 3, CInt(cellBounds.Y + cellBounds.Height / 2), CInt((progressVal / 100 * cellBounds.Width - 6)), CInt((cellBounds.Height - 6) / 2))

                brLinear = New LinearGradientBrush(New Point(0, cellBounds.Y), New Point(0, cellBounds.Bottom), Color.FromArgb(200, 255, 255, 255), Color.FromArgb(0, 255, 255, 255))
                g.FillRectangle(brLinear, cellBounds.X + 3, cellBounds.Y + 3, cellBounds.Width - 6, CInt((cellBounds.Height - 6) / 2))

                brLinear = New LinearGradientBrush(New Point(0, cellBounds.Y), New Point(0, cellBounds.Bottom), Color.FromArgb(178, 178, 178), Color.FromArgb(140, 140, 140))
                g.DrawLine(New Pen(brLinear), cellBounds.X + 2, cellBounds.Y + 3, cellBounds.X + 2, cellBounds.Bottom - 4)
                g.DrawLine(New Pen(brLinear), cellBounds.X + 3, cellBounds.Bottom - 3, cellBounds.Right - 4, cellBounds.Bottom - 3)
                g.DrawLine(New Pen(brLinear), cellBounds.Right - 3, cellBounds.Bottom - 4, cellBounds.Right - 3, cellBounds.Top + 3)
                g.DrawLine(New Pen(brLinear), cellBounds.Right - 4, cellBounds.Y + 2, cellBounds.X + 3, cellBounds.Y + 2)

                g.DrawString(progressVal & "%", cellStyle.Font, New SolidBrush(Color.FromArgb(70, 255, 255, 255)), New Rectangle(cellBounds.X + 1, cellBounds.Y + 1, cellBounds.Width, cellBounds.Height), sFormat)
                g.DrawString(progressVal & "%", cellStyle.Font, New SolidBrush(Color.FromArgb(70, 255, 255, 255)), New Rectangle(cellBounds.X - 1, cellBounds.Y + 1, cellBounds.Width, cellBounds.Height), sFormat)
                g.DrawString(progressVal & "%", cellStyle.Font, New SolidBrush(Color.FromArgb(70, 255, 255, 255)), New Rectangle(cellBounds.X + 1, cellBounds.Y - 1, cellBounds.Width, cellBounds.Height), sFormat)
                g.DrawString(progressVal & "%", cellStyle.Font, New SolidBrush(Color.FromArgb(70, 255, 255, 255)), New Rectangle(cellBounds.X - 1, cellBounds.Y - 1, cellBounds.Width, cellBounds.Height), sFormat)
                g.DrawString(progressVal & "%", cellStyle.Font, New SolidBrush(Color.Black), cellBounds, sFormat)
            End If
        End Sub
    End Class
End Namespace