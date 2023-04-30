Namespace UIControls.Button
    Partial Class eGlassButton

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing Then
                    If _imageButton IsNot Nothing Then
                        _imageButton.Parent.Dispose()
                        _imageButton.Parent = Nothing
                        _imageButton.Dispose()
                        _imageButton = Nothing
                    End If
                    DestroyFrames()
                    If components IsNot Nothing Then
                        components.Dispose()
                    End If
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#Region "Component Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.timer = New System.Windows.Forms.Timer(Me.components)
        End Sub

#End Region

        Friend WithEvents timer As System.Windows.Forms.Timer

    End Class
End Namespace
