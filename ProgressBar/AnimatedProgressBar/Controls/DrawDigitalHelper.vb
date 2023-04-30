
Imports System.Linq
Imports System.Drawing

Namespace UIControls.ProgressBar
    NotInheritable Class DrawDigitalHelper
        Private Sub New()
        End Sub
        Public Shared RctWidth As Single

#Region "General Methods"

        ''' <summary>
        ''' Draws the given number in the 7-Segment format.
        ''' </summary>
        Public Shared Sub DrawNumbers(ByVal g As Graphics, ByVal number As Integer, ByVal maximum As Integer, ByVal foreColor As Color, ByVal drect As RectangleF)
            Dim isOnlyOutline As Boolean = True
            Try
                Dim shift As Single = 0

                Dim num As String = number.ToString()
                Dim max As String = maximum.ToString()

                Dim chars As Char() = num.ToCharArray()
                For i As Integer = 0 To chars.Length - 1
                    If isOnlyOutline Then
                        For j As Integer = max.Length - 1 To 0 Step -1
                            If j > chars.Length - 1 Then
                                DrawADigit(g, 0, foreColor, New PointF(drect.X + shift, drect.Y), isOnlyOutline, drect.Height)
                                shift += RctWidth / max.Length
                            Else
                                isOnlyOutline = False
                                Exit For
                            End If
                        Next
                    End If

                    Dim c As Char = chars(i)


                    DrawADigit(g, Int16.Parse(c.ToString()), foreColor, New PointF(drect.X + shift, drect.Y), isOnlyOutline, drect.Height)
                    shift += RctWidth / max.Length
                Next
            Catch generatedExceptionName As Exception


            End Try
        End Sub

#End Region

#Region "Helper Methods"

        ''' <summary>
        ''' Draws a digit in 7-Segement format.
        ''' </summary>
        Private Shared Sub DrawADigit(ByVal g As Graphics, ByVal number As Integer, ByVal foreColor As Color, ByVal position As PointF, ByVal isOnlyOutline As Boolean, ByVal height As Single)
            Dim width As Single
            width = 10.0F * height / 13

            Dim outline As New Pen(Color.FromArgb(25, Color.Gray))

            '#Region "Form Polygon Points"

            'Segment A
            Dim segmentA As PointF() = New PointF(4) {}
            segmentA(0) = InlineAssignHelper(segmentA(4), New PointF(position.X + GetX(2.8F, width), position.Y + GetY(1.0F, height)))
            segmentA(1) = New PointF(position.X + GetX(10, width), position.Y + GetY(1.0F, height))
            segmentA(2) = New PointF(position.X + GetX(8.8F, width), position.Y + GetY(2.0F, height))
            segmentA(3) = New PointF(position.X + GetX(3.8F, width), position.Y + GetY(2.0F, height))

            'Segment B
            Dim segmentB As PointF() = New PointF(4) {}
            segmentB(0) = InlineAssignHelper(segmentB(4), New PointF(position.X + GetX(10, width), position.Y + GetY(1.4F, height)))
            segmentB(1) = New PointF(position.X + GetX(9.3F, width), position.Y + GetY(6.8F, height))
            segmentB(2) = New PointF(position.X + GetX(8.4F, width), position.Y + GetY(6.4F, height))
            segmentB(3) = New PointF(position.X + GetX(9.0F, width), position.Y + GetY(2.2F, height))

            'Segment C
            Dim segmentC As PointF() = New PointF(4) {}
            segmentC(0) = InlineAssignHelper(segmentC(4), New PointF(position.X + GetX(9.2F, width), position.Y + GetY(7.2F, height)))
            segmentC(1) = New PointF(position.X + GetX(8.7F, width), position.Y + GetY(12.7F, height))
            segmentC(2) = New PointF(position.X + GetX(7.6F, width), position.Y + GetY(11.9F, height))
            segmentC(3) = New PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.7F, height))

            'Segment D
            Dim segmentD As PointF() = New PointF(4) {}
            segmentD(0) = InlineAssignHelper(segmentD(4), New PointF(position.X + GetX(7.4F, width), position.Y + GetY(12.1F, height)))
            segmentD(1) = New PointF(position.X + GetX(8.4F, width), position.Y + GetY(13.0F, height))
            segmentD(2) = New PointF(position.X + GetX(1.3F, width), position.Y + GetY(13.0F, height))
            segmentD(3) = New PointF(position.X + GetX(2.2F, width), position.Y + GetY(12.1F, height))

            'Segment E
            Dim segmentE As PointF() = New PointF(4) {}
            segmentE(0) = InlineAssignHelper(segmentE(4), New PointF(position.X + GetX(2.2F, width), position.Y + GetY(11.8F, height)))
            segmentE(1) = New PointF(position.X + GetX(1.0F, width), position.Y + GetY(12.7F, height))
            segmentE(2) = New PointF(position.X + GetX(1.7F, width), position.Y + GetY(7.2F, height))
            segmentE(3) = New PointF(position.X + GetX(2.8F, width), position.Y + GetY(7.7F, height))

            'Segment F
            Dim segmentF As PointF() = New PointF(4) {}
            segmentF(0) = InlineAssignHelper(segmentF(4), New PointF(position.X + GetX(3.0F, width), position.Y + GetY(6.4F, height)))
            segmentF(1) = New PointF(position.X + GetX(1.8F, width), position.Y + GetY(6.8F, height))
            segmentF(2) = New PointF(position.X + GetX(2.6F, width), position.Y + GetY(1.3F, height))
            segmentF(3) = New PointF(position.X + GetX(3.6F, width), position.Y + GetY(2.2F, height))

            'Segment G
            Dim segmentG As PointF() = New PointF(6) {}
            segmentG(0) = InlineAssignHelper(segmentG(6), New PointF(position.X + GetX(2.0F, width), position.Y + GetY(7.0F, height)))
            segmentG(1) = New PointF(position.X + GetX(3.1F, width), position.Y + GetY(6.5F, height))
            segmentG(2) = New PointF(position.X + GetX(8.3F, width), position.Y + GetY(6.5F, height))
            segmentG(3) = New PointF(position.X + GetX(9.0F, width), position.Y + GetY(7.0F, height))
            segmentG(4) = New PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.5F, height))
            segmentG(5) = New PointF(position.X + GetX(2.9F, width), position.Y + GetY(7.5F, height))

            '#End Region

            '#Region "Draw Segments Outline"

            g.FillPolygon(outline.Brush, segmentA)
            g.FillPolygon(outline.Brush, segmentB)
            g.FillPolygon(outline.Brush, segmentC)
            g.FillPolygon(outline.Brush, segmentD)
            g.FillPolygon(outline.Brush, segmentE)
            g.FillPolygon(outline.Brush, segmentF)
            g.FillPolygon(outline.Brush, segmentG)
            outline.Dispose()

            '#End Region

            If isOnlyOutline Then
                Return
            End If

            Dim fillPen As New Pen(foreColor)

            '#Region "Fill Segments"

            'Fill SegmentA
            If IsNumberAvailable(number, 0, 2, 3, 5, 6, _
             7, 8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentA)
            End If

            'Fill SegmentB
            If IsNumberAvailable(number, 0, 1, 2, 3, 4, _
             7, 8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentB)
            End If

            'Fill SegmentC
            If IsNumberAvailable(number, 0, 1, 3, 4, 5, _
             6, 7, 8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentC)
            End If

            'Fill SegmentD
            If IsNumberAvailable(number, 0, 2, 3, 5, 6, _
             8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentD)
            End If

            'Fill SegmentE
            If IsNumberAvailable(number, 0, 2, 6, 8) Then
                g.FillPolygon(fillPen.Brush, segmentE)
            End If

            'Fill SegmentF
            If IsNumberAvailable(number, 0, 4, 5, 6, 7, _
             8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentF)
            End If

            'Fill SegmentG
            If IsNumberAvailable(number, 2, 3, 4, 5, 6, _
             8, 9) Then
                g.FillPolygon(fillPen.Brush, segmentG)
            End If
            fillPen.Dispose()

            '#End Region
        End Sub

        ''' <summary>
        ''' Gets Relative X for the given width to draw digit.
        ''' </summary>
        Private Shared Function GetX(ByVal x As Single, ByVal width As Single) As Single
            Return x * width / 12
        End Function

        ''' <summary>
        ''' Gets relative Y for the given height to draw digit.
        ''' </summary>
        Private Shared Function GetY(ByVal y As Single, ByVal height As Single) As Single
            Return y * height / 15
        End Function

        ''' <summary>
        ''' Returns true if a given number is available in the given list.
        ''' </summary>
        Private Shared Function IsNumberAvailable(ByVal number As Integer, ByVal ParamArray listOfNumbers As Integer()) As Boolean
            If listOfNumbers.Length > 0 Then
                Dim results = From n In listOfNumbers Where n = number

                If results.Count() > 0 Then
                    Return True
                End If
            End If

            Return False
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function

#End Region
    End Class
End Namespace