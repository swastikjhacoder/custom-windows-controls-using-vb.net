<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class eContactDetails
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtwebsite = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtemail = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtfax = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtphone = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtcountry = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtpincode = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtstate = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtcity = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtaddress2 = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.txtaddress1 = New ESAR_Controls.UIControls.TextBox.eTextBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Address Info:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(0, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Contact Info:"
        '
        'txtwebsite
        '
        Me.txtwebsite.BackColor = System.Drawing.Color.White
        Me.txtwebsite.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.Http
        Me.txtwebsite.Location = New System.Drawing.Point(196, 161)
        Me.txtwebsite.Name = "txtwebsite"
        Me.txtwebsite.Required = False
        Me.txtwebsite.ShowErrorIcon = False
        Me.txtwebsite.Size = New System.Drawing.Size(185, 21)
        Me.txtwebsite.TabIndex = 9
        Me.txtwebsite.TextBox = ""
        Me.txtwebsite.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtwebsite.WaterMark = "Enter website address..."
        Me.txtwebsite.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtwebsite.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtwebsite.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtemail
        '
        Me.txtemail.BackColor = System.Drawing.Color.White
        Me.txtemail.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.Email
        Me.txtemail.Location = New System.Drawing.Point(3, 161)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Required = False
        Me.txtemail.ShowErrorIcon = False
        Me.txtemail.Size = New System.Drawing.Size(187, 21)
        Me.txtemail.TabIndex = 8
        Me.txtemail.TextBox = ""
        Me.txtemail.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtemail.WaterMark = "Enter e-mail id..."
        Me.txtemail.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtemail.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtfax
        '
        Me.txtfax.BackColor = System.Drawing.Color.White
        Me.txtfax.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtfax.Location = New System.Drawing.Point(196, 134)
        Me.txtfax.Name = "txtfax"
        Me.txtfax.Required = False
        Me.txtfax.ShowErrorIcon = False
        Me.txtfax.Size = New System.Drawing.Size(185, 21)
        Me.txtfax.TabIndex = 7
        Me.txtfax.TextBox = ""
        Me.txtfax.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtfax.WaterMark = "Enter fax number..."
        Me.txtfax.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtfax.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfax.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtphone
        '
        Me.txtphone.BackColor = System.Drawing.Color.White
        Me.txtphone.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtphone.Location = New System.Drawing.Point(3, 134)
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Required = False
        Me.txtphone.ShowErrorIcon = False
        Me.txtphone.Size = New System.Drawing.Size(187, 21)
        Me.txtphone.TabIndex = 6
        Me.txtphone.TextBox = ""
        Me.txtphone.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtphone.WaterMark = "Enter phone number..."
        Me.txtphone.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtphone.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtcountry
        '
        Me.txtcountry.BackColor = System.Drawing.Color.White
        Me.txtcountry.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtcountry.Location = New System.Drawing.Point(128, 94)
        Me.txtcountry.Name = "txtcountry"
        Me.txtcountry.Required = False
        Me.txtcountry.ShowErrorIcon = False
        Me.txtcountry.Size = New System.Drawing.Size(253, 21)
        Me.txtcountry.TabIndex = 5
        Me.txtcountry.TextBox = ""
        Me.txtcountry.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtcountry.WaterMark = "Enter country..."
        Me.txtcountry.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtcountry.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcountry.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtpincode
        '
        Me.txtpincode.BackColor = System.Drawing.Color.White
        Me.txtpincode.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtpincode.Location = New System.Drawing.Point(3, 94)
        Me.txtpincode.Name = "txtpincode"
        Me.txtpincode.Required = False
        Me.txtpincode.ShowErrorIcon = False
        Me.txtpincode.Size = New System.Drawing.Size(119, 21)
        Me.txtpincode.TabIndex = 4
        Me.txtpincode.TextBox = ""
        Me.txtpincode.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtpincode.WaterMark = "Enter pin code..."
        Me.txtpincode.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtpincode.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpincode.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtstate
        '
        Me.txtstate.BackColor = System.Drawing.Color.White
        Me.txtstate.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtstate.Location = New System.Drawing.Point(196, 68)
        Me.txtstate.Name = "txtstate"
        Me.txtstate.Required = False
        Me.txtstate.ShowErrorIcon = False
        Me.txtstate.Size = New System.Drawing.Size(185, 21)
        Me.txtstate.TabIndex = 3
        Me.txtstate.TextBox = ""
        Me.txtstate.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtstate.WaterMark = "Enter state..."
        Me.txtstate.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtstate.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstate.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtcity
        '
        Me.txtcity.BackColor = System.Drawing.Color.White
        Me.txtcity.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtcity.Location = New System.Drawing.Point(3, 68)
        Me.txtcity.Name = "txtcity"
        Me.txtcity.Required = False
        Me.txtcity.ShowErrorIcon = False
        Me.txtcity.Size = New System.Drawing.Size(187, 21)
        Me.txtcity.TabIndex = 2
        Me.txtcity.TextBox = ""
        Me.txtcity.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtcity.WaterMark = "Enter city..."
        Me.txtcity.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtcity.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcity.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtaddress2
        '
        Me.txtaddress2.BackColor = System.Drawing.Color.White
        Me.txtaddress2.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtaddress2.Location = New System.Drawing.Point(3, 42)
        Me.txtaddress2.Name = "txtaddress2"
        Me.txtaddress2.Required = False
        Me.txtaddress2.ShowErrorIcon = False
        Me.txtaddress2.Size = New System.Drawing.Size(378, 21)
        Me.txtaddress2.TabIndex = 1
        Me.txtaddress2.TextBox = ""
        Me.txtaddress2.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtaddress2.WaterMark = "Enter address line 2..."
        Me.txtaddress2.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtaddress2.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress2.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'txtaddress1
        '
        Me.txtaddress1.BackColor = System.Drawing.Color.White
        Me.txtaddress1.LinkType = ESAR_Controls.UIControls.TextBox.LinkTypes.None
        Me.txtaddress1.Location = New System.Drawing.Point(3, 16)
        Me.txtaddress1.Name = "txtaddress1"
        Me.txtaddress1.Required = False
        Me.txtaddress1.ShowErrorIcon = False
        Me.txtaddress1.Size = New System.Drawing.Size(378, 21)
        Me.txtaddress1.TabIndex = 0
        Me.txtaddress1.TextBox = ""
        Me.txtaddress1.ValidationMode = ESAR_Controls.UIControls.TextBox.eTextBox.ValidationModes.None
        Me.txtaddress1.WaterMark = "Enter address line 1..."
        Me.txtaddress1.WaterMarkActiveForeColor = System.Drawing.Color.Gray
        Me.txtaddress1.WaterMarkFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress1.WaterMarkForeColor = System.Drawing.Color.LightGray
        '
        'eContactDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.txtwebsite)
        Me.Controls.Add(Me.txtemail)
        Me.Controls.Add(Me.txtfax)
        Me.Controls.Add(Me.txtphone)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtcountry)
        Me.Controls.Add(Me.txtpincode)
        Me.Controls.Add(Me.txtstate)
        Me.Controls.Add(Me.txtcity)
        Me.Controls.Add(Me.txtaddress2)
        Me.Controls.Add(Me.txtaddress1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "eContactDetails"
        Me.Size = New System.Drawing.Size(384, 185)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtaddress1 As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtaddress2 As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtcity As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtstate As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtcountry As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtpincode As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtphone As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtfax As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtemail As ESAR_Controls.UIControls.TextBox.eTextBox
    Friend WithEvents txtwebsite As ESAR_Controls.UIControls.TextBox.eTextBox

End Class
