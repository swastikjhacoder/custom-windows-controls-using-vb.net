Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Namespace UIControls.Button

    Public Delegate Sub ItemClickedDelegate(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)

    Partial Public Class eDDControl
        Inherits UserControl

        Public Event ItemClickedEvent As ItemClickedDelegate

#Region "Members"

        Public LstOfValues As New List(Of String)()

#End Region

#Region "Constructors"

        Public Sub New()
            InitializeComponent()
        End Sub

#End Region

#Region "Methods"

        Public Sub FillControlList(ByVal lst As List(Of String))
            LstOfValues = lst
            SetMyButtonProperties()
        End Sub

        Private Sub ShowDropDown()
            Dim contextMenuStrip As New ContextMenuStrip()
            'get the path of the image
            Dim imgPath As String = GetFilePath()
            'adding contextMenuStrip items acconrding to LstOfValues count
            For i As Integer = 0 To LstOfValues.Count - 2
                'add the item
                contextMenuStrip.Items.Add(LstOfValues(i))
                'add the image
                If File.Exists((imgPath & Convert.ToString("icon")) + i + ".bmp") Then
                    contextMenuStrip.Items(i).Image = Image.FromFile((imgPath & Convert.ToString("icon")) + i + ".bmp")
                End If
            Next
            'adding ItemClicked event to contextMenuStrip
            AddHandler contextMenuStrip.ItemClicked, AddressOf contextMenuStrip_ItemClicked
            'show menu strip control
            contextMenuStrip.Show(btnDropDown, New Point(0, btnDropDown.Height))
        End Sub

        Private Sub SetMyButtonProperties()
            ' Assign an image to the button.
            Dim imgPath As String = GetFilePath()
            btnDropDown.Image = Image.FromFile(imgPath & Convert.ToString("arrow.png"))
            ' Align the image right of the button
            btnDropDown.ImageAlign = ContentAlignment.MiddleRight
            'Align the text left of the button.
            btnDropDown.TextAlign = ContentAlignment.MiddleLeft
        End Sub

        Private Function GetFilePath() As String
            'string path = string.Empty;
            'foreach (string value in Application.StartupPath.Split('\\')) {
            '    if (value == "bin") {
            '        break;
            '    }
            '    path += value + "\\";
            '}
            'return path;
            Dim value As String = Application.StartupPath.Substring(Application.StartupPath.IndexOf("bin", System.StringComparison.Ordinal))
            Return Application.StartupPath.Replace(value, String.Empty)

        End Function


#End Region

#Region "Events"

        Private Sub btnDropDown_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                ShowDropDown()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        End Sub

        Private Sub contextMenuStrip_ItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
            Try
                Dim item As ToolStripItem = e.ClickedItem
                'set the text of the button
                btnDropDown.Text = item.Text
                RaiseEvent ItemClickedEvent(sender, e)
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        End Sub

#End Region

    End Class
End Namespace