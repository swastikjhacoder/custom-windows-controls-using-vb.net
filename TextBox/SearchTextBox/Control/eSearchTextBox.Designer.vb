
Namespace UIControls.TextBox
    Partial Class eSearchTextBox
        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                _inactiveFont.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.searchOverlayLabel = New System.Windows.Forms.Label()
            Me.searchText = New System.Windows.Forms.TextBox()
            Me.searchImage = New System.Windows.Forms.PictureBox()
            DirectCast(Me.searchImage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' searchOverlayLabel
            ' 
            Me.searchOverlayLabel.Anchor = DirectCast(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.searchOverlayLabel.AutoSize = True
            Me.searchOverlayLabel.Location = New System.Drawing.Point(2, 3)
            Me.searchOverlayLabel.Margin = New System.Windows.Forms.Padding(0)
            Me.searchOverlayLabel.Name = "searchOverlayLabel"
            Me.searchOverlayLabel.Size = New System.Drawing.Size(0, 13)
            Me.searchOverlayLabel.TabIndex = 0
            ' 
            ' searchText
            ' 
            Me.searchText.Anchor = DirectCast((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.searchText.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.searchText.Location = New System.Drawing.Point(2, 3)
            Me.searchText.Margin = New System.Windows.Forms.Padding(0)
            Me.searchText.Name = "searchText"
            Me.searchText.Size = New System.Drawing.Size(125, 13)
            Me.searchText.TabIndex = 0
            Me.searchText.TabStop = False
            AddHandler Me.searchText.TextChanged, AddressOf Me.searchText_TextChanged
            AddHandler Me.searchText.GotFocus, AddressOf searchText_GotFocus
            AddHandler Me.searchText.LostFocus, AddressOf searchText_LostFocus
            ' 
            ' searchImage
            ' 
            Me.searchImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
            Me.searchImage.Anchor = DirectCast(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.searchImage.Cursor = System.Windows.Forms.Cursors.Arrow
            Me.searchImage.Image = Global.ESAR_Controls.My.Resources.inactive_search
            Me.searchImage.Location = New System.Drawing.Point(127, 0)
            Me.searchImage.Margin = New System.Windows.Forms.Padding(0)
            Me.searchImage.Name = "searchImage"
            Me.searchImage.Size = New System.Drawing.Size(23, 20)
            Me.searchImage.TabIndex = 1
            Me.searchImage.TabStop = False
            AddHandler Me.searchImage.Click, AddressOf searchImage_Click
            AddHandler Me.searchImage.MouseMove, AddressOf searchImage_MouseMove
            ' 
            ' SearchTextBox
            ' 
            Me.BackColor = System.Drawing.SystemColors.Window
            Me.Controls.Add(Me.searchOverlayLabel)
            Me.Controls.Add(Me.searchText)
            Me.Controls.Add(Me.searchImage)
            Me.Cursor = System.Windows.Forms.Cursors.IBeam
            Me.Name = "SearchTextBox"
            Me.Size = New System.Drawing.Size(150, 20)
            DirectCast(Me.searchImage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private searchOverlayLabel As System.Windows.Forms.Label
        Private searchText As System.Windows.Forms.TextBox
        Private searchImage As System.Windows.Forms.PictureBox
    End Class
End Namespace