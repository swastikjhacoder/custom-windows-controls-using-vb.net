Imports ESAR_Controls.UIControls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVistaProgressBar
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.VistaProgressBar1 = New ProgressBar.eVistaProgressBar
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(12, 51)
        Me.TrackBar1.Maximum = 100
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(264, 45)
        Me.TrackBar1.TabIndex = 2
        Me.TrackBar1.TickFrequency = 5
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        Me.TrackBar1.Value = 50
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PropertyGrid1.Location = New System.Drawing.Point(282, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(194, 264)
        Me.PropertyGrid1.TabIndex = 0
        '
        'VistaProgressBar1
        '
        Me.VistaProgressBar1.BackColor = System.Drawing.Color.Transparent
        Me.VistaProgressBar1.DisplayText = ""
        Me.VistaProgressBar1.EndColor = System.Drawing.Color.Blue
        Me.VistaProgressBar1.Location = New System.Drawing.Point(13, 13)
        Me.VistaProgressBar1.MarqueeSpeed = 20
        Me.VistaProgressBar1.Name = "VistaProgressBar1"
        Me.VistaProgressBar1.ShowPercentage = UIControls.ProgressBar.eVistaProgressBar.TextShowFormats.None
        Me.VistaProgressBar1.Size = New System.Drawing.Size(264, 32)
        Me.VistaProgressBar1.StartColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.VistaProgressBar1.TabIndex = 5
        Me.VistaProgressBar1.Value = 50
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 264)
        Me.Controls.Add(Me.VistaProgressBar1)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents VistaProgressBar1 As UIControls.ProgressBar.eVistaProgressBar

End Class
