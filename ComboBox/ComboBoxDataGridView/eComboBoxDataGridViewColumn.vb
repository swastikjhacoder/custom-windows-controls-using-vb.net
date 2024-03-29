Imports System.ComponentModel
Imports System.Windows.Forms

Namespace UIControls.ComboBox
    Public Class ComboBoxDataGridViewColumn
        Inherits DataGridViewTextBoxColumn

        Public Sub New()
            Dim cell As ComboBoxDataGridViewCell = New ComboBoxDataGridViewCell
            MyBase.CellTemplate = cell
            MyBase.SortMode = DataGridViewColumnSortMode.Automatic
        End Sub

        Private ReadOnly Property AccGridComboBoxDataGridViewCellTemplate() As ComboBoxDataGridViewCell
            Get
                Dim cell As ComboBoxDataGridViewCell = TryCast(Me.CellTemplate, ComboBoxDataGridViewCell)
                If cell Is Nothing Then Throw New InvalidOperationException( _
                    "DataGridViewAccGridComboBoxColumn does not have a CellTemplate.")
                Return cell
            End Get
        End Property

        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property CellTemplate() As System.Windows.Forms.DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridViewCell)
                Dim cell As ComboBoxDataGridViewCell = TryCast(value, ComboBoxDataGridViewCell)
                If Not value Is Nothing AndAlso cell Is Nothing Then Throw New InvalidCastException( _
                    "Value provided for CellTemplate must be of type DataGridViewAccTextBoxCell or derive from it.")
                MyBase.CellTemplate = value
            End Set
        End Property

        Private myDataGridView As ToolStripDataGridView = Nothing
        Public Property ComboDataGridView() As System.Windows.Forms.DataGridView
            Get
                If Not myDataGridView Is Nothing Then Return myDataGridView.DataGridViewControl
                Return Nothing
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridView)
                If Not value Is Nothing Then
                    myDataGridView = New ToolStripDataGridView(value, _CloseOnSingleClick)
                Else
                    myDataGridView = Nothing
                End If
            End Set
        End Property

        Private _ValueMember As String = ""
        Public Property ValueMember() As String
            Get
                Return _ValueMember
            End Get
            Set(ByVal value As String)
                _ValueMember = value
            End Set
        End Property

        Private _CloseOnSingleClick As Boolean = True
        Public Property CloseOnSingleClick() As Boolean
            Get
                Return _CloseOnSingleClick
            End Get
            Set(ByVal value As Boolean)
                _CloseOnSingleClick = value
                If Not myDataGridView Is Nothing Then myDataGridView.CloseOnSingleClick = value
            End Set
        End Property

        Private _InstantBinding As Boolean = True
        Public Property InstantBinding() As Boolean
            Get
                Return _InstantBinding
            End Get
            Set(ByVal value As Boolean)
                _InstantBinding = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return "DataGridViewAccGridComboBoxColumn{Name=" & Me.Name & ", Index=" & Me.Index.ToString & "}"
        End Function

        Friend Function GetToolStripDataGridView() As ToolStripDataGridView
            Return myDataGridView
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not myDataGridView Is Nothing _
                    AndAlso Not myDataGridView.DataGridViewControl Is Nothing _
                    AndAlso Not myDataGridView.DataGridViewControl.IsDisposed Then _
                    myDataGridView.DataGridViewControl.Dispose()
                If Not myDataGridView Is Nothing AndAlso Not myDataGridView.IsDisposed Then _
                    myDataGridView.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

    End Class
End Namespace