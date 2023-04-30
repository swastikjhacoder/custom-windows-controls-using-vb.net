#Region "Option..."
Option Explicit On
#End Region

#Region "Import..."
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.Drawing.Point
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop.Word
Imports System.ComponentModel
Imports System.Collections.Specialized
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace UIControls.TextBox

    Public Class eRicherTextBox
        Inherits System.Windows.Forms.RichTextBox

#Region "Variable..."

        Const MaximumTextLength As Integer = 10000
        Const TextLengthToBeRemoved As Integer = 3000
        Const MaximumNoOfControl As Integer = 50
        Const NoOfControlToBeRemoved As Integer = 20
        Private KeepShort As Boolean = True
        Const EM_GETSCROLLPOS As Integer = &H400 + 221
        Private ControlList As New List(Of MetaInfo)()
        Private emptyItem As Object = System.Reflection.Missing.Value
        Private oNothing As Object = Nothing
        Private oTrue As Object = True
        Private oFalse As Object = False
        Private oAlwaysSuggest As Object = True
        Private oIgnoreUpperCase As Object = False
        Private mAlwaysSuggest As Boolean = True
        Private mIgnoreUpperCase As Boolean = False
        Private Const AnInch As Double = 14.4
        Private Const WM_USER As Integer = &H400
        Private Const EM_FORMATRANGE As Integer = WM_USER + 57
        Const MAX_TAB_STOPS = 32&
        Const EM_LINEFROMCHAR = &HC9
        Const EM_LINEINDEX = &HBB
        Const EM_SETPARAFORMAT = &H447
        Const PFM_LINESPACING = &H100
        Private Const EM_GETPARAFORMAT = 1085
        Private Const PFM_NUMBERING As Int32 = &H20
        Private Const BULLET_NUMBER2 = 2
        Private Const BULLET_NUMBER3 = 3
        Private Const BULLET_NUMBER4 = 4
        Private Const WM_USERS As Integer = 1024
        Private Const EM_GETCHARFORMAT As Integer = (WM_USER + 58)
        Private Const EM_SETCHARFORMAT As Integer = (WM_USER + 68)
        Private Const SCF_SELECTION As Integer = 1
        Private Const SCF_WORD As Integer = 2
        Private Const SCF_ALL As Integer = 4
        Private Const FF_UNKNOWN As String = "UNKNOWN"
        Public printMarginTop As Integer
        Public printMarginLeft As Integer
        Public printMarginRight As Integer
        Public printMarginBottom As Integer
        Public printLandScape As Boolean

#End Region

#Region "My Constants"

        ' Not used in this application.  Descriptions can be found with documentation
        ' of Windows GDI function SetMapMode
        Private Const MM_TEXT As Integer = 1
        Private Const MM_LOMETRIC As Integer = 2
        Private Const MM_HIMETRIC As Integer = 3
        Private Const MM_LOENGLISH As Integer = 4
        Private Const MM_HIENGLISH As Integer = 5
        Private Const MM_TWIPS As Integer = 6

        ' Ensures that the metafile maintains a 1:1 aspect ratio
        Private Const MM_ISOTROPIC As Integer = 7

        ' Allows the x-coordinates and y-coordinates of the metafile to be adjusted
        ' independently
        Private Const MM_ANISOTROPIC As Integer = 8

        ' The number of hundredths of millimeters (0.01 mm) in an inch
        ' For more information, see GetImagePrefix() method.
        Private Const HMM_PER_INCH As Integer = 2540

        ' The number of twips in an inch
        ' For more information, see GetImagePrefix() method.
        Private Const TWIPS_PER_INCH As Integer = 1440

#End Region

#Region "Public Enums"

        ' Enum for possible RTF colors
        Public Enum RtfColor
            Black
            Maroon
            Green
            Olive
            Navy
            Purple
            Teal
            Gray
            Silver
            Red
            Lime
            Yellow
            Blue
            Fuchsia
            Aqua
            White
        End Enum

        ' Specifies the flags/options for the unmanaged call to the GDI+ method
        ' Metafile.EmfToWmfBits().
        Private Enum EmfToWmfBitsFlags

            ' Use the default conversion
            EmfToWmfBitsFlagsDefault = &H0

            ' Embedded the source of the EMF metafiel within the resulting WMF
            ' metafile
            EmfToWmfBitsFlagsEmbedEmf = &H1

            ' Place a 22-byte header in the resulting WMF file.  The header is
            ' required for the metafile to be considered placeable.
            EmfToWmfBitsFlagsIncludePlaceable = &H2

            ' Don't simulate clipping by using the XOR operator.
            EmfToWmfBitsFlagsNoXORClip = &H4
        End Enum

#End Region

#Region "Sub New..."

        Public Sub New()
            MyBase.New()
            InitializeComponent()
            AddHandler Me.VScroll, AddressOf TRichTextBox_VScroll
            AddHandler Me.SizeChanged, AddressOf TRichTextBox_SizeChanged
            AddHandler Me.LinkClicked, AddressOf TRichTextBox_LinkClicked
        End Sub

#End Region

#Region "Friend Class MetaInfo..."

        Friend Class MetaInfo
            Private m_charIndex As Integer
            Public Property CharIndex() As Integer
                Get
                    Return m_charIndex
                End Get
                Set(ByVal value As Integer)
                    m_charIndex = value
                End Set
            End Property
            Private m_deltaY As Integer
            Public Property DeltaY() As Integer
                Get
                    Return m_deltaY
                End Get
                Set(ByVal value As Integer)
                    m_deltaY = value
                End Set
            End Property
            Private m_theControl As Control
            Public Property TheControl() As Control
                Get
                    Return m_theControl
                End Get
                Set(ByVal value As Control)
                    m_theControl = value
                End Set
            End Property
            Public Sub New(ByVal theControl As Control)
                Me.m_theControl = theControl
            End Sub

        End Class

#End Region

#Region "My Privates"

        ' The default text color
        Private textColor As RtfColor
        ' The default text background color
        Private highlightColor As RtfColor
        ' Dictionary that maps color enums to RTF color codes
        Private rtfClr As HybridDictionary
        ' Dictionary that mapas Framework font families to RTF font families
        Private rtfFontFamily As HybridDictionary
        ' The horizontal resolution at which the control is being displayed
        Private xDpi As Single
        ' The vertical resolution at which the control is being displayed
        Private yDpi As Single

#End Region

#Region "Properties..."

        Public Property AlwaysSuggest() As Boolean
            Get
                Return mAlwaysSuggest
            End Get
            Set(ByVal value As Boolean)
                mAlwaysSuggest = value

                If mAlwaysSuggest = True Then
                    oAlwaysSuggest = True
                Else
                    oAlwaysSuggest = False
                End If
            End Set
        End Property

        Public Property IgnoreUpperCase() As Boolean
            Get
                Return mIgnoreUpperCase
            End Get
            Set(ByVal value As Boolean)
                mIgnoreUpperCase = value

                If mIgnoreUpperCase = True Then
                    oIgnoreUpperCase = True
                Else
                    oIgnoreUpperCase = False
                End If
            End Set
        End Property

#End Region

