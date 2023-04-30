Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices

Namespace UIControls
    Class eNativeMethods
        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
        End Function
    End Class
End Namespace