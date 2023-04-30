
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D

Namespace UIControls.ProgressBar
    <Editor(GetType(GradientDigitBoxEditor), GetType(UITypeEditor))> _
    <TypeConverter(GetType(GradientDigitBoxConverter))> _
    Public Class GradientDigitBox
        'Implements IDisposable
#Region "Enum"

        Public Enum DigitalNumberLayout
            LeftSide
            RightSide
        End Enum

#End Region

#Region "Event"

        ''' <summary>
        ''' Occurs when the sub properties changed of the DigitBoxGradient property.
        ''' </summary>
        <Description("Occurs when the sub properties changed of the DigitBoxGradient property")> _
        Public Event GradientChanged As EventHandler

#End Region

#Region "Instance Members"

        Private isBlended As Boolean = False
        Private colorArray As Color() = {Color.WhiteSmoke, Color.Aquamarine, Color.LightGray}
        Private m_digitalNumberSide As DigitalNumberLayout = DigitalNumberLayout.RightSide

#End Region

#Region "Constructor"

        Public Sub New()
        End Sub

        Public Sub New(ByVal first As Color, ByVal second As Color)
            Me.colorArray(0) = first
            Me.colorArray(1) = second
        End Sub

        Public Sub New(ByVal first As Color, ByVal second As Color, ByVal isBlended As Boolean)
            Me.colorArray(0) = first
            Me.colorArray(1) = second
            Me.isBlended = isBlended
        End Sub

        Public Sub New(ByVal first As Color, ByVal second As Color, ByVal borderColor As Color, ByVal isBlended As Boolean)
            Me.colorArray(0) = first
            Me.colorArray(1) = second
            Me.colorArray(2) = borderColor
            Me.isBlended = isBlended
        End Sub

        Public Sub New(ByVal first As Color, ByVal second As Color, ByVal borderColor As Color, ByVal isBlended As Boolean, ByVal side As DigitalNumberLayout)
            Me.colorArray(0) = first
            Me.colorArray(1) = second
            Me.colorArray(2) = borderColor
            Me.isBlended = isBlended
            Me.m_digitalNumberSide = side
        End Sub

#End Region

#Region "Property"

        ''' <summary>
        ''' Determines whether the blended effect enable or not for DigitBox background.
        ''' </summary>
        <Description("Determines whether the blended effect enable or not for DigitBox background")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(False)> _
        <Browsable(True)> _
        Public Property IsBlendedForBackground() As Boolean
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
        ''' Gets or Sets, the first background color of the DigitBox rectangle.
        ''' </summary>
        <Description("Gets or Sets, the first background color of the DigitBox rectangle")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "WhiteSmoke")> _
        <Browsable(True)> _
        Public Property ColorStart() As Color
            Get
                Return colorArray(0)
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(colorArray(0)) Then
                    colorArray(0) = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the second background color of the DigitBox rectangle.
        ''' </summary>
        <Description("Gets or Sets, the second background color of the DigitBox rectangle")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "Aquamarine")> _
        <Browsable(True)> _
        Public Property ColorEnd() As Color
            Get
                Return colorArray(1)
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(colorArray(1)) Then
                    colorArray(1) = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the border color of the DigitBox rectangle.
        ''' </summary>
        <Description("Gets or Sets, the border color of the DigitBox rectangle")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(Color), "LightGray")> _
        <Browsable(True)> _
        Public Property BorderColor() As Color
            Get
                Return colorArray(2)
            End Get
            Set(ByVal value As Color)
                If Not value.Equals(colorArray(2)) Then
                    colorArray(2) = value
                    OnGradientChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets, the layouts of the DigitBox rectangle on the control.
        ''' </summary>
        <Description("Gets or Sets, the layouts of the DigitBox rectangle on the control")> _
        <RefreshProperties(RefreshProperties.Repaint)> _
        <NotifyParentProperty(True)> _
        <DefaultValue(GetType(DigitalNumberLayout), "RightSide")> _
        <Browsable(True)> _
        Public Property DigitalNumberSide() As DigitalNumberLayout
            Get
                Return m_digitalNumberSide
            End Get
            Set(ByVal value As DigitalNumberLayout)
                If Not value.Equals(m_digitalNumberSide) Then
                    m_digitalNumberSide = value
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

    Class GradientDigitBoxConverter
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

        ' The exact same process occurs in reverse when converting a GradientDigitBox object to a string.
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
            Dim gradient As GradientDigitBox = TryCast(value, GradientDigitBox)
            ' Gelen object tipimizi GradientDigitBox tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
            Dim converter As New ColorConverter()
            Return [String].Format("{0}, {1}, {2}, {3}, {4}", converter.ConvertToString(gradient.ColorStart), converter.ConvertToString(gradient.ColorEnd), converter.ConvertToString(gradient.BorderColor), gradient.IsBlendedForBackground, gradient.DigitalNumberSide)
        End Function

        Private Function FromString(ByVal value As Object) As GradientDigitBox
            Dim result As String() = DirectCast(value, String).Split(","c)
            If result.Length <> 5 Then
                Throw New ArgumentException("Could not convert to value")
            End If

            Try
                Dim gradient As New GradientDigitBox()

                ' Retrieve the colors
                Dim converter As New ColorConverter()
                gradient.ColorStart = DirectCast(converter.ConvertFromString(result(0)), Color)
                gradient.ColorEnd = DirectCast(converter.ConvertFromString(result(1)), Color)
                gradient.BorderColor = DirectCast(converter.ConvertFromString(result(2)), Color)
                ' Retrieve the boolean value
                Dim booleanConverter As New BooleanConverter()
                gradient.IsBlendedForBackground = CBool(booleanConverter.ConvertFromString(result(3)))
                gradient.DigitalNumberSide = CType([Enum].Parse(GetType(GradientDigitBox.DigitalNumberLayout), result(4), True), GradientDigitBox.DigitalNumberLayout)

                Return gradient
            Catch generatedExceptionName As Exception
                Throw New ArgumentException("Could not convert to value")
            End Try
        End Function

#End Region
    End Class

    Class GradientDigitBoxEditor
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
            Dim gradient As GradientDigitBox = TryCast(e.Value, GradientDigitBox)
            Using brush As New LinearGradientBrush(Point.Empty, New Point(0, e.Bounds.Height), gradient.ColorStart, gradient.ColorEnd)
                If Not gradient.IsBlendedForBackground Then
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