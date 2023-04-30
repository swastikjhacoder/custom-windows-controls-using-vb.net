Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization

Namespace UIControls.ComboBox
    Public Class eComboBoxMultiColumn
        Inherits Windows.Forms.ComboBox
        Private _AutoComplete As Boolean
        Private _AutoDropdown As Boolean
        Private _BackColorEven As Color = Color.White
        Private _BackColorOdd As Color = Color.White
        Private _ColumnNameString As String = ""
        Private _ColumnWidthDefault As Integer = 75
        Private _ColumnWidthString As String = ""
        Private _LinkedColumnIndex As Integer
        Private _LinkedTextBox As Windows.Forms.TextBox
        Private _TotalWidth As Integer = 0
        Private _ValueMemberColumnIndex As Integer = 0

        Private _ColumnNames As New Collection(Of String)()
        Private _ColumnWidths As New Collection(Of Integer)()

        Public Sub New()
            DrawMode = DrawMode.OwnerDrawVariable

            ' If all of your boxes will be RightToLeft, uncomment 
            ' the following line to make RTL the default.
            'RightToLeft = RightToLeft.Yes;

            ' Remove the Context Menu to disable pasting 
            ContextMenu = New ContextMenu()
        End Sub

        Public Event OpenSearchForm As System.EventHandler

        Public Property AutoComplete() As Boolean
            Get
                Return _AutoComplete
            End Get
            Set(ByVal value As Boolean)
                _AutoComplete = value
            End Set
        End Property

        Public Property AutoDropdown() As Boolean
            Get
                Return _AutoDropdown
            End Get
            Set(ByVal value As Boolean)
                _AutoDropdown = value
            End Set
        End Property

        Public Property BackColorEven() As Color
            Get
                Return _BackColorEven
            End Get
            Set(ByVal value As Color)
                _BackColorEven = value
            End Set
        End Property

        Public Property BackColorOdd() As Color
            Get
                Return _BackColorOdd
            End Get
            Set(ByVal value As Color)
                _BackColorOdd = value
            End Set
        End Property

        Public ReadOnly Property ColumnNameCollection() As Collection(Of String)
            Get
                Return _ColumnNames
            End Get
        End Property

        Public Property ColumnNames() As String
            Get
                Return _ColumnNameString
            End Get

            Set(ByVal value As String)
                ' If the column string is blank, leave it blank.
                ' The default width will be used for all columns.
                If Not Convert.ToBoolean(value.Trim().Length) Then
                    _ColumnNameString = ""
                ElseIf value IsNot Nothing Then
                    Dim delimiterChars As Char() = {","c, ";"c, ":"c}
                    Dim columnNames__1 As String() = value.Split(delimiterChars)

                    If Not DesignMode Then
                        _ColumnNames.Clear()
                    End If

                    ' After splitting the string into an array, iterate
                    ' through the strings and check that they're all valid.
                    For Each s As String In columnNames__1
                        ' Does it have length?
                        If Convert.ToBoolean(s.Trim().Length) Then
                            If Not DesignMode Then
                                _ColumnNames.Add(s.Trim())
                            End If
                        Else
                            ' The value is blank
                            Throw New NotSupportedException("Column names can not be blank.")
                        End If
                    Next
                    _ColumnNameString = value
                End If
            End Set
        End Property

        Public ReadOnly Property ColumnWidthCollection() As Collection(Of Integer)
            Get
                Return _ColumnWidths
            End Get
        End Property

        Public Property ColumnWidthDefault() As Integer
            Get
                Return _ColumnWidthDefault
            End Get
            Set(ByVal value As Integer)
                _ColumnWidthDefault = value
            End Set
        End Property

        Public Property ColumnWidths() As String
            Get
                Return _ColumnWidthString
            End Get

            Set(ByVal value As String)
                ' If the column string is blank, leave it blank.
                ' The default width will be used for all columns.
                If Not Convert.ToBoolean(value.Trim().Length) Then
                    _ColumnWidthString = ""
                ElseIf value IsNot Nothing Then
                    Dim delimiterChars As Char() = {","c, ";"c, ":"c}
                    Dim columnWidths__1 As String() = value.Split(delimiterChars)
                    Dim invalidValue As String = ""
                    Dim invalidIndex As Integer = -1
                    Dim idx As Integer = 1
                    Dim intValue As Integer

                    ' After splitting the string into an array, iterate
                    ' through the strings and check that they're all integers
                    ' or blanks
                    For Each s As String In columnWidths__1
                        ' If it has length, test if it's an integer
                        If Convert.ToBoolean(s.Trim().Length) Then
                            ' It's not an integer. Flag the offending value.
                            If Not Integer.TryParse(s, intValue) Then
                                invalidIndex = idx
                                invalidValue = s
                            Else
                                ' The value was okay. Increment the item index.
                                idx += 1
                            End If
                        Else
                            ' The value is a space. Use the default width.
                            idx += 1
                        End If
                    Next

                    ' If an invalid value was found, raise an exception.
                    If invalidIndex > -1 Then
                        Dim errMsg As String

                        errMsg = (Convert.ToString("Invalid column width '") & invalidValue) + "' located at column " + invalidIndex.ToString()
                        Throw New ArgumentOutOfRangeException(errMsg)
                    Else
                        ' The string is fine
                        _ColumnWidthString = value

                        ' Only set the values of the collections at runtime.
                        ' Setting them at design time doesn't accomplish 
                        ' anything and causes errors since the collections 
                        ' don't exist at design time.
                        If Not DesignMode Then
                            _ColumnWidths.Clear()
                            For Each s As String In columnWidths__1
                                ' Initialize a column width to an integer
                                If Convert.ToBoolean(s.Trim().Length) Then
                                    _ColumnWidths.Add(Convert.ToInt32(s))
                                Else
                                    ' Initialize the column to the default
                                    _ColumnWidths.Add(_ColumnWidthDefault)
                                End If
                            Next

                            ' If the column is bound to data, set the column widths
                            ' for any columns that aren't explicitly set by the 
                            ' string value entered by the programmer
                            If DataManager IsNot Nothing Then
                                InitializeColumns()
                            End If
                        End If
                    End If
                End If
            End Set
        End Property

        Public Shadows Property DrawMode() As DrawMode
            Get
                Return MyBase.DrawMode
            End Get
            Set(ByVal value As DrawMode)
                If value <> DrawMode.OwnerDrawVariable Then
                    Throw New NotSupportedException("Needs to be DrawMode.OwnerDrawVariable")
                End If
                MyBase.DrawMode = value
            End Set
        End Property

        Public Shadows Property DropDownStyle() As ComboBoxStyle
            Get
                Return MyBase.DropDownStyle
            End Get
            Set(ByVal value As ComboBoxStyle)
                If value <> ComboBoxStyle.DropDown Then
                    Throw New NotSupportedException("ComboBoxStyle.DropDown is the only supported style")
                End If
                MyBase.DropDownStyle = value
            End Set
        End Property

        Public Property LinkedColumnIndex() As Integer
            Get
                Return _LinkedColumnIndex
            End Get
            Set(ByVal value As Integer)
                If value < 0 Then
                    Throw New ArgumentOutOfRangeException("A column index can not be negative")
                End If
                _LinkedColumnIndex = value
            End Set
        End Property

        Public Property LinkedTextBox() As System.Windows.Forms.TextBox
            Get
                Return _LinkedTextBox
            End Get
            Set(ByVal value As System.Windows.Forms.TextBox)
                _LinkedTextBox = value

                If _LinkedTextBox IsNot Nothing Then
                    ' Set any default properties of the Linked Textbox here
                    _LinkedTextBox.[ReadOnly] = True
                    _LinkedTextBox.TabStop = False
                End If
            End Set
        End Property

        Public ReadOnly Property TotalWidth() As Integer
            Get
                Return _TotalWidth
            End Get
        End Property

        Protected Overrides Sub OnDataSourceChanged(ByVal e As EventArgs)
            MyBase.OnDataSourceChanged(e)

            InitializeColumns()
        End Sub

        Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
            MyBase.OnDrawItem(e)

            If DesignMode Then
                Return
            End If

            e.DrawBackground()

            Dim boundsRect As Rectangle = e.Bounds
            Dim lastRight As Integer = 0

            Dim brushForeColor As Color
            If (e.State And DrawItemState.Selected) = 0 Then
                ' Item is not selected. Use BackColorOdd & BackColorEven
                Dim backColor As Color
                backColor = If(Convert.ToBoolean(e.Index Mod 2), _BackColorOdd, _BackColorEven)
                Using brushBackColor As New SolidBrush(backColor)
                    e.Graphics.FillRectangle(brushBackColor, e.Bounds)
                End Using
                brushForeColor = Color.Black
            Else
                ' Item is selected. Use ForeColor = White
                brushForeColor = Color.White
            End If

            Using linePen As New Pen(SystemColors.GrayText)
                Using brush As New SolidBrush(brushForeColor)
                    If Not Convert.ToBoolean(_ColumnNames.Count) Then
                        e.Graphics.DrawString(Convert.ToString(Items(e.Index)), Font, brush, boundsRect)
                    Else
                        ' If the ComboBox is displaying a RightToLeft language, draw it this way.
                        If RightToLeft.Equals(RightToLeft.Yes) Then
                            ' Define a StringFormat object to make the string display RTL.
                            Dim rtl As New StringFormat()
                            rtl.Alignment = StringAlignment.Near
                            rtl.FormatFlags = StringFormatFlags.DirectionRightToLeft

                            ' Draw the strings in reverse order from high column index to zero column index.
                            For colIndex As Integer = _ColumnNames.Count - 1 To 0 Step -1
                                If Convert.ToBoolean(_ColumnWidths(colIndex)) Then
                                    Dim item As String = Convert.ToString(FilterItemOnProperty(Items(e.Index), _ColumnNames(colIndex)))

                                    boundsRect.X = lastRight
                                    boundsRect.Width = CInt(_ColumnWidths(colIndex))
                                    lastRight = boundsRect.Right

                                    ' Draw the string with the RTL object.
                                    e.Graphics.DrawString(item, Font, brush, boundsRect, rtl)

                                    If colIndex > 0 Then
                                        e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom)
                                    End If
                                End If
                            Next
                        Else
                            ' If the ComboBox is displaying a LeftToRight language, draw it this way.
                            ' Display the strings in ascending order from zero to the highest column.
                            For colIndex As Integer = 0 To _ColumnNames.Count - 1
                                If Convert.ToBoolean(_ColumnWidths(colIndex)) Then
                                    Dim item As String = Convert.ToString(FilterItemOnProperty(Items(e.Index), _ColumnNames(colIndex)))

                                    boundsRect.X = lastRight
                                    boundsRect.Width = CInt(_ColumnWidths(colIndex))
                                    lastRight = boundsRect.Right
                                    e.Graphics.DrawString(item, Font, brush, boundsRect)

                                    If colIndex < _ColumnNames.Count - 1 Then
                                        e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom)
                                    End If
                                End If
                            Next
                        End If
                    End If
                End Using
            End Using

            e.DrawFocusRectangle()
        End Sub

        Protected Overrides Sub OnDropDown(ByVal e As EventArgs)
            MyBase.OnDropDown(e)

            If _TotalWidth > 0 Then
                If Items.Count > MaxDropDownItems Then
                    ' The vertical scrollbar is present. Add its width to the total.
                    ' If you don't then RightToLeft languages will have a few characters obscured.
                    Me.DropDownWidth = _TotalWidth + SystemInformation.VerticalScrollBarWidth
                Else
                    Me.DropDownWidth = _TotalWidth
                End If
            End If
        End Sub

        Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
            ' Use the Delete or Escape Key to blank out the ComboBox and
            ' allow the user to type in a new value
            If (e.KeyCode = Keys.Delete) OrElse (e.KeyCode = Keys.Escape) Then
                SelectedIndex = -1
                Text = ""
                If _LinkedTextBox IsNot Nothing Then
                    _LinkedTextBox.Text = ""
                End If
            ElseIf e.KeyCode = Keys.F3 Then
                ' Fire the OpenSearchForm Event
                RaiseEvent OpenSearchForm(Me, System.EventArgs.Empty)
            End If
        End Sub

        ' Some of the code for OnKeyPress was derived from some VB.NET code  
        ' posted by Laurent Muller as a suggested improvement for another control.
        ' http://www.codeproject.com/vb/net/autocomplete_combobox.asp?df=100&forumid=3716&select=579095#xx579095xx
        Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
            Dim idx As Integer = -1
            Dim toFind As String

            DroppedDown = _AutoDropdown
            If Not [Char].IsControl(e.KeyChar) Then
                If _AutoComplete Then
                    toFind = Text.Substring(0, SelectionStart) + e.KeyChar
                    idx = FindStringExact(toFind)

                    If idx = -1 Then
                        ' An exact match for the whole string was not found
                        ' Find a substring instead.
                        idx = FindString(toFind)
                    Else
                        ' An exact match was found. Close the dropdown.
                        DroppedDown = False
                    End If

                    If idx <> -1 Then
                        ' The substring was found.
                        SelectedIndex = idx
                        SelectionStart = toFind.Length
                        SelectionLength = Text.Length - SelectionStart
                    Else
                        ' The last keystroke did not create a valid substring.
                        ' If the substring is not found, cancel the keypress
                        e.KeyChar = CChar(ChrW(0))
                    End If
                Else
                    ' AutoComplete = false. Treat it like a DropDownList by finding the
                    ' KeyChar that was struck starting from the current index
                    idx = FindString(e.KeyChar.ToString(), SelectedIndex)

                    If idx <> -1 Then
                        SelectedIndex = idx
                    End If
                End If
            End If

            ' Do no allow the user to backspace over characters. Treat it like
            ' a left arrow instead. The user must not be allowed to change the 
            ' value in the ComboBox. 
            ' A Backspace Key is hit
            ' AutoComplete = true
            If (e.KeyChar = CChar(ChrW(Keys.Back))) AndAlso (_AutoComplete) AndAlso (Convert.ToBoolean(SelectionStart)) Then
                ' And the SelectionStart is positive
                ' Find a substring that is one character less the the current selection.
                ' This mimicks moving back one space with an arrow key. This substring should
                ' always exist since we don't allow invalid selections to be typed. If you're
                ' on the 3rd character of a valid code, then the first two characters have to 
                ' be valid. Moving back to them and finding the 1st occurrence should never fail.
                toFind = Text.Substring(0, SelectionStart - 1)
                idx = FindString(toFind)

                If idx <> -1 Then
                    SelectedIndex = idx
                    SelectionStart = toFind.Length
                    SelectionLength = Text.Length - SelectionStart
                End If
            End If

            ' e.Handled is always true. We handle every keystroke programatically.
            e.Handled = True
        End Sub

        Protected Overrides Sub OnSelectedValueChanged(ByVal e As EventArgs)
            MyBase.OnSelectedValueChanged(e)
            'Added after version 1.3 on 01/31/2008
            If _LinkedTextBox IsNot Nothing Then
                If _LinkedColumnIndex < _ColumnNames.Count Then
                    _LinkedTextBox.Text = Convert.ToString(FilterItemOnProperty(SelectedItem, _ColumnNames(_LinkedColumnIndex)))
                End If
            End If
        End Sub

        Protected Overrides Sub OnValueMemberChanged(ByVal e As EventArgs)
            MyBase.OnValueMemberChanged(e)

            InitializeValueMemberColumn()
        End Sub

        Private Sub InitializeColumns()
            If Not Convert.ToBoolean(_ColumnNameString.Length) Then
                Dim propertyDescriptorCollection As PropertyDescriptorCollection = DataManager.GetItemProperties()

                _TotalWidth = 0
                _ColumnNames.Clear()

                For colIndex As Integer = 0 To propertyDescriptorCollection.Count - 1
                    _ColumnNames.Add(propertyDescriptorCollection(colIndex).Name)

                    ' If the index is greater than the collection of explicitly
                    ' set column widths, set any additional columns to the default
                    If colIndex >= _ColumnWidths.Count Then
                        _ColumnWidths.Add(_ColumnWidthDefault)
                    End If
                    _TotalWidth += _ColumnWidths(colIndex)
                Next
            Else
                _TotalWidth = 0

                For colIndex As Integer = 0 To _ColumnNames.Count - 1
                    ' If the index is greater than the collection of explicitly
                    ' set column widths, set any additional columns to the default
                    If colIndex >= _ColumnWidths.Count Then
                        _ColumnWidths.Add(_ColumnWidthDefault)
                    End If
                    _TotalWidth += _ColumnWidths(colIndex)

                Next
            End If

            ' Check to see if the programmer is trying to display a column
            ' in the linked textbox that is greater than the columns in the 
            ' ComboBox. I handle this error by resetting it to zero.
            If _LinkedColumnIndex >= _ColumnNames.Count Then
                ' Or replace this with an OutOfBounds Exception
                _LinkedColumnIndex = 0
            End If
        End Sub

        Private Sub InitializeValueMemberColumn()
            Dim colIndex As Integer = 0
            For Each columnName As [String] In _ColumnNames
                If [String].Compare(columnName, ValueMember, True, CultureInfo.CurrentUICulture) = 0 Then
                    _ValueMemberColumnIndex = colIndex
                    Exit For
                End If
                colIndex += 1
            Next
        End Sub
    End Class
End Namespace