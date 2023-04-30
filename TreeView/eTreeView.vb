
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing

Namespace UIControls.TreeView
    <ToolboxBitmap(GetType(eTreeview), "Resources.view_tree.bmp")> _
    Public Class eTreeview
        Inherits Windows.Forms.TreeView

        Friend Class NativeMethods
            Public Const TV_FIRST As Integer = &H1100
            Public Const TVM_SETEXTENDEDSTYLE As Integer = TV_FIRST + 44
            Public Const TVM_GETEXTENDEDSTYLE As Integer = TV_FIRST + 45
            Public Const TVM_SETAUTOSCROLLINFO As Integer = TV_FIRST + 59
            Public Const TVS_NOHSCROLL As Integer = &H8000
            Public Const TVS_EX_AUTOHSCROLL As Integer = &H20
            Public Const TVS_EX_FADEINOUTEXPANDOS As Integer = &H40
            Public Const GWL_STYLE As Integer = -16

            <DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
            Friend Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInt32, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
            End Function

            <DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
            Friend Shared Sub SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer)
            End Sub

            <DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
            Friend Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
            End Function

            <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode)> _
            Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
            End Function
        End Class

        'some help from http://www.danielmoth.com/Blog/2007/01/treeviewvista.html
        Const TVS_EX_FADEINOUTEXPANDOS As Integer = &H40
        Const TVS_EX_AUTOHSCROLL As Integer = &H20
        'Imports the UXTheme DLL
        <DllImport("uxtheme", CharSet:=CharSet.Unicode)> _
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As [String], ByVal textSubIdList As [String]) As Int32
        End Function
        Public Sub New()
            SetWindowTheme(Me.Handle, "explorer", Nothing)
            Me.HotTracking = True
            'important
            Me.ShowLines = False
            'NativeMethods.SendMessage(this.Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, 0, NativeMethods.TVS_EX_AUTOHSCROLL);
            NativeMethods.SendMessage(Me.Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, 0, NativeMethods.TVS_EX_FADEINOUTEXPANDOS)
        End Sub
        'protected override CreateParams CreateParams
        '{
        '    get
        '    {
        '        CreateParams cParams = base.CreateParams;
        '        //Set the button to use external styles
        '        //cParams.Style |= TVS_EX_FADEINOUTEXPANDOS;
        '        cParams.Style |= NativeMethods.TVS_NOHSCROLL; // lose the horizotnal scrollbar
        '        //cParams.Style |= TVS_EX_AUTOHSCROLL;
        '        //Uncomment the code below if you want the button to only show the icon
        '        //cParams.Style |= 0x00000040; // BS_ICON value
        '        return cParams;
        '    }
        '}
    End Class
End Namespace