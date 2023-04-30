Imports System.ComponentModel
Imports System.Windows.Forms

Namespace UIControls.ComboBox
    Public Class ComboBoxDataGridViewCell
        Inherits DataGridViewTextBoxCell

        Friend ReadOnly Property EditingAccGridComboBox() As eComboBoxDataGridView
            Get
                Return TryCast(Me.DataGridView.EditingControl, eComboBoxDataGridViewEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property EditType() As Type
            Get
                Return GetType(eComboBoxDataGridViewEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property ValueType() As Type
            Get
                Return GetType(Object)
            End Get
        End Property

        Public Overrides Sub InitializeEditingControl(ByVal nRowIndex As Integer, _
            ByVal nInitialFormattedValue As Object, ByVal nDataGridViewCellStyle As DataGridViewCellStyle)

            MyBase.InitializeEditingControl(nRowIndex, nInitialFormattedValue, nDataGridViewCellStyle)

            Dim cEditBox As eComboBoxDataGridView = TryCast(Me.DataGridView.EditingControl, eComboBoxDataGridView)

            If cEditBox IsNot Nothing Then

                If Not MyBase.OwningColumn Is Nothing AndAlso Not DirectCast(MyBase.OwningColumn,  _
                    ComboBoxDataGridViewColumn).ComboDataGridView Is Nothing Then

                    cEditBox.AddToolStripDataGridView(DirectCast(MyBase.OwningColumn,  _
                        ComboBoxDataGridViewColumn).GetToolStripDataGridView)
                    cEditBox.ValueMember = DirectCast(MyBase.OwningColumn, ComboBoxDataGridViewColumn).ValueMember
                    cEditBox.InstantBinding = DirectCast(MyBase.OwningColumn, ComboBoxDataGridViewColumn).InstantBinding

                End If

                Try
                    cEditBox.SelectedValue = Value
                Catch ex As Exception
                    cEditBox.SelectedValue = Nothing
                End Try

            End If

        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Overrides Sub DetachEditingControl()
            Dim cDataGridView As Windows.Forms.DataGridView = Me.DataGridView
            If cDataGridView Is Nothing OrElse cDataGridView.EditingControl Is Nothing Then
                Throw New InvalidOperationException("Cell is detached or its grid has no editing control.")
            End If

            Dim EditBox As eComboBoxDataGridView = TryCast(cDataGridView.EditingControl, eComboBoxDataGridView)
            If EditBox IsNot Nothing Then
                ' avoid interferences between the editing sessions
                'EditBox.ClearUndo()
            End If

            MyBase.DetachEditingControl()
        End Sub

        Public Overrides Function ToString() As String
            Return "DataGridViewAccGridComboBoxCell{ColIndex=" & Me.ColumnIndex.ToString & _
                ", RowIndex=" & Me.RowIndex.ToString & "}"
        End Function

        Private Sub OnCommonChange()
            If Not Me.DataGridView Is Nothing AndAlso Not Me.DataGridView.IsDisposed _
                AndAlso Not Me.DataGridView.Disposing Then
                If Me.RowIndex = -1 Then
                    Me.DataGridView.InvalidateColumn(Me.ColumnIndex)
                Else
                    Me.DataGridView.UpdateCellValue(Me.ColumnIndex, Me.RowIndex)
                End If
            End If
        End Sub

        Private Function OwnsEditingControl(ByVal nRowIndex As Integer) As Boolean
            If nRowIndex = -1 OrElse Me.DataGridView Is Nothing OrElse _
                Me.DataGridView.IsDisposed OrElse Me.DataGridView.Disposing Then Return False

            Dim cEditingControl As DataGridViewComboBoxEditingControl = _
                TryCast(Me.DataGridView.EditingControl, DataGridViewComboBoxEditingControl)
            Return (cEditingControl IsNot Nothing AndAlso _
                nRowIndex = DirectCast(cEditingControl, IDataGridViewEditingControl).EditingControlRowIndex)
        End Function

        Protected Overrides Function SetValue(ByVal rowIndex As Integer, ByVal value As Object) As Boolean
            If Not Me.DataGridView Is Nothing AndAlso Not Me.DataGridView.EditingControl Is Nothing _
                AndAlso TypeOf Me.DataGridView.EditingControl Is eComboBoxDataGridView Then
                Return MyBase.SetValue(rowIndex, DirectCast(Me.DataGridView.EditingControl,  _
                    eComboBoxDataGridView).SelectedValue)
            Else
                Return MyBase.SetValue(rowIndex, value)
            End If
        End Function

    End Class
End Namespace