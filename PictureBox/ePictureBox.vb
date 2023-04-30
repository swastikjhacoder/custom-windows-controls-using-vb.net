Option Strict On

Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing.Imaging

Namespace UIControls.PictureBox

    <ToolboxBitmap(GetType(Windows.Forms.PictureBox))> Public Class ePictureBox : Inherits Windows.Forms.PictureBox

#Region " Fields "

        Friend WithEvents cmsPhotoDelete As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents cmsPhotoPaste As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents cmsPhotoCopy As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents cmsPhotoCut As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents cmsPhotoLoad As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents cmsPhotoSave As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents cmsPhotoUndo As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents cmsPhoto As System.Windows.Forms.ContextMenuStrip

        Private components As System.ComponentModel.IContainer

        Private m_ReadOnly As Boolean = False
        Private m_ImageSizeLimit As Long = -1
        Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator

        Private _PreviousImage As Image = Nothing

#End Region

#Region " Properties "

        <Category("ProfilePic Settings"), Description("True sets control read only pop up menu will be hidden")> _
        Public Property [ReadOnly]() As Boolean
            Get
                Return m_ReadOnly
            End Get
            Set(ByVal Value As Boolean)
                m_ReadOnly = Value
            End Set
        End Property

        <Category("ProfilePic Settings"), Description("Maximum image file size limit"), DefaultValue(999999999)> _
        Public Property ImageSizeLimit() As Long
            Get
                Return m_ImageSizeLimit
            End Get
            Set(ByVal Value As Long)
                m_ImageSizeLimit = Value
            End Set
        End Property

#End Region

#Region " Constructors "

        Public Sub New()
            MyBase.New()
            Me.InitializeComponent()
            Me.BorderStyle = BorderStyle.FixedSingle
            'Me.Image = Global.eCreative.My.Resources.people
            Me.SizeMode = PictureBoxSizeMode.StretchImage
            Me.Size = New System.Drawing.Size(100, 111)
        End Sub

#End Region

