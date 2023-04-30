Imports System.Windows.Forms

Namespace UIControls.ComboBox
    Public Class ToolStripDataGridView
        Inherits ToolStripControlHost

        Private _CloseOnSingleClick As Boolean = True
        Private _MinDropDownWidth As Integer
        Private _DropDownHeight As Integer

        ' Call the base constructor passing in a MonthCalendar instance.
        Public Sub New(ByVal nDataGridView As Windows.Forms.DataGridView, ByVal nCloseOnSingleClick As Boolean)
            MyBase.New(nDataGridView)
            Me.AutoSize = False
            Me._MinDropDownWidth = nDataGridView.Width
            Me._CloseOnSingleClick = nCloseOnSingleClick
            Me._DropDownHeight = nDataGridView.Height
        End Sub

        Public Property CloseOnSingleClick() As Boolean
            Get
                Return _CloseOnSingleClick
            End Get
            Set(ByVal value As Boolean)
                _CloseOnSingleClick = value
            End Set
        End Property

        Public ReadOnly Property DataGridViewControl() As System.Windows.Forms.DataGridView
            Get
                Return TryCast(Control, System.Windows.Forms.DataGridView)
            End Get
        End Property

        Public ReadOnly Property MinDropDownWidth() As Integer
            Get
                Return _MinDropDownWidth
            End Get
        End Property

        Public ReadOnly Property DropDownHeight() As Integer
            Get
                Return _DropDownHeight
            End Get
        End Property

        Private Sub OnDataGridViewCellMouseEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
            If Not e.RowIndex < 0 AndAlso DataGridViewControl.Rows(e.RowIndex).Displayed Then _
                DataGridViewControl.CurrentCell = DataGridViewControl.Item(0, e.RowIndex)
            If Not DataGridViewControl.Focused Then DataGridViewControl.Focus()
        End Sub

        Private Sub OnDataGridViewKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If e.KeyCode = Keys.Enter Then
                DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
                e.Handled = True
            End If
        End Sub

        Private Sub myDataGridView_DoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
            DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
        End Sub

        Private Sub myDataGridView_Click(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
            If _CloseOnSingleClick Then DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
        End Sub

        ' Subscribe and unsubscribe the control events you wish to expose.
        Protected Overrides Sub OnSubscribeControlEvents(ByVal c As Control)
            ' Call the base so the base events are connected.
            MyBase.OnSubscribeControlEvents(c)

            ' Cast the control to a MonthCalendar control.
            Dim nDataGridView As Windows.Forms.DataGridView = DirectCast(c, Windows.Forms.DataGridView)

            ' Add the event.
            AddHandler nDataGridView.CellMouseEnter, AddressOf OnDataGridViewCellMouseEnter
            AddHandler nDataGridView.KeyDown, AddressOf OnDataGridViewKeyDown
            AddHandler nDataGridView.CellDoubleClick, AddressOf myDataGridView_DoubleClick
            AddHandler nDataGridView.CellClick, AddressOf myDataGridView_Click

        End Sub

        Protected Overrides Sub OnUnsubscribeControlEvents(ByVal c As Control)
            ' Call the base method so the basic events are unsubscribed.
            MyBase.OnUnsubscribeControlEvents(c)

            ' Cast the control to a MonthCalendar control.
            Dim nDataGridView As Windows.Forms.DataGridView = DirectCast(c, Windows.Forms.DataGridView)

            ' Remove the event.
            RemoveHandler nDataGridView.CellMouseEnter, AddressOf OnDataGridViewCellMouseEnter
            RemoveHandler nDataGridView.KeyDown, AddressOf OnDataGridViewKeyDown
            RemoveHandler nDataGridView.CellDoubleClick, AddressOf myDataGridView_DoubleClick
            RemoveHandler nDataGridView.CellClick, AddressOf myDataGridView_Click

        End Sub

        Protected Overrides Sub OnBoundsChanged()
            MyBase.OnBoundsChanged()
            If Not Control Is Nothing Then
                DirectCast(Control, Windows.Forms.DataGridView).Size = Me.Size
                DirectCast(Control, Windows.Forms.DataGridView).AutoResizeColumns()
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not Control Is Nothing AndAlso Not DirectCast(Control, Windows.Forms.DataGridView).IsDisposed Then Control.Dispose()
        End Sub

    End Class
End Namespace