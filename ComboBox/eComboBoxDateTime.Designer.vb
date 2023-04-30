
Namespace UIControls.ComboBox
    Partial Class eComboBoxDateTime
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
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
            Me.SuspendLayout()
            ' 
            ' ComboBoxDateTime
            ' 
            Me.Name = "BetterComboBox"
            Me.Size = New System.Drawing.Size(90, 21)
            Me.FormatString = "d"
            Me.ResumeLayout(False)

        End Sub

#End Region
    End Class
End Namespace