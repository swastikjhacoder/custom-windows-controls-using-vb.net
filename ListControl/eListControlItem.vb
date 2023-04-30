Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms

Namespace UIControls.ListControl
    Public Class eListControlItem

        Public Event SelectionChanged(ByVal sender As Object)

        Friend WithEvents tmrMouseLeave As New System.Windows.Forms.Timer With {.Interval = 10}

#Region "Properties"

        Dim mImage As Image
        Public Property Image() As Image
            Get
                Return mImage
            End Get
            Set(ByVal value As Image)
                mImage = value
                Refresh()
            End Set
        End Property

        Dim mSong As String = ""
        Public Property Song() As String
            Get
                Return mSong
            End Get
            Set(ByVal value As String)
                mSong = value
                Refresh()
            End Set
        End Property

        Dim mArtist As String = ""
        Public Property Artist() As String
            Get
                Return mArtist
            End Get
            Set(ByVal value As String)
                mArtist = value
                Refresh()
            End Set
        End Property

        Dim mAlbum As String = ""
        Public Property Album() As String
            Get
                Return mAlbum
            End Get
            Set(ByVal value As String)
                mAlbum = value
                Refresh()
            End Set
        End Property

        Private mSelected As Boolean
        Public Property Selected() As Boolean
            Get
                Return mSelected
            End Get
            Set(ByVal value As Boolean)
                mSelected = value
                Refresh()
            End Set
        End Property


#End Region

#Region "Mouse coding"

        Private Enum MouseCapture
            Outside
            Inside
        End Enum
        Private Enum ButtonState
            ButtonUp
            ButtonDown
            Disabled
        End Enum
        Dim bState As Windows.Forms.ButtonState
        Dim bMouse As MouseCapture

        Private Sub ListControlItem_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
            If Selected = False Then
                Selected = True
                RaiseEvent SelectionChanged(Me)
            End If
        End Sub

        Private Sub metroRadioGroup_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown ', rdButton.MouseDown
            bState = ButtonState.ButtonDown
            Refresh()
        End Sub

        Private Sub metroRadioGroup_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
            bMouse = MouseCapture.Inside
            tmrMouseLeave.Start()
            Refresh()
        End Sub

        Private Sub metroRadioGroup_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp ', rdButton.MouseUp
            bState = ButtonState.ButtonUp
            Refresh()
        End Sub

        Private Sub tmrMouseLeave_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrMouseLeave.Tick
            Dim scrPT = Control.MousePosition
            Dim ctlPT As Point = Me.PointToClient(scrPT)
            '
            If ctlPT.X < 0 Or ctlPT.Y < 0 Or ctlPT.X > Me.Width Or ctlPT.Y > Me.Height Then
                ' Stop timer
                tmrMouseLeave.Stop()
                bMouse = MouseCapture.Outside
                Refresh()
            Else
                bMouse = MouseCapture.Inside
            End If
        End Sub
#End Region

