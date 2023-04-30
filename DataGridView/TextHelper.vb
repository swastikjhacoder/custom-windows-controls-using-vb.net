
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Namespace UIControls.DataGridView
    Public NotInheritable Class TextHelper
        Private Sub New()
        End Sub
        Public Shared Function TranslateAligment(ByVal aligment As HorizontalAlignment) As StringAlignment
            If aligment = HorizontalAlignment.Left Then
                Return StringAlignment.Near
            ElseIf aligment = HorizontalAlignment.Right Then
                Return StringAlignment.Far
            Else
                Return StringAlignment.Center
            End If
        End Function

        Public Shared Function TranslateGridColumnAligment(ByVal aligment As DataGridViewContentAlignment) As HorizontalAlignment
            If aligment = DataGridViewContentAlignment.BottomLeft OrElse aligment = DataGridViewContentAlignment.MiddleLeft OrElse aligment = DataGridViewContentAlignment.TopLeft Then
                Return HorizontalAlignment.Left
            ElseIf aligment = DataGridViewContentAlignment.BottomRight OrElse aligment = DataGridViewContentAlignment.MiddleRight OrElse aligment = DataGridViewContentAlignment.TopRight Then
                Return HorizontalAlignment.Right
            Else
                Return HorizontalAlignment.Left
            End If
        End Function

        Public Shared Function TranslateAligmentToFlag(ByVal aligment As HorizontalAlignment) As TextFormatFlags
            If aligment = HorizontalAlignment.Left Then
                Return TextFormatFlags.Left
            ElseIf aligment = HorizontalAlignment.Right Then
                Return TextFormatFlags.Right
            Else
                Return TextFormatFlags.HorizontalCenter
            End If
        End Function

        Public Shared Function TranslateTrimmingToFlag(ByVal trimming As StringTrimming) As TextFormatFlags
            If trimming = StringTrimming.EllipsisCharacter Then
                Return TextFormatFlags.EndEllipsis
            ElseIf trimming = StringTrimming.EllipsisPath Then
                Return TextFormatFlags.PathEllipsis
            End If
            If trimming = StringTrimming.EllipsisWord Then
                Return TextFormatFlags.WordEllipsis
            End If
            If trimming = StringTrimming.Word Then
                Return TextFormatFlags.WordBreak
            Else
                Return TextFormatFlags.[Default]
            End If
        End Function
    End Class
End Namespace