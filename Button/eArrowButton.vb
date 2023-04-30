#Region "Imports..."

Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Data
Imports System.Windows.Forms
Imports System.ComponentModel.Design

#End Region

Namespace UIControls.Button
#Region "Enums..."

    Public Enum eButtonAction As Integer
        PRESSED = &H1
        FOCUS
        MOUSEOVER
        ENABLED
    End Enum

#End Region

    <Description("Arrow Button Control")> _
    <Designer(GetType(ArrowButtonDesigner))> _
    Public Class eArrowButton
        Inherits System.Windows.Forms.Control

#Region "Designer generated code"
        ''' <summary>
        ''' Erforderliche Methode für die Designerunterstützung. 
        ''' Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        ''' </summary>
        Private Sub InitializeComponent()
            ' 
            ' ArrowButton
            ' 
            Me.Name = "eArrowButton"
            Me.Size = New System.Drawing.Size(48, 48)
            AddHandler Me.MouseUp, AddressOf Me.ArrowButton_MouseUp
            AddHandler Me.MouseEnter, AddressOf Me.ArrowButton_MouseEnter
            AddHandler Me.MouseLeave, AddressOf Me.ArrowButton_MouseLeave
            AddHandler Me.MouseDown, AddressOf Me.ArrowButton_MouseDown

        End Sub
#End Region

#Region "Events / Delegates"

        Public Delegate Sub ArrowButtonClickDelegate(ByVal sender As Object, ByVal e As EventArgs)
        Public Event OnClickEvent As ArrowButtonClickDelegate

#End Region

#Region "Members"

        Private components As System.ComponentModel.Container = Nothing
        Private m_pnts As Point() = Nothing
        ' Array with the arrow points
        Private m_CntPnt As Point
        ' Centerpoint 
        Private m_gp As New GraphicsPath()
        ' Build the arrow
        Private m_ButtonState As New BitArray()
        ' actual button state ( pressed, focus etc. )
        Private m_nRotDeg As Integer = 0
        ' Rotatin in degrees
        Private m_ColorS As Color = Color.WhiteSmoke
        ' Used start-, endcolor for the OnPaint method
        Private m_ColorE As Color = Color.DarkGray
        Private m_NormalStartColor As Color = Color.WhiteSmoke
        ' In normal state
        Private m_NormalEndColor As Color = Color.DarkGray
        Private m_HoverStartColor As Color = Color.WhiteSmoke
        ' If Mousecursor is over
        Private m_HoverEndColor As Color = Color.DarkRed

#End Region

#Region "Constants"

        Private Const MINSIZE As Integer = 24
        ' Minimum squaresize
#End Region

#Region "Properties / DesignerProperties"

        ''' <summary>
        ''' Startcolor for the GradientBrush
        ''' </summary>
        <Description("The start color"), Category("ArrowButton")> _
        Public Property NormalStartColor() As Color
            Get
                Return m_NormalStartColor
            End Get
            Set(ByVal value As Color)
                m_NormalStartColor = value
            End Set
        End Property

        ''' <summary>
        ''' Endcolor for the GradientBrush
        ''' </summary>
        <Description("The end color"), Category("ArrowButton")> _
        Public Property NormalEndColor() As Color
            Get
                Return m_NormalEndColor
            End Get
            Set(ByVal value As Color)
                m_NormalEndColor = value
                Refresh()
            End Set
        End Property

        <Description("The hover start color"), Category("ArrowButton")> _
        Public Property HoverStartColor() As Color
            Get
                Return m_HoverStartColor
            End Get
            Set(ByVal value As Color)
                m_HoverStartColor = value
                Refresh()
            End Set
        End Property

        <Description("The hover end color"), Category("ArrowButton")> _
        Public Property HoverEndColor() As Color
            Get
                Return m_HoverEndColor
            End Get
            Set(ByVal value As Color)
                m_HoverEndColor = value
                Refresh()
            End Set
        End Property

        <Description("Is arrow enabled"), Category("ArrowButton")> _
        Public Property ArrowEnabled() As Boolean
            Get
                Return m_ButtonState(CInt(eButtonAction.ENABLED))
            End Get
            Set(ByVal value As Boolean)
                m_ButtonState(CInt(eButtonAction.ENABLED)) = value
            End Set
        End Property

        <Description("Pointing direction"), Category("ArrowButton")> _
        Public Property Rotation() As Integer
            Get
                Return m_nRotDeg
            End Get
            Set(ByVal value As Integer)
                m_nRotDeg = value
                Clear()
                Init()
                Refresh()
            End Set
        End Property

#End Region

