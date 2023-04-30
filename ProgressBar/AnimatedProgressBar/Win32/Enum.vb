

Namespace UIControls.ProgressBar.Win32
    Partial Class User32
#Region "Symbolic Constants"

        'internal const int _CS_DROPSHADOW = 0x20000;
        Friend Const _LWA_ALPHA As Integer = &H2
        Friend Const _HT_CAPTION As Integer = &H2
        Friend Const _HT_CLIENT As Integer = 1
        Friend Const _HT_TRANSPARENT As Integer = -1
        Friend Const _AC_SRC_OVER As Byte = &H0
        Friend Const _AC_SRC_ALPHA As Byte = &H1

#End Region

        <Flags()> _
        Friend Enum MenuFlags As UInteger
            MF_STRING = 0
            MF_GRAYED = &H1
            MF_DISABLED = &H2
            MF_CHECKED = &H8
            MF_POPUP = &H10
            MF_BARBREAK = &H20
            MF_BREAK = &H40
            MF_BYPOSITION = &H400
            MF_SEPARATOR = &H800
            MF_REMOVE = &H1000
        End Enum

        ''' <summary>
        ''' Specifies values from SetWindowPosZ enumeration.
        ''' </summary>
        Friend Enum SetWindowPosZ
            ''' <summary>
            ''' Specified HWND_TOP enumeration value.
            ''' </summary>
            HWND_TOP = 0

            ''' <summary>
            ''' Specified HWND_BOTTOM enumeration value.
            ''' </summary>
            HWND_BOTTOM = 1

            ''' <summary>
            ''' Specified HWND_TOPMOST enumeration value.
            ''' </summary>
            HWND_TOPMOST = -1

            ''' <summary>
            ''' Specified HWND_NOTOPMOST enumeration value.
            ''' </summary>
            HWND_NOTOPMOST = -2
        End Enum

        '[CLSCompliant(false)]
        <Flags()> _
        Friend Enum FlagsSetWindowPos As UInteger
            SWP_NOSIZE = &H1
            SWP_NOMOVE = &H2
            SWP_NOZORDER = &H4
            SWP_NOREDRAW = &H8
            SWP_NOACTIVATE = &H10
            SWP_FRAMECHANGED = &H20
            SWP_SHOWWINDOW = &H40
            SWP_HIDEWINDOW = &H80
            SWP_NOCOPYBITS = &H100
            SWP_NOOWNERZORDER = &H200
            SWP_NOSENDCHANGING = &H400
            SWP_DRAWFRAME = &H20
            SWP_NOREPOSITION = &H200
            SWP_DEFERERASE = &H2000
            SWP_ASYNCWINDOWPOS = &H4000
        End Enum

        <Flags()> _
        Friend Enum RedrawWindowFlags As UInteger
            ''' <summary>
            ''' Invalidates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            ''' You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_INVALIDATE invalidates the entire window.
            ''' </summary>
            Invalidate = &H1

            ''' <summary>Causes the OS to post a WM_PAINT message to the window regardless of whether a portion of the window is invalid.</summary>
            InternalPaint = &H2

            ''' <summary>
            ''' Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
            ''' Specify this value in combination with the RDW_INVALIDATE value; otherwise, RDW_ERASE has no effect.
            ''' </summary>
            [Erase] = &H4

            ''' <summary>
            ''' Validates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            ''' You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_VALIDATE validates the entire window.
            ''' This value does not affect internal WM_PAINT messages.
            ''' </summary>
            Validate = &H8

            NoInternalPaint = &H10

            ''' <summary>Suppresses any pending WM_ERASEBKGND messages.</summary>
            NoErase = &H20

            ''' <summary>Excludes child windows, if any, from the repainting operation.</summary>
            NoChildren = &H40

            ''' <summary>Includes child windows, if any, in the repainting operation.</summary>
            AllChildren = &H80

            ''' <summary>Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND and WM_PAINT messages before the RedrawWindow returns, if necessary.</summary>
            UpdateNow = &H100

            ''' <summary>
            ''' Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND messages before RedrawWindow returns, if necessary.
            ''' The affected windows receive WM_PAINT messages at the ordinary time.
            ''' </summary>
            EraseNow = &H200

            Frame = &H400

            NoFrame = &H800
        End Enum

        Friend Enum ShowWindowStyles
            SW_HIDE = 0
            SW_SHOWNORMAL = 1
            SW_NORMAL = 1
            SW_SHOWMINIMIZED = 2
            SW_SHOWMAXIMIZED = 3
            SW_MAXIMIZE = 3
            SW_SHOWNOACTIVATE = 4
            SW_SHOW = 5
            SW_MINIMIZE = 6
            SW_SHOWMINNOACTIVE = 7
            SW_SHOWNA = 8
            SW_RESTORE = 9
            SW_SHOWDEFAULT = 10
            SW_FORCEMINIMIZE = 11
            SW_MAX = 11
        End Enum

#Region "Windows Messages"

        ''' <summary>
        ''' Specifies values from Msgs enumeration.
        ''' </summary>
        Friend Enum Msgs
            ''' <summary>
            ''' Specified WM_NULL enumeration value.
            ''' </summary>
            WM_NULL = &H0

            ''' <summary>
            ''' Specified WM_CREATE enumeration value.
            ''' </summary>
            WM_CREATE = &H1

            ''' <summary>
            ''' Specified WM_DESTROY enumeration value.
            ''' </summary>
            WM_DESTROY = &H2

            ''' <summary>
            ''' Specified WM_MOVE enumeration value.
            ''' </summary>
            WM_MOVE = &H3

            ''' <summary>
            ''' Specified WM_SIZE enumeration value.
            ''' </summary>
            WM_SIZE = &H5

            ''' <summary>
            ''' Specified WM_ACTIVATE enumeration value.
            ''' </summary>
            WM_ACTIVATE = &H6

            ''' <summary>
            ''' Specified WM_SETFOCUS enumeration value.
            ''' </summary>
            WM_SETFOCUS = &H7

            ''' <summary>
            ''' Specified WM_KILLFOCUS enumeration value.
            ''' </summary>
            WM_KILLFOCUS = &H8

            ''' <summary>
            ''' Specified WM_ENABLE enumeration value.
            ''' </summary>
            WM_ENABLE = &HA

            ''' <summary>
            ''' Specified WM_SETREDRAW enumeration value.
            ''' </summary>
            WM_SETREDRAW = &HB

            ''' <summary>
            ''' Specified WM_SETTEXT enumeration value.
            ''' </summary>
            WM_SETTEXT = &HC

            ''' <summary>
            ''' Specified WM_GETTEXT enumeration value.
            ''' </summary>
            WM_GETTEXT = &HD

            ''' <summary>
            ''' Specified WM_GETTEXTLENGTH enumeration value.
            ''' </summary>
            WM_GETTEXTLENGTH = &HE

            ''' <summary>
            ''' Specified WM_PAINT enumeration value.
            ''' </summary>
            WM_PAINT = &HF

            ''' <summary>
            ''' Specified WM_CLOSE enumeration value.
            ''' </summary>
            WM_CLOSE = &H10

            ''' <summary>
            ''' Specified WM_QUERYENDSESSION enumeration value.
            ''' </summary>
            WM_QUERYENDSESSION = &H11

            ''' <summary>
            ''' Specified WM_QUIT enumeration value.
            ''' </summary>
            WM_QUIT = &H12

            ''' <summary>
            ''' Specified WM_QUERYOPEN enumeration value.
            ''' </summary>
            WM_QUERYOPEN = &H13

            ''' <summary>
            ''' Specified WM_ERASEBKGND enumeration value.
            ''' </summary>
            WM_ERASEBKGND = &H14

            ''' <summary>
            ''' Specified WM_SYSCOLORCHANGE enumeration value.
            ''' </summary>
            WM_SYSCOLORCHANGE = &H15

            ''' <summary>
            ''' Specified WM_ENDSESSION enumeration value.
            ''' </summary>
            WM_ENDSESSION = &H16

            ''' <summary>
            ''' Specified WM_SHOWWINDOW enumeration value.
            ''' </summary>
            WM_SHOWWINDOW = &H18

            ''' <summary>
            ''' Specified WM_WININICHANGE enumeration value.
            ''' </summary>
            WM_WININICHANGE = &H1A

            ''' <summary>
            ''' Specified WM_SETTINGCHANGE enumeration value.
            ''' </summary>
            WM_SETTINGCHANGE = &H1A

            ''' <summary>
            ''' Specified WM_DEVMODECHANGE enumeration value.
            ''' </summary>
            WM_DEVMODECHANGE = &H1B

            ''' <summary>
            ''' Specified WM_ACTIVATEAPP enumeration value.
            ''' </summary>
            WM_ACTIVATEAPP = &H1C

            ''' <summary>
            ''' Specified WM_FONTCHANGE enumeration value.
            ''' </summary>
            WM_FONTCHANGE = &H1D

            ''' <summary>
            ''' Specified WM_TIMECHANGE enumeration value.
            ''' </summary>
            WM_TIMECHANGE = &H1E

            ''' <summary>
            ''' Specified WM_CANCELMODE enumeration value.
            ''' </summary>
            WM_CANCELMODE = &H1F

            ''' <summary>
            ''' Specified WM_SETCURSOR enumeration value.
            ''' </summary>
            WM_SETCURSOR = &H20

            ''' <summary>
            ''' Specified WM_MOUSEACTIVATE enumeration value.
            ''' </summary>
            WM_MOUSEACTIVATE = &H21

            ''' <summary>
            ''' Specified WM_CHILDACTIVATE enumeration value.
            ''' </summary>
            WM_CHILDACTIVATE = &H22

            ''' <summary>
            ''' Specified WM_QUEUESYNC enumeration value.
            ''' </summary>
            WM_QUEUESYNC = &H23

            ''' <summary>
            ''' Specified WM_GETMINMAXINFO enumeration value.
            ''' </summary>
            WM_GETMINMAXINFO = &H24

            ''' <summary>
            ''' Specified WM_PAINTICON enumeration value.
            ''' </summary>
            WM_PAINTICON = &H26

            ''' <summary>
            ''' Specified WM_ICONERASEBKGND enumeration value.
            ''' </summary>
            WM_ICONERASEBKGND = &H27

            ''' <summary>
            ''' Specified WM_NEXTDLGCTL enumeration value.
            ''' </summary>
            WM_NEXTDLGCTL = &H28

            ''' <summary>
            ''' Specified WM_SPOOLERSTATUS enumeration value.
            ''' </summary>
            WM_SPOOLERSTATUS = &H2A

            ''' <summary>
            ''' Specified WM_DRAWITEM enumeration value.
            ''' </summary>
            WM_DRAWITEM = &H2B

            ''' <summary>
            ''' Specified WM_MEASUREITEM enumeration value.
            ''' </summary>
            WM_MEASUREITEM = &H2C

            ''' <summary>
            ''' Specified WM_DELETEITEM enumeration value.
            ''' </summary>
            WM_DELETEITEM = &H2D

            ''' <summary>
            ''' Specified WM_VKEYTOITEM enumeration value.
            ''' </summary>
            WM_VKEYTOITEM = &H2E

            ''' <summary>
            ''' Specified WM_CHARTOITEM enumeration value.
            ''' </summary>
            WM_CHARTOITEM = &H2F

            ''' <summary>
            ''' Specified WM_SETFONT enumeration value.
            ''' </summary>
            WM_SETFONT = &H30

            ''' <summary>
            ''' Specified WM_GETFONT enumeration value.
            ''' </summary>
            WM_GETFONT = &H31

            ''' <summary>
            ''' Specified WM_SETHOTKEY enumeration value.
            ''' </summary>
            WM_SETHOTKEY = &H32

            ''' <summary>
            ''' Specified WM_GETHOTKEY enumeration value.
            ''' </summary>
            WM_GETHOTKEY = &H33

            ''' <summary>
            ''' Specified WM_QUERYDRAGICON enumeration value.
            ''' </summary>
            WM_QUERYDRAGICON = &H37

            ''' <summary>
            ''' Specified WM_COMPAREITEM enumeration value.
            ''' </summary>
            WM_COMPAREITEM = &H39

            ''' <summary>
            ''' Specified WM_GETOBJECT enumeration value.
            ''' </summary>
            WM_GETOBJECT = &H3D

            ''' <summary>
            ''' Specified WM_COMPACTING enumeration value.
            ''' </summary>
            WM_COMPACTING = &H41

            ''' <summary>
            ''' Specified WM_COMMNOTIFY enumeration value.
            ''' </summary>
            WM_COMMNOTIFY = &H44

            ''' <summary>
            ''' Specified WM_WINDOWPOSCHANGING enumeration value.
            ''' </summary>
            WM_WINDOWPOSCHANGING = &H46

            ''' <summary>
            ''' Specified WM_WINDOWPOSCHANGED enumeration value.
            ''' </summary>
            WM_WINDOWPOSCHANGED = &H47

            ''' <summary>
            ''' Specified WM_POWER enumeration value.
            ''' </summary>
            WM_POWER = &H48

            ''' <summary>
            ''' Specified WM_COPYDATA enumeration value.
            ''' </summary>
            WM_COPYDATA = &H4A

            ''' <summary>
            ''' Specified WM_CANCELJOURNAL enumeration value.
            ''' </summary>
            WM_CANCELJOURNAL = &H4B

            ''' <summary>
            ''' Specified WM_NOTIFY enumeration value.
            ''' </summary>
            WM_NOTIFY = &H4E

            ''' <summary>
            ''' Specified WM_INPUTLANGCHANGEREQUEST enumeration value.
            ''' </summary>
            WM_INPUTLANGCHANGEREQUEST = &H50

            ''' <summary>
            ''' Specified WM_INPUTLANGCHANGE enumeration value.
            ''' </summary>
            WM_INPUTLANGCHANGE = &H51

            ''' <summary>
            ''' Specified WM_TCARD enumeration value.
            ''' </summary>
            WM_TCARD = &H52

            ''' <summary>
            ''' Specified WM_HELP enumeration value.
            ''' </summary>
            WM_HELP = &H53

            ''' <summary>
            ''' Specified WM_USERCHANGED enumeration value.
            ''' </summary>
            WM_USERCHANGED = &H54

            ''' <summary>
            ''' Specified WM_NOTIFYFORMAT enumeration value.
            ''' </summary>
            WM_NOTIFYFORMAT = &H55

            ''' <summary>
            ''' Specified WM_CONTEXTMENU enumeration value.
            ''' </summary>
            WM_CONTEXTMENU = &H7B

            ''' <summary>
            ''' Specified WM_STYLECHANGING enumeration value.
            ''' </summary>
            WM_STYLECHANGING = &H7C

            ''' <summary>
            ''' Specified WM_STYLECHANGED enumeration value.
            ''' </summary>
            WM_STYLECHANGED = &H7D

            ''' <summary>
            ''' Specified WM_DISPLAYCHANGE enumeration value.
            ''' </summary>
            WM_DISPLAYCHANGE = &H7E

            ''' <summary>
            ''' Specified WM_GETICON enumeration value.
            ''' </summary>
            WM_GETICON = &H7F

            ''' <summary>
            ''' Specified WM_SETICON enumeration value.
            ''' </summary>
            WM_SETICON = &H80

            ''' <summary>
            ''' Specified WM_NCCREATE enumeration value.
            ''' </summary>
            WM_NCCREATE = &H81

            ''' <summary>
            ''' Specified VK_RMENU enumeration value.
            ''' </summary>
            WM_NCDESTROY = &H82

            ''' <summary>
            ''' Specified WM_NCCALCSIZE enumeration value.
            ''' </summary>
            WM_NCCALCSIZE = &H83

            ''' <summary>
            ''' Specified WM_NCHITTEST enumeration value.
            ''' </summary>
            WM_NCHITTEST = &H84

            ''' <summary>
            ''' Specified WM_NCPAINT enumeration value.
            ''' </summary>
            WM_NCPAINT = &H85

            ''' <summary>
            ''' Specified WM_NCACTIVATE enumeration value.
            ''' </summary>
            WM_NCACTIVATE = &H86

            ''' <summary>
            ''' Specified WM_GETDLGCODE enumeration value.
            ''' </summary>
            WM_GETDLGCODE = &H87

            ''' <summary>
            ''' Specified WM_SYNCPAINT enumeration value.
            ''' </summary>
            WM_SYNCPAINT = &H88

            ''' <summary>
            ''' Specified WM_NCMOUSEMOVE enumeration value.
            ''' </summary>
            WM_NCMOUSEMOVE = &HA0

            ''' <summary>
            ''' Specified WM_NCLBUTTONDOWN enumeration value.
            ''' </summary>
            WM_NCLBUTTONDOWN = &HA1

            ''' <summary>
            ''' Specified WM_NCLBUTTONUP enumeration value.
            ''' </summary>
            WM_NCLBUTTONUP = &HA2

            ''' <summary>
            ''' Specified WM_NCLBUTTONDBLCLK enumeration value.
            ''' </summary>
            WM_NCLBUTTONDBLCLK = &HA3

            ''' <summary>
            ''' Specified WM_NCRBUTTONDOWN enumeration value.
            ''' </summary>
            WM_NCRBUTTONDOWN = &HA4

            ''' <summary>
            ''' Specified WM_NCRBUTTONUP enumeration value.
            ''' </summary>
            WM_NCRBUTTONUP = &HA5

            ''' <summary>
            ''' Specified WM_NCRBUTTONDBLCLK enumeration value.
            ''' </summary>
            WM_NCRBUTTONDBLCLK = &HA6

            ''' <summary>
            ''' Specified WM_NCMBUTTONDOWN enumeration value.
            ''' </summary>
            WM_NCMBUTTONDOWN = &HA7

            ''' <summary>
            ''' Specified WM_NCMBUTTONUP enumeration value.
            ''' </summary>
            WM_NCMBUTTONUP = &HA8

            ''' <summary>
            ''' Specified WM_NCMBUTTONDBLCLK enumeration value.
            ''' </summary>
            WM_NCMBUTTONDBLCLK = &HA9

            ''' <summary>
            ''' Specified WM_NCXBUTTONDOWN enumeration value.
            ''' </summary>
            WM_NCXBUTTONDOWN = &HAB

            ''' <summary>
            ''' Specified WM_NCXBUTTONUP enumeration value.
            ''' </summary>
            WM_NCXBUTTONUP = &HAC

            ''' <summary>
            ''' Specified WM_KEYDOWN enumeration value.
            ''' </summary>
            WM_KEYDOWN = &H100

            ''' <summary>
            ''' Specified WM_KEYUP enumeration value.
            ''' </summary>
            WM_KEYUP = &H101

            ''' <summary>
            ''' Specified WM_CHAR enumeration value.
            ''' </summary>
            WM_CHAR = &H102

            ''' <summary>
            ''' Specified WM_DEADCHAR enumeration value.
            ''' </summary>
            WM_DEADCHAR = &H103

            ''' <summary>
            ''' Specified WM_SYSKEYDOWN enumeration value.
            ''' </summary>
            WM_SYSKEYDOWN = &H104

            ''' <summary>
            ''' Specified WM_SYSKEYUP enumeration value.
            ''' </summary>
            WM_SYSKEYUP = &H105

            ''' <summary>
            ''' Specified WM_SYSCHAR enumeration value.
            ''' </summary>
            WM_SYSCHAR = &H106

            ''' <summary>
            ''' Specified WM_SYSDEADCHAR enumeration value.
            ''' </summary>
            WM_SYSDEADCHAR = &H107

            ''' <summary>
            ''' Specified WM_KEYLAST enumeration value.
            ''' </summary>
            WM_KEYLAST = &H108

            ''' <summary>
            ''' Specified WM_IME_STARTCOMPOSITION enumeration value.
            ''' </summary>
            WM_IME_STARTCOMPOSITION = &H10D

            ''' <summary>
            ''' Specified WM_IME_ENDCOMPOSITION enumeration value.
            ''' </summary>
            WM_IME_ENDCOMPOSITION = &H10E

            ''' <summary>
            ''' Specified WM_IME_COMPOSITION enumeration value.
            ''' </summary>
            WM_IME_COMPOSITION = &H10F

            ''' <summary>
            ''' Specified WM_IME_KEYLAST enumeration value.
            ''' </summary>
            WM_IME_KEYLAST = &H10F

            ''' <summary>
            ''' Specified WM_INITDIALOG enumeration value.
            ''' </summary>
            WM_INITDIALOG = &H110

            ''' <summary>
            ''' Specified WM_COMMAND enumeration value.
            ''' </summary>
            WM_COMMAND = &H111

            ''' <summary>
            ''' Specified WM_SYSCOMMAND enumeration value.
            ''' </summary>
            WM_SYSCOMMAND = &H112

            ''' <summary>
            ''' Specified WM_TIMER enumeration value.
            ''' </summary>
            WM_TIMER = &H113

            ''' <summary>
            ''' Specified WM_HSCROLL enumeration value.
            ''' </summary>
            WM_HSCROLL = &H114

            ''' <summary>
            ''' Specified WM_VSCROLL enumeration value.
            ''' </summary>
            WM_VSCROLL = &H115

            ''' <summary>
            ''' Specified WM_INITMENU enumeration value.
            ''' </summary>
            WM_INITMENU = &H116

            ''' <summary>
            ''' Specified WM_INITMENUPOPUP enumeration value.
            ''' </summary>
            WM_INITMENUPOPUP = &H117

            ''' <summary>
            ''' Specified WM_MENUSELECT enumeration value.
            ''' </summary>
            WM_MENUSELECT = &H11F

            ''' <summary>
            ''' Specified WM_MENUCHAR enumeration value.
            ''' </summary>
            WM_MENUCHAR = &H120

            ''' <summary>
            ''' Specified WM_ENTERIDLE enumeration value.
            ''' </summary>
            WM_ENTERIDLE = &H121

            ''' <summary>
            ''' Specified WM_MENURBUTTONUP enumeration value.
            ''' </summary>
            WM_MENURBUTTONUP = &H122

            ''' <summary>
            ''' Specified WM_MENUDRAG enumeration value.
            ''' </summary>
            WM_MENUDRAG = &H123

            ''' <summary>
            ''' Specified WM_MENUGETOBJECT enumeration value.
            ''' </summary>
            WM_MENUGETOBJECT = &H124

            ''' <summary>
            ''' Specified WM_UNINITMENUPOPUP enumeration value.
            ''' </summary>
            WM_UNINITMENUPOPUP = &H125

            ''' <summary>
            ''' Specified WM_MENUCOMMAND enumeration value.
            ''' </summary>
            WM_MENUCOMMAND = &H126

            ''' <summary>
            ''' Specified WM_CTLCOLORMSGBOX enumeration value.
            ''' </summary>
            WM_CTLCOLORMSGBOX = &H132

            ''' <summary>
            ''' Specified WM_CTLCOLOREDIT enumeration value.
            ''' </summary>
            WM_CTLCOLOREDIT = &H133

            ''' <summary>
            ''' Specified WM_CTLCOLORLISTBOX enumeration value.
            ''' </summary>
            WM_CTLCOLORLISTBOX = &H134

            ''' <summary>
            ''' Specified WM_CTLCOLORBTN enumeration value.
            ''' </summary>
            WM_CTLCOLORBTN = &H135

            ''' <summary>
            ''' Specified WM_CTLCOLORDLG enumeration value.
            ''' </summary>
            WM_CTLCOLORDLG = &H136

            ''' <summary>
            ''' Specified WM_CTLCOLORSCROLLBAR enumeration value.
            ''' </summary>
            WM_CTLCOLORSCROLLBAR = &H137

            ''' <summary>
            ''' Specified WM_CTLCOLORSTATIC enumeration value.
            ''' </summary>
            WM_CTLCOLORSTATIC = &H138

            ''' <summary>
            ''' Specified WM_MOUSEMOVE enumeration value.
            ''' </summary>
            WM_MOUSEMOVE = &H200

            ''' <summary>
            ''' Specified WM_LBUTTONDOWN enumeration value.
            ''' </summary>
            WM_LBUTTONDOWN = &H201

            ''' <summary>
            ''' Specified WM_LBUTTONUP enumeration value.
            ''' </summary>
            WM_LBUTTONUP = &H202

            ''' <summary>
            ''' Specified WM_LBUTTONDBLCLK enumeration value.
            ''' </summary>
            WM_LBUTTONDBLCLK = &H203

            ''' <summary>
            ''' Specified WM_RBUTTONDOWN enumeration value.
            ''' </summary>
            WM_RBUTTONDOWN = &H204

            ''' <summary>
            ''' Specified WM_RBUTTONUP enumeration value.
            ''' </summary>
            WM_RBUTTONUP = &H205

            ''' <summary>
            ''' Specified WM_RBUTTONDBLCLK enumeration value.
            ''' </summary>
            WM_RBUTTONDBLCLK = &H206

            ''' <summary>
            ''' Specified WM_MBUTTONDOWN enumeration value.
            ''' </summary>
            WM_MBUTTONDOWN = &H207

            ''' <summary>
            ''' Specified WM_MBUTTONUP enumeration value.
            ''' </summary>
            WM_MBUTTONUP = &H208

            ''' <summary>
            ''' Specified WM_MBUTTONDBLCLK enumeration value.
            ''' </summary>
            WM_MBUTTONDBLCLK = &H209

            ''' <summary>
            ''' Specified WM_MOUSEWHEEL enumeration value.
            ''' </summary>
            WM_MOUSEWHEEL = &H20A

            ''' <summary>
            ''' Specified WM_XBUTTONDOWN enumeration value.
            ''' </summary>
            WM_XBUTTONDOWN = &H20B

            ''' <summary>
            ''' Specified WM_XBUTTONUP enumeration value.
            ''' </summary>
            WM_XBUTTONUP = &H20C

            ''' <summary>
            ''' Specified WM_XBUTTONDBLCLK enumeration value.
            ''' </summary>
            WM_XBUTTONDBLCLK = &H20D

            ''' <summary>
            ''' Specified WM_PARENTNOTIFY enumeration value.
            ''' </summary>
            WM_PARENTNOTIFY = &H210

            ''' <summary>
            ''' Specified WM_ENTERMENULOOP enumeration value.
            ''' </summary>
            WM_ENTERMENULOOP = &H211

            ''' <summary>
            ''' Specified WM_EXITMENULOOP enumeration value.
            ''' </summary>
            WM_EXITMENULOOP = &H212

            ''' <summary>
            ''' Specified WM_NEXTMENU enumeration value.
            ''' </summary>
            WM_NEXTMENU = &H213

            ''' <summary>
            ''' Specified WM_SIZING enumeration value.
            ''' </summary>
            WM_SIZING = &H214

            ''' <summary>
            ''' Specified WM_CAPTURECHANGED enumeration value.
            ''' </summary>
            WM_CAPTURECHANGED = &H215

            ''' <summary>
            ''' Specified WM_MOVING enumeration value.
            ''' </summary>
            WM_MOVING = &H216

            ''' <summary>
            ''' Specified WM_DEVICECHANGE enumeration value.
            ''' </summary>
            WM_DEVICECHANGE = &H219

            ''' <summary>
            ''' Specified WM_MDICREATE enumeration value.
            ''' </summary>
            WM_MDICREATE = &H220

            ''' <summary>
            ''' Specified WM_MDIDESTROY enumeration value.
            ''' </summary>
            WM_MDIDESTROY = &H221

            ''' <summary>
            ''' Specified WM_MDIACTIVATE enumeration value.
            ''' </summary>
            WM_MDIACTIVATE = &H222

            ''' <summary>
            ''' Specified WM_MDIRESTORE enumeration value.
            ''' </summary>
            WM_MDIRESTORE = &H223

            ''' <summary>
            ''' Specified WM_MDINEXT enumeration value.
            ''' </summary>
            WM_MDINEXT = &H224

            ''' <summary>
            ''' Specified WM_MDIMAXIMIZE enumeration value.
            ''' </summary>
            WM_MDIMAXIMIZE = &H225

            ''' <summary>
            ''' Specified WM_MDITILE enumeration value.
            ''' </summary>
            WM_MDITILE = &H226

            ''' <summary>
            ''' Specified WM_MDICASCADE enumeration value.
            ''' </summary>
            WM_MDICASCADE = &H227

            ''' <summary>
            ''' Specified WM_MDIICONARRANGE enumeration value.
            ''' </summary>
            WM_MDIICONARRANGE = &H228

            ''' <summary>
            ''' Specified WM_MDIGETACTIVE enumeration value.
            ''' </summary>
            WM_MDIGETACTIVE = &H229

            ''' <summary>
            ''' Specified WM_MDISETMENU enumeration value.
            ''' </summary>
            WM_MDISETMENU = &H230

            ''' <summary>
            ''' Specified WM_ENTERSIZEMOVE enumeration value.
            ''' </summary>
            WM_ENTERSIZEMOVE = &H231

            ''' <summary>
            ''' Specified WM_EXITSIZEMOVE enumeration value.
            ''' </summary>
            WM_EXITSIZEMOVE = &H232

            ''' <summary>
            ''' Specified WM_DROPFILES enumeration value.
            ''' </summary>
            WM_DROPFILES = &H233

            ''' <summary>
            ''' Specified WM_MDIREFRESHMENU enumeration value.
            ''' </summary>
            WM_MDIREFRESHMENU = &H234

            ''' <summary>
            ''' Specified WM_IME_SETCONTEXT enumeration value.
            ''' </summary>
            WM_IME_SETCONTEXT = &H281

            ''' <summary>
            ''' Specified WM_IME_NOTIFY enumeration value.
            ''' </summary>
            WM_IME_NOTIFY = &H282

            ''' <summary>
            ''' Specified WM_IME_CONTROL enumeration value.
            ''' </summary>
            WM_IME_CONTROL = &H283

            ''' <summary>
            ''' Specified WM_IME_COMPOSITIONFULL enumeration value.
            ''' </summary>
            WM_IME_COMPOSITIONFULL = &H284

            ''' <summary>
            ''' Specified WM_IME_SELECT enumeration value.
            ''' </summary>
            WM_IME_SELECT = &H285

            ''' <summary>
            ''' Specified WM_IME_CHAR enumeration value.
            ''' </summary>
            WM_IME_CHAR = &H286

            ''' <summary>
            ''' Specified WM_IME_REQUEST enumeration value.
            ''' </summary>
            WM_IME_REQUEST = &H288

            ''' <summary>
            ''' Specified WM_IME_KEYDOWN enumeration value.
            ''' </summary>
            WM_IME_KEYDOWN = &H290

            ''' <summary>
            ''' Specified WM_IME_KEYUP enumeration value.
            ''' </summary>
            WM_IME_KEYUP = &H291

            ''' <summary>
            ''' Specified WM_MOUSEHOVER enumeration value.
            ''' </summary>
            WM_MOUSEHOVER = &H2A1
            WM_MOUSELEAVE = &H2A3
            WM_CUT = &H300
            WM_COPY = &H301
            WM_PASTE = &H302
            WM_CLEAR = &H303

            ''' <summary>
            ''' Specified WM_UNDO enumeration value.
            ''' </summary>
            WM_UNDO = &H304

            ''' <summary>
            ''' Specified WM_RENDERFORMAT enumeration value.
            ''' </summary>
            WM_RENDERFORMAT = &H305

            ''' <summary>
            ''' Specified WM_RENDERALLFORMATS enumeration value.
            ''' </summary>
            WM_RENDERALLFORMATS = &H306

            ''' <summary>
            ''' Specified WM_DESTROYCLIPBOARD enumeration value.
            ''' </summary>
            WM_DESTROYCLIPBOARD = &H307

            ''' <summary>
            ''' Specified WM_DRAWCLIPBOARD enumeration value.
            ''' </summary>
            WM_DRAWCLIPBOARD = &H308

            ''' <summary>
            ''' Specified WM_PAINTCLIPBOARD enumeration value.
            ''' </summary>
            WM_PAINTCLIPBOARD = &H309

            ''' <summary>
            ''' Specified WM_VSCROLLCLIPBOARD enumeration value.
            ''' </summary>
            WM_VSCROLLCLIPBOARD = &H30A

            ''' <summary>
            ''' Specified WM_SIZECLIPBOARD enumeration value.
            ''' </summary>
            WM_SIZECLIPBOARD = &H30B

            ''' <summary>
            ''' Specified WM_ASKCBFORMATNAME enumeration value.
            ''' </summary>
            WM_ASKCBFORMATNAME = &H30C

            ''' <summary>
            ''' Specified WM_CHANGECBCHAIN enumeration value.
            ''' </summary>
            WM_CHANGECBCHAIN = &H30D

            ''' <summary>
            ''' Specified WM_HSCROLLCLIPBOARD enumeration value.
            ''' </summary>
            WM_HSCROLLCLIPBOARD = &H30E

            ''' <summary>
            ''' Specified WM_QUERYNEWPALETTE enumeration value.
            ''' </summary>
            WM_QUERYNEWPALETTE = &H30F

            ''' <summary>
            ''' Specified WM_PALETTEISCHANGING enumeration value.
            ''' </summary>
            WM_PALETTEISCHANGING = &H310

            ''' <summary>
            ''' Specified WM_PALETTECHANGED enumeration value.
            ''' </summary>
            WM_PALETTECHANGED = &H311

            ''' <summary>
            ''' Specified WM_HOTKEY enumeration value.
            ''' </summary>
            WM_HOTKEY = &H312

            ''' <summary>
            ''' Specified WM_PRINT enumeration value.
            ''' </summary>
            WM_PRINT = &H317

            ''' <summary>
            ''' Specified WM_PRINTCLIENT enumeration value.
            ''' </summary>
            WM_PRINTCLIENT = &H318

            ''' <summary>
            ''' Specified WM_HANDHELDFIRST enumeration value.
            ''' </summary>
            WM_HANDHELDFIRST = &H358

            ''' <summary>
            ''' Specified WM_HANDHELDLAST enumeration value.
            ''' </summary>
            WM_HANDHELDLAST = &H35F

            ''' <summary>
            ''' Specified WM_AFXFIRST enumeration value.
            ''' </summary>
            WM_AFXFIRST = &H360

            ''' <summary>
            ''' Specified WM_AFXLAST enumeration value.
            ''' </summary>
            WM_AFXLAST = &H37F

            ''' <summary>
            ''' Specified WM_PENWINFIRST enumeration value.
            ''' </summary>
            WM_PENWINFIRST = &H380

            ''' <summary>
            ''' Specified WM_PENWINLAST enumeration value.
            ''' </summary>
            WM_PENWINLAST = &H38F

            ''' <summary>
            ''' Specified WM_APP enumeration value.
            ''' </summary>
            WM_APP = &H8000

            ''' <summary>
            ''' Specified WM_USER enumeration value.
            ''' </summary>
            WM_USER = &H400

            ''' <summary>
            ''' Specified WM_REFLECT enumeration value.
            ''' </summary>
            WM_REFLECT = WM_USER + &H1C00

            ''' <summary>
            ''' Specified WM_THEMECHANGED enumeration value.
            ''' </summary>
            WM_THEMECHANGED = &H31A
        End Enum

