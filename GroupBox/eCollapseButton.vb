'***************************************************************************************************|
'/<summary>
'/ CollapseButton is the base class that manages the mouse movement inside the
'/ CollapseBox. It's behavior is similar to a button.
'/</summary>
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

Namespace UIControls.GroupBox
    Public Class eCollapseButton
        Inherits Control

        Private components As Container

        Public Enum ButtonState
            Normal
            TrackingInside
            TrackingOutside
        End Enum

        Protected m_ButtonState As ButtonState = ButtonState.Normal

        Public Sub New()
            MyBase.New()

            'This call is required by the Component Designer.
            InitializeComponent()

        End Sub
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region " Component Designer generated code "
        Private Sub InitializeComponent()

            AddHandler Resize, AddressOf CollapseButton_Resize
            AddHandler MouseUp, AddressOf CollapseButton_MouseUp
            AddHandler MouseEnter, AddressOf CollapseButton_MouseEnter
            AddHandler MouseMove, AddressOf CollapseButton_MouseMove
            AddHandler MouseLeave, AddressOf CollapseButton_MouseLeave
            AddHandler MouseDown, AddressOf CollapseButton_MouseDown
            AddHandler Paint, AddressOf CollapseButton_Paint

        End Sub
#End Region
        Private Sub CollapseButton_Paint(ByVal sender As Object, _
                                           ByVal e As PaintEventArgs)
            '
            ' needs to be implemented by the derived class
            ''
        End Sub
        Private Sub CollapseButton_MouseDown(ByVal sender As Object, _
                                                ByVal e As MouseEventArgs)

            m_ButtonState = ButtonState.TrackingInside
            Invalidate()

        End Sub

        Private Sub CollapseButton_MouseEnter(ByVal sender As Object, _
                                                ByVal e As EventArgs)

            If m_ButtonState = ButtonState.TrackingOutside Then
                m_ButtonState = ButtonState.TrackingInside
                Invalidate()
            End If

        End Sub

        Private Sub CollapseButton_MouseLeave(ByVal sender As Object, _
                                                ByVal e As EventArgs)

            If m_ButtonState = ButtonState.TrackingInside Then
                m_ButtonState = ButtonState.TrackingOutside
                Invalidate()
            End If

        End Sub

        Private Sub CollapseButton_MouseMove(ByVal sender As Object, _
                                                      ByVal e As MouseEventArgs)

            If m_ButtonState = ButtonState.Normal Then
                Return
            End If

            Dim bounds As Rectangle = New Rectangle(0, 0, Width, Height)

            If m_ButtonState = ButtonState.TrackingInside Then
                If Not bounds.Contains(e.X, e.Y) Then
                    CollapseButton_MouseLeave(sender, e)
                End If
            ElseIf _
                        m_ButtonState = ButtonState.TrackingOutside Then
                If bounds.Contains(e.X, e.Y) Then
                    CollapseButton_MouseEnter(sender, e)
                End If
            End If

        End Sub
        Private Sub CollapseButton_MouseUp(ByVal sender As Object, _
                                                ByVal e As MouseEventArgs)
            If m_ButtonState <> ButtonState.Normal Then
                m_ButtonState = ButtonState.Normal
                Invalidate()

            End If
        End Sub

        Private Sub CollapseButton_Resize(ByVal sender As Object, _
                                            ByVal e As EventArgs)
            Invalidate()
        End Sub

    End Class
End Namespace