#Region " Methods "

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ePictureBox))
            Me.cmsPhoto = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.cmsPhotoLoad = New System.Windows.Forms.ToolStripMenuItem
            Me.cmsPhotoSave = New System.Windows.Forms.ToolStripMenuItem
            Me.cmsPhotoUndo = New System.Windows.Forms.ToolStripMenuItem
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
            Me.cmsPhotoCut = New System.Windows.Forms.ToolStripMenuItem
            Me.cmsPhotoCopy = New System.Windows.Forms.ToolStripMenuItem
            Me.cmsPhotoPaste = New System.Windows.Forms.ToolStripMenuItem
            Me.cmsPhotoDelete = New System.Windows.Forms.ToolStripMenuItem
            Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
            Me.cmsPhoto.SuspendLayout()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'cmsPhoto
            '
            Me.cmsPhoto.BackColor = System.Drawing.Color.GhostWhite
            Me.cmsPhoto.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsPhotoLoad, Me.cmsPhotoSave, Me.ToolStripSeparator1, Me.cmsPhotoUndo, Me.ToolStripSeparator2, Me.cmsPhotoCut, Me.cmsPhotoCopy, Me.cmsPhotoPaste, Me.cmsPhotoDelete})
            Me.cmsPhoto.Name = "cmsSearch"
            Me.cmsPhoto.Size = New System.Drawing.Size(121, 164)
            '
            'cmsPhotoLoad
            '
            Me.cmsPhotoLoad.Image = CType(resources.GetObject("cmsPhotoLoad.Image"), System.Drawing.Image)
            Me.cmsPhotoLoad.Name = "cmsPhotoLoad"
            Me.cmsPhotoLoad.Size = New System.Drawing.Size(120, 22)
            Me.cmsPhotoLoad.Text = "Load..."
            '
            'cmsPhotoSave
            '
            Me.cmsPhotoSave.Image = CType(resources.GetObject("cmsPhotoSave.Image"), System.Drawing.Image)
            Me.cmsPhotoSave.Name = "cmsPhotoSave"
            Me.cmsPhotoSave.Size = New System.Drawing.Size(120, 22)
            Me.cmsPhotoSave.Text = "Save"
            '
            'cmsPhotoUndo
            '
            Me.cmsPhotoUndo.Image = CType(resources.GetObject("cmsPhotoUndo.Image"), System.Drawing.Image)
            Me.cmsPhotoUndo.Name = "cmsPhotoUndo"
            Me.cmsPhotoUndo.Size = New System.Drawing.Size(120, 22)
            Me.cmsPhotoUndo.Text = "Undo"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(117, 6)
            '
            'cmsPhotoCut
            '
            Me.cmsPhotoCut.Image = CType(resources.GetObject("cmsPhotoCut.Image"), System.Drawing.Image)
            Me.cmsPhotoCut.Name = "cmsPhotoCut"
            Me.cmsPhotoCut.Size = New System.Drawing.Size(120, 22)
            Me.cmsPhotoCut.Text = "Cut"
            '
            'cmsPhotoCopy
            '
            Me.cmsPhotoCopy.Image = CType(resources.GetObject("cmsPhotoCopy.Image"), System.Drawing.Image)
            Me.cmsPhotoCopy.Name = "cmsPhotoCopy"
            Me.cmsPhotoCopy.Size = New System.Drawing.Size(120, 22)
            Me.cmsPhotoCopy.Text = "Copy"
            '
            'cmsPhotoPaste
            '
            Me.cmsPhotoPaste.Image = CType(resources.GetObject("cmsPhotoPaste.Image"), System.Drawing.Image)
            Me.cmsPhotoPaste.Name = "cmsPhotoPaste"
            Me.cmsPhotoPaste.Size = New System.Drawing.Size(120, 22)
            Me.cmsPhotoPaste.Text = "Paste"
            '
            'cmsPhotoDelete
            '
            Me.cmsPhotoDelete.Image = CType(resources.GetObject("cmsPhotoDelete.Image"), System.Drawing.Image)
            Me.cmsPhotoDelete.Name = "cmsPhotoDelete"
            Me.cmsPhotoDelete.Size = New System.Drawing.Size(120, 22)
            Me.cmsPhotoDelete.Text = "Delete"
            '
            'ToolStripSeparator2
            '
            Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
            Me.ToolStripSeparator2.Size = New System.Drawing.Size(117, 6)
            '
            'ProfilePic
            '
            Me.cmsPhoto.ResumeLayout(False)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Private Sub ProfilePic_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
            If e.Button = MouseButtons.Right AndAlso Not Me.ReadOnly Then
                Me.ContextMenuStrip = Me.cmsPhoto
            Else : Me.ContextMenuStrip = Nothing
            End If
        End Sub

        Private Sub cmsPhoto_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsPhoto.Opening

            If Me.Image Is Nothing Then
                Me.cmsPhotoSave.Enabled = False
                Me.cmsPhotoCut.Enabled = False
                Me.cmsPhotoCopy.Enabled = False
                Me.cmsPhotoDelete.Enabled = False
            Else
                Me.cmsPhotoSave.Enabled = True
                Me.cmsPhotoCut.Enabled = True
                Me.cmsPhotoCopy.Enabled = True
                Me.cmsPhotoDelete.Enabled = True
            End If

            If _PreviousImage Is Nothing Then
                Me.cmsPhotoUndo.Enabled = False
            Else : Me.cmsPhotoUndo.Enabled = True
            End If

            If Clipboard.ContainsImage Then
                Me.cmsPhotoPaste.Enabled = True
            Else : Me.cmsPhotoPaste.Enabled = False
            End If

        End Sub

        Private Sub cmsPhotoUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPhotoUndo.Click
            Me.Image = _PreviousImage
        End Sub

        Private Sub cmsPhotoSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPhotoSave.Click
            SaveImage(Me.Image)
        End Sub

        Private Sub cmsPhotoCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPhotoCut.Click
            Me.cmsPhotoCopy_Click(Me, EventArgs.Empty)
            Me.cmsPhotoDelete_Click(Me, EventArgs.Empty)
        End Sub

        Private Sub cmsPhotoCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPhotoCopy.Click

            Try

                Clipboard.SetImage(Me.Image)

            Catch ex As Exception
                Return
            End Try

        End Sub

        Private Sub cmsPhotoPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPhotoPaste.Click

            Try

                If Me.Image IsNot Nothing Then _PreviousImage = Me.Image

                Dim data() As Byte = ReadBytes(Clipboard.GetImage())

                If Me.ImageSizeLimit > 0 AndAlso data.LongLength > Me.ImageSizeLimit Then
                    Throw New ArgumentException("Image is bigger than allowed maximum size!")
                Else : If Clipboard.ContainsImage Then Me.Image = Clipboard.GetImage()
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try

        End Sub

        Private Sub cmsPhotoDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPhotoDelete.Click
            _PreviousImage = Me.Image
            Me.Image = Nothing
        End Sub

        Private Sub cmsPhotoLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPhotoLoad.Click
            LoadImage(Me, Me.ImageSizeLimit)
        End Sub

        ''' <summary>
        ''' Browses for an Image that it loads into the supplied image control
        ''' the image should be equal to or below the set file size limit
        ''' </summary>
        ''' <param name="sourceControl"></param>
        ''' <param name="fileSizeLimit"></param>
        ''' <remarks></remarks>
        Private Sub LoadImage(ByVal sourceControl As Control, ByVal fileSizeLimit As Long)

            Dim openFileDLG As New OpenFileDialog()

            Try
                Cursor.Current = Cursors.WaitCursor()

                With openFileDLG
                    Try
                        .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
                    Catch ex As Exception
                        Exit Try
                    End Try

                    .Filter = "JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|Bitmap (*.bmp) |*.bmp|" & _
                              "GIF (*.gif) |*.gif|PNG (*.png) |*.png"
                End With

                If openFileDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim data() As Byte = ReadBytes(openFileDLG.FileName.ToString())
                    If fileSizeLimit > 0 AndAlso data.LongLength > fileSizeLimit Then
                        Throw New ArgumentException("Image is bigger than allowed maximum size!")
                    Else
                        Dim ms As New MemoryStream(CType(data, Byte()))
                        If TypeOf sourceControl Is Windows.Forms.PictureBox Then
                            CType(sourceControl, Windows.Forms.PictureBox).Image = Image.FromStream(ms)
                        End If
                    End If
                End If

            Catch eX As Exception
                MessageBox.Show(eX.Message)

            Finally
                Cursor.Current = Cursors.Default()

            End Try

        End Sub

        ''' <summary>
        ''' Browses for an Image that it loads into the supplied image control
        ''' </summary>
        ''' <param name="sourceControl"></param>
        ''' <remarks></remarks>
        Private Sub LoadImage(ByVal sourceControl As Control)
            LoadImage(sourceControl, -1)
        End Sub

        ''' <summary>
        ''' Returns bytes of a file from the supplied path
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ReadBytes(ByVal path As String) As Byte()

            Try

                'A stream of bytes that represnts the binary file
                Dim fs As FileStream = New FileStream(path, FileMode.Open, FileAccess.Read)

                'The reader reads the binary data from the file stream
                Dim reader As BinaryReader = New BinaryReader(fs)

                'Bytes from the binary reader stored in data array
                Dim data() As Byte = reader.ReadBytes(CInt(fs.Length))

                fs.Close()
                reader.Close()

                Return data

            Catch ex As Exception
                Throw ex

            End Try

        End Function

        ''' <summary>
        ''' Returns bytes of the supplied image
        ''' </summary>
        ''' <param name="data"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ReadBytes(ByVal data As Image) As Byte()

            Try

                Dim ms As New IO.MemoryStream()
                data.Save(ms, Imaging.ImageFormat.Jpeg)
                Return ms.ToArray()

            Catch ex As Exception
                Throw ex

            End Try

        End Function

        ''' <summary>
        ''' Opens a save file dialog to save the supplied image as supplied file name
        ''' </summary>
        ''' <param name="data"></param>
        ''' <param name="fileName"></param>
        ''' <remarks></remarks>
        Private Sub SaveImage(ByVal data As Image, ByVal fileName As String)

            Dim saveFileDLG As New SaveFileDialog()

            Try

                Cursor.Current = Cursors.WaitCursor()

                With saveFileDLG

                    Try
                        .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
                    Catch ex As Exception
                        Exit Try
                    End Try

                    .Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG (*.png) |*.png"
                    .Title = "Save image to a file"
                    .FileName = fileName
                    .ShowDialog()

                End With

                If Not String.IsNullOrEmpty(saveFileDLG.FileName) Then

                    ' Saves the Image via a FileStream created by the OpenFile method.
                    Dim fs As FileStream = CType(saveFileDLG.OpenFile(), FileStream)

                    ' Saves the Image in the appropriate ImageFormat based upon the
                    ' file type selected in the dialog box.
                    ' NOTE that the FilterIndex property is one-based.
                    Select Case saveFileDLG.FilterIndex

                        Case 1 : data.Save(fs, ImageFormat.Jpeg)
                        Case 2 : data.Save(fs, ImageFormat.Bmp)
                        Case 3 : data.Save(fs, ImageFormat.Gif)
                        Case 4 : data.Save(fs, ImageFormat.Png)

                    End Select

                    fs.Close()

                End If

            Catch eX As Exception
                MessageBox.Show(eX.Message)

            Finally
                Cursor.Current = Cursors.Default()

            End Try

        End Sub

        ''' <summary>
        ''' Opens a save file dialog to save the supplied image as Photo
        ''' </summary>
        ''' <param name="data"></param>
        ''' <remarks></remarks>
        Private Sub SaveImage(ByVal data As Image)
            SaveImage(data, "Photo")
        End Sub

#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

End Namespace

