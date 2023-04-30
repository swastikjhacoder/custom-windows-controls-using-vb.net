Imports System.Windows.Forms

Namespace UIControls.GroupBox
    ''' <summary>
    ''' This class oversees the checkin/unchecking when mixing RadioButton objects with RadioGroupBox objects within the same container.
    ''' </summary>
    Public Class eRadioButtonPanel
        Inherits Windows.Forms.Panel
        ''' <summary>
        ''' RadioButtonPanel public constructor.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Hooks Check callback events of RadioButton objects within the panel to the RadioButtonPanel object.
        ''' </summary>
        Public Sub AddCheckEventListeners()
            For Each control As Control In Me.Controls
                Dim radioButton As RadioButton = TryCast(control, RadioButton)
                If radioButton IsNot Nothing Then
                    AddHandler radioButton.CheckedChanged, AddressOf radioButton_CheckedChanged
                Else
                    Dim radioGroupBox As eRadioGroupBox = TryCast(control, eRadioGroupBox)
                    If radioGroupBox IsNot Nothing Then
                        AddHandler radioGroupBox.CheckedChanged, AddressOf radioGroupBox_CheckedChanged
                    End If
                End If
            Next
        End Sub

        ''' <summary>
        ''' Event callback called when a RadioButton's Check property is changed.
        ''' </summary>
        ''' <param name="sender">Object(RadioButton)</param>
        ''' <param name="e">EventArgs</param>
        Public Sub radioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            HandleRadioButtonClick(DirectCast(sender, Control))
        End Sub

        ''' <summary>
        ''' Event callback called when a RadioGroupBox's Check property is changed.
        ''' </summary>
        ''' <param name="sender">Object(RadioGroupBox)</param>
        ''' <param name="e">EventArgs</param>
        Public Sub radioGroupBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            HandleRadioButtonClick(DirectCast(sender, Control))
        End Sub

        Private Sub HandleRadioButtonClick(ByVal clickedControl As Control)
            If clickedControl Is Nothing Then
                Return
            End If

            If TypeOf clickedControl.Parent Is eRadioGroupBox Then
                ' If a RadioGroupBox is checked, the sender is the RadioButton,
                ' not the RadioGroupBox, but we need the RadioGroupBox object.
                clickedControl = clickedControl.Parent
            End If

            Dim clickedRadioButton As RadioButton = TryCast(clickedControl, RadioButton)
            If clickedRadioButton IsNot Nothing Then
                If clickedRadioButton.Checked <> True Then
                    ' Only respond to check events that pertain to the control being checked on
                    Return
                End If
            Else
                Dim clickedRadioGroupBox As eRadioGroupBox = TryCast(clickedControl, eRadioGroupBox)
                If clickedRadioGroupBox IsNot Nothing Then
                    If clickedRadioGroupBox.Checked <> True Then
                        ' Only respond to check events that pertain to the control being checked on
                        Return
                    End If
                End If
            End If

            For Each control As Control In Me.Controls
                If control IsNot clickedControl Then
                    Dim radioButton As RadioButton = TryCast(control, RadioButton)
                    If radioButton IsNot Nothing Then
                        ' Normally .NET and WinForms would take care of this but
                        ' we need a mechanism that turns off radio buttons if a
                        ' radio group box is checked.
                        If radioButton.Checked <> False Then
                            radioButton.Checked = False
                        End If
                    Else
                        Dim radioGroupBox As eRadioGroupBox = TryCast(control, eRadioGroupBox)
                        If radioGroupBox IsNot Nothing Then
                            radioGroupBox.Checked = False
                            ' Not expected... must be some other kind of control.
                        Else
                        End If
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
