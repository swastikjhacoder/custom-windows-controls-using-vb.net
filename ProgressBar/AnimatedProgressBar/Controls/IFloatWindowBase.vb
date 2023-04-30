
Imports System.Windows.Forms

Namespace UIControls.ProgressBar
    Public Interface IFloatWindowBase
        Inherits IFloatWindowAlphaMembers

        Function SetFloatWindowTaskbarText(ByVal controlHandle As IntPtr, ByVal text As [String]) As Boolean

        Property DockUndockProgressBar() As Boolean

        ReadOnly Property DockParentContainer() As Control
    End Interface

    Public Interface IFloatWindowAlphaMembers

        ReadOnly Property FloatWindowHandle() As IntPtr

        ReadOnly Property IsLayered() As Boolean

        Property TargetTransparency() As Byte

        Property CurrentTransparency() As Byte
    End Interface
End Namespace