#Region "Structure..."

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure CHARFORMAT2_STRUCT
            Public cbSize As UInt32
            Public dwMask As UInt32
            Public dwEffects As UInt32
            Public yHeight As Int32
            Public yOffset As Int32
            Public crTextColor As Int32
            Public bCharSet As Byte
            Public bPitchAndFamily As Byte
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
            Public szFaceName() As Char
            Public wWeight As UInt16
            Public sSpacing As UInt16
            Public crBackColor As Integer
            ' Color.ToArgb() -> int
            Public lcid As Integer
            Public dwReserved As Integer
            Public sStyle As Int16
            Public wKerning As Int16
            Public bUnderlineType As Byte
            Public bAnimation As Byte
            Public bRevAuthor As Byte
            Public bReserved1 As Byte
        End Structure

        Public Structure PARAFORMAT2
            Dim cbSize As UInteger
            Dim dwMask As UInteger
            Dim wNumbering As UInt16
            Dim wEffects As UInt16
            Dim dxStartIndent As Integer
            Dim dxRightIndent As Integer
            Dim dxOffset As Integer
            Dim wAlignment As UInt16
            Dim cTabCount As Int16
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
            Public rgxTabs() As Integer
            Dim dySpaceBefore As Integer
            Dim dySpaceAfter As Integer
            Dim dyLineSpacing As Integer
            Dim sStyle As Int16
            Dim bLineSpacingRule As Byte
            Dim bOutlineLevel As Byte
            Dim wShadingWeight As UInt16
            Dim wShadingStyle As UInt16
            Dim wNumberingStart As UInt16
            Dim wNumberingStyle As UInt16
            Dim wNumberingTab As UInt16
            Dim wBorderSpace As UInt16
            Dim wBorderWidth As UInt16
            Dim wBorders As UInt16
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
         Private Structure RECT
            Public Left As Integer
            Public Top As Integer
            Public Right As Integer
            Public Bottom As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure CHARRANGE
            Public cpMin As Integer          ' First character of range (0 for start of doc)
            Public cpMax As Integer          ' Last character of range (-1 for end of doc)
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure FORMATRANGE
            Public hdc As IntPtr             ' Actual DC to draw on
            Public hdcTarget As IntPtr       ' Target DC for determining text formatting
            Public rc As RECT                ' Region of the DC to draw to (in twips)
            Public rcPage As RECT            ' Region of the whole DC (page size) (in twips)
            Public chrg As CHARRANGE         ' Range of text to draw (see above declaration)
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure CHARFORMATSTRUCT
            Public cbSize As Integer
            Public dwMask As UInt32
            Public dwEffects As UInt32
            Public yHeight As Int32
            Public yOffset As Int32
            Public crTextColor As Int32
            Public bCharSet As Byte
            Public bPitchAndFamily As Byte
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
            Public szFaceName As Char()
        End Structure

        Public Structure PARAFORMATPARA
            Dim cbSize As Integer
            Dim wPad1 As Integer
            Dim dwMask As Long
            Dim wNumbering As Integer
            Dim wReserved As Integer
            Dim dxStartIndent As Long
            Dim dxRightIndent As Long
            Dim dxOffset As Long
            Dim wAlignment As Integer
            Dim cTabCount As Integer
            Dim lTabStops() As Long
            Dim dySpaceBefore As Long          ' Vertical spacing before para
            Dim dySpaceAfter As Long           ' Vertical spacing after para
            Dim dyLineSpacing As Long          ' Line spacing depending on Rule
            Dim sStyle As Integer              ' Style handle
            Dim bLineSpacingRule As Byte       ' Rule for line spacing
            Dim bCRC As Byte                   ' Reserved for CRC for rapid searching
            Dim wShadingWeight As Integer      ' Shading in hundredths of a per cent
            Dim wShadingStyle As Integer       ' Nibble 0: style, 1: cfpat, 2: cbpat
            Dim wNumberingStart As Integer     ' Starting value for numbering
            Dim wNumberingStyle As Integer     ' Alignment, roman/arabic, (), ), .,     etc.
            Dim wNumberingTab As Integer       ' Space between 1st indent and 1st-line text
            Dim wBorderSpace As Integer        ' Space between border and text(twips)
            Dim wBorderWidth As Integer        ' Border pen width (twips)
            Dim wBorders As Integer            ' Byte 0: bits specify which borders; Nibble 2: border style; 3: color                                     index*/
        End Structure

#End Region

#Region "Elements required to create an RTF document"

        Private Const RTF_HEADER As String = "{\rtf1\ansi\ansicpg1252\deff0\deflang1033"
        Private Const RTF_DOCUMENT_PRE As String = "\viewkind4\uc1\pard\cf1\f0\fs20"
        Private Const RTF_DOCUMENT_POST As String = "\cf0\fs17}"
        Private RTF_IMAGE_POST As String = "}"

#End Region

