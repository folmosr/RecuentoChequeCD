﻿Imports System.Data.SqlClient

Public Class DataAccesss
    Inherits ConnectionAccess

#Region "Public Methods"

    Public Function Load() As DataTable
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dt As DataTable = New DataTable()
        Try

            con.ConnectionString = ConnectionString()
            con.Open()
            cmd.Connection = con

            cmd.CommandText = "sp_CargaDocumentos"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Id_Recuento", Modulo.Id_Recuento)
            cmd.Parameters.AddWithValue("@Id_Recuento_Contenedor", Modulo.Id_Recuento_Contenedor)
            cmd.Parameters.AddWithValue("@Tipo_Recuento", Modulo.Tipo_Recuento)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                dt.Load(reader)
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Ocurrieron errores durante el proceso: " & vbNewLine & ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Finally
            con.Close()
        End Try
        Return dt
    End Function
    Public Function Process(ByVal Dt As DataTable) As Boolean
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim cad As DataTable
        Try

            con.ConnectionString = ConnectionString()
            con.Open()
            cmd.Connection = con

            cmd.CommandText = "sp_ProcesaDetalleDocumentos"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@LoadTable", CleanDataTable(Dt))
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                cad = New DataTable
                cad.Load(reader)
                ProcessCAD(cad)
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Ocurrieron errores durante el proceso: " & vbNewLine & ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Return False
        Finally
            con.Close()
        End Try
        Return True
    End Function

#End Region

#Region "Internal Methods"

    Friend Sub RollBack(ByVal dt As DataTable, Optional ByVal instancia As Int16 = 1, Optional ByVal salida As Boolean = False)
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim paramDt As New DataTable
        Try
            paramDt = dt.Copy()
            CleanDataTable(paramDt)
            con.ConnectionString = IIf((instancia = 1), ConnectionStringForCAD(), ConnectionString())
            con.Open()
            cmd.Connection = con

            cmd.CommandText = "sp_LimpiaDetalleDocumentos"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@LoadTable", paramDt)
            cmd.ExecuteNonQuery()
            If (salida) Then
                Exit Sub
            End If
            RollBack(dt, 0, True)
        Catch ex As Exception
            Throw
        Finally
            con.Close()
        End Try
    End Sub

#End Region

#Region "Private Methods"

    Private Function CleanDataTable(dt As DataTable) As DataTable
        Dim columns As String = "Micr,ImagenRBitmap,ImagenABitmap,ImagenR,ImagenA,Estado"
        Dim ColumnsArr() As String = columns.Split(",")
        For Each col As String In ColumnsArr
            dt.Columns.Remove(col)
        Next
        Return dt
    End Function

    Private Sub ProcessCAD(cad As DataTable)
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Try

            con.ConnectionString = ConnectionStringForCAD()
            con.Open()
            cmd.Connection = con

            cmd.CommandText = "sp_ProcesaDetalleDocumentos"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@LoadTable", cad)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw
        Finally
            con.Close()
        End Try
    End Sub

#End Region

End Class