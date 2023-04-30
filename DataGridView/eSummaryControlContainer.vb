
Imports System.Data
Imports System.Collections
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Generic


Namespace UIControls.DataGridView

    Public Class eSummaryControlContainer
        Inherits Control

#Region "Declare variables"
        Private sumBoxHash As Hashtable
        Private dgv As eDataGridViewSummary
        Private sumRowHeaderLabel As Windows.Forms.Label


        Private m_initialHeight As Integer
        Public Property InitialHeight() As Integer
            Get
                Return m_initialHeight
            End Get
            Set(ByVal value As Integer)
                m_initialHeight = value
            End Set
        End Property

        Private m_lastVisibleState As Boolean
        Public Property LastVisibleState() As Boolean
            Get
                Return m_lastVisibleState
            End Get
            Set(ByVal value As Boolean)
                m_lastVisibleState = value
            End Set
        End Property

        Private m_summaryRowBackColor As Color
        Public Property SummaryRowBackColor() As Color
            Get
                Return m_summaryRowBackColor
            End Get
            Set(ByVal value As Color)
                m_summaryRowBackColor = value
            End Set
        End Property


        ''' <summary>
        ''' Event is raised when visibility changes and the
        ''' lastVisibleState is not the new visible state
        ''' </summary>
        Public Event VisibilityChanged As EventHandler

#End Region

