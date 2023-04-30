Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Drawing

Namespace UIControls.TextBox
    <ToolboxBitmap(GetType(eRichTextBox), "Resources.richtextbox.bmp")> _
    Public Class eRichTextBox
        Public attach As Integer = 0
        Public dgrowindex As Integer = 0
        Dim FileChooser As New OpenFileDialog()

        Private Sub eRichTextBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            rtb.SelectionIndent = 25
        End Sub

        Private Sub SpellChecker_DeletedWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.SpellingEventArgs) Handles SpellChecker.DeletedWord
            Dim start As Integer = rtb.SelectionStart
            Dim length As Integer = rtb.SelectionLength
            rtb.Select(e.TextIndex, e.Word.Length)
            rtb.SelectedText = ""
            If ((start + length) > rtb.Text.Length) Then
                length = 0
            End If
            rtb.Select(start, length)
        End Sub

        Private Sub SpellChecker_ReplacedWord(ByVal sender As Object, ByVal e As NetSpell.SpellChecker.ReplaceWordEventArgs) Handles SpellChecker.ReplacedWord
            Dim start As Integer = rtb.SelectionStart
            Dim length As Integer = rtb.SelectionLength
            rtb.Select(e.TextIndex, e.Word.Length)
            rtb.SelectedText = e.ReplacementWord
            If ((start + length) > rtb.Text.Length) Then
                length = 0
            End If
            rtb.Select(start, length)
        End Sub

        Private Sub rtb_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb.SelectionChanged
            BoldToolStripButton.Checked = rtb.SelectionFont.Bold
            UnderlineToolStripButton.Checked = rtb.SelectionFont.Underline
            LeftToolStripButton.Checked = IIf(rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left, True, False)
            CenterToolStripButton.Checked = IIf(rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center, True, False)
            RightToolStripButton.Checked = IIf(rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right, True, False)
            BulletsToolStripButton.Checked = rtb.SelectionBullet
        End Sub

        Private Sub checkBullets()
            If rtb.SelectionBullet = True Then
                BulletsToolStripButton.Checked = True
            Else
                BulletsToolStripButton.Checked = False
            End If
        End Sub

        Private Sub BoldToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BoldToolStripButton.Click
            Dim currentFont As System.Drawing.Font = rtb.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle
            If rtb.SelectionFont.Bold = True Then
                newFontStyle = currentFont.Style - Drawing.FontStyle.Bold
            Else
                newFontStyle = currentFont.Style + Drawing.FontStyle.Bold
            End If
            rtb.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle)
            BoldToolStripButton.Checked = IIf(rtb.SelectionFont.Bold, True, False)
        End Sub

        Private Sub UnderlineToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnderlineToolStripButton.Click
            Dim currentFont As System.Drawing.Font = rtb.SelectionFont
            Dim newFontStyle As System.Drawing.FontStyle
            If rtb.SelectionFont.Underline = True Then
                newFontStyle = currentFont.Style - Drawing.FontStyle.Underline
            Else
                newFontStyle = currentFont.Style + Drawing.FontStyle.Underline
            End If
            rtb.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle)
            UnderlineToolStripButton.Checked = IIf(rtb.SelectionFont.Underline, True, False)
        End Sub

        Private Sub LeftToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftToolStripButton.Click
            rtb.SelectionAlignment = HorizontalAlignment.Left
        End Sub

        Private Sub CenterToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CenterToolStripButton.Click
            rtb.SelectionAlignment = HorizontalAlignment.Center
        End Sub

        Private Sub RightToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightToolStripButton.Click
            rtb.SelectionAlignment = HorizontalAlignment.Right
        End Sub

        Private Sub BulletsToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulletsToolStripButton.Click
            rtb.SelectionBullet = Not rtb.SelectionBullet
            BulletsToolStripButton.Checked = rtb.SelectionBullet
        End Sub

        Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click
            If rtb IsNot Nothing Then
                rtb.Paste()
            End If
        End Sub

        Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click
            If rtb IsNot Nothing Then
                rtb.Copy()
            End If
        End Sub

        Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click
            If rtb IsNot Nothing Then
                rtb.Cut()
            End If
            rtb.SelectedText = ""
        End Sub

        Private Sub ItalicToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItalicToolStripButton.Click
            If rtb.SelectionFont Is Nothing Then
                Return
            End If
            Dim style As FontStyle = rtb.SelectionFont.Style
            If rtb.SelectionFont.Italic Then
                style = style And Not FontStyle.Italic
            Else
                style = style Or FontStyle.Italic
            End If
            rtb.SelectionFont = New Font(rtb.SelectionFont, style)
        End Sub

        Private Sub UndoToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripButton.Click
            If rtb IsNot Nothing Then
                rtb.Undo()
            End If
        End Sub

        Private Sub RedoToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripButton.Click
            If rtb IsNot Nothing Then
                rtb.Redo()
            End If
        End Sub

        Private Sub DateToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateToolStripButton.Click
            Dim time As String = DateTime.Now.ToString()
            rtb.AppendText(time)
        End Sub

        Private Sub ImageToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageToolStripButton.Click
            Dim GetPicture As New OpenFileDialog
            GetPicture.Filter = "PNGs (*.png), Bitmaps (*.bmp), GIFs (*.gif), JPEGs (*.jpg)|*.bmp;*.gif;*.jpg;*.png|PNGs (*.png)|*.png|Bitmaps (*.bmp)|*.bmp|GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg"
            GetPicture.FilterIndex = 1
            GetPicture.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            If GetPicture.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim SelectedPicture As String = GetPicture.FileName
                Dim Picture As Bitmap = New Bitmap(SelectedPicture)
                Dim cboard As Object = Clipboard.GetData(System.Windows.Forms.DataFormats.Text)
                Clipboard.SetImage(Picture)
                Dim PictureFormat As DataFormats.Format = DataFormats.GetFormat(DataFormats.Bitmap)
                If rtb.CanPaste(PictureFormat) Then
                    rtb.Paste(PictureFormat)
                End If
                Clipboard.Clear()
            End If
        End Sub
    End Class
End Namespace
