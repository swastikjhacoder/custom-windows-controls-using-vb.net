Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D

Namespace UIControls.ProgressBar
    <Editor(GetType(GradientHoverEditor), GetType(UITypeEditor))> _
    <TypeConverter(GetType(GradientHoverConverter))> _
    Public Class GradientHover
        'Implements IDisposable
#Region "Event"

        ''' <summary>
        ''' Occurs when the sub properties changed of the HoverGradient property.
        ''' </summary>
        <Description("Occurs when the sub properties changed of the HoverGradient property")> _
        Public Event GradientChanged As EventHandler

#End Region

#Region "Instance Members"

        Private m_colorStart As Color = Color.WhiteSmoke
        Private m_colorEnd As Color = Color.LightSteelBlue
        Private m_hoverForeColor As Color = Color.Maroon
        Private m_gradientStyle As LinearGradientMode = LinearGradientMode.Vertical

#End Region

#Region "Constructor"

        Public Sub New()
        End Sub

        Public Sub New(ByVal first As Color, ByVal second As Color, ByVal hoverForeColor As Color, ByVal gradientStyle As LinearGradientMode)
            Me.m_colorStart = first
            Me.m_colorEnd = second
            Me.m_hoverForeColor = hoverForeColor
            Me.m_gradientStyle = gradientStyle
        End Sub

#End Region

#Region "Property"

        ''' <summary>
        ''' Gets or Sets the first hover cell color.
        ''' </summary>
        <Description("Gets or Sets the first hover cell color")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "WhiteSmoke")> _
        <Browsable(True)> _
        Public Property ColorStart() As Color
            Get
                Return m_colorStart
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(m_colorStart) Then
                    m_colorStart = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the second hover cell color.
        ''' </summary>
        <Description("Gets or Sets the second hover cell color")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "LightSteelBlue")> _
        <Browsable(True)> _
        Public Property ColorEnd() As Color
            Get
                Return m_colorEnd
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(m_colorEnd) Then
                    m_colorEnd = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the hover cell text color.
        ''' </summary>
        <Description("Gets or Sets the hover cell text color")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "Maroon")> _
        <Browsable(True)> _
        Public Property HoverForeColor() As Color
            Get
                Return m_hoverForeColor
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(m_hoverForeColor) Then
                    m_hoverForeColor = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the hover gradient cell style.
        ''' </summary>
        <Description("Gets or Sets the hover gradient cell style")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(LinearGradientMode), "Vertical")> _
        <Browsable(True)> _
        Public Property GradientStyle() As LinearGradientMode
            Get
                Return m_gradientStyle
            End Get
            Set(ByVal value As LinearGradientMode)
                If Not value.Equals(m_gradientStyle) Then
                    m_gradientStyle = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
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

    Class GradientHoverConverter
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

        ' The exact same process occurs in reverse when converting a GradientHover object to a string.
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
            Dim gradient As GradientHover = TryCast(value, GradientHover)
            ' Gelen object tipimizi GradientHover tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
            Dim converter As New ColorConverter()
            Return [String].Format("{0}, {1}, {2}, {3}", converter.ConvertToString(gradient.ColorStart), converter.ConvertToString(gradient.ColorEnd), converter.ConvertToString(gradient.HoverForeColor), gradient.GradientStyle)
        End Function

        Private Function FromString(ByVal value As Object) As GradientHover
            Dim result As String() = DirectCast(value, String).Split(","c)
            If result.Length <> 4 Then
                Throw New ArgumentException("Could not convert to value")
            End If

            Try
                Dim gradient As New GradientHover()

                ' Retrieve the colors
                Dim converter As New ColorConverter()
                gradient.ColorStart = DirectCast(converter.ConvertFromString(result(0)), Color)
                gradient.ColorEnd = DirectCast(converter.ConvertFromString(result(1)), Color)
                gradient.HoverForeColor = DirectCast(converter.ConvertFromString(result(2)), Color)
                gradient.GradientStyle = DirectCast([Enum].Parse(GetType(LinearGradientMode), result(3), True), LinearGradientMode)

                Return gradient
            Catch generatedExceptionName As Exception
                Throw New ArgumentException("Could not convert to value")
            End Try
        End Function

#End Region
    End Class

    Class GradientHoverEditor
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
            Dim gradient As GradientHover = TryCast(e.Value, GradientHover)
            Using brush As New LinearGradientBrush(e.Bounds, gradient.ColorStart, gradient.ColorEnd, gradient.GradientStyle)
                e.Graphics.FillRectangle(brush, e.Bounds)
            End Using
        End Sub

#End Region
    End Class
End Namespace