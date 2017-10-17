Imports System.Data.SqlClient

Public Class DataAccesss
    Inherits ConnectionAccess

#Region "Methods"

    Public Function Process(ByVal Dt As DataTable) As Boolean


        Using bulkCopy As SqlBulkCopy =
              New SqlBulkCopy(ConnectionString(), SqlBulkCopyOptions.FireTriggers)
            bulkCopy.ColumnMappings.Add("Monto", "Monto")
            bulkCopy.ColumnMappings.Add("Fecha", "Fecha")
            bulkCopy.ColumnMappings.Add("NroCheque", "NroCheque")
            bulkCopy.ColumnMappings.Add("CodBanco", "CodBanco")
            bulkCopy.ColumnMappings.Add("CodPlza", "CodPlza")
            bulkCopy.ColumnMappings.Add("CtaCorriente", "CtaCorriente")
            bulkCopy.ColumnMappings.Add("Id_Recuento_Contenedor", "Id_Recuento_Contenedor")
            bulkCopy.ColumnMappings.Add("Tipo_Recuento", "Tipo_Recuento")
            bulkCopy.ColumnMappings.Add("IniProceso", "IniProceso")
            bulkCopy.ColumnMappings.Add("FinProceso", "FinProceso")
            bulkCopy.DestinationTableName = "dbo.Detalle_Documentos"
            Try
                ' Write from the source to the destination.
                bulkCopy.WriteToServer(Dt)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                Return False
            End Try
        End Using

        Return True
    End Function
    Public Function RollBack() As Boolean
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Try
            con.ConnectionString = ConnectionString()
            con.Open()
            cmd.Connection = con

            cmd.CommandText = "DELETE Detalle FROM Detalle_Documentos AS Detalle INNER JOIN Recuento_Contenedor AS Recuento ON (Recuento.Id_Recuento_Contenedor = Detalle.Id_Recuento_Contenedor AND Recuento.Id_Recuento_Contenedor = @NroContenedor AND Recuento.Id_Recuento = @NroRecuento)"
            cmd.Parameters.Add(New SqlParameter("@NroContenedor", Modulo.Id_Recuento_Contenedor))
            cmd.Parameters.Add(New SqlParameter("@NroRecuento", Modulo.Id_Recuento))
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Se ha producido error(es) al tratar de eliminar registros " & vbNewLine & ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            Return False
        Finally
            con.Close()
        End Try
        Return True
    End Function
#End Region 'Methods

End Class