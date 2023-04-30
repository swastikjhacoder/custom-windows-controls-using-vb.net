
Namespace UIControls.GroupBox
    Partial Class eCheckGroupBox
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
            Me.m_checkBox = New System.Windows.Forms.CheckBox()
            Me.SuspendLayout()
            ' 
            ' m_checkBox
            ' 
            Me.m_checkBox.AutoSize = True
            Me.m_checkBox.Checked = True
            Me.m_checkBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.m_checkBox.Location = New System.Drawing.Point(0, 0)
            Me.m_checkBox.Name = "m_checkBox"
            Me.m_checkBox.Size = New System.Drawing.Size(104, 24)
            Me.m_checkBox.TabIndex = 0
            Me.m_checkBox.Text = "checkBox"
            Me.m_checkBox.UseVisualStyleBackColor = True
            AddHandler Me.m_checkBox.CheckStateChanged, AddressOf Me.checkBox_CheckStateChanged
            AddHandler Me.m_checkBox.CheckedChanged, AddressOf Me.checkBox_CheckedChanged
            ' 
            ' CheckGroupBox
            ' 
            AddHandler Me.ControlAdded, AddressOf Me.CheckGroupBox_ControlAdded
            Me.ResumeLayout(False)

        End Sub
#End Region

        Private m_checkBox As System.Windows.Forms.CheckBox
    End Class
End Namespace