#Region "Constructors"

        Public Sub New()
            InitializeComponent()
            Init()
            ' Make the paintings faster and flickerfree
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)

            ' Inital state
            m_ButtonState(CInt(eButtonAction.PRESSED)) = False

            m_ButtonState(CInt(eButtonAction.ENABLED)) = True
        End Sub

#End Region

#Region "Overrides"

        ''' <summary>
        ''' Repaint ( recalc ) the arrow 
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            Clear()
            Init()
            Refresh()
        End Sub

        ''' <summary>
        ''' Paint the arrow.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            If m_ButtonState(CInt(eButtonAction.MOUSEOVER)) = True Then
                m_ColorS = HoverStartColor
                m_ColorE = HoverEndColor
            Else
                m_ColorS = NormalStartColor
                m_ColorE = NormalEndColor
            End If

            Dim rect As Rectangle = Me.ClientRectangle
            Dim b As New LinearGradientBrush(rect, m_ColorS, m_ColorE, 0, True)
            ' no clipping at design time
            If Me.DesignMode <> True Then
                e.Graphics.SetClip(m_gp)
            End If
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.FillPath(b, m_gp)
            b.Dispose()
            ColorArrowFrame(e, m_ButtonState)
            DrawArrowText(e.Graphics)
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' Sequence to create the button
        ''' </summary>
        Private Sub Init()
            ' Check if clientrect is a square
            MakeSquare()

            ' Make the arrow smaller than the panelwidth, because 
            ' the diagonal from arrowhead to an edge from the arrowbottom,
            ' is bigger than the panelwidth and so the edges were clipped
            ' during rotation.
            Dim dx As Integer = Me.Width - 3

            ' The arrow consist of eight points ( 0=7 )
            BuildInitialArrow(dx)

            ' Calculate the CenterPoint position
            m_CntPnt = New Point(Me.Width / 2, Me.Width / 2)

            ' Turn arrow around the CenterPoint
            RotateArrow(Rotation)

            ' Prevent clipping
            MoveCenterPoint(dx)

            ' Build the graphical path out of the arrowpoints
            GraphicsPathFromPoints(m_pnts, m_gp, m_CntPnt)

        End Sub

        ''' <summary>
        ''' To prevent clipping of the arrow edges after an 
        ''' rotation, we shift the CenterPoint. For that we 
        ''' must only check the points 0 and 1
        ''' </summary>
        Private Sub MoveCenterPoint(ByVal dx As Integer)
            ' Sector I
            If (m_nRotDeg >= 0) AndAlso (m_nRotDeg <= 90) Then
                Dim cy As Integer = (m_pnts(1).Y) - ((dx / 2))
                If cy > 0 Then
                    m_CntPnt.Y -= cy
                End If
                Dim cx As Integer = (m_pnts(0).X) + ((dx / 2))
                If cx < 0 Then
                    m_CntPnt.X -= cx
                End If
            End If

            ' Sector II
            If (m_nRotDeg >= 91) AndAlso (m_nRotDeg <= 180) Then
                Dim cy As Integer = (m_pnts(0).Y) - ((dx / 2))
                If cy > 0 Then
                    m_CntPnt.Y += cy
                End If
                Dim cx As Integer = (m_pnts(1).X) + ((dx / 2))
                If cx < 0 Then
                    m_CntPnt.X -= cx
                End If
            End If

            ' Sector III
            If (m_nRotDeg >= 181) AndAlso (m_nRotDeg <= 270) Then
                Dim cy As Integer = (m_pnts(1).Y) + ((dx / 2))
                If cy < 0 Then
                    m_CntPnt.Y -= cy
                End If
                Dim cx As Integer = (m_pnts(1).X) + ((dx / 2))
                If cx < 0 Then
                    m_CntPnt.X -= cx
                End If
            End If

            ' Sector IV
            If (m_nRotDeg >= 271) AndAlso (m_nRotDeg <= 360) Then
                Dim cy As Integer = (m_pnts(0).Y) - ((dx / 2))
                If cy > 0 Then
                    m_CntPnt.Y += cy
                End If
                Dim cx As Integer = (m_pnts(1).X) - ((dx / 2))
                If cx > 0 Then
                    m_CntPnt.X -= cx
                End If
            End If
        End Sub


        ''' <summary>
        ''' Build the startarrow. it is a upward pointing arrow.
        ''' </summary>
        ''' <param name="dx">The maximum height and width of the panel</param>
        Private Sub BuildInitialArrow(ByVal dx As Integer)
            ' The arrow consist of eight points
            m_pnts = New Point(7) {}

            ' The initial points build an arrow in up-direction
            m_pnts(0) = New Point(-dx / 4, +dx / 2)
            m_pnts(1) = New Point(+dx / 4, +dx / 2)
            m_pnts(2) = New Point(+dx / 4, 0)
            m_pnts(3) = New Point(+dx / 2, 0)
            m_pnts(4) = New Point(0, -dx / 2)
            m_pnts(5) = New Point(-dx / 2, 0)
            m_pnts(6) = New Point(-dx / 4, 0)
            m_pnts(7) = New Point(-dx / 4, +dx / 2)
        End Sub

        ''' <summary>
        ''' If the placeholder is not exact a square,
        ''' make it a square
        ''' </summary>
        Private Sub MakeSquare()
            Me.SuspendLayout()
            If Me.Width < MINSIZE Then
                Me.Size = New Size(MINSIZE, MINSIZE)
            Else
                If Me.Size.Width < Me.Size.Height Then
                    Me.Size = New Size(Me.Size.Width, Me.Size.Width)
                Else
                    Me.Size = New Size(Me.Size.Height, Me.Size.Height)
                End If
            End If
            Me.ResumeLayout()
        End Sub


        ''' <summary>
        ''' Create the arrow as a GraphicsPath from the point array
        ''' </summary>
        ''' <param name="pnts">Array with points</param>
        ''' <param name="gp">The GraphicsPath object</param>
        ''' <param name="hs">Point with offset data</param>
        Private Sub GraphicsPathFromPoints(ByVal pnts As Point(), ByVal gp As GraphicsPath, ByVal hs As Point)
            For i As Integer = 0 To pnts.Length - 2
                gp.AddLine(hs.X + pnts(i).X, hs.Y + pnts(i).Y, hs.X + pnts(i + 1).X, hs.Y + pnts(i + 1).Y)
            Next
        End Sub


        ''' <summary>
        ''' Display a the text on the arrow
        ''' </summary>
        ''' <param name="g"></param>
        Private Sub DrawArrowText(ByVal g As Graphics)
            If Text = [String].Empty Then
                Return
            End If
            Dim f As New StringFormat()
            f.Alignment = StringAlignment.Center
            f.LineAlignment = StringAlignment.Center
            Dim dx As Single = 0
            Dim dy As Single = 0
            If (m_ButtonState(CInt(eButtonAction.PRESSED))) AndAlso (m_ButtonState(CInt(eButtonAction.ENABLED)) = True) Then
                dx = 1 / g.DpiX
                dy = 1 / g.DpiY
            End If
            g.PageUnit = GraphicsUnit.Inch
            g.TranslateTransform((ClientRectangle.Width / g.DpiX) / 2, (ClientRectangle.Height / g.DpiY) / 2)
            ' to prevent that the text is not readable, add 90 degrees
            ' to turn in a readable direction ( 175 and 330 are arbitrary values )
            If (Rotation >= 175) AndAlso (Rotation <= 330) Then
                g.RotateTransform(Rotation + 90)
            Else
                g.RotateTransform(Rotation + 270)
            End If
            Dim c As Color = ForeColor
            If m_ButtonState(CInt(eButtonAction.ENABLED)) = False Then
                '.InactiveCaptionText;
                c = SystemColors.GrayText
            End If
            g.DrawString(Text, Font, New SolidBrush(c), 0 + dx, 0 + dy, f)
        End Sub

        ''' <summary>
        ''' Simply clear the points and reset the graphicpath
        ''' </summary>
        Private Sub Clear()
            m_pnts = Nothing
            m_gp.Reset()
        End Sub

        Private Sub ColorArrowFrame(ByVal e As PaintEventArgs, ByVal butstate As BitArray)
            If m_ButtonState(CInt(eButtonAction.ENABLED)) Then
                Dim p1 As Pen = Nothing
                If butstate(CInt(eButtonAction.PRESSED)) = False Then
                    For i As Integer = 0 To 6
                        If m_pnts(i).Y <= m_pnts(i + 1).Y Then
                            p1 = New Pen(SystemColors.ControlLightLight, 2)
                        Else
                            p1 = New Pen(SystemColors.ControlDark, 2)
                        End If
                        e.Graphics.DrawLine(p1, m_CntPnt.X + m_pnts(i).X, m_CntPnt.Y + m_pnts(i).Y, m_CntPnt.X + m_pnts(i + 1).X, m_CntPnt.Y + m_pnts(i + 1).Y)
                    Next
                End If

                If butstate(CInt(eButtonAction.PRESSED)) = True Then
                    For i As Integer = 0 To 6
                        If m_pnts(i).Y <= m_pnts(i + 1).Y Then
                            p1 = New Pen(SystemColors.ControlDark, 2)
                        Else
                            p1 = New Pen(SystemColors.ControlLightLight, 2)
                        End If
                        e.Graphics.DrawLine(p1, m_CntPnt.X + m_pnts(i).X, m_CntPnt.Y + m_pnts(i).Y, m_CntPnt.X + m_pnts(i + 1).X, m_CntPnt.Y + m_pnts(i + 1).Y)
                    Next
                End If
            End If
        End Sub

        ''' <summary>
        ''' Rotate the arrow around the CenterPoint to get different 
        ''' pointing directions.
        ''' </summary>
        ''' <param name="nDeg">Rotation in degree</param>
        Private Sub RotateArrow(ByVal nDeg As Integer)
            ' only values between 0 and 360
            If nDeg > 360 Then
                nDeg -= 360
            End If

            m_nRotDeg = nDeg
            Dim bog As Double = (Math.PI / 180) * nDeg
            Dim cosA As Double = Math.Cos(bog)
            Dim sinA As Double = Math.Sin(bog)

            For i As Integer = 0 To 7
                Dim a As Integer = m_pnts(i).X
                Dim b As Integer = m_pnts(i).Y

                Dim x As Double = (a * cosA) - (b * sinA)
                Dim y As Double = (b * cosA) + (a * sinA)

                m_pnts(i).X = CInt(x)
                m_pnts(i).Y = CInt(y)
            Next
        End Sub


