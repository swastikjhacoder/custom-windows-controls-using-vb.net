
Imports System.Drawing
Imports System.Threading
Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Drawing.Imaging

Namespace UIControls.ProgressBar.Components
    Public Class eAniEasyProgressTaskManager
        Inherits Component
#Region "Events"

        ''' <summary>
        ''' Event raised when the task is succesfully started.
        ''' </summary>
        <Description("Event raised when the task is succesfully started")> _
        Public Event TaskStarted As EventHandler

        ''' <summary>
        ''' Event raised when the task is succesfully stopped.
        ''' </summary>
        <Description("Event raised when the task is succesfully stopped")> _
        Public Event TaskStopped As EventHandler

        ''' <summary>
        ''' Event raised when the task operation is succesfully completed.
        ''' </summary>
        <Description("Event raised when the task operation is succesfully completed")> _
        Public Event TaskCompleted As EventHandler

        ''' <summary>
        ''' Represents an event which occurs when the index of the active frame changed.
        ''' </summary>
        <Description("Represents an event which occurs when the index of the active frame changed")> _
        Public Event TaskProgress As EventHandler(Of ReportTaskProgressEventArgs)

#End Region

#Region "Instance Members"

        Private m_stepCount As Integer = 0
        Private m_stepInterval As Integer = 10

        ' This is the thread where the task is carried out.
        Private thread As Thread
        Private invokeContext As System.Windows.Forms.Control

#Region "Animated Image Instances"

        Private m_animatedGif As Image
        Private dimension As FrameDimension
        Private [step] As Integer = 1

#End Region

#End Region

