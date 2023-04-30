
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports ESAR_Controls.UIControls.TabControl
Imports ESAR_Controls.UIControls.TabControl.BaseClasses

Namespace UIControls.TabControl
    <Designer(GetType(Design.eTabStripDesigner))> _
    <DefaultEvent("TabStripItemSelectionChanged")> _
    <DefaultProperty("Items")> _
    <ToolboxItem(True)> _
    <ToolboxBitmap(GetType(eTabStrip), "Resources.eTabStrip.bmp")> _
    Public Class eTabStrip
        Inherits BaseStyledPanel
        'Implements ISupportInitialize
        Implements IDisposable
#Region "Static Fields"

        Friend Shared PreferredWidth As Integer = 350
        Friend Shared PreferredHeight As Integer = 200

#End Region

#Region "Constants"

        Private Const DEF_HEADER_HEIGHT As Integer = 19
        Private Const DEF_GLYPH_WIDTH As Integer = 40

        Private DEF_START_POS As Integer = 10

#End Region

#Region "Events"

        Public Event TabStripItemClosing As TabStripItemClosingHandler
        Public Event TabStripItemSelectionChanged As TabStripItemChangedHandler
        Public Event MenuItemsLoading As HandledEventHandler
        Public Event MenuItemsLoaded As EventHandler
        Public Event TabStripItemClosed As EventHandler

#End Region

#Region "Fields"

        Private stripButtonRect As Rectangle = Rectangle.Empty
        Private m_selectedItem As eTabStripItem = Nothing
        Private menu As ContextMenuStrip = Nothing
        Private menuGlyph As eTabStripMenuGlyph = Nothing
        Private closeButton As eTabStripCloseButton = Nothing
        Private m_items As eTabStripItemCollection
        Private sf As StringFormat = Nothing
        Private Shared Shadows defaultFont As New Font("Tahoma", 8.25F, FontStyle.Regular)

        Private m_alwaysShowClose As Boolean = True
        Private isIniting As Boolean = False
        Private m_alwaysShowMenuGlyph As Boolean = True
        Private menuOpen As Boolean = False

#End Region

#Region "Methods"

