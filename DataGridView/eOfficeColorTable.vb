Imports System.Drawing

Namespace UIControls.DataGridView
    Public Class eOfficeColorTable


        Public ReadOnly Property ColumnHeaderStartColor() As Color
            Get
                Return Color.FromArgb(245, 249, 251)
            End Get
        End Property

        Public ReadOnly Property ColumnHeaderMidColor1() As Color
            Get
                Return Color.FromArgb(234, 239, 245)
            End Get
        End Property

        Public ReadOnly Property ColumnHeaderMidColor2() As Color
            Get
                Return Color.FromArgb(224, 231, 240)
            End Get
        End Property

        Public ReadOnly Property ColumnHeaderEndColor() As Color
            Get
                Return Color.FromArgb(212, 220, 233)
            End Get
        End Property

        Public ReadOnly Property ColumnHeaderActiveStartColor() As Color
            Get
                Return Color.FromArgb(248, 214, 152)
            End Get
        End Property

        Public ReadOnly Property ColumnHeaderActiveMidColor1() As Color
            Get
                Return Color.FromArgb(246, 207, 131)
            End Get
        End Property

        Public ReadOnly Property ColumnHeaderActiveMidColor2() As Color
            Get
                Return Color.FromArgb(244, 201, 117)
            End Get
        End Property

        Public ReadOnly Property ColumnHeaderActiveEndColor() As Color
            Get
                Return Color.FromArgb(242, 195, 99)
            End Get
        End Property

        Public ReadOnly Property GridColor() As Color
            Get
                Return Color.FromArgb(208, 215, 229)
            End Get
        End Property

        Public ReadOnly Property DefaultCellColor() As Color
            Get
                Return Color.FromArgb(255, 255, 255)
            End Get
        End Property

        Public ReadOnly Property ActiveCellColor() As Color
            Get
                Return Color.FromArgb(135, 169, 213)
            End Get
        End Property

        Public ReadOnly Property ReadonlyCellColor() As Color
            Get
                Return Color.FromArgb(138, 138, 138)
            End Get
        End Property

        Public ReadOnly Property ActiveBorderColor() As Color
            Get
                Return Color.FromArgb(255, 189, 105)
            End Get
        End Property
    End Class

End Namespace