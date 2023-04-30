
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing

Namespace UIControls.Button
    'This control now supports dropdowns.
    Public Delegate Sub DropDownClicked()
    Public Class eSplitButton
        Inherits Windows.Forms.Button

        Const BS_SPLITBUTTON As Integer = &HC

        'Import
        <DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
        Private Shared Function SendMessage(ByVal hWnd As HandleRef, ByVal Msg As UInt32, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr
        End Function

        Public Sub New()
            InitializeComponent()
            'Set the flatstyle
            Me.FlatStyle = FlatStyle.System
            AddHandler Me.DropDown_Clicked, AddressOf Me.launchmenu
            AddHandler Me.ddm_.Collapse, AddressOf Me.CloseMenuDropdown
        End Sub

        'Set the theme
        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim cParams As CreateParams = MyBase.CreateParams
                cParams.Style = cParams.Style Or BS_SPLITBUTTON
                Return cParams
            End Get
        End Property

        Const BCM_FIRST As Integer = &H1600
        Const BCM_SETDROPDOWNSTATE As Integer = &H1606
        Const WM_NOTIFY As Integer = &H4E

        'Receives messages sent to the control
        Public isatdropdown As Integer = 0
        Public ismousedown As Integer = 0
        Public isbumped As Integer = 0
        Public dropdownpushed As Integer = 0
        Protected Overrides Sub WndProc(ByRef m As Message)
            ' Listen for operating system messages; Filter out a lot of messages here
            'The dropdown glyph changes:
            'Mousedown (at dropdown area) => Dropdown Event fired => Glyph changes => MouseUp => Glyph Changes
            Select Case m.Msg
                Case (VistaConstants.BCM_SETDROPDOWNSTATE)
                    'PROBLEM: other buttons also have would encounter this message
                    If m.HWnd = Me.Handle Then
                        If m.WParam.ToString() = "1" Then
                            If dropdownpushed = 0 Then
                                'One time-initiation per mouse press
                                Me.dropdownpushed = 1
                                RaiseEvent DropDown_Clicked()
                            End If
                        End If
                        If Me.ismousedown = 1 Then
                            Me.isatdropdown = 1
                        End If
                    End If
                    Exit Select
                Case (VistaConstants.WM_PAINT)
                    'Paints the control to have the dropdown when needed
                    If Me.dropdownpushed = 1 Then
                        Me.SetDropDownState(1)
                    End If
                    Exit Select
                Case (VistaConstants.WM_LBUTTONDOWN)
                    Me.ismousedown = 1
                    Exit Select
                Case (VistaConstants.WM_MOUSELEAVE)
                    If Me.isatdropdown = 1 Then
                        Me.SetDropDownState(0)
                        Me.isatdropdown = 0
                        Me.ismousedown = 0
                    End If
                    Exit Select
                Case (VistaConstants.WM_LBUTTONUP)
                    If Me.isatdropdown = 1 Then
                        Me.SetDropDownState(0)
                        Me.isatdropdown = 0
                        Me.ismousedown = 0
                    End If
                    Exit Select
                Case (VistaConstants.WM_KILLFOCUS)
                    If Me.isatdropdown = 1 Then
                        Me.SetDropDownState(0)
                        Me.isatdropdown = 0
                        Me.ismousedown = 0
                    End If
                    Exit Select
            End Select
            MyBase.WndProc(m)
        End Sub
        Public Sub SetDropDownState(ByVal Pushed As Integer)
            If Pushed = 0 Then
                'Removes dropdown pushed state
                Me.dropdownpushed = 0
            End If
            VistaConstants.SendMessage(Me.Handle, BCM_FIRST + &H6, Pushed, 0)
        End Sub
        Public Event DropDown_Clicked As DropDownClicked
        'The event which would be fired whenever the drop-down is clicked on.
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            ' 
            ' SplitButton
            ' 
            Me.ResumeLayout(False)
        End Sub

        Public Sub launchmenu()
            If ddm_.MenuItems.Count <> 0 Then
                Me.ddm_.Show(Me, New Point(Me.Width, Me.Height), LeftRightAlignment.Left)
            End If
        End Sub

        Public Sub CloseMenuDropdown(ByVal sender As Object, ByVal e As EventArgs)
            'Hmm... contextmenu "collapse" state doesn't seem to fire up when a menuitem is clicked. Does not work here yet.
            Me.SetDropDownState(0)
        End Sub

        Private ddm_ As New ContextMenu()
        Public Property Dropdownmenu() As ContextMenu
            Get
                Return ddm_
            End Get
            Set(ByVal value As ContextMenu)
                'this.ddm_.Collapse += new EventHandler(this.CloseMenuDropdown);
                ddm_ = value
            End Set
        End Property

    End Class
End Namespace