#Region "Public"

        ''' <summary>
        ''' Returns hit test results
        ''' </summary>
        ''' <param name="pt"></param>
        ''' <returns></returns>
        Public Function HitTest(ByVal pt As Point) As HitTestResult
            If closeButton.Bounds.Contains(pt) Then
                Return HitTestResult.CloseButton
            End If

            If menuGlyph.Bounds.Contains(pt) Then
                Return HitTestResult.MenuGlyph
            End If

            If GetTabItemByPoint(pt) IsNot Nothing Then
                Return HitTestResult.TabItem
            End If

            'No other result is available.
            Return HitTestResult.None
        End Function

        ''' <summary>
        ''' Add a <see cref="eTabStripItem"/> to this control without selecting it.
        ''' </summary>
        ''' <param name="tabItem"></param>
        Public Sub AddTab(ByVal tabItem As eTabStripItem)
            AddTab(tabItem, False)
        End Sub

        ''' <summary>
        ''' Add a <see cref="eTabStripItem"/> to this control.
        ''' User can make the currently selected item or not.
        ''' </summary>
        ''' <param name="tabItem"></param>
        Public Sub AddTab(ByVal tabItem As eTabStripItem, ByVal autoSelect As Boolean)
            tabItem.Dock = DockStyle.Fill
            Items.Add(tabItem)

            If (autoSelect AndAlso tabItem.Visible) OrElse (tabItem.Visible AndAlso Items.DrawnCount < 1) Then
                SelectedItem = tabItem
                SelectItem(tabItem)
            End If
        End Sub

        ''' <summary>
        ''' Remove a <see cref="eTabStripItem"/> from this control.
        ''' </summary>
        ''' <param name="tabItem"></param>
        Public Sub RemoveTab(ByVal tabItem As eTabStripItem)
            Dim tabIndex As Integer = Items.IndexOf(tabItem)

            If tabIndex >= 0 Then
                UnSelectItem(tabItem)
                Items.Remove(tabItem)
            End If

            If Items.Count > 0 Then
                If RightToLeft = RightToLeft.No Then
                    If Items(tabIndex - 1) IsNot Nothing Then
                        SelectedItem = Items(tabIndex - 1)
                    Else
                        SelectedItem = Items.FirstVisible
                    End If
                Else
                    If Items(tabIndex + 1) IsNot Nothing Then
                        SelectedItem = Items(tabIndex + 1)
                    Else
                        SelectedItem = Items.LastVisible
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Get a <see cref="eTabStripItem"/> at provided point.
        ''' If no item was found, returns null value.
        ''' </summary>
        ''' <param name="pt"></param>
        ''' <returns></returns>
        Public Function GetTabItemByPoint(ByVal pt As Point) As eTabStripItem
            Dim item As eTabStripItem = Nothing
            Dim found As Boolean = False

            For i As Integer = 0 To Items.Count - 1
                Dim current As eTabStripItem = Items(i)

                If current.StripRect.Contains(pt) AndAlso current.Visible AndAlso current.IsDrawn Then
                    item = current
                    found = True
                End If

                If found Then
                    Exit For
                End If
            Next

            Return item
        End Function

        ''' <summary>
        ''' Display items menu
        ''' </summary>
        Public Overridable Sub ShowMenu()
            If menu.Visible = False AndAlso menu.Items.Count > 0 Then
                If RightToLeft = RightToLeft.No Then
                    menu.Show(Me, New Point(menuGlyph.Bounds.Left, menuGlyph.Bounds.Bottom))
                Else
                    menu.Show(Me, New Point(menuGlyph.Bounds.Right, menuGlyph.Bounds.Bottom))
                End If

                menuOpen = True
            End If
        End Sub

#End Region

#Region "Internal"

        Friend Sub UnDrawAll()
            For i As Integer = 0 To Items.Count - 1
                Items(i).IsDrawn = False
            Next
        End Sub

        Friend Sub SelectItem(ByVal tabItem As eTabStripItem)
            tabItem.Dock = DockStyle.Fill
            tabItem.Visible = True
            tabItem.Selected = True
        End Sub

        Friend Sub UnSelectItem(ByVal tabItem As eTabStripItem)
            'tabItem.Visible = false;
            tabItem.Selected = False
        End Sub

#End Region

#Region "Protected"

        ''' <summary>
        ''' Fires <see cref="TabStripItemClosing"/> event.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Friend Overridable Sub OnTabStripItemClosing(ByVal e As TabStripItemClosingEventArgs)
            RaiseEvent TabStripItemClosing(e)
        End Sub

        ''' <summary>
        ''' Fires <see cref="TabStripItemClosed"/> event.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Friend Overridable Sub OnTabStripItemClosed(ByVal e As EventArgs)
            RaiseEvent TabStripItemClosed(Me, e)
        End Sub

        ''' <summary>
        ''' Fires <see cref="MenuItemsLoading"/> event.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overridable Sub OnMenuItemsLoading(ByVal e As HandledEventArgs)
            RaiseEvent MenuItemsLoading(Me, e)
        End Sub
        ''' <summary>
        ''' Fires <see cref="MenuItemsLoaded"/> event.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overridable Sub OnMenuItemsLoaded(ByVal e As EventArgs)
            RaiseEvent MenuItemsLoaded(Me, e)
        End Sub

        ''' <summary>
        ''' Fires <see cref="TabStripItemSelectionChanged"/> event.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overridable Sub OnTabStripItemChanged(ByVal e As TabStripItemChangedEventArgs)
            RaiseEvent TabStripItemSelectionChanged(e)
        End Sub

        ''' <summary>
        ''' Loads menu items based on <see cref="eTabStripItem"/>s currently added
        ''' to this control.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overridable Sub OnMenuItemsLoad(ByVal e As EventArgs)
            menu.RightToLeft = RightToLeft
            menu.Items.Clear()

            For i As Integer = 0 To Items.Count - 1
                Dim item As eTabStripItem = Items(i)
                If Not item.Visible Then
                    Continue For
                End If

                Dim tItem As New ToolStripMenuItem(item.Title)
                tItem.Tag = item
                tItem.Image = item.Image
                menu.Items.Add(tItem)
            Next

            OnMenuItemsLoaded(EventArgs.Empty)
        End Sub