#Region "RTF Helpers"

        ''' <summary>
        ''' Creates a font table from a font object.  When an Insert or Append 
        ''' operation is performed a font is either specified or the default font
        ''' is used.  In any case, on any Insert or Append, only one font is used,
        ''' thus the font table will always contain a single font.  The font table
        ''' should have the form ...
        ''' 
        ''' {\fonttbl{\f0\[FAMILY]\fcharset0 [FONT_NAME];}
        ''' </summary>
        ''' <param name="_font"></param>
        ''' <returns></returns>
        Private Function GetFontTable(ByVal _font As System.Drawing.Font) As String

            Dim _fontTable As New StringBuilder()

            ' Append table control string
            _fontTable.Append("{\fonttbl{\f0")
            _fontTable.Append("\")

            ' If the font's family corresponds to an RTF family, append the
            ' RTF family name, else, append the RTF for unknown font family.
            'If rtfFontFamily.Contains(_font.FontFamily.Name) Then
            '    _fontTable.Append(rtfFontFamily(_font.FontFamily.Name))
            'Else
            '    _fontTable.Append(rtfFontFamily(FF_UNKNOWN))
            'End If

            ' \fcharset specifies the character set of a font in the font table.
            ' 0 is for ANSI.
            _fontTable.Append("\fcharset0 ")

            ' Append the name of the font
            _fontTable.Append(_font.Name)

            ' Close control string
            _fontTable.Append(";}}")

            Return _fontTable.ToString()
        End Function

        ''' <summary>
        ''' Creates a font table from the RtfColor structure.  When an Insert or Append
        ''' operation is performed, _textColor and _backColor are either specified
        ''' or the default is used.  In any case, on any Insert or Append, only three
        ''' colors are used.  The default color of the RichTextBox (signified by a
        ''' semicolon (;) without a definition), is always the first color (index 0) in
        ''' the color table.  The second color is always the text color, and the third
        ''' is always the highlight color (color behind the text).  The color table
        ''' should have the form ...
        ''' 
        ''' {\colortbl ;[TEXT_COLOR];[HIGHLIGHT_COLOR];}
        ''' 
        ''' </summary>
        ''' <param name="_textColor"></param>
        ''' <param name="_backColor"></param>
        ''' <returns></returns>
        Private Function GetColorTable(ByVal _textColor As RtfColor, ByVal _backColor As RtfColor) As String

            Dim _colorTable As New StringBuilder()

            ' Append color table control string and default font (;)
            _colorTable.Append("{\colortbl ;")

            ' Append the text color
            '_colorTable.Append(rtfClr(_textColor))
            '_colorTable.Append(";")

            ' Append the highlight color
            '_colorTable.Append(rtfClr(_backColor))
            _colorTable.Append(";}\n")

            Return _colorTable.ToString()
        End Function

        ''' <summary>
        ''' Called by overrided RichTextBox.Rtf accessor.
        ''' Removes the null character from the RTF.  This is residue from developing
        ''' the control for a specific instant messaging protocol and can be ommitted.
        ''' </summary>
        ''' <param name="_originalRtf"></param>
        ''' <returns>RTF without null character</returns>
        Private Function RemoveBadChars(ByVal _originalRtf As String) As String
            Return _originalRtf.Replace(vbNullChar, "")
        End Function

#End Region

#Region "Functions..."

#Region "Private Share Function..."
        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Private Overloads Shared Function SendMessage(ByVal hWnd As HandleRef, ByVal msg As Integer, ByVal wParam As Integer, ByRef lParam As PARAFORMAT2) As Integer
        End Function
        <DllImportAttribute("user32.dll")> _
        Private Shared Function GetKeyState(ByVal keystate As Integer) As UInteger
        End Function
        <DllImport("user32.dll")> _
        Private Shared Function SendMessages(ByVal hwnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByRef pt As POINT) As Integer
        End Function

        Public Function GetKeyStateInsert() As UInteger
            Return GetKeyState(45)
            ' Keynumber InsertKey
        End Function
#End Region

#Region "Private Declare Function..."
        Private Declare Function SendMessages Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        Private Overloads Declare Function SendMessage Lib "USER32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr

#End Region

#Region "Public Function..."
        Public Function HundredthInchToTwips(ByVal n As Integer) As Int32
            'Return DirectCast(n * 14, Int32)
            Return CInt(n * 14.4)
        End Function
        ''' <summary>
        ''' Sets the font only for the selected part of the rich text box
        ''' without modifying the other properties like size or style
        ''' </summary>
        ''' <param name="face">Name of the font to use</param>
        ''' <returns>true on success, false on failure</returns>
        Public Function SetSelectionFont(ByVal face As String) As Boolean
            Dim cf As New CHARFORMATSTRUCT()
            cf.cbSize = Marshal.SizeOf(cf)
            cf.szFaceName = New Char(31) {}
            cf.dwMask = CFM_FACE
            face.CopyTo(0, cf.szFaceName, 0, Math.Min(31, face.Length))

            Dim lParam As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
            Marshal.StructureToPtr(cf, lParam, False)

            Dim res As Integer = SendMessage(Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
            Return (res = 0)
        End Function
        ''' <summary>
        ''' Sets the font size only for the selected part of the rich text box
        ''' without modifying the other properties like font or style
        ''' </summary>
        ''' <param name="size">new point size to use</param>
        ''' <returns>true on success, false on failure</returns>
        Public Function SetSelectionSize(ByVal size As Integer) As Boolean
            Dim cf As New CHARFORMATSTRUCT()
            cf.cbSize = Marshal.SizeOf(cf)
            cf.dwMask = CFM_SIZE
            ' yHeight is in 1/20 pt
            cf.yHeight = size * 20

            Dim lParam As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
            Marshal.StructureToPtr(cf, lParam, False)

            Dim res As Integer = SendMessage(Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
            Return (res = 0)
        End Function
        Public Sub InsertTextAsRtf(ByVal _text As String)
            InsertTextAsRtf(_text, Me.Font)
        End Sub
        Public Sub InsertTextAsRtf(ByVal _text As String, ByVal _font As System.Drawing.Font)
            InsertTextAsRtf(_text, _font, textColor)
        End Sub
        Public Sub InsertTextAsRtf(ByVal _text As String, ByVal _font As System.Drawing.Font, ByVal _textColor As RtfColor)
            InsertTextAsRtf(_text, _font, _textColor, highlightColor)
        End Sub
        Public Sub InsertTextAsRtf(ByVal _text As String, ByVal _font As System.Drawing.Font, ByVal _textColor As RtfColor, ByVal _backColor As RtfColor)

            Dim _rtf As New StringBuilder()

            ' Append the RTF header
            _rtf.Append(RTF_HEADER)

            ' Create the font table from the font passed in and append it to the
            ' RTF string
            _rtf.Append(GetFontTable(_font))

            ' Create the color table from the colors passed in and append it to the
            ' RTF string
            _rtf.Append(GetColorTable(_textColor, _backColor))

            ' Create the document area from the text to be added as RTF and append
            ' it to the RTF string.
            _rtf.Append(GetDocumentArea(_text, _font))

            Me.SelectedRtf = _rtf.ToString()
        End Sub
        Private Function GetDocumentArea(ByVal _text As String, ByVal _font As System.Drawing.Font) As String

            Dim _doc As New StringBuilder()

            ' Append the standard RTF document area control string
            _doc.Append(RTF_DOCUMENT_PRE)

            ' Set the highlight color (the color behind the text) to the
            ' third color in the color table.  See GetColorTable for more details.
            _doc.Append("\highlight2")

            ' If the font is bold, attach corresponding tag
            If _font.Bold Then
                _doc.Append("\b")
            End If

            ' If the font is italic, attach corresponding tag
            If _font.Italic Then
                _doc.Append("\i")
            End If

            ' If the font is strikeout, attach corresponding tag
            If _font.Strikeout Then
                _doc.Append("\strike")
            End If

            ' If the font is underlined, attach corresponding tag
            If _font.Underline Then
                _doc.Append("\ul")
            End If

            ' Set the font to the first font in the font table.
            ' See GetFontTable for more details.
            _doc.Append("\f0")

            ' Set the size of the font.  In RTF, font size is measured in
            ' half-points, so the font size is twice the value obtained from
            ' Font.SizeInPoints
            _doc.Append("\fs")
            _doc.Append(CInt(Math.Round((2 * _font.SizeInPoints))))

            ' Apppend a space before starting actual text (for clarity)
            _doc.Append(" ")

            ' Append actual text, however, replace newlines with RTF \par.
            ' Any other special text should be handled here (e.g.) tabs, etc.
            _doc.Append(_text.Replace(vbLf, "\par "))

            ' RTF isn't strict when it comes to closing control words, but what the
            ' heck ...

            ' Remove the highlight
            _doc.Append("\highlight0")

            ' If font is bold, close tag
            If _font.Bold Then
                _doc.Append("\b0")
            End If

            ' If font is italic, close tag
            If _font.Italic Then
                _doc.Append("\i0")
            End If

            ' If font is strikeout, close tag
            If _font.Strikeout Then
                _doc.Append("\strike0")
            End If

            ' If font is underlined, cloes tag
            If _font.Underline Then
                _doc.Append("\ulnone")
            End If

            ' Revert back to default font and size
            _doc.Append("\f0")
            _doc.Append("\fs20")

            ' Close the document area control string
            _doc.Append(RTF_DOCUMENT_POST)

            Return _doc.ToString()
        End Function
        Public Function GetSelectionLink() As Integer
            Return GetSelectionStyle(CFM_LINK, CFE_LINK)
        End Function

        Private Function GetSelectionStyle(ByVal mask As UInt32, ByVal effect As UInt32) As Integer
            Dim cf As CHARFORMAT2_STRUCT = New CHARFORMAT2_STRUCT
            cf.cbSize = CType(Marshal.SizeOf(cf), UInt32)
            cf.szFaceName = New Char((32) - 1) {}
            Dim wpar As IntPtr = New IntPtr(SCF_SELECTION)
            Dim lpar As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
            Marshal.StructureToPtr(cf, lpar, False)
            Dim res As IntPtr = SendMessage(Handle, EM_GETCHARFORMAT, wpar, lpar)
            cf = CType(Marshal.PtrToStructure(lpar, GetType(CHARFORMAT2_STRUCT)), CHARFORMAT2_STRUCT)
            Dim state As Integer
            ' dwMask holds the information which properties are consistent throughout the selection:
            If ((cf.dwMask And mask) _
                        = mask) Then
                If ((cf.dwEffects And effect) _
                            = effect) Then
                    state = 1
                Else
                    state = 0
                End If
            Else
                state = -1
            End If
            Marshal.FreeCoTaskMem(lpar)
            Return state
        End Function

        Public Function Print(ByVal charFrom As Integer, ByVal charTo As Integer, ByVal e As PrintPageEventArgs) As Integer

            ' Mark starting and ending character 
            Dim cRange As CHARRANGE
            cRange.cpMin = charFrom
            cRange.cpMax = charTo

            ' Calculate the area to render and print
            Dim rectToPrint As RECT
            rectToPrint.Top = e.MarginBounds.Top * AnInch
            rectToPrint.Bottom = e.MarginBounds.Bottom * AnInch
            rectToPrint.Left = e.MarginBounds.Left * AnInch
            rectToPrint.Right = e.MarginBounds.Right * AnInch

            ' Calculate the size of the page
            Dim rectPage As RECT
            rectPage.Top = e.PageBounds.Top * AnInch
            rectPage.Bottom = e.PageBounds.Bottom * AnInch
            rectPage.Left = e.PageBounds.Left * AnInch
            rectPage.Right = e.PageBounds.Right * AnInch

            Dim hdc As IntPtr = e.Graphics.GetHdc()

            Dim fmtRange As FORMATRANGE
            fmtRange.chrg = cRange                 ' Indicate character from to character to 
            fmtRange.hdc = hdc                     ' Use the same DC for measuring and rendering
            fmtRange.hdcTarget = hdc               ' Point at printer hDC
            fmtRange.rc = rectToPrint              ' Indicate the area on page to print
            fmtRange.rcPage = rectPage             ' Indicate whole size of page

            Dim res As IntPtr = IntPtr.Zero

            Dim wparam As IntPtr = IntPtr.Zero
            wparam = New IntPtr(1)

            ' Move the pointer to the FORMATRANGE structure in memory
            Dim lparam As IntPtr = IntPtr.Zero
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange))
            Marshal.StructureToPtr(fmtRange, lparam, False)

            ' Send the rendered data for printing 
            res = SendMessage(Handle, EM_FORMATRANGE, wparam, lparam)

            ' Free the block of memory allocated
            Marshal.FreeCoTaskMem(lparam)

            ' Release the device context handle obtained by a previous call
            e.Graphics.ReleaseHdc(hdc)

            ' Return last + 1 character printer
            Return res.ToInt32()
        End Function
#End Region

#End Region

#Region "CHARFORMAT2 Flags..."
        Private Const CFE_BOLD As UInt32 = 1
        Private Const CFE_ITALIC As UInt32 = 2
        Private Const CFE_UNDERLINE As UInt32 = 4
        Private Const CFE_STRIKEOUT As UInt32 = 8
        Private Const CFE_PROTECTED As UInt32 = 16
        Private Const CFE_LINK As UInt32 = 32
        Private Const CFE_AUTOCOLOR As UInt32 = 1073741824
        Private Const CFE_SUBSCRIPT As UInt32 = 65536
        Private Const CFE_SUPERSCRIPT As UInt32 = 131072
        Private Const CFM_SMALLCAPS As Integer = 64
        Private Const CFM_ALLCAPS As Integer = 128
        Private Const CFM_HIDDEN As Integer = 256
        Private Const CFM_OUTLINE As Integer = 512
        Private Const CFM_SHADOW As Integer = 1024
        Private Const CFM_EMBOSS As Integer = 2048
        Private Const CFM_IMPRINT As Integer = 4096
        Private Const CFM_DISABLED As Integer = 8192
        Private Const CFM_REVISED As Integer = 16384
        Private Const CFM_BACKCOLOR As Integer = 67108864
        Private Const CFM_LCID As Integer = 33554432
        Private Const CFM_UNDERLINETYPE As Integer = 8388608
        Private Const CFM_WEIGHT As Integer = 4194304
        Private Const CFM_SPACING As Integer = 2097152
        Private Const CFM_KERNING As Integer = 1048576
        Private Const CFM_STYLE As Integer = 524288
        Private Const CFM_ANIMATION As Integer = 262144
        Private Const CFM_REVAUTHOR As Integer = 32768
        Private Const CFM_BOLD As UInt32 = 1
        Private Const CFM_ITALIC As UInt32 = 2
        Private Const CFM_UNDERLINE As UInt32 = 4
        Private Const CFM_STRIKEOUT As UInt32 = 8
        Private Const CFM_PROTECTED As UInt32 = 16
        Private Const CFM_LINK As UInt32 = 32
        Private Const CFM_SIZE As UInt32 = 2147483648
        Private Const CFM_COLOR As UInt32 = 1073741824
        Private Const CFM_FACE As UInt32 = 536870912
        Private Const CFM_OFFSET As UInt32 = 268435456
        Private Const CFM_CHARSET As UInt32 = 134217728
        Private Const CFM_SUBSCRIPT As UInt32 = (CFE_SUBSCRIPT Or CFE_SUPERSCRIPT)
        Private Const CFM_SUPERSCRIPT As UInt32 = CFM_SUBSCRIPT
        Private Const CFU_UNDERLINENONE As Byte = 0
        Private Const CFU_UNDERLINE As Byte = 1
        Private Const CFU_UNDERLINEWORD As Byte = 2
        Private Const CFU_UNDERLINEDOUBLE As Byte = 3
        Private Const CFU_UNDERLINEDOTTED As Byte = 4
        Private Const CFU_UNDERLINEDASH As Byte = 5
        Private Const CFU_UNDERLINEDASHDOT As Byte = 6
        Private Const CFU_UNDERLINEDASHDOTDOT As Byte = 7
        Private Const CFU_UNDERLINEWAVE As Byte = 8
        Private Const CFU_UNDERLINETHICK As Byte = 9
        Private Const CFU_UNDERLINEHAIRLINE As Byte = 10
#End Region

#Region "Constructor and Defaults..."

        Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
            MyBase.OnPaint(pe)
        End Sub

#End Region

#Region "Public Event..."
        Public Event CursorPositionChanged As EventHandler

        Public Sub InsertTable(ByVal Column As Integer, ByVal row As Integer, ByVal pwidth As Integer)
            Dim width As Integer = HundredthInchToTwips(pwidth)
            Dim border As String = "\brdrs\brdrw15"
            Dim tablestring As New StringBuilder()

            tablestring.Append("\trowd\trgraph70\trleft170")
            tablestring.Append(Convert.ToString("\trbrdrt") & border)
            tablestring.Append(Convert.ToString("\trbrdrl") & border)
            tablestring.Append(Convert.ToString("\trbrdrb") & border)
            tablestring.Append(Convert.ToString("\trbrdrr") & border)

            For kolom As Integer = 0 To Column - 1
                tablestring.Append(Convert.ToString("\clbrdrt") & border)
                tablestring.Append(Convert.ToString("\clbrdrl") & border)
                tablestring.Append(Convert.ToString("\clbrdrb") & border)
                tablestring.Append(Convert.ToString("\clbrdrr") & border)
                Dim breedte As Integer = (width \ Column) * (kolom + 1)
                tablestring.Append("\cellx")
                tablestring.Append(breedte.ToString())
            Next

            tablestring.Append("\pard")

            For rij As Integer = 0 To row - 1
                tablestring.Append("\intbl")
                For kolom As Integer = 0 To row - 1
                    tablestring.Append("\cell")
                Next
                tablestring.Append("\row")
            Next
            tablestring.Append("\pard\par}")

            InsertTextAsRtf(tablestring.ToString())
        End Sub

#End Region

#Region "Private Declare Sub..."

        Public Sub SelectedFont(ByVal family As System.Drawing.FontFamily, ByVal size As Single, ByVal fontstyle As System.Drawing.FontStyle)
            Me.SelectionFont = New System.Drawing.Font(family, size, fontstyle)
            Me.Focus()
        End Sub

        ''' <summary>
        ''' Free cached data from rich edit control after printing
        ''' </summary>
        Public Sub FormatRangeDone()
            Dim lParam As New IntPtr(0)
            SendMessage(Handle, EM_FORMATRANGE, 0, lParam)
        End Sub

        ''' <summary>
        ''' Insert a given text as a link into the RichTextBox at the current insert position.
        ''' </summary>
        ''' <param name="text">Text to be inserted</param>
        Public Overloads Sub InsertLink(ByVal text As String)
            InsertLink(text, Me.SelectionStart)
        End Sub

        ''' <summary>
        ''' Insert a given text at a given position as a link. 
        ''' </summary>
        ''' <param name="text">Text to be inserted</param>
        ''' <param name="position">Insert position</param>
        Public Overloads Sub InsertLink(ByVal text As String, ByVal position As Integer)
            If ((position < 0) _
                        OrElse (position > Me.Text.Length)) Then
                Throw New ArgumentOutOfRangeException("position")
            End If
            Me.SelectionStart = position
            Me.SelectedText = text
            Me.Select(position, text.Length)
            Me.SetSelectionLink(True)
            Me.Select((position + text.Length), 0)
        End Sub

        ''' <summary>
        ''' Insert a given text at at the current input position as a link.
        ''' The link text is followed by a hash (#) and the given hyperlink text, both of
        ''' them invisible.
        ''' When clicked on, the whole link text and hyperlink string are given in the
        ''' LinkClickedEventArgs.
        ''' </summary>
        ''' <param name="text">Text to be inserted</param>
        ''' <param name="hyperlink">Invisible hyperlink string to be inserted</param>
        Public Overloads Sub InsertLink(ByVal text As String, ByVal hyperlink As String)
            InsertLink(text, hyperlink, Me.SelectionStart)
        End Sub

        ''' <summary>
        ''' Insert a given text at a given position as a link. The link text is followed by
        ''' a hash (#) and the given hyperlink text, both of them invisible.
        ''' When clicked on, the whole link text and hyperlink string are given in the
        ''' LinkClickedEventArgs.
        ''' </summary>
        ''' <param name="text">Text to be inserted</param>
        ''' <param name="hyperlink">Invisible hyperlink string to be inserted</param>
        ''' <param name="position">Insert position</param>
        Public Overloads Sub InsertLink(ByVal text As String, ByVal hyperlink As String, ByVal position As Integer)
            If ((position < 0) _
                        OrElse (position > Me.Text.Length)) Then
                Throw New ArgumentOutOfRangeException("position")
            End If
            Me.SelectionStart = position
            Me.SelectedRtf = ("{\rtf1\ansi " _
                        + (text + ("\v #" _
                        + (hyperlink + "\v0}"))))
            Me.Select(position, (text.Length _
                            + (hyperlink.Length + 1)))
            Me.SetSelectionLink(True)
            Me.Select((position _
                            + (text.Length _
                            + (hyperlink.Length + 1))), 0)
        End Sub

        ''' <summary>
        ''' Set the current selection's link style
        ''' </summary>
        ''' <param name="link">true: set link style, false: clear link style</param>
        Public Sub SetSelectionLink(ByVal link As Boolean)
            SetSelectionStyle(CFM_LINK, link)
            'TODO: Warning!!!, inline IF is not supported ?
        End Sub


        Public Sub SelLineSpacing(ByVal rtbTarget As RichTextBox, ByVal SpacingRule As Byte, Optional ByVal LineSpacing As Integer = 20)
            Dim Para As New PARAFORMAT2
            With Para
                ReDim .rgxTabs(31)

                .cbSize = CUInt(Marshal.SizeOf(Para))
                .dwMask = PFM_LINESPACING
                .bLineSpacingRule = SpacingRule
                .dyLineSpacing = LineSpacing
            End With
            Dim result As Integer = SendMessage(New HandleRef(rtbTarget, rtbTarget.Handle), EM_SETPARAFORMAT, 1, Para)
            If result = 0 Then
                MessageBox.Show("EM_SETPARAFORMAT Failed")
            End If
        End Sub

        Private Sub SetSelectionStyle(ByVal mask As UInt32, ByVal effect As UInt32)
            Dim cf As CHARFORMAT2_STRUCT = New CHARFORMAT2_STRUCT
            cf.cbSize = CType(Marshal.SizeOf(cf), UInt32)
            cf.dwMask = mask
            cf.dwEffects = effect
            Dim wpar As IntPtr = New IntPtr(SCF_SELECTION)
            Dim lpar As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
            Marshal.StructureToPtr(cf, lpar, False)
            Dim res As IntPtr = SendMessage(Handle, EM_SETCHARFORMAT, wpar, lpar)
            Marshal.FreeCoTaskMem(lpar)
        End Sub

        Public Sub AddControl(ByVal oneControl As Control)
            Dim one As New MetaInfo(oneControl)
            MyBase.Controls.Add(oneControl)
            one.CharIndex = Me.TextLength
            one.TheControl.Location = Me.GetPositionFromCharIndex(one.CharIndex)
            one.DeltaY = Me.GetPositionFromCharIndex(0).Y - one.TheControl.Location.Y
            ControlList.Add(one)
            Do
                Me.AppendText(Environment.NewLine)
            Loop While Me.GetPositionFromCharIndex(Me.TextLength).Y < (oneControl.Location.Y + oneControl.Height)
            RemoveSome()
            AutoScroll()
        End Sub

        Public Sub AutoScroll()
            Me.SelectionStart = Me.TextLength - 1
            Me.ScrollToCaret()
        End Sub

        Private Sub RemoveSome()
            If Not KeepShort Then
                Return
            End If
            Dim texttoRemove As Integer = 0
            Dim imgtoRemove As Integer = 0
            Try
                If Me.TextLength > MaximumTextLength Then
                    texttoRemove = TextLengthToBeRemoved
                    Me.Text = Me.Text.Substring(texttoRemove)
                    texttoRemove += Me.Text.IndexOf(vbLf)
                    If texttoRemove > TextLengthToBeRemoved Then
                        Me.Text = Me.Text.Substring(texttoRemove - TextLengthToBeRemoved)
                    End If
                    For Each oldone As MetaInfo In ControlList
                        If oldone.CharIndex < texttoRemove Then
                            imgtoRemove += 1
                        Else
                            oldone.CharIndex -= texttoRemove
                        End If
                    Next
                    For i As Integer = 0 To imgtoRemove - 1
                        Me.Controls(0).Dispose()
                        ControlList.RemoveAt(0)
                    Next
                    CalculateDelta()
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Try
                If ControlList.Count > MaximumNoOfControl Then
                    imgtoRemove = NoOfControlToBeRemoved
                    For i As Integer = 0 To imgtoRemove - 1
                        texttoRemove = ControlList(0).CharIndex
                        ControlList.RemoveAt(0)
                        Me.Controls(0).Dispose()
                    Next
                    Me.Text = Me.Text.Substring(texttoRemove)
                    For Each oldone As MetaInfo In ControlList
                        oldone.CharIndex -= texttoRemove
                    Next
                    CalculateDelta()
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub TRichTextBox_VScroll(ByVal sender As Object, ByVal e As EventArgs)
            Dim pt As New POINT()
            SendMessages(Me.Handle, EM_GETSCROLLPOS, 0, pt)
            For Each one As MetaInfo In ControlList
                one.TheControl.Location = New System.Drawing.Point(one.TheControl.Location.X, -pt.y - one.DeltaY)
            Next
        End Sub

        Private Sub TRichTextBox_SizeChanged(ByVal sender As Object, ByVal e As EventArgs)
            CalculateDelta()
        End Sub

        Private Sub CalculateDelta()
            For Each one As MetaInfo In ControlList
                one.TheControl.Location = Me.GetPositionFromCharIndex(one.CharIndex)
                one.DeltaY = Me.GetPositionFromCharIndex(0).Y - one.TheControl.Location.Y
            Next
        End Sub

        Private Sub TRichTextBox_LinkClicked(ByVal sender As Object, ByVal e As LinkClickedEventArgs)
            System.Diagnostics.Process.Start(e.LinkText)
        End Sub

#End Region

#Region "Protected Overridable Sub..."
        Protected Overridable Sub OnCursorPositionChanged(ByVal e As EventArgs)
            RaiseEvent CursorPositionChanged(Me, e)
        End Sub

        Protected Overrides Sub OnSelectionChanged(ByVal e As EventArgs)
            If SelectionLength = 0 Then
                OnCursorPositionChanged(e)
            Else
                MyBase.OnSelectionChanged(e)
            End If
        End Sub
#End Region

#Region "Public Sub..."

        Public Sub Insert_Bullet2()
            Dim param As New PARAFORMAT2
            param.cbSize = Marshal.SizeOf(param)
            SendMessage(New HandleRef(Me, Me.Handle), EM_GETPARAFORMAT, 0, param)
            param.dwMask = PFM_NUMBERING
            param.wNumbering = BULLET_NUMBER2
            SendMessage(New HandleRef(Me, Me.Handle), EM_SETPARAFORMAT, 0, param)
        End Sub

        Public Sub Insert_Bullet3()
            Dim param As New PARAFORMAT2
            param.cbSize = Marshal.SizeOf(param)
            SendMessage(New HandleRef(Me, Me.Handle), EM_GETPARAFORMAT, 0, param)
            param.dwMask = PFM_NUMBERING
            param.wNumbering = BULLET_NUMBER3
            SendMessage(New HandleRef(Me, Me.Handle), EM_SETPARAFORMAT, 0, param)
        End Sub

        Public Sub Insert_Bullet4()
            Dim param As New PARAFORMAT2
            param.cbSize = Marshal.SizeOf(param)
            SendMessage(New HandleRef(Me, Me.Handle), EM_GETPARAFORMAT, 0, param)
            param.dwMask = PFM_NUMBERING
            param.wNumbering = BULLET_NUMBER4
            SendMessage(New HandleRef(Me, Me.Handle), EM_SETPARAFORMAT, 0, param)
        End Sub

#End Region

#Region "Public ReadOnly Property..."

        <DefaultValue(False)> _
        Public Shadows Property DetectUrls() As Boolean
            Get
                Return MyBase.DetectUrls
            End Get
            Set(ByVal value As Boolean)
                MyBase.DetectUrls = value
            End Set
        End Property

        Public ReadOnly Property CurrentColumn() As Integer
            Get
                Return CursorPosition.Column(Me, SelectionStart)
            End Get
        End Property

        Public ReadOnly Property CurrentLine() As Integer
            Get
                Return CursorPosition.Line(Me, SelectionStart)
            End Get
        End Property

        Public ReadOnly Property CurrentPosition() As Integer
            Get
                Return Me.SelectionStart
            End Get
        End Property

        Public ReadOnly Property SelectionEnd() As Integer
            Get
                Return SelectionStart + SelectionLength
            End Get
        End Property
#End Region

#Region "Perform Spell Check..."

        Public Sub CheckSpelling()
            Dim SpellingErrors As Integer = 0
            Dim ErrorCountMessage As String = String.Empty
            Dim WordApp As New Microsoft.Office.Interop.Word.Application()
            WordApp.Visible = False
            WordApp.ShowWindowsInTaskbar = False
            If Me.Text.Length > 0 Then
                Dim WordDoc As _Document = WordApp.Documents.Add(emptyItem, emptyItem, emptyItem, oFalse)
                WordDoc.Words.First.InsertBefore(Me.Text)
                Dim docErrors As Microsoft.Office.Interop.Word.ProofreadingErrors = WordDoc.SpellingErrors
                SpellingErrors = docErrors.Count
                WordDoc.CheckSpelling(oNothing, oIgnoreUpperCase, oAlwaysSuggest, oNothing, oNothing, oNothing, _
                 oNothing, oNothing, oNothing, oNothing, oNothing, oNothing)
                ErrorCountMessage = "Spell check complete; errors detected: " & SpellingErrors
                Dim first As Object = 0
                Dim last As Object = WordDoc.Characters.Count - 1
                Me.Text = WordDoc.Range(first, last).Text
            Else
                ErrorCountMessage = "Unable to spell check an empty text box."
            End If
            WordApp.Quit(oFalse, emptyItem, emptyItem)
            MessageBox.Show(ErrorCountMessage, "Finished Spelling Check")
        End Sub

#End Region

#Region "Property: SelectionBackColor..."
        <StructLayout(LayoutKind.Sequential)> Private Structure CharFormat2
            Public cbSize As Int32
            Public dwMask As Int32
            Public dwEffects As Int32
            Public yHeight As Int32
            Public yOffset As Int32
            Public crTextColor As Int32
            Public bCharSet As Byte
            Public bPitchAndFamily As Byte
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public szFaceName As String
            Public wWeight As Int16
            Public sSpacing As Int16
            Public crBackColor As Int32
            Public lcid As Int32
            Public dwReserved As Int32
            Public sStyle As Int16
            Public wKerning As Int16
            Public bUnderlineType As Byte
            Public bAnimation As Byte
            Public bRevAuthor As Byte
            Public bReserved1 As Byte
        End Structure

        Private Const LF_FACESIZE = 32
        Private Const WM_SETTEXT = &HC
        Private Const CFE_AUTOBACKCOLOR = CFM_BACKCOLOR
        Private Const EM_SETBKGNDCOLOR = (WM_USER + 67)
        'Private Const CFM_BACKCOLOR = &H4000000
        'Private Const EM_SETCHARFORMAT = (WM_USER + 68)
        'Private Const EM_GETCHARFORMAT = (WM_USER + 58)
        'Private Const SCF_SELECTION = &H1&

        Private Overloads Declare Auto Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByRef lParam As CharFormat2) As Boolean

        ' Here we do the magic...
        Public Overloads Property SelectionBackColor() As Color
            Get
                ' We need to ask the RTB for the backcolor of the current selection.
                ' This is done using SendMessage with a format structure which the RTB will fill in for us.
                Dim HWND As IntPtr = Me.Handle ' Force the creation of the window handle...
                Dim Format As New CharFormat2
                Format.dwMask = CFM_BACKCOLOR
                Format.cbSize = Marshal.SizeOf(Format)
                SendMessage(Me.Handle, EM_GETCHARFORMAT, SCF_SELECTION, Format)
                Return ColorTranslator.FromOle(Format.crBackColor)
            End Get
            Set(ByVal Value As Color)
                ' Here we do relatively the same thing as in Get, but we are telling the RTB to set
                ' the color this time instead of returning it to us.
                Dim HWND As IntPtr = Me.Handle ' Force the creation of the window handle...
                Dim Format As New CharFormat2
                Format.crBackColor = ColorTranslator.ToOle(Value)
                Format.dwMask = CFM_BACKCOLOR
                Format.cbSize = Marshal.SizeOf(Format)
                SendMessage(Me.Handle, EM_SETCHARFORMAT, SCF_SELECTION, Format)
            End Set
        End Property
#End Region

#Region "Insert Image"

        ''' <summary>
        ''' Inserts an image into the RichTextBox.  The image is wrapped in a Windows
        ''' Format Metafile, because although Microsoft discourages the use of a WMF,
        ''' the RichTextBox (and even MS Word), wraps an image in a WMF before inserting
        ''' the image into a document.  The WMF is attached in HEX format (a string of
        ''' HEX numbers).
        ''' 
        ''' The RTF Specification v1.6 says that you should be able to insert bitmaps,
        ''' .jpegs, .gifs, .pngs, and Enhanced Metafiles (.emf) directly into an RTF
        ''' document without the WMF wrapper. This works fine with MS Word,
        ''' however, when you don't wrap images in a WMF, WordPad and
        ''' RichTextBoxes simply ignore them.  Both use the riched20.dll or msfted.dll.
        ''' </summary>
        ''' <remarks>
        ''' NOTE: The image is inserted wherever the caret is at the time of the call,
        ''' and if any text is selected, that text is replaced.
        ''' </remarks>
        ''' <param name="_image"></param>
        Public Sub InsertImage(ByVal _image As Image)
            Dim _rtf As New StringBuilder()
            _rtf.Append(RTF_HEADER)
            _rtf.Append(GetFontTable(Me.Font))
            _rtf.Append(GetImagePrefix(_image))
            _rtf.Append(GetRtfImage(_image))
            _rtf.Append(RTF_IMAGE_POST)
            Me.SelectedRtf = _rtf.ToString()
        End Sub

        ''' <summary>
        ''' Wraps the image in an Enhanced Metafile by drawing the image onto the
        ''' graphics context, then converts the Enhanced Metafile to a Windows
        ''' Metafile, and finally appends the bits of the Windows Metafile in HEX
        ''' to a string and returns the string.
        ''' </summary>
        ''' <param name="_image"></param>
        ''' <returns>
        ''' A string containing the bits of a Windows Metafile in HEX
        ''' </returns>
        Private Function GetRtfImage(ByVal _image As Image) As String

            Dim _rtf As StringBuilder = Nothing

            ' Used to store the enhanced metafile
            Dim _stream As MemoryStream = Nothing

            ' Used to create the metafile and draw the image
            Dim _graphics As Graphics = Nothing

            ' The enhanced metafile
            Dim _metaFile As Metafile = Nothing

            ' Handle to the device context used to create the metafile
            Dim _hdc As IntPtr

            Try
                _rtf = New StringBuilder()
                _stream = New MemoryStream()

                ' Get a graphics context from the RichTextBox
                Using _grp = Me.CreateGraphics()

                    ' Get the device context from the graphics context
                    _hdc = _graphics.GetHdc()

                    ' Create a new Enhanced Metafile from the device context
                    _metaFile = New Metafile(_stream, _hdc)

                    ' Release the device context
                    _graphics.ReleaseHdc(_hdc)
                End Using

                ' Get a graphics context from the Enhanced Metafile
                Using _grp = Graphics.FromImage(_metaFile)

                    ' Draw the image on the Enhanced Metafile

                    _graphics.DrawImage(_image, New System.Drawing.Rectangle(0, 0, _image.Width, _image.Height))
                End Using

                ' Get the handle of the Enhanced Metafile
                Dim _hEmf As IntPtr = _metaFile.GetHenhmetafile()

                ' A call to EmfToWmfBits with a null buffer return the size of the
                ' buffer need to store the WMF bits.  Use this to get the buffer
                ' size.
                Dim _bufferSize As UInteger = GdipEmfToWmfBits(_hEmf, 0, Nothing, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault)

                ' Create an array to hold the bits
                Dim _buffer As Byte() = New Byte(_bufferSize - 1) {}

                ' A call to EmfToWmfBits with a valid buffer copies the bits into the
                ' buffer an returns the number of bits in the WMF.  
                Dim _convertedSize As UInteger = GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault)

                ' Append the bits to the RTF string
                For i As Integer = 0 To _buffer.Length - 1
                    _rtf.Append([String].Format("{0:X2}", _buffer(i)))
                Next

                Return _rtf.ToString()
            Finally
                If _graphics IsNot Nothing Then
                    _graphics.Dispose()
                End If
                If _metaFile IsNot Nothing Then
                    _metaFile.Dispose()
                End If
                If _stream IsNot Nothing Then
                    _stream.Close()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Creates the RTF control string that describes the image being inserted.
        ''' This description (in this case) specifies that the image is an
        ''' MM_ANISOTROPIC metafile, meaning that both X and Y axes can be scaled
        ''' independently.  The control string also gives the images current dimensions,
        ''' and its target dimensions, so if you want to control the size of the
        ''' image being inserted, this would be the place to do it. The prefix should
        ''' have the form ...
        ''' 
        ''' {\pict\wmetafile8\picw[A]\pich[B]\picwgoal[C]\pichgoal[D]
        ''' 
        ''' where ...
        ''' 
        ''' A	= current width of the metafile in hundredths of millimeters (0.01mm)
        '''		= Image Width in Inches * Number of (0.01mm) per inch
        '''		= (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 2540
        '''		= (Image Width in Pixels / Graphics.DpiX) * 2540
        ''' 
        ''' B	= current height of the metafile in hundredths of millimeters (0.01mm)
        '''		= Image Height in Inches * Number of (0.01mm) per inch
        '''		= (Image Height in Pixels / Graphics Context's Vertical Resolution) * 2540
        '''		= (Image Height in Pixels / Graphics.DpiX) * 2540
        ''' 
        ''' C	= target width of the metafile in twips
        '''		= Image Width in Inches * Number of twips per inch
        '''		= (Image Width in Pixels / Graphics Context's Horizontal Resolution) * 1440
        '''		= (Image Width in Pixels / Graphics.DpiX) * 1440
        ''' 
        ''' D	= target height of the metafile in twips
        '''		= Image Height in Inches * Number of twips per inch
        '''		= (Image Height in Pixels / Graphics Context's Horizontal Resolution) * 1440
        '''		= (Image Height in Pixels / Graphics.DpiX) * 1440
        '''	
        ''' </summary>
        ''' <remarks>
        ''' The Graphics Context's resolution is simply the current resolution at which
        ''' windows is being displayed.  Normally it's 96 dpi, but instead of assuming
        ''' I just added the code.
        ''' 
        ''' According to Ken Howe at pbdr.com, "Twips are screen-independent units
        ''' used to ensure that the placement and proportion of screen elements in
        ''' your screen application are the same on all display systems."
        ''' 
        ''' Units Used
        ''' ----------
        ''' 1 Twip = 1/20 Point
        ''' 1 Point = 1/72 Inch
        ''' 1 Twip = 1/1440 Inch
        ''' 
        ''' 1 Inch = 2.54 cm
        ''' 1 Inch = 25.4 mm
        ''' 1 Inch = 2540 (0.01)mm
        ''' </remarks>
        ''' <param name="_image"></param>
        ''' <returns></returns>
        Private Function GetImagePrefix(ByVal _image As Image) As String
            Dim _rtf As New StringBuilder()
            Dim picw As Integer = CInt(Math.Round((_image.Width / xDpi) * HMM_PER_INCH))
            Dim pich As Integer = CInt(Math.Round((_image.Height / yDpi) * HMM_PER_INCH))
            Dim picwgoal As Integer = CInt(Math.Round((_image.Width / xDpi) * TWIPS_PER_INCH))
            Dim pichgoal As Integer = CInt(Math.Round((_image.Height / yDpi) * TWIPS_PER_INCH))
            _rtf.Append("{\pict\wmetafile8")
            _rtf.Append("\picw")
            _rtf.Append(picw)
            _rtf.Append("\pich")
            _rtf.Append(pich)
            _rtf.Append("\picwgoal")
            _rtf.Append(picwgoal)
            _rtf.Append("\pichgoal")
            _rtf.Append(pichgoal)
            _rtf.Append(" ")
            Return _rtf.ToString()
        End Function

        ''' <summary>
        ''' Use the EmfToWmfBits function in the GDI+ specification to convert a 
        ''' Enhanced Metafile to a Windows Metafile
        ''' </summary>
        ''' <param name="_hEmf">
        ''' A handle to the Enhanced Metafile to be converted
        ''' </param>
        ''' <param name="_bufferSize">
        ''' The size of the buffer used to store the Windows Metafile bits returned
        ''' </param>
        ''' <param name="_buffer">
        ''' An array of bytes used to hold the Windows Metafile bits returned
        ''' </param>
        ''' <param name="_mappingMode">
        ''' The mapping mode of the image.  This control uses MM_ANISOTROPIC.
        ''' </param>
        ''' <param name="_flags">
        ''' Flags used to specify the format of the Windows Metafile returned
        ''' </param>
        <DllImportAttribute("gdiplus.dll")> _
        Private Shared Function GdipEmfToWmfBits(ByVal _hEmf As IntPtr, ByVal _bufferSize As UInteger, ByVal _buffer As Byte(), ByVal _mappingMode As Integer, ByVal _flags As EmfToWmfBitsFlags) As UInteger
        End Function

        '''' <summary>
        '''' Wraps the image in an Enhanced Metafile by drawing the image onto the
        '''' graphics context, then converts the Enhanced Metafile to a Windows
        '''' Metafile, and finally appends the bits of the Windows Metafile in HEX
        '''' to a string and returns the string.
        '''' </summary>
        '''' <param name="_image"></param>
        '''' <returns>
        '''' A string containing the bits of a Windows Metafile in HEX
        '''' </returns>
        'Private Function GetRtfImage(ByVal _image As Image) As String
        '    Dim _rtf As StringBuilder = Nothing
        '    Dim _stream As MemoryStream = Nothing
        '    Dim _graphics As Graphics = Nothing 
        '    Dim _metaFile As Metafile = Nothing
        '    Dim _hdc As IntPtr
        '    Try
        '        _rtf = New StringBuilder()
        '        _stream = New MemoryStream()
        '        Using _graphics = Me.CreateGraphics()
        '            _hdc = _graphics.GetHdc()
        '            _metaFile = New Metafile(_stream, _hdc)
        '            _graphics.ReleaseHdc(_hdc)
        '        End Using
        '        Using _graphics = Graphics.FromImage(_metaFile)
        '            _graphics.DrawImage(_image, New System.Drawing.Rectangle(0, 0, _image.Width, _image.Height))
        '        End Using
        '        Dim _hEmf As IntPtr = _metaFile.GetHenhmetafile()
        '        Dim _bufferSize As UInteger = GdipEmfToWmfBits(_hEmf, 0, Nothing, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault)
        '        Dim _buffer As Byte() = New Byte(_bufferSize - 1) {}
        '        Dim _convertedSize As UInteger = GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault)
        '        For i As Integer = 0 To _buffer.Length - 1
        '            _rtf.Append([String].Format("{0:X2}", _buffer(i)))
        '        Next
        '        Return _rtf.ToString()
        '    Finally
        '        If _graphics IsNot Nothing Then
        '            _graphics.Dispose()
        '        End If
        '        If _metaFile IsNot Nothing Then
        '            _metaFile.Dispose()
        '        End If
        '        If _stream IsNot Nothing Then
        '            _stream.Close()
        '        End If
        '    End Try
        'End Function

#End Region

#Region "Append RTF or Text to RichTextBox Contents"

        Public Sub AppendRtf(ByVal _rtf As String)
            Me.[Select](Me.TextLength, 0)
            Me.SelectedRtf = _rtf
        End Sub

        Public Sub InsertRtf(ByVal _rtf As String)
            Me.SelectedRtf = _rtf
        End Sub

        Public Sub AppendTextAsRtf(ByVal _text As String)
            AppendTextAsRtf(_text, Me.Font)
        End Sub

        Public Sub AppendTextAsRtf(ByVal _text As String, ByVal _font As System.Drawing.Font)
            AppendTextAsRtf(_text, _font, textColor)
        End Sub

        Public Sub AppendTextAsRtf(ByVal _text As String, ByVal _font As System.Drawing.Font, ByVal _textColor As RtfColor)
            AppendTextAsRtf(_text, _font, _textColor, highlightColor)
        End Sub

        Public Sub AppendTextAsRtf(ByVal _text As String, ByVal _font As System.Drawing.Font, ByVal _textColor As RtfColor, ByVal _backColor As RtfColor)
            Me.[Select](Me.TextLength, 0)
            InsertTextAsRtf(_text, _font, _textColor, _backColor)
        End Sub

#End Region

#Region "Proc: ClearBackColor..."

#Region "ScrollBarTypes..."
        Private Enum ScrollBarTypes
            SB_HORZ = 0
            SB_VERT = 1
            SB_CTL = 2
            SB_BOTH = 3
        End Enum
#End Region

#Region "SrollBarInfoFlags..."
        Private Enum ScrollBarInfoFlags
            SIF_RANGE = &H1
            SIF_PAGE = &H2
            SIF_POS = &H4
            SIF_DISABLENOSCROLL = &H8
            SIF_TRACKPOS = &H10
            SIF_ALL = (SIF_RANGE Or SIF_PAGE Or SIF_POS Or SIF_TRACKPOS)
        End Enum
#End Region

        Public Sub ClearBackColor(Optional ByVal ClearAll As Boolean = True)
            Dim HWND As IntPtr = Me.Handle ' Force the creation of the window handle...

            LockWindowUpdate(Me.Handle)   ' Lock drawing...
            Me.SuspendLayout()
            Dim ScrollPosVert As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_VERT)
            Dim ScrollPosHoriz As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_HORZ)
            Dim SelStart As Integer = Me.SelectionStart
            Dim SelLength As Integer = Me.SelectionLength

            If ClearAll Then Me.SelectAll() ' Should we clear everything or just use the current selection?
            Dim Format As New CharFormat2
            Format.crBackColor = -1
            Format.dwMask = CFM_BACKCOLOR
            Format.dwEffects = CFE_AUTOBACKCOLOR  ' Clears the backcolor
            Format.cbSize = Marshal.SizeOf(Format)
            SendMessage(Me.Handle, EM_SETCHARFORMAT, SCF_SELECTION, Format)

            ' Return the previous values...
            Me.SelectionStart = SelStart
            Me.SelectionLength = SelLength
            'SendMessage(Me.Handle, EMFlags.EM_SETSCROLLPOS, 0, New RichTextBox.POINT(ScrollPoshoriz, ScrollPosVert))
            Me.ResumeLayout()
            LockWindowUpdate(IntPtr.Zero) ' Unlock drawing...
        End Sub

        <StructLayout(LayoutKind.Sequential)> Private Structure SCROLLINFO
            Public cbSize As Integer ' UINT cbSize; 
            Public fMask As ScrollBarInfoFlags ' UINT fMask; 
            Public nMin As Integer 'int  nMin; 
            Public nMax As Integer 'int  nMax; 
            Public nPage As Integer 'UINT nPage;  
            Public nPos As Integer ' int  nPos; 
            Public nTrackPos As Integer ' int  nTrackPos; 
        End Structure

        Private Declare Function GetScrollInfo Lib "User32" (ByVal hWnd As IntPtr, ByVal fnBar As ScrollBarTypes, ByRef lpsi As SCROLLINFO) As Boolean
        Private Function GetScrollBarPos(ByVal hWnd As IntPtr, ByVal BarType As ScrollBarTypes) As Integer
            Dim INFO As SCROLLINFO
            INFO.fMask = ScrollBarInfoFlags.SIF_POS
            INFO.cbSize = Marshal.SizeOf(INFO)
            GetScrollInfo(hWnd, BarType, INFO)
            Return INFO.nPos
        End Function
#End Region

#Region "Proc: Highlight..."
        Private Declare Function LockWindowUpdate Lib "user32.dll" (ByVal hWndLock As IntPtr) As Boolean
        Public Sub Highlight(ByVal FindWhat As String, ByVal Highlight As Color, ByVal MatchCase As Boolean, ByVal MatchWholeWord As Boolean)
            LockWindowUpdate(Me.Handle)   ' Lock drawing...
            Me.SuspendLayout()
            Dim ScrollPosVert As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_VERT)
            Dim ScrollPosHoriz As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_HORZ)
            Dim SelStart As Integer = Me.SelectionStart
            Dim SelLength As Integer = Me.SelectionLength

            Dim StartFrom As Integer = 0
            Dim Length As Integer = FindWhat.Length
            Dim Finds As RichTextBoxFinds
            ' Setup the flags for searching.
            If MatchCase Then Finds = Finds Or RichTextBoxFinds.MatchCase
            If MatchWholeWord Then Finds = Finds Or RichTextBoxFinds.WholeWord

            ' Do the search.
            While Me.Find(FindWhat, StartFrom, Finds) > -1
                Me.SelectionBackColor = Highlight
                StartFrom = Me.SelectionStart + Me.SelectionLength  ' Continue after the one we found..
            End While

            ' Return the previous values...
            Me.SelectionStart = SelStart
            Me.SelectionLength = SelLength
            'SendMessage(Me.Handle, EMFlags.EM_SETSCROLLPOS, 0, New RichTextBox.POINT(ScrollPosHoriz, ScrollPosVert))
            Me.ResumeLayout()
            LockWindowUpdate(IntPtr.Zero) ' Unlock drawing...
        End Sub
