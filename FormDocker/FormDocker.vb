Namespace UIControls.eForms
    Public Class FormDocker

        ''' <summary>
        ''' Constructor for FormDocker.
        ''' </summary>
        ''' <param name="Form">The form to dock.</param>
        ''' <param name="DockMode">Where to dock the form.</param>
        ''' <param name="Padding">The spacing between the form and the edge of the screen.</param>
        ''' <param name="AutoRefreshOnChange">Whether to refresh when a property is changed.</param>
        ''' <param name="RestoreOriginalLocation">Whether to restore the form to its previous location when undocked.</param>
        ''' <remarks></remarks>
        Public Sub New(ByRef Form As Windows.Forms.Form, ByVal DockMode As FormDockMode, ByVal Padding As Windows.Forms.Padding, Optional ByVal AutoRefreshOnChange As Boolean = True, Optional ByVal RestoreOriginalLocation As Boolean = True)
            _Form = Form
            _DockMode = DockMode
            _Dock = True
            _Padding = Padding
            _AutoRefreshOnChange = AutoRefreshOnChange
            _RestoreOriginalLocation = RestoreOriginalLocation
        End Sub

        '--------------
        '--- Events ---
        '--------------

        ''' <summary>
        ''' Occurs after the form's docking has been refreshed.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Event Refreshed(ByVal sender As Object, ByVal e As EventArgs)
        ''' <summary>
        ''' Occurs when the dock mode is changed.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Event DockModeChanged(ByVal sender As Object, ByVal e As EventArgs)
        ''' <summary>
        ''' Occurs when the form is docked or undocked.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Event DockChanged(ByVal sender As Object, ByVal e As EventArgs)

        '-------------
        '--- Enums ---
        '-------------

        ''' <summary>
        ''' Contains the different docking modes.
        ''' </summary>
        ''' <remarks></remarks>
        Enum FormDockMode
            Top
            Bottom
            Left
            Right
            TopRight
            TopLeft
            BottomRight
            BottomLeft
            Fill
        End Enum

        '------------------
        '--- Properties ---
        '------------------

        Private _DockMode As FormDockMode
        ''' <summary>
        ''' Where to dock the form.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DockMode() As FormDockMode
            Get
                Return _DockMode
            End Get
            Set(ByVal value As FormDockMode)
                _DockMode = value
                RaiseEvent DockModeChanged(Me, EventArgs.Empty)
                If _AutoRefreshOnChange Then
                    Refresh()
                End If
            End Set
        End Property

        Private _Dock As Boolean
        ''' <summary>
        ''' Whether the form is docked or not. Set to 'True' to dock the form.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Dock() As Boolean
            Get
                Return _Dock
            End Get
            Set(ByVal value As Boolean)
                _Dock = value
                RaiseEvent DockChanged(Me, EventArgs.Empty)
                If _RestoreOriginalLocation And _Dock = False Then
                    _Form.Location = _OriginalLocation
                End If
                If _Dock = True And _RestoreOriginalLocation Then
                    _OriginalLocation = _Form.Location
                End If
                If _AutoRefreshOnChange Then
                    Refresh()
                End If
            End Set
        End Property

        Private _Form As Windows.Forms.Form
        ''' <summary>
        ''' The form to dock.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Form() As Windows.Forms.Form
            Get
                Return _Form
            End Get
            Set(ByVal value As Windows.Forms.Form)
                _Form = value
                If _AutoRefreshOnChange Then
                    Refresh()
                End If
            End Set
        End Property

        Private _Padding As Windows.Forms.Padding
        ''' <summary>
        ''' The spacing between the form and the edge of the screen.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Padding() As Windows.Forms.Padding
            Get
                Return _Padding
            End Get
            Set(ByVal value As Windows.Forms.Padding)
                _Padding = value
                If AutoRefreshOnChange = True Then
                    Refresh()
                End If
            End Set
        End Property

        Private _AutoRefreshOnChange As Boolean
        ''' <summary>
        ''' Whether to refresh when a property is changed.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AutoRefreshOnChange() As Boolean
            Get
                Return _AutoRefreshOnChange
            End Get
            Set(ByVal value As Boolean)
                _AutoRefreshOnChange = value
            End Set
        End Property

        Private _OriginalLocation As Drawing.Point
        ''' <summary>
        ''' The original location to restore to.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property OriginalLocation() As Drawing.Point
            Get
                Return _OriginalLocation
            End Get
        End Property

        Private _RestoreOriginalLocation As Boolean
        ''' <summary>
        ''' Whether to restore the form to its previous location when undocked.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property RestoreOriginalLocation() As Boolean
            Get
                Return _RestoreOriginalLocation
            End Get
            Set(ByVal value As Boolean)
                _RestoreOriginalLocation = value
            End Set
        End Property

        '------------
        '--- Subs ---
        '------------

        ''' <summary>
        ''' Refreshes the docking. (Re-positions the form)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Refresh()
            If _Dock = True Then
                Select Case _DockMode
                    Case FormDockMode.Top
                        _Form.Top = 0 + Padding.Top
                        _Form.Left = 0 + Padding.Left
                        _Form.Width = My.Computer.Screen.WorkingArea.Width - Padding.Right - Padding.Left
                        _Form.Height = _Form.Height
                    Case FormDockMode.Bottom
                        _Form.Top = My.Computer.Screen.WorkingArea.Height - _Padding.Bottom - _Form.Height
                        _Form.Left = 0 + Padding.Left
                        _Form.Width = My.Computer.Screen.WorkingArea.Width - Padding.Right - Padding.Left
                        _Form.Height = _Form.Height
                    Case FormDockMode.Left
                        _Form.Top = 0 + Padding.Top
                        _Form.Left = 0 + Padding.Left
                        _Form.Width = _Form.Width
                        _Form.Height = My.Computer.Screen.WorkingArea.Height - Padding.Bottom - Padding.Top
                    Case FormDockMode.Right
                        _Form.Top = 0 + Padding.Top
                        _Form.Left = My.Computer.Screen.WorkingArea.Width - _Form.Width - Padding.Right
                        _Form.Width = _Form.Width
                        _Form.Height = My.Computer.Screen.WorkingArea.Height - Padding.Bottom - Padding.Top
                    Case FormDockMode.Fill
                        _Form.Top = 0 + Padding.Top
                        _Form.Left = 0 + Padding.Left
                        _Form.Width = My.Computer.Screen.WorkingArea.Width - Padding.Right - Padding.Left
                        _Form.Height = My.Computer.Screen.WorkingArea.Height - Padding.Bottom - Padding.Top
                    Case FormDockMode.TopLeft
                        _Form.Top = 0 + Padding.Top
                        _Form.Left = 0 + Padding.Left
                    Case FormDockMode.TopRight
                        _Form.Top = 0 + Padding.Top
                        _Form.Left = My.Computer.Screen.WorkingArea.Width - _Form.Width - Padding.Right
                    Case FormDockMode.BottomLeft
                        _Form.Top = My.Computer.Screen.WorkingArea.Height - _Form.Height - Padding.Bottom
                        _Form.Left = 0 + Padding.Left
                    Case FormDockMode.BottomRight
                        _Form.Top = My.Computer.Screen.WorkingArea.Height - _Form.Height - Padding.Bottom
                        _Form.Left = My.Computer.Screen.WorkingArea.Width - _Form.Width - Padding.Right
                End Select
            End If
            RaiseEvent Refreshed(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Docks the form.
        ''' </summary>
        ''' <remarks>Equivalent to Dock = True</remarks>
        Public Sub DockForm()
            Dock = True
            Refresh()
        End Sub

        ''' <summary>
        ''' Undocks the form.
        ''' </summary>
        ''' <remarks>Equivalent to Dock = False</remarks>
        Public Sub UndockForm()
            Dock = False
            Refresh()
        End Sub

        '-----------------
        '--- Functions ---
        '-----------------

        Public Overrides Function ToString() As String
            Dim Str As String
            Str = "Docked: " & Dock & ", Dock Mode: " & DockMode.ToString
            Return Str
        End Function

    End Class

End Namespace