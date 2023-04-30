Imports System.Drawing
Imports System.Windows.Forms

Namespace UIControls.ListControl
    <ToolboxBitmap(GetType(eListControl), "Resources.listcontrol.bmp")> _
    Public Class eListControl

        Public Event ItemClick(ByVal sender As Object, ByVal Index As Integer)

        Public Sub Add(ByVal Song As String, ByVal Artist As String, ByVal Album As String, ByVal SongImage As Image, ByVal Rating As Integer)
            Dim c As New eListControlItem
            With c
                ' Assign an auto generated name
                .Name = "item" & flpListBox.Controls.Count + 1
                .Margin = New Windows.Forms.Padding(0)
                ' set properties
                .Song = Song
                .Artist = Artist
                .Album = Album
                .Image = SongImage
            End With
            ' To check when the selection is changed
            AddHandler c.SelectionChanged, AddressOf SelectionChanged
            AddHandler c.Click, AddressOf ItemClicked
            '
            flpListBox.Controls.Add(c)
            SetupAnchors()
        End Sub

        Public Sub AddItem(ByVal Song As String, ByVal Album As String, ByVal SongImage As Image)
            Dim c As New eListControlItem
            With c
                ' Assign an auto generated name
                .Name = "item" & flpListBox.Controls.Count + 1
                .Margin = New Windows.Forms.Padding(0)
                ' set properties
                .Song = Song
                .Album = Album
                .Image = SongImage
            End With
            ' To check when the selection is changed
            AddHandler c.SelectionChanged, AddressOf SelectionChanged
            AddHandler c.Click, AddressOf ItemClicked
            '
            flpListBox.Controls.Add(c)
            SetupAnchors()
        End Sub

        Public Sub Remove(ByVal Index As Integer)
            Dim c As eListControlItem = flpListBox.Controls(Index)
            Remove(c.Name)  ' call the below sub
        End Sub

        Public Sub Remove(ByVal name As String)
            ' grab which control is being removed
            Dim c As eListControlItem = flpListBox.Controls(name)
            flpListBox.Controls.Remove(c)
            ' remove the event hook
            RemoveHandler c.SelectionChanged, AddressOf SelectionChanged
            RemoveHandler c.Click, AddressOf ItemClicked
            ' now dispose off properly
            c.Dispose()
            SetupAnchors()
        End Sub

        Public Sub Clear()
            Do
                If flpListBox.Controls.Count = 0 Then Exit Do
                Dim c As eListControlItem = flpListBox.Controls(0)
                flpListBox.Controls.Remove(c)
                ' remove the event hook
                RemoveHandler c.SelectionChanged, AddressOf SelectionChanged
                RemoveHandler c.Click, AddressOf ItemClicked
                ' now dispose off properly
                c.Dispose()
            Loop
            mLastSelected = Nothing
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                Return flpListBox.Controls.Count
            End Get
        End Property

        Private Sub SetupAnchors()
            If flpListBox.Controls.Count > 0 Then

                For i = 0 To flpListBox.Controls.Count - 1
                    Dim c As Control = flpListBox.Controls(i)

                    If i = 0 Then
                        ' Its the first control, all subsequent controls follow 
                        ' the anchor behavior of this control.
                        c.Anchor = AnchorStyles.Left + AnchorStyles.Top
                        c.Width = flpListBox.Width - SystemInformation.VerticalScrollBarWidth

                    Else
                        ' It is not the first control. Set its anchor to
                        ' copy the width of the first control in the list.
                        c.Anchor = AnchorStyles.Left + AnchorStyles.Right

                    End If

                Next

            End If
        End Sub

        Private Sub flpListBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles flpListBox.Resize
            If flpListBox.Controls.Count Then
                flpListBox.Controls(0).Width = flpListBox.Width - SystemInformation.VerticalScrollBarWidth
            End If
        End Sub

        Dim mLastSelected As eListControlItem = Nothing
        Private Sub SelectionChanged(ByVal sender As Object)
            If mLastSelected IsNot Nothing Then
                mLastSelected.Selected = False
            End If
            mLastSelected = sender
        End Sub

        Private Sub ItemClicked(ByVal sender As Object, ByVal e As System.EventArgs)
            RaiseEvent ItemClick(Me, flpListBox.Controls.IndexOfKey(sender.name))
        End Sub

    End Class

End Namespace