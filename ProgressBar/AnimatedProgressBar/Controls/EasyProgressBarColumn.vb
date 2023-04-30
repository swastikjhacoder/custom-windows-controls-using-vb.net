
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Namespace UIControls.ProgressBar
    Public Class EasyProgressBarColumn
        Inherits DataGridViewColumn
#Region "Instance Members"

        Private m_minimum As Integer = 0
        ' Initializer
        Private m_maximum As Integer = 100
        ' Initializer
        Private m_isPaintBorder As Boolean = True
        ' Initializer
        Private m_showPercentage As Boolean = True
        ' Initializer
        Private m_borderColor As Color = Color.DarkGray
        ' Initializer
        Private m_controlBorderStyle As EasyProgressBarBorderStyle = EasyProgressBarBorderStyle.Flat
        ' Initializer
        Private m_hoverGradient As GradientHover
        Private m_progressGradient As GradientProgress
        Private m_backgroundGradient As GradientBackground
        Private m_progressColorizer As ColorizerProgress

#End Region

#Region "Constructor"

        Public Sub New()
            MyBase.New(New EasyProgressBarCell())
            m_hoverGradient = New GradientHover()
            AddHandler m_hoverGradient.GradientChanged, AddressOf COLUMN_INVALIDATE

            m_progressGradient = New GradientProgress()
            AddHandler m_progressGradient.GradientChanged, AddressOf COLUMN_INVALIDATE

            m_backgroundGradient = New GradientBackground()
            AddHandler m_backgroundGradient.GradientChanged, AddressOf COLUMN_INVALIDATE

            m_progressColorizer = New ColorizerProgress()
            AddHandler m_progressColorizer.ProgressColorizerChanged, AddressOf COLUMN_INVALIDATE
        End Sub

#End Region

