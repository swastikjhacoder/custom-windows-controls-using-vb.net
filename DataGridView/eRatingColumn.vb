
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Namespace UIControls.DataGridView
    Public Class eRatingColumn
        Inherits DataGridViewImageColumn
        Public Sub New()
            Me.CellTemplate = New RatingCell()
            Me.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.ValueType = GetType(Integer)
        End Sub
    End Class

    Public Class RatingCell
        Inherits DataGridViewImageCell
        Public Sub New()
            ' Value type is an integer.
            ' Formatted value type is an image since we derive from the ImageCell
            Me.ValueType = GetType(Integer)
        End Sub

        Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, ByVal valueTypeConverter As TypeConverter, ByVal formattedValueTypeConverter As TypeConverter, ByVal context As DataGridViewDataErrorContexts) As Object
            ' Convert integer to star images
            Return starImages
        End Function

        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            ' default new row to 3 stars
            Get
                Return 3
            End Get
        End Property

        Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal elementState As DataGridViewElementStates, ByVal value As Object, _
         ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, ByVal paintParts As DataGridViewPaintParts)
            Dim cellImage As Image = DirectCast(formattedValue, Image)

            Dim starNumber As Integer = GetStarFromMouse(cellBounds, Me.DataGridView.PointToClient(Control.MousePosition))

            If starNumber <> -1 Then
                cellImage = starHotImages
            End If
            ' starImages starHotImages
            ' surpress painting of selection
            MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, _
             cellImage, errorText, cellStyle, advancedBorderStyle, (paintParts And Not DataGridViewPaintParts.SelectionBackground))
        End Sub

        ' Update cell's value when the user clicks on a star
        Protected Overrides Sub OnContentClick(ByVal e As DataGridViewCellEventArgs)
            MyBase.OnContentClick(e)
            Dim starNumber As Integer = GetStarFromMouse(Me.DataGridView.GetCellDisplayRectangle(Me.DataGridView.CurrentCellAddress.X, Me.DataGridView.CurrentCellAddress.Y, False), Me.DataGridView.PointToClient(Control.MousePosition))

            If starNumber <> -1 Then
                Me.Value = starNumber
            End If
        End Sub

#Region "Invalidate cells when mouse moves or leaves the cell"
        Protected Overrides Sub OnMouseLeave(ByVal rowIndex As Integer)
            MyBase.OnMouseLeave(rowIndex)
            Me.DataGridView.InvalidateCell(Me)
        End Sub
        Protected Overrides Sub OnMouseMove(ByVal e As DataGridViewCellMouseEventArgs)
            MyBase.OnMouseMove(e)
            Me.DataGridView.InvalidateCell(Me)
        End Sub
#End Region

#Region "Private Implementation"
        Shared starImages As Image
        Shared starHotImages As Image
        Const IMAGEWIDTH As Integer = 58

        Private Function GetStarFromMouse(ByVal cellBounds As Rectangle, ByVal mouseLocation As Point) As Integer
            If cellBounds.Contains(mouseLocation) Then

                Dim mouseXRelativeToCell As Integer = (mouseLocation.X - cellBounds.X)
                Dim imageXArea As Integer = (cellBounds.Width / 2) - (IMAGEWIDTH / 2)
                If ((mouseXRelativeToCell + 4) < imageXArea) OrElse (mouseXRelativeToCell >= (imageXArea + IMAGEWIDTH)) Then
                    Return -1
                Else
                    Dim oo As Integer = CInt(Math.Round(((CSng(mouseXRelativeToCell - imageXArea + 5) / CSng(IMAGEWIDTH)) * 5.0F), MidpointRounding.AwayFromZero))
                    If oo > 5 OrElse oo < 0 Then
                        System.Diagnostics.Debugger.Break()
                    End If
                    Return oo
                End If
            Else
                Return -1
            End If
        End Function
        ' setup star images
#Region "Load star images"
        Shared Sub New()
            ' load normal stars
            starImages = Global.ESAR_Controls.My.Resources.starone
            starHotImages = Global.ESAR_Controls.My.Resources.starhotone
        End Sub
#End Region

#End Region

    End Class
End Namespace