#End Region

#Region "Proc: ScrollToBottom..."

#Region "Scroller Flags..."
        Private Enum EMFlags
            EM_SETSCROLLPOS = &H400 + 222
        End Enum
#End Region

#Region "ScrollBarFlags..."
        Private Enum ScrollBarFlags
            SBS_HORZ = &H0
            SBS_VERT = &H1
            SBS_TOPALIGN = &H2
            SBS_LEFTALIGN = &H2
            SBS_BOTTOMALIGN = &H4
            SBS_RIGHTALIGN = &H4
            SBS_SIZEBOXTOPLEFTALIGN = &H2
            SBS_SIZEBOXBOTTOMRIGHTALIGN = &H4
            SBS_SIZEBOX = &H8
            SBS_SIZEGRIP = &H10
        End Enum
#End Region

#Region "Structure: POINT..."
        <StructLayout(LayoutKind.Sequential)> Private Class POINT
            Public x As Integer
            Public y As Integer

            Public Sub New()
            End Sub

            Public Sub New(ByVal x As Integer, ByVal y As Integer)
                Me.x = x
                Me.y = y
            End Sub
        End Class
#End Region

        Private Declare Function GetScrollRange Lib "User32" (ByVal hWnd As IntPtr, ByVal nBar As Integer, ByRef lpMinPos As Integer, ByRef lpMaxPos As Integer) As Boolean
        'Private Overloads Declare Auto Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As RichTextBox.POINT) As IntPtr
        Public Sub ScrollToBottom()
            Dim Min, Max As Integer
            GetScrollRange(Me.Handle, ScrollBarFlags.SBS_VERT, Min, Max)
            'SendMessage(Me.Handle, EMFlags.EM_SETSCROLLPOS, 0, New RichTextBox.POINT(0, Max - Me.Height))
        End Sub

