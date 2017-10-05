Imports System.Data.SqlClient

Public Class DataAccesss : Inherits ConnectionAccess

    Public Function Process(ByVal Dt As DataTable) As Boolean
        Using connection As SqlConnection =
           New SqlConnection(ConnectionString())
            connection.Open()

            Using bulkCopy As SqlBulkCopy =
              New SqlBulkCopy(connection)
                bulkCopy.ColumnMappings.Add(3, 1)
                bulkCopy.ColumnMappings.Add(4, 2)
                bulkCopy.ColumnMappings.Add(7, 3)
                bulkCopy.ColumnMappings.Add(8, 4)
                bulkCopy.ColumnMappings.Add(9, 5)
                bulkCopy.ColumnMappings.Add(10, 6)
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

End Class