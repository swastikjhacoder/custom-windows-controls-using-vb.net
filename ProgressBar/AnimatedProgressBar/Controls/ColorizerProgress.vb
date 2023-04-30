
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms.Design

Namespace UIControls.ProgressBar
    <Editor(GetType(ColorizerProgressEditor), GetType(UITypeEditor))> _
    <TypeConverter(GetType(ColorizerProgressConverter))> _
    Public Class ColorizerProgress
        Implements IProgressColorizer
#Region "Event"

        ''' <summary>
        ''' Occurs when the sub properties changed of the ProgressColorizer property.
        ''' </summary>
        <Description("Occurs when the sub properties changed of the ProgressColorizer property")> _
        Public Event ProgressColorizerChanged As EventHandler

#End Region

#Region "Instance Members"

        ' Red, Green, Blue, Alpha
        Private rgba As Byte() = {180, 180, 180, 180}
        ' IsColorizerEnabled, IsTransparencyEnabled
        Private options As Boolean() = {False, False}

#End Region

#Region "Constructor"

        Public Sub New()
        End Sub

        Public Sub New(ByVal red As Byte, ByVal green As Byte, ByVal blue As Byte, ByVal alpha As Byte, ByVal isColorizerEnabled As Boolean, ByVal isTransparencyEnabled As Boolean)
            ' Sets RGBA
            rgba(0) = red
            rgba(1) = green
            rgba(2) = blue
            rgba(3) = alpha

            ' Sets Options
            options(0) = isColorizerEnabled
            options(1) = isTransparencyEnabled
        End Sub

#End Region

#Region "Virtual Methods"

        Protected Overridable Sub OnProgressColorizerChanged(ByVal e As EventArgs)
            RaiseEvent ProgressColorizerChanged(Me, e)
        End Sub

#End Region

#Region "IProgressColorizer Members"

        ''' <summary>
        ''' Determines whether the colorizer effect is enable or not for progress bitmap.
        ''' </summary>
        <Description("Determines whether the colorizer effect is enable or not for progress bitmap")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        Public Property IsColorizerEnabled() As Boolean Implements IProgressColorizer.IsColorizerEnabled
            Get
                Return options(0)
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(options(0)) Then
                    options(0) = value
                    OnProgressColorizerChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Determines whether the transparency effect is visible or not for progress bitmap.
        ''' </summary>
        <Description("Determines whether the transparency effect is visible or not for progress bitmap")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        Public Property IsTransparencyEnabled() As Boolean Implements IProgressColorizer.IsTransparencyEnabled
            Get
                Return options(1)
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(options(1)) Then
                    options(1) = value
                    OnProgressColorizerChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' The red color component property must be in the range of 0 to 255.
        ''' </summary>
        <Description("The red color component property must be in the range of 0 to 255")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType([Byte]), "180")> _
        <Browsable(True)> _
        Public Property Red() As Byte Implements IProgressColorizer.Red
            Get
                Return rgba(0)
            End Get
            Set(ByVal value As Byte)
                If Not value.Equals(rgba(0)) Then
                    rgba(0) = value

                    If IsColorizerEnabled Then
                        OnProgressColorizerChanged(EventArgs.Empty)
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' The green color component property must be in the range of 0 to 255.
        ''' </summary>
        <Description("The green color component property must be in the range of 0 to 255")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType([Byte]), "180")> _
        <Browsable(True)> _
        Public Property Green() As Byte Implements IProgressColorizer.Green
            Get
                Return rgba(1)
            End Get
            Set(ByVal value As Byte)
                If Not value.Equals(rgba(1)) Then
                    rgba(1) = value

                    If IsColorizerEnabled Then
                        OnProgressColorizerChanged(EventArgs.Empty)
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' The blue color component property must be in the range of 0 to 255.
        ''' </summary>
        <Description("The blue color component property must be in the range of 0 to 255")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType([Byte]), "180")> _
        <Browsable(True)> _
        Public Property Blue() As Byte Implements IProgressColorizer.Blue
            Get
                Return rgba(2)
            End Get
            Set(ByVal value As Byte)
                If Not value.Equals(rgba(2)) Then
                    rgba(2) = value

                    If IsColorizerEnabled Then
                        OnProgressColorizerChanged(EventArgs.Empty)
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' This property must be in the range of 50 to 255.
        ''' </summary>
        <Description("This property must be in the range of 50 to 255")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType([Byte]), "180")> _
        <Browsable(True)> _
        Public Property Alpha() As Byte Implements IProgressColorizer.Alpha
            Get
                Return rgba(3)
            End Get
            Set(ByVal value As Byte)
                If Not value.Equals(rgba(3)) Then
                    If value < 50 Then
                        value = 50
                    End If

                    rgba(3) = value

                    If IsTransparencyEnabled Then
                        OnProgressColorizerChanged(EventArgs.Empty)
                    End If
                End If
            End Set
        End Property

#End Region

#Region "IDisposable Members"

        Public Sub Dispose() Implements IDisposable.Dispose
            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class

    Class ColorizerProgressConverter
        Inherits ExpandableObjectConverter
#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Override Methods"

        'All the CanConvertTo() method needs to is check that the target type is a string.
        Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
            If destinationType Is GetType(String) Then
                Return True
            Else
                Return MyBase.CanConvertTo(context, destinationType)
            End If
        End Function

        'ConvertTo() simply checks that it can indeed convert to the desired type.
        Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
            If destinationType Is GetType(String) Then
                Return ToString(value)
            Else
                Return MyBase.ConvertTo(context, culture, value, destinationType)
            End If
        End Function

        ' The exact same process occurs in reverse when converting a ColorizerProgress object to a string.
        '        First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
        '        the ConvertFrom() method. 

        Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
            If sourceType Is GetType(String) Then
                Return True
            Else
                Return MyBase.CanConvertFrom(context, sourceType)
            End If
        End Function

        Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
            If TypeOf value Is String Then
                Return FromString(value)
            Else
                Return MyBase.ConvertFrom(context, culture, value)
            End If
        End Function

#End Region

#Region "Helper Methods"

        Private Overloads Function ToString(ByVal value As Object) As String
            ' Gelen object tipimizi ColorizerProgress tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
            Dim colorizer As ColorizerProgress = TryCast(value, ColorizerProgress)

            Return [String].Format("{0}, {1}, {2}, {3}, {4}, {5}", colorizer.Red, colorizer.Green, colorizer.Blue, colorizer.Alpha, colorizer.IsColorizerEnabled, _
             colorizer.IsTransparencyEnabled)
        End Function

        Private Function FromString(ByVal value As Object) As ColorizerProgress
            Dim result As String() = DirectCast(value, String).Split(","c)
            If result.Length <> 6 Then
                Throw New ArgumentException("Could not convert to value")
            End If

            Try
                Dim colorizer As New ColorizerProgress()

                Dim byteConverter As New ByteConverter()
                Dim booleanConverter As New BooleanConverter()

                ' Retrieve the values of the object.
                colorizer.Red = CByte(byteConverter.ConvertFromString(result(0)))
                colorizer.Green = CByte(byteConverter.ConvertFromString(result(1)))
                colorizer.Blue = CByte(byteConverter.ConvertFromString(result(2)))
                colorizer.Alpha = CByte(byteConverter.ConvertFromString(result(3)))
                colorizer.IsColorizerEnabled = CBool(booleanConverter.ConvertFromString(result(4)))
                colorizer.IsTransparencyEnabled = CBool(booleanConverter.ConvertFromString(result(5)))

                Return colorizer
            Catch generatedExceptionName As Exception
                Throw New ArgumentException("Could not convert to value")
            End Try
        End Function

#End Region
    End Class

    Class ColorizerProgressEditor
        Inherits UITypeEditor
#Region "Constructor"

        Public Sub New()
            MyBase.New()
        End Sub

#End Region

#Region "Destructor"

        Protected Overrides Sub Finalize()
            Try
                GC.SuppressFinalize(Me)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Override Methods"

        Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
            If provider IsNot Nothing Then
                Dim editorService As IWindowsFormsEditorService = TryCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
                If editorService IsNot Nothing Then
                    Using dropDownProgress As New eDropDownProgress(value, editorService)
                        editorService.DropDownControl(dropDownProgress)
                        value = dropDownProgress.Colorizer
                    End Using
                End If
            End If

            Return value
        End Function

        Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
            Return UITypeEditorEditStyle.DropDown
            ' We choose the drop-down style.
        End Function

        ''' <summary>
        ''' Indicates whether the specified context supports painting a representation of an object's value within the specified context.
        ''' </summary>
        ''' <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        ''' <returns>Normally, true if PaintValue is implemented; otherwise, false.But, in this scope returns false.</returns>
        Public Overrides Function GetPaintValueSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return False
            ' We turn down thumbnails.
        End Function

#End Region
    End Class
End Namespace