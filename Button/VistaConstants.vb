Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices

Namespace UIControls.Button
    ''' <summary>
    ''' Control style and notification constants
    ''' </summary>
    Public Class VistaConstants
        'The constants can be found from the Windows Vista SDK
        'This file contains most of the useful constants/methods that can be used in conjunction with the controls.
        'The constants are found as C++ code, and they are transferred here as C# code.
        'Imports the User32 DLL
        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
        End Function
        <DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function
        <DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr
        End Function
        'Imports the UXTheme DLL
        <DllImport("uxtheme", CharSet:=CharSet.Unicode)> _
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As [String], ByVal textSubIdList As [String]) As Int32
        End Function

        'Button styles
        Public Const BS_COMMANDLINK As Integer = &HE
        Public Const BS_SPLITBUTTON As Integer = &HC
        Public Const BM_SETSTYLE As Integer = &HF4
        'Button messages
        Public Const BCM_SETNOTE As Integer = &H1609
        Public Const BCM_SETSHIELD As Integer = &H160C
        Public Const BCM_FIRST As Integer = &H1600
        Public Const BCM_SETDROPDOWNSTATE As Integer = BCM_FIRST + &H6
        Public Const BM_SETIMAGE As Integer = &HF7

        Public Const ECM_FIRST As Integer = &H1500
        Public Const EM_SETCUEBANNER As Integer = ECM_FIRST + 1

        'Static messages
        Public Const STM_SETICON As Integer = &H170

        'Treeview universal constants
        Public Const TV_FIRST As Integer = &H1100
        'Treeview messages
        Public Const TVM_SETEXTENDEDSTYLE As Integer = TV_FIRST + 44
        Public Const TVM_GETEXTENDEDSTYLE As Integer = TV_FIRST + 45
        Public Const TVM_SETAUTOSCROLLINFO As Integer = TV_FIRST + 59
        'Treeview styles
        Public Const TVS_NOHSCROLL As Integer = &H8000
        'Treeview extended styles
        Public Const TVS_EX_AUTOHSCROLL As Integer = &H20
        Public Const TVS_EX_FADEINOUTEXPANDOS As Integer = &H40
        Public Const GWL_STYLE As Integer = -16

        'Listview universal constants
        Public Const LVM_FIRST As Integer = &H1000
        'Listview messages
        Public Const LVM_SETEXTENDEDLISTVIEWSTYLE As Integer = LVM_FIRST + 54
        Public Const LVS_EX_FULLROWSELECT As Integer = &H20
        'Listview extended styles
        Public Const LVS_EX_DOUBLEBUFFER As Integer = &H10000

        'Progressbar styles
        Public Const PBS_SMOOTHREVERSE As Integer = &H10
        Public Const PBST_NORMAL As Integer = &H1
        'used with PBM_SETSTATE
        Public Const PBST_ERROR As Integer = &H2
        'used with PBM_SETSTATE
        Public Const PBST_PAUSED As Integer = &H3
        'used with PBM_SETSTATE
        'Progressbar messages
        Public Const PBM_SETSTATE As Integer = WM_USER + 16

        'Windows notifications
        Public Const WM_NULL As Integer = &H0
        Public Const WM_CREATE As Integer = &H1
        Public Const WM_DESTROY As Integer = &H2
        Public Const WM_MOVE As Integer = &H3
        Public Const WM_SIZE As Integer = &H5
        Public Const WM_ACTIVATE As Integer = &H6
        Public Const WM_SETFOCUS As Integer = &H7
        Public Const WM_KILLFOCUS As Integer = &H8
        Public Const WM_ENABLE As Integer = &HA
        Public Const WM_SETREDRAW As Integer = &HB
        Public Const WM_SETTEXT As Integer = &HC
        Public Const WM_GETTEXT As Integer = &HD
        Public Const WM_GETTEXTLENGTH As Integer = &HE
        Public Const WM_PAINT As Integer = &HF
        Public Const WM_CLOSE As Integer = &H10
        Public Const WM_QUERYENDSESSION As Integer = &H11
        Public Const WM_QUIT As Integer = &H12
        Public Const WM_QUERYOPEN As Integer = &H13
        Public Const WM_ERASEBKGND As Integer = &H14
        Public Const WM_SYSCOLORCHANGE As Integer = &H15
        Public Const WM_ENDSESSION As Integer = &H16
        Public Const WM_SYSTEMERROR As Integer = &H17
        Public Const WM_SHOWWINDOW As Integer = &H18
        Public Const WM_CTLCOLOR As Integer = &H19
        Public Const WM_WININICHANGE As Integer = &H1A
        Public Const WM_SETTINGCHANGE As Integer = &H1A
        Public Const WM_DEVMODECHANGE As Integer = &H1B
        Public Const WM_ACTIVATEAPP As Integer = &H1C
        Public Const WM_FONTCHANGE As Integer = &H1D
        Public Const WM_TIMECHANGE As Integer = &H1E
        Public Const WM_CANCELMODE As Integer = &H1F
        Public Const WM_SETCURSOR As Integer = &H20
        Public Const WM_MOUSEACTIVATE As Integer = &H21
        Public Const WM_CHILDACTIVATE As Integer = &H22
        Public Const WM_QUEUESYNC As Integer = &H23
        Public Const WM_GETMINMAXINFO As Integer = &H24
        Public Const WM_PAINTICON As Integer = &H26
        Public Const WM_ICONERASEBKGND As Integer = &H27
        Public Const WM_NEXTDLGCTL As Integer = &H28
        Public Const WM_SPOOLERSTATUS As Integer = &H2A
        Public Const WM_DRAWITEM As Integer = &H2B
        Public Const WM_MEASUREITEM As Integer = &H2C
        Public Const WM_DELETEITEM As Integer = &H2D
        Public Const WM_VKEYTOITEM As Integer = &H2E
        Public Const WM_CHARTOITEM As Integer = &H2F

        Public Const WM_SETFONT As Integer = &H30
        Public Const WM_GETFONT As Integer = &H31
        Public Const WM_SETHOTKEY As Integer = &H32
        Public Const WM_GETHOTKEY As Integer = &H33
        Public Const WM_QUERYDRAGICON As Integer = &H37
        Public Const WM_COMPAREITEM As Integer = &H39
        Public Const WM_COMPACTING As Integer = &H41
        Public Const WM_WINDOWPOSCHANGING As Integer = &H46
        Public Const WM_WINDOWPOSCHANGED As Integer = &H47
        Public Const WM_POWER As Integer = &H48
        Public Const WM_COPYDATA As Integer = &H4A
        Public Const WM_CANCELJOURNAL As Integer = &H4B
        Public Const WM_NOTIFY As Integer = &H4E
        Public Const WM_INPUTLANGCHANGEREQUEST As Integer = &H50
        Public Const WM_INPUTLANGCHANGE As Integer = &H51
        Public Const WM_TCARD As Integer = &H52
        Public Const WM_HELP As Integer = &H53
        Public Const WM_USERCHANGED As Integer = &H54
        Public Const WM_NOTIFYFORMAT As Integer = &H55
        Public Const WM_CONTEXTMENU As Integer = &H7B
        Public Const WM_STYLECHANGING As Integer = &H7C
        Public Const WM_STYLECHANGED As Integer = &H7D
        Public Const WM_DISPLAYCHANGE As Integer = &H7E
        Public Const WM_GETICON As Integer = &H7F
        Public Const WM_SETICON As Integer = &H80

        Public Const WM_NCCREATE As Integer = &H81
        Public Const WM_NCDESTROY As Integer = &H82
        Public Const WM_NCCALCSIZE As Integer = &H83
        Public Const WM_NCHITTEST As Integer = &H84
        Public Const WM_NCPAINT As Integer = &H85
        Public Const WM_NCACTIVATE As Integer = &H86
        Public Const WM_GETDLGCODE As Integer = &H87
        Public Const WM_NCMOUSEMOVE As Integer = &HA0
        Public Const WM_NCLBUTTONDOWN As Integer = &HA1
        Public Const WM_NCLBUTTONUP As Integer = &HA2
        Public Const WM_NCLBUTTONDBLCLK As Integer = &HA3
        Public Const WM_NCRBUTTONDOWN As Integer = &HA4
        Public Const WM_NCRBUTTONUP As Integer = &HA5
        Public Const WM_NCRBUTTONDBLCLK As Integer = &HA6
        Public Const WM_NCMBUTTONDOWN As Integer = &HA7
        Public Const WM_NCMBUTTONUP As Integer = &HA8
        Public Const WM_NCMBUTTONDBLCLK As Integer = &HA9

        Public Const WM_KEYFIRST As Integer = &H100
        Public Const WM_KEYDOWN As Integer = &H100
        Public Const WM_KEYUP As Integer = &H101
        Public Const WM_CHAR As Integer = &H102
        Public Const WM_DEADCHAR As Integer = &H103
        Public Const WM_SYSKEYDOWN As Integer = &H104
        Public Const WM_SYSKEYUP As Integer = &H105
        Public Const WM_SYSCHAR As Integer = &H106
        Public Const WM_SYSDEADCHAR As Integer = &H107
        Public Const WM_KEYLAST As Integer = &H108

        Public Const WM_IME_STARTCOMPOSITION As Integer = &H10D
        Public Const WM_IME_ENDCOMPOSITION As Integer = &H10E
        Public Const WM_IME_COMPOSITION As Integer = &H10F
        Public Const WM_IME_KEYLAST As Integer = &H10F

        Public Const WM_INITDIALOG As Integer = &H110
        Public Const WM_COMMAND As Integer = &H111
        Public Const WM_SYSCOMMAND As Integer = &H112
        Public Const WM_TIMER As Integer = &H113
        Public Const WM_HSCROLL As Integer = &H114
        Public Const WM_VSCROLL As Integer = &H115
        Public Const WM_INITMENU As Integer = &H116
        Public Const WM_INITMENUPOPUP As Integer = &H117
        Public Const WM_MENUSELECT As Integer = &H11F
        Public Const WM_MENUCHAR As Integer = &H120
        Public Const WM_ENTERIDLE As Integer = &H121

        Public Const WM_CTLCOLORMSGBOX As Integer = &H132
        Public Const WM_CTLCOLOREDIT As Integer = &H133
        Public Const WM_CTLCOLORLISTBOX As Integer = &H134
        Public Const WM_CTLCOLORBTN As Integer = &H135
        Public Const WM_CTLCOLORDLG As Integer = &H136
        Public Const WM_CTLCOLORSCROLLBAR As Integer = &H137
        Public Const WM_CTLCOLORSTATIC As Integer = &H138

        Public Const WM_MOUSEFIRST As Integer = &H200
        Public Const WM_MOUSEMOVE As Integer = &H200
        Public Const WM_LBUTTONDOWN As Integer = &H201
        Public Const WM_LBUTTONUP As Integer = &H202
        Public Const WM_LBUTTONDBLCLK As Integer = &H203
        Public Const WM_RBUTTONDOWN As Integer = &H204
        Public Const WM_RBUTTONUP As Integer = &H205
        Public Const WM_RBUTTONDBLCLK As Integer = &H206
        Public Const WM_MBUTTONDOWN As Integer = &H207
        Public Const WM_MBUTTONUP As Integer = &H208
        Public Const WM_MBUTTONDBLCLK As Integer = &H209
        Public Const WM_MOUSELAST As Integer = &H20A
        Public Const WM_MOUSEWHEEL As Integer = &H20A

        Public Const WM_PARENTNOTIFY As Integer = &H210
        Public Const WM_ENTERMENULOOP As Integer = &H211
        Public Const WM_EXITMENULOOP As Integer = &H212
        Public Const WM_NEXTMENU As Integer = &H213
        Public Const WM_SIZING As Integer = &H214
        Public Const WM_CAPTURECHANGED As Integer = &H215
        Public Const WM_MOVING As Integer = &H216
        Public Const WM_POWERBROADCAST As Integer = &H218
        Public Const WM_DEVICECHANGE As Integer = &H219

        Public Const WM_MDICREATE As Integer = &H220
        Public Const WM_MDIDESTROY As Integer = &H221
        Public Const WM_MDIACTIVATE As Integer = &H222
        Public Const WM_MDIRESTORE As Integer = &H223
        Public Const WM_MDINEXT As Integer = &H224
        Public Const WM_MDIMAXIMIZE As Integer = &H225
        Public Const WM_MDITILE As Integer = &H226
        Public Const WM_MDICASCADE As Integer = &H227
        Public Const WM_MDIICONARRANGE As Integer = &H228
        Public Const WM_MDIGETACTIVE As Integer = &H229
        Public Const WM_MDISETMENU As Integer = &H230
        Public Const WM_ENTERSIZEMOVE As Integer = &H231
        Public Const WM_EXITSIZEMOVE As Integer = &H232
        Public Const WM_DROPFILES As Integer = &H233
        Public Const WM_MDIREFRESHMENU As Integer = &H234

        Public Const WM_IME_SETCONTEXT As Integer = &H281
        Public Const WM_IME_NOTIFY As Integer = &H282
        Public Const WM_IME_CONTROL As Integer = &H283
        Public Const WM_IME_COMPOSITIONFULL As Integer = &H284
        Public Const WM_IME_SELECT As Integer = &H285
        Public Const WM_IME_CHAR As Integer = &H286
        Public Const WM_IME_KEYDOWN As Integer = &H290
        Public Const WM_IME_KEYUP As Integer = &H291

        Public Const WM_MOUSEHOVER As Integer = &H2A1
        Public Const WM_NCMOUSELEAVE As Integer = &H2A2
        Public Const WM_MOUSELEAVE As Integer = &H2A3

        Public Const WM_CUT As Integer = &H300
        Public Const WM_COPY As Integer = &H301
        Public Const WM_PASTE As Integer = &H302
        Public Const WM_CLEAR As Integer = &H303
        Public Const WM_UNDO As Integer = &H304

        Public Const WM_RENDERFORMAT As Integer = &H305
        Public Const WM_RENDERALLFORMATS As Integer = &H306
        Public Const WM_DESTROYCLIPBOARD As Integer = &H307
        Public Const WM_DRAWCLIPBOARD As Integer = &H308
        Public Const WM_PAINTCLIPBOARD As Integer = &H309
        Public Const WM_VSCROLLCLIPBOARD As Integer = &H30A
        Public Const WM_SIZECLIPBOARD As Integer = &H30B
        Public Const WM_ASKCBFORMATNAME As Integer = &H30C
        Public Const WM_CHANGECBCHAIN As Integer = &H30D
        Public Const WM_HSCROLLCLIPBOARD As Integer = &H30E
        Public Const WM_QUERYNEWPALETTE As Integer = &H30F
        Public Const WM_PALETTEISCHANGING As Integer = &H310
        Public Const WM_PALETTECHANGED As Integer = &H311

        Public Const WM_HOTKEY As Integer = &H312
        Public Const WM_PRINT As Integer = &H317
        Public Const WM_PRINTCLIENT As Integer = &H318

        Public Const WM_HANDHELDFIRST As Integer = &H358
        Public Const WM_HANDHELDLAST As Integer = &H35F
        Public Const WM_PENWINFIRST As Integer = &H380
        Public Const WM_PENWINLAST As Integer = &H38F
        Public Const WM_COALESCE_FIRST As Integer = &H390
        Public Const WM_COALESCE_LAST As Integer = &H39F
        Public Const WM_DDE_FIRST As Integer = &H3E0
        Public Const WM_DDE_INITIATE As Integer = &H3E0
        Public Const WM_DDE_TERMINATE As Integer = &H3E1
        Public Const WM_DDE_ADVISE As Integer = &H3E2
        Public Const WM_DDE_UNADVISE As Integer = &H3E3
        Public Const WM_DDE_ACK As Integer = &H3E4
        Public Const WM_DDE_DATA As Integer = &H3E5
        Public Const WM_DDE_REQUEST As Integer = &H3E6
        Public Const WM_DDE_POKE As Integer = &H3E7
        Public Const WM_DDE_EXECUTE As Integer = &H3E8
        Public Const WM_DDE_LAST As Integer = &H3E8

        Public Const WM_USER As Integer = &H400
        Public Const WM_APP As Integer = &H8000
    End Class
End Namespace