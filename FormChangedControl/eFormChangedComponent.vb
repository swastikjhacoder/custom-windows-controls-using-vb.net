Imports System.ComponentModel
Imports System.Windows.Forms

Namespace UIControls.eForms
    <ProvideProperty("MonitorForChanges", GetType(Control))> _
    <ProvideProperty("ChangeEventToMonitor", GetType(Control))> _
    <ProvideProperty("ValueNameToMonitor", GetType(Control))> _
    Public Class eFormChangedComponent
        Implements System.ComponentModel.IExtenderProvider

#Region "Private members"
        Private WithEvents _ControlChanged As New ChangeEventMonitoringCollection
#End Region

#Region "Events"
        Public Event FormControlChanged As EventHandler(Of FormChangedEventArgs)
        Private Sub _ControlChanged_MonitoredControlChanged(ByVal sender As Object, ByVal e As FormChangedEventArgs) Handles _ControlChanged.MonitoredControlChanged
            RaiseEvent FormControlChanged(Me, e)
        End Sub
#End Region

#Region "Public interface"
        ''' <summary>
        ''' Resets all the changed flags for the controls on the form
        ''' </summary>
        ''' <remarks>
        ''' You should reset the dirty flags when a record is saved, for example
        ''' </remarks>
        Public Sub ResetDirtyFlags()
            For Each f As ChangeEventMonitoring In _ControlChanged
                f.Changed = False
            Next
        End Sub


        Private Function GetChangeEventName(ByVal ctl As Control) As String
            If TypeOf (ctl) Is Windows.Forms.TextBox Then
                Return "TextChanged"
            ElseIf TypeOf (ctl) Is CheckBox Then
                Return "CheckedChanged"
            ElseIf TypeOf (ctl) Is Windows.Forms.ComboBox Then
                Return "SelectedValueChanged"
            Else
                'Todo : add a thing here for every type of control to monitor
                Return ""
            End If
        End Function

        ''' <summary>
        ''' Returns the collection of controls that have changed since the last reset
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ControlsThatHaveChanged() As Generic.List(Of Control)
            Get
                Dim cRet As New Generic.List(Of Control)
                For Each f As ChangeEventMonitoring In _ControlChanged
                    If f.Changed Then
                        cRet.Add(f.ControlToMonitor)
                    End If
                Next
                Return cRet
            End Get
        End Property


#Region "MonitorForChanges"
        <Category("Change Monitoring")> _
        <Description("Whether or not to monitor this control for changes")> _
        Public Function GetMonitorForChanges(ByVal ctl As Control) As Boolean
            Return _ControlChanged.Item(ctl).Monitor
        End Function

        <Category("Change Monitoring")> _
        <Description("Whether or not to monitor this control for changes")> _
        Public Sub SetMonitorForChanges(ByVal ctl As Control, ByVal value As Boolean)
            _ControlChanged.Item(ctl).Monitor = value
        End Sub
#End Region

#Region "ChangeEventToMonitor"
        <Category("Change Monitoring")> _
        <Description("Which event to monitor this control for changes")> _
        Public Function GetChangeEventToMonitor(ByVal ctl As Control) As String
            Return _ControlChanged.Item(ctl).ChangeEventToMonitor
        End Function

        <Category("Change Monitoring")> _
        <Description("Which event to monitor this control for changes")> _
        Public Sub SetChangeEventToMonitor(ByVal ctl As Control, ByVal ChangeEvent As String)
            If ChangeEvent <> "" Then
                '\\ validate that it is a property of the control
                If ctl.GetType.GetEvent(ChangeEvent) Is Nothing Then
                    Throw New ArgumentException(ChangeEvent & " is not an event of " & ctl.GetType.ToString)
                End If
            End If
            _ControlChanged.Item(ctl).ChangeEventToMonitor = ChangeEvent
        End Sub
#End Region