#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Property"

        ' Override CellTemplate Property 

        Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)
                ' Ensure that the cell used for the template is an EasyProgressBarCell.
                If value IsNot Nothing AndAlso Not value.[GetType]().IsAssignableFrom(GetType(EasyProgressBarCell)) Then
                    Throw New InvalidCastException("The cell template must be an EasyProgressBarCell.")
                End If

                MyBase.CellTemplate = value
            End Set
        End Property

        ''' <summary>
        ''' You can change the hover cell gradient appearance from here.
        ''' </summary>
        <Description("You can change the hover cell gradient appearance from here")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        Public Property HoverGradient() As GradientHover
            Get
                Return m_hoverGradient
            End Get
            Set(ByVal value As GradientHover)
                Try
                    If Not value.Equals(m_hoverGradient) Then
                        RemoveHandler m_hoverGradient.GradientChanged, AddressOf COLUMN_INVALIDATE


                        m_hoverGradient = value
                        AddHandler m_hoverGradient.GradientChanged, AddressOf COLUMN_INVALIDATE

                        If MyBase.DataGridView IsNot Nothing Then
                            MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' You can change the progress appearance from here.
        ''' </summary>
        <Description("You can change the progress appearance from here")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        Public Property ProgressGradient() As GradientProgress
            Get
                Return m_progressGradient
            End Get
            Set(ByVal value As GradientProgress)
                Try
                    If Not value.Equals(m_progressGradient) Then
                        RemoveHandler m_progressGradient.GradientChanged, AddressOf COLUMN_INVALIDATE


                        m_progressGradient = value
                        AddHandler m_progressGradient.GradientChanged, AddressOf COLUMN_INVALIDATE

                        If MyBase.DataGridView IsNot Nothing Then
                            MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' You can change the background appearance from here.
        ''' </summary>
        <Description("You can change the background appearance from here")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        Public Property BackgroundGradient() As GradientBackground
            Get
                Return m_backgroundGradient
            End Get
            Set(ByVal value As GradientBackground)
                Try
                    If Not value.Equals(m_backgroundGradient) Then
                        RemoveHandler m_backgroundGradient.GradientChanged, AddressOf COLUMN_INVALIDATE


                        m_backgroundGradient = value
                        AddHandler m_backgroundGradient.GradientChanged, AddressOf COLUMN_INVALIDATE

                        If MyBase.DataGridView IsNot Nothing Then
                            MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' You can change the color components of the progress bitmap[RGBA Colorizer for progress indicator].
        ''' </summary>
        <Description("You can change the color components of the progress bitmap[RGBA Colorizer for progress indicator]")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Browsable(True)> _
        <Category("Gradient")> _
        Public Property ProgressColorizer() As ColorizerProgress
            Get
                Return m_progressColorizer
            End Get
            Set(ByVal value As ColorizerProgress)
                Try
                    If Not value.Equals(m_progressColorizer) Then
                        RemoveHandler m_progressColorizer.ProgressColorizerChanged, AddressOf COLUMN_INVALIDATE


                        m_progressColorizer = value
                        AddHandler m_progressColorizer.ProgressColorizerChanged, AddressOf COLUMN_INVALIDATE

                        If MyBase.DataGridView IsNot Nothing Then
                            MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    MessageBox.Show("Value cannot be null!, please enter a valid value.")
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the minimum progress value of the control.
        ''' </summary>
        <Description("Gets or Sets, the minimum progress value of the control")> _
        <DefaultValue(0)> _
        <Browsable(True)> _
        <Category("Progress")> _
        Public Property Minimum() As Integer
            Get
                Return m_minimum
            End Get
            Set(ByVal value As Integer)
                If Not value.Equals(m_minimum) Then
                    If value >= m_maximum Then
                        Throw New ArgumentException("Minimum must be smaller than Maximum.")
                    ElseIf value < 0 Then
                        Throw New ArgumentException("Minimum must be positive integer.")
                    End If

                    m_minimum = value

                    If MyBase.DataGridView IsNot Nothing Then
                        MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the maximum progress value of the control.
        ''' </summary>
        <Description("Gets or Sets, the maximum progress value of the control")> _
        <DefaultValue(100)> _
        <Browsable(True)> _
        <Category("Progress")> _
        Public Property Maximum() As Integer
            Get
                Return m_maximum
            End Get
            Set(ByVal value As Integer)
                If Not value.Equals(m_maximum) Then
                    If value <= m_minimum Then
                        Throw New ArgumentException("Maximum must be greater than Minimum.")
                    End If

                    m_maximum = value

                    If MyBase.DataGridView IsNot Nothing Then
                        MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the control's border is draw or not.
        ''' </summary>
        <Description("Determines whether the control's border is draw or not")> _
        <DefaultValue(True)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        Public Property IsPaintBorder() As Boolean
            Get
                Return m_isPaintBorder
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(m_isPaintBorder) Then
                    m_isPaintBorder = value

                    If MyBase.DataGridView IsNot Nothing Then
                        MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the percentage text is show or hide.
        ''' </summary>
        <Description("Determines whether the percentage text is show or hide")> _
        <DefaultValue(True)> _
        <Browsable(True)> _
        <Category("Appearance")> _
        Public Property ShowPercentage() As Boolean
            Get
                Return m_showPercentage
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(m_showPercentage) Then
                    m_showPercentage = value

                    If MyBase.DataGridView IsNot Nothing Then
                        MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the control's border color from here.
        ''' </summary>
        <Description("Gets or Sets, the control's border color from here")> _
        <DefaultValue(GetType(Color), "DarkGray")> _
        <Browsable(True)> _
        <Category("Appearance")> _
        Public Property BorderColor() As System.Drawing.Color
            Get
                Return m_borderColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                If Not value.Equals(m_borderColor) Then
                    m_borderColor = value

                    If m_isPaintBorder Then
                        If MyBase.DataGridView IsNot Nothing Then
                            MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                        End If
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the current border style of the ProgressBar cell.
        ''' </summary>
        <Description("Gets or Sets, the current border style of the ProgressBar cell")> _
        <DefaultValue(GetType(EasyProgressBarBorderStyle), "Flat")> _
        <Browsable(True)> _
        <Category("Appearance")> _
        Public Property ControlBorderStyle() As EasyProgressBarBorderStyle
            Get
                Return m_controlBorderStyle
            End Get
            Set(ByVal value As EasyProgressBarBorderStyle)
                If Not value.Equals(m_controlBorderStyle) Then
                    m_controlBorderStyle = value

                    If m_isPaintBorder Then
                        If MyBase.DataGridView IsNot Nothing Then
                            MyBase.DataGridView.InvalidateColumn(MyBase.Index)
                        End If
                    End If
                End If
            End Set
        End Property

