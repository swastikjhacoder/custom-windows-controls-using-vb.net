Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles


Namespace UIControls.GroupBox
    ''' <summary>
    ''' RadioGroupBox is a GroupBox with an embeded RadioButton.
    ''' </summary>
    <ToolboxBitmap(GetType(eRadioGroupBox), "Resources.RadioGroupBox.bmp")> _
    Partial Public Class eRadioGroupBox
        Inherits Windows.Forms.GroupBox
        ' Constants
        Private Const RADIOBUTTON_X_OFFSET As Integer = 10
        Private Const RADIOBUTTON_Y_OFFSET As Integer = -2

        ' Members
        Private m_bDisableChildrenIfUnchecked As Boolean

        ''' <summary>
        ''' RadioGroupBox public constructor.
        ''' </summary>
        Public Sub New()
            Me.InitializeComponent()
            Me.m_bDisableChildrenIfUnchecked = False
            Me.m_radioButton.Parent = Me
            Me.m_radioButton.Location = New System.Drawing.Point(RADIOBUTTON_X_OFFSET, RADIOBUTTON_Y_OFFSET)
            Me.Checked = False

            ' Set the color of the RadioButon's text to the color of the label in a standard groupbox control.
            Dim vsr As New VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal)
            Dim groupBoxTextColor As Color = vsr.GetColor(ColorProperty.TextColor)
            Me.m_radioButton.ForeColor = groupBoxTextColor
        End Sub

#Region "Properties"
        ''' <summary>
        ''' The text associated with the control.
        ''' </summary>
        Public Overrides Property Text() As String
            Get
                If Me.Site IsNot Nothing AndAlso Me.Site.DesignMode = True Then
                    ' Design-time mode
                    Return Me.m_radioButton.Text
                Else
                    ' Run-time
                    ' Set the text of the GroupBox to a space, so the gap appears before the RadioButton.
                    Return " "
                End If
            End Get
            Set(ByVal value As String)
                MyBase.Text = " "
                ' Set the text of the GroupBox to a space, so the gap appears before the RadioButton.
                Me.m_radioButton.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Indicates whether the radio button is checked or not.
        ''' </summary>
        <Description("Indicates whether the radio button is checked or not.")> _
        <Category("Appearance")> _
        <DefaultValue(False)> _
        Public Property Checked() As Boolean
            Get
                Return Me.m_radioButton.Checked
            End Get
            Set(ByVal value As Boolean)
                If Me.m_radioButton.Checked <> value Then
                    Me.m_radioButton.Checked = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Determines if child controls of the GroupBox are disabled when the CheckBox is unchecked.
        ''' </summary>
        <Description("Determines if child controls of the GroupBox are disabled when the RadioButton is unchecked.")> _
        <Category("Appearance")> _
        <DefaultValue(False)> _
        Public Property DisableChildrenIfUnchecked() As Boolean
            Get
                Return Me.m_bDisableChildrenIfUnchecked
            End Get
            Set(ByVal value As Boolean)
                If Me.m_bDisableChildrenIfUnchecked <> value Then
                    Me.m_bDisableChildrenIfUnchecked = value
                End If
            End Set
        End Property
#End Region

#Region "Event Handlers"
        ''' <summary>
        ''' Occurs when the 'checked' property changes value.
        ''' </summary>
        <Description("Occurs when the 'checked' property changes value.")> _
        Public Event CheckedChanged As EventHandler

        '
        ' Summary:
        '     Raises the System.Windows.Forms.RadioButton.checkBox_CheckedChanged event.
        ''' <summary>
        ''' Raises the System.Windows.Forms.
        ''' </summary>
        ''' <param name="e">An System.EventArgs that contains the event data.</param>
        Protected Overridable Sub OnCheckedChanged(ByVal e As EventArgs)
        End Sub
#End Region

#Region "Events"
        Private Sub radioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim radioButton As RadioButton = TryCast(sender, RadioButton)
            If radioButton Is Nothing Then
                Return
            End If

            Dim target As eRadioGroupBox = TryCast(radioButton.Parent, eRadioGroupBox)
            If target Is Nothing Then
                Return
            End If

            If Me.m_bDisableChildrenIfUnchecked = True Then
                Dim bEnabled As Boolean = Me.m_radioButton.Checked
                For Each control As Control In Me.Controls
                    If control IsNot Me.m_radioButton Then
                        control.Enabled = bEnabled
                    End If
                Next
            End If

            If target.Checked = False Then
                Return
            End If

            Dim parentControl As Control = target.Parent
            If parentControl Is Nothing Then
                Return
            End If

            For Each childControl As Control In parentControl.Controls
                If TypeOf childControl Is eRadioGroupBox Then
                    If childControl IsNot Me Then
                        TryCast(childControl, eRadioGroupBox).Checked = False
                    End If
                End If
            Next

            RaiseEvent CheckedChanged(sender, e)
        End Sub

        Private Sub CheckGroupBox_ControlAdded(ByVal sender As Object, ByVal e As ControlEventArgs)
            If Me.m_bDisableChildrenIfUnchecked = True Then
                e.Control.Enabled = Me.Checked
            End If
        End Sub
#End Region
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
