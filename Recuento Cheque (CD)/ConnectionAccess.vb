Imports System.Configuration

Public Class ConnectionAccess

    ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("ConString").ConnectionString
        End Get
    End Property
End Class
