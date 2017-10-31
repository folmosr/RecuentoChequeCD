Imports System.ComponentModel
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)>
Public Structure NETRESOURCE
    Public dwScope As UInteger
    Public dwType As UInteger
    Public dwDisplayType As UInteger
    Public dwUsage As UInteger
    <MarshalAs(UnmanagedType.LPTStr)>
    Public lpLocalName As String
    <MarshalAs(UnmanagedType.LPTStr)>
    Public lpRemoteName As String
    <MarshalAs(UnmanagedType.LPTStr)>
    Public lpComment As String
    <MarshalAs(UnmanagedType.LPTStr)>
    Public lpProvider As String
End Structure

Module Modulo
    Public Const NO_ERROR As UInteger = 0
    Public Const RESOURCETYPE_DISK As UInteger = 1
    Public AGC As Integer
    Public BACKSPACE As Int16 = 8
    Public BatchSort As Double
    Public Ciclo As Integer
    Public Cliente As String
    Public CX30 As Boolean
    Public DCCEndosoA As Boolean
    Public DCCEndosoF As String
    Public DCCEndosoO As String
    Public DCCEndosoR As Boolean
    Public DCCendosoS As String
    Public DCCendosoT As String
    Public DCCEndosoX As String
    Public DCCEndosoY As String
    Public DECIMAL_POINT As Int16 = 44
    Public DocsMin As Int32
    Public Endoso_Fnt As String
    Public Endoso_Hgh As String
    Public Endoso_Pos As String
    Public Endoso_Sec As String
    Public EndosoBmpI As Boolean
    Public EndosoFont As Boolean
    Public EndosoReal As Boolean
    Public EndosoTbis As String
    Public EndosoText As String
    Public ENTER As Int16 = 13
    Public Franqueo As Integer
    Public Id_Recuento As String
    Public Id_Recuento_Contenedor As String
    Public ImagenActual As String
    Public Indice As Int16
    Public IQA1 As Boolean
    Public IQA2 As Boolean
    Public IQA3 As Boolean
    Public IQA4 As Boolean
    Public IQA5 As Boolean
    Public IQA6 As Boolean
    Public IQA7 As Boolean
    Public IQA8 As Boolean
    Public ListaCheques As List(Of Cheque)
    Public Mim As Boolean
    Public MM1 As String
    Public MM2 As String
    Public MM3 As String
    Public MM4 As String
    Public MM5 As String
    Public MM6 As String
    Public MM7 As String
    Public MM8 As String
    Public MM9 As String
    Public NINE As Int16 = 57
    Public NOT_FOUND As Int16 = -1
    Public NSort As Boolean
    Public PathImagenes As String
    Public PathImagenesSucursal As String
    Public PathInicio As String
    Public Res As Integer
    Public ScannerType As Integer
    Public Sort1 As Boolean
    Public Sort2 As Boolean
    Public Sort3 As Boolean
    Public SortEnabled As Boolean
    Public SSort As Boolean
    Public Sucursal As String
    Public THOUNSAND_POINT As Int16 = 46
    Public timertics As Int32
    ''' <summary>
    ''' Indica que tipo de proceso se lleva a cabo
    ''' 0 es un proceso de creacion 
    ''' 1 proceso de edicion
    ''' </summary>
    Public Tipo_Proceso As Int16 = 0

    Public Tipo_Recuento As String
    Public TS240 As Boolean
    Public ZERO As Int16 = 48
    <System.Runtime.CompilerServices.Extension>
    Public Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim td As New DataTable
        Dim entityType As Type = GetType(T)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each prop As PropertyDescriptor In properties
            td.Columns.Add(prop.Name)
        Next

        For Each item As T In list
            Dim row As DataRow = td.NewRow()

            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = prop.GetValue(item)
            Next

            td.Rows.Add(row)
        Next

        Return td
    End Function

    <DllImport("mpr.dll", CharSet:=CharSet.Auto)>
    Public Function WNetAddConnection2(ByRef lpNetResource As NETRESOURCE, <[In](), MarshalAs(UnmanagedType.LPTStr)> ByVal lpPassword As String, <[In](), MarshalAs(UnmanagedType.LPTStr)> ByVal lpUserName As String, ByVal dwFlags As UInteger) As UInteger
    End Function

    <DllImport("mpr.dll", CharSet:=CharSet.Auto)>
    Public Function WNetCancelConnection2(<[In](), MarshalAs(UnmanagedType.LPTStr)> ByVal lpName As String, ByVal dwFlags As UInteger, <MarshalAs(UnmanagedType.Bool)> ByVal fForce As Boolean) As UInteger
    End Function

End Module