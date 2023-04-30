

Namespace UIControls.TabControl
#Region "TabStripItemClosingEventArgs"

    Public Class TabStripItemClosingEventArgs
        Inherits EventArgs
        Public Sub New(ByVal item As eTabStripItem)
            _item = item
        End Sub

        Private _cancel As Boolean = False
        Private _item As eTabStripItem

        Public Property Item() As eTabStripItem
            Get
                Return _item
            End Get
            Set(ByVal value As eTabStripItem)
                _item = value
            End Set
        End Property

        Public Property Cancel() As Boolean
            Get
                Return _cancel
            End Get
            Set(ByVal value As Boolean)
                _cancel = value
            End Set
        End Property

    End Class

#End Region

#Region "TabStripItemChangedEventArgs"

    Public Class TabStripItemChangedEventArgs
        Inherits EventArgs
        Private itm As eTabStripItem
        Private m_changeType As eTabStripItemChangeTypes

        Public Sub New(ByVal item As eTabStripItem, ByVal type As eTabStripItemChangeTypes)
            m_changeType = type
            itm = item
        End Sub

        Public ReadOnly Property ChangeType() As eTabStripItemChangeTypes
            Get
                Return m_changeType
            End Get
        End Property

        Public ReadOnly Property Item() As eTabStripItem
            Get
                Return itm
            End Get
        End Property
    End Class

#End Region

    Public Delegate Sub TabStripItemChangedHandler(ByVal e As TabStripItemChangedEventArgs)
    Public Delegate Sub TabStripItemClosingHandler(ByVal e As TabStripItemClosingEventArgs)

End Namespace