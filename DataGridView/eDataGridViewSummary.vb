
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms


Namespace UIControls.DataGridView
    ''' <summary>
    ''' Todo. Add RightToLeft Support for ReadOnlyTextbox
    ''' </summary>    
    Partial Public Class eDataGridViewSummary
        Inherits Windows.Forms.DataGridView
        Implements ISupportInitialize

#Region "Enter Functions..."
        Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
            Try
                Dim key As Keys = (keyData And Keys.KeyCode)
                If key = Keys.Enter Then
                    Return Me.ProcessRightKey(keyData)
                End If
                Return MyBase.ProcessDialogKey(keyData)
            Catch ex As Exception

            End Try
        End Function

        Public Shadows Function ProcessRightKey(ByVal keyData As Keys) As Boolean
            Try
                Dim key As Keys = (keyData And Keys.KeyCode)
                If key = Keys.Enter Then
                    If (MyBase.CurrentCell.ColumnIndex = (MyBase.ColumnCount - 1)) AndAlso (MyBase.CurrentCell.RowIndex = (MyBase.RowCount - 1)) Then
                        'This causes the last cell to be checked for errors
                        MyBase.EndEdit()
                        DirectCast(MyBase.DataSource, BindingSource).AddNew()
                        MyBase.CurrentCell = MyBase.Rows(MyBase.RowCount - 1).Cells(0)
                        Return True
                    End If
                    If (MyBase.CurrentCell.ColumnIndex = (MyBase.ColumnCount - 1)) AndAlso (MyBase.CurrentCell.RowIndex + 1 <> MyBase.NewRowIndex) Then
                        MyBase.CurrentCell = MyBase.Rows(MyBase.CurrentCell.RowIndex + 1).Cells(0)
                        Return True
                    End If
                    Return MyBase.ProcessRightKey(keyData)
                End If
                Return MyBase.ProcessRightKey(keyData)
            Catch ex As Exception

            End Try
        End Function

        Protected Overrides Function ProcessDataGridViewKey(ByVal e As KeyEventArgs) As Boolean
            Try
                If e.KeyCode = Keys.Enter Then
                    Return Me.ProcessRightKey(e.KeyData)
                End If
                Return MyBase.ProcessDataGridViewKey(e)
            Catch ex As Exception

            End Try
        End Function
#End Region

#Region "Browsable properties"

        ''' <summary>
        ''' If true a row header at the left side 
        ''' of the summaryboxes is displayed.
        ''' </summary>
        Private m_displaySumRowHeader As Boolean
        <Browsable(True), Category("Summary")> _
        Public Property DisplaySumRowHeader() As Boolean
            Get
                Return m_displaySumRowHeader
            End Get
            Set(ByVal value As Boolean)
                m_displaySumRowHeader = value
            End Set
        End Property

        ''' <summary>
        ''' Text displayed in the row header
        ''' of the summary row.
        ''' </summary>
        Private m_sumRowHeaderText As String
        <Browsable(True), Category("Summary")> _
        Public Property SumRowHeaderText() As String
            Get
                Return m_sumRowHeaderText
            End Get
            Set(ByVal value As String)
                m_sumRowHeaderText = value
            End Set
        End Property

        ''' <summary>
        ''' Text displayed in the row header
        ''' of the summary row.
        ''' </summary>
        Private m_sumRowHeaderTextBold As Boolean
        <Browsable(True), Category("Summary")> _
        Public Property SumRowHeaderTextBold() As Boolean
            Get
                Return m_sumRowHeaderTextBold
            End Get
            Set(ByVal value As Boolean)
                m_sumRowHeaderTextBold = value
            End Set
        End Property

        ''' <summary>
        ''' Add columns to sum up in text form
        ''' </summary>
        Private m_summaryColumns As String()
        <Browsable(True), Category("Summary")> _
        Public Property SummaryColumns() As String()
            Get
                Return m_summaryColumns
            End Get
            Set(ByVal value As String())
                m_summaryColumns = value
            End Set
        End Property

        ''' <summary>
        ''' Display the summary Row
        ''' </summary>
        Private m_summaryRowVisible As Boolean
        <Browsable(True), Category("Summary")> _
        Public Property SummaryRowVisible() As Boolean
            Get
                Return m_summaryRowVisible
            End Get
            Set(ByVal value As Boolean)
                m_summaryRowVisible = value
                If summaryControl IsNot Nothing AndAlso spacePanel IsNot Nothing Then
                    summaryControl.Visible = value
                    spacePanel.Visible = value
                End If
            End Set
        End Property

        Private m_summaryRowSpace As Integer = 0
        <Browsable(True), Category("Summary")> _
        Public Property SummaryRowSpace() As Integer
            Get
                Return m_summaryRowSpace
            End Get
            Set(ByVal value As Integer)
                m_summaryRowSpace = value
            End Set
        End Property

        Private m_formatString As String = "F02"
        <Browsable(True), Category("Summary"), DefaultValue("F02")> _
        Public Property FormatString() As String
            Get
                Return m_formatString
            End Get
            Set(ByVal value As String)
                m_formatString = value
            End Set
        End Property

        <Browsable(True), Category("Summary")> _
        Public Property SummaryRowBackColor() As Color
            Get
                Return Me.summaryControl.SummaryRowBackColor
            End Get
            Set(ByVal value As Color)
                summaryControl.SummaryRowBackColor = value
            End Set
        End Property


        ''' <summary>
        ''' advoid user from setting the scrollbars manually
        ''' </summary>
        <Browsable(False)> _
        Public Shadows Property ScrollBars() As ScrollBars
            Get
                Return MyBase.ScrollBars
            End Get
            Set(ByVal value As ScrollBars)
                MyBase.ScrollBars = value
            End Set
        End Property


