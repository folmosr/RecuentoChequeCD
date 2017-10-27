Imports System.Configuration

Public Class ConnectionAccess

#Region "Properties"

    ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("ConString").ConnectionString
        End Get
    End Property

    ReadOnly Property ConnectionStringForCAD() As String
        Get
            Return ConfigurationManager.ConnectionStrings("CADConString").ConnectionString
        End Get
    End Property
#End Region 'Properties

End Class