#End Region

#Region "Overrides"

        Protected Overrides Sub OnRightToLeftChanged(ByVal e As EventArgs)
            MyBase.OnRightToLeftChanged(e)
            UpdateLayout()
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            SetDefaultSelected()
            Dim borderRc As Rectangle = ClientRectangle
            borderRc.Width -= 1
            borderRc.Height -= 1

            If RightToLeft = RightToLeft.No Then
                DEF_START_POS = 10
            Else
                DEF_START_POS = stripButtonRect.Right
            End If

            e.Graphics.DrawRectangle(SystemPens.ControlDark, borderRc)
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

            '#Region "Draw Pages"

            For i As Integer = 0 To Items.Count - 1
                Dim currentItem As eTabStripItem = Items(i)
                If Not currentItem.Visible AndAlso Not DesignMode Then
                    Continue For
                End If

                OnCalcTabPage(e.Graphics, currentItem)
                currentItem.IsDrawn = False

                If Not AllowDraw(currentItem) Then
                    Continue For
                End If

                OnDrawTabPage(e.Graphics, currentItem)
            Next

            '#End Region

            '#Region "Draw UnderPage Line"

            If RightToLeft = RightToLeft.No Then
                If Items.DrawnCount = 0 OrElse Items.VisibleCount = 0 Then
                    e.Graphics.DrawLine(SystemPens.ControlDark, New Point(0, DEF_HEADER_HEIGHT), New Point(ClientRectangle.Width, DEF_HEADER_HEIGHT))
                ElseIf SelectedItem IsNot Nothing AndAlso SelectedItem.IsDrawn Then
                    Dim [end] As New Point(CInt(SelectedItem.StripRect.Left) - 9, DEF_HEADER_HEIGHT)
                    e.Graphics.DrawLine(SystemPens.ControlDark, New Point(0, DEF_HEADER_HEIGHT), [end])
                    [end].X += CInt(SelectedItem.StripRect.Width) + 10
                    e.Graphics.DrawLine(SystemPens.ControlDark, [end], New Point(ClientRectangle.Width, DEF_HEADER_HEIGHT))
                End If
            Else
                If Items.DrawnCount = 0 OrElse Items.VisibleCount = 0 Then
                    e.Graphics.DrawLine(SystemPens.ControlDark, New Point(0, DEF_HEADER_HEIGHT), New Point(ClientRectangle.Width, DEF_HEADER_HEIGHT))
                ElseIf SelectedItem IsNot Nothing AndAlso SelectedItem.IsDrawn Then
                    Dim [end] As New Point(CInt(SelectedItem.StripRect.Left), DEF_HEADER_HEIGHT)
                    e.Graphics.DrawLine(SystemPens.ControlDark, New Point(0, DEF_HEADER_HEIGHT), [end])
                    [end].X += CInt(SelectedItem.StripRect.Width) + 20
                    e.Graphics.DrawLine(SystemPens.ControlDark, [end], New Point(ClientRectangle.Width, DEF_HEADER_HEIGHT))
                End If
            End If

            '#End Region

            '#Region "Draw Menu and Close Glyphs"

            If AlwaysShowMenuGlyph OrElse Items.DrawnCount > Items.VisibleCount Then
                menuGlyph.DrawGlyph(e.Graphics)
            End If

            If AlwaysShowClose OrElse (SelectedItem IsNot Nothing AndAlso SelectedItem.CanClose) Then
                closeButton.DrawCross(e.Graphics)
            End If

            '#End Region
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            MyBase.OnMouseDown(e)

            If e.Button <> MouseButtons.Left Then
                Return
            End If

            Dim result As HitTestResult = HitTest(e.Location)
            If result = HitTestResult.MenuGlyph Then
                Dim args As New HandledEventArgs(False)
                OnMenuItemsLoading(args)

                If Not args.Handled Then
                    OnMenuItemsLoad(EventArgs.Empty)
                End If

                ShowMenu()
            ElseIf result = HitTestResult.CloseButton Then
                If SelectedItem IsNot Nothing Then
                    Dim args As New TabStripItemClosingEventArgs(SelectedItem)
                    OnTabStripItemClosing(args)
                    If Not args.Cancel AndAlso SelectedItem.CanClose Then
                        RemoveTab(SelectedItem)
                        OnTabStripItemClosed(EventArgs.Empty)
                    End If
                End If
            ElseIf result = HitTestResult.TabItem Then
                Dim item As eTabStripItem = GetTabItemByPoint(e.Location)
                If item IsNot Nothing Then
                    SelectedItem = item
                End If
            End If

            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)

            If menuGlyph.Bounds.Contains(e.Location) Then
                menuGlyph.IsMouseOver = True
                Invalidate(menuGlyph.Bounds)
            Else
                If menuGlyph.IsMouseOver AndAlso Not menuOpen Then
                    menuGlyph.IsMouseOver = False
                    Invalidate(menuGlyph.Bounds)
                End If
            End If

            If closeButton.Bounds.Contains(e.Location) Then
                closeButton.IsMouseOver = True
                Invalidate(closeButton.Bounds)
            Else
                If closeButton.IsMouseOver Then
                    closeButton.IsMouseOver = False
                    Invalidate(closeButton.Bounds)
                End If
            End If
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
            MyBase.OnMouseLeave(e)
            menuGlyph.IsMouseOver = False
            Invalidate(menuGlyph.Bounds)

            closeButton.IsMouseOver = False
            Invalidate(closeButton.Bounds)
        End Sub

        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            MyBase.OnSizeChanged(e)
            If isIniting Then
                Return
            End If

            UpdateLayout()
        End Sub

