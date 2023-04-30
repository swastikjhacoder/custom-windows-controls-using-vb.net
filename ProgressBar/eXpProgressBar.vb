Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Namespace UIControls.ProgressBar

#Region "  Gradient Mode  "

    Public Enum GradientMode
        Vertical
        VerticalCenter
        Horizontal
        HorizontalCenter
        Diagonal
    End Enum

#End Region

    <ToolboxBitmap(GetType(eVistaProgressBar), "Resources.progress_bar.bmp")> _
    Public Class eXpProgressBar
        Inherits Control
#Region "  Constructor  "

        Private Const CategoryName As String = "Xp ProgressBar"

        Public Sub New()
            InitializeComponent()
        End Sub

#End Region

#Region "  Private Fields  "

        Private mColor1 As Color = Color.FromArgb(170, 240, 170)

        Private mColor2 As Color = Color.FromArgb(10, 150, 10)

        Private mColorBackGround As Color = Color.White

        Private mColorText As Color = Color.Black

        Private mDobleBack As Image = Nothing

        Private mGradientStyle As GradientMode = GradientMode.VerticalCenter

        Private mMax As Integer = 100

        Private mMin As Integer = 0

        Private mPosition As Integer = 50

        Private mSteepDistance As Byte = 2

        Private mSteepWidth As Byte = 6

#End Region

#Region "  Dispose  "

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not Me.IsDisposed Then
                If mDobleBack IsNot Nothing Then
                    mDobleBack.Dispose()
                End If
                If mBrush1 IsNot Nothing Then
                    mBrush1.Dispose()
                End If

                If mBrush2 IsNot Nothing Then
                    mBrush2.Dispose()
                End If

                MyBase.Dispose(disposing)
            End If
        End Sub

#End Region

#Region "  Colors   "

        <Category(CategoryName)> _
        <Description("The Back Color of the Progress Bar")> _
        Public Property ColorBackGround() As Color
            Get
                Return mColorBackGround
            End Get
            Set(ByVal value As Color)
                mColorBackGround = value
                Me.InvalidateBuffer(True)
            End Set
        End Property

        <Category(CategoryName)> _
        <Description("The Border Color of the gradient in the Progress Bar")> _
        Public Property ColorBarBorder() As Color
            Get
                Return mColor1
            End Get
            Set(ByVal value As Color)
                mColor1 = value
                Me.InvalidateBuffer(True)
            End Set
        End Property

        <Category(CategoryName)> _
        <Description("The Center Color of the gradient in the Progress Bar")> _
        Public Property ColorBarCenter() As Color
            Get
                Return mColor2
            End Get
            Set(ByVal value As Color)
                mColor2 = value
                Me.InvalidateBuffer(True)
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <Description("Set to TRUE to reset all colors like the Windows XP Progress Bar ®")> _
        <Category(CategoryName)> _
        <DefaultValue(False)> _
        Public Property ColorsXP() As Boolean
            Get
                Return False
            End Get
            Set(ByVal value As Boolean)
                ColorBarBorder = Color.FromArgb(170, 240, 170)
                ColorBarCenter = Color.FromArgb(10, 150, 10)
                ColorBackGround = Color.White
            End Set
        End Property

        <Category(CategoryName)> _
        <Description("The Color of the text displayed in the Progress Bar")> _
        Public Property ColorText() As Color
            Get
                Return mColorText
            End Get
            Set(ByVal value As Color)
                mColorText = value

                If Me.Text <> [String].Empty Then
                    Me.Invalidate()
                End If
            End Set
        End Property

#End Region

