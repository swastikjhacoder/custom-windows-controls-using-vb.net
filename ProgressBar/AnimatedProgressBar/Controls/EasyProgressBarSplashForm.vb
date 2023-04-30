
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports ESAR_Controls.UIControls.ProgressBar.Components
Imports ESAR_Controls.UIControls.ProgressBar.Win32

Namespace UIControls.ProgressBar
    Public Class EasyProgressBarSplashForm
        Inherits Form
#Region "Event"

        ''' <summary>
        ''' Represents an event which occurs when the index of the active frame changed.
        ''' </summary>
        <Description("Represents an event which occurs when the index of the active frame changed")> _
        Public Event FrameChanged As EventHandler(Of ReportTaskProgressEventArgs)

#End Region

#Region "Instance Members"

        Private newFrameImage As Image = Nothing
        Private m_easyProgressTaskManager As eAniEasyProgressTaskManager = Nothing

#End Region

#Region "Constructor"

        Public Sub New()
            Me.SetStyle(ControlStyles.Selectable, False)
            Me.SetStyle(ControlStyles.ResizeRedraw, False)
            Me.SetStyle(ControlStyles.FixedWidth Or ControlStyles.FixedHeight, True)
            Me.DoubleBuffered = True

            Me.ShowIcon = False
            Me.ControlBox = False
            Me.MaximizeBox = False
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.Manual
            Me.FormBorderStyle = FormBorderStyle.None
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

        ''' <summary>
        ''' Gets or Sets the task manager for painting this control.
        ''' </summary>
        <Description("Gets or Sets the task manager for painting this control")> _
        Public Property EasyProgressTaskManager() As eAniEasyProgressTaskManager
            Get
                Return m_easyProgressTaskManager
            End Get
            Set(ByVal value As eAniEasyProgressTaskManager)
                Try
                    If Not value.Equals(m_easyProgressTaskManager) Then
                        If m_easyProgressTaskManager IsNot Nothing Then
                            RemoveHandler m_easyProgressTaskManager.TaskProgress, AddressOf OnFrameChanged
                        End If

                        m_easyProgressTaskManager = value
                        AddHandler m_easyProgressTaskManager.TaskProgress, AddressOf OnFrameChanged
                    End If
                Catch generatedExceptionName As NullReferenceException
                    If m_easyProgressTaskManager IsNot Nothing Then
                        RemoveHandler m_easyProgressTaskManager.TaskProgress, AddressOf OnFrameChanged
                    End If

                    m_easyProgressTaskManager = Nothing
                End Try
            End Set
        End Property

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams

                ' This form has to have the WS_EX_LAYERED extended style.
                cp.ExStyle = cp.ExStyle Or CInt(User32.WindowExStyles.WS_EX_LAYERED)
                ' Hide from ALT+Tab menu, Turn on WS_EX_TOOLWINDOW style.
                cp.ExStyle = cp.ExStyle Or CInt(User32.WindowExStyles.WS_EX_TOOLWINDOW)
                Return cp
            End Get
        End Property

        Protected Overrides ReadOnly Property ShowWithoutActivation() As Boolean
            ' True if the window will not be activated when it is shown; otherwise, false.
            Get
                Return True
            End Get
        End Property

#End Region

#Region "Virtual Methods"

        Protected Overridable Sub OnFrameChanged(ByVal sender As Object, ByVal e As ReportTaskProgressEventArgs)
            RaiseEvent FrameChanged(Me, e)

            newFrameImage = e.GetActiveFrameImage
            If newFrameImage IsNot Nothing Then
                GDIWindow(DirectCast(newFrameImage, Bitmap))
            End If
        End Sub

#End Region

#Region "Override Methods"

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                EasyProgressTaskManager = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub

#End Region

#Region "Helper Methods"

        Private Sub GDIWindow(ByVal bmp As Bitmap)
            Dim hDC As IntPtr = User32.GetDC(IntPtr.Zero)
            Try
                Dim hMemDC As IntPtr = User32.CreateCompatibleDC(hDC)
                Try
                    Dim hBmp As IntPtr = bmp.GetHbitmap(Color.FromArgb(0))
                    Try
                        Dim previousBmp As IntPtr = User32.SelectObject(hMemDC, hBmp)
                        Try
                            Dim ptDst As New Point(Left, Top)
                            Dim size As New Size(bmp.Width, bmp.Height)
                            Dim ptSrc As New Point(0, 0)

                            Dim blend As New User32.BLENDFUNCTION()
                            blend.BlendOp = User32._AC_SRC_OVER
                            blend.BlendFlags = 0
                            blend.SourceConstantAlpha = 255
                            blend.AlphaFormat = User32._AC_SRC_ALPHA

                            User32.UpdateLayeredWindow(Handle, hDC, ptDst, size, hMemDC, ptSrc, _
                             0, blend, User32._LWA_ALPHA)
                        Finally
                            User32.SelectObject(hDC, previousBmp)
                        End Try
                    Finally
                        User32.DeleteObject(hBmp)
                    End Try
                Finally
                    User32.DeleteDC(hMemDC)
                End Try
            Finally
                User32.ReleaseDC(IntPtr.Zero, hDC)
            End Try
        End Sub

#End Region
    End Class
End Namespace