#End Region

#Region "Helper Methods"

        Private Sub COLUMN_INVALIDATE(ByVal sender As Object, ByVal e As EventArgs)
            If MyBase.DataGridView IsNot Nothing Then
                MyBase.DataGridView.InvalidateColumn(MyBase.Index)
            End If
        End Sub

#End Region

#Region "Override Methods"

        Public Overrides Function Clone() As Object
            Dim progressColumn As EasyProgressBarColumn = TryCast(MyBase.Clone(), EasyProgressBarColumn)
            progressColumn.Minimum = m_minimum
            progressColumn.Maximum = m_maximum
            progressColumn.IsPaintBorder = m_isPaintBorder
            progressColumn.ShowPercentage = m_showPercentage
            progressColumn.BorderColor = m_borderColor
            progressColumn.ControlBorderStyle = m_controlBorderStyle
            progressColumn.HoverGradient = m_hoverGradient
            progressColumn.ProgressGradient = m_progressGradient
            progressColumn.BackgroundGradient = m_backgroundGradient
            progressColumn.ProgressColorizer = m_progressColorizer

            Return progressColumn
        End Function

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                RemoveHandler m_hoverGradient.GradientChanged, AddressOf COLUMN_INVALIDATE
                m_hoverGradient.Dispose()

                RemoveHandler m_progressGradient.GradientChanged, AddressOf COLUMN_INVALIDATE
                m_progressGradient.Dispose()

                RemoveHandler m_backgroundGradient.GradientChanged, AddressOf COLUMN_INVALIDATE
                m_backgroundGradient.Dispose()

                RemoveHandler m_progressColorizer.ProgressColorizerChanged, AddressOf COLUMN_INVALIDATE
                m_progressColorizer.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#End Region
    End Class

    Public Class EasyProgressBarCell
        Inherits DataGridViewTextBoxCell
#Region "Static Members Of The Class"

        Private Shared hoverCellIndex As Integer = -1

#End Region

#Region "Instance Members"

        Private borderRectangle As Rectangle
        Private progressRectangle As Rectangle
        Private backgroundRectangle As Rectangle
        Private m_progressColumn As EasyProgressBarColumn

#End Region

#Region "Constructor"

        Public Sub New()
            MyBase.New()
        End Sub

#End Region

        Public ReadOnly Property ProgressColumn() As EasyProgressBarColumn
            Get
                If m_progressColumn Is Nothing Then
                    ' Instantiate our progress column instance from base.OwningColumn property.
                    m_progressColumn = TryCast(MyBase.OwningColumn, EasyProgressBarColumn)
                End If

                Return m_progressColumn
            End Get
        End Property

