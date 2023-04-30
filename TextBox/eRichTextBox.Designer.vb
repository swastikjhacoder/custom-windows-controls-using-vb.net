Namespace UIControls.TextBox
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eRichTextBox
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
            Me.ToolStrip2 = New System.Windows.Forms.ToolStrip
            Me.BoldToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.ItalicToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.UnderlineToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
            Me.LeftToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.CenterToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.RightToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
            Me.BulletsToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
            Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
            Me.UndoToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.RedoToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
            Me.DateToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.ImageToolStripButton = New System.Windows.Forms.ToolStripButton
            Me.AttachToolStripProgressBar = New System.Windows.Forms.ToolStripProgressBar
            Me.SaveFileDlg = New System.Windows.Forms.SaveFileDialog
            Me.OpenFileDlg = New System.Windows.Forms.OpenFileDialog
            Me.ColorDlg = New System.Windows.Forms.ColorDialog
            Me.FontDlg = New System.Windows.Forms.FontDialog
            Me.SpellChecker = New NetSpell.SpellChecker.Spelling(Me.components)
            Me.TimerAttachProgress = New System.Windows.Forms.Timer(Me.components)
            Me.rtb = New ESAR_Controls.UIControls.TextBox.eRicherTextBox
            Me.ToolStrip2.SuspendLayout()
            Me.SuspendLayout()
            '
            'ToolStrip2
            '
            Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BoldToolStripButton, Me.ItalicToolStripButton, Me.UnderlineToolStripButton, Me.ToolStripSeparator4, Me.LeftToolStripButton, Me.CenterToolStripButton, Me.RightToolStripButton, Me.ToolStripSeparator3, Me.BulletsToolStripButton, Me.ToolStripSeparator2, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton, Me.toolStripSeparator1, Me.UndoToolStripButton, Me.RedoToolStripButton, Me.ToolStripSeparator6, Me.DateToolStripButton, Me.ImageToolStripButton, Me.AttachToolStripProgressBar})
            Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip2.Name = "ToolStrip2"
            Me.ToolStrip2.Size = New System.Drawing.Size(536, 25)
            Me.ToolStrip2.TabIndex = 2
            Me.ToolStrip2.Text = "ToolStrip2"
            '
            'BoldToolStripButton
            '
            Me.BoldToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BoldToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.bold16
            Me.BoldToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.BoldToolStripButton.Name = "BoldToolStripButton"
            Me.BoldToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.BoldToolStripButton.Text = "Bold"
            '
            'ItalicToolStripButton
            '
            Me.ItalicToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ItalicToolStripButton.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
            Me.ItalicToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.italic16
            Me.ItalicToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ItalicToolStripButton.Name = "ItalicToolStripButton"
            Me.ItalicToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.ItalicToolStripButton.Text = "i"
            Me.ItalicToolStripButton.ToolTipText = "Italic"
            '
            'UnderlineToolStripButton
            '
            Me.UnderlineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.UnderlineToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.underline16
            Me.UnderlineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.UnderlineToolStripButton.Name = "UnderlineToolStripButton"
            Me.UnderlineToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.UnderlineToolStripButton.Text = "Underline"
            '
            'ToolStripSeparator4
            '
            Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
            Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
            '
            'LeftToolStripButton
            '
            Me.LeftToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.LeftToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.textalignleft16
            Me.LeftToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.LeftToolStripButton.Name = "LeftToolStripButton"
            Me.LeftToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.LeftToolStripButton.Text = "Left"
            '
            'CenterToolStripButton
            '
            Me.CenterToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.CenterToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.textaligncenter16
            Me.CenterToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.CenterToolStripButton.Name = "CenterToolStripButton"
            Me.CenterToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.CenterToolStripButton.Text = "Center"
            '
            'RightToolStripButton
            '
            Me.RightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.RightToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.textalignright16
            Me.RightToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.RightToolStripButton.Name = "RightToolStripButton"
            Me.RightToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.RightToolStripButton.Text = "Right"
            '
            'ToolStripSeparator3
            '
            Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
            Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
            '
            'BulletsToolStripButton
            '
            Me.BulletsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.BulletsToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.bullets
            Me.BulletsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.BulletsToolStripButton.Name = "BulletsToolStripButton"
            Me.BulletsToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.BulletsToolStripButton.Text = "Bullets"
            '
            'ToolStripSeparator2
            '
            Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
            Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
            '
            'CutToolStripButton
            '
            Me.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.CutToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.cut16
            Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.CutToolStripButton.Name = "CutToolStripButton"
            Me.CutToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.CutToolStripButton.Text = "C&ut"
            '
            'CopyToolStripButton
            '
            Me.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.CopyToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.copy16
            Me.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.CopyToolStripButton.Name = "CopyToolStripButton"
            Me.CopyToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.CopyToolStripButton.Text = "&Copy"
            '
            'PasteToolStripButton
            '
            Me.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.PasteToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.paste
            Me.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.PasteToolStripButton.Name = "PasteToolStripButton"
            Me.PasteToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.PasteToolStripButton.Text = "&Paste"
            '
            'toolStripSeparator1
            '
            Me.toolStripSeparator1.Name = "toolStripSeparator1"
            Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
            '
            'UndoToolStripButton
            '
            Me.UndoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.UndoToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.undo_icon
            Me.UndoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.UndoToolStripButton.Name = "UndoToolStripButton"
            Me.UndoToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.UndoToolStripButton.Text = "Undo"
            '
            'RedoToolStripButton
            '
            Me.RedoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.RedoToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.redo_icon
            Me.RedoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.RedoToolStripButton.Name = "RedoToolStripButton"
            Me.RedoToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.RedoToolStripButton.Text = "Redo"
            '
            'ToolStripSeparator6
            '
            Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
            Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
            '
            'DateToolStripButton
            '
            Me.DateToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.DateToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources._Date
            Me.DateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.DateToolStripButton.Name = "DateToolStripButton"
            Me.DateToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.DateToolStripButton.Text = "Insert Date Time"
            '
            'ImageToolStripButton
            '
            Me.ImageToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ImageToolStripButton.Image = Global.ESAR_Controls.My.Resources.Resources.Picture
            Me.ImageToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ImageToolStripButton.Name = "ImageToolStripButton"
            Me.ImageToolStripButton.Size = New System.Drawing.Size(23, 22)
            Me.ImageToolStripButton.Text = "Insert Picture"
            '
            'AttachToolStripProgressBar
            '
            Me.AttachToolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.AttachToolStripProgressBar.Name = "AttachToolStripProgressBar"
            Me.AttachToolStripProgressBar.Size = New System.Drawing.Size(100, 22)
            Me.AttachToolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            Me.AttachToolStripProgressBar.Visible = False
            '
            'OpenFileDlg
            '
            Me.OpenFileDlg.FileName = "OpenFileDialog1"
            '
            'SpellChecker
            '
            Me.SpellChecker.Dictionary = Nothing
            '
            'TimerAttachProgress
            '
            Me.TimerAttachProgress.Interval = 40
            '
            'rtb
            '
            Me.rtb.AlwaysSuggest = True
            Me.rtb.DetectUrls = True
            Me.rtb.Dock = System.Windows.Forms.DockStyle.Fill
            Me.rtb.HiglightColor = ESAR_Controls.UIControls.TextBox.eRicherTextBox.RtfColor.Black
            Me.rtb.IgnoreUpperCase = False
            Me.rtb.Location = New System.Drawing.Point(0, 25)
            Me.rtb.Name = "rtb"
            Me.rtb.Size = New System.Drawing.Size(536, 286)
            Me.rtb.TabIndex = 3
            Me.rtb.Text = ""
            '
            'eRichTextBox
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.rtb)
            Me.Controls.Add(Me.ToolStrip2)
            Me.Name = "eRichTextBox"
            Me.Size = New System.Drawing.Size(536, 311)
            Me.ToolStrip2.ResumeLayout(False)
            Me.ToolStrip2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
        Friend WithEvents BoldToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ItalicToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents UnderlineToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents LeftToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents CenterToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents RightToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents BulletsToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents CopyToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents PasteToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents UndoToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents RedoToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents DateToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents ImageToolStripButton As System.Windows.Forms.ToolStripButton
        Friend WithEvents AttachToolStripProgressBar As System.Windows.Forms.ToolStripProgressBar
        Friend WithEvents SaveFileDlg As System.Windows.Forms.SaveFileDialog
        Friend WithEvents OpenFileDlg As System.Windows.Forms.OpenFileDialog
        Friend WithEvents ColorDlg As System.Windows.Forms.ColorDialog
        Friend WithEvents FontDlg As System.Windows.Forms.FontDialog
        Friend WithEvents SpellChecker As NetSpell.SpellChecker.Spelling
        Friend WithEvents TimerAttachProgress As System.Windows.Forms.Timer
        Public WithEvents rtb As ESAR_Controls.UIControls.TextBox.eRicherTextBox

    End Class
End Namespace