#End Region

#Region "Mouseactions"

        Private Sub ArrowButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            m_ButtonState(CInt(eButtonAction.PRESSED)) = False
            Refresh()
        End Sub

        Private Sub ArrowButton_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
            m_ButtonState(CInt(eButtonAction.MOUSEOVER)) = False
            Refresh()
        End Sub

        Private Sub ArrowButton_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
            m_ButtonState(CInt(eButtonAction.MOUSEOVER)) = True
            Refresh()
        End Sub

        Private Sub ArrowButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            m_ButtonState(CInt(eButtonAction.PRESSED)) = True
            Refresh()
            OnArrowClick(e)
        End Sub


#End Region

#Region "EventHandler function"

        Protected Sub OnArrowClick(ByVal e As EventArgs)
            RaiseEvent OnClickEvent(Me, e)
        End Sub

#End Region

#Region "Disposing"

        ''' <summary>
        ''' Clear the used resources
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#End Region

    End Class

#Region "Class Bitarray"

    Public Class BitArray
#Region "Constants"

        Const NUMBITS As Integer = 32
        ' Bits = int = 4 byte = 32 bits
#End Region

#Region "Members"

        Private m_Bits As Integer

#End Region

#Region "Indexer"

        ''' <summary>
        ''' The Indexer for a 32 bit array
        ''' </summary>
        Default Public Property Item(ByVal BitPos As Integer) As Boolean
            Get
                BitPosValid(BitPos)
                Return ((m_Bits And (1 << (BitPos Mod 8))) <> 0)
            End Get
            Set(ByVal value As Boolean)
                BitPosValid(BitPos)
                ' Set the bit to 1
                If value Then
                    m_Bits = m_Bits Or (1 << (BitPos Mod 8))
                Else
                    ' Set the bit to 0
                    m_Bits = m_Bits And Not (1 << (BitPos Mod 8))
                End If
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Check if the wanted bit is in a valid Range
        ''' </summary>
        ''' <param name="BitPos"></param>
        Private Sub BitPosValid(ByVal BitPos As Integer)
            If (BitPos < 0) OrElse (BitPos >= NUMBITS) Then
                Throw New ArgumentOutOfRangeException()
            End If
        End Sub

        ''' <summary>
        ''' Clear all bits in the "bitarray"
        ''' </summary>
        Public Sub Clear()
            m_Bits = &H0
        End Sub

#End Region
    End Class

#End Region

#Region "Class ArrowButtonDesigner"

    Public Class ArrowButtonDesigner
        Inherits System.Windows.Forms.Design.ControlDesigner
#Region "Constructors"

        Public Sub New()
        End Sub

#End Region

#Region "Overrides"

        Protected Overrides Sub PostFilterProperties(ByVal Properties As IDictionary)
            Properties.Remove("AllowDrop")
            Properties.Remove("BackColor")
            Properties.Remove("BackgroundImage")
            Properties.Remove("ContextMenu")
            Properties.Remove("FlatStyle")
            Properties.Remove("Image")
            Properties.Remove("ImageAlign")
            Properties.Remove("ImageIndex")
            Properties.Remove("ImageList")
            Properties.Remove("TextAlign")
            Properties.Remove("Enabled")
        End Sub

#End Region
    End Class

#End Region
End Namespace