#Region "  Position   "

        <RefreshProperties(RefreshProperties.Repaint)> _
        <Category(CategoryName)> _
        <Description("The Current Position of the Progress Bar")> _
        Public Property Position() As Integer
            Get
                Return mPosition
            End Get
            Set(ByVal value As Integer)
                If value > mMax Then
                    mPosition = mMax
                ElseIf value < mMin Then
                    mPosition = mMin
                Else
                    mPosition = value
                End If
                Me.Invalidate()
            End Set
        End Property

        <RefreshProperties(RefreshProperties.Repaint)> _
        <Category(CategoryName)> _
        <Description("The Max Position of the Progress Bar")> _
        Public Property PositionMax() As Integer
            Get
                Return mMax
            End Get
            Set(ByVal value As Integer)
                If value > mMin Then
                    mMax = value

                    If mPosition > mMax Then
                        Position = mMax
                    End If

                    Me.InvalidateBuffer(True)
                End If
            End Set
        End Property

        <RefreshProperties(RefreshProperties.Repaint)> _
        <Category(CategoryName)> _
        <Description("The Min Position of the Progress Bar")> _
        Public Property PositionMin() As Integer
            Get
                Return mMin
            End Get
            Set(ByVal value As Integer)
                If value < mMax Then
                    mMin = value

                    If mPosition < mMin Then
                        Position = mMin
                    End If
                    Me.InvalidateBuffer(True)
                End If
            End Set
        End Property

        <Category(CategoryName)> _
        <Description("The number of Pixels between two Steeps in Progress Bar")> _
        <DefaultValue(CByte(2))> _
        Public Property SteepDistance() As Byte
            Get
                Return mSteepDistance
            End Get
            Set(ByVal value As Byte)
                If value >= 0 Then
                    mSteepDistance = value
                    Me.InvalidateBuffer(True)
                End If
            End Set
        End Property

#End Region

#Region "  Progress Style   "

        <Category(CategoryName)> _
        <Description("The Style of the gradient bar in Progress Bar")> _
        <DefaultValue(GradientMode.VerticalCenter)> _
        Public Property GradientStyle() As GradientMode
            Get
                Return mGradientStyle
            End Get
            Set(ByVal value As GradientMode)
                If mGradientStyle <> value Then
                    mGradientStyle = value
                    CreatePaintElements()
                    Me.Invalidate()
                End If
            End Set
        End Property

        <Category(CategoryName)> _
        <Description("The number of Pixels of the Steeps in Progress Bar")> _
        <DefaultValue(CByte(6))> _
        Public Property SteepWidth() As Byte
            Get
                Return mSteepWidth
            End Get
            Set(ByVal value As Byte)
                If value > 0 Then
                    mSteepWidth = value
                    Me.InvalidateBuffer(True)
                End If
            End Set
        End Property

#End Region

#Region "  BackImage  "

        <RefreshProperties(RefreshProperties.Repaint)> _
        <Category(CategoryName)> _
        Public Overrides Property BackgroundImage() As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(ByVal value As Image)
                MyBase.BackgroundImage = value
                InvalidateBuffer()
            End Set
        End Property

#End Region

#Region "  Text Override  "

        <Category(CategoryName)> _
        <Description("The Text displayed in the Progress Bar")> _
        <DefaultValue("")> _
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                If MyBase.Text <> value Then
                    MyBase.Text = value
                    Me.Invalidate()
                End If
            End Set
        End Property

#End Region

#Region "  Text Shadow  "

        Private mTextShadow As Boolean = True

        <Category(CategoryName)> _
        <Description("Set the Text shadow in the Progress Bar")> _
        <DefaultValue(True)> _
        Public Property TextShadow() As Boolean
            Get
                Return mTextShadow
            End Get
            Set(ByVal value As Boolean)
                mTextShadow = value
                Me.Invalidate()
            End Set
        End Property

#End Region

#Region "  Text Shadow Alpha  "

        Private mTextShadowAlpha As Byte = 150

        <Category(CategoryName)> _
        <Description("Set the Alpha Channel of the Text shadow in the Progress Bar")> _
        <DefaultValue(CByte(150))> _
        Public Property TextShadowAlpha() As Byte
            Get
                Return mTextShadowAlpha
            End Get
            Set(ByVal value As Byte)
                If mTextShadowAlpha <> value Then
                    mTextShadowAlpha = value
                    Me.TextShadow = True
                End If
            End Set
        End Property

#End Region

#Region "  Paint Methods  "

