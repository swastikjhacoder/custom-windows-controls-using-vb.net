#Region "Imports..."
Imports System
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
#End Region
#Region "Class eContactDetails..."
Public Class eContactDetails
#Region "Declarations..."
    Dim ctc As New cTextChange()
    Dim emailok As Boolean = False
    Dim websiteok As Boolean = False
#End Region
#Region "Methods..."
    Private Sub LoadCountry()
        Try
            Dim Country As New AutoCompleteStringCollection
            Country.Add("Afghanistan")
            Country.Add("Albania")
            Country.Add("Algeria")
            Country.Add("American Samoa")
            Country.Add("Andorra")
            Country.Add("Angola")
            Country.Add("Anguilla")
            Country.Add("Antigua and Barbuda")
            Country.Add("Argentina")
            Country.Add("Armenia")
            Country.Add("Aruba")
            Country.Add("Australia")
            Country.Add("Austria")
            Country.Add("Azerbaijan")
            Country.Add("Bahamas")
            Country.Add("Bahrain")
            Country.Add("Bangladesh")
            Country.Add("Barbados")
            Country.Add("Belarus")
            Country.Add("Belgium")
            Country.Add("Belize")
            Country.Add("Benin")
            Country.Add("Bermuda")
            Country.Add("Bhutan")
            Country.Add("Bolivia")
            Country.Add("Bosnia-Herzegovina")
            Country.Add("Botswana")
            Country.Add("Bouvet Island")
            Country.Add("Brazil")
            Country.Add("Brunei")
            Country.Add("Bulgaria")
            Country.Add("Burkina Faso")
            Country.Add("Burundi")
            Country.Add("Cambodia")
            Country.Add("Cameroon")
            Country.Add("Canada")
            Country.Add("Cape Verde")
            Country.Add("Cayman Islands")
            Country.Add("Central African Republic")
            Country.Add("Chad")
            Country.Add("Chile")
            Country.Add("China")
            Country.Add("Christmas Island")
            Country.Add("Cocos (Keeling) Islands")
            Country.Add("Colombia")
            Country.Add("Comoros")
            Country.Add("Congo, Democratic Republic of the (Zaire) Africa")
            Country.Add("Congo, Republic of	Africa")
            Country.Add("Cook Islands")
            Country.Add("Costa Rica")
            Country.Add("Croatia")
            Country.Add("Cuba")
            Country.Add("Cyprus")
            Country.Add("Czech Republic")
            Country.Add("Denmark")
            Country.Add("Djibouti")
            Country.Add("Dominica")
            Country.Add("Dominican Republic")
            Country.Add("Ecuador")
            Country.Add("Egypt")
            Country.Add("El Salvador")
            Country.Add("Equatorial Guinea")
            Country.Add("Eritrea")
            Country.Add("Estonia")
            Country.Add("Ethiopia")
            Country.Add("Falkland Islands")
            Country.Add("Faroe Islands")
            Country.Add("Fiji")
            Country.Add("Finland")
            Country.Add("France")
            Country.Add("French Guiana")
            Country.Add("Gabon")
            Country.Add("Gambia")
            Country.Add("Georgia")
            Country.Add("Germany")
            Country.Add("Ghana")
            Country.Add("Gibraltar")
            Country.Add("Greece")
            Country.Add("Greenland")
            Country.Add("Grenada")
            Country.Add("Guadeloupe")
            Country.Add("Guam (USA)")
            Country.Add("Guatemala")
            Country.Add("Guinea")
            Country.Add("Guinea Bissau")
            Country.Add("Guyana")
            Country.Add("Haiti")
            Country.Add("Holy See")
            Country.Add("Honduras")
            Country.Add("Hong Kong")
            Country.Add("Hungary")
            Country.Add("Iceland")
            Country.Add("India")
            Country.Add("Indonesia")
            Country.Add("Iran")
            Country.Add("Iraq")
            Country.Add("Ireland")
            Country.Add("Israel")
            Country.Add("Italy")
            Country.Add("Ivory Coast (Cote D`Ivoire)")
            Country.Add("Jamaica")
            Country.Add("Japan")
            Country.Add("Jordan")
            Country.Add("Kazakhstan")
            Country.Add("Kenya")
            Country.Add("Kiribati")
            Country.Add("Kuwait")
            Country.Add("Kyrgyzstan")
            Country.Add("Laos")
            Country.Add("Latvia")
            Country.Add("Lebanon")
            Country.Add("Lesotho")
            Country.Add("Liberia")
            Country.Add("Libya")
            Country.Add("Liechtenstein")
            Country.Add("Lithuania")
            Country.Add("Luxembourg")
            Country.Add("Macau")
            Country.Add("Macedonia")
            Country.Add("Madagascar")
            Country.Add("Malawi")
            Country.Add("Malaysia")
            Country.Add("Maldives")
            Country.Add("Mali")
            Country.Add("Malta")
            Country.Add("Marshall Islands")
            Country.Add("Martinique (French)")
            Country.Add("Mauritania")
            Country.Add("Mauritius")
            Country.Add("Mayotte")
            Country.Add("Mexico")
            Country.Add("Micronesia")
            Country.Add("Moldova")
            Country.Add("Monaco")
            Country.Add("Mongolia")
            Country.Add("Montenegro")
            Country.Add("Montserrat")
            Country.Add("Morocco")
            Country.Add("Mozambique")
            Country.Add("Myanmar")
            Country.Add("Namibia")
            Country.Add("Nauru")
            Country.Add("Nepal")
            Country.Add("Netherlands")
            Country.Add("Netherlands Antilles")
            Country.Add("New Caledonia (French)")
            Country.Add("New Zealand")
            Country.Add("Nicaragua")
            Country.Add("Niger")
            Country.Add("Nigeria")
            Country.Add("Niue")
            Country.Add("Norfolk Island")
            Country.Add("North Korea")
            Country.Add("Northern Mariana Islands")
            Country.Add("Norway")
            Country.Add("Oman")
            Country.Add("Pakistan")
            Country.Add("Palau")
            Country.Add("Panama")
            Country.Add("Papua New Guinea")
            Country.Add("Paraguay")
            Country.Add("Peru")
            Country.Add("Philippines")
            Country.Add("Pitcairn Island")
            Country.Add("Poland")
            Country.Add("Polynesia (French)")
            Country.Add("Portugal")
            Country.Add("Puerto Rico")
            Country.Add("Qatar")
            Country.Add("Reunion")
            Country.Add("Romania")
            Country.Add("Russia")
            Country.Add("Rwanda")
            Country.Add("Saint Helena")
            Country.Add("Saint Kitts and Nevis")
            Country.Add("Saint Lucia")
            Country.Add("Saint Pierre and Miquelon")
            Country.Add("Saint Vincent and Grenadines")
            Country.Add("Samoa")
            Country.Add("San Marino")
            Country.Add("Sao Tome and Principe")
            Country.Add("Saudi Arabia")
            Country.Add("Senegal")
            Country.Add("Serbia")
            Country.Add("Seychelles")
            Country.Add("Sierra Leone")
            Country.Add("Singapore")
            Country.Add("Slovakia")
            Country.Add("Slovenia")
            Country.Add("Solomon Islands")
            Country.Add("Somalia")
            Country.Add("South Africa")
            Country.Add("South Georgia and South Sandwich Islands")
            Country.Add("South Korea")
            Country.Add("Spain")
            Country.Add("Sri Lanka")
            Country.Add("Sudan")
            Country.Add("Suriname")
            Country.Add("Svalbard and Jan Mayen Islands")
            Country.Add("Swaziland")
            Country.Add("Sweden")
            Country.Add("Switzerland")
            Country.Add("Syria")
            Country.Add("Taiwan")
            Country.Add("Tajikistan")
            Country.Add("Tanzania")
            Country.Add("Thailand")
            Country.Add("Timor-Leste (East Timor)")
            Country.Add("Togo")
            Country.Add("Tokelau")
            Country.Add("Tonga")
            Country.Add("Trinidad and Tobago")
            Country.Add("Tunisia")
            Country.Add("Turkey")
            Country.Add("Turkmenistan")
            Country.Add("Turks and Caicos Islands")
            Country.Add("Tuvalu")
            Country.Add("Uganda")
            Country.Add("Ukraine")
            Country.Add("United Arab Emirates")
            Country.Add("United Kingdom")
            Country.Add("United States")
            Country.Add("Uruguay")
            Country.Add("Uzbekistan")
            Country.Add("Vanuatu")
            Country.Add("Venezuela")
            Country.Add("Vietnam")
            Country.Add("Virgin Islands")
            Country.Add("Wallis and Futuna Islands")
            Country.Add("Yemen")
            Country.Add("Zambia")
            Country.Add("Zimbabwe")
            txtcountry.AutoCompleteMode = AutoCompleteMode.Suggest
            txtcountry.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtcountry.AutoCompleteCustomSource = Country
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadState()
        Try
            Dim State As New AutoCompleteStringCollection
            State.Add("Delhi")
            State.Add("Andhra Pradesh")
            State.Add("Arunachal Pradesh")
            State.Add("Asom (Assam)")
            State.Add("Bihar")
            State.Add("Chhattisgarh")
            State.Add("Goa")
            State.Add("Gujarat")
            State.Add("Haryana")
            State.Add("Himachal Pradesh")
            State.Add("Jammu and Kashmir")
            State.Add("Srinagar")
            State.Add("Jharkhand")
            State.Add("Karnataka")
            State.Add("Kerala")
            State.Add("Madhya Pradesh")
            State.Add("Maharashtra")
            State.Add("Manipur")
            State.Add("Meghalaya")
            State.Add("Mizoram")
            State.Add("Nagaland")
            State.Add("Odisha (Orissa)")
            State.Add("Punjab")
            State.Add("Rajasthan")
            State.Add("Sikkim")
            State.Add("Tamil Nadu")
            State.Add("Tripura")
            State.Add("Uttar Pradesh")
            State.Add("Uttarakhand (Uttaranchal)")
            State.Add("West Bengal")
            txtstate.AutoCompleteMode = AutoCompleteMode.Suggest
            txtstate.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtstate.AutoCompleteCustomSource = State
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UrlIsValid()
        Try
            If txtwebsite.Text.Trim() <> "" Then
                Dim rex As Match = Regex.Match(Trim(txtwebsite.Text), "^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$", RegexOptions.IgnoreCase)
                If rex.Success = False Then
                    websiteok = False
                    Dim result As DialogResult = MessageBox.Show("Please enter a valid web address.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    If result = DialogResult.OK Then
                        txtwebsite.Focus()
                    End If
                    Exit Sub
                Else
                    websiteok = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckEmail()
        Try
            If txtemail.Text.Trim() <> "" Then
                Dim rex As Match = Regex.Match(Trim(txtemail.Text), "^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,3})$", RegexOptions.IgnoreCase)
                If rex.Success = False Then
                    emailok = False
                    Dim result As DialogResult = MessageBox.Show("Please enter a valid e-mail id.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    If result = DialogResult.OK Then
                        txtemail.Focus()
                    End If
                    Exit Sub
                Else
                    emailok = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Attributes..."
    Private Sub txtaddress1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtaddress1.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtaddress2.Focus()
        End If
    End Sub

    Private Sub txtaddress2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtaddress2.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtcity.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtaddress1.Focus()
        End If
    End Sub

    Private Sub txtcity_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcity.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtstate.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtaddress2.Focus()
        End If
    End Sub

    Private Sub txtstate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtstate.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtpincode.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtcity.Focus()
        End If
    End Sub

    Private Sub txtpincode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpincode.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtcountry.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtstate.Focus()
        End If
    End Sub

    Private Sub txtcountry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcountry.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtphone.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtpincode.Focus()
        End If
    End Sub

    Private Sub txtphone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtphone.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtfax.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtcountry.Focus()
        End If
    End Sub

    Private Sub txtfax_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtfax.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtemail.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtphone.Focus()
        End If
    End Sub

    Private Sub txtemail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtemail.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Down Or e.KeyCode = Windows.Forms.Keys.Enter Then
            txtwebsite.Focus()
        ElseIf e.KeyCode = Windows.Forms.Keys.Up Then
            txtfax.Focus()
        End If
    End Sub

    Private Sub TextBoxKeyPressInteger(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfax.KeyPress, txtphone.KeyPress, txtpincode.KeyPress
        If Asc(e.KeyChar) < 32 Then Exit Sub
        If Not (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 58) Then
            e.KeyChar = ChrW(CInt(0))
        End If
        If Asc(e.KeyChar) = 32 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtemail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtemail.KeyPress
        If Asc(e.KeyChar) = 32 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtwebsite_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtwebsite.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Up Then
            txtemail.Focus()
        End If
    End Sub

    Private Sub txtwebsite_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwebsite.KeyPress
        If Asc(e.KeyChar) = 32 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtemail_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtemail.LostFocus
        If txtemail.Text.Trim() <> "" Then
            CheckEmail()
        End If
    End Sub

    Private Sub txtwebsite_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtwebsite.LostFocus
        If txtwebsite.Text.Trim() <> "" Then
            UrlIsValid()
        End If
    End Sub

    Private Sub txtaddress1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaddress1.TextChanged, txtaddress2.TextChanged, txtcity.TextChanged, txtcountry.TextChanged, txtstate.TextChanged
        Dim x As TextBox = CType(sender, TextBox)
        ctc.ConvertTextBoxValidValue(x)
    End Sub
#End Region
#Region "Control Events..."
    Private Sub eContactDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadCountry()
        LoadState()
    End Sub
#End Region
End Class
#End Region