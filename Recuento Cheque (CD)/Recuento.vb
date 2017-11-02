
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Deployment.Application
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Security.Principal
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web

Imports Recuento_Cheque__CD_

Public Class Recuento

#Region "Internal Methods"

    Friend Function Registrado(micr As String) As Boolean
        micr = Regex.Replace(micr.Replace(">", String.Empty).Replace("<", String.Empty).Replace(":", String.Empty), "\s", "")
        Dim count As Int32 = Modulo.ListaCheques.Where(Function(x) x.Micr = micr).Count()
        If (count > 0) Then
            MessageBox.Show("El código CMC7 que intenta agregar ya existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            Return False
        End If
        Return True
    End Function

#End Region

#Region "Private Methods"

    Private Function ActualizaCheque() As Boolean
        Dim currentDate As Date = DateTime.Now()
        Dim resM As Boolean = IsValidMonto()
        Dim res As Boolean = DateTime.TryParse(DtFecha.Value.ToString(), currentDate)
        If ((res) And (resM)) Then
            Dim objCheque As Cheque = Modulo.ListaCheques.ElementAt(Modulo.Indice)
            If (Not objCheque.Micr.IndexOf("?") > -1) Then
                objCheque.Monto = Convert.ToInt64(TxtMonto.Text.Replace(".", Nothing))
                objCheque.Fecha = DtFecha.Value
                ActualizaTotal()
            Else
                MessageBox.Show("Código CMC7 inválido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                Micr.Show()
                Return False
            End If
        Else
            If (Not resM) Then
                Return resM
            End If
            If (Not res) Then
                MessageBox.Show("Fecha inválida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                DtFecha.Select()
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub ActualizaTotal()
        Dim sum As Long = 0
        sum = Modulo.ListaCheques.Sum(Function(item) item.Monto)
        TxtTotal.Text = sum.ToString("N0")
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
        'BtnRestart.Enabled = False
        'BtnExpulsar.Enabled = False
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

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim item As Cheque = Modulo.ListaCheques.ElementAt(Modulo.Indice)
        If ((Modulo.Tipo_Proceso = 1) And (item.FinProceso IsNot Nothing)) Then
            Dim root As String = ConfigurationManager.AppSettings.Item("machine").ToString()
            Dim file_name_front = root & "/" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("frontAKA")
            Dim file_name_back = root & "/" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("backAKA")
            Dim Db As DataAccesss = New DataAccesss()
            Dim cmc7 As String = item.NroCheque & item.CodBanco & item.CodPlza & item.CtaCorriente
            Dim tmpLista As List(Of Cheque) = New List(Of Cheque)()
            tmpLista.Add(item)
            If (IO.File.Exists(file_name_front & cmc7 & ".jpeg") And IO.File.Exists(file_name_back & cmc7 & ".jpeg")) Then
                IO.File.Delete(file_name_front & cmc7 & ".jpeg")
                IO.File.Delete(file_name_back & cmc7 & ".jpeg")
            End If
            Db.RollBack(tmpLista.ConvertToDataTable())
        End If
        Modulo.ListaCheques.RemoveAt(Modulo.Indice)
        ReiniciaControlesDetalle()
        Modulo.Indice = IIf((Modulo.Indice - 1) < 0, 0, (Modulo.Indice - 1))
        If ((Modulo.Indice = 0) And (Modulo.Indice = ListaCheques.Count)) Then
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

    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles BtnFirst.Click
        Modulo.Indice = 0
        SetDatosAControles()
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim len As Int32 = Modulo.ListaCheques.Count
        DigiNormal(999)
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
        If (Modulo.ListaCheques.Count > 0) Then
            Dim Reintento As MsgBoxResult
            Dim resp As Boolean
            Dim Db As DataAccesss
            ProgressBar1.UseWaitCursor = True
            If (Modulo.Tipo_Proceso = 0) Then
                SetFinProceso()
            End If
            If (ActualizaCheque()) Then
                BloqueaDetalle()
                BtnRestart.Enabled = False
                BtnExpulsar.Enabled = False
                ProgressBar1.Enabled = True
                If (UploadFile()) Then
                    Dim dt As DataTable = Modulo.ListaCheques.ConvertToDataTable()
                    Db = New DataAccesss()
                    If (Modulo.Tipo_Proceso = 1) Then
                        Db.RollBack(dt)
                    End If
                    resp = Db.Process(dt)
                    If (Not resp) Then
                        Reintento = MessageBox.Show("Imposible almacenar la información recabada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                        ProgressBar1.Value = 0
                    Else
                        ProgressBar1.Increment(1)
                        MessageBox.Show("Proceso realizado satisfactoriamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Application.Exit()
                    End If
                Else
                    MessageBox.Show("Imposible almacenar las imagenes digitalizadas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                End If
            End If
        Else
            MessageBox.Show("No hay documentos que procesar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    Private Sub BtnRestart_Click(sender As Object, e As EventArgs) Handles BtnRestart.Click
        Dim result As Integer = MessageBox.Show("¿Está seguro de reliazar esta acción? Perderá toda la información.", "Reinicar Proceso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        If result = DialogResult.Yes Then
            Modulo.ListaCheques.Clear()
            Modulo.DocsMin = 0
            ReiniciaControlesDetalle()
            DigiNormal(999)
            MostrarPrimerChequeEnLista()
            SetMaximunToProgressBar()
        End If
    End Sub

    Private Sub CargarDataProcesada()
        Dim Db As DataAccesss = New DataAccesss()
        Dim dt As DataTable = Db.Load()
        Dim root As String = ConfigurationManager.AppSettings.Item("machine").ToString()
        Modulo.ListaCheques = New List(Of Cheque)
        Dim file_name_front = root & "/" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("frontAKA")
        Dim file_name_back = root & "/" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("backAKA")
        Dim cmc7 As String
        If (dt.Rows.Count > 0) Then
            For Each dr In dt.Rows
                cmc7 = (dr("NroCheque").ToString() & dr("CodBanco").ToString() & dr("CodPlza").ToString() & dr("CtaCorriente").ToString())
                If (IO.File.Exists(file_name_front & cmc7 & ".jpeg") And IO.File.Exists(file_name_back & cmc7 & ".jpeg")) Then
                    Modulo.ListaCheques.Add(New Cheque With {
                                        .Fecha = dr("Fecha").ToString(),
                                        .NroCheque = dr("NroCheque").ToString(),
                                        .Monto = dr("Monto").ToString(),
                                        .CodBanco = dr("CodBanco").ToString(),
                                        .CodPlza = dr("CodPlza").ToString(),
                                        .CtaCorriente = dr("CtaCorriente").ToString(),
                                        .Id_Recuento_Contenedor = dr("Id_Recuento_Contenedor").ToString(),
                                        .Tipo_Recuento = dr("Tipo_Recuento").ToString(),
                                        .Estado = 2,
                                        .IniProceso = ToDatetime(dr("IniProceso").ToString()),
                                        .FinProceso = ToDatetime(dr("FinProceso").ToString()),
                                        .ImagenABitmap = LoadChequeImage(file_name_front & cmc7 & ".jpeg", New System.IO.MemoryStream),
                                        .ImagenRBitmap = LoadChequeImage(file_name_back & cmc7 & ".jpeg", New System.IO.MemoryStream),
                                        .Micr = cmc7
                     })

                Else
                    Modulo.ListaCheques.Add(New Cheque With {
                        .Fecha = dr("Fecha").ToString(),
                        .NroCheque = dr("NroCheque").ToString(),
                        .Monto = dr("Monto").ToString(),
                        .CodBanco = dr("CodBanco").ToString(),
                        .CodPlza = dr("CodPlza").ToString(),
                        .CtaCorriente = dr("CtaCorriente").ToString(),
                        .Id_Recuento_Contenedor = dr("Id_Recuento_Contenedor").ToString(),
                        .Tipo_Recuento = dr("Tipo_Recuento").ToString(),
                        .Estado = 2,
                        .IniProceso = dr("IniProceso").ToString(),
                         .FinProceso = dr("FinProceso").ToString(),
                         .Micr = cmc7
                        })
                End If
            Next
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
        BtnBuscar.Enabled = True
    End Sub

    Private Sub DigiNormal(ByVal DocNum As Integer)
        Dim IndicesTif As IO.StreamWriter = New StreamWriter(PathImagenes + "IndicesTif.Dat", True, Encoding.Default)
        Dim IndicesJpg As IO.StreamWriter = New StreamWriter(PathImagenes + "IndicesJpg.Dat", True, Encoding.Default)
        Dim IndicesBmp As IO.StreamWriter = New StreamWriter(PathImagenes + "IndicesBmp.Dat", True, Encoding.Default)

        Dim ImgALen As String = Space(255)
        Dim ImgRLen As String = Space(255)
        Dim MICRLen As String = Space(255)
        Dim MICROCR As String = Space(255)
        Dim ConfOCR As String = Space(255)
        Dim MICR As String = Space(255)

        Dim Ret As MsgBoxResult

        Dim ImgA As String = ""
        Dim ImgR As String = ""

        Dim DispImagenA As Bitmap
        Dim DispImagenR As Bitmap

        Dim SiguienteI As Integer

        Dim flag As Boolean = False
        Dim retry As Int16 = 0

        ' Batch
        ' =====
        Res = BUICSetParam(160, 1)

        ' Resolución
        ' ==========
        BUICSetParam(CFG_IMAGE_RESOLUTION, 0)

        ' Código de Banda
        ' ===============
        BUICSetParam(BPARAM_MAGNTYPE, 0)

        ' 8 Bits 256 Tonos
        ' ================
        Res = BUICSetParam(CFG_IMAGE_GRAY256LEVEL, 0)

        ' Calidad JPEG
        ' ============
        Res = BUICSetParam(CFG_MISC_JPEG_QUALITY, Convert.ToInt32(50))

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
        Res = BUICSetParam(119, 0)

        ' CX 30 Salida
        ' ============
        Res = BUICSetParam(191, 1)

        ' SORT
        ' ====
        Res = BUICSetParam(CFG_DEV_SORTER, 0)

        ' DCC Franqueo
        '' ============
        Res = BUICSetParam(196, Franqueo)

        If (Res >= 0) Then
            For Ciclo = 1 To DocNum
                ImgALen = Space(255)
                ImgRLen = Space(255)
                MICRLen = Space(255)
                MICROCR = Space(255)
                ConfOCR = Space(255)
                MICR = Space(255)
                SiguienteI = SiguienteImagen(2)
                ImagenActual = "A" + SiguienteI.ToString("00000") + ".Jpg"
                ImgA = PathImagenes + "A" + SiguienteI.ToString("00000") + ".Jpg"
                ImgR = PathImagenes + "R" + SiguienteI.ToString("00000") + ".Jpg"
                Res = BUICScanGray(7, ImgA, ImgALen, ImgR, ImgRLen, MICR, MICRLen, 4)
                If (Res >= 0) Then
                    MICR = MICR.Substring(0, MICR.IndexOf(Chr(0)))
                    MICR = MICR.Replace("@", "?")
                    IndicesJpg.WriteLine(MICR.PadRight(50) + ImgA + " " + ImgR)
                Else
                    ImagenActual = "A" + (SiguienteI - 1).ToString("00000") + ".JPG"
                End If

                If (Res >= 0) Then

                    Dim FileA As FileStream = New FileStream(ImgA, FileMode.Open)
                    Dim FileR As FileStream = New FileStream(ImgR, FileMode.Open)

                    If (Registrado(MICR)) Then
                        DocsMin = DocsMin + 1

                        DispImagenA = New Bitmap(Image.FromStream(FileA))
                        DispImagenR = New Bitmap(Image.FromStream(FileR))


                        Modulo.ListaCheques.Add(New Cheque(MICR, DispImagenA, DispImagenR, Convert.ToInt32(Modulo.Id_Recuento_Contenedor), Modulo.Tipo_Recuento))


                        FrontPictureBox.Image = DispImagenA
                        BackPictureBox.Image = DispImagenR

                        LblChcCount.Text = DocsMin
                    End If
                    FileA.Close()
                    FileR.Close()
                    flag = True
                Else
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

            If (retry = 1) Then
                Reintentar(IndicesTif, IndicesJpg, IndicesBmp)

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

    Private Sub DtFecha_Enter(sender As Object, e As EventArgs) Handles DtFecha.Enter
        ActualizaCheque()
        ActualizaTotal()
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
                        ElseIf (key = "tipo_recuento") Then
                            Modulo.Tipo_Recuento = value
                        Else
                            Modulo.Id_Recuento = value
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
        If ((TxtMonto.Text = Nothing) Or (TxtMonto.Text = 0)) Then
            MessageBox.Show("Se requiere Monto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            TxtMonto.Select()
            Return False
        End If
        Dim pattern As New Regex("^(?:0|[1-9][0-9]{0,2}(?:\.[0-9]{3})*)$") '(\,\d+)? 
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

    Private Function LoadChequeImage(path As String, ms As System.IO.MemoryStream) As Bitmap
        Dim myJpgImage As Image = Image.FromFile(path)
        myJpgImage.Save(ms, ImageFormat.Bmp)
        myJpgImage.Dispose()
        Return Image.FromStream(ms)
    End Function

    Private Sub MostrarPrimerChequeEnLista()
        If (Modulo.ListaCheques.Count > 0) Then
            Dim fCheque As Cheque = Modulo.ListaCheques.First()
            DesbloqueaDetalle()
            SetDatosAControles()
        End If
    End Sub

    Private Sub OpenImagesDirectory()
        Dim nr As New NETRESOURCE
        Dim root As String = ConfigurationManager.AppSettings.Item("machine").ToString()
        nr.dwType = Modulo.RESOURCETYPE_DISK
        nr.lpRemoteName = root
        If WNetAddConnection2(nr, ConfigurationManager.AppSettings.Item("pass").ToString(), ConfigurationManager.AppSettings.Item("user").ToString(), 0) <> NO_ERROR Then
            MessageBox.Show("WNetAddConnection2 failed.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Application.Exit()
        End If
    End Sub

    Private Sub Recuento_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim count As Int32 = Modulo.ListaCheques.Where(Function(x) x.Estado = 0).Count()
        Dim reintento As MsgBoxResult
        If (count > 0) Then
            reintento = MessageBox.Show("Existen datos sin ser procesador, al cerrar la aplicación estos datos se perderán ¿Está usted de acuerdo?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If (reintento = MsgBoxResult.Yes) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub Recuento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetQueryStringParameters()
        OpenImagesDirectory()
        SetTootlTips()
        ReiniciaControlesDetalle()
        BloqueaDetalle()
        CargarDataProcesada()
        If (Inicializador()) Then
            If (Modulo.ListaCheques.Count = 0) Then
                LimpiaContenedorDeImagenes()
                DigiNormal(999)
            Else
                Modulo.Tipo_Proceso = 1
                ActualizaTotal()
            End If
        End If
        MostrarPrimerChequeEnLista()
        SetMaximunToProgressBar()
    End Sub
    Private Sub ReiniciaControlesDetalle()
        TxtMonto.Text = 0
        TxtTotal.Text = "0"
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
        DigiNormal(999)
        MostrarPrimerChequeEnLista()
    End Sub

    Private Sub SaveImage(imagenRBitmap As Bitmap, v As String)
        Using memory As MemoryStream = New MemoryStream
            Using fs As FileStream = New FileStream(v, FileMode.Create, FileAccess.ReadWrite)
                imagenRBitmap.Save(memory, ImageFormat.Jpeg)
                Dim bytes As Byte() = memory.ToArray()
                fs.Write(bytes, 0, bytes.Length)
            End Using
        End Using
    End Sub

    Private Sub SetDatosAControles()
        If (Modulo.Indice < Modulo.ListaCheques.Count) Then
            Dim objCheque As Cheque = Modulo.ListaCheques.ElementAt(Modulo.Indice)
            Dim posicion As String
            posicion = If(Modulo.Indice = 0, "1", (Modulo.Indice + 1).ToString())
            LblChcCount.Text = posicion & "/" & Convert.ToString(Modulo.ListaCheques.Count)
            LblChcSerial.Text = (objCheque.NroCheque & objCheque.CodBanco & objCheque.CodPlza & objCheque.CtaCorriente)
            FrontPictureBox.Image = objCheque.ImagenABitmap
            BackPictureBox.Image = objCheque.ImagenRBitmap
            TxtMonto.Text = objCheque.Monto.ToString("N0") '.00#
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

    Private Sub SetFinProceso()
        For Each item In Modulo.ListaCheques
            item.FinProceso = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Next
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

    Private Function ToDatetime(ByVal datestamp As String) As String
        Dim parse As DateTime = DateTime.Parse(datestamp)
        Return parse.ToString("yyyy-MM-dd HH:mm:ss")
    End Function
    Private Sub TxtMonto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtMonto.KeyPress
        Dim keyvalue As Int32 = Asc(e.KeyChar)
        If ((keyvalue = Modulo.BACKSPACE) Or (((keyvalue >= Modulo.ZERO) And (keyvalue <= Modulo.NINE)) Or (keyvalue = Modulo.DECIMAL_POINT) Or (keyvalue = Modulo.THOUNSAND_POINT))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        If (keyvalue = Modulo.ENTER) Then
            TxtMonto.Text = Int32.Parse(TxtMonto.Text.Replace(".", Nothing)).ToString("N0")
            If (IsValidMonto()) Then
                DtFecha.Select()
            End If
        End If
    End Sub
    Private Sub TxtMonto_Leave(sender As Object, e As EventArgs) Handles TxtMonto.Leave
        TxtMonto.Text = Int32.Parse(TxtMonto.Text.Replace(".", Nothing)).ToString("N0")
    End Sub

    Private Function UploadFile() As Boolean
        Try
            Dim root As String = ConfigurationManager.AppSettings.Item("machine").ToString()
            Dim file_name_front = root & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("frontAKA")
            Dim file_name_back = root & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\" & ConfigurationManager.AppSettings.Item("backAKA")
            If Not IO.Directory.Exists(root & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\") Then
                IO.Directory.CreateDirectory(root & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\")
            Else
                IO.Directory.Delete(root & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\", True)
                IO.Directory.CreateDirectory(root & "\" & Modulo.Cliente & "\" & Date.Now().Date.ToString("dd-MM-yyyy") & "\" & Modulo.Sucursal & "\")
            End If
            For Each item As Cheque In ListaCheques
                SaveImage(item.ImagenABitmap, file_name_front & item.NroCheque & item.CodBanco & item.CodPlza & item.CtaCorriente & ".jpeg")
                SaveImage(item.ImagenRBitmap, file_name_back & item.NroCheque & item.CodBanco & item.CodPlza & item.CtaCorriente & ".jpeg")
                ProgressBar1.Increment(1)
                item.Estado = IIf((item.Estado = 2), 2, 1)
            Next
            'If WNetCancelConnection2(root, 0, True) <> NO_ERROR Then
            '    Return False
            'End If
            Return True
        Catch e As Exception
            Return False
        End Try
    End Function

#End Region

End Class