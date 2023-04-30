
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace UIControls.ProgressBar
    ''' <summary>
    ''' Please be carefull to use this tool for your controls. Call the ResumeLogic() method after the SuspendLogic() method at the same control.
    ''' </summary>
    Public NotInheritable Class CustomControlsLogic
        Private Sub New()
        End Sub
#Region "Symbolic Constants"

        Private Const WM_USER As Integer = &H400
        Private Const WM_SETREDRAW As Integer = &HB
        Private Const EM_GETEVENTMASK As Integer = (WM_USER + 59)
        Private Const EM_SETEVENTMASK As Integer = (WM_USER + 69)

#End Region

#Region "Interop"

        <DllImport("user32")> _
        Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Boolean, ByVal lParam As Int32) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)> _
        Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
        End Function

#End Region

        Private Shared EVENTMASK As IntPtr = IntPtr.Zero

#Region "General Methods"

        ''' <summary>
        ''' Stops painting messages and events.
        ''' </summary>
        ''' <param name="parent">Parent control handle</param>
        Public Shared Sub SuspendLogic(ByVal parent As Control)
            ' Stop redrawing:
            SendMessage(parent.Handle, WM_SETREDRAW, 0, IntPtr.Zero)

            ' Stop sending of events:
            EVENTMASK = SendMessage(parent.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero)
        End Sub

        ''' <summary>
        ''' Starts painting messages and events.
        ''' </summary>
        ''' <param name="parent">Parent control handle</param>
        Public Shared Sub ResumeLogic(ByVal parent As Control)
            ' Turn-On events:
            SendMessage(parent.Handle, EM_SETEVENTMASK, 0, EVENTMASK)

            ' Turn-On redrawing:
            SendMessage(parent.Handle, WM_SETREDRAW, 1, IntPtr.Zero)

            parent.Refresh()
        End Sub

        ''' <summary>
        ''' Creates a new instance of the specified type using that type's allowed properties.
        ''' </summary>
        ''' <param name="source">Source Object (To be cloned.)</param>
        ''' <returns>Returns created new object</returns>
        Public Shared Function GetMyClone(ByVal source As [Object]) As [Object]
            ' Grab the type of the source instance.
            Dim sourceType As Type = source.[GetType]()

            ' Firstly, we create a new instance using that type's default constructor.
            Dim newObject As Object = Activator.CreateInstance(sourceType, False)

            For Each pi As System.Reflection.PropertyInfo In sourceType.GetProperties()
                If pi.CanWrite Then
                    ' Gets custom attribute for the current property item.
                    Dim attribute As IsCloneableAttribute = GetAttributeForSpecifiedProperty(pi)
                    If attribute Is Nothing Then
                        Continue For
                    End If

                    If attribute.IsCloneable Then
                        ' We query if the property support the ICloneable interface.
                        Dim ICloneType As Type = pi.PropertyType.GetInterface("ICloneable", True)
                        If ICloneType IsNot Nothing Then
                            ' Getting the ICloneable interface from the object.
                            Dim IClone As ICloneable = DirectCast(pi.GetValue(source, Nothing), ICloneable)
                            If IClone IsNot Nothing Then
                                ' We use the Clone() method to set the new value to the property.
                                pi.SetValue(newObject, IClone.Clone(), Nothing)
                            Else
                                ' If return value is null, just set it null.
                                pi.SetValue(newObject, Nothing, Nothing)
                            End If
                        Else
                            ' If the property doesn't support the ICloneable interface then just set it.
                            pi.SetValue(newObject, pi.GetValue(source, Nothing), Nothing)
                        End If
                    End If
                End If
            Next

            Return newObject
        End Function

#End Region

#Region "Helper Methods"

        Private Shared Function GetAttributeForSpecifiedProperty(ByVal element As System.Reflection.MemberInfo) As IsCloneableAttribute
            Dim attribute__1 As IsCloneableAttribute = DirectCast(Attribute.GetCustomAttribute(element, GetType(IsCloneableAttribute)), IsCloneableAttribute)
            Return attribute__1
        End Function

#End Region
    End Class

    ''' <summary>
    ''' To apply clone support for specific property, please use this tool.
    ''' </summary>
    <AttributeUsage(AttributeTargets.[Property], AllowMultiple:=False)> _
    Public Class IsCloneableAttribute
        Inherits Attribute
#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

        ' Attribute constructor for positional parameters.
        Public Sub New(ByVal isCloneable As Boolean)
            Me.IsCloneable = isCloneable
        End Sub

        ''' <summary>
        ''' Determines whether the current property is cloneable or not.
        ''' </summary>
        Public Property IsCloneable() As Boolean
            Get
                Return m_IsCloneable
            End Get
            Private Set(ByVal value As Boolean)
                m_IsCloneable = value
            End Set
        End Property
        Private m_IsCloneable As Boolean
    End Class
End Namespace