#End Region

#Region "Declare variables"

        Public Event CreateSummary As EventHandler
        Private hScrollBar As HScrollBar
        Private summaryControl As eSummaryControlContainer
        Private panel As Windows.Forms.Panel, spacePanel As Windows.Forms.Panel
        Private refBox As Windows.Forms.TextBox

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()

            refBox = New Windows.Forms.TextBox()
            panel = New Windows.Forms.Panel()
            spacePanel = New Windows.Forms.Panel()
            hScrollBar = New HScrollBar()

            summaryControl = New eSummaryControlContainer(Me)
            AddHandler summaryControl.VisibilityChanged, AddressOf summaryControl_VisibilityChanged

            AddHandler Resize, AddressOf DataGridControlSum_Resize
            AddHandler RowHeadersWidthChanged, AddressOf DataGridControlSum_Resize
            AddHandler ColumnAdded, AddressOf DataGridControlSum_ColumnAdded
            AddHandler ColumnRemoved, AddressOf DataGridControlSum_ColumnRemoved

            AddHandler hScrollBar.Scroll, AddressOf hScrollBar_Scroll
            AddHandler hScrollBar.VisibleChanged, AddressOf hScrollBar_VisibleChanged

            hScrollBar.Top += summaryControl.Bottom
            hScrollBar.Minimum = 0
            hScrollBar.Maximum = 0
            hScrollBar.Value = 0
        End Sub

#End Region

#Region "public functions"

        ''' <summary>
        ''' Refresh the summary
        ''' </summary>
        Public Sub RefreshSummary()
            If Me.summaryControl IsNot Nothing Then
                Me.summaryControl.RefreshSummary()
            End If
        End Sub

#End Region

#Region "Calculate Columns and Scrollbars width"
        Private Sub DataGridControlSum_ColumnRemoved(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            calculateColumnsWidth()
            summaryControl.Width = columnsWidth
            hScrollBar.Maximum = Convert.ToInt32(columnsWidth)
            resizeHScrollBar()
        End Sub
        Private Sub DataGridControlSum_ColumnAdded(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            calculateColumnsWidth()
            summaryControl.Width = columnsWidth
            hScrollBar.Maximum = Convert.ToInt32(columnsWidth)
            resizeHScrollBar()
        End Sub

        Private columnsWidth As Integer = 0
        ''' <summary>
        ''' Calculate the width of all visible columns
        ''' </summary>
        Private Sub calculateColumnsWidth()
            columnsWidth = 0
            For iCnt As Integer = 0 To ColumnCount - 1
                If Columns(iCnt).Visible Then
                    If Columns(iCnt).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill Then
                        columnsWidth += Columns(iCnt).MinimumWidth
                    Else
                        columnsWidth += Columns(iCnt).Width
                    End If
                End If
            Next
        End Sub