#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Override Property"

        Public Overrides ReadOnly Property EditType() As Type
            Get
                ' Return the type of the default editing contol that EasyProgressBarCell uses.
                Return GetType(DataGridViewTextBoxEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property ValueType() As Type
            Get
                ' Return the type of the value that EasyProgressBarCell contains.
                Return GetType(Integer)
            End Get
        End Property

        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            Get
                ' The default value is zero.
                Return 0
            End Get
        End Property

#End Region

#Region "Helper Methods"

        Private Sub GenerateProgressBar(ByVal cellBounds As Rectangle)
            progressRectangle = New Rectangle(Point.Empty, cellBounds.Size)

            borderRectangle = cellBounds

            ' ProgressBar Draw Area 

            borderRectangle.Inflate(-4, -4)
            progressRectangle.Height -= 8

            ' Set Background Rectangle 

            backgroundRectangle = borderRectangle

            If ProgressColumn.IsPaintBorder Then
                progressRectangle.Inflate(-1, -1)
                backgroundRectangle.Inflate(-1, -1)
            End If

            Dim progressWidth As Integer, currentProgress As Integer = CInt(Value)

            If CInt(Value) >= ProgressColumn.Maximum Then
                progressWidth = backgroundRectangle.Width
            Else
                progressWidth = backgroundRectangle.Width * currentProgress / ProgressColumn.Maximum
            End If

            progressRectangle.Width = progressWidth
            progressRectangle.Height -= 1
        End Sub

#End Region

#Region "Virtual Methods"

        Protected Overridable Sub DrawBorder(ByVal gr As Graphics)
            If Not ProgressColumn.IsPaintBorder Then
                Return
            End If

            Dim topLeft As Pen
            Dim bottomRight As Pen

            Select Case ProgressColumn.ControlBorderStyle
                Case EasyProgressBarBorderStyle.Raised
                    topLeft = New Pen(New SolidBrush(SystemColors.ControlLightLight))
                    bottomRight = New Pen(New SolidBrush(SystemColors.ControlDark))
                    Exit Select
                Case EasyProgressBarBorderStyle.Sunken
                    topLeft = New Pen(New SolidBrush(SystemColors.ControlDark))
                    bottomRight = New Pen(New SolidBrush(SystemColors.ControlLightLight))
                    Exit Select
                Case Else
                    topLeft = New Pen(ProgressColumn.BorderColor)
                    bottomRight = topLeft
                    Exit Select
            End Select

            gr.DrawLine(topLeft, borderRectangle.Left, borderRectangle.Top, borderRectangle.Right - 1, borderRectangle.Top)
            ' Top
            gr.DrawLine(topLeft, borderRectangle.Left, borderRectangle.Top, borderRectangle.Left, borderRectangle.Bottom - 1)
            ' Left
            gr.DrawLine(bottomRight, borderRectangle.Left, borderRectangle.Bottom - 1, borderRectangle.Right - 1, borderRectangle.Bottom - 1)
            ' Bottom
            gr.DrawLine(bottomRight, borderRectangle.Right - 1, borderRectangle.Top, borderRectangle.Right - 1, borderRectangle.Bottom - 1)
            ' Right
            topLeft.Dispose()
            bottomRight.Dispose()
        End Sub

        Protected Overridable Sub DrawProgress(ByVal gr As Graphics)
            If progressRectangle.Width < 2 Then
                Return
            End If

            Dim left As New Point(progressRectangle.X, progressRectangle.Y)
            Dim right As New Point(progressRectangle.Right, progressRectangle.Y)

            ' Create a new empty image for manipulations. If you use this constructor, you get a new Bitmap object that represents a bitmap in memory with a PixelFormat of Format32bppARGB.
            Using overlay As New Bitmap(progressRectangle.Width + 2, progressRectangle.Height + 2)
                ' Make an associated Graphics object.
                Using bmpGraphics As Graphics = Graphics.FromImage(overlay)
                    Using brush As New LinearGradientBrush(Point.Empty, New Point(0, progressRectangle.Height), ProgressColumn.ProgressGradient.ManipuleStart.BaseColor, ProgressColumn.ProgressGradient.ManipuleEnd.BaseColor)
                        If Not ProgressColumn.ProgressGradient.IsBlendedForProgress Then
                            bmpGraphics.FillRectangle(brush, progressRectangle)
                        Else
                            Dim bl As New Blend(2)
                            bl.Factors = New Single() {0.3F, 1.0F}
                            bl.Positions = New Single() {0.0F, 1.0F}
                            brush.Blend = bl
                            bmpGraphics.FillRectangle(brush, progressRectangle)
                        End If
                    End Using

                    Dim topInner As New LinearGradientBrush(left, right, ProgressColumn.ProgressGradient.ManipuleStart.Light, ProgressColumn.ProgressGradient.ManipuleEnd.Light)
                    Dim topOuter As New LinearGradientBrush(left, right, ProgressColumn.ProgressGradient.ManipuleStart.Lighter, ProgressColumn.ProgressGradient.ManipuleEnd.Lighter)
                    Dim bottomInner As New LinearGradientBrush(left, right, ProgressColumn.ProgressGradient.ManipuleStart.Dark, ProgressColumn.ProgressGradient.ManipuleEnd.Dark)
                    Dim bottomOuter As New LinearGradientBrush(left, right, ProgressColumn.ProgressGradient.ManipuleStart.Darker, ProgressColumn.ProgressGradient.ManipuleEnd.Darker)

                    ' Inner Top
                    Using pen As New Pen(topInner)
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Y + 1, progressRectangle.Right - 1, progressRectangle.Y + 1)
                    End Using

                    ' Inner Left
                    Using pen As New Pen(ProgressColumn.ProgressGradient.ManipuleStart.Light)
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Y + 1, progressRectangle.X + 1, progressRectangle.Bottom - 1)
                    End Using

                    ' Outer Top
                    Using pen As New Pen(topOuter)
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Y, progressRectangle.Right, progressRectangle.Y)
                    End Using

                    ' Outer Left
                    Using pen As New Pen(ProgressColumn.ProgressGradient.ManipuleStart.Lighter)
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Y, progressRectangle.X, progressRectangle.Bottom)
                    End Using

                    ' Inner Bottom
                    Using pen As New Pen(bottomInner)
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Bottom - 1, progressRectangle.Right - 1, progressRectangle.Bottom - 1)
                    End Using

                    ' Inner Right
                    Using pen As New Pen(ProgressColumn.ProgressGradient.ManipuleEnd.Dark)
                        bmpGraphics.DrawLine(pen, progressRectangle.Right - 1, progressRectangle.Y + 1, progressRectangle.Right - 1, progressRectangle.Bottom - 1)
                    End Using

                    ' Outer Bottom
                    Using pen As New Pen(bottomOuter)
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Bottom, progressRectangle.Right, progressRectangle.Bottom)
                    End Using

                    ' Outer Right
                    Using pen As New Pen(ProgressColumn.ProgressGradient.ManipuleEnd.Darker)
                        bmpGraphics.DrawLine(pen, progressRectangle.Right, progressRectangle.Y, progressRectangle.Right, progressRectangle.Bottom)
                    End Using

                    topInner.Dispose()
                    topOuter.Dispose()
                    bottomInner.Dispose()
                    bottomOuter.Dispose()
                End Using

                ' Create a new color matrix,
                '                   The value Alpha in row 4, column 4 specifies the alpha value 

                ' Red component   [from 0.0 to 1.0 increase red color component.]
                ' Green component [from 0.0 to 1.0 increase green color component.]
                ' Blue component  [from 0.0 to 1.0 increase blue color component.]
                ' Alpha component [from 1.0 to 0.0 increase transparency bitmap.]
                ' White component [0.0: goes to Original color, 1.0: goes to white for all color component(Red, Green, Blue.)]
                Dim jaggedMatrix As Single()() = New Single()() {New Single() {If(ProgressColumn.ProgressColorizer.IsColorizerEnabled, ProgressColumn.ProgressColorizer.Red / 255.0F, 1.0F), 0.0F, 0.0F, 0.0F, 0.0F}, New Single() {0.0F, If(ProgressColumn.ProgressColorizer.IsColorizerEnabled, ProgressColumn.ProgressColorizer.Green / 255.0F, 1.0F), 0.0F, 0.0F, 0.0F}, New Single() {0.0F, 0.0F, If(ProgressColumn.ProgressColorizer.IsColorizerEnabled, ProgressColumn.ProgressColorizer.Blue / 255.0F, 1.0F), 0.0F, 0.0F}, New Single() {0.0F, 0.0F, 0.0F, If(ProgressColumn.ProgressColorizer.IsTransparencyEnabled, ProgressColumn.ProgressColorizer.Alpha / 255.0F, 1.0F), 0.0F}, New Single() {If(ProgressColumn.ProgressColorizer.IsColorizerEnabled, 0.2F, 0.0F), If(ProgressColumn.ProgressColorizer.IsColorizerEnabled, 0.2F, 0.0F), If(ProgressColumn.ProgressColorizer.IsColorizerEnabled, 0.2F, 0.0F), 0.0F, 1.0F}}

                Dim colorMatrix As New ColorMatrix(jaggedMatrix)

                ' Create an ImageAttributes object and set its color matrix
                Using attributes As New ImageAttributes()
                    attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)

                    gr.DrawImage(overlay, New Rectangle(borderRectangle.Location, New Size(overlay.Width, overlay.Height)), 0, 0, overlay.Width, overlay.Height, _
                     GraphicsUnit.Pixel, attributes)
                End Using
            End Using
        End Sub

        Protected Overridable Sub DrawBackground(ByVal gr As Graphics)
            If backgroundRectangle.Width < 2 Then
                Return
            End If

            Using brush As New LinearGradientBrush(New Point(backgroundRectangle.Left, backgroundRectangle.Top), New Point(backgroundRectangle.Left, backgroundRectangle.Bottom), ProgressColumn.BackgroundGradient.ColorStart, ProgressColumn.BackgroundGradient.ColorEnd)
                If Not ProgressColumn.BackgroundGradient.IsBlendedForBackground Then
                    gr.FillRectangle(brush, backgroundRectangle)
                Else
                    Dim bl As New Blend(2)
                    bl.Factors = New Single() {0.3F, 1.0F}
                    bl.Positions = New Single() {0.0F, 1.0F}
                    brush.Blend = bl
                    gr.FillRectangle(brush, backgroundRectangle)
                End If
            End Using
        End Sub