#Region "  OnPaint  "

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            'System.Diagnostics.Debug.WriteLine("Paint " + this.Name + "  Pos: "+this.Position.ToString());
            If Not Me.IsDisposed Then
                Dim mSteepTotal As Integer = mSteepWidth + mSteepDistance
                Dim mUtilWidth As Single = Me.Width - 6 + mSteepDistance

                If mDobleBack Is Nothing Then
                    mUtilWidth = Me.Width - 6 + mSteepDistance
                    Dim mMaxSteeps As Integer = CInt(mUtilWidth / mSteepTotal)
                    Me.Width = 6 + mSteepTotal * mMaxSteeps

                    mDobleBack = New Bitmap(Me.Width, Me.Height)

                    Dim g2 As Graphics = Graphics.FromImage(mDobleBack)

                    CreatePaintElements()

                    g2.Clear(mColorBackGround)

                    If Me.BackgroundImage IsNot Nothing Then
                        Dim textuBrush As New TextureBrush(Me.BackgroundImage, WrapMode.Tile)
                        g2.FillRectangle(textuBrush, 0, 0, Me.Width, Me.Height)
                        textuBrush.Dispose()
                    End If
                    '				g2.DrawImage()

                    g2.DrawRectangle(mPenOut2, outnnerRect2)
                    g2.DrawRectangle(mPenOut, outnnerRect)
                    g2.DrawRectangle(mPenIn, innerRect)

                    g2.Dispose()
                End If

                Dim ima As Image = New Bitmap(mDobleBack)

                Dim gtemp As Graphics = Graphics.FromImage(ima)

                Dim mCantSteeps As Integer = CInt(((CSng(mPosition) - mMin) / (mMax - mMin)) * mUtilWidth / mSteepTotal)

                For i As Integer = 0 To mCantSteeps - 1
                    DrawSteep(gtemp, i)
                Next

                If Me.Text <> [String].Empty Then
                    gtemp.TextRenderingHint = TextRenderingHint.AntiAlias
                    DrawCenterString(gtemp, Me.ClientRectangle)
                End If

                e.Graphics.DrawImage(ima, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle, GraphicsUnit.Pixel)
                ima.Dispose()

                gtemp.Dispose()
            End If

        End Sub

        Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        End Sub

#End Region

#Region "  OnSizeChange  "

        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            If Not Me.IsDisposed Then
                If Me.Height < 12 Then
                    Me.Height = 12
                End If

                MyBase.OnSizeChanged(e)
                Me.InvalidateBuffer(True)
            End If

        End Sub

        Protected Overrides ReadOnly Property DefaultSize() As Size
            Get
                Return New Size(100, 29)
            End Get
        End Property


#End Region

#Region "  More Draw Methods  "

        Private Sub DrawSteep(ByVal g As Graphics, ByVal number As Integer)
            g.FillRectangle(mBrush1, 4 + number * (mSteepDistance + mSteepWidth), mSteepRect1.Y + 1, mSteepWidth, mSteepRect1.Height)
            g.FillRectangle(mBrush2, 4 + number * (mSteepDistance + mSteepWidth), mSteepRect2.Y + 1, mSteepWidth, mSteepRect2.Height - 1)
        End Sub

        Private Sub InvalidateBuffer()
            InvalidateBuffer(False)
        End Sub

        Private Sub InvalidateBuffer(ByVal InvalidateControl As Boolean)
            If mDobleBack IsNot Nothing Then
                mDobleBack.Dispose()
                mDobleBack = Nothing
            End If

            If InvalidateControl Then
                Me.Invalidate()
            End If
        End Sub

        Private Sub DisposeBrushes()
            If mBrush1 IsNot Nothing Then
                mBrush1.Dispose()
                mBrush1 = Nothing
            End If

            If mBrush2 IsNot Nothing Then
                mBrush2.Dispose()
                mBrush2 = Nothing
            End If

        End Sub

        Private Sub DrawCenterString(ByVal gfx As Graphics, ByVal box As Rectangle)
            Dim ss As SizeF = gfx.MeasureString(Me.Text, Me.Font)

            Dim left As Single = box.X + (box.Width - ss.Width) / 2
            Dim top As Single = box.Y + (box.Height - ss.Height) / 2

            If mTextShadow Then
                Dim mShadowBrush As New SolidBrush(Color.FromArgb(mTextShadowAlpha, Color.Black))
                gfx.DrawString(Me.Text, Me.Font, mShadowBrush, left + 1, top + 1)
                mShadowBrush.Dispose()
            End If
            Dim mTextBrush As New SolidBrush(mColorText)
            gfx.DrawString(Me.Text, Me.Font, mTextBrush, left, top)
            mTextBrush.Dispose()

        End Sub

#End Region

