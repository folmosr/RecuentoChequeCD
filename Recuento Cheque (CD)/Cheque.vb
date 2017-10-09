Public Class Cheque

#Region "Fields"

    Private _codBanco As String
    Private _codPlza As String
    Private _ctaCorriente As String
    Private _fecha As Date
    Private _imagenA As String
    Private _imagenABitmap As Bitmap
    Private _imagenR As String
    Private _imagenRBitmap As Bitmap
    Private _micr As String
    Private _monto As Single
    Private _nroCheque As String
    Private _id_recuento_contenedor As Int32

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal micr As String, ByVal imagenA As Bitmap, ByVal imagenR As Bitmap, ByVal idRecuento As Int32)
        Me.Micr = micr.Replace(">", String.Empty).Replace("<", String.Empty).Replace(":", String.Empty)
        If (Me.Micr.Length = 29) Then
            Me.NroCheque = Me.Micr.Substring(0, 7)
            Me.CodBanco = Me.Micr.Substring(8, 3)
            Me.CodPlza = Me.Micr.Substring(11, 4)
            Me.CtaCorriente = Me.Micr.Substring(15, 11)
            Me.Micr = Me.Micr.Substring(0, 27)
        End If
        Me.ImagenA = GetBase64Code(imagenA)
        Me.ImagenA = GetBase64Code(imagenR)
        Me.ImagenABitmap = imagenA
        Me.ImagenRBitmap = imagenR
        Me.Fecha = Date.Now
        Me.Id_recuento_contenedor = idRecuento
        Me.Monto = 0
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public Property CodBanco() As String
        Get
            Return _codBanco
        End Get
        Set(value As String)
            _codBanco = value
        End Set
    End Property

    Public Property CodPlza() As String
        Get
            Return _codPlza
        End Get
        Set(value As String)
            _codPlza = value
        End Set
    End Property

    Public Property CtaCorriente() As String
        Get
            Return _ctaCorriente
        End Get
        Set(value As String)
            _ctaCorriente = value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return _fecha
        End Get
        Set(value As Date)
            _fecha = value
        End Set
    End Property

    Public Property ImagenA() As String
        Get
            Return _imagenA
        End Get
        Set(value As String)
            _imagenA = value
        End Set
    End Property

    Public Property ImagenABitmap() As Bitmap
        Get
            Return _imagenABitmap
        End Get
        Set(value As Bitmap)
            _imagenABitmap = value
        End Set
    End Property

    Public Property ImagenR() As String
        Get
            Return _imagenR
        End Get
        Set(value As String)
            _imagenR = value
        End Set
    End Property

    Public Property ImagenRBitmap() As Bitmap
        Get
            Return _imagenRBitmap
        End Get
        Set(value As Bitmap)
            _imagenRBitmap = value
        End Set
    End Property

    Public Property Micr() As String
        Get
            Return _micr
        End Get
        Set(value As String)
            _micr = value
        End Set
    End Property

    Public Property Monto() As Single
        Get
            Return _monto
        End Get
        Set(value As Single)
            _monto = value
        End Set
    End Property

    Public Property NroCheque() As String
        Get
            Return _nroCheque
        End Get
        Set(value As String)
            _nroCheque = value
        End Set
    End Property

    Public Property Id_recuento_contenedor As Int32
        Get
            Return _id_recuento_contenedor
        End Get
        Set(value As Int32)
            _id_recuento_contenedor = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub SetMICR()
        Me.Micr = (Me.NroCheque & Me.CodBanco & Me.CodPlza & Me.CtaCorriente)
    End Sub

    Private Function Base64ToImage(ByVal base64string As String) As System.Drawing.Image
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

    Private Function GetBase64Code(ByVal imagen As Bitmap) As String
        Dim bitmapBytes As Byte()

        Using stream As New System.IO.MemoryStream

            imagen.Save(stream, Imaging.ImageFormat.Jpeg)
            bitmapBytes = stream.ToArray

        End Using

        Return Convert.ToBase64String(bitmapBytes)
    End Function

#End Region 'Methods

End Class