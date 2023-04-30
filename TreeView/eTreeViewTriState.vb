Namespace UIControls.TreeView

    Public Class eTreeViewTriState
        Inherits System.Windows.Forms.TreeView

        Public Enum CheckedState As Integer
            UnInitialised = -1
            UnChecked
            Checked
            Mixed
        End Enum

        Private IgnoreClickAction As Integer = 0

        Public Enum TriStateStyles As Integer
            Standard = 0
            Installer
        End Enum

        Private TriStateStyle As TriStateStyles = TriStateStyles.Standard

        <System.ComponentModel.Category("Tri-State Tree View")> _
        <System.ComponentModel.DisplayName("Style")> _
        <System.ComponentModel.Description("Style of the Tri-State Tree View")> _
        Public Property TriStateStyleProperty() As TriStateStyles
            Get
                Return TriStateStyle
            End Get
            Set(ByVal value As TriStateStyles)
                TriStateStyle = value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
            StateImageList = New System.Windows.Forms.ImageList()
            For i As Integer = 0 To 2
                Dim bmp As New System.Drawing.Bitmap(16, 16)
                Dim chkGraphics As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
                Select Case i
                    Case 0
                        System.Windows.Forms.CheckBoxRenderer.DrawCheckBox(chkGraphics, New System.Drawing.Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal)
                        Exit Select
                    Case 1
                        System.Windows.Forms.CheckBoxRenderer.DrawCheckBox(chkGraphics, New System.Drawing.Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal)
                        Exit Select
                    Case 2
                        System.Windows.Forms.CheckBoxRenderer.DrawCheckBox(chkGraphics, New System.Drawing.Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal)
                        Exit Select
                End Select

                StateImageList.Images.Add(bmp)
            Next
        End Sub

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            CheckBoxes = False
            IgnoreClickAction += 1
            UpdateChildState(Me.Nodes, CInt(CheckedState.UnChecked), False, True)
            IgnoreClickAction -= 1
        End Sub

        Protected Overrides Sub OnAfterCheck(ByVal e As System.Windows.Forms.TreeViewEventArgs)
            MyBase.OnAfterCheck(e)

            If IgnoreClickAction > 0 Then
                Return
            End If
            IgnoreClickAction += 1
            Dim tn As System.Windows.Forms.TreeNode = e.Node
            tn.StateImageIndex = If(tn.Checked, CInt(CheckedState.Checked), CInt(CheckedState.UnChecked))
            UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, False)
            UpdateParentState(e.Node.Parent)
            IgnoreClickAction -= 1
        End Sub

        Protected Overrides Sub OnAfterExpand(ByVal e As System.Windows.Forms.TreeViewEventArgs)
            MyBase.OnAfterExpand(e)
            IgnoreClickAction += 1
            UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, True)
            IgnoreClickAction -= 1
        End Sub

        Protected Sub UpdateChildState(ByVal Nodes As System.Windows.Forms.TreeNodeCollection, ByVal StateImageIndex As Integer, ByVal Checked As Boolean, ByVal ChangeUninitialisedNodesOnly As Boolean)
            For Each tnChild As System.Windows.Forms.TreeNode In Nodes
                If Not ChangeUninitialisedNodesOnly OrElse tnChild.StateImageIndex = -1 Then
                    tnChild.StateImageIndex = StateImageIndex
                    tnChild.Checked = Checked
                    If tnChild.Nodes.Count > 0 Then
                        UpdateChildState(tnChild.Nodes, StateImageIndex, Checked, ChangeUninitialisedNodesOnly)
                    End If
                End If
            Next
        End Sub

        Protected Sub UpdateParentState(ByVal tn As System.Windows.Forms.TreeNode)
            If tn Is Nothing Then
                Return
            End If

            Dim OrigStateImageIndex As Integer = tn.StateImageIndex

            Dim UnCheckedNodes As Integer = 0, CheckedNodes As Integer = 0, MixedNodes As Integer = 0
            For Each tnChild As System.Windows.Forms.TreeNode In tn.Nodes
                If tnChild.StateImageIndex = CInt(CheckedState.Checked) Then
                    CheckedNodes += 1
                ElseIf tnChild.StateImageIndex = CInt(CheckedState.Mixed) Then
                    MixedNodes += 1
                    Exit For
                Else
                    UnCheckedNodes += 1
                End If
            Next

            If TriStateStyle = TriStateStyles.Installer Then
                If MixedNodes = 0 Then
                    If UnCheckedNodes = 0 Then
                        tn.Checked = True
                    Else
                        tn.Checked = False
                    End If
                End If
            End If
            If MixedNodes > 0 Then
                tn.StateImageIndex = CInt(CheckedState.Mixed)
            ElseIf CheckedNodes > 0 AndAlso UnCheckedNodes = 0 Then
                If tn.Checked Then
                    tn.StateImageIndex = CInt(CheckedState.Checked)
                Else
                    tn.StateImageIndex = CInt(CheckedState.Mixed)
                End If
            ElseIf CheckedNodes > 0 Then
                tn.StateImageIndex = CInt(CheckedState.Mixed)
            Else
                If tn.Checked Then
                    tn.StateImageIndex = CInt(CheckedState.Mixed)
                Else
                    tn.StateImageIndex = CInt(CheckedState.UnChecked)
                End If
            End If

            If OrigStateImageIndex <> tn.StateImageIndex AndAlso tn.Parent IsNot Nothing Then
                UpdateParentState(tn.Parent)
            End If
        End Sub

        Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
            MyBase.OnKeyDown(e)
            If e.KeyCode = System.Windows.Forms.Keys.Space Then
                SelectedNode.Checked = Not SelectedNode.Checked
            End If
        End Sub

        Protected Overrides Sub OnNodeMouseClick(ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
            MyBase.OnNodeMouseClick(e)
            Dim info As System.Windows.Forms.TreeViewHitTestInfo = HitTest(e.X, e.Y)
            If info Is Nothing OrElse info.Location <> System.Windows.Forms.TreeViewHitTestLocations.StateImage Then
                Return
            End If
            Dim tn As System.Windows.Forms.TreeNode = e.Node
            tn.Checked = Not tn.Checked
        End Sub
    End Class
End Namespace