#Region "  CreatePaintElements   "

        Private innerRect As Rectangle
        Private mBrush1 As LinearGradientBrush
        Private mBrush2 As LinearGradientBrush
        Private mPenIn As New Pen(Color.FromArgb(239, 239, 239))

        Private mPenOut As New Pen(Color.FromArgb(104, 104, 104))
        Private mPenOut2 As New Pen(Color.FromArgb(190, 190, 190))

        Private mSteepRect1 As Rectangle
        Private mSteepRect2 As Rectangle
        Private outnnerRect As Rectangle
        Private outnnerRect2 As Rectangle

        Private Sub CreatePaintElements()
            DisposeBrushes()

            Select Case mGradientStyle
                Case GradientMode.VerticalCenter

                    mSteepRect1 = New Rectangle(0, 2, mSteepWidth, Me.Height / 2 + CInt(Me.Height * 0.05))
                    mBrush1 = New LinearGradientBrush(mSteepRect1, mColor1, mColor2, LinearGradientMode.Vertical)

                    mSteepRect2 = New Rectangle(0, mSteepRect1.Bottom - 1, mSteepWidth, Me.Height - mSteepRect1.Height - 4)
                    mBrush2 = New LinearGradientBrush(mSteepRect2, mColor2, mColor1, LinearGradientMode.Vertical)
                    Exit Select

                Case GradientMode.Vertical
                    mSteepRect1 = New Rectangle(0, 3, mSteepWidth, Me.Height - 7)
                    mBrush1 = New LinearGradientBrush(mSteepRect1, mColor1, mColor2, LinearGradientMode.Vertical)
                    mSteepRect2 = New Rectangle(-100, -100, 1, 1)
                    mBrush2 = New LinearGradientBrush(mSteepRect2, mColor2, mColor1, LinearGradientMode.Horizontal)
                    Exit Select


                Case GradientMode.Horizontal
                    mSteepRect1 = New Rectangle(0, 3, mSteepWidth, Me.Height - 7)

                    '					mBrush1 = new LinearGradientBrush(rTemp, mColor1, mColor2, LinearGradientMode.Horizontal);
                    mBrush1 = New LinearGradientBrush(Me.ClientRectangle, mColor1, mColor2, LinearGradientMode.Horizontal)
                    mSteepRect2 = New Rectangle(-100, -100, 1, 1)
                    mBrush2 = New LinearGradientBrush(mSteepRect2, Color.Red, Color.Red, LinearGradientMode.Horizontal)
                    Exit Select


                Case GradientMode.HorizontalCenter
                    mSteepRect1 = New Rectangle(0, 3, mSteepWidth, Me.Height - 7)
                    '					mBrush1 = new LinearGradientBrush(rTemp, mColor1, mColor2, LinearGradientMode.Horizontal);
                    mBrush1 = New LinearGradientBrush(Me.ClientRectangle, mColor1, mColor2, LinearGradientMode.Horizontal)
                    mBrush1.SetBlendTriangularShape(0.5F)

                    mSteepRect2 = New Rectangle(-100, -100, 1, 1)
                    mBrush2 = New LinearGradientBrush(mSteepRect2, Color.Red, Color.Red, LinearGradientMode.Horizontal)
                    Exit Select


                Case GradientMode.Diagonal
                    mSteepRect1 = New Rectangle(0, 3, mSteepWidth, Me.Height - 7)
                    '					mBrush1 = new LinearGradientBrush(rTemp, mColor1, mColor2, LinearGradientMode.ForwardDiagonal);
                    mBrush1 = New LinearGradientBrush(Me.ClientRectangle, mColor1, mColor2, LinearGradientMode.ForwardDiagonal)
                    '					((LinearGradientBrush) mBrush1).SetBlendTriangularShape(0.5f);

                    mSteepRect2 = New Rectangle(-100, -100, 1, 1)
                    mBrush2 = New LinearGradientBrush(mSteepRect2, Color.Red, Color.Red, LinearGradientMode.Horizontal)
                    Exit Select
                Case Else

                    mBrush1 = New LinearGradientBrush(mSteepRect1, mColor1, mColor2, LinearGradientMode.Vertical)
                    mBrush2 = New LinearGradientBrush(mSteepRect2, mColor2, mColor1, LinearGradientMode.Vertical)
                    Exit Select

            End Select

            innerRect = New Rectangle(Me.ClientRectangle.X + 2, Me.ClientRectangle.Y + 2, Me.ClientRectangle.Width - 4, Me.ClientRectangle.Height - 4)
            outnnerRect = New Rectangle(Me.ClientRectangle.X, Me.ClientRectangle.Y, Me.ClientRectangle.Width - 1, Me.ClientRectangle.Height - 1)
            outnnerRect2 = New Rectangle(Me.ClientRectangle.X + 1, Me.ClientRectangle.Y + 1, Me.ClientRectangle.Width, Me.ClientRectangle.Height)

        End Sub

#End Region

#End Region
    End Class

End Namespace