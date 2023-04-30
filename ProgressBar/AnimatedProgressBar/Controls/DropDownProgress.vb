
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.Runtime.InteropServices

Namespace UIControls.ProgressBar
    <ToolboxItem(False)> _
    Partial Public Class eDropDownProgress
        Inherits UserControl
#Region "API"

#Region "Symbolic Constants"

        Private Const WM_PAINT As Integer = &HF
        Private Const WM_NCPAINT As Integer = &H85
        Private Const WM_ERASEBKGND As Integer = &H14

#End Region

#Region "UnmanagedMethods"

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetDC(ByVal hwnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Friend Shared Function ReleaseDC(ByVal hwnd As IntPtr, ByVal hdc As IntPtr) As Integer
        End Function

#End Region

#End Region

#Region "Instance Members"

        Private editorService As IWindowsFormsEditorService = Nothing
        Private m_colorizer As ColorizerProgress = Nothing

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Sub New(ByVal value As Object, ByVal editorService As IWindowsFormsEditorService)
            Me.New()
            Me.editorService = editorService
            If TypeOf value Is ColorizerProgress Then
                Me.m_colorizer = TryCast(value, ColorizerProgress)
            End If
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

        Public ReadOnly Property Colorizer() As ColorizerProgress
            Get
                Return InlineAssignHelper(m_colorizer, New ColorizerProgress(CByte(redTrackBar.Value), CByte(greenTrackBar.Value), CByte(blueTrackBar.Value), CByte(alphaTrackBar.Value), m_colorizer.IsColorizerEnabled, m_colorizer.IsTransparencyEnabled))
            End Get
        End Property

#End Region

#Region "Override Methods"

        ''' <summary>
        ''' Uygulamaya gelecek windows mesajları burda işlenir.
        ''' </summary>
        ''' <param name="m">Windows mesaj parametresi</param>
        Protected Overrides Sub WndProc(ByRef m As Message)
            ' Kendi process işlemlerini yapsın.
            MyBase.WndProc(m)
            ' Daha sonra bizim metodumuzuda çağırsın.
            DrawBorder(m, Me.Width, Me.Height)
        End Sub

#End Region

#Region "Helper Methods"

        ''' <summary>
        ''' Kontrolümüz için kenarlık çiziyoruz.
        ''' </summary>
        ''' <param name="message">Kontrolümüzün Win32 mesaj işleme komutu</param>
        ''' <param name="width">Kontrolümüzün genişlik değeri</param>
        ''' <param name="height">Kontrolümüzün yükseklik değeri</param>
        Private Sub DrawBorder(ByRef message As Message, ByVal width As Integer, ByVal height As Integer)
            If message.Msg = WM_NCPAINT OrElse message.Msg = WM_ERASEBKGND OrElse message.Msg = WM_PAINT Then
                Dim hdc As IntPtr = GetDC(message.HWnd)

                If hdc <> IntPtr.Zero Then
                    Dim graphics__1 As Graphics = Graphics.FromHdc(hdc)
                    Dim rectangle As New Rectangle(0, 0, width, height)
                    ControlPaint.DrawBorder(graphics__1, rectangle, SystemColors.ActiveBorder, Me.Padding.Left, ButtonBorderStyle.Solid, SystemColors.ActiveBorder, _
                     Me.Padding.Top, ButtonBorderStyle.Solid, SystemColors.ActiveBorder, Me.Padding.Right, ButtonBorderStyle.Solid, SystemColors.ActiveBorder, _
                     Me.Padding.Bottom, ButtonBorderStyle.Solid)

                    ' Grafik nesnesinden kurtul.
                    ReleaseDC(message.HWnd, hdc)
                End If
            End If
        End Sub

#End Region

#Region "Event Handlers"

        Private Sub DropDownProgress_Load(ByVal sender As Object, ByVal e As EventArgs)
            redTrackBar.Value = m_colorizer.Red
            greenTrackBar.Value = m_colorizer.Green
            blueTrackBar.Value = m_colorizer.Blue
            alphaTrackBar.Value = m_colorizer.Alpha
        End Sub

        Private Sub closeDialogBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
            editorService.CloseDropDown()
        End Sub

        Private Sub TRACKBAR_VALUE_CHANGED(ByVal sender As Object, ByVal e As EventArgs)
            If TypeOf sender Is TrackBar Then
                Dim result As String = Nothing
                Dim ctrl As TrackBar = DirectCast(sender, TrackBar)
                If ctrl.Value = 0 Then
                    result = "Min"
                ElseIf ctrl.Value = 255 Then
                    result = "Max"
                End If

                Select Case ctrl.Name
                    Case "redTrackBar"
                        label1.Text = [String].Format("Red: {0}", If(result, ctrl.Value.ToString()))
                        Exit Select
                    Case "greenTrackBar"
                        label2.Text = [String].Format("Green: {0}", If(result, ctrl.Value.ToString()))
                        Exit Select
                    Case "blueTrackBar"
                        label3.Text = [String].Format("Blue: {0}", If(result, ctrl.Value.ToString()))
                        Exit Select
                    Case Else
                        label4.Text = [String].Format("Alpha: {0}", If(result, ctrl.Value.ToString()))
                        Exit Select
                End Select
            End If
        End Sub
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function

#End Region
    End Class

    Partial Class eDropDownProgress
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If

                For Each control As System.Windows.Forms.Control In Controls
                    control.Dispose()
                Next
            End If

            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.redTrackBar = New System.Windows.Forms.TrackBar()
            Me.greenTrackBar = New System.Windows.Forms.TrackBar()
            Me.blueTrackBar = New System.Windows.Forms.TrackBar()
            Me.alphaTrackBar = New System.Windows.Forms.TrackBar()
            Me.closeDialogBtn = New System.Windows.Forms.Button()
            Me.label1 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.label3 = New System.Windows.Forms.Label()
            Me.label4 = New System.Windows.Forms.Label()
            DirectCast(Me.redTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.greenTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.blueTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.alphaTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' redTrackBar
            ' 
            Me.redTrackBar.Location = New System.Drawing.Point(29, 27)
            Me.redTrackBar.Maximum = 255
            Me.redTrackBar.Name = "redTrackBar"
            Me.redTrackBar.Size = New System.Drawing.Size(288, 45)
            Me.redTrackBar.TabIndex = 1
            Me.redTrackBar.TickFrequency = 15
            Me.redTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft
            AddHandler Me.redTrackBar.ValueChanged, AddressOf Me.TRACKBAR_VALUE_CHANGED
            ' 
            ' greenTrackBar
            ' 
            Me.greenTrackBar.Location = New System.Drawing.Point(29, 78)
            Me.greenTrackBar.Maximum = 255
            Me.greenTrackBar.Name = "greenTrackBar"
            Me.greenTrackBar.Size = New System.Drawing.Size(288, 45)
            Me.greenTrackBar.TabIndex = 2
            Me.greenTrackBar.TickFrequency = 15
            Me.greenTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft
            AddHandler Me.greenTrackBar.ValueChanged, AddressOf Me.TRACKBAR_VALUE_CHANGED
            ' 
            ' blueTrackBar
            ' 
            Me.blueTrackBar.Location = New System.Drawing.Point(29, 129)
            Me.blueTrackBar.Maximum = 255
            Me.blueTrackBar.Name = "blueTrackBar"
            Me.blueTrackBar.Size = New System.Drawing.Size(288, 45)
            Me.blueTrackBar.TabIndex = 3
            Me.blueTrackBar.TickFrequency = 15
            Me.blueTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft
            AddHandler Me.blueTrackBar.ValueChanged, AddressOf Me.TRACKBAR_VALUE_CHANGED
            ' 
            ' alphaTrackBar
            ' 
            Me.alphaTrackBar.Location = New System.Drawing.Point(29, 180)
            Me.alphaTrackBar.Maximum = 255
            Me.alphaTrackBar.Minimum = 50
            Me.alphaTrackBar.Name = "alphaTrackBar"
            Me.alphaTrackBar.Size = New System.Drawing.Size(288, 45)
            Me.alphaTrackBar.TabIndex = 4
            Me.alphaTrackBar.TickFrequency = 15
            Me.alphaTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft
            Me.alphaTrackBar.Value = 50
            AddHandler Me.alphaTrackBar.ValueChanged, AddressOf Me.TRACKBAR_VALUE_CHANGED
            ' 
            ' closeDialogBtn
            ' 
            Me.closeDialogBtn.Font = New System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(162))
            Me.closeDialogBtn.Location = New System.Drawing.Point(121, 230)
            Me.closeDialogBtn.Name = "closeDialogBtn"
            Me.closeDialogBtn.Size = New System.Drawing.Size(105, 23)
            Me.closeDialogBtn.TabIndex = 8
            Me.closeDialogBtn.Text = "Close Dialog"
            Me.closeDialogBtn.UseVisualStyleBackColor = True
            AddHandler Me.closeDialogBtn.Click, AddressOf Me.closeDialogBtn_Click
            ' 
            ' label1
            ' 
            Me.label1.AutoSize = True
            Me.label1.ForeColor = System.Drawing.Color.Red
            Me.label1.Location = New System.Drawing.Point(32, 11)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(30, 13)
            Me.label1.TabIndex = 9
            Me.label1.Text = "Red:"
            ' 
            ' label2
            ' 
            Me.label2.AutoSize = True
            Me.label2.ForeColor = System.Drawing.Color.Green
            Me.label2.Location = New System.Drawing.Point(32, 62)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(40, 13)
            Me.label2.TabIndex = 10
            Me.label2.Text = "Green:"
            ' 
            ' label3
            ' 
            Me.label3.AutoSize = True
            Me.label3.ForeColor = System.Drawing.Color.Blue
            Me.label3.Location = New System.Drawing.Point(32, 113)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(31, 13)
            Me.label3.TabIndex = 11
            Me.label3.Text = "Blue:"
            ' 
            ' label4
            ' 
            Me.label4.AutoSize = True
            Me.label4.ForeColor = System.Drawing.Color.Black
            Me.label4.Location = New System.Drawing.Point(32, 164)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(38, 13)
            Me.label4.TabIndex = 12
            Me.label4.Text = "Alpha:"
            ' 
            ' DropDownProgress
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.closeDialogBtn)
            Me.Controls.Add(Me.alphaTrackBar)
            Me.Controls.Add(Me.blueTrackBar)
            Me.Controls.Add(Me.greenTrackBar)
            Me.Controls.Add(Me.redTrackBar)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(162))
            Me.Name = "DropDownProgress"
            Me.Padding = New System.Windows.Forms.Padding(4)
            Me.Size = New System.Drawing.Size(347, 261)
            AddHandler Me.Load, AddressOf Me.DropDownProgress_Load
            DirectCast(Me.redTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.greenTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.blueTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.alphaTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private redTrackBar As System.Windows.Forms.TrackBar
        Private greenTrackBar As System.Windows.Forms.TrackBar
        Private blueTrackBar As System.Windows.Forms.TrackBar
        Private alphaTrackBar As System.Windows.Forms.TrackBar
        Private closeDialogBtn As System.Windows.Forms.Button
        Private label1 As System.Windows.Forms.Label
        Private label2 As System.Windows.Forms.Label
        Private label3 As System.Windows.Forms.Label
        Private label4 As System.Windows.Forms.Label
    End Class
End Namespace