#Region "Constructor"

        Public Sub New()
            FPS = 30.0
            CancelWaitTime = TimeSpan.Zero
            CancelCheckInterval = 1
        End Sub

        Public Sub New(ByVal container As IContainer)
            Me.New()
            container.Add(Me)
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
        ''' Gets or Sets the frame per second value of the specified image.
        ''' </summary>
        <Description("Gets or Sets the frame per second value of the specified image")> _
        <DefaultValue(30.0)> _
        <Browsable(True)> _
        Public Property FPS() As Double
            Get
                Return m_FPS
            End Get
            Set(ByVal value As Double)
                m_FPS = value
            End Set
        End Property
        Private m_FPS As Double

        ''' <summary>
        ''' Gets or Sets the image to be animated.
        ''' </summary>
        <Description("Gets or Sets the image to be animated")> _
        <RefreshProperties(RefreshProperties.All)> _
        <Browsable(True)> _
        Public Property AnimatedGif() As Image
            Get
                Return m_animatedGif
            End Get
            Set(ByVal value As Image)
                Try
                    If IsWorking Then
                        Throw New InvalidOperationException("You cannot change this value while the task is running.")
                    End If

                    If Not value.Equals(m_animatedGif) Then
                        m_animatedGif = value

                        ' Gets the GUID and sets framedimension.
                        dimension = New FrameDimension(m_animatedGif.FrameDimensionsList(0))
                        ' Sets the total number of frames in the animation.
                        FrameCount = m_animatedGif.GetFrameCount(dimension)
                    End If
                Catch generatedExceptionName As NullReferenceException
                    m_animatedGif = Nothing
                    dimension = Nothing
                    FrameCount = 0
                    CurrentFrame = 0
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Gets the total number of frames of the specified image.
        ''' </summary>
        <Description("Gets the total number of frames of the specified image")> _
        <Browsable(True)> _
        Public Property FrameCount() As Integer
            Get
                Return m_FrameCount
            End Get
            Private Set(ByVal value As Integer)
                m_FrameCount = value
            End Set
        End Property
        Private m_FrameCount As Integer

        ''' <summary>
        ''' Gets the index of the active frame.
        ''' </summary>
        <Description("Gets the index of the active frame")> _
        <Browsable(True)> _
        Public Property CurrentFrame() As Integer
            Get
                Return m_CurrentFrame
            End Get
            Private Set(ByVal value As Integer)
                m_CurrentFrame = value
            End Set
        End Property
        Private m_CurrentFrame As Integer

        Private ReadOnly Property GetNextFrame() As Image
            Get
                If m_animatedGif IsNot Nothing Then
                    CurrentFrame += [step]

                    ' If the animation reaches a boundary.
                    If CurrentFrame >= FrameCount OrElse CurrentFrame < 1 Then
                        If IsReverseEnabled Then
                            ' Reverse and apply it.
                            [step] *= -1
                            CurrentFrame += [step]
                        Else
                            CurrentFrame = 0
                        End If
                    End If

                    Return GetSpecifiedFrame(CurrentFrame)
                End If

                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Determines whether the animated gif should play backwards when it reaches the end of the frames.
        ''' </summary>
        <Description("Determines whether the animated gif should play backwards when it reaches the end of the frames")> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        Public Property IsReverseEnabled() As Boolean
            Get
                Return m_IsReverseEnabled
            End Get
            Set(ByVal value As Boolean)
                m_IsReverseEnabled = value
            End Set
        End Property
        Private m_IsReverseEnabled As Boolean

        ''' <summary>
        ''' If task monitor is running, returns true otherwise; false.
        ''' </summary>
        <Description("If task monitor is running, returns true otherwise; false")> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        Public Property IsWorking() As Boolean
            Get
                Return m_IsWorking
            End Get
            Private Set(ByVal value As Boolean)
                m_IsWorking = value
            End Set
        End Property
        Private m_IsWorking As Boolean

        ''' <summary>
        ''' The StepCount property must be in the range of 0 to 100 (Frame count of the animated image) '0' means is looped.
        ''' </summary>
        <Description("The StepCount property must be in the range of 0 to 100 (Frame count of the animated image) '0' means is looped")> _
        <DefaultValue(0)> _
        <Browsable(True)> _
        Public Property StepCount() As Integer
            Get
                Return m_stepCount
            End Get
            Set(ByVal value As Integer)
                Try
                    If IsWorking Then
                        Throw New InvalidOperationException("You cannot change this value while the task is running.")
                    End If

                    If Not value.Equals(m_stepCount) Then
                        If value < 0 Then
                            value = 0
                        ElseIf value > 100 Then
                            value = 100
                        End If

                        m_stepCount = value
                    End If
                Catch ex As Exception
                    TaskLog = ex.Message
                End Try
            End Set
        End Property

        ''' <summary>
        ''' The StepInterval property must be in the range of 10 to 300 (Millisecond species).
        ''' </summary>
        <Description("The StepInterval property must be in the range of 10 to 300 (Millisecond species)")> _
        <DefaultValue(10)> _
        <Browsable(True)> _
        Public Property StepInterval() As Integer
            Get
                Return m_stepInterval
            End Get
            Set(ByVal value As Integer)
                Try
                    If IsWorking Then
                        Throw New InvalidOperationException("You cannot change this value while the task is running.")
                    End If

                    If Not value.Equals(m_stepInterval) Then
                        If value < 10 Then
                            value = 10
                        ElseIf value > 300 Then
                            value = 300
                        End If

                        m_stepInterval = value
                    End If
                Catch ex As Exception
                    TaskLog = ex.Message
                End Try
            End Set
        End Property

        ''' <summary>
        ''' Gets the currently status of the task thread.
        ''' </summary>
        <Description("Gets the currently status of the task thread")> _
        <Browsable(True)> _
        Public Property TaskLog() As String
            Get
                Return m_TaskLog
            End Get
            Private Set(ByVal value As String)
                m_TaskLog = value
            End Set
        End Property
        Private m_TaskLog As String

        ''' <summary>
        ''' How often the thread checks to see if a cancellation message has been heeded(A number of seconds).
        ''' </summary>
        <Description("How often the thread checks to see if a cancellation message has been heeded(A number of seconds)")> _
        <DefaultValue(1)> _
        <Browsable(True)> _
        Public Property CancelCheckInterval() As Integer
            Get
                Return m_CancelCheckInterval
            End Get
            Set(ByVal value As Integer)
                m_CancelCheckInterval = value
            End Set
        End Property
        Private m_CancelCheckInterval As Integer

        ''' <summary>
        ''' How long the thread will wait (in total) before aborting a thread that hasn't responded to
        ''' the cancellation message. TimeSpan.Zero means polite stops are not enabled.
        ''' </summary>
        <Description("How long the thread will wait (in total) before aborting a thread that hasn't responded to the cancellation message. TimeSpan.Zero means polite stops are not enabled")> _
        <DefaultValue(GetType(TimeSpan), "00:00:00")> _
        <Browsable(True)> _
        Public Property CancelWaitTime() As TimeSpan
            Get
                Return m_CancelWaitTime
            End Get
            Set(ByVal value As TimeSpan)
                m_CancelWaitTime = value
            End Set
        End Property
        Private m_CancelWaitTime As TimeSpan

#End Region

#Region "Helper Methods"

        Private Function GetSpecifiedFrame(ByVal index As Integer) As Image
            Dim img As Image = Nothing
            Try
                ' Selects the frame specified by the dimension and index.
                m_animatedGif.SelectActiveFrame(dimension, index)

                ' Return its copy.
                img = DirectCast(m_animatedGif.Clone(), Image)
            Catch


            End Try

            Return img
        End Function

        Private Sub StartTaskAsync()
            Try
                DoTask()

                If invokeContext.IsHandleCreated Then
                    ' Fire notification event on the user interface thread.
                    invokeContext.Invoke(New EventHandler(AddressOf OnTaskCompleted), New Object() {Me, EventArgs.Empty})
                End If

                ' Write log message.
                TaskLog = [String].Format("Task Completed !!!, The task {0} is completed succesfully at {1:F}", thread.CurrentThread.Name, DateTime.Now)
            Catch generatedExceptionName As ThreadAbortException
                ' Write log message.
                TaskLog = [String].Format("Task Aborted !!!, The task {0} is destroyed and stopped at {1:F}", thread.CurrentThread.Name, DateTime.Now)
            Finally
                ' Update status.
                IsWorking = False
            End Try
        End Sub

        Private Sub DoTask()
            Dim ticks1 As Long = 0
            Dim ticks2 As Long = 0

            Dim interval As Double = CDbl(Stopwatch.Frequency) / FPS
            Dim i As Integer = 1
            While If(StepCount = 0, True, i <= StepCount)
                ticks2 = Stopwatch.GetTimestamp()
                If ticks2 >= ticks1 + interval Then
                    ticks1 = Stopwatch.GetTimestamp()

                    If invokeContext.IsHandleCreated Then
                        Try
                            ' Fire notification event.
                            invokeContext.Invoke(New EventHandler(Of ReportTaskProgressEventArgs)(AddressOf OnTaskProgress), New Object() {Me, New ReportTaskProgressEventArgs(GetNextFrame)})
                        Catch generatedExceptionName As NullReferenceException


                        End Try
                    End If
                End If

                ' Pause specified milliseconds before the next pass.
                thread.Sleep(TimeSpan.FromMilliseconds(StepInterval))
                i += 1
            End While
        End Sub

        Private Sub OnTaskProgress(ByVal sender As Object, ByVal e As ReportTaskProgressEventArgs)
            RaiseEvent TaskProgress(sender, e)
        End Sub

        Private Sub OnTaskCompleted(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent TaskCompleted(sender, e)
        End Sub

        Private Sub OnTaskStarted()
            RaiseEvent TaskStarted(Me, EventArgs.Empty)
        End Sub

        Private Sub OnTaskStopped()
            RaiseEvent TaskStopped(Me, EventArgs.Empty)
        End Sub

#End Region

#Region "General Methods"

        ''' <summary>
        ''' Start the new operation.
        ''' </summary>
        Public Sub Start(ByVal invokeContext As System.Windows.Forms.Control)
            If IsWorking Then
                Throw New InvalidOperationException("Already in progress.")
            Else
                Me.invokeContext = invokeContext

                ' Create the thread and run it in the background,
                ' so it will terminate automatically if the application ends.
                thread = New Thread(AddressOf StartTaskAsync)
                thread.Name = [String].Format("thread({0})", Guid.NewGuid())
                thread.IsBackground = True

                ' Start the thread.
                thread.Start()

                ' Signal that the task is started.
                OnTaskStarted()

                ' Update status.
                IsWorking = True

                ' Write log message.
                TaskLog = [String].Format("Task Started !!!, The task {0} is started at {1:F}", thread.Name, DateTime.Now)
            End If
        End Sub

        ''' <summary>
        ''' Stops the currently running task.
        ''' </summary>
        Public Sub StopTask()
            ' Perform no operation if task isn't running.
            If Not IsWorking Then
                Return
            End If

            ' Try the polite approach.
            If CancelWaitTime <> TimeSpan.Zero Then
                Dim startTime As DateTime = DateTime.Now

                ' Write log message.
                TaskLog = [String].Format("Task cancellation pending request !!!, The task {0} is aborting now {1:F}", thread.Name, startTime)

                Do
                    System.Windows.Forms.Application.DoEvents()

                    ' Still waiting for the time limit to pass.
                    ' Allow other threads to do some work.
                    thread.Sleep(TimeSpan.FromSeconds(CancelCheckInterval))
                Loop While DateTime.Now.Subtract(startTime).TotalSeconds < CancelWaitTime.Seconds
            End If

            If thread.IsAlive Then
                ' Tell the task thread to abort itself immediately, raises a ThreadAbortException in the task thread after calling the Thread.Join() method.
                thread.Abort()

                ' Make sure it's finished.
                thread.Join()

                ' Signal that the task is stopped.
                OnTaskStopped()
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
                StopTask()
            End If

            MyBase.Dispose(disposing)
        End Sub

#End Region
    End Class

    Public Class ReportTaskProgressEventArgs
        Inherits EventArgs
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
        ''' Gets or Sets the index of the active frame image.
        ''' </summary>
        Public Property GetActiveFrameImage() As Image
            Get
                Return m_GetActiveFrameImage
            End Get
            Set(ByVal value As Image)
                m_GetActiveFrameImage = value
            End Set
        End Property
        Private m_GetActiveFrameImage As Image

#End Region

#Region "Constructor"

        Public Sub New(ByVal image As Image)
            GetActiveFrameImage = image
        End Sub

#End Region
    End Class
End Namespace