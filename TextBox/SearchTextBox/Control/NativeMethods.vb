
Imports System.Text
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Timers
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace UIControls.TextBox
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure APPBARDATA
        Public cbSize As UInteger
        Public hWnd As IntPtr
        Public uCallbackMessage As UInteger
        Public uEdge As AppBarEdge
        Public rc As RECT
        Public lParam As Integer
    End Structure

    Friend Enum MousePositionCodes
        HTERROR = (-2)
        HTTRANSPARENT = (-1)
        HTNOWHERE = 0
        HTCLIENT = 1
        HTCAPTION = 2
        HTSYSMENU = 3
        HTGROWBOX = 4
        HTSIZE = HTGROWBOX
        HTMENU = 5
        HTHSCROLL = 6
        HTVSCROLL = 7
        HTMINBUTTON = 8
        HTMAXBUTTON = 9
        HTLEFT = 10
        HTRIGHT = 11
        HTTOP = 12
        HTTOPLEFT = 13
        HTTOPRIGHT = 14
        HTBOTTOM = 15
        HTBOTTOMLEFT = 16
        HTBOTTOMRIGHT = 17
        HTBORDER = 18
        HTREDUCE = HTMINBUTTON
        HTZOOM = HTMAXBUTTON
        HTSIZEFIRST = HTLEFT
        HTSIZELAST = HTBOTTOMRIGHT
        HTOBJECT = 19
        HTCLOSE = 20
        HTHELP = 21
    End Enum

    Public Enum AppBarEdge As UInteger
        NotDocked = UInt32.MaxValue
        ScreenLeft = 0
        ' ABE_LEFT
        ScreenTop = 1
        ' ABE_TOP
        ScreenRight = 2
        ' ABE_RIGHT
        ScreenBottom = 3
        ' ABE_BOTTOM
    End Enum

    Friend Enum AppBarMessage As UInteger
        ABM_NEW = &H0
        ABM_REMOVE = &H1
        ABM_QUERYPOS = &H2
        ABM_SETPOS = &H3
        ABM_GETSTATE = &H4
        ABM_GETTASKBARPOS = &H5
        ABM_ACTIVATE = &H6
        ABM_GETAUTOHIDEBAR = &H7
        ABM_SETAUTOHIDEBAR = &H8
        ABM_WINDOWPOSCHANGED = &H9
        ABM_SETSTATE = &HA
    End Enum

    Friend Enum AppBarState
        ABS_MANUAL = 0
        ABS_AUTOHIDE = 1
        ABS_ALWAYSONTOP = 2
        ABS_AUTOHIDEANDONTOP = 3
    End Enum

    Friend Enum AppBarNotification
        ABN_STATECHANGE = 0
        ABN_POSCHANGED
        ABN_FULLSCREENAPP
        ABN_WINDOWARRANGE
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer

        Public Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
            Me.left = left
            Me.top = top
            Me.right = right
            Me.bottom = bottom
        End Sub

        Public Shared Widening Operator CType(ByVal r As Rectangle) As RECT
            Return New RECT(r.Left, r.Top, r.Right, r.Bottom)
        End Operator

        Public Shared Narrowing Operator CType(ByVal r As RECT) As Rectangle
            Return New Rectangle(r.left, r.top, r.right - r.left, r.bottom - r.top)
        End Operator
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure POINT
        Public X As Integer
        Public Y As Integer

        Public Sub New(ByVal x As Integer, ByVal y As Integer)
            Me.X = x
            Me.Y = y
        End Sub

        Public Shared Widening Operator CType(ByVal p As POINT) As System.Drawing.Point
            Return New System.Drawing.Point(p.X, p.Y)
        End Operator

        Public Shared Widening Operator CType(ByVal p As System.Drawing.Point) As POINT
            Return New POINT(p.X, p.Y)
        End Operator
    End Structure

    Friend NotInheritable Class NativeMethods
        Private Sub New()
        End Sub
        <DllImport("shell32.dll", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function SHAppBarMessage(ByVal dwMessage As AppBarMessage, ByRef pData As APPBARDATA) As UInteger
        End Function

        <DllImport("user32.dll", ExactSpelling:=True)> _
        Public Shared Function MoveWindow(ByVal hWnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal bRepaint As Boolean) As Boolean
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function RegisterWindowMessage(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpString As String) As UInteger
        End Function

        <DllImport("user32.dll", EntryPoint:="ReleaseCapture")> _
        Public Shared Function StopMouseCapture() As Boolean
        End Function

        <DllImport("user32.dll", EntryPoint:="SetCapture")> _
        Public Shared Function StartMouseCapture(ByVal hWnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", EntryPoint:="GetCapture")> _
        Public Shared Function GetMouseCapture() As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function DragDetect(ByVal hwnd As IntPtr, ByVal pt As POINT) As Boolean
        End Function

        <DllImport("user32.dll")> _
        Public Shared Sub SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
        End Sub
    End Class
End Namespace