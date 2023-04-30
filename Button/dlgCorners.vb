
Public Class dlgCorners
    Private cnrs As CornersProperty = New CornersProperty()
    Public ratio As Single = 1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        TheSample.Corners.UpperLeft = cnrs.UpperLeft
        TheSample.Corners.UpperRight = cnrs.UpperRight
        TheSample.Corners.LowerLeft = cnrs.LowerLeft
        TheSample.Corners.LowerRight = cnrs.LowerRight

        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub tbarUpperLeft_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarUpperLeft.Scroll
        TheSample.Corners.UpperLeft = CShort(tbarUpperLeft.Value)
        lblUL.Text = CStr(CInt(tbarUpperLeft.Value / ratio))
        TheSample.Invalidate()
    End Sub

    Private Sub tbarUpperRight_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarUpperRight.Scroll
        TheSample.Corners.UpperRight = CShort(tbarUpperRight.Value)
        lblUR.Text = CStr(CInt(tbarUpperRight.Value / ratio))
        TheSample.Invalidate()
    End Sub

    Private Sub tbarLowerLeft_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarLowerLeft.Scroll
        TheSample.Corners.LowerLeft = CShort(tbarLowerLeft.Value)
        lblLL.Text = CStr(CInt(tbarLowerLeft.Value / ratio))
        TheSample.Invalidate()
    End Sub

    Private Sub tbarLowerRight_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarLowerRight.Scroll
        TheSample.Corners.LowerRight = CShort(tbarLowerRight.Value)
        lblLR.Text = CStr(CInt(tbarLowerRight.Value / ratio))
        TheSample.Invalidate()
    End Sub

    Private Sub tbarAll_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbarAll.Scroll
        tbarUpperLeft.Value = tbarAll.Value
        tbarUpperRight.Value = tbarAll.Value
        tbarLowerLeft.Value = tbarAll.Value
        tbarLowerRight.Value = tbarAll.Value
        UpdateLabels()
        TheSample.Corners.UpperLeft = CShort(tbarAll.Value)
        TheSample.Corners.UpperRight = CShort(tbarAll.Value)
        TheSample.Corners.LowerLeft = CShort(tbarAll.Value)
        TheSample.Corners.LowerRight = CShort(tbarAll.Value)
        TheSample.Invalidate()

    End Sub
    Sub UpdateLabels()
        lblUL.Text = CStr(CInt(tbarUpperLeft.Value / ratio))
        lblUR.Text = CStr(CInt(tbarUpperRight.Value / ratio))
        lblLL.Text = CStr(CInt(tbarLowerLeft.Value / ratio))
        lblLR.Text = CStr(CInt(tbarLowerRight.Value / ratio))

    End Sub
    Private Sub dlgCorners_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cnrs.UpperLeft = TheSample.Corners.UpperLeft
        cnrs.UpperRight = TheSample.Corners.UpperRight
        cnrs.LowerLeft = TheSample.Corners.LowerLeft
        cnrs.LowerRight = TheSample.Corners.LowerRight
        UpdateLabels()
    End Sub

End Class
