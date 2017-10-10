Imports System.Data.SqlClient

Public Class DataAccesss
    Inherits ConnectionAccess

#Region "Methods"

    Public Function Process(ByVal Dt As DataTable) As Boolean
        Using connection As SqlConnection =
           New SqlConnection(ConnectionString())
            connection.Open()

            Using bulkCopy As SqlBulkCopy =
              New SqlBulkCopy(connection)
                bulkCopy.ColumnMappings.Add("Monto", "Monto")
                bulkCopy.ColumnMappings.Add("Fecha", "Fecha")
                bulkCopy.ColumnMappings.Add("NroCheque", "NroCheque")
                bulkCopy.ColumnMappings.Add("CodBanco", "CodBanco")
                bulkCopy.ColumnMappings.Add("CodPlza", "CodPlza")
                bulkCopy.ColumnMappings.Add("CtaCorriente", "CtaCorriente")
                bulkCopy.ColumnMappings.Add("Id_Recuento_Contenedor", "Id_Recuento_Contenedor")
                bulkCopy.ColumnMappings.Add("Tipo_Recuento", "Tipo_Recuento")
                bulkCopy.DestinationTableName = "dbo.Detalle_Documentos"
                Try
                    ' Write from the source to the destination.
                    bulkCopy.WriteToServer(Dt)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Excepción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Return False
                End Try
            End Using
        End Using
        Return True
    End Function

#End Region 'Methods

End Class