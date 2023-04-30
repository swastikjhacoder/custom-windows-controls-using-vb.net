
Namespace UIControls.GroupBox
    Partial Class eRadioGroupBox
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
            Me.m_radioButton = New System.Windows.Forms.RadioButton()
            Me.SuspendLayout()
            ' 
            ' m_radioButton
            ' 
            Me.m_radioButton.AutoSize = True
            Me.m_radioButton.Location = New System.Drawing.Point(0, 0)
            Me.m_radioButton.Name = "m_radioButton"
            Me.m_radioButton.Size = New System.Drawing.Size(104, 24)
            Me.m_radioButton.TabIndex = 0
            Me.m_radioButton.TabStop = True
            Me.m_radioButton.Text = "radioButton"
            Me.m_radioButton.UseVisualStyleBackColor = True
            AddHandler Me.m_radioButton.CheckedChanged, AddressOf Me.radioButton_CheckedChanged
            ' 
            ' RadioGroupBox
            ' 
            AddHandler Me.ControlAdded, AddressOf Me.CheckGroupBox_ControlAdded
            Me.ResumeLayout(False)

        End Sub
#End Region

        Private m_radioButton As System.Windows.Forms.RadioButton
    End Class
End Namespace
