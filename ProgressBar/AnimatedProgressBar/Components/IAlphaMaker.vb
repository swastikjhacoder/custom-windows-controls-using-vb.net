

Namespace UIControls.ProgressBar.Components
    Public Interface IAlphaMaker
        ''' <summary>
        ''' How many steps to take effect for destination opacity, the StepCount property must be in the range of 5 to 15.
        ''' </summary>
        Property StepCount() As Integer

        ''' <summary>
        ''' Gets or sets, the fade interval, the StepInterval property must be in the range of 10 to 300.
        ''' </summary>
        Property StepInterval() As Integer

        ''' <summary>
        ''' Sets the current float window to layered window or clear its flag.
        ''' </summary>
        ''' <param name="isLayered">Currently float window layered flag</param>
        Sub SetLayered(ByRef isLayered As Boolean)

        ''' <summary>
        ''' Starts a new fading operation for specified opacity.
        ''' </summary>
        ''' <param name="opacity">Alpha value</param>
        Sub SeekToOpacity(ByVal opacity As Byte)

        ''' <summary>
        ''' Updates the float window's opacity.
        ''' </summary>
        ''' <param name="opacity">Alpha value</param>
        ''' <param name="isItRefreshed">If parameter is true, the currently float window will be refreshed; otherwise, does not perform an redraw operation.</param>
        Sub UpdateOpacity(ByVal opacity As Byte, ByVal isItRefreshed As Boolean)

        ''' <summary>
        ''' Gets or sets, the active float window's IFloatWindowAlphaMembers interface.
        ''' </summary>
        Property IFloatWindowControl() As IFloatWindowAlphaMembers

        ''' <summary>
        ''' Occurs when the fading operation is successfully completed.
        ''' </summary>
        Event FadingOperationCompleted As EventHandler
    End Interface
End Namespace
