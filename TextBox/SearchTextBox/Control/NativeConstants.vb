

Namespace UIControls.TextBox
    Public NotInheritable Class NativeConstants
        Private Sub New()
        End Sub
        Public Const AUTOSUGGEST As Integer = &H10000000, AUTOSUGGEST_OFF As Integer = &H20000000, AUTOAPPEND As Integer = &H40000000, AUTOAPPEND_OFF As Integer = (CInt(&H80000000))

        Public Const ARW_BOTTOMLEFT As Integer = &H0, ARW_BOTTOMRIGHT As Integer = &H1, ARW_TOPLEFT As Integer = &H2, ARW_TOPRIGHT As Integer = &H3, ARW_LEFT As Integer = &H0, ARW_RIGHT As Integer = &H0, _
         ARW_UP As Integer = &H4, ARW_DOWN As Integer = &H4, ARW_HIDE As Integer = &H8, ACM_OPENA As Integer = (&H400 + 100), ACM_OPENW As Integer = (&H400 + 103), ADVF_NODATA As Integer = 1, _
         ADVF_ONLYONCE As Integer = 2, ADVF_PRIMEFIRST As Integer = 4

        Public Const BCM_GETIDEALSIZE As Integer = &H1601, BI_RGB As Integer = 0, BS_PATTERN As Integer = 3, BITSPIXEL As Integer = 12, BDR_RAISEDOUTER As Integer = &H1, BDR_SUNKENOUTER As Integer = &H2, _
         BDR_RAISEDINNER As Integer = &H4, BDR_SUNKENINNER As Integer = &H8, BDR_RAISED As Integer = &H5, BDR_SUNKEN As Integer = &HA, BF_LEFT As Integer = &H1, BF_TOP As Integer = &H2, _
         BF_RIGHT As Integer = &H4, BF_BOTTOM As Integer = &H8, BF_ADJUST As Integer = &H2000, BF_FLAT As Integer = &H4000, BF_MIDDLE As Integer = &H800, BFFM_INITIALIZED As Integer = 1, _
         BFFM_SELCHANGED As Integer = 2, BFFM_SETSELECTION As Integer = &H400 + 103, BFFM_ENABLEOK As Integer = &H400 + 101, BS_PUSHBUTTON As Integer = &H0, BS_DEFPUSHBUTTON As Integer = &H1, BS_MULTILINE As Integer = &H2000, _
         BS_PUSHLIKE As Integer = &H1000, BS_OWNERDRAW As Integer = &HB, BS_RADIOBUTTON As Integer = &H4, BS_3STATE As Integer = &H5, BS_GROUPBOX As Integer = &H7, BS_LEFT As Integer = &H100, _
         BS_RIGHT As Integer = &H200, BS_CENTER As Integer = &H300, BS_TOP As Integer = &H400, BS_BOTTOM As Integer = &H800, BS_VCENTER As Integer = &HC00, BS_RIGHTBUTTON As Integer = &H20, _
         BN_CLICKED As Integer = 0, BM_SETCHECK As Integer = &HF1, BM_SETSTATE As Integer = &HF3, BM_CLICK As Integer = &HF5

        Public Const CDERR_DIALOGFAILURE As Integer = &HFFFF, CDERR_STRUCTSIZE As Integer = &H1, CDERR_INITIALIZATION As Integer = &H2, CDERR_NOTEMPLATE As Integer = &H3, CDERR_NOHINSTANCE As Integer = &H4, CDERR_LOADSTRFAILURE As Integer = &H5, _
         CDERR_FINDRESFAILURE As Integer = &H6, CDERR_LOADRESFAILURE As Integer = &H7, CDERR_LOCKRESFAILURE As Integer = &H8, CDERR_MEMALLOCFAILURE As Integer = &H9, CDERR_MEMLOCKFAILURE As Integer = &HA, CDERR_NOHOOK As Integer = &HB, _
         CDERR_REGISTERMSGFAIL As Integer = &HC, CFERR_NOFONTS As Integer = &H2001, CFERR_MAXLESSTHANMIN As Integer = &H2002, CC_RGBINIT As Integer = &H1, CC_FULLOPEN As Integer = &H2, CC_PREVENTFULLOPEN As Integer = &H4, _
         CC_SHOWHELP As Integer = &H8, CC_ENABLEHOOK As Integer = &H10, CC_SOLIDCOLOR As Integer = &H80, CC_ANYCOLOR As Integer = &H100, CF_SCREENFONTS As Integer = &H1, CF_SHOWHELP As Integer = &H4, _
         CF_ENABLEHOOK As Integer = &H8, CF_INITTOLOGFONTSTRUCT As Integer = &H40, CF_EFFECTS As Integer = &H100, CF_APPLY As Integer = &H200, CF_SCRIPTSONLY As Integer = &H400, CF_NOVECTORFONTS As Integer = &H800, _
         CF_NOSIMULATIONS As Integer = &H1000, CF_LIMITSIZE As Integer = &H2000, CF_FIXEDPITCHONLY As Integer = &H4000, CF_FORCEFONTEXIST As Integer = &H10000, CF_TTONLY As Integer = &H40000, CF_SELECTSCRIPT As Integer = &H400000, _
         CF_NOVERTFONTS As Integer = &H1000000, CP_WINANSI As Integer = 1004

        ' <desktop>
        ' Internet Explorer (icon on desktop)
        ' Start Menu\Programs
        ' My Documents
        ' <user name>\Favorites
        ' Start Menu\Programs\Startup
        ' <user name>\Recent
        ' <user name>\SendTo
        ' <user name>\Start Menu
        ' <user name>\Desktop
        ' <user name>\Application Data
        ' <user name>\Local Settings\Applicaiton Data (non roaming)
        ' All Users\Application Data
        ' GetSystemDirectory()
        ' C:\Program Files
        Public Const cmb4 As Integer = &H473, CS_DBLCLKS As Integer = &H8, CS_DROPSHADOW As Integer = &H20000, CF_TEXT As Integer = 1, CF_BITMAP As Integer = 2, CF_METAFILEPICT As Integer = 3, _
         CF_SYLK As Integer = 4, CF_DIF As Integer = 5, CF_TIFF As Integer = 6, CF_OEMTEXT As Integer = 7, CF_DIB As Integer = 8, CF_PALETTE As Integer = 9, _
         CF_PENDATA As Integer = 10, CF_RIFF As Integer = 11, CF_WAVE As Integer = 12, CF_UNICODETEXT As Integer = 13, CF_ENHMETAFILE As Integer = 14, CF_HDROP As Integer = 15, _
         CF_LOCALE As Integer = 16, CLSCTX_INPROC_SERVER As Integer = &H1, CLSCTX_LOCAL_SERVER As Integer = &H4, CW_USEDEFAULT As Integer = (CInt(&H80000000)), CWP_SKIPINVISIBLE As Integer = &H1, COLOR_WINDOW As Integer = 5, _
         CB_ERR As Integer = (-1), CBN_SELCHANGE As Integer = 1, CBN_DBLCLK As Integer = 2, CBN_EDITCHANGE As Integer = 5, CBN_EDITUPDATE As Integer = 6, CBN_DROPDOWN As Integer = 7, _
         CBN_CLOSEUP As Integer = 8, CBN_SELENDOK As Integer = 9, CBS_SIMPLE As Integer = &H1, CBS_DROPDOWN As Integer = &H2, CBS_DROPDOWNLIST As Integer = &H3, CBS_OWNERDRAWFIXED As Integer = &H10, _
         CBS_OWNERDRAWVARIABLE As Integer = &H20, CBS_AUTOHSCROLL As Integer = &H40, CBS_HASSTRINGS As Integer = &H200, CBS_NOINTEGRALHEIGHT As Integer = &H400, CB_GETEDITSEL As Integer = &H140, CB_LIMITTEXT As Integer = &H141, _
         CB_SETEDITSEL As Integer = &H142, CB_ADDSTRING As Integer = &H143, CB_DELETESTRING As Integer = &H144, CB_GETCURSEL As Integer = &H147, CB_INSERTSTRING As Integer = &H14A, CB_RESETCONTENT As Integer = &H14B, _
         CB_FINDSTRING As Integer = &H14C, CB_SETCURSEL As Integer = &H14E, CB_SHOWDROPDOWN As Integer = &H14F, CB_GETITEMDATA As Integer = &H150, CB_SETITEMHEIGHT As Integer = &H153, CB_GETITEMHEIGHT As Integer = &H154, _
         CB_GETDROPPEDSTATE As Integer = &H157, CB_FINDSTRINGEXACT As Integer = &H158, CB_SETDROPPEDWIDTH As Integer = &H160, CDRF_DODEFAULT As Integer = &H0, CDRF_NEWFONT As Integer = &H2, CDRF_SKIPDEFAULT As Integer = &H4, _
         CDRF_NOTIFYPOSTPAINT As Integer = &H10, CDRF_NOTIFYITEMDRAW As Integer = &H20, CDRF_NOTIFYSUBITEMDRAW As Integer = CDRF_NOTIFYITEMDRAW, CDDS_PREPAINT As Integer = &H1, CDDS_POSTPAINT As Integer = &H2, CDDS_ITEM As Integer = &H10000, _
         CDDS_SUBITEM As Integer = &H20000, CDDS_ITEMPREPAINT As Integer = (&H10000 Or &H1), CDDS_ITEMPOSTPAINT As Integer = (&H10000 Or &H2), CDIS_SELECTED As Integer = &H1, CDIS_GRAYED As Integer = &H2, CDIS_DISABLED As Integer = &H4, _
         CDIS_CHECKED As Integer = &H8, CDIS_FOCUS As Integer = &H10, CDIS_DEFAULT As Integer = &H20, CDIS_HOT As Integer = &H40, CDIS_MARKED As Integer = &H80, CDIS_INDETERMINATE As Integer = &H100, _
         CDIS_SHOWKEYBOARDCUES As Integer = &H200, CLR_NONE As Integer = CInt(&HFFFFFFFF), CLR_DEFAULT As Integer = CInt(&HFF000000), CCS_NORESIZE As Integer = &H4, CCS_NOPARENTALIGN As Integer = &H8, CCS_NODIVIDER As Integer = &H40, _
         CBEM_INSERTITEMA As Integer = (&H400 + 1), CBEM_GETITEMA As Integer = (&H400 + 4), CBEM_SETITEMA As Integer = (&H400 + 5), CBEM_INSERTITEMW As Integer = (&H400 + 11), CBEM_SETITEMW As Integer = (&H400 + 12), CBEM_GETITEMW As Integer = (&H400 + 13), _
         CBEN_ENDEDITA As Integer = ((0 - 800) - 5), CBEN_ENDEDITW As Integer = ((0 - 800) - 6), CONNECT_E_NOCONNECTION As Integer = CInt(&H80040200), CONNECT_E_CANNOTCONNECT As Integer = CInt(&H80040202), CTRLINFO_EATS_RETURN As Integer = 1, CTRLINFO_EATS_ESCAPE As Integer = 2, _
         CSIDL_DESKTOP As Integer = &H0, CSIDL_INTERNET As Integer = &H1, CSIDL_PROGRAMS As Integer = &H2, CSIDL_PERSONAL As Integer = &H5, CSIDL_FAVORITES As Integer = &H6, CSIDL_STARTUP As Integer = &H7, _
         CSIDL_RECENT As Integer = &H8, CSIDL_SENDTO As Integer = &H9, CSIDL_STARTMENU As Integer = &HB, CSIDL_DESKTOPDIRECTORY As Integer = &H10, CSIDL_TEMPLATES As Integer = &H15, CSIDL_APPDATA As Integer = &H1A, _
         CSIDL_LOCAL_APPDATA As Integer = &H1C, CSIDL_INTERNET_CACHE As Integer = &H20, CSIDL_COOKIES As Integer = &H21, CSIDL_HISTORY As Integer = &H22, CSIDL_COMMON_APPDATA As Integer = &H23, CSIDL_SYSTEM As Integer = &H25, _
         CSIDL_PROGRAM_FILES As Integer = &H26, CSIDL_PROGRAM_FILES_COMMON As Integer = &H2B
        ' C:\Program Files\Common
        Public Const DUPLICATE As Integer = &H6, DISPID_UNKNOWN As Integer = (-1), DISPID_PROPERTYPUT As Integer = (-3), DISPATCH_METHOD As Integer = &H1, DISPATCH_PROPERTYGET As Integer = &H2, DISPATCH_PROPERTYPUT As Integer = &H4, _
         DV_E_DVASPECT As Integer = CInt(&H8004006B), DISP_E_MEMBERNOTFOUND As Integer = CInt(&H80020003), DISP_E_PARAMNOTFOUND As Integer = CInt(&H80020004), DISP_E_EXCEPTION As Integer = CInt(&H80020009), DEFAULT_GUI_FONT As Integer = 17, DIB_RGB_COLORS As Integer = 0, _
         DRAGDROP_E_NOTREGISTERED As Integer = CInt(&H80040100), DRAGDROP_E_ALREADYREGISTERED As Integer = CInt(&H80040101), DUPLICATE_SAME_ACCESS As Integer = &H2, DFC_CAPTION As Integer = 1, DFC_MENU As Integer = 2, DFC_SCROLL As Integer = 3, _
         DFC_BUTTON As Integer = 4, DFCS_CAPTIONCLOSE As Integer = &H0, DFCS_CAPTIONMIN As Integer = &H1, DFCS_CAPTIONMAX As Integer = &H2, DFCS_CAPTIONRESTORE As Integer = &H3, DFCS_CAPTIONHELP As Integer = &H4, _
         DFCS_MENUARROW As Integer = &H0, DFCS_MENUCHECK As Integer = &H1, DFCS_MENUBULLET As Integer = &H2, DFCS_SCROLLUP As Integer = &H0, DFCS_SCROLLDOWN As Integer = &H1, DFCS_SCROLLLEFT As Integer = &H2, _
         DFCS_SCROLLRIGHT As Integer = &H3, DFCS_SCROLLCOMBOBOX As Integer = &H5, DFCS_BUTTONCHECK As Integer = &H0, DFCS_BUTTONRADIO As Integer = &H4, DFCS_BUTTON3STATE As Integer = &H8, DFCS_BUTTONPUSH As Integer = &H10, _
         DFCS_INACTIVE As Integer = &H100, DFCS_PUSHED As Integer = &H200, DFCS_CHECKED As Integer = &H400, DFCS_FLAT As Integer = &H4000, DT_LEFT As Integer = &H0, DT_RIGHT As Integer = &H2, _
         DT_VCENTER As Integer = &H4, DT_SINGLELINE As Integer = &H20, DT_NOCLIP As Integer = &H100, DT_CALCRECT As Integer = &H400, DT_NOPREFIX As Integer = &H800, DT_EDITCONTROL As Integer = &H2000, _
         DT_EXPANDTABS As Integer = &H40, DT_END_ELLIPSIS As Integer = &H8000, DT_RTLREADING As Integer = &H20000, DT_WORDBREAK As Integer = &H10, DCX_WINDOW As Integer = &H1, DCX_CACHE As Integer = &H2, _
         DCX_LOCKWINDOWUPDATE As Integer = &H400, DI_NORMAL As Integer = &H3, DLGC_WANTARROWS As Integer = &H1, DLGC_WANTTAB As Integer = &H2, DLGC_WANTALLKEYS As Integer = &H4, DLGC_WANTCHARS As Integer = &H80, _
         DTM_SETSYSTEMTIME As Integer = (&H1000 + 2), DTM_SETRANGE As Integer = (&H1000 + 4), DTM_SETFORMATA As Integer = (&H1000 + 5), DTM_SETFORMATW As Integer = (&H1000 + 50), DTM_SETMCCOLOR As Integer = (&H1000 + 6), DTM_SETMCFONT As Integer = (&H1000 + 9), _
         DTS_UPDOWN As Integer = &H1, DTS_SHOWNONE As Integer = &H2, DTS_LONGDATEFORMAT As Integer = &H4, DTS_TIMEFORMAT As Integer = &H9, DTS_RIGHTALIGN As Integer = &H20, DTN_DATETIMECHANGE As Integer = ((0 - 760) + 1), _
         DTN_USERSTRINGA As Integer = ((0 - 760) + 2), DTN_USERSTRINGW As Integer = ((0 - 760) + 15), DTN_WMKEYDOWNA As Integer = ((0 - 760) + 3), DTN_WMKEYDOWNW As Integer = ((0 - 760) + 16), DTN_FORMATA As Integer = ((0 - 760) + 4), DTN_FORMATW As Integer = ((0 - 760) + 17), _
         DTN_FORMATQUERYA As Integer = ((0 - 760) + 5), DTN_FORMATQUERYW As Integer = ((0 - 760) + 18), DTN_DROPDOWN As Integer = ((0 - 760) + 6), DTN_CLOSEUP As Integer = ((0 - 760) + 7), DVASPECT_CONTENT As Integer = 1, DVASPECT_TRANSPARENT As Integer = 32, _
         DVASPECT_OPAQUE As Integer = 16

        Public Const E_NOTIMPL As Integer = CInt(&H80004001), E_OUTOFMEMORY As Integer = CInt(&H8007000E), E_INVALIDARG As Integer = CInt(&H80070057), E_NOINTERFACE As Integer = CInt(&H80004002), E_FAIL As Integer = CInt(&H80004005), E_ABORT As Integer = CInt(&H80004004), _
         E_UNEXPECTED As Integer = CInt(&H8000FFFF), INET_E_DEFAULT_ACTION As Integer = CInt(&H800C0011), ETO_OPAQUE As Integer = &H2, ETO_CLIPPED As Integer = &H4, EMR_POLYTEXTOUTA As Integer = 96, EMR_POLYTEXTOUTW As Integer = 97, _
         EDGE_RAISED As Integer = (&H1 Or &H4), EDGE_SUNKEN As Integer = (&H2 Or &H8), EDGE_ETCHED As Integer = (&H2 Or &H4), EDGE_BUMP As Integer = (&H1 Or &H8), ES_LEFT As Integer = &H0, ES_CENTER As Integer = &H1, _
         ES_RIGHT As Integer = &H2, ES_MULTILINE As Integer = &H4, ES_UPPERCASE As Integer = &H8, ES_LOWERCASE As Integer = &H10, ES_AUTOVSCROLL As Integer = &H40, ES_AUTOHSCROLL As Integer = &H80, _
         ES_NOHIDESEL As Integer = &H100, ES_READONLY As Integer = &H800, ES_PASSWORD As Integer = &H20, EN_CHANGE As Integer = &H300, EN_HSCROLL As Integer = &H601, EN_VSCROLL As Integer = &H602, _
         EN_ALIGN_LTR_EC As Integer = &H700, EN_ALIGN_RTL_EC As Integer = &H701, EC_LEFTMARGIN As Integer = &H1, EC_RIGHTMARGIN As Integer = &H2, EM_GETSEL As Integer = &HB0, EM_SETSEL As Integer = &HB1, _
         EM_SCROLL As Integer = &HB5, EM_SCROLLCARET As Integer = &HB7, EM_GETMODIFY As Integer = &HB8, EM_SETMODIFY As Integer = &HB9, EM_GETLINECOUNT As Integer = &HBA, EM_REPLACESEL As Integer = &HC2, _
         EM_GETLINE As Integer = &HC4, EM_LIMITTEXT As Integer = &HC5, EM_CANUNDO As Integer = &HC6, EM_UNDO As Integer = &HC7, EM_SETPASSWORDCHAR As Integer = &HCC, EM_GETPASSWORDCHAR As Integer = &HD2, _
         EM_EMPTYUNDOBUFFER As Integer = &HCD, EM_SETREADONLY As Integer = &HCF, EM_SETMARGINS As Integer = &HD3, EM_POSFROMCHAR As Integer = &HD6, EM_CHARFROMPOS As Integer = &HD7, EM_LINEFROMCHAR As Integer = &HC9, _
         EM_LINEINDEX As Integer = &HBB

        Public Const FNERR_SUBCLASSFAILURE As Integer = &H3001, FNERR_INVALIDFILENAME As Integer = &H3002, FNERR_BUFFERTOOSMALL As Integer = &H3003, FRERR_BUFFERLENGTHZERO As Integer = &H4001, FADF_BSTR As Integer = (&H100), FADF_UNKNOWN As Integer = (&H200), _
         FADF_DISPATCH As Integer = (&H400), FADF_VARIANT As Integer = (CInt(&H800)), FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000, FORMAT_MESSAGE_IGNORE_INSERTS As Integer = &H200, FVIRTKEY As Integer = &H1, FSHIFT As Integer = &H4, _
         FALT As Integer = &H10

        Public Const GMEM_MOVEABLE As Integer = &H2, GMEM_ZEROINIT As Integer = &H40, GMEM_DDESHARE As Integer = &H2000, GWL_WNDPROC As Integer = (-4), GWL_HWNDPARENT As Integer = (-8), GWL_STYLE As Integer = (-16), _
         GWL_EXSTYLE As Integer = (-20), GWL_ID As Integer = (-12), GW_HWNDFIRST As Integer = 0, GW_HWNDLAST As Integer = 1, GW_HWNDNEXT As Integer = 2, GW_HWNDPREV As Integer = 3, _
         GW_CHILD As Integer = 5, GMR_VISIBLE As Integer = 0, GMR_DAYSTATE As Integer = 1, GDI_ERROR As Integer = (CInt(&HFFFFFFFF)), GDTR_MIN As Integer = &H1, GDTR_MAX As Integer = &H2, _
         GDT_VALID As Integer = 0, GDT_NONE As Integer = 1

        Public Const HOLLOW_BRUSH As Integer = 5, HC_ACTION As Integer = 0, HC_GETNEXT As Integer = 1, HC_SKIP As Integer = 2, HTNOWHERE As Integer = 0, HTCLIENT As Integer = 1, _
         HTBOTTOM As Integer = 15, HTBOTTOMRIGHT As Integer = 17, HELPINFO_WINDOW As Integer = &H1, HCF_HIGHCONTRASTON As Integer = &H1, HDI_ORDER As Integer = &H80, HDM_GETITEMCOUNT As Integer = (&H1200 + 0), _
         HDM_INSERTITEMA As Integer = (&H1200 + 1), HDM_INSERTITEMW As Integer = (&H1200 + 10), HDM_GETITEMA As Integer = (&H1200 + 3), HDM_GETITEMW As Integer = (&H1200 + 11), HDM_SETITEMA As Integer = (&H1200 + 4), HDM_SETITEMW As Integer = (&H1200 + 12), _
         HDN_ITEMCHANGINGA As Integer = ((0 - 300) - 0), HDN_ITEMCHANGINGW As Integer = ((0 - 300) - 20), HDN_ITEMCHANGEDA As Integer = ((0 - 300) - 1), HDN_ITEMCHANGEDW As Integer = ((0 - 300) - 21), HDN_ITEMCLICKA As Integer = ((0 - 300) - 2), HDN_ITEMCLICKW As Integer = ((0 - 300) - 22), _
         HDN_ITEMDBLCLICKA As Integer = ((0 - 300) - 3), HDN_ITEMDBLCLICKW As Integer = ((0 - 300) - 23), HDN_DIVIDERDBLCLICKA As Integer = ((0 - 300) - 5), HDN_DIVIDERDBLCLICKW As Integer = ((0 - 300) - 25), HDN_BEGINTDRAG As Integer = ((0 - 300) - 10), HDN_BEGINTRACKA As Integer = ((0 - 300) - 6), _
         HDN_BEGINTRACKW As Integer = ((0 - 300) - 26), HDN_ENDDRAG As Integer = ((0 - 300) - 11), HDN_ENDTRACKA As Integer = ((0 - 300) - 7), HDN_ENDTRACKW As Integer = ((0 - 300) - 27), HDN_TRACKA As Integer = ((0 - 300) - 8), HDN_TRACKW As Integer = ((0 - 300) - 28), _
         HDN_GETDISPINFOA As Integer = ((0 - 300) - 9), HDN_GETDISPINFOW As Integer = ((0 - 300) - 29)


        ' ImageList drawing effects
        '
        Public Const IME_CMODE_NATIVE As Integer = &H1, IME_CMODE_KATAKANA As Integer = &H2, IME_CMODE_FULLSHAPE As Integer = &H8, INPLACE_E_NOTOOLSPACE As Integer = CInt(&H800401A1), ICON_SMALL As Integer = 0, ICON_BIG As Integer = 1, _
         IDC_ARROW As Integer = 32512, IDC_IBEAM As Integer = 32513, IDC_WAIT As Integer = 32514, IDC_CROSS As Integer = 32515, IDC_SIZEALL As Integer = 32646, IDC_SIZENWSE As Integer = 32642, _
         IDC_SIZENESW As Integer = 32643, IDC_SIZEWE As Integer = 32644, IDC_SIZENS As Integer = 32645, IDC_UPARROW As Integer = 32516, IDC_NO As Integer = 32648, IDC_APPSTARTING As Integer = 32650, _
         IDC_HELP As Integer = 32651, IMAGE_ICON As Integer = 1, IMAGE_CURSOR As Integer = 2, ICC_LISTVIEW_CLASSES As Integer = &H1, ICC_TREEVIEW_CLASSES As Integer = &H2, ICC_BAR_CLASSES As Integer = &H4, _
         ICC_TAB_CLASSES As Integer = &H8, ICC_PROGRESS_CLASS As Integer = &H20, ICC_DATE_CLASSES As Integer = &H100, ILC_MASK As Integer = &H1, ILC_COLOR As Integer = &H0, ILC_COLOR4 As Integer = &H4, _
         ILC_COLOR8 As Integer = &H8, ILC_COLOR16 As Integer = &H10, ILC_COLOR24 As Integer = &H18, ILC_COLOR32 As Integer = &H20, ILD_NORMAL As Integer = &H0, ILD_TRANSPARENT As Integer = &H1, _
         ILD_MASK As Integer = &H10, ILD_ROP As Integer = &H40, ILS_NORMAL As Integer = &H0, ILS_GLOW As Integer = &H1, ILS_SHADOW As Integer = &H2, ILS_SATURATE As Integer = &H4, _
         ILS_ALPHA As Integer = &H8

        Public Const IDM_PRINT As Integer = 27, IDM_PAGESETUP As Integer = 2004, IDM_PRINTPREVIEW As Integer = 2003, IDM_PROPERTIES As Integer = 28, IDM_SAVEAS As Integer = 71

        Public Const CSC_NAVIGATEFORWARD As Integer = &H1, CSC_NAVIGATEBACK As Integer = &H2

        Public Const STG_E_INVALIDFUNCTION As Integer = CInt(&H80030001)
        Public Const STG_E_FILENOTFOUND As Integer = CInt(&H80030002)
        Public Const STG_E_PATHNOTFOUND As Integer = CInt(&H80030003)
        Public Const STG_E_TOOMANYOPENFILES As Integer = CInt(&H80030004)
        Public Const STG_E_ACCESSDENIED As Integer = CInt(&H80030005)
        Public Const STG_E_INVALIDHANDLE As Integer = CInt(&H80030006)
        Public Const STG_E_INSUFFICIENTMEMORY As Integer = CInt(&H80030008)
        Public Const STG_E_INVALIDPOINTER As Integer = CInt(&H80030009)
        Public Const STG_E_NOMOREFILES As Integer = CInt(&H80030012)
        Public Const STG_E_DISKISWRITEPROTECTED As Integer = CInt(&H80030013)
        Public Const STG_E_SEEKERROR As Integer = CInt(&H80030019)
        Public Const STG_E_WRITEFAULT As Integer = CInt(&H8003001D)
        Public Const STG_E_READFAULT As Integer = CInt(&H8003001E)
        Public Const STG_E_SHAREVIOLATION As Integer = CInt(&H80030020)
        Public Const STG_E_LOCKVIOLATION As Integer = CInt(&H80030021)

        Public Const KEYEVENTF_KEYUP As Integer = &H2

        Public Const LOGPIXELSX As Integer = 88, LOGPIXELSY As Integer = 90, LB_ERR As Integer = (-1), LB_ERRSPACE As Integer = (-2), LBN_SELCHANGE As Integer = 1, LBN_DBLCLK As Integer = 2, _
         LB_ADDSTRING As Integer = &H180, LB_INSERTSTRING As Integer = &H181, LB_DELETESTRING As Integer = &H182, LB_RESETCONTENT As Integer = &H184, LB_SETSEL As Integer = &H185, LB_SETCURSEL As Integer = &H186, _
         LB_GETSEL As Integer = &H187, LB_GETCARETINDEX As Integer = &H19F, LB_GETCURSEL As Integer = &H188, LB_GETTEXT As Integer = &H189, LB_GETTEXTLEN As Integer = &H18A, LB_GETTOPINDEX As Integer = &H18E, _
         LB_FINDSTRING As Integer = &H18F, LB_GETSELCOUNT As Integer = &H190, LB_GETSELITEMS As Integer = &H191, LB_SETTABSTOPS As Integer = &H192, LB_SETHORIZONTALEXTENT As Integer = &H194, LB_SETCOLUMNWIDTH As Integer = &H195, _
         LB_SETTOPINDEX As Integer = &H197, LB_GETITEMRECT As Integer = &H198, LB_SETITEMHEIGHT As Integer = &H1A0, LB_GETITEMHEIGHT As Integer = &H1A1, LB_FINDSTRINGEXACT As Integer = &H1A2, LB_ITEMFROMPOINT As Integer = &H1A9, _
         LB_SETLOCALE As Integer = &H1A5


        Public Const LBS_NOTIFY As Integer = &H1, LBS_MULTIPLESEL As Integer = &H8, LBS_OWNERDRAWFIXED As Integer = &H10, LBS_OWNERDRAWVARIABLE As Integer = &H20, LBS_HASSTRINGS As Integer = &H40, LBS_USETABSTOPS As Integer = &H80, _
         LBS_NOINTEGRALHEIGHT As Integer = &H100, LBS_MULTICOLUMN As Integer = &H200, LBS_WANTKEYBOARDINPUT As Integer = &H400, LBS_EXTENDEDSEL As Integer = &H800, LBS_DISABLENOSCROLL As Integer = &H1000, LBS_NOSEL As Integer = &H4000, _
         LOCK_WRITE As Integer = &H1, LOCK_EXCLUSIVE As Integer = &H2, LOCK_ONLYONCE As Integer = &H4, LV_VIEW_TILE As Integer = &H4, LVBKIF_SOURCE_NONE As Integer = &H0, LVBKIF_SOURCE_URL As Integer = &H2, _
         LVBKIF_STYLE_NORMAL As Integer = &H0, LVBKIF_STYLE_TILE As Integer = &H10, LVS_ICON As Integer = &H0, LVS_REPORT As Integer = &H1, LVS_SMALLICON As Integer = &H2, LVS_LIST As Integer = &H3, _
         LVS_SINGLESEL As Integer = &H4, LVS_SHOWSELALWAYS As Integer = &H8, LVS_SORTASCENDING As Integer = &H10, LVS_SORTDESCENDING As Integer = &H20, LVS_SHAREIMAGELISTS As Integer = &H40, LVS_NOLABELWRAP As Integer = &H80, _
         LVS_AUTOARRANGE As Integer = &H100, LVS_EDITLABELS As Integer = &H200, LVS_NOSCROLL As Integer = &H2000, LVS_ALIGNTOP As Integer = &H0, LVS_ALIGNLEFT As Integer = &H800, LVS_NOCOLUMNHEADER As Integer = &H4000, _
         LVS_NOSORTHEADER As Integer = CInt(&H8000), LVS_OWNERDATA As Integer = &H1000, LVSCW_AUTOSIZE As Integer = -1, LVSCW_AUTOSIZE_USEHEADER As Integer = -2, LVM_SCROLL As Integer = (&H1000 + 20), LVM_SETBKCOLOR As Integer = (&H1000 + 1), _
         LVM_SETBKIMAGEA As Integer = (&H1000 + 68), LVM_SETBKIMAGEW As Integer = (&H1000 + 138), LVM_SETINFOTIP As Integer = (&H1000 + 173), LVSIL_NORMAL As Integer = 0, LVSIL_SMALL As Integer = 1, LVSIL_STATE As Integer = 2, _
         LVM_SETIMAGELIST As Integer = (&H1000 + 3), LVM_SETTOOLTIPS As Integer = (&H1000 + 74), LVIF_TEXT As Integer = &H1, LVIF_IMAGE As Integer = &H2, LVIF_INDENT As Integer = &H10, LVIF_PARAM As Integer = &H4, _
         LVIF_STATE As Integer = &H8, LVIF_GROUPID As Integer = &H100, LVIF_COLUMNS As Integer = &H200, LVIS_FOCUSED As Integer = &H1, LVIS_SELECTED As Integer = &H2, LVIS_CUT As Integer = &H4, _
         LVIS_DROPHILITED As Integer = &H8, LVIS_OVERLAYMASK As Integer = &HF00, LVIS_STATEIMAGEMASK As Integer = &HF000, LVM_GETITEMA As Integer = (&H1000 + 5), LVM_GETITEMW As Integer = (&H1000 + 75), LVM_SETITEMA As Integer = (&H1000 + 6), _
         LVM_SETITEMW As Integer = (&H1000 + 76), LVM_SETITEMPOSITION32 As Integer = (&H1000 + 49), LVM_INSERTITEMA As Integer = (&H1000 + 7), LVM_INSERTITEMW As Integer = (&H1000 + 77), LVM_DELETEITEM As Integer = (&H1000 + 8), LVM_DELETECOLUMN As Integer = (&H1000 + 28), _
         LVM_DELETEALLITEMS As Integer = (&H1000 + 9), LVM_UPDATE As Integer = (&H1000 + 42), LVNI_FOCUSED As Integer = &H1, LVNI_SELECTED As Integer = &H2, LVM_GETNEXTITEM As Integer = (&H1000 + 12), LVFI_PARAM As Integer = &H1, _
         LVFI_NEARESTXY As Integer = &H40, LVFI_PARTIAL As Integer = &H8, LVFI_STRING As Integer = &H2, LVM_FINDITEMA As Integer = (&H1000 + 13), LVM_FINDITEMW As Integer = (&H1000 + 83), LVIR_BOUNDS As Integer = 0, _
         LVIR_ICON As Integer = 1, LVIR_LABEL As Integer = 2, LVIR_SELECTBOUNDS As Integer = 3, LVM_GETITEMRECT As Integer = (&H1000 + 14), LVM_GETSUBITEMRECT As Integer = (&H1000 + 56), LVM_GETSTRINGWIDTHA As Integer = (&H1000 + 17), _
         LVM_GETSTRINGWIDTHW As Integer = (&H1000 + 87), LVHT_NOWHERE As Integer = &H1, LVHT_ONITEMICON As Integer = &H2, LVHT_ONITEMLABEL As Integer = &H4, LVHT_ABOVE As Integer = &H8, LVHT_BELOW As Integer = &H10, _
         LVHT_RIGHT As Integer = &H20, LVHT_LEFT As Integer = &H40, LVHT_ONITEM As Integer = (&H2 Or &H4 Or &H8), LVHT_ONITEMSTATEICON As Integer = &H8, LVM_SUBITEMHITTEST As Integer = (&H1000 + 57), LVM_HITTEST As Integer = (&H1000 + 18), _
         LVM_ENSUREVISIBLE As Integer = (&H1000 + 19), LVA_DEFAULT As Integer = &H0, LVA_ALIGNLEFT As Integer = &H1, LVA_ALIGNTOP As Integer = &H2, LVA_SNAPTOGRID As Integer = &H5, LVM_ARRANGE As Integer = (&H1000 + 22), _
         LVM_EDITLABELA As Integer = (&H1000 + 23), LVM_EDITLABELW As Integer = (&H1000 + 118), LVCDI_ITEM As Integer = &H0, LVCF_FMT As Integer = &H1, LVCF_WIDTH As Integer = &H2, LVCF_TEXT As Integer = &H4, _
         LVCF_SUBITEM As Integer = &H8, LVCF_IMAGE As Integer = &H10, LVCF_ORDER As Integer = &H20, LVGA_HEADER_LEFT As Integer = &H1, LVGA_HEADER_CENTER As Integer = &H2, LVGA_HEADER_RIGHT As Integer = &H4, _
         LVGA_FOOTER_LEFT As Integer = &H8, LVGA_FOOTER_CENTER As Integer = &H10, LVGA_FOOTER_RIGHT As Integer = &H20, LVGF_NONE As Integer = &H0, LVGF_HEADER As Integer = &H1, LVGF_FOOTER As Integer = &H2, _
         LVGF_STATE As Integer = &H4, LVGF_ALIGN As Integer = &H8, LVGF_GROUPID As Integer = &H10, LVGS_NORMAL As Integer = &H0, LVGS_COLLAPSED As Integer = &H1, LVGS_HIDDEN As Integer = &H2, _
         LVIM_AFTER As Integer = &H1, LVTVIF_FIXEDSIZE As Integer = &H3, LVTVIM_TILESIZE As Integer = &H1, LVTVIM_COLUMNS As Integer = &H2, LVM_ENABLEGROUPVIEW As Integer = (&H1000 + 157), LVM_MOVEITEMTOGROUP As Integer = (&H1000 + 154), _
         LVM_GETCOLUMNA As Integer = (&H1000 + 25), LVM_GETCOLUMNW As Integer = (&H1000 + 95), LVM_SETCOLUMNA As Integer = (&H1000 + 26), LVM_SETCOLUMNW As Integer = (&H1000 + 96), LVM_INSERTCOLUMNA As Integer = (&H1000 + 27), LVM_INSERTCOLUMNW As Integer = (&H1000 + 97), _
         LVM_INSERTGROUP As Integer = (&H1000 + 145), LVM_REMOVEGROUP As Integer = (&H1000 + 150), LVM_INSERTMARKHITTEST As Integer = (&H1000 + 168), LVM_REMOVEALLGROUPS As Integer = (&H1000 + 160), LVM_GETCOLUMNWIDTH As Integer = (&H1000 + 29), LVM_SETCOLUMNWIDTH As Integer = (&H1000 + 30), _
         LVM_SETINSERTMARK As Integer = (&H1000 + 166), LVM_GETHEADER As Integer = (&H1000 + 31), LVM_SETTEXTCOLOR As Integer = (&H1000 + 36), LVM_SETTEXTBKCOLOR As Integer = (&H1000 + 38), LVM_GETTOPINDEX As Integer = (&H1000 + 39), LVM_SETITEMPOSITION As Integer = (&H1000 + 15), _
         LVM_SETITEMSTATE As Integer = (&H1000 + 43), LVM_GETITEMSTATE As Integer = (&H1000 + 44), LVM_GETITEMTEXTA As Integer = (&H1000 + 45), LVM_GETITEMTEXTW As Integer = (&H1000 + 115), LVM_GETHOTITEM As Integer = (&H1000 + 61), LVM_SETITEMTEXTA As Integer = (&H1000 + 46), _
         LVM_SETITEMTEXTW As Integer = (&H1000 + 116), LVM_SETITEMCOUNT As Integer = (&H1000 + 47), LVM_SORTITEMS As Integer = (&H1000 + 48), LVM_GETSELECTEDCOUNT As Integer = (&H1000 + 50), LVM_GETISEARCHSTRINGA As Integer = (&H1000 + 52), LVM_GETISEARCHSTRINGW As Integer = (&H1000 + 117), _
         LVM_SETEXTENDEDLISTVIEWSTYLE As Integer = (&H1000 + 54), LVM_SETVIEW As Integer = (&H1000 + 142), LVM_GETGROUPINFO As Integer = (&H1000 + 149), LVM_SETGROUPINFO As Integer = (&H1000 + 147), LVM_HASGROUP As Integer = (&H1000 + 161), LVM_SETTILEVIEWINFO As Integer = (&H1000 + 162), _
         LVM_GETTILEVIEWINFO As Integer = (&H1000 + 163), LVM_GETINSERTMARK As Integer = (&H1000 + 167), LVM_GETINSERTMARKRECT As Integer = (&H1000 + 169), LVM_SETINSERTMARKCOLOR As Integer = (&H1000 + 170), LVM_GETINSERTMARKCOLOR As Integer = (&H1000 + 171), LVM_ISGROUPVIEWENABLED As Integer = (&H1000 + 175), _
         LVS_EX_UNDERLINEHOT As Integer = &H800, LVS_EX_GRIDLINES As Integer = &H1, LVS_EX_CHECKBOXES As Integer = &H4, LVS_EX_TRACKSELECT As Integer = &H8, LVS_EX_HEADERDRAGDROP As Integer = &H10, LVS_EX_FULLROWSELECT As Integer = &H20, _
         LVS_EX_ONECLICKACTIVATE As Integer = &H40, LVS_EX_TWOCLICKACTIVATE As Integer = &H80, LVS_EX_INFOTIP As Integer = &H400, LVN_ITEMCHANGING As Integer = ((0 - 100) - 0), LVN_ITEMCHANGED As Integer = ((0 - 100) - 1), LVN_BEGINLABELEDITA As Integer = ((0 - 100) - 5), _
         LVN_BEGINLABELEDITW As Integer = ((0 - 100) - 75), LVN_ENDLABELEDITA As Integer = ((0 - 100) - 6), LVN_ENDLABELEDITW As Integer = ((0 - 100) - 76), LVN_COLUMNCLICK As Integer = ((0 - 100) - 8), LVN_BEGINDRAG As Integer = ((0 - 100) - 9), LVN_BEGINRDRAG As Integer = ((0 - 100) - 11), _
         LVN_ODFINDITEMA As Integer = ((0 - 100) - 52), LVN_ODFINDITEMW As Integer = ((0 - 100) - 79), LVN_ITEMACTIVATE As Integer = ((0 - 100) - 14), LVN_GETDISPINFOA As Integer = ((0 - 100) - 50), LVN_GETDISPINFOW As Integer = ((0 - 100) - 77), LVN_ODCACHEHINT As Integer = ((0 - 100) - 13), _
         LVN_ODSTATECHANGED As Integer = ((0 - 100) - 15), LVN_SETDISPINFOA As Integer = ((0 - 100) - 51), LVN_SETDISPINFOW As Integer = ((0 - 100) - 78), LVN_GETINFOTIPA As Integer = ((0 - 100) - 57), LVN_GETINFOTIPW As Integer = ((0 - 100) - 58), LVN_KEYDOWN As Integer = ((0 - 100) - 55), _
         LWA_COLORKEY As Integer = &H1, LWA_ALPHA As Integer = &H2

        Public Const LANG_NEUTRAL As Integer = &H0, LOCALE_IFIRSTDAYOFWEEK As Integer = &H100C

        Public Const MEMBERID_NIL As Integer = (-1), MAX_PATH As Integer = 260, MM_TEXT As Integer = 1, MM_ANISOTROPIC As Integer = 8, MK_LBUTTON As Integer = &H1, MK_RBUTTON As Integer = &H2, _
         MK_SHIFT As Integer = &H4, MK_CONTROL As Integer = &H8, MK_MBUTTON As Integer = &H10, MNC_EXECUTE As Integer = 2, MNC_SELECT As Integer = 3, MIIM_STATE As Integer = &H1, _
         MIIM_ID As Integer = &H2, MIIM_SUBMENU As Integer = &H4, MIIM_TYPE As Integer = &H10, MIIM_DATA As Integer = &H20, MB_OK As Integer = &H0, MF_BYCOMMAND As Integer = &H0, _
         MF_BYPOSITION As Integer = &H400, MF_ENABLED As Integer = &H0, MF_GRAYED As Integer = &H1, MF_POPUP As Integer = &H10, MF_SYSMENU As Integer = &H2000, MFT_MENUBREAK As Integer = &H40, _
         MFT_SEPARATOR As Integer = &H800, MFT_RIGHTORDER As Integer = &H2000, MFT_RIGHTJUSTIFY As Integer = &H4000, MDITILE_VERTICAL As Integer = &H0, MDITILE_HORIZONTAL As Integer = &H1, MCM_SETMAXSELCOUNT As Integer = (&H1000 + 4), _
         MCM_SETSELRANGE As Integer = (&H1000 + 6), MCM_GETMONTHRANGE As Integer = (&H1000 + 7), MCM_GETMINREQRECT As Integer = (&H1000 + 9), MCM_SETCOLOR As Integer = (&H1000 + 10), MCM_SETTODAY As Integer = (&H1000 + 12), MCM_GETTODAY As Integer = (&H1000 + 13), _
         MCM_HITTEST As Integer = (&H1000 + 14), MCM_SETFIRSTDAYOFWEEK As Integer = (&H1000 + 15), MCM_SETRANGE As Integer = (&H1000 + 18), MCM_SETMONTHDELTA As Integer = (&H1000 + 20), MCM_GETMAXTODAYWIDTH As Integer = (&H1000 + 21), MCHT_TITLE As Integer = &H10000, _
         MCHT_CALENDAR As Integer = &H20000, MCHT_TODAYLINK As Integer = &H30000, MCHT_TITLEBK As Integer = (&H10000), MCHT_TITLEMONTH As Integer = (&H10000 Or &H1), MCHT_TITLEYEAR As Integer = (&H10000 Or &H2), MCHT_TITLEBTNNEXT As Integer = (&H10000 Or &H1000000 Or &H3), _
         MCHT_TITLEBTNPREV As Integer = (&H10000 Or &H2000000 Or &H3), MCHT_CALENDARBK As Integer = (&H20000), MCHT_CALENDARDATE As Integer = (&H20000 Or &H1), MCHT_CALENDARDATENEXT As Integer = ((&H20000 Or &H1) Or &H1000000), MCHT_CALENDARDATEPREV As Integer = ((&H20000 Or &H1) Or &H2000000), MCHT_CALENDARDAY As Integer = (&H20000 Or &H2), _
         MCHT_CALENDARWEEKNUM As Integer = (&H20000 Or &H3), MCSC_TEXT As Integer = 1, MCSC_TITLEBK As Integer = 2, MCSC_TITLETEXT As Integer = 3, MCSC_MONTHBK As Integer = 4, MCSC_TRAILINGTEXT As Integer = 5, _
         MCN_SELCHANGE As Integer = ((0 - 750) + 1), MCN_GETDAYSTATE As Integer = ((0 - 750) + 3), MCN_SELECT As Integer = ((0 - 750) + 4), MCS_DAYSTATE As Integer = &H1, MCS_MULTISELECT As Integer = &H2, MCS_WEEKNUMBERS As Integer = &H4, _
         MCS_NOTODAYCIRCLE As Integer = &H8, MCS_NOTODAY As Integer = &H10, MSAA_MENU_SIG As Integer = (CInt(&HAA0DF00D))

        Public Const NIM_ADD As Integer = &H0, NIM_MODIFY As Integer = &H1, NIM_DELETE As Integer = &H2, NIF_MESSAGE As Integer = &H1, NIF_ICON As Integer = &H2, NIF_TIP As Integer = &H4, _
         NFR_ANSI As Integer = 1, NFR_UNICODE As Integer = 2, NM_CLICK As Integer = ((0 - 0) - 2), NM_DBLCLK As Integer = ((0 - 0) - 3), NM_RCLICK As Integer = ((0 - 0) - 5), NM_RDBLCLK As Integer = ((0 - 0) - 6), _
         NM_CUSTOMDRAW As Integer = ((0 - 0) - 12), NM_RELEASEDCAPTURE As Integer = ((0 - 0) - 16)

        Public Const OFN_READONLY As Integer = &H1, OFN_OVERWRITEPROMPT As Integer = &H2, OFN_HIDEREADONLY As Integer = &H4, OFN_NOCHANGEDIR As Integer = &H8, OFN_SHOWHELP As Integer = &H10, OFN_ENABLEHOOK As Integer = &H20, _
         OFN_NOVALIDATE As Integer = &H100, OFN_ALLOWMULTISELECT As Integer = &H200, OFN_PATHMUSTEXIST As Integer = &H800, OFN_FILEMUSTEXIST As Integer = &H1000, OFN_CREATEPROMPT As Integer = &H2000, OFN_EXPLORER As Integer = &H80000, _
         OFN_NODEREFERENCELINKS As Integer = &H100000, OFN_ENABLESIZING As Integer = &H800000, OFN_USESHELLITEM As Integer = &H1000000, OLEIVERB_PRIMARY As Integer = 0, OLEIVERB_SHOW As Integer = -1, OLEIVERB_HIDE As Integer = -3, _
         OLEIVERB_UIACTIVATE As Integer = -4, OLEIVERB_INPLACEACTIVATE As Integer = -5, OLEIVERB_DISCARDUNDOSTATE As Integer = -6, OLEIVERB_PROPERTIES As Integer = -7, OLE_E_NOCONNECTION As Integer = CInt(&H80040004), OLE_E_PROMPTSAVECANCELLED As Integer = CInt(&H8004000C), _
         OLEMISC_RECOMPOSEONRESIZE As Integer = &H1, OLEMISC_INSIDEOUT As Integer = &H80, OLEMISC_ACTIVATEWHENVISIBLE As Integer = &H100, OLEMISC_ACTSLIKEBUTTON As Integer = &H1000, OLEMISC_SETCLIENTSITEFIRST As Integer = &H20000, OBJ_PEN As Integer = 1, _
         OBJ_BRUSH As Integer = 2, OBJ_DC As Integer = 3, OBJ_METADC As Integer = 4, OBJ_PAL As Integer = 5, OBJ_FONT As Integer = 6, OBJ_BITMAP As Integer = 7, _
         OBJ_REGION As Integer = 8, OBJ_METAFILE As Integer = 9, OBJ_MEMDC As Integer = 10, OBJ_EXTPEN As Integer = 11, OBJ_ENHMETADC As Integer = 12, ODS_CHECKED As Integer = &H8, _
         ODS_COMBOBOXEDIT As Integer = &H1000, ODS_DEFAULT As Integer = &H20, ODS_DISABLED As Integer = &H4, ODS_FOCUS As Integer = &H10, ODS_GRAYED As Integer = &H2, ODS_HOTLIGHT As Integer = &H40, _
         ODS_INACTIVE As Integer = &H80, ODS_NOACCEL As Integer = &H100, ODS_NOFOCUSRECT As Integer = &H200, ODS_SELECTED As Integer = &H1, OLECLOSE_SAVEIFDIRTY As Integer = 0, OLECLOSE_PROMPTSAVE As Integer = 2

        Public Const PDERR_SETUPFAILURE As Integer = &H1001, PDERR_PARSEFAILURE As Integer = &H1002, PDERR_RETDEFFAILURE As Integer = &H1003, PDERR_LOADDRVFAILURE As Integer = &H1004, PDERR_GETDEVMODEFAIL As Integer = &H1005, PDERR_INITFAILURE As Integer = &H1006, _
         PDERR_NODEVICES As Integer = &H1007, PDERR_NODEFAULTPRN As Integer = &H1008, PDERR_DNDMMISMATCH As Integer = &H1009, PDERR_CREATEICFAILURE As Integer = &H100A, PDERR_PRINTERNOTFOUND As Integer = &H100B, PDERR_DEFAULTDIFFERENT As Integer = &H100C, _
         PD_NOSELECTION As Integer = &H4, PD_NOPAGENUMS As Integer = &H8, PD_NOCURRENTPAGE As Integer = &H800000, PD_COLLATE As Integer = &H10, PD_PRINTTOFILE As Integer = &H20, PD_SHOWHELP As Integer = &H800, _
         PD_ENABLEPRINTHOOK As Integer = &H1000, PD_DISABLEPRINTTOFILE As Integer = &H80000, PD_NONETWORKBUTTON As Integer = &H200000, PSD_MINMARGINS As Integer = &H1, PSD_MARGINS As Integer = &H2, PSD_INHUNDREDTHSOFMILLIMETERS As Integer = &H8, _
         PSD_DISABLEMARGINS As Integer = &H10, PSD_DISABLEPRINTER As Integer = &H20, PSD_DISABLEORIENTATION As Integer = &H100, PSD_DISABLEPAPER As Integer = &H200, PSD_SHOWHELP As Integer = &H800, PSD_ENABLEPAGESETUPHOOK As Integer = &H2000, _
         PSD_NONETWORKBUTTON As Integer = &H200000, PS_SOLID As Integer = 0, PS_DOT As Integer = 2, PLANES As Integer = 14, PRF_CHECKVISIBLE As Integer = &H1, PRF_NONCLIENT As Integer = &H2, _
         PRF_CLIENT As Integer = &H4, PRF_ERASEBKGND As Integer = &H8, PRF_CHILDREN As Integer = &H10, PM_NOREMOVE As Integer = &H0, PM_REMOVE As Integer = &H1, PM_NOYIELD As Integer = &H2, _
         PBM_SETRANGE As Integer = (&H400 + 1), PBM_SETPOS As Integer = (&H400 + 2), PBM_SETSTEP As Integer = (&H400 + 4), PBM_SETRANGE32 As Integer = (&H400 + 6), PBM_SETBARCOLOR As Integer = (&H400 + 9), PBM_SETBKCOLOR As Integer = (&H2000 + 1), _
         PSM_SETTITLEA As Integer = (&H400 + 111), PSM_SETTITLEW As Integer = (&H400 + 120), PSM_SETFINISHTEXTA As Integer = (&H400 + 115), PSM_SETFINISHTEXTW As Integer = (&H400 + 121), PATCOPY As Integer = &HF00021, PATINVERT As Integer = &H5A0049

        Public Const PBS_SMOOTH As Integer = &H1

        Public Const QS_KEY As Integer = &H1, QS_MOUSEMOVE As Integer = &H2, QS_MOUSEBUTTON As Integer = &H4, QS_POSTMESSAGE As Integer = &H8, QS_TIMER As Integer = &H10, QS_PAINT As Integer = &H20, _
         QS_SENDMESSAGE As Integer = &H40, QS_HOTKEY As Integer = &H80, QS_ALLPOSTMESSAGE As Integer = &H100, QS_MOUSE As Integer = QS_MOUSEMOVE Or QS_MOUSEBUTTON, QS_INPUT As Integer = QS_MOUSE Or QS_KEY, QS_ALLEVENTS As Integer = QS_INPUT Or QS_POSTMESSAGE Or QS_TIMER Or QS_PAINT Or QS_HOTKEY, _
         QS_ALLINPUT As Integer = QS_INPUT Or QS_POSTMESSAGE Or QS_TIMER Or QS_PAINT Or QS_HOTKEY Or QS_SENDMESSAGE

        Public Const RPC_E_CHANGED_MODE As Integer = CInt(&H80010106), RGN_AND As Integer = 1, RPC_E_CANTCALLOUT_ININPUTSYNCCALL As Integer = CInt(&H8001010D), RGN_DIFF As Integer = 4, RDW_INVALIDATE As Integer = &H1, RDW_ERASE As Integer = &H4, _
         RDW_ALLCHILDREN As Integer = &H80, RDW_FRAME As Integer = &H400, RB_INSERTBANDA As Integer = (&H400 + 1), RB_INSERTBANDW As Integer = (&H400 + 10)

        Public Const stc4 As Integer = &H443, SHGFP_TYPE_CURRENT As Integer = 0, STGM_READ As Integer = &H0, STGM_WRITE As Integer = &H1, STGM_READWRITE As Integer = &H2, STGM_SHARE_EXCLUSIVE As Integer = &H10, _
         STGM_CREATE As Integer = &H1000, STGM_TRANSACTED As Integer = &H10000, STGM_CONVERT As Integer = &H20000, STGM_DELETEONRELEASE As Integer = &H4000000, STARTF_USESHOWWINDOW As Integer = &H1, SB_HORZ As Integer = 0, _
         SB_VERT As Integer = 1, SB_CTL As Integer = 2, SB_LINEUP As Integer = 0, SB_LINELEFT As Integer = 0, SB_LINEDOWN As Integer = 1, SB_LINERIGHT As Integer = 1, _
         SB_PAGEUP As Integer = 2, SB_PAGELEFT As Integer = 2, SB_PAGEDOWN As Integer = 3, SB_PAGERIGHT As Integer = 3, SB_THUMBPOSITION As Integer = 4, SB_THUMBTRACK As Integer = 5, _
         SB_LEFT As Integer = 6, SB_RIGHT As Integer = 7, SB_ENDSCROLL As Integer = 8, SB_TOP As Integer = 6, SB_BOTTOM As Integer = 7, SORT_DEFAULT As Integer = &H0, _
         SUBLANG_DEFAULT As Integer = &H1, SW_HIDE As Integer = 0, SW_NORMAL As Integer = 1, SW_SHOWMINIMIZED As Integer = 2, SW_SHOWMAXIMIZED As Integer = 3, SW_MAXIMIZE As Integer = 3, _
         SW_SHOWNOACTIVATE As Integer = 4, SW_SHOW As Integer = 5, SW_MINIMIZE As Integer = 6, SW_SHOWMINNOACTIVE As Integer = 7, SW_SHOWNA As Integer = 8, SW_RESTORE As Integer = 9, _
         SW_MAX As Integer = 10, SWP_NOSIZE As Integer = &H1, SWP_NOMOVE As Integer = &H2, SWP_NOZORDER As Integer = &H4, SWP_NOACTIVATE As Integer = &H10, SWP_SHOWWINDOW As Integer = &H40, _
         SWP_HIDEWINDOW As Integer = &H80, SWP_DRAWFRAME As Integer = &H20, SM_CXSCREEN As Integer = 0, SM_CYSCREEN As Integer = 1, SM_CXVSCROLL As Integer = 2, SM_CYHSCROLL As Integer = 3, _
         SM_CYCAPTION As Integer = 4, SM_CXBORDER As Integer = 5, SM_CYBORDER As Integer = 6, SM_CYVTHUMB As Integer = 9, SM_CXHTHUMB As Integer = 10, SM_CXICON As Integer = 11, _
         SM_CYICON As Integer = 12, SM_CXCURSOR As Integer = 13, SM_CYCURSOR As Integer = 14, SM_CYMENU As Integer = 15, SM_CYKANJIWINDOW As Integer = 18, SM_MOUSEPRESENT As Integer = 19, _
         SM_CYVSCROLL As Integer = 20, SM_CXHSCROLL As Integer = 21, SM_DEBUG As Integer = 22, SM_SWAPBUTTON As Integer = 23, SM_CXMIN As Integer = 28, SM_CYMIN As Integer = 29, _
         SM_CXSIZE As Integer = 30, SM_CYSIZE As Integer = 31, SM_CXFRAME As Integer = 32, SM_CYFRAME As Integer = 33, SM_CXMINTRACK As Integer = 34, SM_CYMINTRACK As Integer = 35, _
         SM_CXDOUBLECLK As Integer = 36, SM_CYDOUBLECLK As Integer = 37, SM_CXICONSPACING As Integer = 38, SM_CYICONSPACING As Integer = 39, SM_MENUDROPALIGNMENT As Integer = 40, SM_PENWINDOWS As Integer = 41, _
         SM_DBCSENABLED As Integer = 42, SM_CMOUSEBUTTONS As Integer = 43, SM_CXFIXEDFRAME As Integer = 7, SM_CYFIXEDFRAME As Integer = 8, SM_SECURE As Integer = 44, SM_CXEDGE As Integer = 45, _
         SM_CYEDGE As Integer = 46, SM_CXMINSPACING As Integer = 47, SM_CYMINSPACING As Integer = 48, SM_CXSMICON As Integer = 49, SM_CYSMICON As Integer = 50, SM_CYSMCAPTION As Integer = 51, _
         SM_CXSMSIZE As Integer = 52, SM_CYSMSIZE As Integer = 53, SM_CXMENUSIZE As Integer = 54, SM_CYMENUSIZE As Integer = 55, SM_ARRANGE As Integer = 56, SM_CXMINIMIZED As Integer = 57, _
         SM_CYMINIMIZED As Integer = 58, SM_CXMAXTRACK As Integer = 59, SM_CYMAXTRACK As Integer = 60, SM_CXMAXIMIZED As Integer = 61, SM_CYMAXIMIZED As Integer = 62, SM_NETWORK As Integer = 63, _
         SM_CLEANBOOT As Integer = 67, SM_CXDRAG As Integer = 68, SM_CYDRAG As Integer = 69, SM_SHOWSOUNDS As Integer = 70, SM_CXMENUCHECK As Integer = 71, SM_CYMENUCHECK As Integer = 72, _
         SM_MIDEASTENABLED As Integer = 74, SM_MOUSEWHEELPRESENT As Integer = 75, SM_XVIRTUALSCREEN As Integer = 76, SM_YVIRTUALSCREEN As Integer = 77, SM_CXVIRTUALSCREEN As Integer = 78, SM_CYVIRTUALSCREEN As Integer = 79, _
         SM_CMONITORS As Integer = 80, SM_SAMEDISPLAYFORMAT As Integer = 81, SM_REMOTESESSION As Integer = &H1000

        Public Const SND_SYNC As Integer = 0, SND_ASYNC As Integer = &H1, SND_NODEFAULT As Integer = &H2, SND_MEMORY As Integer = &H4, SND_LOOP As Integer = &H8, SND_PURGE As Integer = &H40, _
         SND_FILENAME As Integer = &H20000, SND_NOSTOP As Integer = &H10

        Public Const MB_ICONHAND As Integer = &H10, MB_ICONQUESTION As Integer = &H20, MB_ICONEXCLAMATION As Integer = &H30, MB_ICONASTERISK As Integer = &H40


        Public Const FLASHW_STOP As Integer = 0, FLASHW_CAPTION As Integer = &H1, FLASHW_TRAY As Integer = &H2, FLASHW_ALL As Integer = FLASHW_CAPTION Or FLASHW_TRAY, FLASHW_TIMER As Integer = &H4, FLASHW_TIMERNOFG As Integer = &HC

        Public Const HLP_FILE As Integer = 1, HLP_KEYWORD As Integer = 2, HLP_NAVIGATOR As Integer = 3, HLP_OBJECT As Integer = 4

        Public Const SHGFI_ICON As Integer = &H100, SHGFI_DISPLAYNAME As Integer = &H200, SHGFI_TYPENAME As Integer = &H400, SHGFI_ATTRIBUTES As Integer = &H800, SHGFI_ICONLOCATION As Integer = &H1000, SHGFI_EXETYPE As Integer = &H2000, _
         SHGFI_SYSICONINDEX As Integer = &H4000, SHGFI_LINKOVERLAY As Integer = &H8000, SHGFI_SELECTED As Integer = &H10000, SHGFI_ATTR_SPECIFIED As Integer = &H20000, SHGFI_LARGEICON As Integer = &H0, SHGFI_SMALLICON As Integer = &H1, _
         SHGFI_OPENICON As Integer = &H2, SHGFI_SHELLICONSIZE As Integer = &H4, SHGFI_PIDL As Integer = &H8, SHGFI_USEFILEATTRIBUTES As Integer = &H10, SHGFI_ADDOVERLAYS As Integer = &H20, SHGFI_OVERLAYINDEX As Integer = &H40

        Public Const SW_SCROLLCHILDREN As Integer = &H1, SW_INVALIDATE As Integer = &H2, SW_ERASE As Integer = &H4, SW_SMOOTHSCROLL As Integer = &H10, SC_SIZE As Integer = &HF000, SC_MINIMIZE As Integer = &HF020, _
         SC_MAXIMIZE As Integer = &HF030, SC_CLOSE As Integer = &HF060, SC_KEYMENU As Integer = &HF100, SC_RESTORE As Integer = &HF120, SC_MOVE As Integer = &HF010, SS_LEFT As Integer = &H0, _
         SS_CENTER As Integer = &H1, SS_RIGHT As Integer = &H2, SS_OWNERDRAW As Integer = &HD, SS_NOPREFIX As Integer = &H80, SS_SUNKEN As Integer = &H1000, SBS_HORZ As Integer = &H0, _
         SBS_VERT As Integer = &H1, SIF_RANGE As Integer = &H1, SIF_PAGE As Integer = &H2, SIF_POS As Integer = &H4, SIF_TRACKPOS As Integer = &H10, SIF_ALL As Integer = (&H1 Or &H2 Or &H4 Or &H10), _
         SPI_GETFONTSMOOTHING As Integer = &H4A, SPI_GETDROPSHADOW As Integer = &H1024, SPI_GETFLATMENU As Integer = &H1022, SPI_GETFONTSMOOTHINGTYPE As Integer = &H200A, SPI_GETFONTSMOOTHINGCONTRAST As Integer = &H200C, SPI_ICONHORIZONTALSPACING As Integer = &HD, _
         SPI_ICONVERTICALSPACING As Integer = &H18, SPI_GETICONTITLEWRAP As Integer = &H19, SPI_GETICONTITLELOGFONT As Integer = &H1F, SPI_GETKEYBOARDCUES As Integer = &H100A, SPI_GETKEYBOARDDELAY As Integer = &H16, SPI_GETKEYBOARDPREF As Integer = &H44, _
         SPI_GETKEYBOARDSPEED As Integer = &HA, SPI_GETMOUSEHOVERWIDTH As Integer = &H62, SPI_GETMOUSEHOVERHEIGHT As Integer = &H64, SPI_GETMOUSEHOVERTIME As Integer = &H66, SPI_GETMOUSESPEED As Integer = &H70, SPI_GETMENUDROPALIGNMENT As Integer = &H1B, _
         SPI_GETMENUFADE As Integer = &H1012, SPI_GETMENUSHOWDELAY As Integer = &H6A, SPI_GETCOMBOBOXANIMATION As Integer = &H1004, SPI_GETGRADIENTCAPTIONS As Integer = &H1008, SPI_GETHOTTRACKING As Integer = &H100E, SPI_GETLISTBOXSMOOTHSCROLLING As Integer = &H1006, _
         SPI_GETMENUANIMATION As Integer = &H1002, SPI_GETSELECTIONFADE As Integer = &H1014, SPI_GETTOOLTIPANIMATION As Integer = &H1016, SPI_GETUIEFFECTS As Integer = &H103E, SPI_GETACTIVEWINDOWTRACKING As Integer = &H1000, SPI_GETACTIVEWNDTRKTIMEOUT As Integer = &H2002, _
         SPI_GETANIMATION As Integer = &H48, SPI_GETBORDER As Integer = &H5, SPI_GETCARETWIDTH As Integer = &H2006, SM_CYFOCUSBORDER As Integer = 84, SM_CXFOCUSBORDER As Integer = 83, SM_CYSIZEFRAME As Integer = SM_CYFRAME, _
         SM_CXSIZEFRAME As Integer = SM_CXFRAME, SPI_GETDRAGFULLWINDOWS As Integer = 38, SPI_GETNONCLIENTMETRICS As Integer = 41, SPI_GETWORKAREA As Integer = 48, SPI_GETHIGHCONTRAST As Integer = 66, SPI_GETDEFAULTINPUTLANG As Integer = 89, _
         SPI_GETSNAPTODEFBUTTON As Integer = 95, SPI_GETWHEELSCROLLLINES As Integer = 104, SBARS_SIZEGRIP As Integer = &H100, SB_SETTEXTA As Integer = (&H400 + 1), SB_SETTEXTW As Integer = (&H400 + 11), SB_GETTEXTA As Integer = (&H400 + 2), _
         SB_GETTEXTW As Integer = (&H400 + 13), SB_GETTEXTLENGTHA As Integer = (&H400 + 3), SB_GETTEXTLENGTHW As Integer = (&H400 + 12), SB_SETPARTS As Integer = (&H400 + 4), SB_SIMPLE As Integer = (&H400 + 9), SB_GETRECT As Integer = (&H400 + 10), _
         SB_SETICON As Integer = (&H400 + 15), SB_SETTIPTEXTA As Integer = (&H400 + 16), SB_SETTIPTEXTW As Integer = (&H400 + 17), SB_GETTIPTEXTA As Integer = (&H400 + 18), SB_GETTIPTEXTW As Integer = (&H400 + 19), SBT_OWNERDRAW As Integer = &H1000, _
         SBT_NOBORDERS As Integer = &H100, SBT_POPOUT As Integer = &H200, SBT_RTLREADING As Integer = &H400, SRCCOPY As Integer = &HCC0020, STATFLAG_DEFAULT As Integer = &H0, STATFLAG_NONAME As Integer = &H1, _
         STATFLAG_NOOPEN As Integer = &H2, STGC_DEFAULT As Integer = &H0, STGC_OVERWRITE As Integer = &H1, STGC_ONLYIFCURRENT As Integer = &H2, STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE As Integer = &H4, STREAM_SEEK_SET As Integer = &H0, _
         STREAM_SEEK_CUR As Integer = &H1, STREAM_SEEK_END As Integer = &H2

        Public Const S_OK As Integer = &H0
        Public Const S_FALSE As Integer = &H1

        Public Const TRANSPARENT As Integer = 1, TME_HOVER As Integer = &H1, TME_LEAVE As Integer = &H2, TPM_LEFTBUTTON As Integer = &H0, TPM_LEFTALIGN As Integer = &H0, TPM_VERTICAL As Integer = &H40, _
         TV_FIRST As Integer = &H1100, TBSTATE_CHECKED As Integer = &H1, TBSTATE_ENABLED As Integer = &H4, TBSTATE_HIDDEN As Integer = &H8, TBSTATE_INDETERMINATE As Integer = &H10, TBSTYLE_BUTTON As Integer = &H0, _
         TBSTYLE_SEP As Integer = &H1, TBSTYLE_CHECK As Integer = &H2, TBSTYLE_DROPDOWN As Integer = &H8, TBSTYLE_TOOLTIPS As Integer = &H100, TBSTYLE_FLAT As Integer = &H800, TBSTYLE_LIST As Integer = &H1000, _
         TBSTYLE_EX_DRAWDDARROWS As Integer = &H1, TB_ENABLEBUTTON As Integer = (&H400 + 1), TB_ISBUTTONCHECKED As Integer = (&H400 + 10), TB_ISBUTTONINDETERMINATE As Integer = (&H400 + 13), TB_ADDBUTTONSA As Integer = (&H400 + 20), TB_ADDBUTTONSW As Integer = (&H400 + 68), _
         TB_INSERTBUTTONA As Integer = (&H400 + 21), TB_INSERTBUTTONW As Integer = (&H400 + 67), TB_DELETEBUTTON As Integer = (&H400 + 22), TB_GETBUTTON As Integer = (&H400 + 23), TB_SAVERESTOREA As Integer = (&H400 + 26), TB_SAVERESTOREW As Integer = (&H400 + 76), _
         TB_ADDSTRINGA As Integer = (&H400 + 28), TB_ADDSTRINGW As Integer = (&H400 + 77), TB_BUTTONSTRUCTSIZE As Integer = (&H400 + 30), TB_SETBUTTONSIZE As Integer = (&H400 + 31), TB_AUTOSIZE As Integer = (&H400 + 33), TB_GETROWS As Integer = (&H400 + 40), _
         TB_GETBUTTONTEXTA As Integer = (&H400 + 45), TB_GETBUTTONTEXTW As Integer = (&H400 + 75), TB_SETIMAGELIST As Integer = (&H400 + 48), TB_GETRECT As Integer = (&H400 + 51), TB_GETBUTTONSIZE As Integer = (&H400 + 58), TB_GETBUTTONINFOW As Integer = (&H400 + 63), _
         TB_SETBUTTONINFOW As Integer = (&H400 + 64), TB_GETBUTTONINFOA As Integer = (&H400 + 65), TB_SETBUTTONINFOA As Integer = (&H400 + 66), TB_MAPACCELERATORA As Integer = (&H400 + 78), TB_SETEXTENDEDSTYLE As Integer = (&H400 + 84), TB_MAPACCELERATORW As Integer = (&H400 + 90), _
         TB_SETTOOLTIPS As Integer = (&H400 + 36), TBIF_IMAGE As Integer = &H1, TBIF_TEXT As Integer = &H2, TBIF_STATE As Integer = &H4, TBIF_STYLE As Integer = &H8, TBIF_COMMAND As Integer = &H20, _
         TBIF_SIZE As Integer = &H40, TBN_GETBUTTONINFOA As Integer = ((0 - 700) - 0), TBN_GETBUTTONINFOW As Integer = ((0 - 700) - 20), TBN_QUERYINSERT As Integer = ((0 - 700) - 6), TBN_DROPDOWN As Integer = ((0 - 700) - 10), TBN_GETDISPINFOA As Integer = ((0 - 700) - 16), _
         TBN_GETDISPINFOW As Integer = ((0 - 700) - 17), TBN_GETINFOTIPA As Integer = ((0 - 700) - 18), TBN_GETINFOTIPW As Integer = ((0 - 700) - 19), TTS_ALWAYSTIP As Integer = &H1, TTS_NOPREFIX As Integer = &H2, TTS_NOANIMATE As Integer = &H10, _
         TTS_NOFADE As Integer = &H20, TTS_BALLOON As Integer = &H40, TTI_WARNING As Integer = 2, TTF_IDISHWND As Integer = &H1, TTF_RTLREADING As Integer = &H4, TTF_TRACK As Integer = &H20, _
         TTF_CENTERTIP As Integer = &H2, TTF_SUBCLASS As Integer = &H10, TTF_TRANSPARENT As Integer = &H100, TTF_ABSOLUTE As Integer = &H80, TTDT_AUTOMATIC As Integer = 0, TTDT_RESHOW As Integer = 1, _
         TTDT_AUTOPOP As Integer = 2, TTDT_INITIAL As Integer = 3, TTM_TRACKACTIVATE As Integer = (&H400 + 17), TTM_TRACKPOSITION As Integer = (&H400 + 18), TTM_ACTIVATE As Integer = (&H400 + 1), TTM_POP As Integer = (&H400 + 28), _
         TTM_ADJUSTRECT As Integer = (&H400 + 31), TTM_SETDELAYTIME As Integer = (&H400 + 3), TTM_SETTITLEA As Integer = (WM_USER + 32), TTM_SETTITLEW As Integer = (WM_USER + 33), TTM_ADDTOOLA As Integer = (&H400 + 4), TTM_ADDTOOLW As Integer = (&H400 + 50), _
         TTM_DELTOOLA As Integer = (&H400 + 5), TTM_DELTOOLW As Integer = (&H400 + 51), TTM_NEWTOOLRECTA As Integer = (&H400 + 6), TTM_NEWTOOLRECTW As Integer = (&H400 + 52), TTM_RELAYEVENT As Integer = (&H400 + 7), TTM_GETTIPBKCOLOR As Integer = (&H400 + 22), _
         TTM_SETTIPBKCOLOR As Integer = (&H400 + 19), TTM_SETTIPTEXTCOLOR As Integer = (&H400 + 20), TTM_GETTIPTEXTCOLOR As Integer = (&H400 + 23), TTM_GETTOOLINFOA As Integer = (&H400 + 8), TTM_GETTOOLINFOW As Integer = (&H400 + 53), TTM_SETTOOLINFOA As Integer = (&H400 + 9), _
         TTM_SETTOOLINFOW As Integer = (&H400 + 54), TTM_HITTESTA As Integer = (&H400 + 10), TTM_HITTESTW As Integer = (&H400 + 55), TTM_GETTEXTA As Integer = (&H400 + 11), TTM_GETTEXTW As Integer = (&H400 + 56), TTM_UPDATE As Integer = (&H400 + 29), _
         TTM_UPDATETIPTEXTA As Integer = (&H400 + 12), TTM_UPDATETIPTEXTW As Integer = (&H400 + 57), TTM_ENUMTOOLSA As Integer = (&H400 + 14), TTM_ENUMTOOLSW As Integer = (&H400 + 58), TTM_GETCURRENTTOOLA As Integer = (&H400 + 15), TTM_GETCURRENTTOOLW As Integer = (&H400 + 59), _
         TTM_WINDOWFROMPOINT As Integer = (&H400 + 16), TTM_GETDELAYTIME As Integer = (&H400 + 21), TTM_SETMAXTIPWIDTH As Integer = (&H400 + 24), TTN_GETDISPINFOA As Integer = ((0 - 520) - 0), TTN_GETDISPINFOW As Integer = ((0 - 520) - 10), TTN_SHOW As Integer = ((0 - 520) - 1), _
         TTN_POP As Integer = ((0 - 520) - 2), TTN_NEEDTEXTA As Integer = ((0 - 520) - 0), TTN_NEEDTEXTW As Integer = ((0 - 520) - 10), TBS_AUTOTICKS As Integer = &H1, TBS_VERT As Integer = &H2, TBS_TOP As Integer = &H4, _
         TBS_BOTTOM As Integer = &H0, TBS_BOTH As Integer = &H8, TBS_NOTICKS As Integer = &H10, TBM_GETPOS As Integer = (&H400), TBM_SETTIC As Integer = (&H400 + 4), TBM_SETPOS As Integer = (&H400 + 5), _
         TBM_SETRANGE As Integer = (&H400 + 6), TBM_SETRANGEMIN As Integer = (&H400 + 7), TBM_SETRANGEMAX As Integer = (&H400 + 8), TBM_SETTICFREQ As Integer = (&H400 + 20), TBM_SETPAGESIZE As Integer = (&H400 + 21), TBM_SETLINESIZE As Integer = (&H400 + 23), _
         TB_LINEUP As Integer = 0, TB_LINEDOWN As Integer = 1, TB_PAGEUP As Integer = 2, TB_PAGEDOWN As Integer = 3, TB_THUMBPOSITION As Integer = 4, TB_THUMBTRACK As Integer = 5, _
         TB_TOP As Integer = 6, TB_BOTTOM As Integer = 7, TB_ENDTRACK As Integer = 8, TVS_HASBUTTONS As Integer = &H1, TVS_HASLINES As Integer = &H2, TVS_LINESATROOT As Integer = &H4, _
         TVS_EDITLABELS As Integer = &H8, TVS_SHOWSELALWAYS As Integer = &H20, TVS_RTLREADING As Integer = &H40, TVS_CHECKBOXES As Integer = &H100, TVS_TRACKSELECT As Integer = &H200, TVS_FULLROWSELECT As Integer = &H1000, _
         TVS_INFOTIP As Integer = &H800, TVS_NOTOOLTIPS As Integer = &H80, TVIF_TEXT As Integer = &H1, TVIF_IMAGE As Integer = &H2, TVIF_PARAM As Integer = &H4, TVIF_STATE As Integer = &H8, _
         TVIF_HANDLE As Integer = &H10, TVIF_SELECTEDIMAGE As Integer = &H20, TVIS_SELECTED As Integer = &H2, TVIS_EXPANDED As Integer = &H20, TVIS_EXPANDEDONCE As Integer = &H40, TVIS_STATEIMAGEMASK As Integer = &HF000, _
         TVI_ROOT As Integer = (CInt(&HFFFF0000)), TVI_FIRST As Integer = (CInt(&HFFFF0001)), TVM_INSERTITEMA As Integer = (&H1100 + 0), TVM_INSERTITEMW As Integer = (&H1100 + 50), TVM_DELETEITEM As Integer = (&H1100 + 1), TVM_EXPAND As Integer = (&H1100 + 2), _
         TVE_COLLAPSE As Integer = &H1, TVE_EXPAND As Integer = &H2, TVM_GETITEMRECT As Integer = (&H1100 + 4), TVM_GETINDENT As Integer = (&H1100 + 6), TVM_SETINDENT As Integer = (&H1100 + 7), TVM_SETIMAGELIST As Integer = (&H1100 + 9), _
         TVM_GETNEXTITEM As Integer = (&H1100 + 10), TVGN_NEXT As Integer = &H1, TVGN_PREVIOUS As Integer = &H2, TVGN_FIRSTVISIBLE As Integer = &H5, TVGN_NEXTVISIBLE As Integer = &H6, TVGN_PREVIOUSVISIBLE As Integer = &H7, _
         TVGN_CARET As Integer = &H9, TVM_SELECTITEM As Integer = (&H1100 + 11), TVM_GETITEMA As Integer = (&H1100 + 12), TVM_GETITEMW As Integer = (&H1100 + 62), TVM_SETITEMA As Integer = (&H1100 + 13), TVM_SETITEMW As Integer = (&H1100 + 63), _
         TVM_EDITLABELA As Integer = (&H1100 + 14), TVM_EDITLABELW As Integer = (&H1100 + 65), TVM_GETEDITCONTROL As Integer = (&H1100 + 15), TVM_GETVISIBLECOUNT As Integer = (&H1100 + 16), TVM_HITTEST As Integer = (&H1100 + 17), TVM_ENSUREVISIBLE As Integer = (&H1100 + 20), _
         TVM_ENDEDITLABELNOW As Integer = (&H1100 + 22), TVM_GETISEARCHSTRINGA As Integer = (&H1100 + 23), TVM_GETISEARCHSTRINGW As Integer = (&H1100 + 64), TVM_SETITEMHEIGHT As Integer = (&H1100 + 27), TVM_GETITEMHEIGHT As Integer = (&H1100 + 28), TVN_SELCHANGINGA As Integer = ((0 - 400) - 1), _
         TVN_SELCHANGINGW As Integer = ((0 - 400) - 50), TVN_GETINFOTIPA As Integer = ((0 - 400) - 13), TVN_GETINFOTIPW As Integer = ((0 - 400) - 14), TVN_SELCHANGEDA As Integer = ((0 - 400) - 2), TVN_SELCHANGEDW As Integer = ((0 - 400) - 51), TVC_UNKNOWN As Integer = &H0, _
         TVC_BYMOUSE As Integer = &H1, TVC_BYKEYBOARD As Integer = &H2, TVN_GETDISPINFOA As Integer = ((0 - 400) - 3), TVN_GETDISPINFOW As Integer = ((0 - 400) - 52), TVN_SETDISPINFOA As Integer = ((0 - 400) - 4), TVN_SETDISPINFOW As Integer = ((0 - 400) - 53), _
         TVN_ITEMEXPANDINGA As Integer = ((0 - 400) - 5), TVN_ITEMEXPANDINGW As Integer = ((0 - 400) - 54), TVN_ITEMEXPANDEDA As Integer = ((0 - 400) - 6), TVN_ITEMEXPANDEDW As Integer = ((0 - 400) - 55), TVN_BEGINDRAGA As Integer = ((0 - 400) - 7), TVN_BEGINDRAGW As Integer = ((0 - 400) - 56), _
         TVN_BEGINRDRAGA As Integer = ((0 - 400) - 8), TVN_BEGINRDRAGW As Integer = ((0 - 400) - 57), TVN_BEGINLABELEDITA As Integer = ((0 - 400) - 10), TVN_BEGINLABELEDITW As Integer = ((0 - 400) - 59), TVN_ENDLABELEDITA As Integer = ((0 - 400) - 11), TVN_ENDLABELEDITW As Integer = ((0 - 400) - 60), _
         TCS_BOTTOM As Integer = &H2, TCS_RIGHT As Integer = &H2, TCS_FLATBUTTONS As Integer = &H8, TCS_HOTTRACK As Integer = &H40, TCS_VERTICAL As Integer = &H80, TCS_TABS As Integer = &H0, _
         TCS_BUTTONS As Integer = &H100, TCS_MULTILINE As Integer = &H200, TCS_RIGHTJUSTIFY As Integer = &H0, TCS_FIXEDWIDTH As Integer = &H400, TCS_RAGGEDRIGHT As Integer = &H800, TCS_OWNERDRAWFIXED As Integer = &H2000, _
         TCS_TOOLTIPS As Integer = &H4000, TCM_SETIMAGELIST As Integer = (&H1300 + 3), TCIF_TEXT As Integer = &H1, TCIF_IMAGE As Integer = &H2, TCM_GETITEMA As Integer = (&H1300 + 5), TCM_GETITEMW As Integer = (&H1300 + 60), _
         TCM_SETITEMA As Integer = (&H1300 + 6), TCM_SETITEMW As Integer = (&H1300 + 61), TCM_INSERTITEMA As Integer = (&H1300 + 7), TCM_INSERTITEMW As Integer = (&H1300 + 62), TCM_DELETEITEM As Integer = (&H1300 + 8), TCM_DELETEALLITEMS As Integer = (&H1300 + 9), _
         TCM_GETITEMRECT As Integer = (&H1300 + 10), TCM_GETCURSEL As Integer = (&H1300 + 11), TCM_SETCURSEL As Integer = (&H1300 + 12), TCM_ADJUSTRECT As Integer = (&H1300 + 40), TCM_SETITEMSIZE As Integer = (&H1300 + 41), TCM_SETPADDING As Integer = (&H1300 + 43), _
         TCM_GETROWCOUNT As Integer = (&H1300 + 44), TCM_GETTOOLTIPS As Integer = (&H1300 + 45), TCM_SETTOOLTIPS As Integer = (&H1300 + 46), TCN_SELCHANGE As Integer = ((0 - 550) - 1), TCN_SELCHANGING As Integer = ((0 - 550) - 2), TBSTYLE_WRAPPABLE As Integer = &H200, _
         TVM_SETBKCOLOR As Integer = (TV_FIRST + 29), TVM_SETTEXTCOLOR As Integer = (TV_FIRST + 30), TYMED_NULL As Integer = 0, TVM_GETLINECOLOR As Integer = (TV_FIRST + 41), TVM_SETLINECOLOR As Integer = (TV_FIRST + 40), TVM_SETTOOLTIPS As Integer = (TV_FIRST + 24), _
         TVSIL_STATE As Integer = 2, TVM_SORTCHILDRENCB As Integer = (TV_FIRST + 21)

        Public Const TVHT_NOWHERE As Integer = &H1, TVHT_ONITEMICON As Integer = &H2, TVHT_ONITEMLABEL As Integer = &H4, TVHT_ONITEM As Integer = (TVHT_ONITEMICON Or TVHT_ONITEMLABEL Or TVHT_ONITEMSTATEICON), TVHT_ONITEMINDENT As Integer = &H8, TVHT_ONITEMBUTTON As Integer = &H10, _
         TVHT_ONITEMRIGHT As Integer = &H20, TVHT_ONITEMSTATEICON As Integer = &H40, TVHT_ABOVE As Integer = &H100, TVHT_BELOW As Integer = &H200, TVHT_TORIGHT As Integer = &H400, TVHT_TOLEFT As Integer = &H800

        Public Const UIS_SET As Integer = 1, UIS_CLEAR As Integer = 2, UIS_INITIALIZE As Integer = 3, UISF_HIDEFOCUS As Integer = &H1, UISF_HIDEACCEL As Integer = &H2, USERCLASSTYPE_FULL As Integer = 1, _
         USERCLASSTYPE_SHORT As Integer = 2, USERCLASSTYPE_APPNAME As Integer = 3, UOI_FLAGS As Integer = 1

        Public Const VIEW_E_DRAW As Integer = CInt(&H80040140), VK_LEFT As Integer = &H25, VK_UP As Integer = &H26, VK_RIGHT As Integer = &H27, VK_DOWN As Integer = &H28, VK_TAB As Integer = &H9, _
         VK_SHIFT As Integer = &H10, VK_CONTROL As Integer = &H11, VK_MENU As Integer = &H12, VK_ESCAPE As Integer = &H1B

        Public Const WAVE_FORMAT_PCM As Integer = &H1, WAVE_FORMAT_ADPCM As Integer = &H2, WAVE_FORMAT_IEEE_FLOAT As Integer = &H3

        Public Const MMIO_READ As Integer = &H0, MMIO_ALLOCBUF As Integer = &H10000, MMIO_FINDRIFF As Integer = &H20

        Public Const WH_JOURNALPLAYBACK As Integer = 1, WH_GETMESSAGE As Integer = 3, WH_MOUSE As Integer = 7, WSF_VISIBLE As Integer = &H1, WM_NULL As Integer = &H0, WM_CREATE As Integer = &H1, _
         WM_DELETEITEM As Integer = &H2D, WM_DESTROY As Integer = &H2, WM_MOVE As Integer = &H3, WM_SIZE As Integer = &H5, WM_ACTIVATE As Integer = &H6, WA_INACTIVE As Integer = 0, _
         WA_ACTIVE As Integer = 1, WA_CLICKACTIVE As Integer = 2, WM_SETFOCUS As Integer = &H7, WM_KILLFOCUS As Integer = &H8, WM_ENABLE As Integer = &HA, WM_SETREDRAW As Integer = &HB, _
         WM_SETTEXT As Integer = &HC, WM_GETTEXT As Integer = &HD, WM_GETTEXTLENGTH As Integer = &HE, WM_PAINT As Integer = &HF, WM_CLOSE As Integer = &H10, WM_QUERYENDSESSION As Integer = &H11, _
         WM_QUIT As Integer = &H12, WM_QUERYOPEN As Integer = &H13, WM_ERASEBKGND As Integer = &H14, WM_SYSCOLORCHANGE As Integer = &H15, WM_ENDSESSION As Integer = &H16, WM_SHOWWINDOW As Integer = &H18, _
         WM_WININICHANGE As Integer = &H1A, WM_SETTINGCHANGE As Integer = &H1A, WM_DEVMODECHANGE As Integer = &H1B, WM_ACTIVATEAPP As Integer = &H1C, WM_FONTCHANGE As Integer = &H1D, WM_TIMECHANGE As Integer = &H1E, _
         WM_CANCELMODE As Integer = &H1F, WM_SETCURSOR As Integer = &H20, WM_MOUSEACTIVATE As Integer = &H21, WM_CHILDACTIVATE As Integer = &H22, WM_QUEUESYNC As Integer = &H23, WM_GETMINMAXINFO As Integer = &H24, _
         WM_PAINTICON As Integer = &H26, WM_ICONERASEBKGND As Integer = &H27, WM_NEXTDLGCTL As Integer = &H28, WM_SPOOLERSTATUS As Integer = &H2A, WM_DRAWITEM As Integer = &H2B, WM_MEASUREITEM As Integer = &H2C, _
         WM_VKEYTOITEM As Integer = &H2E, WM_CHARTOITEM As Integer = &H2F, WM_SETFONT As Integer = &H30, WM_GETFONT As Integer = &H31, WM_SETHOTKEY As Integer = &H32, WM_GETHOTKEY As Integer = &H33, _
         WM_QUERYDRAGICON As Integer = &H37, WM_COMPAREITEM As Integer = &H39, WM_GETOBJECT As Integer = &H3D, WM_COMPACTING As Integer = &H41, WM_COMMNOTIFY As Integer = &H44, WM_WINDOWPOSCHANGING As Integer = &H46, _
         WM_WINDOWPOSCHANGED As Integer = &H47, WM_POWER As Integer = &H48, WM_COPYDATA As Integer = &H4A, WM_CANCELJOURNAL As Integer = &H4B, WM_NOTIFY As Integer = &H4E, WM_INPUTLANGCHANGEREQUEST As Integer = &H50, _
         WM_INPUTLANGCHANGE As Integer = &H51, WM_TCARD As Integer = &H52, WM_HELP As Integer = &H53, WM_USERCHANGED As Integer = &H54, WM_NOTIFYFORMAT As Integer = &H55, WM_CONTEXTMENU As Integer = &H7B, _
         WM_STYLECHANGING As Integer = &H7C, WM_STYLECHANGED As Integer = &H7D, WM_DISPLAYCHANGE As Integer = &H7E, WM_GETICON As Integer = &H7F, WM_SETICON As Integer = &H80, WM_NCCREATE As Integer = &H81, _
         WM_NCDESTROY As Integer = &H82, WM_NCCALCSIZE As Integer = &H83, WM_NCHITTEST As Integer = &H84, WM_NCPAINT As Integer = &H85, WM_NCACTIVATE As Integer = &H86, WM_GETDLGCODE As Integer = &H87, _
         WM_NCMOUSEMOVE As Integer = &HA0, WM_NCLBUTTONDOWN As Integer = &HA1, WM_NCLBUTTONUP As Integer = &HA2, WM_NCLBUTTONDBLCLK As Integer = &HA3, WM_NCRBUTTONDOWN As Integer = &HA4, WM_NCRBUTTONUP As Integer = &HA5, _
         WM_NCRBUTTONDBLCLK As Integer = &HA6, WM_NCMBUTTONDOWN As Integer = &HA7, WM_NCMBUTTONUP As Integer = &HA8, WM_NCMBUTTONDBLCLK As Integer = &HA9, WM_NCXBUTTONDOWN As Integer = &HAB, WM_NCXBUTTONUP As Integer = &HAC, _
         WM_NCXBUTTONDBLCLK As Integer = &HAD, WM_KEYFIRST As Integer = &H100, WM_KEYDOWN As Integer = &H100, WM_KEYUP As Integer = &H101, WM_CHAR As Integer = &H102, WM_DEADCHAR As Integer = &H103, _
         WM_CTLCOLOR As Integer = &H19, WM_SYSKEYDOWN As Integer = &H104, WM_SYSKEYUP As Integer = &H105, WM_SYSCHAR As Integer = &H106, WM_SYSDEADCHAR As Integer = &H107, WM_KEYLAST As Integer = &H108, _
         WM_IME_STARTCOMPOSITION As Integer = &H10D, WM_IME_ENDCOMPOSITION As Integer = &H10E, WM_IME_COMPOSITION As Integer = &H10F, WM_IME_KEYLAST As Integer = &H10F, WM_INITDIALOG As Integer = &H110, WM_COMMAND As Integer = &H111, _
         WM_SYSCOMMAND As Integer = &H112, WM_TIMER As Integer = &H113, WM_HSCROLL As Integer = &H114, WM_VSCROLL As Integer = &H115, WM_INITMENU As Integer = &H116, WM_INITMENUPOPUP As Integer = &H117, _
         WM_MENUSELECT As Integer = &H11F, WM_MENUCHAR As Integer = &H120, WM_ENTERIDLE As Integer = &H121, WM_UNINITMENUPOPUP As Integer = &H125, WM_CHANGEUISTATE As Integer = &H127, WM_UPDATEUISTATE As Integer = &H128, _
         WM_QUERYUISTATE As Integer = &H129, WM_CTLCOLORMSGBOX As Integer = &H132, WM_CTLCOLOREDIT As Integer = &H133, WM_CTLCOLORLISTBOX As Integer = &H134, WM_CTLCOLORBTN As Integer = &H135, WM_CTLCOLORDLG As Integer = &H136, _
         WM_CTLCOLORSCROLLBAR As Integer = &H137, WM_CTLCOLORSTATIC As Integer = &H138, WM_MOUSEFIRST As Integer = &H200, WM_MOUSEMOVE As Integer = &H200, WM_LBUTTONDOWN As Integer = &H201, WM_LBUTTONUP As Integer = &H202, _
         WM_LBUTTONDBLCLK As Integer = &H203, WM_RBUTTONDOWN As Integer = &H204, WM_RBUTTONUP As Integer = &H205, WM_RBUTTONDBLCLK As Integer = &H206, WM_MBUTTONDOWN As Integer = &H207, WM_MBUTTONUP As Integer = &H208, _
         WM_MBUTTONDBLCLK As Integer = &H209, WM_XBUTTONDOWN As Integer = &H20B, WM_XBUTTONUP As Integer = &H20C, WM_XBUTTONDBLCLK As Integer = &H20D, WM_MOUSEWHEEL As Integer = &H20A, WM_MOUSELAST As Integer = &H20A

        Public Const WHEEL_DELTA As Integer = 120, WM_PARENTNOTIFY As Integer = &H210, WM_ENTERMENULOOP As Integer = &H211, WM_EXITMENULOOP As Integer = &H212, WM_NEXTMENU As Integer = &H213, WM_SIZING As Integer = &H214, _
         WM_CAPTURECHANGED As Integer = &H215, WM_MOVING As Integer = &H216, WM_POWERBROADCAST As Integer = &H218, WM_DEVICECHANGE As Integer = &H219, WM_IME_SETCONTEXT As Integer = &H281, WM_IME_NOTIFY As Integer = &H282, _
         WM_IME_CONTROL As Integer = &H283, WM_IME_COMPOSITIONFULL As Integer = &H284, WM_IME_SELECT As Integer = &H285, WM_IME_CHAR As Integer = &H286, WM_IME_KEYDOWN As Integer = &H290, WM_IME_KEYUP As Integer = &H291, _
         WM_MDICREATE As Integer = &H220, WM_MDIDESTROY As Integer = &H221, WM_MDIACTIVATE As Integer = &H222, WM_MDIRESTORE As Integer = &H223, WM_MDINEXT As Integer = &H224, WM_MDIMAXIMIZE As Integer = &H225, _
         WM_MDITILE As Integer = &H226, WM_MDICASCADE As Integer = &H227, WM_MDIICONARRANGE As Integer = &H228, WM_MDIGETACTIVE As Integer = &H229, WM_MDISETMENU As Integer = &H230, WM_ENTERSIZEMOVE As Integer = &H231, _
         WM_EXITSIZEMOVE As Integer = &H232, WM_DROPFILES As Integer = &H233, WM_MDIREFRESHMENU As Integer = &H234, WM_MOUSEHOVER As Integer = &H2A1, WM_MOUSELEAVE As Integer = &H2A3, WM_CUT As Integer = &H300, _
         WM_COPY As Integer = &H301, WM_PASTE As Integer = &H302, WM_CLEAR As Integer = &H303, WM_UNDO As Integer = &H304, WM_RENDERFORMAT As Integer = &H305, WM_RENDERALLFORMATS As Integer = &H306, _
         WM_DESTROYCLIPBOARD As Integer = &H307, WM_DRAWCLIPBOARD As Integer = &H308, WM_PAINTCLIPBOARD As Integer = &H309, WM_VSCROLLCLIPBOARD As Integer = &H30A, WM_SIZECLIPBOARD As Integer = &H30B, WM_ASKCBFORMATNAME As Integer = &H30C, _
         WM_CHANGECBCHAIN As Integer = &H30D, WM_HSCROLLCLIPBOARD As Integer = &H30E, WM_QUERYNEWPALETTE As Integer = &H30F, WM_PALETTEISCHANGING As Integer = &H310, WM_PALETTECHANGED As Integer = &H311, WM_HOTKEY As Integer = &H312, _
         WM_PRINT As Integer = &H317, WM_PRINTCLIENT As Integer = &H318, WM_HANDHELDFIRST As Integer = &H358, WM_HANDHELDLAST As Integer = &H35F, WM_AFXFIRST As Integer = &H360, WM_AFXLAST As Integer = &H37F, _
         WM_PENWINFIRST As Integer = &H380, WM_PENWINLAST As Integer = &H38F, WM_APP As Integer = CInt(&H8000), WM_USER As Integer = &H400, WM_REFLECT As Integer = WM_USER + &H1C00, WS_OVERLAPPED As Integer = &H0, _
         WS_POPUP As Integer = CInt(&H80000000), WS_CHILD As Integer = &H40000000, WS_MINIMIZE As Integer = &H20000000, WS_VISIBLE As Integer = &H10000000, WS_DISABLED As Integer = &H8000000, WS_CLIPSIBLINGS As Integer = &H4000000, _
         WS_CLIPCHILDREN As Integer = &H2000000, WS_MAXIMIZE As Integer = &H1000000, WS_CAPTION As Integer = &HC00000, WS_BORDER As Integer = &H800000, WS_DLGFRAME As Integer = &H400000, WS_VSCROLL As Integer = &H200000, _
         WS_HSCROLL As Integer = &H100000, WS_SYSMENU As Integer = &H80000, WS_THICKFRAME As Integer = &H40000, WS_TABSTOP As Integer = &H10000, WS_MINIMIZEBOX As Integer = &H20000, WS_MAXIMIZEBOX As Integer = &H10000, _
         WS_EX_DLGMODALFRAME As Integer = &H1, WS_EX_MDICHILD As Integer = &H40, WS_EX_TOOLWINDOW As Integer = &H80, WS_EX_CLIENTEDGE As Integer = &H200, WS_EX_CONTEXTHELP As Integer = &H400, WS_EX_RIGHT As Integer = &H1000, _
         WS_EX_LEFT As Integer = &H0, WS_EX_RTLREADING As Integer = &H2000, WS_EX_LEFTSCROLLBAR As Integer = &H4000, WS_EX_CONTROLPARENT As Integer = &H10000, WS_EX_STATICEDGE As Integer = &H20000, WS_EX_APPWINDOW As Integer = &H40000, _
         WS_EX_LAYERED As Integer = &H80000, WS_EX_TOPMOST As Integer = &H8, WS_EX_LAYOUTRTL As Integer = &H400000, WS_EX_NOINHERITLAYOUT As Integer = &H100000, WPF_SETMINPOSITION As Integer = &H1, WM_CHOOSEFONT_GETLOGFONT As Integer = (&H400 + 1)
    End Class
End Namespace