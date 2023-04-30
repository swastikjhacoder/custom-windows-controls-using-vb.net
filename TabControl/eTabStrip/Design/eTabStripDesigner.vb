
Imports System.Windows.Forms.Design
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Collections
Imports System.Windows.Forms

Namespace UIControls.TabControl.Design
    Public Class eTabStripDesigner
        Inherits ParentControlDesigner
#Region "Fields"

        Private changeService As IComponentChangeService

#End Region

#Region "Initialize & Dispose"

        Public Overrides Sub Initialize(ByVal component As System.ComponentModel.IComponent)
            MyBase.Initialize(component)

            'Design services
            changeService = DirectCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)

            'Bind design events
            AddHandler changeService.ComponentRemoving, AddressOf OnRemoving

            Verbs.Add(New DesignerVerb("Add TabStrip", New EventHandler(AddressOf OnAddTabStrip)))
            Verbs.Add(New DesignerVerb("Remove TabStrip", New EventHandler(AddressOf OnRemoveTabStrip)))
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            RemoveHandler changeService.ComponentRemoving, AddressOf OnRemoving

            MyBase.Dispose(disposing)
        End Sub

#End Region

#Region "Private Methods"

        Private Sub OnRemoving(ByVal sender As Object, ByVal e As ComponentEventArgs)
            Dim host As IDesignerHost = DirectCast(GetService(GetType(IDesignerHost)), IDesignerHost)

            'Removing a button
            If TypeOf e.Component Is eTabStripItem Then
                Dim itm As eTabStripItem = TryCast(e.Component, eTabStripItem)
                If Control.Items.Contains(itm) Then
                    changeService.OnComponentChanging(Control, Nothing)
                    Control.RemoveTab(itm)
                    changeService.OnComponentChanged(Control, Nothing, Nothing, Nothing)
                    Return
                End If
            End If

            If TypeOf e.Component Is eTabStrip Then
                For i As Integer = Control.Items.Count - 1 To 0 Step -1
                    Dim itm As eTabStripItem = Control.Items(i)
                    changeService.OnComponentChanging(Control, Nothing)
                    Control.RemoveTab(itm)
                    host.DestroyComponent(itm)
                    changeService.OnComponentChanged(Control, Nothing, Nothing, Nothing)
                Next
            End If
        End Sub

        Private Sub OnAddTabStrip(ByVal sender As Object, ByVal e As EventArgs)
            Dim host As IDesignerHost = DirectCast(GetService(GetType(IDesignerHost)), IDesignerHost)
            Dim transaction As DesignerTransaction = host.CreateTransaction("Add TabStrip")
            Dim itm As eTabStripItem = DirectCast(host.CreateComponent(GetType(eTabStripItem)), eTabStripItem)
            changeService.OnComponentChanging(Control, Nothing)
            Control.AddTab(itm)
            Dim indx As Integer = Control.Items.IndexOf(itm) + 1
            itm.Title = "TabStrip Page " + indx.ToString()
            Control.SelectItem(itm)
            changeService.OnComponentChanged(Control, Nothing, Nothing, Nothing)
            transaction.Commit()
        End Sub

        Private Sub OnRemoveTabStrip(ByVal sender As Object, ByVal e As EventArgs)
            Dim host As IDesignerHost = DirectCast(GetService(GetType(IDesignerHost)), IDesignerHost)
            Dim transaction As DesignerTransaction = host.CreateTransaction("Remove Button")
            changeService.OnComponentChanging(Control, Nothing)
            Dim itm As eTabStripItem = Control.Items(Control.Items.Count - 1)
            Control.UnSelectItem(itm)
            Control.Items.Remove(itm)
            changeService.OnComponentChanged(Control, Nothing, Nothing, Nothing)
            transaction.Commit()
        End Sub

#End Region

#Region "Overrides"

        ''' <summary>
        ''' Check HitTest on <see cref="eTabStrip"/> control and
        ''' let the user click on close and menu buttons.
        ''' </summary>
        ''' <param name="point"></param>
        ''' <returns></returns>
        Protected Overrides Function GetHitTest(ByVal point As Point) As Boolean
            Dim result As HitTestResult = Control.HitTest(point)
            If result = HitTestResult.CloseButton OrElse result = HitTestResult.MenuGlyph Then
                Return True
            End If

            Return False
        End Function

        Protected Overrides Sub PreFilterProperties(ByVal properties As IDictionary)
            MyBase.PreFilterProperties(properties)

            properties.Remove("DockPadding")
            properties.Remove("DrawGrid")
            properties.Remove("Margin")
            properties.Remove("Padding")
            properties.Remove("BorderStyle")
            properties.Remove("ForeColor")
            properties.Remove("BackColor")
            properties.Remove("BackgroundImage")
            properties.Remove("BackgroundImageLayout")
            properties.Remove("GridSize")
            properties.Remove("ImeMode")
        End Sub

        Protected Overrides Sub WndProc(ByRef msg As Message)
            If msg.Msg = &H201 Then
                Dim pt As Point = Control.PointToClient(Cursor.Position)
                Dim itm As eTabStripItem = Control.GetTabItemByPoint(pt)
                If itm IsNot Nothing Then
                    Control.SelectedItem = itm
                    Dim selection As New ArrayList()
                    selection.Add(itm)
                    Dim selectionService As ISelectionService = DirectCast(GetService(GetType(ISelectionService)), ISelectionService)
                    selectionService.SetSelectedComponents(selection)
                End If
            End If

            MyBase.WndProc(msg)
        End Sub

        Public Overrides ReadOnly Property AssociatedComponents() As ICollection
            Get
                Return Control.Items
            End Get
        End Property

        Public Overridable Shadows ReadOnly Property Control() As eTabStrip
            Get
                Return TryCast(MyBase.Control, eTabStrip)
            End Get
        End Property

#End Region
    End Class
End Namespace