#End Region

#Region "Override Methods"

        Protected Overrides Sub OnMouseEnter(ByVal rowIndex As Integer)
            ' Handle hover cell index.
            hoverCellIndex = rowIndex

            ' Invalidate the current cell.
            Me.DataGridView.InvalidateCell(Me.ColumnIndex, rowIndex)
        End Sub

        Protected Overrides Sub OnMouseLeave(ByVal rowIndex As Integer)
            hoverCellIndex = -1

            ' Invalidate the current cell.
            Me.DataGridView.InvalidateCell(Me.ColumnIndex, rowIndex)
        End Sub

        Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal cellState As DataGridViewElementStates, ByVal value As Object, _
         ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, ByVal paintParts As DataGridViewPaintParts)
            ' Create or ReCreate progressBar cell's rectangles.
            GenerateProgressBar(cellBounds)

            ' Is the mouse hovering over this cell?
            If (hoverCellIndex = rowIndex) AndAlso ((cellState And DataGridViewElementStates.Selected) <> DataGridViewElementStates.Selected) Then
                ' Draw HoverCell Gradient Background.
                Using brush As New LinearGradientBrush(cellBounds, ProgressColumn.HoverGradient.ColorStart, ProgressColumn.HoverGradient.ColorEnd, ProgressColumn.HoverGradient.GradientStyle)
                    graphics.FillRectangle(brush, cellBounds)
                End Using

                ' Draw ProgressBar Gradient Background.
                DrawBackground(graphics)
                ' Draw ProgressBar Gradient Progress.
                DrawProgress(graphics)
                ' Draw ProgressBar Border Color.
                DrawBorder(graphics)
                ' Draw Standard Painting.

                If ProgressColumn.ShowPercentage Then
                    Dim ownCellStyle As New DataGridViewCellStyle(cellStyle)
                    ownCellStyle.ForeColor = ProgressColumn.HoverGradient.HoverForeColor

                    MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                     formattedValue, errorText, ownCellStyle, advancedBorderStyle, paintParts And Not (DataGridViewPaintParts.Background Or DataGridViewPaintParts.SelectionBackground))
                Else
                    MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                     formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts And Not (DataGridViewPaintParts.Background Or DataGridViewPaintParts.SelectionBackground Or DataGridViewPaintParts.ContentBackground Or DataGridViewPaintParts.ContentForeground))
                End If
            Else
                ' We don't draw content background and foreground style at this point.
                MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                 formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts And Not (DataGridViewPaintParts.ContentBackground Or DataGridViewPaintParts.ContentForeground))

                ' Draw ProgressBar Gradient Background.
                DrawBackground(graphics)
                ' Draw ProgressBar Gradient Progress.
                DrawProgress(graphics)
                ' Draw ProgressBar Border Color.
                DrawBorder(graphics)
                ' Draw Standard Painting.

                If ProgressColumn.ShowPercentage Then
                    MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                     formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.None Or DataGridViewPaintParts.ContentBackground Or DataGridViewPaintParts.ContentForeground)
                Else
                    MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                     formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.None)
                End If
            End If
        End Sub

        ' First attempt value is DefaultNewRowValue of the cell.
        Protected Overrides Function SetValue(ByVal rowIndex As Integer, ByVal value As Object) As Boolean
            If TypeOf value Is Int32 Then
                Dim current As Integer = CInt(value)

                If ProgressColumn IsNot Nothing Then
                    If current < ProgressColumn.Minimum Then
                        current = ProgressColumn.Minimum
                        MessageBox.Show("The value must be greater than or equal to Minimum.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    If current > ProgressColumn.Maximum Then
                        current = ProgressColumn.Maximum
                        MessageBox.Show("The value must be smaller than or equal to Maximum.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If

                Return MyBase.SetValue(rowIndex, current)
            Else
                Throw New FormatException("The value type must be an Int32 type.")
            End If
        End Function

        ' This function returns a formatted string value by DataGridViewCellStyle.Format of this cell.
        Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, ByVal valueTypeConverter As TypeConverter, ByVal formattedValueTypeConverter As TypeConverter, ByVal context As DataGridViewDataErrorContexts) As Object
            If cellStyle.Format.StartsWith("0 %", StringComparison.InvariantCulture) Then
                Dim current As Single = CInt(value) / CSng(ProgressColumn.Maximum)
                Return MyBase.GetFormattedValue(current, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
            End If

            Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
        End Function

        ' FormattedValue type is string and then this value type converted to our value type(int).
        Public Overrides Function ParseFormattedValue(ByVal formattedValue As Object, ByVal cellStyle As DataGridViewCellStyle, ByVal formattedValueTypeConverter As TypeConverter, ByVal valueTypeConverter As TypeConverter) As Object
            Dim result As Object

            Try
                result = MyBase.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter)
            Catch generatedExceptionName As FormatException
                result = 0
                MessageBox.Show("The value type must be an Int32 type.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End Try

            Return result
        End Function

        Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle As DataGridViewCellStyle)
            ' Set the value of the editing control by current cell value.
            MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

            Dim editingControl As DataGridViewTextBoxEditingControl = TryCast(DataGridView.EditingControl, DataGridViewTextBoxEditingControl)
            editingControl.Text = Me.Value.ToString()
        End Sub

#End Region
    End Class
End Namespace