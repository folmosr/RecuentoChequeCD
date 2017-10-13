
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Deployment.Application
Imports System.IO
Imports System.Security.Principal
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web

Imports Recuento_Cheque__CD_

Public Class Recuento


#Region "Private Methods"

    Private Function ActualizaCheque() As Boolean
        Dim currentDate As Date = DateTime.Now()
        Dim res As Boolean = DateTime.TryParse(DtFecha.Value.ToString(), currentDate)
        If ((Convert.ToSingle(TxtMonto.Text) > 0) And (res) And (IsValidMonto())) Then
            Dim objCheque As Cheque = Modulo.ListaCheques.ElementAt(Modulo.Indice)
            If (Not objCheque.Micr.IndexOf("?") > -1) Then
                objCheque.Monto = Convert.ToSingle(TxtMonto.Text)
                objCheque.Fecha = DtFecha.Value
                ActualizaTotal()
            Else
                MessageBox.Show("Código CMC7 inválido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                Micr.Show()
                Return False
            End If
        Else
            If (Not res) Then
                MessageBox.Show("Fecha inválida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                DtFecha.Select()
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub ActualizaTotal()
        Dim sum As Single = 0
        sum = Modulo.ListaCheques.Sum(Function(item) item.Monto)
        TxtTotal.Text = sum.ToString("#,#.00#;(#,#.00#)")
    End Sub

    Private Sub BloqueaDetalle()
        TxtMonto.Enabled = False
        DtFecha.Enabled = False
        TxtTotal.Enabled = False
        BtnFirst.Enabled = False
        BtnBack.Enabled = False
        BtnNext.Enabled = False
        BtnLast.Enabled = False
        BtnGuardar.Enabled = False
        BtnEliminar.Enabled = False
        BtnProcesar.Enabled = False
        BtnRestart.Enabled = False
        BtnExpulsar.Enabled = False
        BtnBuscar.Enabled = False
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        If (ActualizaCheque()) Then
            Modulo.Indice -= 1
            Modulo.Indice = If(Modulo.Indice < 0, 0, Modulo.Indice)

            SetDatosAControles()
            If (Modulo.Indice = 0) Then
                BtnBack.Enabled = False
            End If
            If (Not BtnNext.Enabled) Then
                BtnNext.Enabled = True
            End If
        End If
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Buscar.Show()
    End Sub

    'Private Sub TxtMonto_LostFocus(sender As Object, e As EventArgs) Handles TxtMonto.LostFocus
    '    If (TxtMonto.Text = 0) Then
    '        MessageBox.Show("Se requiere Monto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
    '        TxtMonto.Select()
    '        Return
    '    End If
    '    Dim pattern As New Regex("^(?!0+\,00)(?=.{1,9}(\,|$))(?!0(?!\,))\d{1,3}(\.\d{3})*(\,\d+)?$")
    '    If (Not pattern.IsMatch(TxtMonto.Text)) Then
    '        MessageBox.Show("Monto Inválido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
    '        TxtMonto.Select()
    '        Return
    '    End If
    'End Sub
    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Modulo.ListaCheques.RemoveAt(Indice)
        ReiniciaControlesDetalle()
        If ((Indice = 0) And (Indice = ListaCheques.Count)) Then
            MessageBox.Show("Ya no hay cheques que mostrar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        ElseIf (Modulo.Indice = Modulo.ListaCheques.Count) Then
            Modulo.Indice = 0
        Else
            SetDatosAControles()
            ActualizaTotal()
        End If
    End Sub

    Private Sub BtnExpulsar_Click(sender As Object, e As EventArgs) Handles BtnExpulsar.Click
        BtnExpulsar.Enabled = False
        BUICEjectDocument()
        BtnExpulsar.Enabled = True
    End Sub

    'Private Sub CalculaTotal()
    '    Dim total As Single = 0
    '    For Each objCheque As Cheque In Modulo.ListaCheques
    '        total += objCheque.Monto
    '    Next
    '    TxtTotal.Text = total.ToString("#,#.00#;(#,#.00#)")
    'End Sub
    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles BtnFirst.Click
        Modulo.Indice = 0
        SetDatosAControles()
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim len As Int32 = Modulo.ListaCheques.Count
        DigiAvanzada(999)
        Modulo.Indice = len
        SetDatosAControles()
        SetMaximunToProgressBar()
    End Sub

    Private Sub BtnLast_Click(sender As Object, e As EventArgs) Handles BtnLast.Click
        Modulo.Indice = (Modulo.ListaCheques.Count - 1)
        SetDatosAControles()
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        If (ActualizaCheque()) Then
            Modulo.Indice += 1
            SetDatosAControles()
        End If
    End Sub

    Private Sub BtnProcesar_Click(sender As Object, e As EventArgs) Handles BtnProcesar.Click
        ProgressBar1.Increment(1)
        If (ActualizaCheque()) Then
            Dim Db As DataAccesss = New DataAccesss()
            If (Db.Process(ListaCheques.ConvertToDataTable())) Then
                UploadFile()
                MessageBox.Show("Proceso realizado satisfactoriamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Application.Exit()
            End If
        End If
    End Sub

    Private Sub BtnRestart_Click(sender As Object, e As EventArgs) Handles BtnRestart.Click
        Dim result As Integer = MessageBox.Show("¿Está seguro de reliazar esta acción? Perderá toda la información.", "Reinicar Proceso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        If result = DialogResult.Yes Then
            Modulo.ListaCheques.Clear()
            Modulo.DocsMin = 0
            ReiniciaControlesDetalle()
            DigiAvanzada(999)
            MostrarPrimerChequeEnLista()
            SetMaximunToProgressBar()
        End If
    End Sub

    Private Sub DesbloqueaDetalle()
        TxtMonto.Enabled = True
        DtFecha.Enabled = True
        TxtTotal.Enabled = True
        BtnFirst.Enabled = True
        BtnBack.Enabled = True
        BtnNext.Enabled = True
        BtnLast.Enabled = True
        BtnGuardar.Enabled = True
        BtnEliminar.Enabled = True
        BtnProcesar.Enabled = True
        BtnRestart.Enabled = True
        BtnExpulsar.Enabled = True
        BtnBuscar.Enabled = True
    End Sub

    Private Sub DigiAvanzada(ByVal DocNum As Integer)
        Dim IndicesTif As IO.StreamWriter = New StreamWriter(PathImagenes + "Modulo.IndicesTif.Dat", True, Encoding.Default)
        Dim IndicesJpg As IO.StreamWriter = New StreamWriter(PathImagenes + "Modulo.IndicesJpg.Dat", True, Encoding.Default)
        Dim IndicesBmp As IO.StreamWriter = New StreamWriter(PathImagenes + "Modulo.IndicesBmp.Dat", True, Encoding.Default)

        Dim ImgALen As String = Space(255)
        Dim ImgRLen As String = Space(255)
        Dim MICRLen As String = Space(255)
        Dim MICROCR As String = Space(255)
        Dim ConfOCR As String = Space(255)
        Dim MICR As String = Space(255)
        Dim EndosoSec As String = ""
        'Dim Tiempo1 As DateTime
        'Dim Tiempo2 As DateTime
        Dim Ret As MsgBoxResult
        Dim ResVE As Integer

        Dim ImgA As String = ""
        Dim ImgR As String = ""
        'Dim FontFile As String
        'Dim ResOCR As Integer
        'Dim Tamano As String

        'Dim DCCFNTf As String
        'Dim DCCFNTh As Integer
        'Dim DCCOri1 As Integer

        Dim lngMemHwndFront As Integer
        Dim lngMemHwndBack As Integer

        Dim lngMemPtrFront As Integer
        Dim lngMemPtrBack As Integer

        'Dim Banderas As Integer
        Dim DispImagenA As Bitmap
        Dim DispImagenR As Bitmap

        Dim SiguienteI As Integer

        Dim flag As Boolean = False
        Dim retry As Int16 = 0
        ' Batch
        ' =====
        'If (CBBatch.Checked) Then
        Res = BUICSetParam(160, 1)
        ' Else
        'Res = BUICSetParam(160, 0)
        'End If

        ' Resolución
        ' ==========
        Res = BUICSetParam(CFG_IMAGE_RESOLUTION, 1)
        'If (RB100.Checked) Then Res = BUICSetParam(CFG_IMAGE_RESOLUTION, 0)
        'If (RB200.Checked) Then Res = BUICSetParam(CFG_IMAGE_RESOLUTION, 1)
        'If (RB300.Checked) Then Res = BUICSetParam(CFG_IMAGE_RESOLUTION, 4)

        ' Código de Banda
        ' ===============
        Res = BUICSetParam(BPARAM_MAGNTYPE, 0)
        'If (RBCMC7.Checked) Then Res = BUICSetParam(BPARAM_MAGNTYPE, 0)
        'If (RBE13B.Checked) Then Res = BUICSetParam(BPARAM_MAGNTYPE, 1)
        'If (RBEOCR.Checked) Then Res = BUICSetParam(BPARAM_MAGNTYPE, 1)

        ' 8 Bits 256 Tonos
        ' ================
        Res = BUICSetParam(CFG_IMAGE_GRAY256LEVEL, 1)

        ' Calidad JPEG
        ' ============
        Res = BUICSetParam(CFG_MISC_JPEG_QUALITY, 75)

        ' OCR
        ' ===
        Res = BUICSetParam(TBPARAM_EXTREADER_TYPE, 1)

        ' ThresHold
        ' =========
        Res = BUICSetParam(BPARAM_FRONTTHRESHOLD, 9)
        Res = BUICSetParam(BPARAM_REARTHRESHOLD, 9)

        ' Double Feed
        ' ===========
        Res = BUICSetParam(BPARAM_PHOTOCELL, 1)

        ' Color
        ' =====
        'If (RBColor.Checked) Then
        'Res = BUICSetParam(119, 1)
        'Else
        Res = BUICSetParam(119, 0)
        'End If

        ' CX 30 Salida
        ' ============
        'If (CX30 And DocNum > 1) Then
        'Res = BUICSetParam(191, 0)
        'Else
        Res = BUICSetParam(191, 1)
        'End If

        ' SORT
        ' ====
        'If (SSort And SortEnabled And Not CBBatch.Checked) Then
        ' Res = BUICSetParam(CFG_DEV_SORTER, 1)
        'Res = BUICSetParam(TBPARAM_SORTER_INPUT, 0)
        'Else
        'If (SSort And SortEnabled And CBBatch.Checked) Then
        ' Res = BUICSetParam(CFG_DEV_SORTER, 1)
        ' Res = BUICSetParam(TBPARAM_SORTER_INPUT, 1)
        ' Dim Func As MyFunc
        'Func = New MyFunc(AddressOf MICRCallBack)
        ' Res = funcSetUpCallBack(&H100000, Func)
        'Else
        Res = BUICSetParam(CFG_DEV_SORTER, 0)
        '    End If
        'End If

        ' DCC Franqueo
        ' ============
        'If (CX30 Or TS240) Then
        Res = BUICSetParam(196, Franqueo)
        'Else
        'Res = BUICSetParam(196, 0)
        'End If

        ' Endoso Virtual
        ' ==============
        Res = BUICSetParam(170, 1)

        ' Reserva Memoria
        ' ===============
        If Not MemoryGet(lngMemHwndFront, lngMemPtrFront) Then
            MsgBox("Unable to allocate memory for Front image.")
        End If

        If Not MemoryGet(lngMemHwndBack, lngMemPtrBack) Then
            MsgBox("Unable to allocate memory for Back image.")
        End If

        ' Endoso Real
        ' ===========
        'If (EndosoReal) Then
        'If (String.Compare(Endoso_Sec, "1") = 0) Then
        'If (RBBMP.Checked) Then EndosoSec = SiguienteImagen(3).ToString("00000")
        ' If (RBTiff.Checked) Then EndosoSec = SiguienteImagen(1).ToString("00000")
        '  If (RBJPEG.Checked) Then EndosoSec = SiguienteImagen(2).ToString("00000")
        '   If (RBColor.Checked) Then EndosoSec = SiguienteImagen(2).ToString("00000")
        'Else
        '   EndosoSec = ""
        'End If
        'If (EndosoBmpI) Then
        'Res = BUICSetParam(CFG_DEV_PRINTER, 1)
        'CheckEndorsementStart(ScannerType, 1000)
        ' CheckEndorsementText(ScannerType, Convert.ToInt32(Endoso_Hgh), 0, 1, 1, 0, Endoso_Fnt, EndosoText + " " + EndosoSec)
        '  CheckEndorsementEnd(ScannerType, PathInicio + "Endoso.Bmp")
        '   Res = funcTS400SetPrint(35, Convert.ToInt32(Endoso_Pos), PathInicio + "Endoso.Bmp")
        'End If
        'If (EndosoFont) Then
        'Res = BUICSetParam(CFG_DEV_PRINTER, 1)
        'If (ScannerType = 200) Then
        '     FontFile = PathInicio + "Ts200_IJAsciiFont.bin"
        '  Else
        '       FontFile = PathInicio + "Pc2424.fnt"
        '    End If
        '     Res = funcTS400SetLoadFont(0, 0, FontFile)
        '      Res = funcTS400SetPrint(34, Convert.ToInt32(Endoso_Pos), EndosoText + " " + EndosoSec)
        '   End If
        'Else
        Res = BUICSetParam(CFG_DEV_PRINTER, 0)
        'End If

        If (Res >= 0) Then
            For Ciclo = 1 To DocNum
                ImgALen = Space(255)
                ImgRLen = Space(255)
                MICRLen = Space(255)
                MICROCR = Space(255)
                ConfOCR = Space(255)
                MICR = Space(255)

                'Tiempo1 = DateTime.Now.AddSeconds(5)
                'If (CX30 And DocNum > 1) Then
                ' Do
                ' Res = BUICStatus()
                'Tiempo2 = DateTime.Now()
                'Application.DoEvents()
                'Loop Until (Res = 1 Or Tiempo1 < Tiempo2)
                'End If

                'If (RBTiff.Checked) Then
                'Banderas = 6
                'If (CB1.Checked) Then Banderas = Banderas + 1
                'If (CB4.Checked) Then Banderas = Banderas + 8
                'If (CB5.Checked) Then Banderas = Banderas + 16
                'If (CB6.Checked) Then Banderas = Banderas + 32
                'If (CB7.Checked) Then Banderas = Banderas + 64
                'If (CB8.Checked) Then Banderas = Banderas + 256
                SiguienteI = SiguienteImagen(1)
                ImagenActual = "A" + SiguienteI.ToString("00000") + ".Tif"
                ImgA = PathImagenes + "A" + SiguienteI.ToString("00000") + ".Tif"
                ImgR = PathImagenes + "R" + SiguienteI.ToString("00000") + ".Tif"
                Res = BUICScanMemoryGray(7, lngMemPtrFront, ImgALen, lngMemPtrBack, ImgRLen, MICR, MICRLen, 3)

                'If (DCCEndosoA Or DCCEndosoR) Then
                'ResVE = BUICSetParam(170, 1)
                'If (DCCendosoS) Then
                'EndosoSec = DCCendosoT + " " + SiguienteI.ToString()
                'Else
                '   EndosoSec = DCCendosoT
                'End If
                '   EndosoSec = EndosoSec.Replace("!|", Chr(10))
                '  Select Case DCCEndosoO.Substring(0, 3)
                ' Case "000" : DCCOri1 = 0
                'Case "090" : DCCOri1 = 1
                'Case "180" : DCCOri1 = 2
                'Case "270" : DCCOri1 = 3
                'End Select
                'DCCFNTh = Convert.ToInt32(DCCEndosoF.Substring(DCCEndosoF.Length - 2, 2))
                'DCCFNTf = DCCEndosoF.Substring(0, DCCEndosoF.Length - 3)
                'If (DCCEndosoA) Then ResVE = DCCVirtualEndorsement(lngMemPtrFront, EndosoSec, DCCFNTh, DCCEndosoX, DCCEndosoY, 1, 0, DCCOri1, 0, 0, 0, 0, DCCFNTf)
                'If (DCCEndosoR) Then ResVE = DCCVirtualEndorsement(lngMemPtrBack, EndosoSec, DCCFNTh, DCCEndosoX, DCCEndosoY, 1, 0, DCCOri1, 0, 0, 0, 0, DCCFNTf)
                'Else
                ResVE = BUICSetParam(170, 0)
                ' End If

                If (Res >= 0) Then
                    Res = funcConvGrayImageEdgeDetectBW(lngMemPtrFront, ImgA, 500, 6)
                    Res = funcConvGrayImageEdgeDetectBW(lngMemPtrBack, ImgR, 500, 6)
                    If (Res >= 0) Then
                        MICR = MICR.Substring(0, MICR.IndexOf(Chr(0)))
                        MICR = MICR.Replace("@", "?")
                        IndicesTif.WriteLine(MICR.PadRight(50) + ImgA + " " + ImgR)
                    Else
                        ImagenActual = "A" + (SiguienteI - 1).ToString("00000") + ".Tif"
                    End If
                    'If (Res >= 0 And RBEOCR.Checked) Then
                    'ResOCR = FindE13BMicr(ImgA, 1, 0, MICROCR, ConfOCR)
                    'If (ResOCR = 0) Then
                    'MICROCR = MICROCR.Substring(0, MICROCR.IndexOf(Chr(0)))
                    'MICROCR = MICROCR.Replace("@", "?")
                    'Else
                    'MICROCR = "---"
                    '    End If
                    'Else
                    'MICROCR = "---"
                    'End If
                Else
                    ImagenActual = "A" + (SiguienteI - 1).ToString("00000") + ".Tif"
                    MICROCR = "---"
                End If
                'End If

                If (Res >= 0) Then
                    'If (SSort And SortEnabled And Not CBBatch.Checked) Then
                    'Dim ImgSrt As Integer
                    ' If (Sort1) Then
                    'ImgSrt = Convert.ToInt32(ImagenActual.Substring(1, 5))
                    'If (ImgSrt = Math.Floor(ImgSrt / 2) * 2) Then
                    'TS400SetPocket(0)
                    'Else
                    '   TS400SetPocket(1)
                    'End If
                    'End If
                    'If (Sort2) Then
                    '        ImgSrt = Convert.ToInt32(ImagenActual.Substring(1, 5))
                    '        If (ImgSrt = Math.Floor(ImgSrt / 4) * 4) Then
                    '            TS400SetPocket(1)
                    '        Else
                    '            TS400SetPocket(0)
                    '        End If
                    '    End If
                    '    If (Sort3) Then
                    '        ImgSrt = MICR.IndexOf("?")
                    '        If (ImgSrt >= 0) Then
                    '            TS400SetPocket(1)
                    '        Else
                    '            TS400SetPocket(0)
                    '        End If
                    '    End If
                    'End If

                    Dim FileA As FileStream = New FileStream(ImgA, FileMode.Open)
                    Dim FileR As FileStream = New FileStream(ImgR, FileMode.Open)
                    'Tamano = FileA.Length + FileR.Length
                    'FileA.Close()
                    'FileR.Close()
                    DocsMin = DocsMin + 1
                    'TL1.Text = "Imagen: " + Path.GetFileName(ImgA).Substring(1, 5) + " : [ " + Tamano + " Bytes (2 Lados) ]"
                    'TL2.Text = "MICR: " + MICR
                    'TL3.Text = "OCR:" + MICROCR.Trim()
                    'StatusStrip1.Refresh()

                    ' Dim FileImage As FileStream = New FileStream(ImgA, FileMode.Open)
                    DispImagenA = New Bitmap(Image.FromStream(FileA))
                    DispImagenR = New Bitmap(Image.FromStream(FileR))
                    Modulo.ListaCheques.Add(New Cheque(MICR, DispImagenA, DispImagenR, Convert.ToInt32(Modulo.Id_Recuento_Contenedor), Modulo.Tipo_Recuento))
                    FrontPictureBox.Image = DispImagenA
                    BackPictureBox.Image = DispImagenR
                    'LblChcSerial.Text = MICR
                    LblChcCount.Text = DocsMin
                    'Imagen.Image = DispImagen
                    'Imagen.Visible = True
                    FileA.Close()
                    FileR.Close()
                    flag = True
                Else
                    'Timer1.Enabled = False
                    If (Not flag) Then
                        Ret = MessageBox.Show("Mensaje del digitalizador:" & vbCrLf & vbCrLf & Errores(Res), "Mensaje", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                        If (Ret = MsgBoxResult.Retry) Then
                            retry = 1
                        Else
                            retry = 2
                        End If
                    End If
                    Exit For
                End If
            Next Ciclo
            MemoryRelease(lngMemHwndFront)
            MemoryRelease(lngMemHwndBack)
            If (retry = 1) Then
                Reintentar(IndicesTif, IndicesJpg, IndicesBmp)
            ElseIf (retry = 2) Then
                Application.Exit()
            End If
        Else
            Ret = MessageBox.Show("Mensaje del digitalizador:" & vbCrLf & vbCrLf & Errores(Res), "Mensaje", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            If (Ret) Then
                Reintentar(IndicesTif, IndicesJpg, IndicesBmp)
            Else
                Application.Exit()
            End If
        End If
        IndicesTif.Close()
        IndicesJpg.Close()
        IndicesBmp.Close()
    End Sub

    Private Sub DtFecha_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DtFecha.KeyPress
        Dim keyvalue As Int32 = Asc(e.KeyChar)
        If (keyvalue = Modulo.ENTER) Then
            If (BtnNext.Enabled) Then
                BtnNext.Select()
            Else
                If (BtnBack.Enabled) Then
                    BtnBack.Select()
                Else
                    BtnNext.Select()
                End If
            End If
        End If
    End Sub

    Private Sub DtFecha_ValueChanged(sender As Object, e As EventArgs) Handles DtFecha.ValueChanged
        Dim currentDate As Date = DateTime.Now()
        Dim res As Boolean = DateTime.TryParse(DtFecha.Value.ToString(), currentDate)
        If (Not res) Then
            MessageBox.Show("Fecha inválida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            DtFecha.Select()
        End If
    End Sub

    Private Function Errores(ByVal elerror As Integer) As String
        Dim errmsg As String

        Select Case (elerror)
            Case -1 : errmsg = "Error de BUIC"
            Case -2 : errmsg = "No se encontró dispositivo"
            Case -3 : errmsg = "Manejador no soportado"
            Case -4 : errmsg = "Modo no válido"
            Case -5 : errmsg = "No se encontro manejador"

            Case -100 : errmsg = "No inicializado"
            Case -101 : errmsg = "Error de trabajo"
            Case -102 : errmsg = "Error en parámetro"
            Case -103 : errmsg = "BUIC error en nombre de archivo"
            Case -104 : errmsg = "Error rn tipo de imagen"
            Case -105 : errmsg = "No se encontro archivo"                   '  Unable to open file for compression. */
            Case -106 : errmsg = "Falta memoria"                            '  Unable to allocate memory.      */
            Case -107 : errmsg = "Error de lectura / escritura"             '  Error while reading/writing file*/
            Case -108 : errmsg = "Ventana no válida"                        '  Invalid window specification (1,2 ONLY) */
            Case -109 : errmsg = "Error al abrir ventana"                   '  Error opening window */
            Case -110 : errmsg = "Error al crear ventana"                   '  Error creating window */
            Case -111 : errmsg = "Error al cerrar ventana"                  '  Error closing window */
            Case -112 : errmsg = "Error al desplegar imagen"                '  Error displaying image */
            Case -113 : errmsg = "Falta memoria"                            '  Unable to allocate memory.      */
            Case -114 : errmsg = "Equipo ya inicializado"                   '  BUICInit called again without BUICExit */
            Case -120 : errmsg = "Error en BMP"
            Case -122 : errmsg = "Cadena muy larga"
            Case -123 : errmsg = "TS400 no inicializado"
            Case -124 : errmsg = "No inicializado"
            Case -125 : errmsg = "No hay comunicación con el digitalizador"
            Case -126 : errmsg = "No se encontró un scaner DCC"
            Case -315 : errmsg = "TAG no encontrado"                        '  TIFF magnetic code TAG not found*/
            Case -316 : errmsg = "Archivo no es TIF"                        '  Not a TIFF file */
            Case -317 : errmsg = "Error al crear TAG"                       '  Error setting magnetic code TAG */
            Case -318 : errmsg = "Error al copia archivo"                   '  Cannot copy file to itself */
            Case -319 : errmsg = "Error al crear TAG"                       '  Error setting TIFF magnetic code TAG */
            Case -202 : errmsg = "Error SCSI"                               '  Error during SCSI communication */
            Case -203 : errmsg = "Error en 'CLR'"                           '                                 */
            Case -204 : errmsg = "Error en 'BADGE'"                         '                                  */
            Case -205 : errmsg = "Error en 'GET_DOC'"                       '                                 */
            Case -206 : errmsg = "Error en 'SCSI_ACQ'"                      '                                 */
            Case -207 : errmsg = "Error de TIME_OUT"                        '                                 */
            Case -208 : errmsg = "Error en 'DMA TRANSFER'"                  '                                  */
            Case -209 : errmsg = "Error en 'MAGN_READER'"                   '                                  */
            Case -210 : errmsg = "No usado"                                 '  End of Job (not used)           */
            Case -211 : errmsg = "Error al enviar configuración"            '                                  */
            Case -212 : errmsg = "No existen cheques en alimentador"        '  No cheques in the feeder        */
            Case -213 : errmsg = "Error genérico"                           '  Runner: See gen_err() for more details*/
            Case -214 : errmsg = "No existe archivo de configuración"       '  No B1000.CFG file               */
            Case -215 : errmsg = "Loop sin fin"                             '  Loop not terminated             */
            Case -216 : errmsg = "No está alimentando"                      '  No feeding                      */
            Case -217 : errmsg = "Doble alimentación"                       '  2 or more document in the rail  */
            Case -218 : errmsg = "Error en archivo temporal"                '  Temporary file creation error   */
            Case -219 : errmsg = "Digitalizador abierto"                    '  Coperchio aperto                */
            Case -220 : errmsg = "Documento atorado"                        '  2 or more document in the rail  */
            Case -224 : errmsg = "Falta memoria"                            '  Memory not available            */
            Case -223 : errmsg = "Falta memoria"                            '  Memory allocation error         */
            Case -222 : errmsg = "Error en disco"                           '  Disk read/write                 */
            Case -230 : errmsg = "Salvado en grises no soportado"           '  scan to file with gray not      */
            Case -231 : errmsg = "Problema al rotar archivo"                '  Problem with rotation of file   */
            Case -232 : errmsg = "Problema con Unisoft I"                   '  Problem with UniSoft Imaging    */
            Case -233 : errmsg = "Error al escribir en disco"               '  Disk write                      */
            Case -234 : errmsg = "Problema en Unisoft I"                    '  Problem with UniSoft Imaging    */
            Case -510 : errmsg = "Problema con 'SCAN_GET_DOC'"
            Case -516 : errmsg = "Error en 'PMEC'"
            Case -517 : errmsg = "Error en SCSI"
            Case -518 : errmsg = "Error en 'LIGHTCAL'"
            Case -519 : errmsg = "Error en 'LACKLEVCAL'"
            Case -520 : errmsg = "Error en 'WHITELEVCAL'"
            Case -521 : errmsg = "Error en 'MICRRD'"
            Case -522 : errmsg = "Error en 'BADINSERT'"
            Case -523 : errmsg = "Error en cabaza de impresión"
            Case -524 : errmsg = "Error en 'SCAN FLASH'"
            Case -525 : errmsg = "Error en MICR"
            Case -526 : errmsg = "Error en 'CLEARPRT'"
            Case -527 : errmsg = "Error en 'LOADPRT'"
            Case -528 : errmsg = "Error de lectura"
            Case -529 : errmsg = "Error en 'UPDATE'"
            Case -530 : errmsg = "Error en 'GETDOC'"
            Case -540 : errmsg = "Error en 'ENDDOC'"
            Case -541 : errmsg = "Error en 'SETDOCPOS'"
            Case -542 : errmsg = "Error al realiza EJECT"
            Case -543 : errmsg = "Error en 'TMPCFG'"
            Case -545 : errmsg = "Error en 'ACQPAR'"
            Case -546 : errmsg = "Error en 'ACQPAR'"
            Case -547 : errmsg = "Error en 'PMEC'"
            Case -548 : errmsg = "Error al solicitar estado"
            Case -549 : errmsg = "Error en 'CLRERR'"
            Case -550 : errmsg = "TIME OUT"
            Case -551 : errmsg = "MICR TIME OUT"
            Case -552 : errmsg = "Error en parámetros"
            Case -553 : errmsg = "Error en cabeza de impresión'"            '  No Ink Jet Printer Head   */
            Case -554 : errmsg = "Blanco & Negro no soportado"              '  100 DPI B/W not supported */
            Case -555 : errmsg = "Bits X Pixel no válido"                   '  Bits per pixel invalid    */
            Case -556 : errmsg = "La línea mas grande de 3600"              '  Lines exceeds 3600        */
            Case -557 : errmsg = "Error en ruta de archivo de inicialización"
            Case -558 : errmsg = "Error en ruta de archivo de configuración"
            Case -600 : errmsg = "Error en Font"
            Case -601 : errmsg = "Error en Font"
            Case -602 : errmsg = "Error al clasificar"                      '  Pocket selection ??????   */
            Case -603 : errmsg = "Error en Flash memory"                    '  Error while writing to    */
            Case -604 : errmsg = "Time Out en clasificación"                '  Host sort time out        */
            Case -605 : errmsg = "Función no soportada"                     '  No longer supported function*/
            Case -606 : errmsg = "Error al craer configuración"
            Case -607 : errmsg = "Imagen muy grande"
            Case -608 : errmsg = "TS2_CALLBACK_FULL"
            Case -609 : errmsg = "Error de formato"
            Case -610 : errmsg = "No se encontraron datos"
            Case -611 : errmsg = "CAL desconocida"
            Case -612 : errmsg = "CAL abortada"
            Case -613 : errmsg = "Error en sello"
            Case -614 : errmsg = "Error en autoprueba"                      '  TS200 ERROR -- Selftest error*/
            Case -615 : errmsg = "Error genérico"
            Case -616 : errmsg = "ID no válido"
            Case -617 : errmsg = "Error en puerto"
            Case -618 : errmsg = "Error al abrir dispositivo"
            Case -619 : errmsg = "Sin conexión"
            Case -620 : errmsg = "Función invalida"                         '  Incorrect function.                                                 */
            Case -621 : errmsg = "Archivo no encontrado"                    '  The system cannot find the file specified.                          */
            Case -622 : errmsg = "Acceso denegado"                          '  Access is denied.                                                   */
            Case -623 : errmsg = "Manejador inválido"                       '  The handle is invalid.                                              */
            Case -624 : errmsg = "Sin memoria suficiente"                   '  Not enough storage is available to process this command.            */
            Case -625 : errmsg = "Access code inválido"                     '  The access code is invalid.                                         */
            Case -626 : errmsg = "Datos inválidos"                          '  The data is invalid.                                                */
            Case -627 : errmsg = "Dispositivo no encontrado"                '  The system cannot find the device specified.                        */
            Case -628 : errmsg = "Dispositivo no listo"                     '  The device is not ready.                                            */
            Case -629 : errmsg = "Comando inválido"                         '  The device does not recognize the command.                          */
            Case -630 : errmsg = "Error en longitud de comando"             '  The program issued a command but the command length is incorrect.   */
            Case -631 : errmsg = "Error de escritura"                       '  The system cannot write to the specified device.                    */
            Case -632 : errmsg = "Error de lectura"                         '  The system cannot read from the specified device.                   */
            Case -633 : errmsg = "Error genérico"                           '  A device attached to the system is not functioning.                 */
            Case -634 : errmsg = "Parámetro inválido"                       '  The parameter is incorrect.                                         */
            Case -635 : errmsg = "Error en Dll"                             '  A dynamic link library (DLL) initialization routine failed.         */
            Case -636 : errmsg = "Error de I/O"                             '  The request could not be performed because of an I/O device error.  */
            Case -637 : errmsg = "Dll inválda"                              '  One of the library files needed to run this application is damaged. */
            Case -638 : errmsg = "Dll no encontrada"                        '  One of the library files needed to run this application cannot be found.*/
            Case -639 : errmsg = "Incialización necesaria"                  ' The indicated device requires reinitialization due to hardware errors.*/
            Case -640 : errmsg = "Dispositivo no conectado"                 '  The device is not connected.                                        */
            Case -641 : errmsg = "Dispositivo ya inicializado"              '  An attempt was made to perform an initialization operation when initialization has already been completed.*/
            Case -642 : errmsg = "Dipositivo no encontrado"                 '  No more local devices.                                              */
            Case -643 : errmsg = "TS4DLL no encontrado"                     '  TS4 DLL is not present */
            Case -701 : errmsg = "Error en parámetro de endoso"
            Case -702 : errmsg = "Error en memoria de endoso"
            Case -703 : errmsg = "Error de rotación en endoso"
            Case -704 : errmsg = "Error en archivo BMP de endoso"
            Case -705 : errmsg = "Error de espejo en endoso"
            Case -706 : errmsg = "Error al convertir texto de endoso"
            Case -707 : errmsg = "Error en 'OPEN_FILE_STAT'"
            Case -708 : errmsg = "Error en 'READ_FILE_STAT'"
            Case -709 : errmsg = "Error en 'WRITE_FILE_STAT'"

            Case 62000 : errmsg = "No existen documentos"
            Case 62001 : errmsg = "Documento atorado"
            Case 62002 : errmsg = "Documento no alimentado"
            Case 62003 : errmsg = "Error en lrctura de banda"
            Case 62004 : errmsg = "Error - 'DANGEROUS'"
            Case 62005 : errmsg = "Loop sin final"
            Case 62006 : errmsg = "Error en 'Insert'"
            Case 62007 : errmsg = "Error en 'ACQTIMEOUT'"
            Case 62008 : errmsg = "Error en 'MICRTIMEOUT"
            Case 62009 : errmsg = "Error rn 'EXTRDTIMEOUT'"
            Case 62010 : errmsg = "Error en sello"
            Case 62020 : errmsg = "Error en tarjeta SCSI"
            Case 62021 : errmsg = "Error en cabeza de impresión"
            Case 62022 : errmsg = "Memoria no suficiente"
            Case 62023 : errmsg = "Memoria no suficiente"
            Case 62024 : errmsg = "Error en memoria FLASH"
            Case 62025 : errmsg = "Disco no abierto"
            Case 62026 : errmsg = "Error al crear configuración"
            Case 62027 : errmsg = "Parámetro invalido"
            Case 62028 : errmsg = "Imagen muy rande"
            Case 62029 : errmsg = "Error en archivo de font"
            Case 62030 : errmsg = "Error de formato"
            Case 62031 : errmsg = "No existen datos"
            Case 62040 : errmsg = "Error desconcido de 'CAL'"
            Case 62041 : errmsg = "'CAL' abortado"
            Case 62042 : errmsg = "Error en 'LIGHTCAL'"
            Case 62043 : errmsg = "Error en 'BLACKLEVCAL'"
            Case 62044 : errmsg = "Error en 'WHITELEVCAL'"
            Case 62100 : errmsg = "Error al solicitar estado"
            Case 62101 : errmsg = "Error en 'CLRERR_ERR'"
            Case 62102 : errmsg = "Error en archivo temporal de configuración"
            Case 62103 : errmsg = "Error en 'ACQPAR'"
            Case 62104 : errmsg = "Error en 'PMEC'"
            Case 62105 : errmsg = "Error en 'ACQPAR'"
            Case 62106 : errmsg = "Error en 'PMEC'"
            Case 62107 : errmsg = "Error en 'TMPCFG'"
            Case 62108 : errmsg = "Error en 'GETDOC'"
            Case 62109 : errmsg = "Error en 'EJECT'"
            Case 62110 : errmsg = "Error en 'SETDOCPOS'"
            Case 62111 : errmsg = "Error en 'ENDDOC'"
            Case 62113 : errmsg = "Error en 'EEREAD'"
            Case 62114 : errmsg = "Error en 'LOADPRT'"
            Case 62115 : errmsg = "Error en 'CLEARPRT'"
            Case 62116 : errmsg = "Error en 'SCSICMDTIMEOUT'"
            Case 62117 : errmsg = "Error en 'SCSICMD'"
            Case 62118 : errmsg = "Error en 'SCSIACQ'"
            Case 62119 : errmsg = "Error en 'DESTSELECT'"
            Case Else : errmsg = "Error no definido. Código: " + elerror.ToString()
        End Select
        errmsg = errmsg + " (" + elerror.ToString + ")"
        Return (errmsg)
    End Function

    Private Sub GetQueryStringParameters()
        Dim NameValueTable As New NameValueCollection()
        Dim values() As String
        If (ApplicationDeployment.IsNetworkDeployed) Then
            Dim qString As String = ApplicationDeployment.CurrentDeployment.ActivationUri.Query
            If (qString IsNot Nothing) Then
                NameValueTable = HttpUtility.ParseQueryString(qString)
                For Each key In NameValueTable.Keys
                    values = NameValueTable.GetValues(key)
                    For Each value As String In values
                        If (key = "id_recuento_contenedor") Then
                            Modulo.Id_Recuento_Contenedor = value
                        ElseIf (key = "cliente") Then
                            Modulo.Cliente = value
                        ElseIf (key = "local") Then
                            Modulo.Sucursal = value
                        Else
                            Modulo.Tipo_Recuento = value
                        End If
                    Next value
                Next key
            End If
        End If
    End Sub

    Private Function Inicializador() As Boolean
        Dim Inicializado As Boolean
        Dim Reintento As MsgBoxResult
        Modulo.Indice = 0
        Modulo.PathInicio = Path.GetDirectoryName(Application.ExecutablePath) + "\"
        Modulo.PathImagenes = Path.GetDirectoryName(PathInicio) + "\" + "Imagenes" + "\"
        If (Not Directory.Exists(PathImagenes)) Then Directory.CreateDirectory(PathImagenes)
        'PathImagenesSucursal = Path.GetDirectoryName(PathInicio) + "\" + "ImagenesS" + "\"
        ' If (Not Directory.Exists(PathImagenesSucursal)) Then Directory.CreateDirectory(PathImagenesSucursal)
        Me.Show()
        Application.DoEvents()
        Sleep(2000)
        Inicializado = False
        Do
            Res = BUICInit()
            If (Res = 1) Then
                Dim ND, NT As Integer
                Dim aa, bb, cc As Byte
                Dim dd As String = Space(255)
                Dim ee As String = Space(255)
                Dim ff As String = Space(255)
                Dim SN As String = Space(255)

                CX30 = False
                TS240 = False
                Franqueo = 0
                Inicializado = True
                ScannerType = GetScannerType()
                BUICGetScannerSerialNumber(SN, ND, NT)
                BUICGetScannerInfo(aa, bb, dd, ee, ff, cc)
                ee = ee.Substring(0, ee.IndexOf(Chr(0)))
                SN = SN.Substring(0, SN.IndexOf(Chr(0)))
                Me.Text = "DCC - AMM Demo >>>" + ee.Trim() + " (" + SN + ")"
                Modulo.ListaCheques = New List(Of Cheque)
            Else
                Reintento = MessageBox.Show("No se encontró digitalizador !, Desea reintentar ?", "Inicialización", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            End If
        Loop While (Res <> 1 And Reintento = MsgBoxResult.Yes)
        If (Not Inicializado) Then
            Reintento = MessageBox.Show("No podrá digitalizar documentos !", "Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End If
        Return Inicializado
    End Function

    Private Function IsValidMonto() As Boolean
        If (TxtMonto.Text = 0) Then
            MessageBox.Show("Se requiere Monto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            TxtMonto.Select()
            Return False
        End If
        Dim pattern As New Regex("^(?!0+\,00)(?=.{1,9}(\,|$))(?!0(?!\,))\d+(\.\d{3})*(\,\d+)?$")
        If (Not pattern.IsMatch(TxtMonto.Text)) Then
            MessageBox.Show("Monto Inválido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            TxtMonto.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub LimpiaContenedorDeImagenes()
        For Each deleteFile In Directory.GetFiles(Modulo.PathImagenes, "*.*", SearchOption.TopDirectoryOnly)
            File.Delete(deleteFile)
        Next
    End Sub

    Private Sub MostrarPrimerChequeEnLista()
        If (Modulo.ListaCheques.Count > 0) Then
            Dim fCheque As Cheque = Modulo.ListaCheques.First()
            DesbloqueaDetalle()
            SetDatosAControles()
        End If
    End Sub

    Private Sub Open_Remote_Connection(ByVal strComputer As String, ByVal strUsername As String, ByVal strPassword As String)
        '//====================================================================================
        '//using NET USE to open a connection to the remote computer
        '//with the specified credentials. if we dont do this first, File.Copy will fail
        '//====================================================================================
        Dim ProcessStartInfo As New System.Diagnostics.ProcessStartInfo
        ProcessStartInfo.FileName = "net"
        ProcessStartInfo.Arguments = "use \\" & strComputer & "\c$ /USER:" & strUsername & " " & strPassword
        ProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden
        System.Diagnostics.Process.Start(ProcessStartInfo)

        '//============================================================================
        '//wait 2 seconds to let the above command complete or the copy will still fail
        '//============================================================================
        System.Threading.Thread.Sleep(2000)
    End Sub

    'Private _lista As List(Of Cheque)
    'Private _Modulo.Indice As Int16
    'Public Property Modulo.ListaCheques As List(Of Cheque)
    '    Get
    '        Return _lista
    '    End Get
    '    Set(value As List(Of Cheque))
    '        _lista = value
    '    End Set
    'End Property
    'Public Property Modulo.Indice As Short
    '    Get
    '        Return Modulo.Indice
    '    End Get
    '    Set(value As Short)
    '        _Modulo.Indice = value
    '    End Set
    'End Property
    Private Sub Recuento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetQueryStringParameters()
        SetTootlTips()
        ReiniciaControlesDetalle()
        BloqueaDetalle()
        If (Inicializador()) Then
            LimpiaContenedorDeImagenes()
            DigiAvanzada(999)
            MostrarPrimerChequeEnLista()
            SetMaximunToProgressBar()
        End If
    End Sub

    Private Sub ReiniciaControlesDetalle()
        TxtMonto.Text = 0
        TxtTotal.Text = "0.00"
        DtFecha.Value = Date.Now
        FrontPictureBox.Image = Nothing
        BackPictureBox.Image = Nothing
        LblChcSerial.Text = Nothing
        LblChcCount.Text = "0"
    End Sub

    Private Sub Reintentar(indicesTif As StreamWriter, indicesJpg As StreamWriter, indicesBmp As StreamWriter)
        indicesTif.Close()
        indicesJpg.Close()
        indicesBmp.Close()
        DigiAvanzada(999)
        MostrarPrimerChequeEnLista()
    End Sub

    Private Sub SetDatosAControles()
        If (Modulo.Indice < Modulo.ListaCheques.Count) Then
            Dim objCheque As Cheque = Modulo.ListaCheques.ElementAt(Modulo.Indice)
            Dim posicion As String
            posicion = If(Modulo.Indice = 0, "1", (Modulo.Indice + 1).ToString())
            LblChcCount.Text = posicion & "/" & Convert.ToString(Modulo.ListaCheques.Count)
            LblChcSerial.Text = objCheque.Micr
            FrontPictureBox.Image = objCheque.ImagenABitmap
            BackPictureBox.Image = objCheque.ImagenRBitmap
            TxtMonto.Text = objCheque.Monto.ToString("#,#.00#;(#,#.00#)")
            DtFecha.Value = objCheque.Fecha
            If (Modulo.ListaCheques.Count = 1) Then
                BtnNext.Enabled = False
                BtnBack.Enabled = False
                BtnFirst.Enabled = False
                BtnLast.Enabled = False
            Else
                BtnNext.Enabled = True
                BtnBack.Enabled = True
                BtnFirst.Enabled = True
                BtnLast.Enabled = True
            End If
            If (Modulo.Indice = 0) Then
                BtnBack.Enabled = False
            Else
                BtnBack.Enabled = True
            End If
        Else
            Modulo.Indice -= 1
            SetDatosAControles()
        End If
        If (Modulo.Indice = (Modulo.ListaCheques.Count - 1)) Then
            BtnNext.Enabled = False
            If ((Not BtnBack.Enabled) And (Modulo.ListaCheques.Count > 1)) Then
                BtnBack.Enabled = True
            End If
        End If
        TxtMonto.Select()
    End Sub

    Private Sub SetMaximunToProgressBar()
        ProgressBar1.Maximum = ListaCheques.Count
    End Sub
    Private Sub SetTootlTips()
        ToolTip1.SetToolTip(BtnFirst, "Ir al primer cheque")
        ToolTip2.SetToolTip(BtnNext, "Ir al siguiente cheque")
        ToolTip3.SetToolTip(BtnBack, "Ir al cheque anterior")
        ToolTip4.SetToolTip(BtnLast, "Ir al último cheque")
        ToolTip5.SetToolTip(BtnGuardar, "Agregar(+)")
        ToolTip6.SetToolTip(BtnEliminar, "Eliminar cheque")
        ToolTip7.SetToolTip(BtnProcesar, "Procesar cheques cargados")
        ToolTip8.SetToolTip(BtnRestart, "Reiniciar proceso")
        ToolTip9.SetToolTip(BtnExpulsar, "Expulsar")
    End Sub

    Private Function SiguienteImagen(ByVal tipo As Integer) As Integer
        Dim ImageDir() As String
        Dim archivo As String
        Dim numero As Integer

        numero = 1
        archivo = ""
        Select Case tipo
            Case 1
                ImageDir = Directory.GetFiles(PathImagenes, "*.Tif")
                If (ImageDir.Length > 0) Then
                    archivo = Path.GetFileName(ImageDir(ImageDir.Length - 1))
                    numero = Convert.ToInt16(archivo.Substring(1, 5)) + 1
                End If
            Case 2
                ImageDir = Directory.GetFiles(PathImagenes, "*.Jpg")
                If (ImageDir.Length > 0) Then
                    archivo = Path.GetFileName(ImageDir(ImageDir.Length - 1))
                    numero = Convert.ToInt16(archivo.Substring(1, 5)) + 1
                End If
            Case 3
                ImageDir = Directory.GetFiles(PathImagenes, "*.Bmp")
                If (ImageDir.Length > 0) Then
                    archivo = Path.GetFileName(ImageDir(ImageDir.Length - 1))
                    numero = Convert.ToInt16(archivo.Substring(1, 5)) + 1
                End If
        End Select
        Return (numero)
    End Function

    Private Sub TxtMonto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtMonto.KeyPress
        Dim keyvalue As Int32 = Asc(e.KeyChar)
        If ((keyvalue = Modulo.BACKSPACE) Or (((keyvalue >= Modulo.ZERO) And (keyvalue <= Modulo.NINE)) Or (keyvalue = Modulo.DECIMAL_POINT) Or (keyvalue = Modulo.THOUNSAND_POINT))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If (keyvalue = Modulo.ENTER) Then
            If (IsValidMonto()) Then
                DtFecha.Select()
            End If
        End If
    End Sub

    Private Sub TxtMonto_Leave(sender As Object, e As EventArgs) Handles TxtMonto.Leave
        If (Modulo.Indice = (Modulo.ListaCheques.Count - 1)) Then
            TxtTotal.Text = (Convert.ToSingle(TxtTotal.Text) + Convert.ToSingle(TxtMonto.Text)).ToString("#,#.00#;(#,#.00#)")
        End If
    End Sub

    Private Sub UploadFile()
        Try
            Dim file_name_front = ConfigurationManager.AppSettings.Item("machine") & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("frontAKA")
            Dim file_name_back = ConfigurationManager.AppSettings.Item("machine") & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("backAKA")
            Open_Remote_Connection(ConfigurationManager.AppSettings.Item("machine"), ConfigurationManager.AppSettings.Item("user"), ConfigurationManager.AppSettings.Item("pass"))
            If Not IO.Directory.Exists(ConfigurationManager.AppSettings.Item("machine") & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\") Then
                IO.Directory.CreateDirectory(ConfigurationManager.AppSettings.Item("machine") & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\")
            End If
            For Each item As Cheque In ListaCheques
                item.ImagenABitmap.Save(file_name_front & item.NroCheque & item.CodBanco & item.CodPlza & item.CtaCorriente & ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg)
                item.ImagenABitmap.Save(file_name_back & item.NroCheque & item.CodBanco & item.CodPlza & item.CtaCorriente & ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg)
                ProgressBar1.Increment(1)
            Next
        Catch e As Exception
            MessageBox.Show("Imposible cargar las imágenes digitalizadas" & vbNewLine & e.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

#End Region

End Class