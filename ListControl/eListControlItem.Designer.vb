﻿Namespace UIControls.ListControl
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eListControlItem
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
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
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(eListControlItem))
            Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.SuspendLayout()
            '
            'ImageList1
            '
            Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
            Me.ImageList1.Images.SetKeyName(0, "default")
            '
            'ListControlItem
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.DoubleBuffered = True
            Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
            Me.Name = "ListControlItem"
            Me.Size = New System.Drawing.Size(484, 50)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ImageList1 As System.Windows.Forms.ImageList

    End Class

End Namespace