#Region "Painting"

        Private Sub Paint_DrawBackground(ByVal gfx As Graphics)
            '
            Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)

            '/// Build a rounded rectangle
            Dim p As New GraphicsPath
            Const Roundness = 5
            p.StartFigure()
            p.AddArc(New Rectangle(rect.Left, rect.Top, Roundness, Roundness), 180, 90)
            p.AddLine(rect.Left + Roundness, 0, rect.Right - Roundness, 0)
            p.AddArc(New Rectangle(rect.Right - Roundness, 0, Roundness, Roundness), -90, 90)
            p.AddLine(rect.Right, Roundness, rect.Right, rect.Bottom - Roundness)
            p.AddArc(New Rectangle(rect.Right - Roundness, rect.Bottom - Roundness, Roundness, Roundness), 0, 90)
            p.AddLine(rect.Right - Roundness, rect.Bottom, rect.Left + Roundness, rect.Bottom)
            p.AddArc(New Rectangle(rect.Left, rect.Height - Roundness, Roundness, Roundness), 90, 90)
            p.CloseFigure()


            '/// Draw the background ///
            Dim ColorScheme As Color() = Nothing
            Dim brdr As SolidBrush = Nothing

            If bState = ButtonState.Disabled Then
                ' normal
                brdr = eColorSchemes.DisabledBorder
                ColorScheme = eColorSchemes.DisabledAllColor
            Else
                If mSelected Then
                    ' Selected
                    brdr = eColorSchemes.SelectedBorder

                    If bState = ButtonState.ButtonUp And bMouse = MouseCapture.Outside Then
                        ' normal
                        ColorScheme = eColorSchemes.SelectedNormal

                    ElseIf bState = ButtonState.ButtonUp And bMouse = MouseCapture.Inside Then
                        '  hover 
                        ColorScheme = eColorSchemes.SelectedHover

                    ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Outside Then
                        ' no one cares!
                        Exit Sub
                    ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Inside Then
                        ' pressed
                        ColorScheme = eColorSchemes.SelectedPressed
                    End If

                Else
                    ' Not selected
                    brdr = eColorSchemes.UnSelectedBorder

                    If bState = ButtonState.ButtonUp And bMouse = MouseCapture.Outside Then
                        ' normal
                        brdr = eColorSchemes.DisabledBorder
                        ColorScheme = eColorSchemes.UnSelectedNormal

                    ElseIf bState = ButtonState.ButtonUp And bMouse = MouseCapture.Inside Then
                        '  hover 
                        ColorScheme = eColorSchemes.UnSelectedHover

                    ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Outside Then
                        ' no one cares!
                        Exit Sub
                    ElseIf bState = ButtonState.ButtonDown And bMouse = MouseCapture.Inside Then
                        ' pressed
                        ColorScheme = eColorSchemes.UnSelectedPressed
                    End If

                End If
            End If

            ' Draw
            Dim b As LinearGradientBrush = New LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.Vertical)
            Dim blend As ColorBlend = New ColorBlend
            blend.Colors = ColorScheme
            blend.Positions = New Single() {0.0F, 0.1, 0.9F, 0.95F, 1.0F}
            b.InterpolationColors = blend
            gfx.FillPath(b, p)

            '// Draw border
            gfx.DrawPath(New Pen(brdr), p)

            '// Draw bottom border if Normal state (not hovered)
            If bMouse = MouseCapture.Outside Then
                rect = New Rectangle(rect.Left, Me.Height - 1, rect.Width, 1)
                b = New LinearGradientBrush(rect, Color.Blue, Color.Yellow, LinearGradientMode.Horizontal)
                blend = New ColorBlend
                blend.Colors = New Color() {Color.White, Color.LightGray, Color.White}
                blend.Positions = New Single() {0.0F, 0.5F, 1.0F}
                b.InterpolationColors = blend
                '
                gfx.FillRectangle(b, rect)
            End If
        End Sub

        Private Sub Paint_DrawButton(ByVal gfx As Graphics)

            Dim fnt As Font = Nothing
            Dim sz As SizeF = Nothing
            Dim layoutRect As RectangleF
            Dim SF As New StringFormat With {.Trimming = StringTrimming.EllipsisCharacter}
            Dim workingRect As New Rectangle(40, 0, Me.Width - 40 - 6, Me.Height)

            ' Draw song name
            fnt = New Font("Segoe UI Light", 14)
            sz = gfx.MeasureString(mSong, fnt)
            layoutRect = New RectangleF(40, 0, workingRect.Width, sz.Height)
            gfx.DrawString(mSong, fnt, Brushes.Black, layoutRect, SF)

            ' Draw artist name
            fnt = New Font("Segoe UI Light", 10)
            sz = gfx.MeasureString(mArtist, fnt)
            layoutRect = New RectangleF(42, 30, workingRect.Width, sz.Height)
            gfx.DrawString(mArtist, fnt, Brushes.Black, layoutRect, SF)

            ' Draw album name
            fnt = New Font("Segoe UI Light", 10)
            sz = gfx.MeasureString(mAlbum, fnt)
            layoutRect = New RectangleF(42, 25, workingRect.Width, sz.Height)
            gfx.DrawString(mAlbum, fnt, Brushes.Black, layoutRect, SF)

            ' Album Image
            If mImage IsNot Nothing Then
                gfx.DrawImage(mImage, New Point(7, 7))
            Else
                gfx.DrawImage(ImageList1.Images(0), New Point(7, 7))
            End If

        End Sub

        Private Sub PaintEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            Dim gfx = e.Graphics
            '
            Paint_DrawBackground(gfx)
            Paint_DrawButton(gfx)
        End Sub

#End Region

    End Class

End Namespace