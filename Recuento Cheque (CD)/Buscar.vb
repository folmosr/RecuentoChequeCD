﻿Imports Recuento_Cheque__CD_

Public Class Buscar

#Region "Methods"

    Private Sub BtnCMC_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        If (IsValidMICR()) Then
            Dim objCheque As Cheque = Modulo.ListaCheques.Where(Function(x) x.NroCheque = TxtNCheque.Text AndAlso x.CodBanco = TxtCodBco.Text AndAlso x.CodPlza = TxtCodPlza.Text AndAlso x.CtaCorriente = TxtCtaCorriente.Text).FirstOrDefault()
            Dim Index = Modulo.ListaCheques.FindIndex(Function(x) x.NroCheque = TxtNCheque.Text AndAlso x.CodBanco = TxtCodBco.Text AndAlso x.CodPlza = TxtCodPlza.Text AndAlso x.CtaCorriente = TxtCtaCorriente.Text)
            If (objCheque IsNot Nothing) Then
                Recuento.LblChcSerial.Text = objCheque.Micr
                Recuento.TxtMonto.Text = objCheque.Monto
                Recuento.DtFecha.Value = objCheque.Fecha
                Recuento.FrontPictureBox.Image = objCheque.ImagenABitmap
                Recuento.BackPictureBox.Image = objCheque.ImagenRBitmap
                Recuento.LblChcCount.Text = (Index + 1).ToString() & "/" & Modulo.ListaCheques.Count.ToString()
                Me.Close()
            Else
                MessageBox.Show("Cheque no existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            End If
        End If
    End Sub

    Private Function IsValidMICR() As Boolean
        If (TxtNCheque.Text Is Nothing) Then
            MessageBox.Show("Número Cheque requerido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Return False
        End If
        If (TxtCodBco.Text Is Nothing) Then
            MessageBox.Show("Código de Banco requerido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Return False
        End If
        If (TxtCodPlza.Text Is Nothing) Then
            MessageBox.Show("Código de Plaza requerido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Return False
        End If
        If (TxtCodBco.Text Is Nothing) Then
            MessageBox.Show("Cuenta Corriente requerida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Return False
        End If
        Return True
    End Function

    Private Sub TxtCodBco_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCodBco.KeyPress
        Dim keyvalue As Int32 = Asc(e.KeyChar)
        If ((keyvalue = Modulo.BACKSPACE) Or (((keyvalue >= Modulo.ZERO) And (keyvalue <= Modulo.NINE)) Or (keyvalue = Modulo.DECIMAL_POINT) Or (keyvalue = Modulo.THOUNSAND_POINT))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TxtCodPlza_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCodPlza.KeyPress
        Dim keyvalue As Int32 = Asc(e.KeyChar)
        If ((keyvalue = Modulo.BACKSPACE) Or (((keyvalue >= Modulo.ZERO) And (keyvalue <= Modulo.NINE)) Or (keyvalue = Modulo.DECIMAL_POINT) Or (keyvalue = Modulo.THOUNSAND_POINT))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TxtCtaCorriente_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCtaCorriente.KeyPress
        Dim keyvalue As Int32 = Asc(e.KeyChar)
        If ((keyvalue = Modulo.BACKSPACE) Or (((keyvalue >= Modulo.ZERO) And (keyvalue <= Modulo.NINE)) Or (keyvalue = Modulo.DECIMAL_POINT) Or (keyvalue = Modulo.THOUNSAND_POINT))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TxtNCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNCheque.KeyPress
        Dim keyvalue As Int32 = Asc(e.KeyChar)
        If ((keyvalue = Modulo.BACKSPACE) Or (((keyvalue >= Modulo.ZERO) And (keyvalue <= Modulo.NINE)) Or (keyvalue = Modulo.DECIMAL_POINT) Or (keyvalue = Modulo.THOUNSAND_POINT))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TxtNCheque_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtNCheque.KeyUp
        If (TxtNCheque.TextLength = TxtNCheque.MaxLength) Then
            TxtCodBco.Select()
        End If
    End Sub

    Private Sub TxtCodBco_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtCodBco.KeyUp
        If (TxtCodBco.TextLength = TxtCodBco.MaxLength) Then
            TxtCodPlza.Select()
        End If
    End Sub

    Private Sub TxtCodPlza_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtCodPlza.KeyUp
        If (TxtCodPlza.TextLength = TxtCodPlza.MaxLength) Then
            TxtCtaCorriente.Select()
        End If
    End Sub

    Private Sub TxtCtaCorriente_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtCtaCorriente.KeyUp
        If (TxtCtaCorriente.TextLength = TxtCtaCorriente.MaxLength) Then
            BtnBuscar.Select()
        End If
    End Sub

#End Region 'Methods

End Class