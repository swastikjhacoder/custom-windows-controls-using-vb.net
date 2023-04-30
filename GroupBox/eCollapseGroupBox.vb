'***************************************************************************************************|
'/<summary>
'/ The CollapseGroupBox is a container control that is used to logically group 
'/ a collection of controls. The CollapseGroupBox is added to a CollapsePanel to 
'/ provide the collapsible functionality. 
'/</summary>
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

Namespace UIControls.GroupBox

    Public Class eCollapseGroupBox
        Inherits UserControl

        Private components As Container

        Private boolResizeFromCollapse As Boolean
        Private strCaption As String
        ' size of panel when opened to full size
        Private fullSize As Size = Size

        ' collapse box in upper left corner
        Public collapseBox As eCollapseBox
        ' height of panel when collapsed
        Public Const intCollapsedHeight As Integer = 20
        Public Event CollapseBoxClickedEvent As EventHandler

        '/<summary>
        '/Caption() sets and returns the CollapseGroupBox caption
        '/</summary>
        Public Property Caption() As String
            Get
                Return strCaption
            End Get

            Set(ByVal Value As String)
                strCaption = Value
                Invalidate()
            End Set
        End Property
        '/<summary>
        '/Caption() sets and returns the full size height of the CollapseGroupBox
        '/</summary>
        Public ReadOnly Property FullHeight() As Integer
            Get
                Return fullSize.Height
            End Get
        End Property
        Public Sub New()
            MyBase.New()

            'This call is required by the Component Designer.
            InitializeComponent()

        End Sub
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub
#Region "Component Designer generated code"
        '/ <summary> 
        '/ Required method for Designer support - do not modify 
        '/ the contents of Me. method with the code editor.
        '/ </summary>

        Private Sub InitializeComponent()
            '
            'CollapseGroupBox
            '
            Me.Name = "CollapseGroupBox"
            SuspendLayout()
            '
            'CollapseBox
            '
            collapseBox = New eCollapseBox()
            Me.collapseBox.IsPlus = False
            Me.collapseBox.Location = New System.Drawing.Point(12, 1)
            Me.collapseBox.Name = "collapseBox"
            Me.collapseBox.Size = New System.Drawing.Size(13, 13)
            Me.collapseBox.TabIndex = 1
            AddHandler Me.collapseBox.Click, AddressOf CollapseBox_Click
            AddHandler Me.collapseBox.DoubleClick, AddressOf CollapseBox_DoubleClick
            '
            'CollapseGroupBox
            '
            Me.Controls.Add(collapseBox)
            Me.Name = "CollapseGroupBox"
            AddHandler Resize, AddressOf CollapseGroupBox_Resize
            AddHandler Paint, AddressOf CollapseGroupBox_Paint

            ResumeLayout(False)
        End Sub
#End Region
        Private Sub CollapseGroupBox_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)

            If boolResizeFromCollapse <> True Then
                fullSize = Size
            End If

            boolResizeFromCollapse = False

        End Sub
        Private Sub CollapseGroupBox_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

            Dim g As Graphics = e.Graphics
            Dim theRec As Rectangle = New Rectangle()
            theRec = ClientRectangle

            Dim theEdgeGrayColor As Color = Color.FromKnownColor(KnownColor.Gray)
            Dim thePen As Pen = New Pen(theEdgeGrayColor)

            Dim theTextSize As Integer = _
                CType(g.MeasureString(strCaption, Font).Width, Integer)
            If theTextSize < 1 Then
                theTextSize = 1
            End If

            Dim theCaptionPosition As Integer = _
                (theRec.X + 8) + 2 + collapseBox.Width + 2

            Dim theEndPosition As Integer = theCaptionPosition + theTextSize + 1

            ' manually draw the collapse box
            g.DrawLine(thePen, theRec.X + 8, theRec.Y + 5, theRec.X, theRec.Y + 5)
            g.DrawLine(thePen, theRec.X, theRec.Y + 5, theRec.X, theRec.Bottom - 2)
            g.DrawLine(thePen, theRec.X, theRec.Bottom - 2, theRec.Right - 1, theRec.Bottom - 2)
            g.DrawLine(thePen, theRec.Right - 2, theRec.Bottom - 2, theRec.Right - 2, theRec.Y + 5)
            g.DrawLine(thePen, theRec.Right - 2, theRec.Y + 5, theRec.X + theEndPosition, theRec.Y + 5)
            g.DrawLine(Pens.White, theRec.X + 8, theRec.Y + 6, theRec.X + 1, theRec.Y + 6)
            g.DrawLine(Pens.White, theRec.X + 1, theRec.Y + 6, theRec.X + 1, theRec.Bottom - 3)
            g.DrawLine(Pens.White, theRec.X, theRec.Bottom - 1, theRec.Right, theRec.Bottom - 1)
            g.DrawLine(Pens.White, theRec.Right - 1, theRec.Bottom - 1, theRec.Right - 1, theRec.Y + 5)
            g.DrawLine(Pens.White, theRec.Right - 3, theRec.Y + 6, theRec.X + theEndPosition, theRec.Y + 6)

            Dim sf As StringFormat = New StringFormat()
            Dim drawBrush As SolidBrush = New SolidBrush(Color.Black)

            g.DrawString(strCaption, Font, drawBrush, theCaptionPosition, 0)

        End Sub
        Private Sub CollapseBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

            ' at this point the control's value has changed but hasn't been
            ' redrawn on the screen

            boolResizeFromCollapse = True

            If collapseBox.IsPlus <> True Then
                Size = fullSize
                Invalidate()
            Else
                Dim smallSize As Size = fullSize
                smallSize.Height = intCollapsedHeight
                Size = smallSize
                Invalidate()
            End If

            RaiseEvent CollapseBoxClickedEvent(Me, e)

        End Sub
        Private Sub CollapseBox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

            ' fast clicking registers as double-clicking, so map a double-click
            ' event into a single click.
            CollapseBox_Click(sender, e)
        End Sub
        '/<summary>
        '/Minimize() reduces the CollapseGroupBox to its smallest size
        '/</summary>
        Public Sub Minimize()

            Dim smallSize As Size = fullSize

            boolResizeFromCollapse = True
            collapseBox.IsPlus = True
            smallSize.Height = intCollapsedHeight
            Size = smallSize

            Invalidate()

            RaiseEvent CollapseBoxClickedEvent(Me, New EventArgs())
        End Sub
    End Class
End Namespace