#End Region

#Region "Private"

        Private Function AllowDraw(ByVal item As eTabStripItem) As Boolean
            Dim result As Boolean = True

            If RightToLeft = RightToLeft.No Then
                If item.StripRect.Right >= stripButtonRect.Width Then
                    result = False
                End If
            Else
                If item.StripRect.Left <= stripButtonRect.Left Then
                    Return False
                End If
            End If

            Return result
        End Function

        Private Sub SetDefaultSelected()
            If m_selectedItem Is Nothing AndAlso Items.Count > 0 Then
                SelectedItem = Items(0)
            End If

            For i As Integer = 0 To Items.Count - 1
                Dim itm As eTabStripItem = Items(i)
                itm.Dock = DockStyle.Fill
            Next
        End Sub

        Private Sub OnMenuItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
            Dim clickedItem As eTabStripItem = DirectCast(e.ClickedItem.Tag, eTabStripItem)
            SelectedItem = clickedItem
        End Sub

        Private Sub OnMenuVisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
            If menu.Visible = False Then
                menuOpen = False
            End If
        End Sub

        Private Sub OnCalcTabPage(ByVal g As Graphics, ByVal currentItem As eTabStripItem)
            Dim currentFont As Font = Font
            If currentItem Is SelectedItem Then
                currentFont = New Font(Font, FontStyle.Bold)
            End If

            Dim textSize As SizeF = g.MeasureString(currentItem.Title, currentFont, New SizeF(200, 10), sf)
            textSize.Width += 20

            If RightToLeft = RightToLeft.No Then
                Dim buttonRect As New RectangleF(DEF_START_POS, 3, textSize.Width, 17)
                currentItem.StripRect = buttonRect
                DEF_START_POS += CInt(textSize.Width)
            Else
                Dim buttonRect As New RectangleF(DEF_START_POS - textSize.Width + 1, 3, textSize.Width - 1, 17)
                currentItem.StripRect = buttonRect
                DEF_START_POS -= CInt(textSize.Width)
            End If
        End Sub

        Private Sub OnDrawTabPage(ByVal g As Graphics, ByVal currentItem As eTabStripItem)
            Dim isFirstTab As Boolean = Items.IndexOf(currentItem) = 0
            Dim currentFont As Font = Font

            If currentItem Is SelectedItem Then
                currentFont = New Font(Font, FontStyle.Bold)
            End If

            Dim textSize As SizeF = g.MeasureString(currentItem.Title, currentFont, New SizeF(200, 10), sf)
            textSize.Width += 20
            Dim buttonRect As RectangleF = currentItem.StripRect

            Dim path As New GraphicsPath()
            Dim brush As LinearGradientBrush
            Dim mtop As Integer = 3

            '#Region "Draw Not Right-To-Left Tab"

            If RightToLeft = RightToLeft.No Then
                If currentItem Is SelectedItem OrElse isFirstTab Then
                    path.AddLine(buttonRect.Left - 10, buttonRect.Bottom - 1, buttonRect.Left + (buttonRect.Height / 2) - 4, mtop + 4)
                Else
                    path.AddLine(buttonRect.Left, buttonRect.Bottom - 1, buttonRect.Left, buttonRect.Bottom - (buttonRect.Height / 2) - 2)
                    path.AddLine(buttonRect.Left, buttonRect.Bottom - (buttonRect.Height / 2) - 3, buttonRect.Left + (buttonRect.Height / 2) - 4, mtop + 3)
                End If

                path.AddLine(buttonRect.Left + (buttonRect.Height / 2) + 2, mtop, buttonRect.Right - 3, mtop)
                path.AddLine(buttonRect.Right, mtop + 2, buttonRect.Right, buttonRect.Bottom - 1)
                path.AddLine(buttonRect.Right - 4, buttonRect.Bottom - 1, buttonRect.Left, buttonRect.Bottom - 1)
                path.CloseFigure()

                If currentItem Is SelectedItem Then
                    brush = New LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Window, LinearGradientMode.Vertical)
                Else
                    brush = New LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Control, LinearGradientMode.Vertical)
                End If

                g.FillPath(brush, path)
                g.DrawPath(SystemPens.ControlDark, path)

                If currentItem Is SelectedItem Then
                    g.DrawLine(New Pen(brush), buttonRect.Left - 9, buttonRect.Height + 2, buttonRect.Left + buttonRect.Width - 1, buttonRect.Height + 2)
                End If

                Dim textLoc As New PointF(buttonRect.Left + buttonRect.Height - 4, buttonRect.Top + (buttonRect.Height / 2) - (textSize.Height / 2) - 3)
                Dim textRect As RectangleF = buttonRect
                textRect.Location = textLoc
                textRect.Width = buttonRect.Width - (textRect.Left - buttonRect.Left) - 4
                textRect.Height = textSize.Height + currentFont.Size / 2

                If currentItem Is SelectedItem Then
                    'textRect.Y -= 2;
                    g.DrawString(currentItem.Title, currentFont, New SolidBrush(ForeColor), textRect, sf)
                Else
                    g.DrawString(currentItem.Title, currentFont, New SolidBrush(ForeColor), textRect, sf)
                End If
            End If

            '#End Region

            '#Region "Draw Right-To-Left Tab"

            If RightToLeft = RightToLeft.Yes Then
                If currentItem Is SelectedItem OrElse isFirstTab Then
                    path.AddLine(buttonRect.Right + 10, buttonRect.Bottom - 1, buttonRect.Right - (buttonRect.Height / 2) + 4, mtop + 4)
                Else
                    path.AddLine(buttonRect.Right, buttonRect.Bottom - 1, buttonRect.Right, buttonRect.Bottom - (buttonRect.Height / 2) - 2)
                    path.AddLine(buttonRect.Right, buttonRect.Bottom - (buttonRect.Height / 2) - 3, buttonRect.Right - (buttonRect.Height / 2) + 4, mtop + 3)
                End If

                path.AddLine(buttonRect.Right - (buttonRect.Height / 2) - 2, mtop, buttonRect.Left + 3, mtop)
                path.AddLine(buttonRect.Left, mtop + 2, buttonRect.Left, buttonRect.Bottom - 1)
                path.AddLine(buttonRect.Left + 4, buttonRect.Bottom - 1, buttonRect.Right, buttonRect.Bottom - 1)
                path.CloseFigure()

                If currentItem Is SelectedItem Then
                    brush = New LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Window, LinearGradientMode.Vertical)
                Else
                    brush = New LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Control, LinearGradientMode.Vertical)
                End If

                g.FillPath(brush, path)
                g.DrawPath(SystemPens.ControlDark, path)

                If currentItem Is SelectedItem Then
                    g.DrawLine(New Pen(brush), buttonRect.Right + 9, buttonRect.Height + 2, buttonRect.Right - buttonRect.Width + 1, buttonRect.Height + 2)
                End If

                Dim textLoc As New PointF(buttonRect.Left + 2, buttonRect.Top + (buttonRect.Height / 2) - (textSize.Height / 2) - 2)
                Dim textRect As RectangleF = buttonRect
                textRect.Location = textLoc
                textRect.Width = buttonRect.Width - (textRect.Left - buttonRect.Left) - 10
                textRect.Height = textSize.Height + currentFont.Size / 2

                If currentItem Is SelectedItem Then
                    textRect.Y -= 1
                    g.DrawString(currentItem.Title, currentFont, New SolidBrush(ForeColor), textRect, sf)
                Else
                    g.DrawString(currentItem.Title, currentFont, New SolidBrush(ForeColor), textRect, sf)

                    'g.FillRectangle(Brushes.Red, textRect);
                End If
            End If

            '#End Region

            currentItem.IsDrawn = True
        End Sub

        Private Sub UpdateLayout()
            If RightToLeft = RightToLeft.No Then
                sf.Trimming = StringTrimming.EllipsisCharacter
                sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.NoWrap
                sf.FormatFlags = sf.FormatFlags And StringFormatFlags.DirectionRightToLeft

                stripButtonRect = New Rectangle(0, 0, ClientSize.Width - DEF_GLYPH_WIDTH - 2, 10)
                menuGlyph.Bounds = New Rectangle(ClientSize.Width - DEF_GLYPH_WIDTH, 2, 16, 16)
                closeButton.Bounds = New Rectangle(ClientSize.Width - 20, 2, 16, 16)
            Else
                sf.Trimming = StringTrimming.EllipsisCharacter
                sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.NoWrap
                sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.DirectionRightToLeft

                stripButtonRect = New Rectangle(DEF_GLYPH_WIDTH + 2, 0, ClientSize.Width - DEF_GLYPH_WIDTH - 15, 10)
                closeButton.Bounds = New Rectangle(4, 2, 16, 16)
                'ClientSize.Width - DEF_GLYPH_WIDTH, 2, 16, 16);
                'this.ClientSize.Width - 20, 2, 16, 16);
                menuGlyph.Bounds = New Rectangle(20 + 4, 2, 16, 16)
            End If

            DockPadding.Top = DEF_HEADER_HEIGHT + 1
            DockPadding.Bottom = 1
            DockPadding.Right = 1
            DockPadding.Left = 1
        End Sub

        Private Sub OnCollectionChanged(ByVal sender As Object, ByVal e As CollectionChangeEventArgs)
            Dim itm As eTabStripItem = DirectCast(e.Element, eTabStripItem)

            If e.Action = CollectionChangeAction.Add Then
                Controls.Add(itm)
                OnTabStripItemChanged(New TabStripItemChangedEventArgs(itm, eTabStripItemChangeTypes.Added))
            ElseIf e.Action = CollectionChangeAction.Remove Then
                Controls.Remove(itm)
                OnTabStripItemChanged(New TabStripItemChangedEventArgs(itm, eTabStripItemChangeTypes.Removed))
            Else
                OnTabStripItemChanged(New TabStripItemChangedEventArgs(itm, eTabStripItemChangeTypes.Changed))
            End If

            UpdateLayout()
            Invalidate()
        End Sub

