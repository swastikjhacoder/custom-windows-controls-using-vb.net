
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing

Namespace UIControls.ListView
    <ToolboxBitmap(GetType(eListView), "Resources.listview.bmp")> _
    Public Class eListView
        Inherits Windows.Forms.ListView
        Public Const LVM_FIRST As Integer = &H1000
        Public Const LVM_SETEXTENDEDLISTVIEWSTYLE As Integer = LVM_FIRST + 54
        Public Const LVS_EX_DOUBLEBUFFER As Integer = &H10000
        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
        End Function
        'Imports the UXTheme DLL
        <DllImport("uxtheme", CharSet:=CharSet.Unicode)> _
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As [String], ByVal textSubIdList As [String]) As Int32
        End Function

        Public Sub New()
            'Unknown problem: Calling anything that affects the interface here has no effect
            'SetWindowTheme(this.Handle, "explorer", null); //Explorer style
            'SendMessage(this.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, LVS_EX_DOUBLEBUFFER); //Blue selection
            AddHandler Me.Enter, AddressOf ExplorerListView_Load
        End Sub

        '1-time execution, to set the theme; Used to render the control at run-time without extra code
        Private elv As [Boolean] = False
        Public Sub ExplorerListView_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not elv Then
                SetWindowTheme(Me.Handle, "explorer", Nothing)
                'Explorer style
                SendMessage(Me.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, LVS_EX_DOUBLEBUFFER)
                'Blue selection
                elv = True
            End If
        End Sub

    End Class
End Namespace