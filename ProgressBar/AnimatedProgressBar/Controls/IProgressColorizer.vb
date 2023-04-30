

Namespace UIControls.ProgressBar
    Public Interface IProgressColorizer
        Inherits IDisposable
        ''' <summary>
        ''' Determines whether the colorizer effect is enable or not for progress bitmap.
        ''' </summary>
        Overloads Property IsColorizerEnabled() As Boolean

        ''' <summary>
        ''' Determines whether the transparency effect is visible or not for progress bitmap.
        ''' </summary>
        Overloads Property IsTransparencyEnabled() As Boolean

        ''' <summary>
        ''' Gets or Sets, the red color component value of the progress bitmap.
        ''' </summary>
        Overloads Property Red() As Byte

        ''' <summary>
        ''' Gets or Sets, the green color component value of the progress bitmap.
        ''' </summary>
        Overloads Property Green() As Byte

        ''' <summary>
        ''' Gets or Sets, the blue color component value of the progress bitmap.
        ''' </summary>
        Overloads Property Blue() As Byte

        ''' <summary>
        ''' Gets or Sets, the alpha color component value of the progress bitmap.
        ''' </summary>
        Overloads Property Alpha() As Byte
    End Interface
End Namespace