#End Region

#End Region

#Region "Ctor"

        Public Sub New()
            BeginInit()

            SetStyle(ControlStyles.ContainerControl, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.Selectable, True)

            m_items = New eTabStripItemCollection()
            AddHandler m_items.CollectionChanged, AddressOf OnCollectionChanged
            MyBase.Size = New Size(350, 200)

            menu = New ContextMenuStrip()
            menu.Renderer = ToolStripRenderer
            AddHandler menu.ItemClicked, AddressOf OnMenuItemClicked
            AddHandler menu.VisibleChanged, AddressOf OnMenuVisibleChanged

            menuGlyph = New eTabStripMenuGlyph(ToolStripRenderer)
            closeButton = New eTabStripCloseButton(ToolStripRenderer)
            Font = defaultFont
            sf = New StringFormat()

            EndInit()

            UpdateLayout()
        End Sub

#End Region

#Region "Props"

        <RefreshProperties(RefreshProperties.All)> _
        Public Property SelectedItem() As eTabStripItem
            Get
                Return m_selectedItem
            End Get
            Set(ByVal value As eTabStripItem)
                If m_selectedItem Is value Then
                    Return
                End If

                If value Is Nothing AndAlso Items.Count > 0 Then
                    Dim itm As eTabStripItem = Items(0)
                    If itm.Visible Then
                        m_selectedItem = itm
                        m_selectedItem.Selected = True
                        m_selectedItem.Dock = DockStyle.Fill
                    End If
                Else
                    m_selectedItem = value
                End If

                For Each itm As eTabStripItem In Items
                    If itm Is m_selectedItem Then
                        SelectItem(itm)
                        itm.Dock = DockStyle.Fill
                        itm.Show()
                    Else
                        UnSelectItem(itm)
                        itm.Hide()
                    End If
                Next

                SelectItem(m_selectedItem)
                Invalidate()

                If Not m_selectedItem.IsDrawn Then
                    Items.MoveTo(0, m_selectedItem)
                    Invalidate()
                End If

                OnTabStripItemChanged(New TabStripItemChangedEventArgs(m_selectedItem, eTabStripItemChangeTypes.SelectionChanged))
            End Set
        End Property

        <DefaultValue(True)> _
        Public Property AlwaysShowMenuGlyph() As Boolean
            Get
                Return m_alwaysShowMenuGlyph
            End Get
            Set(ByVal value As Boolean)
                If m_alwaysShowMenuGlyph = value Then
                    Return
                End If

                m_alwaysShowMenuGlyph = value
                Invalidate()
            End Set
        End Property

        <DefaultValue(True)> _
        Public Property AlwaysShowClose() As Boolean
            Get
                Return m_alwaysShowClose
            End Get
            Set(ByVal value As Boolean)
                If m_alwaysShowClose = value Then
                    Return
                End If

                m_alwaysShowClose = value
                Invalidate()
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Items() As eTabStripItemCollection
            Get
                Return m_items
            End Get
        End Property

        <DefaultValue(GetType(Size), "350,200")> _
        Public Shadows Property Size() As Size
            Get
                Return MyBase.Size
            End Get
            Set(ByVal value As Size)
                If MyBase.Size = value Then
                    Return
                End If

                MyBase.Size = value
                UpdateLayout()
            End Set
        End Property

        ''' <summary>
        ''' DesignerSerializationVisibility
        ''' </summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows ReadOnly Property Controls() As ControlCollection
            Get
                Return MyBase.Controls
            End Get
        End Property

#End Region

#Region "ShouldSerialize"

        Public Function ShouldSerializeFont() As Boolean
            Return Font IsNot Nothing AndAlso Not Font.Equals(defaultFont)
        End Function

        Public Function ShouldSerializeSelectedItem() As Boolean
            Return True
        End Function

        Public Function ShouldSerializeItems() As Boolean
            Return m_items.Count > 0
        End Function

        Public Shadows Sub ResetFont()
            Font = defaultFont
        End Sub

#End Region

#Region "ISupportInitialize Members"

        Public Sub BeginInit()
            isIniting = True
        End Sub

        Public Sub EndInit()
            isIniting = False
        End Sub

#End Region

#Region "IDisposable"

        '''<summary>
        '''Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        '''</summary>
        '''<filterpriority>2</filterpriority>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                RemoveHandler m_items.CollectionChanged, AddressOf OnCollectionChanged
                RemoveHandler menu.ItemClicked, AddressOf OnMenuItemClicked
                RemoveHandler menu.VisibleChanged, AddressOf OnMenuVisibleChanged

                For Each item As eTabStripItem In m_items
                    If item IsNot Nothing AndAlso Not item.IsDisposed Then
                        item.Dispose()
                    End If
                Next

                If menu IsNot Nothing AndAlso Not menu.IsDisposed Then
                    menu.Dispose()
                End If

                If sf IsNot Nothing Then
                    sf.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub

#End Region
    End Class
End Namespace