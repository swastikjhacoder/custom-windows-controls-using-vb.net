
Imports System.Text
Imports System.Drawing
Imports System.Runtime.InteropServices

Namespace UIControls.ProgressBar.Win32
    Partial Public Class User32
#Region "Struct"

        <StructLayout(LayoutKind.Sequential)> _
        Friend Structure RECT
            Public left As Integer
            Public top As Integer
            Public right As Integer
            Public bottom As Integer

            Public Overrides Function ToString() As String
                Return "{left=" + left.ToString() + ", " + "top=" + top.ToString() + ", " + "right=" + right.ToString() + ", " + "bottom=" + bottom.ToString() + "}"
            End Function
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Friend Structure BLENDFUNCTION
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
        End Structure

#End Region

#Region "UnmanagedMethods"

        <DllImport("gdi32")> _
        Friend Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
        End Function

        <DllImport("gdi32")> _
        Friend Shared Function SelectObject(ByVal hdc As IntPtr, ByVal hgdiobj As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32", SetLastError:=True)> _
        Friend Shared Function CreateCompatibleDC(ByVal hdc As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32")> _
        Friend Shared Function CreateDC(ByVal driverName As [String], ByVal deviceName As [String], ByVal output As [String], ByVal lpInitData As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32")> _
        Friend Shared Function DeleteDC(ByVal dc As IntPtr) As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetDC(ByVal hwnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function ReleaseDC(ByVal hwnd As IntPtr, ByVal hdc As IntPtr) As Integer
        End Function

        Friend Declare Auto Function UpdateLayeredWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, ByVal hdcSrc As IntPtr, ByRef pprSrc As Point, _
         ByVal crKey As Int32, ByRef pblend As BLENDFUNCTION, ByVal dwFlags As Int32) As Boolean

        <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Friend Shared Function SetWindowText(ByVal hwnd As IntPtr, ByVal lpString As [String]) As Boolean
        End Function

        <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Friend Shared Function GetWindowTextLength(ByVal hWnd As IntPtr) As Integer
        End Function

        ' Sample Code for GetWindowText API
        '            public static string GetText(IntPtr hWnd)
        '            {
        '                // Allocate correct string length first
        '                int length = GetWindowTextLength(hWnd);
        '                StringBuilder sb = new StringBuilder(length + 1);
        '                GetWindowText(hWnd, sb, sb.Capacity);
        '                return sb.ToString();
        '            } 


        <DllImport("user32", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Friend Shared Function GetWindowText(ByVal hWnd As IntPtr, ByVal lpString As StringBuilder, ByVal nMaxCount As Integer) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
        End Function

        <DllImport("user32")> _
        Friend Shared Function SetCapture(ByVal hWnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function ReleaseCapture() As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function IsWindowVisible(ByVal hwnd As IntPtr) As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Public Shared Function SetParent(ByVal hwndChild As IntPtr, ByVal hwndParent As IntPtr) As IntPtr
        End Function

        ''' <summary>
        ''' The GetParent function retrieves a handle to the specified window's parent or owner.
        ''' </summary>
        ''' <param name="hwnd">Handle to the window whose parent window handle is to be retrieved.</param>
        ''' <returns>If the window is a child window, the return value is a handle to the parent window. If the window is a top-level window, the return value is a handle to the owner window. If the window is a top-level unowned window or if the function fails, the return value is NULL.</returns>
        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Public Shared Function GetParent(ByVal hwnd As IntPtr) As IntPtr
        End Function

        ''' <summary>
        ''' The FindWindowEx function retrieves a handle to a window whose class name and window name match the specified strings. The function searches child windows, beginning with the one following the specified child window.
        ''' </summary>
        ''' <param name="hwndParent">Handle to the parent window whose child windows are to be searched.</param>
        ''' <param name="hwndChildAfter">Handle to a child window.</param>
        ''' <param name="lpszClass">Specifies class name.</param>
        ''' <param name="lpszWindow">Pointer to a null-terminated string that specifies the window name (the window's title).</param>
        ''' <returns>If the function succeeds, the return value is a handle to the window that has the specified class and window names.If the function fails, the return value is NULL.</returns>
        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function FindWindowEx(ByVal hwndParent As IntPtr, ByVal hwndChildAfter As IntPtr, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpszClass As String, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpszWindow As String) As IntPtr
        End Function

        ''' <summary>
        ''' The MoveWindow function changes the position and dimensions of the specified window.
        ''' </summary>
        ''' <param name="hwnd">Handle to the window.</param>
        ''' <param name="X">Specifies the new position of the left side of the window.</param>
        ''' <param name="Y">Specifies the new position of the top of the window.</param>
        ''' <param name="nWidth">Specifies the new width of the window.</param>
        ''' <param name="nHeight">Specifies the new height of the window.</param>
        ''' <param name="bRepaint">If the bRepaint parameter is TRUE, the system sends the WM_PAINT message to the window procedure immediately after moving the window (that is, the MoveWindow function calls the UpdateWindow function). If bRepaint is FALSE, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</param>
        ''' <returns>If the function succeeds, the return value is true.If the function fails, the return value is false.</returns>
        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function MoveWindow(ByVal hwnd As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal bRepaint As Boolean) As Boolean
        End Function

        ''' <summary>
        ''' The InvalidateRect function adds a rectangle to the specified window's update region.
        ''' </summary>
        ''' <param name="hwnd">Handle to window.</param>
        ''' <param name="rect">Rectangle coordinates.</param>
        ''' <param name="bErase">Erase state.</param>
        ''' <returns>If the function succeeds, the return value is true.If the function fails, the return value is false.</returns>
        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function InvalidateRect(ByVal hwnd As IntPtr, ByRef rect As Rectangle, ByVal bErase As Boolean) As Boolean
        End Function

        ''' <summary>
        ''' The ValidateRect function validates the client area within a rectangle by removing the rectangle from the update region of the specified window.
        ''' </summary>
        ''' <param name="hwnd">Handle to window.</param>
        ''' <param name="rect">Validation rectangle coordinates.</param>
        ''' <returns>If the function succeeds, the return value is true.If the function fails, the return value is false.</returns>
        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function ValidateRect(ByVal hwnd As IntPtr, ByRef rect As Rectangle) As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal dwStyle As Integer) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetDesktopWindow() As IntPtr
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetClientRect(ByVal hwnd As IntPtr, ByRef rc As RECT) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetClientRect(ByVal hwnd As IntPtr, <[In](), Out()> ByRef rect As Rectangle) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetWindowRect(ByVal hWnd As IntPtr, <[In](), Out()> ByRef rect As Rectangle) As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetWindowRect(ByVal hwnd As IntPtr, ByRef rc As RECT) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function RedrawWindow(ByVal hWnd As IntPtr, ByVal lprcUpdate As IntPtr, ByVal hrgnUpdate As IntPtr, ByVal flags As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' Changes flags that modify attributes of the layered window such as alpha(opacity).
        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetLayeredWindowAttributes(ByVal hwnd As IntPtr, ByVal crKey As UInteger, ByVal bAlpha As Byte, ByVal dwFlags As UInteger) As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As UInteger
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal Width As Integer, ByVal Height As Integer, _
         ByVal flags As FlagsSetWindowPos) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, _
         ByVal uFlags As UInteger) As Boolean
        End Function

        <DllImport("user32", EntryPoint:="SystemParametersInfo", SetLastError:=True)> _
        Friend Shared Function SystemParametersInfoSet(ByVal action As UInteger, ByVal param As UInteger, ByVal vparam As UInteger, ByVal init As UInteger) As Boolean
        End Function

#Region "SystemMenuAPI"

        <DllImport("user32")> _
        Friend Shared Function CreateMenu() As IntPtr
        End Function

        <DllImport("user32")> _
        Friend Shared Function GetSystemMenu(ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As IntPtr
        End Function

        <DllImport("user32")> _
        Friend Shared Function DrawMenuBar(ByVal hWnd As IntPtr) As Boolean
        End Function

        <DllImport("user32")> _
        Friend Shared Function GetMenuItemCount(ByVal hMenu As IntPtr) As Integer
        End Function

        <DllImport("user32")> _
        Friend Shared Function CreatePopupMenu() As IntPtr
        End Function

        <DllImport("user32")> _
        Friend Shared Function RemoveMenu(ByVal hMenu As IntPtr, ByVal uPosition As UInteger, ByVal uFlags As MenuFlags) As Boolean
        End Function

        <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Friend Shared Function InsertMenu(ByVal hmenu As IntPtr, ByVal position As UInteger, ByVal flags As MenuFlags, ByVal item_id As UInteger, <MarshalAs(UnmanagedType.LPTStr)> ByVal item_text As String) As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function AppendMenu(ByVal hMenu As IntPtr, ByVal uFlags As MenuFlags, ByVal uIDNewItem As UInteger, ByVal lpNewItem As String) As Boolean
        End Function

#End Region

#End Region
    End Class
End Namespace
