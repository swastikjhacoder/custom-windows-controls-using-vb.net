
Imports System.Data
Imports System.Linq
Imports System.Windows.Forms

Namespace UIControls.DataGridView
    Partial Public Class eDataGridViewWithPaging
        Inherits UserControl
        Private _CurrentPage As Integer = 1

        Private _FirstButtonText As String = String.Empty
        Public Property FirstButtonText() As String
            Get
                If _FirstButtonText = String.Empty Then
                    Return btnFirst.Text
                Else
                    Return _FirstButtonText
                End If
            End Get
            Set(ByVal value As String)
                _FirstButtonText = value
                btnFirst.Text = _FirstButtonText
            End Set
        End Property

        Private _LastButtonText As String = String.Empty
        Public Property LastButtonText() As String
            Get
                If _LastButtonText = String.Empty Then
                    Return btnLast.Text
                Else
                    Return _LastButtonText
                End If
            End Get
            Set(ByVal value As String)
                _LastButtonText = value
                btnLast.Text = _LastButtonText
            End Set
        End Property

        Private _PreviousButtonText As String = String.Empty
        Public Property PreviousButtonText() As String
            Get
                If _PreviousButtonText = String.Empty Then
                    Return btnPrevious.Text
                Else
                    Return _PreviousButtonText
                End If
            End Get
            Set(ByVal value As String)
                _PreviousButtonText = value
                btnPrevious.Text = _PreviousButtonText
            End Set
        End Property

        Private _NextButtonText As String = String.Empty
        Public Property NextButtonText() As String
            Get
                If _NextButtonText = String.Empty Then
                    Return btnNext.Text
                Else
                    Return _NextButtonText
                End If
            End Get
            Set(ByVal value As String)
                _NextButtonText = value
                btnNext.Text = _NextButtonText
            End Set
        End Property

        Private _Width As Integer
        Public Property ControlWidth() As Integer
            Get
                If _Width = 0 Then
                    Return dataGridView1.Width
                Else
                    Return _Width
                End If
            End Get
            Set(ByVal value As Integer)
                _Width = value
                dataGridView1.Width = _Width
            End Set
        End Property

        Private _Height As Integer
        Public Property ControlHeight() As Integer
            Get
                If _Height = 0 Then
                    Return dataGridView1.Height
                Else
                    Return _Height
                End If
            End Get
            Set(ByVal value As Integer)
                _Height = value
                dataGridView1.Height = _Height
            End Set
        End Property

        Private _PateSize As Integer = 10
        Public Property PageSize() As Integer
            Get
                Return _PateSize
            End Get
            Set(ByVal value As Integer)
                _PateSize = value
            End Set
        End Property

        Private _DataSource As DataTable
        Public Property DataSource() As DataTable
            Get
                Return _DataSource
            End Get
            Set(ByVal value As DataTable)
                _DataSource = value
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If _CurrentPage = 1 Then
                MessageBox.Show("You are already on First Page.")
            Else
                _CurrentPage = 1
                dataGridView1.DataSource = ShowData(_CurrentPage)
            End If
        End Sub

        Private Sub btnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If _CurrentPage = 1 Then
                MessageBox.Show("You are already on First page, you can not go to previous of First page.")
            Else
                _CurrentPage -= 1
                dataGridView1.DataSource = ShowData(_CurrentPage)
            End If
        End Sub

        Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim lastPage As Integer = (DataSource.Rows.Count / PageSize) + 1
            If _CurrentPage = lastPage Then
                MessageBox.Show("You are already on Last page, you can not go to next page of Last page.")
            Else
                _CurrentPage += 1
                dataGridView1.DataSource = ShowData(_CurrentPage)
            End If
        End Sub

        Private Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim previousPage As Integer = _CurrentPage
            _CurrentPage = (DataSource.Rows.Count / PageSize) + 1

            If previousPage = _CurrentPage Then
                MessageBox.Show("You are already on Last Page.")
            Else
                dataGridView1.DataSource = ShowData(_CurrentPage)
            End If
        End Sub

        Public Sub DataBind(ByVal dataTable As DataTable)
            DataSource = dataTable
            dataGridView1.DataSource = ShowData(1)
        End Sub

        Private Function ShowData(ByVal pageNumber As Integer) As DataTable
            Dim dt As New DataTable()
            Dim startIndex As Integer = PageSize * (pageNumber - 1)
            Dim result = DataSource.AsEnumerable().Where(Function(s, k) (k >= startIndex AndAlso k < (startIndex + PageSize)))

            For Each colunm As DataColumn In DataSource.Columns
                dt.Columns.Add(colunm.ColumnName)
            Next

            For Each item As Object In result
                dt.ImportRow(item)
            Next

            txtPaging.Text = String.Format("Page {0} Of {1} Pages", pageNumber, (DataSource.Rows.Count / PageSize) + 1)
            Return dt
        End Function
    End Class
End Namespace