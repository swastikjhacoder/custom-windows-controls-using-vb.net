
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.ComponentModel
Imports ESAR_Controls.UIControls.ProgressBar.Win32

Namespace UIControls.ProgressBar.Components
    Public Class eFloatWindowAlphaMaker
        Inherits Component
        Implements IAlphaMaker
#Region "Instance Members"

        Private m_stepCount As Integer = 8
        Private m_stepInterval As Integer = 20
        Private initialValue As Integer = 0
        Private incrementValue As Integer = 0
        Private direction As Boolean = False
        Private m_iFloatWindowControl As IFloatWindowAlphaMembers = Nothing
        Private controlTimer As Timer

#End Region

#Region "Constructor"

        Public Sub New()
            controlTimer = New Timer()
            controlTimer.Interval = m_stepInterval
            AddHandler controlTimer.Tick, AddressOf controlTimer_Tick
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

#Region "Virtual Methods"

        Protected Overridable Sub OnFadingOperationCompleted(ByVal e As EventArgs)
            RaiseEvent FadingOperationCompleted(Me, e)
        End Sub

#End Region

#Region "Override Methods"

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                RemoveHandler controlTimer.Tick, AddressOf controlTimer_Tick
                controlTimer.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#End Region

#Region "Helper Methods"

        Private Sub RefreshWindow()
            User32.RedrawWindow(m_iFloatWindowControl.FloatWindowHandle, IntPtr.Zero, IntPtr.Zero, CUInt(User32.RedrawWindowFlags.Frame Or User32.RedrawWindowFlags.AllChildren Or User32.RedrawWindowFlags.Invalidate Or User32.RedrawWindowFlags.UpdateNow))
        End Sub

        Private Sub controlTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            If If(direction, ((initialValue + incrementValue) < CInt(m_iFloatWindowControl.TargetTransparency)), ((initialValue + incrementValue) > CInt(m_iFloatWindowControl.TargetTransparency))) Then
                initialValue += incrementValue
            Else
                initialValue = CInt(m_iFloatWindowControl.TargetTransparency)
            End If

            UpdateOpacity(CByte(initialValue), True)

            If m_iFloatWindowControl.CurrentTransparency = m_iFloatWindowControl.TargetTransparency Then
                ' If operation is completed, stops the timer.
                controlTimer.[Stop]()

                ' Trigger a notification event.
                OnFadingOperationCompleted(EventArgs.Empty)
            End If
        End Sub

#End Region

