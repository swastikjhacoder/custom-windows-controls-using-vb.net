
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Namespace UIControls.TextBox.Design
    Friend Class SearchTextBoxDesigner
        Inherits ControlDesigner
        Public Sub New()
            MyBase.AutoResizeHandles = False
        End Sub

        Public Overrides Sub InitializeNewComponent(ByVal defaultValues As IDictionary)
            MyBase.InitializeNewComponent(defaultValues)
            Dim textProperty As PropertyDescriptor = TypeDescriptor.GetProperties(MyBase.Component)("Text")
            If (textProperty IsNot Nothing AndAlso textProperty.PropertyType Is GetType(String)) AndAlso (Not textProperty.IsReadOnly AndAlso textProperty.IsBrowsable) Then
                textProperty.SetValue(MyBase.Component, [String].Empty)
            End If

            Dim cursorProperty As PropertyDescriptor = TypeDescriptor.GetProperties(MyBase.Component)("Cursor")
            If cursorProperty IsNot Nothing AndAlso cursorProperty.PropertyType Is GetType(Cursor) Then
                cursorProperty.SetValue(MyBase.Component, Cursors.IBeam)
            End If

            Dim borderStyleProperty As PropertyDescriptor = TypeDescriptor.GetProperties(MyBase.Component)("BorderStyle")
            If borderStyleProperty IsNot Nothing AndAlso borderStyleProperty.PropertyType Is GetType(BorderStyle) Then
                borderStyleProperty.SetValue(MyBase.Component, BorderStyle.FixedSingle)
            End If
        End Sub

        Protected Overrides Sub PreFilterProperties(ByVal properties As IDictionary)
            MyBase.PreFilterProperties(properties)
            Dim textArray As String() = New String() {"Text"}
            Dim attributes As Attribute() = New Attribute(-1) {}
            For i As Integer = 0 To textArray.Length - 1
                Dim oldPropertyDescriptor As PropertyDescriptor = DirectCast(properties(textArray(i)), PropertyDescriptor)
                If oldPropertyDescriptor IsNot Nothing Then
                    properties(textArray(i)) = TypeDescriptor.CreateProperty(GetType(SearchTextBoxDesigner), oldPropertyDescriptor, attributes)
                End If
            Next
        End Sub

        Private Sub ResetText()
            Me.Control.Text = [String].Empty
        End Sub

        Private Function ShouldSerializeText() As Boolean
            Return TypeDescriptor.GetProperties(GetType(eSearchTextBox))("Text").ShouldSerializeValue(MyBase.Component)
        End Function

        ' Properties
        Public Overrides ReadOnly Property SelectionRules() As SelectionRules
            Get
                Return MyBase.SelectionRules And Not (SelectionRules.BottomSizeable Or SelectionRules.TopSizeable)
            End Get
        End Property

        Private Property Text() As String
            Get
                Return Me.Control.Text
            End Get
            Set(ByVal value As String)
                Me.Control.Text = value
                DirectCast(Me.Control, eSearchTextBox).[Select]()
            End Set
        End Property
    End Class
End Namespace