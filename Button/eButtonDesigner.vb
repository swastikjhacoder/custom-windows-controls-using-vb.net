
Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms

#Region "CButtonDesigner"

Public Class eButtonDesigner
    Inherits ControlDesigner

    Private _CButton As UIControls.Button.eButton
    Private _Lists As DesignerActionListCollection
    Private InABox As Boolean

    Public Overrides Sub Initialize(ByVal component As IComponent)
        MyBase.Initialize(component)

        ' Get clock control shortcut reference
        _CButton = CType(component, UIControls.Button.eButton)
    End Sub

    Protected Overrides Function GetHitTest( _
      ByVal point As System.Drawing.Point) As Boolean
        point = _CButton.PointToClient(point)
        _CButton.CenterPtTracker.IsActive = _
            _CButton.CenterPtTracker.TrackerRectangle.Contains(point)
        _CButton.FocusPtTracker.IsActive = _
            _CButton.FocusPtTracker.TrackerRectangle.Contains(point)
        Return _CButton.CenterPtTracker.IsActive Or _CButton.FocusPtTracker.IsActive
    End Function

    Protected Overrides Sub OnMouseEnter()
        MyBase.OnMouseEnter()
        InABox = True
        _CButton.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave()
        MyBase.OnMouseLeave()
        InABox = False
        _CButton.Invalidate()
    End Sub

    Protected Overrides Sub OnPaintAdornments _
      (ByVal pe As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaintAdornments(pe)

        If _CButton.FillType = UIControls.Button.eButton.eFillType.GradientPath And InABox Then
            Using g As Graphics = pe.Graphics
                Using pn As Pen = New Pen(Color.Gray, 1)
                    pn.DashStyle = DashStyle.Dot
                    g.FillEllipse( _
                        New SolidBrush(Color.FromArgb(100, 255, 255, 255)), _
                        _CButton.CenterPtTracker.TrackerRectangle)
                    g.DrawEllipse(pn, _CButton.CenterPtTracker.TrackerRectangle)

                    g.FillRectangle( _
                        New SolidBrush(Color.FromArgb(100, 255, 255, 255)), _
                        _CButton.FocusPtTracker.TrackerRectangle)
                    Dim rect As RectangleF = _CButton.FocusPtTracker.TrackerRectangle
                    g.DrawRectangle(pn, rect.X, rect.Y, rect.Width, rect.Height)
                End Using
            End Using
        End If
    End Sub

#Region "ActionLists"

    Public Overrides ReadOnly Property ActionLists() As DesignerActionListCollection
        Get
            If _Lists Is Nothing Then
                _Lists = New DesignerActionListCollection
                _Lists.Add(New eButtonActionList(Component))
            End If
            Return _Lists
        End Get
    End Property

#End Region 'ActionLists

End Class

#End Region 'CButtonDesigner

#Region "CButtonActionList"

Public Class eButtonActionList
    Inherits DesignerActionList

    Private _CButton As UIControls.Button.eButton
    Private _DesignerService As DesignerActionUIService

    Public Sub New(ByVal component As IComponent)
        MyBase.New(component)

        ' Save a reference to the control we are designing.
        _CButton = DirectCast(component, UIControls.Button.eButton)

        ' Save a reference to the DesignerActionUIService
        _DesignerService = _
            CType(GetService(GetType(DesignerActionUIService)),  _
            DesignerActionUIService)

        'Makes the Smart Tags open automatically 
        AutoShow = True
    End Sub

#Region "Smart Tag Items"

#Region "Properties"

    Public ReadOnly Property CurrControl() As UIControls.Button.eButton
        Get
            Return _CButton
        End Get
    End Property

#Region "Shape"

    Public Property Shape() As UIControls.Button.eButton.eShape
        Get
            Return _CButton.Shape
        End Get
        Set(ByVal value As UIControls.Button.eButton.eShape)
            SetControlProperty("Shape", value)
        End Set
    End Property

#End Region 'Shape

#Region "Border"

    Public Property BorderColor() As Color
        Get
            Return _CButton.BorderColor
        End Get
        Set(ByVal value As Color)
            SetControlProperty("BorderColor", value)
        End Set
    End Property

    Public Property BorderShow() As Boolean
        Get
            Return _CButton.BorderShow
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("BorderShow", value)
        End Set
    End Property

#End Region 'Border

#Region "Fill"

    <Editor(GetType(BlendTypeEditor), GetType(UITypeEditor))> _
    Public Property ColorFillBlend() As cBlendItems
        Get
            Return _CButton.ColorFillBlend
        End Get
        Set(ByVal value As cBlendItems)
            SetControlProperty("ColorFillBlend", value)
        End Set
    End Property

    Public Property FillType() As UIControls.Button.eButton.eFillType
        Get
            Return _CButton.FillType
        End Get
        Set(ByVal value As UIControls.Button.eButton.eFillType)
            SetControlProperty("FillType", value)
        End Set
    End Property

    Public Property FillTypeLinear() As LinearGradientMode
        Get
            Return _CButton.FillTypeLinear
        End Get
        Set(ByVal value As LinearGradientMode)
            SetControlProperty("FillTypeLinear", value)
        End Set
    End Property

    Public Property ColorFillSolid() As Color
        Get
            Return _CButton.ColorFillSolid
        End Get
        Set(ByVal value As Color)
            SetControlProperty("ColorFillSolid", value)
        End Set
    End Property

#End Region 'Fill

    '#Region "FocalPoints"

    '    Public Property FocalPoints() As cFocalPoints
    '        Get
    '            Return _CButton.FocalPoints
    '        End Get
    '        Set(ByVal value As cFocalPoints)
    '            SetControlProperty("FocalPoints", value)
    '        End Set
    '    End Property

    '#End Region 'FocalPoints

#Region "Text"

    Public Property Text() As String
        Get
            Return _CButton.Text
        End Get
        Set(ByVal value As String)
            SetControlProperty("Text", value)
        End Set
    End Property

    Public Property TextAlign() As ContentAlignment
        Get
            Return _CButton.TextAlign
        End Get
        Set(ByVal value As ContentAlignment)
            SetControlProperty("TextAlign", value)
        End Set
    End Property

    Public Property TextImageRelation() As TextImageRelation
        Get
            Return _CButton.TextImageRelation
        End Get
        Set(ByVal value As TextImageRelation)
            SetControlProperty("TextImageRelation", value)
        End Set
    End Property

    Public Property TextShadowShow() As Boolean
        Get
            Return _CButton.TextShadowShow
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("TextShadowShow", value)
        End Set
    End Property

    Public Property ForeColor() As Color
        Get
            Return _CButton.ForeColor
        End Get
        Set(ByVal value As Color)
            SetControlProperty("ForeColor", value)
        End Set
    End Property

    Public Property TextShadow() As Color
        Get
            Return _CButton.TextShadow
        End Get
        Set(ByVal value As Color)
            SetControlProperty("TextShadow", value)
        End Set
    End Property

#End Region 'Text

#Region "Image"

    Public Property ImageAlign() As ContentAlignment
        Get
            Return _CButton.ImageAlign
        End Get
        Set(ByVal value As ContentAlignment)
            SetControlProperty("ImageAlign", value)
        End Set
    End Property

    Public Property SideImageAlign() As ContentAlignment
        Get
            Return _CButton.SideImageAlign
        End Get
        Set(ByVal value As ContentAlignment)
            SetControlProperty("SideImageAlign", value)
        End Set
    End Property

    Public Property SideImageBehindText() As Boolean
        Get
            Return _CButton.SideImageBehindText
        End Get
        Set(ByVal value As Boolean)
            SetControlProperty("SideImageBehindText", value)
        End Set
    End Property

#End Region '

#End Region 'Properties

#Region "Methods"

    Public Sub AdjustCorners()

        'Create a new Corners Dialog and update the controls on the form
        Using dlg As dlgCorners = New dlgCorners()
            Dim maxcorner As Integer
            Dim ratio As Single
            If _CButton.Width > _CButton.Height Then
                dlg.TheSample.Height = CInt(dlg.TheSample.Width * (_CButton.Height / _CButton.Width))
                dlg.TheSample.Top = CInt((dlg.panSampleHolder.Height - dlg.TheSample.Height) / 2)
                ratio = CSng(dlg.TheSample.Height / _CButton.Height)
                maxcorner = CInt(((dlg.TheSample.Height / 2)) - (((_CButton.Padding.Top * ratio) + (_CButton.Padding.Bottom * ratio)) / 2))
            Else
                dlg.TheSample.Width = CInt(dlg.TheSample.Height * (_CButton.Width / _CButton.Height))
                dlg.TheSample.Left = CInt((dlg.panSampleHolder.Width - dlg.TheSample.Width) / 2)
                maxcorner = CInt(((dlg.TheSample.Width / 2)) - ((_CButton.Padding.Left + _CButton.Padding.Right) / 2))
                ratio = CSng(dlg.TheSample.Width / _CButton.Width)
            End If
            ' Set current Corners values
            With dlg
                .tbarUpperLeft.Maximum = maxcorner
                .tbarUpperRight.Maximum = maxcorner
                .tbarLowerLeft.Maximum = maxcorner
                .tbarLowerRight.Maximum = maxcorner
                .tbarAll.Maximum = maxcorner
                .tbarUpperLeft.TickFrequency = CInt(maxcorner / 2)
                .tbarUpperRight.TickFrequency = CInt(maxcorner / 2)
                .tbarLowerLeft.TickFrequency = CInt(maxcorner / 2)
                .tbarLowerRight.TickFrequency = CInt(maxcorner / 2)
                .tbarAll.TickFrequency = CInt(maxcorner / 2)
                If _CButton.Corners.All > -1 Then
                    .tbarAll.Value = CInt(Math.Min((_CButton.Corners.UpperLeft * ratio), maxcorner))
                End If
                .tbarUpperLeft.Value = CInt(Math.Min((_CButton.Corners.UpperLeft * ratio), maxcorner))
                .tbarUpperRight.Value = CInt(Math.Min((_CButton.Corners.UpperRight * ratio), maxcorner))
                .tbarLowerLeft.Value = CInt(Math.Min((_CButton.Corners.LowerLeft * ratio), maxcorner))
                .tbarLowerRight.Value = CInt(Math.Min((_CButton.Corners.LowerRight * ratio), maxcorner))
                .TheSample.Shape = _CButton.Shape
                .TheSample.FillType = _CButton.FillType
                .TheSample.FillTypeLinear = _CButton.FillTypeLinear
                .TheSample.ColorFillSolid = _CButton.ColorFillSolid
                .TheSample.BorderColor = _CButton.BorderColor
                '.TheSample.ColorFillBlend = _CButton.ColorFillBlend
                '.TheSample.Corners = New CornersProperty(CShort(_CButton.Corners.LowerLeft * ratio), CShort(_CButton.Corners.LowerRight * ratio), CShort(_CButton.Corners.UpperLeft * ratio), CShort(_CButton.Corners.UpperRight * ratio))
                '.TheSample.FocalPoints = _CButton.FocalPoints
                .TheSample.TextMargin = New Padding(CInt(_CButton.TextMargin.Left * ratio), CInt(_CButton.TextMargin.Top * ratio), CInt(_CButton.TextMargin.Right * ratio), CInt(_CButton.TextMargin.Bottom * ratio))
                .TheSample.Padding = New Padding(CInt(_CButton.Padding.Left * ratio), CInt(_CButton.Padding.Top * ratio), CInt(_CButton.Padding.Right * ratio), CInt(_CButton.Padding.Bottom * ratio))
                .TheSample.Text = _CButton.Text
                .TheSample.ForeColor = _CButton.ForeColor
                .TheSample.TextAlign = _CButton.TextAlign
                .TheSample.Font = New Font(_CButton.Font.FontFamily, _CButton.Font.Size * ratio, _CButton.Font.Style)
                .TheSample.TextShadow = _CButton.TextShadow
                .TheSample.TextShadowShow = _CButton.TextShadowShow
                .ratio = ratio
            End With
            ' Update new Corners values if OK button was pressed
            If dlg.ShowDialog() = DialogResult.OK Then
                Dim designerHost As IDesignerHost = CType(Component.Site.GetService(GetType(IDesignerHost)), IDesignerHost)
                If designerHost IsNot Nothing Then
                    Dim t As DesignerTransaction = designerHost.CreateTransaction()
                    Try
                        SetControlProperty("Corners", New CornersProperty(CShort(dlg.TheSample.Corners.LowerLeft / ratio), CShort(dlg.TheSample.Corners.LowerRight / ratio), CShort(dlg.TheSample.Corners.UpperLeft / ratio), CShort(dlg.TheSample.Corners.UpperRight / ratio)))
                        t.Commit()
                    Catch
                        t.Cancel()
                    End Try
                End If
            End If
        End Using
        _CButton.Refresh()
        _DesignerService.Refresh(_CButton)

    End Sub

#End Region 'Methods

    ' Set a control property. This method makes Undo/Redo
    ' work properly and marks the form as modified in the IDE.
    Private Sub SetControlProperty(ByVal property_name As String, ByVal value As Object)
        TypeDescriptor.GetProperties(_CButton) _
            (property_name).SetValue(_CButton, value)
    End Sub

#End Region ' Smart Tag Items

    ' Return the smart tag action items.
    Public Overrides Function GetSortedActionItems() As DesignerActionItemCollection
        Dim items As New DesignerActionItemCollection()

        items.Add( _
            New DesignerActionPropertyItem( _
                "Shape", _
                "Shape", _
                "", _
                "The Shape of the Control"))

        items.Add(New DesignerActionHeaderItem("Fill"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "FillType", _
                "Fill Type", _
                "Fill", _
                "Fill Solid or Gradient"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "FillTypeLinear", _
                "Linear Fill Type", _
                "Fill", _
                "Gradient Fill Type"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "ColorFillBlend", _
                "Blend Colors", _
                "Fill", _
                "Color and Position Arrays for the ColorBlend"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "ColorFillSolid", _
                "Solid Fill Color", _
                "Fill", _
                "The Color for Solid Fills"))

        items.Add(New DesignerActionHeaderItem("Text"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "Text", _
                "Text", _
                "Text", _
                "The Text on the Button"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "TextAlign", _
                "Text Alignment", _
                "Text", _
                "The Alignment to use on the Text"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "ForeColor", _
                "Text Color", _
                "Text", _
                "The Color of the Text"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "TextShadow", _
                "Shadow Color", _
                "Text", _
                "The Color of the Shadow Text"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "TextImageRelation", _
                "Text to Image Relation", _
                "Text", _
                "The Relationship of the Text to the Image"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "TextShadowShow", _
                "Use Text Shadow", _
                "Text", _
                "Turn the Text Shadow On and Off"))

        items.Add(New DesignerActionHeaderItem("Border"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "BorderColor", _
                "Border Color", _
                "Border", _
                "The color of the button's Border"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "BorderShow", _
                "Show Border", _
                "Border", _
                "The show or not show the border"))

        items.Add(New DesignerActionHeaderItem("Images"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "ImageAlign", _
                "Image Alignment", _
                "Images", _
                "The Alignment for the Image"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "SideImageAlign", _
                "SideImage Alignment", _
                "Images", _
                "The Alignment for the SideImage"))
        items.Add( _
            New DesignerActionPropertyItem( _
                "SideImageBehindText", _
                "Is SideImage Behind the Text", _
                "Images", _
                ""))

        items.Add(New DesignerActionHeaderItem("Corners"))

        Dim txt As String = String.Format("UpperLeft={0}, UpperRight={1}, LowerRight={2}, LowerLeft={3}", _
        _CButton.Corners.UpperLeft.ToString, _
        _CButton.Corners.UpperRight.ToString, _
        _CButton.Corners.LowerRight.ToString, _
        _CButton.Corners.LowerLeft.ToString)
        items.Add( _
            New DesignerActionTextItem( _
                 txt, "Corners"))
        items.Add( _
            New DesignerActionMethodItem( _
                Me, _
                "AdjustCorners", _
                "Adjust Corners ", _
                "Corners", _
                "Adjust Corners", _
                True))

        Return items
    End Function

End Class

#End Region 'CButtonActionList

