Namespace UIControls.Button
    Partial Class eDDControl
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
            Me.btnDropDown = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            ' 
            ' btnDropDown
            ' 
            Me.btnDropDown.Anchor = DirectCast((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnDropDown.Location = New System.Drawing.Point(3, 0)
            Me.btnDropDown.Name = "btnDropDown"
            Me.btnDropDown.Size = New System.Drawing.Size(116, 23)
            Me.btnDropDown.TabIndex = 0
            Me.btnDropDown.UseVisualStyleBackColor = True
            AddHandler Me.btnDropDown.Click, AddressOf Me.btnDropDown_Click
            ' 
            ' DDControl
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.btnDropDown)
            Me.Name = "DDControl"
            Me.Size = New System.Drawing.Size(122, 24)
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private btnDropDown As System.Windows.Forms.Button
    End Class

End Namespace