#End Region

#Region "Windows-Styles-Const"

        Friend Enum WindowStyles As UInteger
            WS_OVERLAPPED = &H0
            WS_POPUP = &H80000000UI
            WS_CHILD = &H40000000
            WS_MINIMIZE = &H20000000
            WS_VISIBLE = &H10000000
            WS_DISABLED = &H8000000
            WS_CLIPSIBLINGS = &H4000000
            WS_CLIPCHILDREN = &H2000000
            WS_MAXIMIZE = &H1000000
            WS_CAPTION = &HC00000
            WS_BORDER = &H800000
            WS_DLGFRAME = &H400000
            WS_VSCROLL = &H200000
            WS_HSCROLL = &H100000
            WS_SYSMENU = &H80000
            WS_THICKFRAME = &H40000
            WS_GROUP = &H20000
            WS_TABSTOP = &H10000
            WS_MINIMIZEBOX = &H20000
            WS_MAXIMIZEBOX = &H10000
            WS_TILED = &H0
            WS_ICONIC = &H20000000
            WS_SIZEBOX = &H40000
            WS_POPUPWINDOW = &H80880000UI
            WS_OVERLAPPEDWINDOW = &HCF0000
            WS_TILEDWINDOW = &HCF0000
            WS_CHILDWINDOW = &H40000000
        End Enum

        Friend Enum WindowExStyles
            GWL_STYLE = -16
            GWL_EXSTYLE = (-20)
            'GetWindowLong
            WS_EX_DLGMODALFRAME = &H1
            WS_EX_NOPARENTNOTIFY = &H4
            WS_EX_TOPMOST = &H8
            WS_EX_ACCEPTFILES = &H10
            WS_EX_TRANSPARENT = &H20
            WS_EX_MDICHILD = &H40
            WS_EX_TOOLWINDOW = &H80
            WS_EX_WINDOWEDGE = &H100
            WS_EX_CLIENTEDGE = &H200
            WS_EX_CONTEXTHELP = &H400
            WS_EX_RIGHT = &H1000
            'Gives a window generic right-aligned properties.This depends on the window class.
            WS_EX_LEFT = &H0
            WS_EX_RTLREADING = &H2000
            'Displays the window text using right-to-left reading order properties.
            WS_EX_LTRREADING = &H0
            WS_EX_LEFTSCROLLBAR = &H4000
            'Places a vertical scroll bar to the left of the client area.
            WS_EX_RIGHTSCROLLBAR = &H0
            WS_EX_CONTROLPARENT = &H10000
            WS_EX_STATICEDGE = &H20000
            WS_EX_APPWINDOW = &H40000
            WS_EX_OVERLAPPEDWINDOW = &H300
            WS_EX_PALETTEWINDOW = &H188
            WS_EX_LAYERED = &H80000
            WS_EX_NOACTIVATE = &H8000000
        End Enum

#End Region
    End Class
End Namespace