
Namespace UIControls.DataGridView
    Partial Class eDataGridViewWithPaging
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.dataGridView1 = New System.Windows.Forms.DataGridView()
            Me.btnFirst = New System.Windows.Forms.Button()
            Me.btnPrevious = New System.Windows.Forms.Button()
            Me.btnNext = New System.Windows.Forms.Button()
            Me.btnLast = New System.Windows.Forms.Button()
            Me.txtPaging = New System.Windows.Forms.TextBox()
            Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            DirectCast(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tableLayoutPanel1.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' dataGridView1
            ' 
            Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dataGridView1.Location = New System.Drawing.Point(0, 0)
            Me.dataGridView1.Name = "dataGridView1"
            Me.dataGridView1.Size = New System.Drawing.Size(527, 319)
            Me.dataGridView1.TabIndex = 0
            ' 
            ' btnFirst
            ' 
            Me.btnFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
            Me.btnFirst.Location = New System.Drawing.Point(3, 3)
            Me.btnFirst.Name = "btnFirst"
            Me.btnFirst.Size = New System.Drawing.Size(75, 23)
            Me.btnFirst.TabIndex = 1
            Me.btnFirst.Text = "&First"
            Me.btnFirst.UseVisualStyleBackColor = True
            AddHandler Me.btnFirst.Click, AddressOf Me.btnFirst_Click
            ' 
            ' btnPrevious
            ' 
            Me.btnPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
            Me.btnPrevious.Location = New System.Drawing.Point(103, 3)
            Me.btnPrevious.Name = "btnPrevious"
            Me.btnPrevious.Size = New System.Drawing.Size(75, 23)
            Me.btnPrevious.TabIndex = 2
            Me.btnPrevious.Text = "&Previous"
            Me.btnPrevious.UseVisualStyleBackColor = True
            AddHandler Me.btnPrevious.Click, AddressOf Me.btnPrevious_Click
            ' 
            ' btnNext
            ' 
            Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
            Me.btnNext.Location = New System.Drawing.Point(317, 3)
            Me.btnNext.Name = "btnNext"
            Me.btnNext.Size = New System.Drawing.Size(75, 23)
            Me.btnNext.TabIndex = 3
            Me.btnNext.Text = "&Next"
            Me.btnNext.UseVisualStyleBackColor = True
            AddHandler Me.btnNext.Click, AddressOf Me.btnNext_Click
            ' 
            ' btnLast
            ' 
            Me.btnLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
            Me.btnLast.Location = New System.Drawing.Point(417, 3)
            Me.btnLast.Name = "btnLast"
            Me.btnLast.Size = New System.Drawing.Size(75, 23)
            Me.btnLast.TabIndex = 4
            Me.btnLast.Text = "&Last"
            Me.btnLast.UseVisualStyleBackColor = True
            AddHandler Me.btnLast.Click, AddressOf Me.btnLast_Click
            ' 
            ' txtPaging
            ' 
            Me.txtPaging.ForeColor = System.Drawing.SystemColors.InactiveCaption
            Me.txtPaging.Location = New System.Drawing.Point(203, 3)
            Me.txtPaging.Name = "txtPaging"
            Me.txtPaging.Size = New System.Drawing.Size(100, 20)
            Me.txtPaging.TabIndex = 5
            Me.txtPaging.Text = "Current Page"
            Me.txtPaging.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            ' 
            ' tableLayoutPanel1
            ' 
            Me.tableLayoutPanel1.ColumnCount = 5
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0F))
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0F))
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114.0F))
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0F))
            Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113.0F))
            Me.tableLayoutPanel1.Controls.Add(Me.btnFirst, 0, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.txtPaging, 2, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.btnLast, 4, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.btnPrevious, 1, 0)
            Me.tableLayoutPanel1.Controls.Add(Me.btnNext, 3, 0)
            Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 278)
            Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
            Me.tableLayoutPanel1.RowCount = 1
            Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0F))
            Me.tableLayoutPanel1.Size = New System.Drawing.Size(527, 41)
            Me.tableLayoutPanel1.TabIndex = 6
            ' 
            ' DataGridViewWithPaging
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.tableLayoutPanel1)
            Me.Controls.Add(Me.dataGridView1)
            Me.Name = "DataGridViewWithPaging"
            Me.Size = New System.Drawing.Size(527, 319)
            DirectCast(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tableLayoutPanel1.ResumeLayout(False)
            Me.tableLayoutPanel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private dataGridView1 As System.Windows.Forms.DataGridView
        Private btnFirst As System.Windows.Forms.Button
        Private btnPrevious As System.Windows.Forms.Button
        Private btnNext As System.Windows.Forms.Button
        Private btnLast As System.Windows.Forms.Button
        Private txtPaging As System.Windows.Forms.TextBox
        Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    End Class
End Namespace