#Region "Constructors"

        Public Sub New(ByVal dgv As eDataGridViewSummary)
            If dgv Is Nothing Then
                Throw New Exception("DataGridView is null!")
            End If

            Me.dgv = dgv

            sumBoxHash = New Hashtable()
            sumRowHeaderLabel = New Windows.Forms.Label()


            AddHandler Me.dgv.CreateSummary, AddressOf dgv_CreateSummary
            AddHandler Me.dgv.RowsAdded, AddressOf dgv_RowsAdded
            AddHandler Me.dgv.RowsRemoved, AddressOf dgv_RowsRemoved
            AddHandler Me.dgv.CellValueChanged, AddressOf dgv_CellValueChanged

            AddHandler Me.dgv.Scroll, AddressOf dgv_Scroll
            AddHandler Me.dgv.ColumnWidthChanged, AddressOf dgv_ColumnWidthChanged
            AddHandler Me.dgv.RowHeadersWidthChanged, AddressOf dgv_RowHeadersWidthChanged
            AddHandler Me.VisibleChanged, AddressOf SummaryControlContainer_VisibleChanged

            AddHandler Me.dgv.ColumnAdded, AddressOf dgv_ColumnAdded
            AddHandler Me.dgv.ColumnRemoved, AddressOf dgv_ColumnRemoved
            AddHandler Me.dgv.ColumnStateChanged, AddressOf dgv_ColumnStateChanged

            AddHandler Me.dgv.ColumnDisplayIndexChanged, AddressOf dgv_ColumnDisplayIndexChanged
        End Sub

        Private Sub dgv_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            'resizeSumBoxes();
            reCreateSumBoxes()
        End Sub

        Private Sub dgv_ColumnStateChanged(ByVal sender As Object, ByVal e As DataGridViewColumnStateChangedEventArgs)
            resizeSumBoxes()
        End Sub

        Private Sub dgv_ColumnRemoved(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            reCreateSumBoxes()
        End Sub

        Private Sub dgv_ColumnAdded(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            reCreateSumBoxes()
        End Sub

        Private Sub dgv_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

            Dim roTextBox As eReadOnlyTextBox = DirectCast(sumBoxHash(dgv.Columns(e.ColumnIndex)), eReadOnlyTextBox)
            If roTextBox IsNot Nothing Then
                If roTextBox.IsSummary Then
                    calcSummaries()
                End If
            End If
        End Sub

        Private Sub dgv_RowsAdded(ByVal sender As Object, ByVal e As DataGridViewRowsAddedEventArgs)
            calcSummaries()
        End Sub

        Private Sub dgv_RowsRemoved(ByVal sender As Object, ByVal e As DataGridViewRowsRemovedEventArgs)
            calcSummaries()
        End Sub

        Private Sub SummaryControlContainer_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
            If m_lastVisibleState <> Me.Visible Then
                OnVisiblityChanged(sender, e)
            End If
        End Sub

        Protected Sub OnVisiblityChanged(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent VisibilityChanged(sender, e)

            m_lastVisibleState = Me.Visible
        End Sub

#End Region

#Region "Events and delegates"

        Private Sub dgv_CreateSummary(ByVal sender As Object, ByVal e As EventArgs)
            reCreateSumBoxes()
            calcSummaries()
        End Sub

        Private Sub dgv_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
            resizeSumBoxes()
        End Sub

        Private Sub dgv_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            resizeSumBoxes()
        End Sub

        Private Sub dgv_RowHeadersWidthChanged(ByVal sender As Object, ByVal e As EventArgs)
            resizeSumBoxes()
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            MyBase.OnResize(e)
            resizeSumBoxes()
        End Sub

        Private Sub dgv_Resize(ByVal sender As Object, ByVal e As EventArgs)
            resizeSumBoxes()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Checks if passed object is of type of integer
        ''' </summary>
        ''' <param name="o">object</param>
        ''' <returns>true/ false</returns>
        Protected Function IsInteger(ByVal o As Object) As Boolean
            If TypeOf o Is Int64 Then
                Return True
            End If
            If TypeOf o Is Int32 Then
                Return True
            End If
            If TypeOf o Is Int16 Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' Checks if passed object is of type of decimal/ double
        ''' </summary>
        ''' <param name="o">object</param>
        ''' <returns>true/ false</returns>
        Protected Function IsDecimal(ByVal o As Object) As Boolean
            If TypeOf o Is [Decimal] Then
                Return True
            End If
            If TypeOf o Is [Single] Then
                Return True
            End If
            If TypeOf o Is [Double] Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' Enable manual refresh of the SummaryDataGridView
        ''' </summary>
        Friend Sub RefreshSummary()
            calcSummaries()
        End Sub

        ''' <summary>
        ''' Calculate the Sums of the summary columns
        ''' </summary>
        Private Sub calcSummaries()
            For Each roTextBox As eReadOnlyTextBox In sumBoxHash.Values
                If roTextBox.IsSummary Then
                    roTextBox.Tag = 0
                    roTextBox.Text = "0"
                    roTextBox.Invalidate()
                End If
            Next
            If dgv.SummaryColumns IsNot Nothing AndAlso dgv.SummaryColumns.Length > 0 AndAlso sumBoxHash.Count > 0 Then
                For Each dgvRow As DataGridViewRow In dgv.Rows
                    For Each dgvCell As DataGridViewCell In dgvRow.Cells
                        For Each dgvColumn As DataGridViewColumn In sumBoxHash.Keys
                            If dgvCell.OwningColumn.Equals(dgvColumn) Then
                                Dim sumBox As eReadOnlyTextBox = DirectCast(sumBoxHash(dgvColumn), eReadOnlyTextBox)

                                If sumBox IsNot Nothing AndAlso sumBox.IsSummary Then
                                    If dgvCell.Value IsNot Nothing AndAlso Not (TypeOf dgvCell.Value Is DBNull) Then
                                        If IsInteger(dgvCell.Value) Then
                                            sumBox.Tag = Convert.ToInt64(sumBox.Tag) + Convert.ToInt64(dgvCell.Value)
                                        ElseIf IsDecimal(dgvCell.Value) Then
                                            sumBox.Tag = Convert.ToDecimal(sumBox.Tag) + Convert.ToDecimal(dgvCell.Value)
                                        End If

                                        sumBox.Text = String.Format("{0}", sumBox.Tag)
                                        sumBox.Invalidate()
                                    End If
                                End If
                            End If
                        Next
                    Next
                Next
            End If
        End Sub

        ''' <summary>
        ''' Create summary boxes for each Column of the DataGridView        
        ''' </summary>
        Private Sub reCreateSumBoxes()
            Dim sumBox As eReadOnlyTextBox

            For Each control As Control In sumBoxHash.Values
                Me.Controls.Remove(control)
            Next
            sumBoxHash.Clear()

            Dim iCnt As Integer = 0

            Dim sortedColumns__1 As List(Of DataGridViewColumn) = SortedColumns
            For Each dgvColumn As DataGridViewColumn In sortedColumns__1
                sumBox = New eReadOnlyTextBox()
                sumBoxHash.Add(dgvColumn, sumBox)

                sumBox.Top = 0
                sumBox.Height = dgv.RowTemplate.Height
                sumBox.BorderColor = dgv.GridColor
                If m_summaryRowBackColor = Color.Empty Then
                    sumBox.BackColor = dgv.DefaultCellStyle.BackColor
                Else
                    sumBox.BackColor = m_summaryRowBackColor
                End If
                sumBox.BringToFront()

                If dgv.ColumnCount - iCnt = 1 Then
                    sumBox.IsLastColumn = True
                End If

                If dgv.SummaryColumns IsNot Nothing AndAlso dgv.SummaryColumns.Length > 0 Then
                    For iCntX As Integer = 0 To dgv.SummaryColumns.Length - 1
                        If dgv.SummaryColumns(iCntX) = dgvColumn.DataPropertyName OrElse dgv.SummaryColumns(iCntX) = dgvColumn.Name Then
                            dgvColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                            dgvColumn.CellTemplate.Style.Format = dgv.FormatString
                            sumBox.TextAlign = TextHelper.TranslateGridColumnAligment(dgvColumn.DefaultCellStyle.Alignment)
                            sumBox.IsSummary = True

                            sumBox.FormatString = dgvColumn.CellTemplate.Style.Format
                            If dgvColumn.ValueType Is GetType(System.Int32) OrElse dgvColumn.ValueType Is GetType(System.Int16) OrElse dgvColumn.ValueType Is GetType(System.Int64) OrElse dgvColumn.ValueType Is GetType(System.Single) OrElse dgvColumn.ValueType Is GetType(System.Double) OrElse dgvColumn.ValueType Is GetType(System.Single) OrElse dgvColumn.ValueType Is GetType(System.Decimal) Then
                                sumBox.Tag = System.Activator.CreateInstance(dgvColumn.ValueType)
                            End If
                        End If
                    Next
                End If

                sumBox.BringToFront()
                Me.Controls.Add(sumBox)

                iCnt += 1
            Next

            If dgv.DisplaySumRowHeader Then
                sumRowHeaderLabel.Font = New Font(dgv.DefaultCellStyle.Font, If(dgv.SumRowHeaderTextBold, FontStyle.Bold, FontStyle.Regular))
                sumRowHeaderLabel.Anchor = AnchorStyles.Left
                sumRowHeaderLabel.TextAlign = ContentAlignment.MiddleLeft
                sumRowHeaderLabel.Height = sumRowHeaderLabel.Font.Height
                sumRowHeaderLabel.Top = Convert.ToInt32(Convert.ToDouble(Me.InitialHeight - sumRowHeaderLabel.Height) / 2.0F)
                sumRowHeaderLabel.Text = dgv.SumRowHeaderText

                sumRowHeaderLabel.ForeColor = dgv.DefaultCellStyle.ForeColor
                sumRowHeaderLabel.Margin = New Padding(0)
                sumRowHeaderLabel.Padding = New Padding(0)

                Me.Controls.Add(sumRowHeaderLabel)
            End If
            calcSummaries()
            resizeSumBoxes()
        End Sub

        ''' <summary>
        ''' Order the columns in the way they are displayed
        ''' </summary>
        Private ReadOnly Property SortedColumns() As List(Of DataGridViewColumn)
            Get
                Dim result As New List(Of DataGridViewColumn)()
                Dim column As DataGridViewColumn = dgv.Columns.GetFirstColumn(DataGridViewElementStates.None)
                If column Is Nothing Then
                    Return result
                End If
                result.Add(column)
                While (InlineAssignHelper(column, dgv.Columns.GetNextColumn(column, DataGridViewElementStates.None, DataGridViewElementStates.None))) IsNot Nothing
                    result.Add(column)
                End While

                Return result
            End Get
        End Property

        ''' <summary>
        ''' Resize the summary Boxes depending on the 
        ''' width of the Columns of the DataGridView
        ''' </summary>
        Private Sub resizeSumBoxes()
            Me.SuspendLayout()
            If sumBoxHash.Count > 0 Then
                Try
                    Dim rowHeaderWidth As Integer = If(dgv.RowHeadersVisible, dgv.RowHeadersWidth - 1, 0)
                    Dim sumLabelWidth As Integer = If(dgv.RowHeadersVisible, dgv.RowHeadersWidth - 1, 0)
                    Dim curPos As Integer = rowHeaderWidth

                    If dgv.DisplaySumRowHeader AndAlso sumLabelWidth > 0 Then
                        If dgv.RightToLeft = RightToLeft.Yes Then
                            If sumRowHeaderLabel.Dock <> DockStyle.Right Then
                                sumRowHeaderLabel.Dock = DockStyle.Right
                            End If
                        Else
                            If sumRowHeaderLabel.Dock <> DockStyle.Left Then
                                sumRowHeaderLabel.Dock = DockStyle.Left

                            End If
                        End If
                    Else
                        If sumRowHeaderLabel.Visible Then
                            sumRowHeaderLabel.Visible = False
                        End If
                    End If

                    Dim iCnt As Integer = 0
                    Dim oldBounds As Rectangle

                    For Each dgvColumn As DataGridViewColumn In SortedColumns
                        'dgv.Columns)
                        Dim sumBox As eReadOnlyTextBox = DirectCast(sumBoxHash(dgvColumn), eReadOnlyTextBox)


                        If sumBox IsNot Nothing Then
                            oldBounds = sumBox.Bounds
                            If Not dgvColumn.Visible Then
                                sumBox.Visible = False
                                Continue For
                            End If

                            Dim from As Integer = curPos - dgv.HorizontalScrollingOffset

                            Dim width As Integer = dgvColumn.Width + (If(iCnt = 0, 0, 0))

                            If from < rowHeaderWidth Then
                                width -= rowHeaderWidth - from
                                from = rowHeaderWidth
                            End If

                            If from + width > Me.Width Then
                                width = Me.Width - from
                            End If

                            If width < 4 Then
                                If sumBox.Visible Then
                                    sumBox.Visible = False
                                End If
                            Else
                                If Me.RightToLeft = RightToLeft.Yes Then
                                    from = Me.Width - from - width
                                End If


                                If sumBox.Left <> from OrElse sumBox.Width <> width Then
                                    sumBox.SetBounds(from, 0, width, 0, BoundsSpecified.X Or BoundsSpecified.Width)
                                End If

                                If Not sumBox.Visible Then
                                    sumBox.Visible = True
                                End If
                            End If

                            curPos += dgvColumn.Width + (If(iCnt = 0, 0, 0))
                            If oldBounds <> sumBox.Bounds Then
                                sumBox.Invalidate()

                            End If
                        End If
                        iCnt += 1
                    Next
                Finally
                    Me.ResumeLayout()
                End Try
            End If
        End Sub
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function

#End Region
    End Class
End Namespace