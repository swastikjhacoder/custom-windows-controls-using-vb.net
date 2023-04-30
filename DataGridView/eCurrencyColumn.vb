Imports System.Windows.Forms

Namespace UIControls.DataGridView
    Public Class eCurrencyColumn

        Inherits DataGridViewColumn

        Public Sub New()
            MyBase.New(New CurrencyCell())
        End Sub

        Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)
                If Not (value Is Nothing) AndAlso _
                    Not value.GetType().IsAssignableFrom(GetType(CurrencyCell)) _
                    Then
                    Throw New InvalidCastException("Must be a CurrencyCell")
                End If
                MyBase.CellTemplate = value
            End Set
        End Property
    End Class

    Public Class CurrencyCell
        Inherits DataGridViewTextBoxCell

        Public Sub New()
            Me.Style.Format = "$0.00"
        End Sub

        Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle As DataGridViewCellStyle)
            MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)
            Dim ctl As eCurrencyEditingControl = CType(DataGridView.EditingControl, eCurrencyEditingControl)
            ctl.Text = Me.Value
        End Sub

        Public Overrides ReadOnly Property EditType() As Type
            Get
                Return GetType(eCurrencyEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property ValueType() As Type
            Get
                Return GetType(Decimal)
            End Get
        End Property

        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            Get
                Return 0
            End Get
        End Property
    End Class

    Class eCurrencyEditingControl
        Inherits Windows.Forms.TextBox
        Implements IDataGridViewEditingControl
        Private dataGridViewControl As Windows.Forms.DataGridView
        Private valueIsChanged As Boolean = False
        Private rowIndexNum As Integer

        Public Sub New()

        End Sub

        Public Property Value() As Decimal
            Get
                If MyBase.Text = "" Then
                    Value = 0
                Else
                    Value = Decimal.Parse(Microsoft.VisualBasic.Right(MyBase.Text, Len(MyBase.Text) - 1))
                End If
            End Get
            Set(ByVal value As Decimal)
                String.Format(value, "$###,###,##0.00")
            End Set
        End Property

        Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            MyBase.OnKeyPress(e)
            Dim allowedChars As String = "0123456789$,."
            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                e.Handled = True
            End If
        End Sub

        Protected Overrides Sub OnLeave(ByVal e As System.EventArgs)
            MyBase.OnLeave(e)
            On Error GoTo txtCurrency_Leave_Err
            If MyBase.Text = "" Then
                MyBase.Text = "$0.00"
            Else
                If Microsoft.VisualBasic.Left(MyBase.Text, 1) = "$" Then
                    MyBase.Text = Microsoft.VisualBasic.Right(MyBase.Text, Len(MyBase.Text) - 1)
                End If
                String.Format(Decimal.Parse(MyBase.Text), "$###,###,##0.00")
            End If
            Exit Sub
txtCurrency_Leave_Err:
            Beep()
        End Sub

        Protected Overrides Sub OnValidating(ByVal e As System.ComponentModel.CancelEventArgs)
            MyBase.OnValidating(e)
            On Error GoTo txtCurrency_Validating_Err
            MyBase.Text = FormatCurrency(MyBase.Text, 2, TriState.True, TriState.False)
            Exit Sub
txtCurrency_Validating_Err:
            Beep()
            e.Cancel = True
        End Sub

        Public Property EditingControlFormattedValue() As Object Implements IDataGridViewEditingControl.EditingControlFormattedValue
            Get
                Return Me.Text
            End Get
            Set(ByVal value As Object)
                If TypeOf value Is [String] Then
                    Me.Text = value
                End If
            End Set
        End Property

        Public Function GetEditingControlFormattedValue(ByVal context As DataGridViewDataErrorContexts) As Object Implements IDataGridViewEditingControl.GetEditingControlFormattedValue
            Return Me.Text
        End Function

        Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As DataGridViewCellStyle) Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
            Me.Font = dataGridViewCellStyle.Font
            Me.ForeColor = dataGridViewCellStyle.ForeColor
            Me.BackColor = dataGridViewCellStyle.BackColor
        End Sub

        Public Property EditingControlRowIndex() As Integer Implements IDataGridViewEditingControl.EditingControlRowIndex
            Get
                Return rowIndexNum
            End Get
            Set(ByVal value As Integer)
                rowIndexNum = value
            End Set
        End Property

        Public Function EditingControlWantsInputKey(ByVal key As Keys, ByVal dataGridViewWantsInputKey As Boolean) As Boolean Implements IDataGridViewEditingControl.EditingControlWantsInputKey
            Select Case key And Keys.KeyCode
                Case Keys.Left, Keys.Up, Keys.Down, Keys.Right, _
                    Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp, Keys.OemPeriod, Keys.Decimal
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
            Implements IDataGridViewEditingControl.PrepareEditingControlForEdit
        End Sub

        Public ReadOnly Property RepositionEditingControlOnValueChange() As Boolean Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
            Get
                Return False
            End Get
        End Property

        Public Property EditingControlDataGridView() As System.Windows.Forms.DataGridView Implements IDataGridViewEditingControl.EditingControlDataGridView
            Get
                Return dataGridViewControl
            End Get
            Set(ByVal value As System.Windows.Forms.DataGridView)
                dataGridViewControl = value
            End Set
        End Property

        Public Property EditingControlValueChanged() As Boolean Implements IDataGridViewEditingControl.EditingControlValueChanged
            Get
                Return valueIsChanged
            End Get
            Set(ByVal value As Boolean)
                valueIsChanged = value
            End Set
        End Property

        Public ReadOnly Property EditingControlCursor() As Cursor Implements IDataGridViewEditingControl.EditingPanelCursor
            Get
                Return MyBase.Cursor
            End Get
        End Property

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            valueIsChanged = True
            Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
            MyBase.OnTextChanged(e)
        End Sub
    End Class
End Namespace