
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports ESAR_Controls.UIControls.ProgressBar.Components

Namespace UIControls.ProgressBar
    Public Class eAnimatedEasyProgressControl
        Inherits Control
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
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)

            ' Object Initializer
            Me.Size = New Size(100, 50)
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

#End Region

#Region "Virtual Methods"

        Protected Overridable Sub OnFrameChanged(ByVal sender As Object, ByVal e As ReportTaskProgressEventArgs)
            RaiseEvent FrameChanged(Me, e)

            newFrameImage = e.GetActiveFrameImage

            Me.Invalidate()
            Me.Update()
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

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            If Visible Then
                Try
                    If newFrameImage IsNot Nothing Then
                        Dim rct As Rectangle = Me.ClientRectangle
                        e.Graphics.DrawImageUnscaled(newFrameImage, rct)
                    End If
                Catch


                End Try
            End If
        End Sub

#End Region
    End Class
End Namespace