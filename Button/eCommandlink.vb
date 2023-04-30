Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.ComponentModel

Namespace UIControls.Button
    <ToolboxBitmap(GetType(Windows.Forms.Button))> _
    Public Class eCommandLink
        Inherits System.Windows.Forms.Button
        Public Sub New()
            Me.FlatStyle = FlatStyle.System
        End Sub

        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                cp.Style = cp.Style Or UIControls.Button.VistaConstants.BS_COMMANDLINK
                Return cp
            End Get
        End Property
        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
        End Function
        Private useicon As [Boolean] = True
        'Checks if user wants to use an icon instead of a bitmap
        Private image_ As Bitmap
        'Image alignment is ignored at the moment. Property overrides inherited image property
        'Supports images other than bitmap, supports transparency on .NET 2.0
        <Description("Gets or sets the image that is displayed on a button control."), Category("Appearance"), DefaultValue("")> _
        Public Shadows Property Image() As Bitmap
            Get
                Return image_
            End Get
            Set(ByVal value As Bitmap)
                image_ = value
                If value IsNot Nothing Then
                    Me.useicon = False
                    Me.Icon = Nothing
                End If
                Me.SetShield(False)
                SetImage()
            End Set
        End Property
        Private icon_ As Icon
        <Description("Gets or sets the icon that is displayed on a button control."), Category("Appearance"), DefaultValue("")> _
        Public Property Icon() As Icon
            Get
                Return icon_
            End Get
            Set(ByVal value As Icon)
                icon_ = value
                If icon_ IsNot Nothing Then
                    Me.useicon = True
                End If
                Me.SetShield(False)
                SetImage()
            End Set
        End Property
        <Description("Refreshes the image displayed on the button.")> _
        Public Sub SetImage()
            Dim iconhandle As IntPtr = IntPtr.Zero
            If Not Me.useicon Then
                If Me.image_ IsNot Nothing Then
                    'Gets the handle of the bitmap
                    iconhandle = image_.GetHicon()
                End If
            Else
                If Me.icon_ IsNot Nothing Then
                    iconhandle = Me.Icon.Handle
                End If
            End If
            'Set the button to use the icon. If no icon or bitmap is used, no image is set.
            SendMessage(Me.Handle, UIControls.Button.VistaConstants.BM_SETIMAGE, 1, CInt(iconhandle))
        End Sub
        Private showshield_ As [Boolean] = False
        <Description("Gets or sets whether if the control should use an elevated shield icon."), Category("Appearance"), DefaultValue(False)> _
        Public Property ShowShield() As [Boolean]
            Get
                Return showshield_
            End Get
            Set(ByVal value As [Boolean])
                showshield_ = value
                Me.SetShield(value)
                If Not value Then
                    Me.SetImage()
                End If
            End Set
        End Property
        Public Sub SetShield(ByVal Value As [Boolean])
            UIControls.Button.VistaConstants.SendMessage(Me.Handle, UIControls.Button.VistaConstants.BCM_SETSHIELD, IntPtr.Zero, New IntPtr(If(showshield_, 1, 0)))
        End Sub
        <Description("Gets or sets the note that is displayed on a button control."), Category("Appearance"), DefaultValue("")> _
        Private note_ As String = ""
        Public Property Note() As String
            Get
                Return Me.note_
            End Get
            Set(ByVal value As String)
                Me.note_ = value
                Me.SetNote(Me.note_)
            End Set
        End Property
        <Description("Sets the note displayed on the button.")> _
        Private Sub SetNote(ByVal NoteText As String)
            'Sets the note
            UIControls.Button.VistaConstants.SendMessage(Me.Handle, UIControls.Button.VistaConstants.BCM_SETNOTE, IntPtr.Zero, NoteText)
        End Sub

    End Class
End Namespace