Imports System.Drawing
Imports System.Windows.Forms

Namespace UIControls.ComboBox
    Public Class eComboBoxIcon
        Inherits Windows.Forms.ComboBox

        Private ListaImg1 As New ImageList

        Public Property ImageList() As ImageList
            Get
                Return ListaImg1
            End Get
            Set(ByVal ListaImagem As ImageList)
                ListaImg1 = ListaImagem
            End Set
        End Property

        Public Sub New()
            DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        End Sub



        Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
            e.DrawBackground()
            e.DrawFocusRectangle()

            Dim item As New ComboBoxIconItem
            Dim imageSize As New Size
            imageSize = ListaImg1.ImageSize

            Dim bounds As New Rectangle
            bounds = e.Bounds

            Try
                item = Me.Items(e.Index)
                If (item.ImageIndex <> -1) Then
                    Me.ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex)
                    e.Graphics.DrawString(item.Text, e.Font, New SolidBrush(e.ForeColor), bounds.Left + imageSize.Width, bounds.Top)
                Else
                    e.Graphics.DrawString(item.Text, e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
                End If
            Catch ex As Exception
                If (e.Index <> -1) Then
                    e.Graphics.DrawString(Items(e.Index).ToString(), e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
                Else
                    e.Graphics.DrawString(Text, e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
                End If

            End Try
            MyBase.OnDrawItem(e)
        End Sub


    End Class

    Public Class ComboBoxIconItem
        Private _text As String

        Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal Value As String)
                _text = Value
            End Set
        End Property

        Private _imageIndex As Integer

        Property ImageIndex() As Integer
            Get
                Return _imageIndex
            End Get

            Set(ByVal Value As Integer)
                _imageIndex = Value
            End Set
        End Property

        Public Sub New()
            _text = ""
        End Sub

        Public Sub New(ByVal text As String)
            _text = text
        End Sub

        Public Sub New(ByVal text As String, ByVal imageIndex As Integer)
            _text = text
            _imageIndex = imageIndex
        End Sub


        Public Overrides Function ToString() As String
            Return _text
        End Function
    End Class

End Namespace