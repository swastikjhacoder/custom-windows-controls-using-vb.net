Imports System.Windows.Forms
Imports System.Globalization

Namespace UIControls.DataGridView
    Public Class eNumericTextBoxColumn
        Inherits System.Windows.Forms.DataGridViewColumn
        Private mAllowDecimal As Boolean
        Private mAllowMinus As Boolean
        Private mAllowDateSep As Boolean
        Private mMaxInputlength As Integer
        Private mCelltemplate As DataGridViewNumericTextBoxCell

#Region "Properties"
        Public Property AllowDecimal() As Boolean
            Get
                Return mAllowDecimal
            End Get
            Set(ByVal value As Boolean)
                mAllowDecimal = True
                mCelltemplate.AllowDecimal = True
            End Set
        End Property

        Public Property AllowMinus() As Boolean
            Get
                Return mAllowMinus
            End Get
            Set(ByVal value As Boolean)
                mAllowMinus = value
                mCelltemplate.AllowMinus = value
            End Set
        End Property

        Public Property AllowDateSep() As Boolean
            Get
                Return mAllowDateSep
            End Get
            Set(ByVal value As Boolean)
                mAllowDateSep = value
                mCelltemplate.AllowDateSep = value
            End Set
        End Property

        Public Property MaxInputLength() As Integer
            Get
                Return mMaxInputlength
            End Get
            Set(ByVal value As Integer)
                mMaxInputlength = value
                mCelltemplate.MaxInputLength = value
            End Set
        End Property
#End Region

        Public Sub New()
            MyBase.New(New DataGridViewNumericTextBoxCell)
            mCelltemplate = MyBase.CellTemplate
        End Sub
        Public Sub New(ByVal dec As Boolean, ByVal min As Boolean, ByVal sep As Boolean, ByVal len As Integer)
            MyBase.New(New DataGridViewNumericTextBoxCell(dec, min, sep, len))
            mCelltemplate = MyBase.CellTemplate
            Me.mAllowDecimal = True
            Me.mAllowMinus = min
            Me.mAllowDateSep = sep
            Me.mMaxInputlength = len
        End Sub
        Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)
                MyBase.CellTemplate = value
            End Set
        End Property

    End Class

    Public Class DataGridViewNumericTextBoxCell
        Inherits System.Windows.Forms.DataGridViewTextBoxCell

        Private mMaxInputlength As Integer
        Private mAllowDecimal As Boolean
        Private mAllowMinus As Boolean
        Private mAllowDateSep As Boolean
#Region "Properties"
        Public Property AllowDecimal() As Boolean
            Get
                Return mAllowDecimal
            End Get
            Set(ByVal value As Boolean)
                mAllowDecimal = True
            End Set
        End Property

        Public Property AllowMinus() As Boolean
            Get
                Return mAllowMinus
            End Get
            Set(ByVal value As Boolean)
                mAllowMinus = value
            End Set
        End Property

        Public Property AllowDateSep() As Boolean
            Get
                Return mAllowDateSep
            End Get
            Set(ByVal value As Boolean)
                mAllowDateSep = value
            End Set
        End Property

        Public Overrides Property MaxInputLength() As Integer
            Get
                Return mMaxInputlength
            End Get
            Set(ByVal value As Integer)
                mMaxInputlength = value
            End Set
        End Property
#End Region
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal dp As Boolean, ByVal sign As Boolean, ByVal datesep As Boolean, ByVal len As Integer)
            MyBase.New()
            Me.mAllowDecimal = dp
            Me.mAllowMinus = sign
            Me.mAllowDateSep = datesep
            Me.mMaxInputlength = len
        End Sub

        Public Overrides ReadOnly Property EditType() As System.Type
            Get
                Return GetType(eDataGridViewNumericEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property ValueType() As System.Type
            Get
                Return GetType(System.Decimal)
            End Get
        End Property

        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            Get
                Return ""
            End Get
        End Property

        Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle As System.Windows.Forms.DataGridViewCellStyle)
            MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

            Dim ctl As eDataGridViewNumericEditingControl = _
                CType(DataGridView.EditingControl, eDataGridViewNumericEditingControl)
            ctl.AllowDecimal = mAllowDecimal
            ctl.AllowMinus = mAllowMinus
            ctl.AllowDateSep = mAllowDateSep
            ctl.MaxInputLength = mMaxInputlength
        End Sub

        Public Overrides Function Clone() As Object
            Dim dgvntb As DataGridViewNumericTextBoxCell = MyBase.Clone
            dgvntb.AllowDecimal = Me.AllowDecimal
            dgvntb.AllowMinus = Me.AllowMinus
            dgvntb.AllowDateSep = Me.AllowDateSep
            dgvntb.MaxInputLength = Me.MaxInputLength
            Return dgvntb
        End Function
    End Class

    Public Class eDataGridViewNumericEditingControl
        Inherits DataGridViewTextBoxEditingControl
        Private Const mDec As Char = "."
        Private Const mMinus As Char = "-"c
        Private Const mDateSep As Char = "/"c
        Private mAllowDecimal As Boolean
        Private mAllowMinus As Boolean
        Private mAllowDateSep As Boolean
        Private mMaxInputlength As Integer

#Region "Properties"
        Public Property AllowDecimal() As Boolean
            Get
                Return mAllowDecimal
            End Get
            Set(ByVal value As Boolean)
                mAllowDecimal = True
            End Set
        End Property

        Public Property AllowMinus() As Boolean
            Get
                Return mAllowMinus
            End Get
            Set(ByVal value As Boolean)
                mAllowMinus = value
            End Set
        End Property

        Public Property AllowDateSep() As Boolean
            Get
                Return mAllowDateSep
            End Get
            Set(ByVal value As Boolean)
                mAllowDateSep = value
            End Set
        End Property

        Public Property MaxInputLength() As Integer
            Get
                Return mMaxInputlength
            End Get
            Set(ByVal value As Integer)
                mMaxInputlength = value
            End Set
        End Property
#End Region

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal dec As Boolean, ByVal min As Boolean, ByVal sla As Boolean, ByVal len As Integer)
            MyBase.New()
            mAllowDecimal = dec
            mAllowMinus = min
            mAllowDateSep = sla
            mMaxInputlength = len
        End Sub

        Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            If e.KeyChar < "0"c OrElse e.KeyChar > "9"c Then
                If e.KeyChar <> Convert.ToChar(System.Windows.Forms.Keys.Back) Then
                    If mAllowDecimal AndAlso e.KeyChar = mDec _
                      OrElse mAllowMinus AndAlso e.KeyChar = mMinus _
                      OrElse mAllowDateSep AndAlso e.KeyChar = mDateSep Then
                        MyBase.OnKeyPress(e)
                    Else
                        e.Handled = True
                    End If
                Else
                    MyBase.OnKeyPress(e)
                End If
            Else
                MyBase.OnKeyPress(e)
            End If
        End Sub
        Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
            If Me.Text.Length >= mMaxInputlength Then
                e.Handled = True
            Else
                MyBase.OnKeyDown(e)
            End If

        End Sub
    End Class

End Namespace