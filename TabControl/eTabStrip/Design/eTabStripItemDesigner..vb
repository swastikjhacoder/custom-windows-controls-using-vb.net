Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace UIControls.TabControl.Design
    Public Class eTabStripItemDesigner
        Inherits ParentControlDesigner
#Region "Fields"

        Private TabStrip As eTabStripItem

#End Region

#Region "Init & Dispose"

        Public Overrides Sub Initialize(ByVal component As IComponent)
            MyBase.Initialize(component)
            TabStrip = TryCast(component, eTabStripItem)
        End Sub

#End Region

#Region "Overrides"

        Protected Overrides Sub PreFilterProperties(ByVal properties As System.Collections.IDictionary)
            MyBase.PreFilterProperties(properties)

            properties.Remove("Dock")
            properties.Remove("AutoScroll")
            properties.Remove("AutoScrollMargin")
            properties.Remove("AutoScrollMinSize")
            properties.Remove("DockPadding")
            properties.Remove("DrawGrid")
            properties.Remove("Font")
            properties.Remove("Padding")
            properties.Remove("MinimumSize")
            properties.Remove("MaximumSize")
            properties.Remove("Margin")
            properties.Remove("ForeColor")
            properties.Remove("BackColor")
            properties.Remove("BackgroundImage")
            properties.Remove("BackgroundImageLayout")
            properties.Remove("RightToLeft")
            properties.Remove("GridSize")
            properties.Remove("ImeMode")
            properties.Remove("BorderStyle")
            properties.Remove("AutoSize")
            properties.Remove("AutoSizeMode")
            properties.Remove("Location")
        End Sub

        Public Overrides ReadOnly Property SelectionRules() As SelectionRules
            Get
                Return 0
            End Get
        End Property

        Public Overrides Function CanBeParentedTo(ByVal parentDesigner As IDesigner) As Boolean
            Return (TypeOf parentDesigner.Component Is eTabStrip)
        End Function

        Protected Overrides Sub OnPaintAdornments(ByVal pe As PaintEventArgs)
            If TabStrip IsNot Nothing Then
                Using p As New Pen(SystemColors.ControlDark)
                    p.DashStyle = DashStyle.Dash
                    pe.Graphics.DrawRectangle(p, 0, 0, TabStrip.Width - 1, TabStrip.Height - 1)
                End Using
            End If
        End Sub

#End Region
    End Class
End Namespace