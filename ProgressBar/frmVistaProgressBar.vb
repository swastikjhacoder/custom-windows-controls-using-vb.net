Public Class frmVistaProgressBar

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PropertyGrid1.SelectedObject = VistaProgressBar1
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        VistaProgressBar1.Value = TrackBar1.Value
    End Sub

    Private Sub VistaProgressBar1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VistaProgressBar1.ValueChanged
        TrackBar1.Value = VistaProgressBar1.Value
    End Sub
End Class
