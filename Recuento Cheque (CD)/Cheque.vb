Public Class Cheque

    Private _micr As String
    Private _imagenA As String
    Private _imagenR As String
    Private _imagenABitmap As Bitmap
    Private _imagenRBitmap As Bitmap
    Private _monto As Single
    Private _fecha As Date

    Public Property Micr As String
        Get
            Return _micr
        End Get
        Set(value As String)
            _micr = value
        End Set
    End Property

    Public Property ImagenA As String
        Get
            Return _imagenA
        End Get
        Set(value As String)
            _imagenA = value
        End Set
    End Property

    Public Property ImagenR As String
        Get
            Return _imagenR
        End Get
        Set(value As String)
            _imagenR = value
        End Set
    End Property

    Public Property Monto As Single
        Get
            Return _monto
        End Get
        Set(value As Single)
            _monto = value
        End Set
    End Property

    Public Property Fecha As Date
        Get
            Return _fecha
        End Get
        Set(value As Date)
            _fecha = value
        End Set
    End Property

    Public Property ImagenABitmap As Bitmap
        Get
            Return _imagenABitmap
        End Get
        Set(value As Bitmap)
            _imagenABitmap = value
        End Set
    End Property

    Public Property ImagenRBitmap As Bitmap
        Get
            Return _imagenRBitmap
        End Get
        Set(value As Bitmap)
            _imagenRBitmap = value
        End Set
    End Property

    Public Sub New(ByVal micr As String, ByVal imagenA As Bitmap, ByVal imagenR As Bitmap)
        Me.Micr = micr
        Me.ImagenA = GetBase64Code(imagenA)
        Me.ImagenA = GetBase64Code(imagenR)
        Me.ImagenABitmap = imagenA
        Me.ImagenRBitmap = imagenR
        Me.Fecha = Date.Now
        Me.Monto = 0
    End Sub

    Private Function GetBase64Code(ByVal imagen As Bitmap) As String
        Dim bitmapBytes As Byte()

        Using stream As New System.IO.MemoryStream

            imagen.Save(stream, Imaging.ImageFormat.Jpeg)
            bitmapBytes = stream.ToArray

        End Using

        Return Convert.ToBase64String(bitmapBytes)
    End Function

    Function Base64ToImage(ByVal base64string As String) As System.Drawing.Image
        'Setup image and get data stream together
        Dim img As System.Drawing.Image
        Dim MS As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim b64 As String = base64string.Replace(" ", "+")
        Dim b() As Byte

        'Converts the base64 encoded msg to image data
        b = Convert.FromBase64String(b64)
        MS = New System.IO.MemoryStream(b)

        'creates image
        img = System.Drawing.Image.FromStream(MS)

        Return img
    End Function
End Class
