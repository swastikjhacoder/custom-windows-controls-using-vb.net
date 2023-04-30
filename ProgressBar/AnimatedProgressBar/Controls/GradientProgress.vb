
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D

Namespace UIControls.ProgressBar
    <Editor(GetType(GradientProgressEditor), GetType(UITypeEditor))> _
    <TypeConverter(GetType(GradientProgressConverter))> _
    Public Class GradientProgress
        'Implements IDisposable
#Region "Event"

        ''' <summary>
        ''' Occurs when the sub properties changed of the ProgressGradient property.
        ''' </summary>
        <Description("Occurs when the sub properties changed of the ProgressGradient property")> _
        Public Event GradientChanged As EventHandler

#End Region

#Region "Instance Members"

        Private isBlended As Boolean = False

        Private manipuleArray As ColorManipulation() = New ColorManipulation() {New ColorManipulation(Color.LightBlue), New ColorManipulation(Color.IndianRed)}

#End Region

#Region "Constructor"

        Public Sub New()
        End Sub

        Public Sub New(ByVal first As Color, ByVal second As Color)
            Me.ColorStart = first
            Me.ColorEnd = second
        End Sub

        Public Sub New(ByVal first As Color, ByVal second As Color, ByVal isBlended As Boolean)
            Me.ColorStart = first
            Me.ColorEnd = second
            Me.isBlended = isBlended
        End Sub

#End Region

#Region "Property"

        ''' <summary>
        ''' Determines whether the blended effect enable or not for progress indicator.
        ''' </summary>
        <Description("Determines whether the blended effect enable or not for progress indicator")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        Public Property IsBlendedForProgress() As Boolean
            Get
                Return isBlended
            End Get
            Set(ByVal value As Boolean)
                If Not value.Equals(isBlended) Then
                    isBlended = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the first progress color.
        ''' </summary>
        <Description("Gets or Sets, the first progress color")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "LightBlue")> _
        <Browsable(True)> _
        Public Property ColorStart() As Color
            Get
                Return manipuleArray(0).BaseColor
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(manipuleArray(0).BaseColor) Then
                    manipuleArray(0) = New ColorManipulation(value)
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the second progress color.
        ''' </summary>
        <Description("Gets or Sets, the second progress color")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "IndianRed")> _
        <Browsable(True)> _
        Public Property ColorEnd() As Color
            Get
                Return manipuleArray(1).BaseColor
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(manipuleArray(1).BaseColor) Then
                    manipuleArray(1) = New ColorManipulation(value)
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets, the first manipulation parameters.
        ''' </summary>
        <Description("Gets, the first manipulation parameters")> _
        <Browsable(False)> _
        Public ReadOnly Property ManipuleStart() As ColorManipulation
            Get
                Return manipuleArray(0)
            End Get
        End Property

        ''' <summary>
        ''' Gets, the second manipulation parameters.
        ''' </summary>
        <Description("Gets, the second manipulation parameters")> _
        <Browsable(False)> _
        Public ReadOnly Property ManipuleEnd() As ColorManipulation
            Get
                Return manipuleArray(1)
            End Get
        End Property

#End Region

#Region "Virtual Methods"

        Protected Overridable Sub OnGradientChanged(ByVal e As EventArgs)
            RaiseEvent GradientChanged(Me, e)
        End Sub

#End Region

#Region "IDisposable Members"

        Public Sub Dispose()
            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class

    Class GradientProgressConverter
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

        ' The exact same process occurs in reverse when converting a GradientProgress object to a string.
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
            Dim gradient As GradientProgress = TryCast(value, GradientProgress)
            ' Gelen object tipimizi GradientProgress tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
            Dim converter As New ColorConverter()
            Return [String].Format("{0}, {1}, {2}", converter.ConvertToString(gradient.ColorStart), converter.ConvertToString(gradient.ColorEnd), gradient.IsBlendedForProgress)
        End Function

        Private Function FromString(ByVal value As Object) As GradientProgress
            Dim result As String() = DirectCast(value, String).Split(","c)
            If result.Length <> 3 Then
                Throw New ArgumentException("Could not convert to value")
            End If

            Try
                Dim gradient As New GradientProgress()

                ' Retrieve the colors
                Dim converter As New ColorConverter()
                gradient.ColorStart = DirectCast(converter.ConvertFromString(result(0)), Color)
                gradient.ColorEnd = DirectCast(converter.ConvertFromString(result(1)), Color)
                ' Retrieve boolean value
                Dim booleanConverter As New BooleanConverter()
                gradient.IsBlendedForProgress = CBool(booleanConverter.ConvertFromString(result(2)))

                Return gradient
            Catch generatedExceptionName As Exception
                Throw New ArgumentException("Could not convert to value")
            End Try
        End Function

#End Region
    End Class

    Class GradientProgressEditor
        Inherits UITypeEditor
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

        Public Overrides Function GetPaintValueSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overrides Sub PaintValue(ByVal e As PaintValueEventArgs)
            Dim gradient As GradientProgress = TryCast(e.Value, GradientProgress)
            Using brush As New LinearGradientBrush(Point.Empty, New Point(0, e.Bounds.Height), gradient.ColorStart, gradient.ColorEnd)
                If Not gradient.IsBlendedForProgress Then
                    e.Graphics.FillRectangle(brush, e.Bounds)
                Else
                    Dim bl As New Blend(2)
                    bl.Factors = New Single() {0.3F, 1.0F}
                    bl.Positions = New Single() {0.0F, 1.0F}
                    brush.Blend = bl
                    e.Graphics.FillRectangle(brush, e.Bounds)
                End If
            End Using
        End Sub

#End Region
    End Class
End Namespace