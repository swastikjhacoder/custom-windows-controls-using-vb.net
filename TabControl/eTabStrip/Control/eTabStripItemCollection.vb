
Imports System.ComponentModel
Imports ESAR_Controls.UIControls.TabControl.Helpers

Namespace UIControls.TabControl
    Public Class eTabStripItemCollection
        Inherits CollectionWithEvents
#Region "Fields"

        <Browsable(False)> _
        Public Event CollectionChanged As CollectionChangeEventHandler

        Private lockUpdate As Integer

#End Region

#Region "Ctor"

        Public Sub New()
            lockUpdate = 0
        End Sub

#End Region

#Region "Props"

        Default Public Property Item(ByVal index As Integer) As eTabStripItem
            Get
                If index < 0 OrElse List.Count - 1 < index Then
                    Return Nothing
                End If

                Return DirectCast(List(index), eTabStripItem)
            End Get
            Set(ByVal value As eTabStripItem)
                List(index) = value
            End Set
        End Property

        <Browsable(False)> _
        Public Overridable ReadOnly Property DrawnCount() As Integer
            Get
                Dim count__1 As Integer = Count, res As Integer = 0
                If count__1 = 0 Then
                    Return 0
                End If
                For n As Integer = 0 To count__1 - 1
                    If Me(n).IsDrawn Then
                        res += 1
                    End If
                Next
                Return res
            End Get
        End Property

        Public Overridable ReadOnly Property LastVisible() As eTabStripItem
            Get
                For n As Integer = Count - 1 To 1 Step -1
                    If Me(n).Visible Then
                        Return Me(n)
                    End If
                Next

                Return Nothing
            End Get
        End Property

        Public Overridable ReadOnly Property FirstVisible() As eTabStripItem
            Get
                For n As Integer = 0 To Count - 1
                    If Me(n).Visible Then
                        Return Me(n)
                    End If
                Next

                Return Nothing
            End Get
        End Property

        <Browsable(False)> _
        Public Overridable ReadOnly Property VisibleCount() As Integer
            Get
                Dim count__1 As Integer = Count, res As Integer = 0
                If count__1 = 0 Then
                    Return 0
                End If
                For n As Integer = 0 To count__1 - 1
                    If Me(n).Visible Then
                        res += 1
                    End If
                Next
                Return res
            End Get
        End Property

#End Region

#Region "Methods"

        Protected Overridable Sub OnCollectionChanged(ByVal e As CollectionChangeEventArgs)
            RaiseEvent CollectionChanged(Me, e)
        End Sub

        Protected Overridable Sub BeginUpdate()
            lockUpdate += 1
        End Sub

        Protected Overridable Sub EndUpdate()
            If System.Threading.Interlocked.Decrement(lockUpdate) = 0 Then
                OnCollectionChanged(New CollectionChangeEventArgs(CollectionChangeAction.Refresh, Nothing))
            End If
        End Sub

        Public Overridable Sub AddRange(ByVal items As eTabStripItem())
            BeginUpdate()
            Try
                For Each item As eTabStripItem In items
                    List.Add(item)
                Next
            Finally
                EndUpdate()
            End Try
        End Sub

        Public Overridable Sub Assign(ByVal collection As eTabStripItemCollection)
            BeginUpdate()
            Try
                Clear()
                For n As Integer = 0 To collection.Count - 1
                    Dim item As eTabStripItem = collection(n)
                    Dim newItem As New eTabStripItem()
                    newItem.Assign(item)
                    Add(newItem)
                Next
            Finally
                EndUpdate()
            End Try
        End Sub

        Public Overridable Function Add(ByVal item As eTabStripItem) As Integer
            Dim res As Integer = IndexOf(item)
            If res = -1 Then
                res = List.Add(item)
            End If
            Return res
        End Function

        Public Overridable Sub Remove(ByVal item As eTabStripItem)
            If List.Contains(item) Then
                List.Remove(item)
            End If
        End Sub

        Public Overridable Function MoveTo(ByVal newIndex As Integer, ByVal item As eTabStripItem) As eTabStripItem
            Dim currentIndex As Integer = List.IndexOf(item)
            If currentIndex >= 0 Then
                RemoveAt(currentIndex)
                Insert(0, item)

                Return item
            End If

            Return Nothing
        End Function

        Public Overloads Function IndexOf(ByVal item As eTabStripItem) As Integer
            Return List.IndexOf(item)
        End Function

        Public Overridable Function Contains(ByVal item As eTabStripItem) As Boolean
            Return List.Contains(item)
        End Function

        Public Overridable Sub Insert(ByVal index As Integer, ByVal item As eTabStripItem)
            If Contains(item) Then
                Return
            End If
            List.Insert(index, item)
        End Sub

        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal item As Object)
            Dim itm As eTabStripItem = TryCast(item, eTabStripItem)
            AddHandler itm.Changed, AddressOf OnItem_Changed
            OnCollectionChanged(New CollectionChangeEventArgs(CollectionChangeAction.Add, item))
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal item As Object)
            MyBase.OnRemove(index, item)
            Dim itm As eTabStripItem = TryCast(item, eTabStripItem)
            RemoveHandler itm.Changed, AddressOf OnItem_Changed
            OnCollectionChanged(New CollectionChangeEventArgs(CollectionChangeAction.Remove, item))
        End Sub

        Protected Overrides Sub OnClear()
            If Count = 0 Then
                Return
            End If
            BeginUpdate()
            Try
                For n As Integer = Count - 1 To 0 Step -1
                    RemoveAt(n)
                Next
            Finally
                EndUpdate()
            End Try
        End Sub

        Protected Overridable Sub OnItem_Changed(ByVal sender As Object, ByVal e As EventArgs)
            OnCollectionChanged(New CollectionChangeEventArgs(CollectionChangeAction.Refresh, sender))
        End Sub

#End Region
    End Class
End Namespace