'***************************************************************************************************|
'/<summary>
'/ CollapsePanel is the parent container for the CollapseGroupBox. 
'/</summary>
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

Namespace UIControls.GroupBox
    Public Class eCollapsePanel
        Inherits Windows.Forms.Panel

        Private components As Container
        ' array of CollapseGroupBoxes - a panel can contain multiple group boxes
        Private groupBoxArray As ArrayList
        ' space after panel displays to separate from other controls
        Private Const intGap As Integer = 6

        Public Sub New()

            MyBase.New()

            'This call is required by the Component Designer.
            InitializeComponent()

            groupBoxArray = New ArrayList()

        End Sub
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub
#Region " Component Designer generated code "

        Private Sub InitializeComponent()
        End Sub
#End Region

        '/<summary>
        '/AddGroup() adds the CollapseGroupBox to the CollapsePanel
        '/</summary>
        Public Sub Add(ByVal groupBox As eCollapseGroupBox)

            groupBoxArray.Add(groupBox)

            SuspendLayout()
            Dim theSize As Size = AutoScrollMinSize
            groupBox.Location = New Point(4, theSize.Height + 4)

            theSize.Height += (groupBox.Height + intGap)
            AutoScrollMinSize = theSize

            AddHandler groupBox.CollapseBoxClickedEvent, AddressOf CollapseBox_Click

            Controls.Add(groupBox)

            ResumeLayout(False)

        End Sub
        Private Sub CollapseBox_Click(ByVal sender As Object, ByVal e As EventArgs)

            ' find the group box on the array
            Dim nIndex As Integer = groupBoxArray.IndexOf(sender)
            ' type cast it to the appropriate type
            Dim groupBox As eCollapseGroupBox = _
                CType(groupBoxArray(nIndex), eCollapseGroupBox)

            ' compute the difference between the full height and collapsed height
            Dim nDelta As Integer
            If groupBox.Height = eCollapseGroupBox.intCollapsedHeight Then
                nDelta = -(groupBox.FullHeight - eCollapseGroupBox.intCollapsedHeight)
            Else
                nDelta = (groupBox.FullHeight - eCollapseGroupBox.intCollapsedHeight)
            End If

            ' adjust vertical placement of the group boxes on the panel from the 
            ' computed delta above relative to the current group box. Don't want the 
            ' an expanded group box to overlay a group box underneath it nor should 
            ' there be excess space between the collapsed group box and the group 
            ' boxes below it.
            Dim i As Integer
            ' skip the current group box since it's the one that's collapsed or 
            ' expanded
            For i = (nIndex + 1) To groupBoxArray.Count - 1 Step 1
                groupBox = CType(groupBoxArray(i), eCollapseGroupBox)
                ' set the distance of the group box from the top of the panel
                groupBox.Top += nDelta
            Next

            ' adjust the minimum size of the auto-scroll
            Dim theSize As Size = AutoScrollMinSize
            theSize.Height += nDelta
            AutoScrollMinSize = theSize

        End Sub
    End Class
End Namespace