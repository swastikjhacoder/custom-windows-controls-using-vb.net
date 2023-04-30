#Region "Imports..."
Imports ESAR_Controls.UIControls
Imports System.Windows.Forms

#End Region
#Region "Class cTextChange..."
Public Class cTextChange
#Region "Methods..."
    Public Sub ConvertTextBoxValidValue(ByRef frm As ESAR_Controls.UIControls.TextBox.eTextBox)
        If Control.IsKeyLocked(Keys.CapsLock) Then
        Else
            Dim sel_start As Integer
            Dim sel_length As Integer
            sel_start = frm.SelectionStart
            sel_length = frm.SelectionLength
            frm.Text = StrConv(frm.Text, VbStrConv.ProperCase)
            frm.SelectionStart = sel_start
            frm.SelectionLength = sel_length
        End If
    End Sub
#End Region
End Class
#End Region