#Region "ValueNameToMonitor"
        <Category("Change Monitoring")> _
        <Description("Which property to test on this control for changes")> _
        Public Function GetValueNameToMonitor(ByVal ctl As Control) As String
            Return _ControlChanged.Item(ctl).ValueName
        End Function

        <Category("Change Monitoring")> _
        <Description("Which property to test on this control for changes")> _
        Public Sub SetValueNameToMonitor(ByVal ctl As Control, ByVal ValueName As String)
            If ValueName <> "" Then
                '\\ validate that it is a property of the control
                If ctl.GetType.GetProperty(ValueName) Is Nothing Then
                    Throw New ArgumentException(ValueName & " is not a property of " & ctl.GetType.ToString)
                End If
            End If
            _ControlChanged.Item(ctl).ValueName = ValueName
        End Sub
#End Region

#End Region

#Region "IExtenderProvider implementation"
        ''' <summary>
        ''' Returns true if the given control can be extended by this component - i.e. if it can sensibly be printed
        ''' </summary>
        ''' <param name="extendee">The control being queried for extensibility</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CanExtend(ByVal extendee As Object) As Boolean Implements System.ComponentModel.IExtenderProvider.CanExtend

            If TypeOf (extendee) Is Form Then
                Return False
            ElseIf TypeOf (extendee) Is Control Then
                Return True
            End If

        End Function
#End Region


        Private Class ChangeEventMonitoring

#Region "Private members"
            Private _ctlToMonitor As Control
            Private _Monitor As Boolean
            Private _ChangeEventName As String
            Private _ValueName As String
            Private _CheckSum As Integer
            Private _Changed As Boolean
            Private _MonitoringEvent As Boolean
            Private _EventHandler As EventHandler
#End Region

#Region "Events"
            Public Event MonitoredControlChanged As EventHandler(Of FormChangedEventArgs)
#End Region