#End Region

#Region "Other Events and delegates"

        ''' <summary>
        ''' Moves viewable area of DataGridView according to the position of the scrollbar
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub hScrollBar_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
            Dim position As Integer = Convert.ToInt32((Convert.ToDouble(e.NewValue) / (Convert.ToDouble(hScrollBar.Maximum) / Convert.ToDouble(Columns.Count))))
            If position < Columns.Count Then
                FirstDisplayedScrollingColumnIndex = position
            End If
        End Sub

        Public Sub CreateSummaryRow()
            OnCreateSummary(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Calls the CreateSummary event
        ''' </summary>
        Private Sub OnCreateSummary(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CreateSummary(sender, e)
        End Sub

#End Region

#Region "Adjust summaryControl, scrollbar"

        ''' <summary>
        ''' Position the summaryControl under the
        ''' DataGridView
        ''' </summary>
        Private Sub adjustSumControlToGrid()
            If summaryControl Is Nothing OrElse Parent Is Nothing Then
                Return
            End If
            summaryControl.Top = Height + m_summaryRowSpace
            summaryControl.Left = Left
            summaryControl.Width = Width
        End Sub

        ''' <summary>
        ''' Position the hScrollbar under the summaryControl
        ''' </summary>
        Private Sub adjustScrollbarToSummaryControl()
            If Parent IsNot Nothing Then
                hScrollBar.Top = refBox.Height + 2
                hScrollBar.Width = Width
                hScrollBar.Left = Left

                resizeHScrollBar()
            End If
        End Sub

        ''' <summary>
        ''' Resizes the horizontal scrollbar acording
        ''' to the with of the client size and maximum size of the scrollbar
        ''' </summary>
        Private Sub resizeHScrollBar()
            'Is used to calculate the LageChange of the scrollbar
            Dim vscrollbarWidth As Integer = 0
            If VerticalScrollBar.Visible Then
                vscrollbarWidth = VerticalScrollBar.Width
            End If

            Dim rowHeaderWith As Integer = If(RowHeadersVisible, RowHeadersWidth, 0)

            If columnsWidth > 0 Then
                'This is neccessary if AutoGenerateColumns = true because DataGridControlSum_ColumnAdded won't be fired
                If hScrollBar.Maximum = 0 Then
                    hScrollBar.Maximum = columnsWidth
                End If

                'Calculate how much of the columns are visible in %
                Dim scrollBarWidth As Integer = Convert.ToInt32(Convert.ToDouble(ClientSize.Width - RowHeadersWidth - vscrollbarWidth) / (Convert.ToDouble(hScrollBar.Maximum) / 100.0F))

                If scrollBarWidth > 100 OrElse columnsWidth + rowHeaderWith < ClientSize.Width Then
                    If hScrollBar.Visible Then
                        hScrollBar.Visible = False
                    End If
                ElseIf scrollBarWidth > 0 Then
                    If Not hScrollBar.Visible Then
                        hScrollBar.Visible = True
                    End If
                    hScrollBar.LargeChange = hScrollBar.Maximum / 100 * scrollBarWidth
                    hScrollBar.SmallChange = hScrollBar.LargeChange / 5
                End If
            End If
        End Sub

        Private Sub DataGridControlSum_Resize(ByVal sender As Object, ByVal e As EventArgs)
            If Parent IsNot Nothing Then
                calculateColumnsWidth()
                resizeHScrollBar()
                adjustSumControlToGrid()
                adjustScrollbarToSummaryControl()
            End If
        End Sub

        ''' <summary>
        ''' Recalculate the width of the summary control according to 
        ''' the state of the scrollbar
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub hScrollBar_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Parent IsNot Nothing Then
                'only perform operation if parent is visible
                If Parent.Visible Then
                    Dim height As Integer
                    If hScrollBar.Visible Then
                        height = summaryControl.InitialHeight + hScrollBar.Height
                    Else
                        height = summaryControl.InitialHeight
                    End If

                    If summaryControl.Height <> height AndAlso summaryControl.Visible Then
                        summaryControl.Height = height
                        Me.Height = panel.Height - summaryControl.Height - m_summaryRowSpace
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Recalculate the height of the DataGridView
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub summaryControl_VisibilityChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Not summaryControl.Visible Then
                ScrollBars = ScrollBars.Both
                Me.Height = panel.Height
            Else
                Me.Height = panel.Height - summaryControl.Height - m_summaryRowSpace
                ScrollBars = ScrollBars.Vertical
            End If
        End Sub

        Private Sub changeParent()
            If Not DesignMode AndAlso Parent IsNot Nothing Then

                summaryControl.InitialHeight = Me.refBox.Height
                summaryControl.Height = summaryControl.InitialHeight
                summaryControl.BackColor = Me.RowHeadersDefaultCellStyle.BackColor
                summaryControl.ForeColor = Color.Transparent
                summaryControl.RightToLeft = Me.RightToLeft
                panel.Bounds = Me.Bounds
                panel.BackColor = Me.BackgroundColor

                panel.Dock = Me.Dock
                panel.Anchor = Me.Anchor
                panel.Padding = Me.Padding
                panel.Margin = Me.Margin
                panel.Top = Me.Top
                panel.Left = Me.Left
                panel.BorderStyle = Me.BorderStyle

                Margin = New Padding(0)
                Padding = New Padding(0)
                Top = 0
                Left = 0

                summaryControl.Dock = DockStyle.Bottom
                Me.Dock = DockStyle.Fill

                If TypeOf Me.Parent Is TableLayoutPanel Then
                    Dim rowSpan As Integer, colSpan As Integer

                    Dim tlp As TableLayoutPanel = TryCast(Me.Parent, TableLayoutPanel)
                    Dim cellPos As TableLayoutPanelCellPosition = tlp.GetCellPosition(Me)

                    rowSpan = tlp.GetRowSpan(Me)
                    colSpan = tlp.GetColumnSpan(Me)

                    tlp.Controls.Remove(Me)
                    tlp.Controls.Add(panel, cellPos.Column, cellPos.Row)
                    tlp.SetRowSpan(panel, rowSpan)
                    tlp.SetColumnSpan(panel, colSpan)
                Else
                    Dim parent__1 As Control = Me.Parent
                    'remove DataGridView from ParentControls
                    parent__1.Controls.Remove(Me)
                    parent__1.Controls.Add(panel)
                End If

                Me.BorderStyle = BorderStyle.None

                panel.BringToFront()


                hScrollBar.Top = refBox.Height + 2
                hScrollBar.Width = Me.Width
                hScrollBar.Left = Me.Left

                summaryControl.Controls.Add(hScrollBar)
                hScrollBar.BringToFront()
                panel.Controls.Add(Me)

                spacePanel = New Windows.Forms.Panel()
                spacePanel.BackColor = panel.BackColor
                spacePanel.Height = m_summaryRowSpace
                spacePanel.Dock = DockStyle.Bottom

                panel.Controls.Add(spacePanel)
                panel.Controls.Add(summaryControl)

                resizeHScrollBar()
                adjustSumControlToGrid()
                adjustScrollbarToSummaryControl()

                resizeHScrollBar()
            End If
        End Sub

#End Region

#Region "ISupportInitialzie"

        Public Sub BeginInit()
        End Sub

        Public Sub EndInit()
            changeParent()
        End Sub

#End Region
    End Class
End Namespace