#Region "IAlphaMaker Members"

        ''' <summary>
        ''' How many steps to take effect for destination opacity, the StepCount property must be in the range of 5 to 15.
        ''' </summary>
        <Description("How many steps to take effect for destination opacity, the StepCount property must be in the range of 5 to 15")> _
        <DefaultValue(8)> _
        <Browsable(True)> _
        Public Property StepCount() As Integer Implements IAlphaMaker.StepCount
            Get
                Return m_stepCount
            End Get
            Set(ByVal value As Integer)
                If Not value.Equals(m_stepCount) Then
                    If value < 5 Then
                        value = 5
                    ElseIf value > 15 Then
                        value = 15
                    End If

                    m_stepCount = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets, the fade interval, the StepInterval property must be in the range of 10 to 300.
        ''' </summary>
        <Description("Gets or sets, the fade interval, the StepInterval property must be in the range of 10 to 300")> _
        <DefaultValue(20)> _
        <Browsable(True)> _
        Public Property StepInterval() As Integer Implements IAlphaMaker.StepInterval
            Get
                Return m_stepInterval
            End Get
            Set(ByVal value As Integer)
                If Not value.Equals(m_stepInterval) Then
                    If value < 10 Then
                        value = 10
                    ElseIf value > 300 Then
                        value = 300
                    End If

                    m_stepInterval = value
                    controlTimer.Interval = m_stepInterval
                End If
            End Set
        End Property

        ''' <summary>
        ''' Sets the current float window to layered window or clear its flag.
        ''' </summary>
        ''' <param name="isLayered">Currently float window layered flag</param>
        Public Sub SetLayered(ByRef isLayered As Boolean) Implements IAlphaMaker.SetLayered
            If m_iFloatWindowControl Is Nothing Then
                Return
            End If

            If Not isLayered Then
                User32.SetWindowLong(m_iFloatWindowControl.FloatWindowHandle, CInt(User32.WindowExStyles.GWL_EXSTYLE), User32.GetWindowLong(m_iFloatWindowControl.FloatWindowHandle, CInt(User32.WindowExStyles.GWL_EXSTYLE)) Xor CInt(User32.WindowExStyles.WS_EX_LAYERED))
                User32.SetLayeredWindowAttributes(m_iFloatWindowControl.FloatWindowHandle, 0, m_iFloatWindowControl.TargetTransparency, User32._LWA_ALPHA)
                m_iFloatWindowControl.CurrentTransparency = m_iFloatWindowControl.TargetTransparency
                isLayered = True
            Else
                User32.SetWindowLong(m_iFloatWindowControl.FloatWindowHandle, CInt(User32.WindowExStyles.GWL_EXSTYLE), User32.GetWindowLong(m_iFloatWindowControl.FloatWindowHandle, CInt(User32.WindowExStyles.GWL_EXSTYLE)) Xor CInt(User32.WindowExStyles.WS_EX_LAYERED))
                m_iFloatWindowControl.CurrentTransparency = 255
                isLayered = False
            End If
        End Sub

        ''' <summary>
        ''' Starts a new fading operation for specified opacity.
        ''' </summary>
        ''' <param name="opacity">Alpha value</param>
        Public Sub SeekToOpacity(ByVal opacity As Byte) Implements IAlphaMaker.SeekToOpacity
            If m_iFloatWindowControl Is Nothing OrElse (m_iFloatWindowControl.CurrentTransparency = opacity) Then
                Return
            End If

            If m_iFloatWindowControl.IsLayered Then
                ' Keeps new target opacity value.
                m_iFloatWindowControl.TargetTransparency = opacity

                If m_iFloatWindowControl.CurrentTransparency < opacity Then
                    direction = True
                Else
                    direction = False
                End If

                initialValue = m_iFloatWindowControl.CurrentTransparency
                incrementValue = (opacity - initialValue) \ m_stepCount
                controlTimer.Interval = m_stepInterval
                controlTimer.Start()
            End If
        End Sub

        ''' <summary>
        ''' Updates the float window's opacity.
        ''' </summary>
        ''' <param name="opacity">Alpha value</param>
        ''' <param name="isItRefreshed">If parameter is true, the currently float window will be refreshed; otherwise, does not perform an redraw operation.</param>
        Public Sub UpdateOpacity(ByVal opacity As Byte, ByVal isItRefreshed As Boolean) Implements IAlphaMaker.UpdateOpacity
            If m_iFloatWindowControl Is Nothing OrElse (m_iFloatWindowControl.CurrentTransparency = opacity) Then
                Return
            End If

            If m_iFloatWindowControl.IsLayered Then
                ' Sets new attributes for float window.
                User32.SetLayeredWindowAttributes(m_iFloatWindowControl.FloatWindowHandle, 0, opacity, User32._LWA_ALPHA)
                ' Sets the currently opacity value of the float window.
                m_iFloatWindowControl.CurrentTransparency = opacity

                If isItRefreshed Then
                    RefreshWindow()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Gets or sets, the active float window's IFloatWindowAlphaMembers interface.
        ''' </summary>
        <Browsable(False)> _
        Public Property IFloatWindowControl() As IFloatWindowAlphaMembers Implements IAlphaMaker.IFloatWindowControl
            Get
                Return m_iFloatWindowControl
            End Get
            Set(ByVal value As IFloatWindowAlphaMembers)
                Try
                    If TypeOf value Is eEasyProgressBar Then
                        If DirectCast(value, eEasyProgressBar).DockUndockProgressBar = False Then
                            If Not value.Equals(m_iFloatWindowControl) Then
                                If m_iFloatWindowControl IsNot Nothing AndAlso controlTimer.Enabled Then
                                    ' Stops the timer and updates window for destination opacity.
                                    controlTimer.[Stop]()
                                    UpdateOpacity(m_iFloatWindowControl.TargetTransparency, True)
                                End If

                                m_iFloatWindowControl = value
                            End If
                        End If
                    End If
                Catch generatedExceptionName As NullReferenceException
                    If m_iFloatWindowControl IsNot Nothing AndAlso controlTimer.Enabled Then
                        ' Stops the timer and updates window for destination opacity.
                        controlTimer.[Stop]()
                        UpdateOpacity(m_iFloatWindowControl.TargetTransparency, True)
                    End If

                    m_iFloatWindowControl = Nothing
                End Try
            End Set
        End Property

        Private Function ShouldSerializeIFloatWindowControl() As Boolean
            Return False
        End Function

        ''' <summary>
        ''' Occurs when the fading operation is successfully completed.
        ''' </summary>
        <Description("Occurs when the fading operation is successfully completed")> _
        Public Event FadingOperationCompleted As EventHandler Implements IAlphaMaker.FadingOperationCompleted

#End Region
    End Class
End Namespace