#Region "Public interface"
            ''' <summary>
            ''' The control that is being monitored for change events
            ''' </summary>
            ''' <value></value>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public ReadOnly Property ControlToMonitor() As Control
                Get
                    Return _ctlToMonitor
                End Get
            End Property

            ''' <summary>
            ''' Whether or not to monitor this control for change events
            ''' </summary>
            ''' <value></value>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Property Monitor() As Boolean
                Get
                    Return _Monitor
                End Get
                Set(ByVal value As Boolean)
                    _Monitor = value
                    Call ResetMonitoringState()
                End Set
            End Property

            ''' <summary>
            ''' The name of the event to monitor for changes
            ''' </summary>
            ''' <value></value>
            ''' <returns></returns>
            ''' <remarks>e.g. for a textbox t
            ''' </remarks>
            Public Property ChangeEventToMonitor() As String
                Get
                    Return _ChangeEventName
                End Get
                Set(ByVal value As String)
                    _ChangeEventName = value
                End Set
            End Property

            ''' <summary>
            ''' The name of the property that exposes the 'value' of the control
            ''' </summary>
            ''' <value></value>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Property ValueName() As String
                Get
                    Return _ValueName
                End Get
                Set(ByVal value As String)
                    _ValueName = value
                End Set
            End Property

            Public Property Changed() As Boolean
                Get
                    Return _Changed
                End Get
                Set(ByVal value As Boolean)
                    If value = False Then
                        _CheckSum = GetCurrentChecksum()
                    End If
                    _Changed = value
                End Set
            End Property
#End Region

#Region "Private methods"

            Private Sub GenericChangeEventhandler(ByVal sender As Object, ByVal e As EventArgs)
                '\\ recalculate this control's checksum so we know if it has changed
                _Changed = Not (_CheckSum = GetCurrentChecksum())
                RaiseEvent MonitoredControlChanged(Me, New FormChangedEventArgs(Me.ControlToMonitor, _Changed))
            End Sub

            Private Function ChangeEventhandler() As [Delegate]
                If _EventHandler Is Nothing Then
                    _EventHandler = New EventHandler(AddressOf GenericChangeEventhandler)
                End If
                Return _EventHandler
            End Function

            Private Function GetCurrentChecksum() As Integer
                Dim _pi As System.Reflection.PropertyInfo
                If _ValueName <> "" Then
                    _pi = _ctlToMonitor.GetType.GetProperty(_ValueName)
                    If Not _pi Is Nothing Then
                        Dim _oVal As Object = _pi.GetValue(_ctlToMonitor, Nothing)
                        If Not _oVal Is Nothing Then
                            Return _oVal.GetHashCode
                        End If
                    Else
                        Return 0
                    End If
                Else
                    Return 0
                End If
            End Function

            Private Sub ResetMonitoringState()
                Dim evi As System.Reflection.EventInfo
                evi = _ctlToMonitor.GetType.GetEvent(_ChangeEventName)

                If _MonitoringEvent Then
                    '\\ Remove the handler from the control
                    If Not (evi Is Nothing) Then
                        '\\ Get the method that adds a handler to the changed event
                        Dim mi As System.Reflection.MethodInfo
                        mi = evi.GetRemoveMethod(False)
                        '\\ add a handler to that changed event
                        mi.Invoke(_ctlToMonitor, New Object() {Me.ChangeEventhandler})
                        _MonitoringEvent = True
                    End If
                End If
                If _Monitor Then
                    If Not (evi Is Nothing) Then
                        '\\ Get the method that adds a handler to the changed event
                        Dim mi As System.Reflection.MethodInfo
                        mi = evi.GetAddMethod(False)
                        '\\ add a handler to that changed event
                        mi.Invoke(_ctlToMonitor, New Object() {Me.ChangeEventhandler})
                        _MonitoringEvent = True
                    End If
                End If
            End Sub
#End Region

#Region "Public constructors"
            Public Sub New(ByVal ctlIn As Control, ByVal MonitorIn As Boolean)
                _ctlToMonitor = ctlIn
                If TypeOf (ctlIn) Is Windows.Forms.TextBox Then
                    _ChangeEventName = "TextChanged"
                    _ValueName = "Text"
                ElseIf TypeOf (ctlIn) Is CheckBox Then
                    _ChangeEventName = "CheckedChanged"
                    _ValueName = "Checked"
                ElseIf TypeOf (ctlIn) Is Windows.Forms.ComboBox Then
                    _ChangeEventName = "SelectedValueChanged"
                    _ValueName = "SelectedValue"
                ElseIf TypeOf (ctlIn) Is Windows.Forms.DateTimePicker Then
                    _ChangeEventName = "ValueChanged"
                    _ValueName = "Value"
                Else
                    'Todo : add a thing here for every type of control to monitor
                    _ChangeEventName = ""
                    _ValueName = ""
                End If
                _CheckSum = GetCurrentChecksum()
                Me.Monitor = MonitorIn
            End Sub
#End Region

        End Class

        Private Class ChangeEventMonitoringCollection
            Inherits Generic.List(Of ChangeEventMonitoring)


#Region "Events"
            Public Event MonitoredControlChanged As EventHandler(Of FormChangedEventArgs)
#End Region

            Public Overloads ReadOnly Property Item(ByVal ctl As Control) As ChangeEventMonitoring
                Get
                    For Each f As ChangeEventMonitoring In Me
                        If f.ControlToMonitor Is ctl Then
                            Return f
                            Exit Property
                        End If
                    Next
                    '\\ control was not found :. add it
                    Dim fret As New ChangeEventMonitoring(ctl, True)
                    Me.Add(fret)
                    Return fret
                End Get
            End Property

            Public Overloads Sub Add(ByVal item As ChangeEventMonitoring)
                MyBase.Add(item)
                AddHandler item.MonitoredControlChanged, AddressOf ControlChangeEventhandler
            End Sub


            Private Sub ControlChangeEventhandler(ByVal sender As Object, ByVal e As FormChangedEventArgs)
                '\\ Raise the event onwards
                RaiseEvent MonitoredControlChanged(Me, e)
            End Sub

        End Class


    End Class

    ''' <summary>
    ''' Event arguments for the event raised when an item on a form is changed
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FormChangedEventArgs
        Inherits EventArgs


#Region "Private members"
        Private _ControlChanged As Control
        Private _Changed As Boolean
#End Region

#Region "Public interface"
        ''' <summary>
        ''' The control on the form that has changed
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ControlChanged() As Control
            Get
                Return _ControlChanged
            End Get
        End Property

        ''' <summary>
        ''' True if the control has changed, false if it has changed back to the last saved value
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Changed() As Boolean
            Get
                Return _Changed
            End Get
        End Property
#End Region

        Public Sub New(ByVal ControlChangedIn As Control, ByVal ChangedIn As Boolean)
            _ControlChanged = ControlChangedIn
            _Changed = ChangedIn
        End Sub
    End Class
End Namespace