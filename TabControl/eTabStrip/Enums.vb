
Namespace UIControls.TabControl
    ''' <summary>
    ''' Hit test result of <see cref="eTabStrip"/>
    ''' </summary>
    Public Enum HitTestResult
        CloseButton
        MenuGlyph
        TabItem
        None
    End Enum

    ''' <summary>
    ''' Theme Type
    ''' </summary>
    Public Enum ThemeTypes
        WindowsXP
        Office2000
        Office2003
    End Enum

    ''' <summary>
    ''' Indicates a change into TabStrip collection
    ''' </summary>
    Public Enum eTabStripItemChangeTypes
        Added
        Removed
        Changed
        SelectionChanged
    End Enum
End Namespace