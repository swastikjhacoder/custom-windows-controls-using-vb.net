
Imports System.Collections
Imports System.ComponentModel

Namespace UIControls.TabControl.Helpers
    ''' <summary>
    ''' Represents the method that will handle the event that has no data.
    ''' </summary>
    Public Delegate Sub CollectionClear()

    ''' <summary>
    ''' Represents the method that will handle the event that has item data.
    ''' </summary>
    Public Delegate Sub CollectionChange(ByVal index As Integer, ByVal value As Object)

    ''' <summary>
    ''' Extend collection base class by generating change events.
    ''' </summary>
    Public MustInherit Class CollectionWithEvents
        Inherits CollectionBase
        ' Instance fields
        Private _suspendCount As Integer

        ''' <summary>
        ''' Occurs just before the collection contents are cleared.
        ''' </summary>
        <Browsable(False)> _
        Public Event Clearing As CollectionClear

        ''' <summary>
        ''' Occurs just after the collection contents are cleared.
        ''' </summary>
        <Browsable(False)> _
        Public Event Cleared As CollectionClear

        ''' <summary>
        ''' Occurs just before an item is added to the collection.
        ''' </summary>
        <Browsable(False)> _
        Public Event Inserting As CollectionChange

        ''' <summary>
        ''' Occurs just after an item has been added to the collection.
        ''' </summary>
        <Browsable(False)> _
        Public Event Inserted As CollectionChange

        ''' <summary>
        ''' Occurs just before an item is removed from the collection.
        ''' </summary>
        <Browsable(False)> _
        Public Event Removing As CollectionChange

        ''' <summary>
        ''' Occurs just after an item has been removed from the collection.
        ''' </summary>
        <Browsable(False)> _
        Public Event Removed As CollectionChange

        ''' <summary>
        ''' Initializes DrawTab new instance of the CollectionWithEvents class.
        ''' </summary>
        Public Sub New()
            ' Default to not suspended
            _suspendCount = 0
        End Sub

        ''' <summary>
        ''' Do not generate change events until resumed.
        ''' </summary>
        Public Sub SuspendEvents()
            _suspendCount += 1
        End Sub

        ''' <summary>
        ''' Safe to resume change events.
        ''' </summary>
        Public Sub ResumeEvents()
            _suspendCount -= 1
        End Sub

        ''' <summary>
        ''' Gets DrawTab value indicating if events are currently suspended.
        ''' </summary>
        <Browsable(False)> _
        Public ReadOnly Property IsSuspended() As Boolean
            Get
                Return (_suspendCount > 0)
            End Get
        End Property

        ''' <summary>
        ''' Raises the Clearing event when not suspended.
        ''' </summary>
        Protected Overrides Sub OnClear()
            If Not IsSuspended Then
                ' Any attached event handlers?
                RaiseEvent Clearing()
            End If
        End Sub

        ''' <summary>
        ''' Raises the Cleared event when not suspended.
        ''' </summary>
        Protected Overrides Sub OnClearComplete()
            If Not IsSuspended Then
                ' Any attached event handlers?
                RaiseEvent Cleared()
            End If
        End Sub

        ''' <summary>
        ''' Raises the Inserting event when not suspended.
        ''' </summary>
        ''' <param name="index">Index of object being inserted.</param>
        ''' <param name="value">The object that is being inserted.</param>
        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not IsSuspended Then
                ' Any attached event handlers?
                RaiseEvent Inserting(index, value)
            End If
        End Sub

        ''' <summary>
        ''' Raises the Inserted event when not suspended.
        ''' </summary>
        ''' <param name="index">Index of inserted object.</param>
        ''' <param name="value">The object that has been inserted.</param>
        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
            If Not IsSuspended Then
                ' Any attached event handlers?
                RaiseEvent Inserted(index, value)
            End If
        End Sub

        ''' <summary>
        ''' Raises the Removing event when not suspended.
        ''' </summary>
        ''' <param name="index">Index of object being removed.</param>
        ''' <param name="value">The object that is being removed.</param>
        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not IsSuspended Then
                ' Any attached event handlers?
                RaiseEvent Removing(index, value)
            End If
        End Sub

        ''' <summary>
        ''' Raises the Removed event when not suspended.
        ''' </summary>
        ''' <param name="index">Index of removed object.</param>
        ''' <param name="value">The object that has been removed.</param>
        Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
            If Not IsSuspended Then
                ' Any attached event handlers?
                RaiseEvent Removed(index, value)
            End If
        End Sub

        ''' <summary>
        ''' Returns the index of the first occurrence of DrawTab value.
        ''' </summary>
        ''' <param name="value">The object to locate.</param>
        ''' <returns>Index of object; otherwise -1</returns>
        Protected Function IndexOf(ByVal value As Object) As Integer
            ' Find the 0 based index of the requested entry
            Return List.IndexOf(value)
        End Function
    End Class
End Namespace