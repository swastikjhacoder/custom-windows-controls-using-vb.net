
#Region "Using directive"

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

#End Region

Namespace UIControls.ComboBox
    Partial Public Class eComboBoxDateTime
        Inherits Windows.Forms.ComboBox
#Region "Class Data"
        Private Const WM_LBUTTONDOWN As UInt32 = &H201
        Private Const WM_LBUTTONDBLCLK As UInt32 = &H203
        Private Const WM_KEYF4 As UInt32 = &H134
        Private Const WM_CTLCOLORLISTBOX As UInt32 = &H134
        Private myTSMonthCalendar As ToolStripMonthCalendar
        Private tsDD As ToolStripDropDown
#End Region

        Public Sub New()
            InitializeComponent()
            myTSMonthCalendar = New ToolStripMonthCalendar()
            tsDD = New ToolStripDropDown()

            ' instantiere evenimente
            AddHandler Me.myTSMonthCalendar.MonthCalendarControl.DateChanged, AddressOf Me.myTSMonthCalendar_DateChanged
            AddHandler Me.myTSMonthCalendar.MonthCalendarControl.KeyDown, AddressOf Me.myTSMonthCalendar_KeyDown
        End Sub

#Region "Control's Methods"
        Private Sub myTSMonthCalendar_DateChanged(ByVal sender As Object, ByVal e As DateRangeEventArgs)
            Me.Text = e.[End].ToShortDateString()
        End Sub
        Private Sub myTSMonthCalendar_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If e.KeyCode = Keys.Enter Then
                Me.tsDD.Close()
            End If

        End Sub
#End Region

#Region "Methods"
        Private Function CalculatePoz() As Point
            Dim point As New Point(0, Me.Height)

            If (Me.PointToScreen(New Point(0, 0)).Y + Me.Height + Me.myTSMonthCalendar.Height) > Screen.PrimaryScreen.WorkingArea.Height Then
                point.Y = -Me.myTSMonthCalendar.Height - 7
            End If

            Return point
        End Function
#End Region

        Protected Overrides Sub WndProc(ByRef m As Message)
            '#Region "WM_KEYF4"
            If m.Msg = WM_KEYF4 Then
                Me.Focus()
                Me.tsDD.Refresh()
                If Not Me.tsDD.Visible Then
                    Try
                        If Me.Text <> "" Then
                            Me.myTSMonthCalendar.MonthCalendarControl.SetDate(Convert.ToDateTime(Me.Text))
                        End If
                    Catch generatedExceptionName As Exception
                        MessageBox.Show("Invalid Date!")
                    End Try

                    tsDD.Items.Add(Me.myTSMonthCalendar)
                    tsDD.Show(Me, Me.CalculatePoz())
                End If
                Return
            End If
            '#End Region

            '#Region "WM_LBUTTONDBLCLK"
            If m.Msg = WM_LBUTTONDBLCLK OrElse m.Msg = WM_LBUTTONDOWN Then
                If Not Me.tsDD.Visible Then
                    Try
                        If Me.Text <> "" Then
                            Me.myTSMonthCalendar.MonthCalendarControl.SetDate(Convert.ToDateTime(Me.Text))
                        End If
                    Catch generatedExceptionName As Exception
                        MessageBox.Show("Invalid Date!")
                    End Try

                    tsDD.Items.Add(Me.myTSMonthCalendar)
                    tsDD.Show(Me, Me.CalculatePoz())
                End If
                Return
            End If
            '#End Region

            MyBase.WndProc(m)
        End Sub
    End Class

    'Declare a class that inherits from ToolStripControlHost.
    Public Class ToolStripMonthCalendar
        Inherits ToolStripControlHost
#Region "Class Data"
#End Region

        ' Call the base constructor passing in a MonthCalendar instance.
        Public Sub New()
            MyBase.New(New MonthCalendar())
        End Sub

        Public ReadOnly Property MonthCalendarControl() As MonthCalendar
            Get
                Return TryCast(Control, MonthCalendar)
            End Get
        End Property

        ' Expose the MonthCalendar.FirstDayOfWeek as a property.
        Public Property FirstDayOfWeek() As Day
            Get
                Return MonthCalendarControl.FirstDayOfWeek
            End Get
            Set(ByVal value As Day)
                value = MonthCalendarControl.FirstDayOfWeek
            End Set
        End Property

        ' Expose the AddBoldedDate method.
        Public Sub AddBoldedDate(ByVal dateToBold As DateTime)
            MonthCalendarControl.AddBoldedDate(dateToBold)
        End Sub

        ' Subscribe and unsubscribe the control events you wish to expose.
        Protected Overrides Sub OnSubscribeControlEvents(ByVal c As Control)
            ' Call the base so the base events are connected.
            MyBase.OnSubscribeControlEvents(c)

            ' Cast the control to a MonthCalendar control.
            Dim monthCalendarControl As MonthCalendar = DirectCast(c, MonthCalendar)

            ' Add the event.
            AddHandler monthCalendarControl.DateChanged, AddressOf OnDateChanged
        End Sub

        Protected Overrides Sub OnUnsubscribeControlEvents(ByVal c As Control)
            ' Call the base method so the basic events are unsubscribed.
            MyBase.OnUnsubscribeControlEvents(c)

            ' Cast the control to a MonthCalendar control.
            Dim monthCalendarControl As MonthCalendar = DirectCast(c, MonthCalendar)

            ' Remove the event.
            RemoveHandler monthCalendarControl.DateChanged, AddressOf OnDateChanged
        End Sub

        ' Declare the DateChanged event.
        Public Event DateChanged As DateRangeEventHandler

        ' Raise the DateChanged event.
        Private Sub OnDateChanged(ByVal sender As Object, ByVal e As DateRangeEventArgs)
            RaiseEvent DateChanged(Me, e)
        End Sub
    End Class
End Namespace