#End Region

#Region "Accessors"

        Public Shadows Property Rtf() As String
            Get
                Return RemoveBadChars(MyBase.Rtf)
            End Get
            Set(ByVal value As String)
                MyBase.Rtf = value
            End Set
        End Property

        Public Property HiglightColor() As RtfColor
            Get
                Return highlightColor
            End Get
            Set(ByVal value As RtfColor)
                highlightColor = value
            End Set
        End Property

#End Region

    End Class

    Friend Class CursorPosition

#Region "Public Shared Function..."
        <System.Runtime.InteropServices.DllImport("user32")> _
                Public Shared Function GetCaretPos(ByRef lpPoint As Drawing.Point) As Integer
        End Function
#End Region

#Region "Private Shared Function..."
        Private Shared Function GetCorrection(ByVal e As RichTextBox, ByVal index As Integer) As Integer
            Dim pt1 As Drawing.Point = Drawing.Point.Empty
            GetCaretPos(pt1)
            Dim pt2 As Drawing.Point = e.GetPositionFromCharIndex(index)

            If pt1 <> pt2 Then
                Return 1
            Else
                Return 0
            End If
        End Function
#End Region

#Region "Public Shared Function..."
        Public Shared Function Line(ByVal e As RichTextBox, ByVal index As Integer) As Integer
            Dim correction As Integer = GetCorrection(e, index)
            Return e.GetLineFromCharIndex(index) - correction + 1
        End Function

        Public Shared Function Column(ByVal e As RichTextBox, ByVal index1 As Integer) As Integer
            Dim correction As Integer = GetCorrection(e, index1)
            Dim p As Drawing.Point = e.GetPositionFromCharIndex(index1 - correction)

            If p.X = 1 Then
                Return 1
            End If

            p.X = 0
            Dim index2 As Integer = e.GetCharIndexFromPosition(p)

            Dim col As Integer = index1 - index2 + 1
            Return col
        End Function
#End Region
    End Class

    Class Utility

        Public Shared Function isImageCorrupted(ByVal img As Image) As Boolean
            Dim itis As Boolean = False
            Try
                If Not ImageAnimator.CanAnimate(img) Then
                    Return itis
                End If
                Dim frames As Integer = img.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time)
                If frames <= 1 Then
                    Return itis
                End If
                Dim times As Byte() = img.GetPropertyItem(&H5100).Value
                Dim frame As Integer = 0
                While True
                    Dim dur As Integer = BitConverter.ToInt32(times, 4 * frame)
                    If System.Threading.Interlocked.Increment(frame) >= frames Then
                        Exit While
                    End If
                    img.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Time, frame)

                End While
            Catch ex As Exception
                Throw ex
            End Try
            Return itis
        End Function
    End Class

End Namespace