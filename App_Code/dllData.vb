Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.SqlClient
'Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Web.UI.WebControls

Public Class dllData

    Dim cPath As String
    Dim cOriginData As Double = 0

    Dim cSqlDataSource As String = "localhost"
    Dim cSqlDatabase As String
    Dim cSqlUserID As String = "root"
    Dim cSqlPassword As String

    Dim cDataConnectionOle As OleDbConnection
    Dim cDataCommandOle As OleDbCommand
    Dim cDataConnectionODBC As OdbcConnection
    Dim cDataCommandODBC As OdbcCommand
    Dim cDataConnectionSQL As SqlConnection
    Dim cDataCommandSQL As SqlCommand
    'Dim cDataConnectionMySQL As MySqlConnection
    'Dim cDataCommandMySQL As MySqlCommand

    Dim cDataOleDB As OleDbDataReader
    Dim cDataODBC As OdbcDataReader
    Dim cDataSQL As SqlDataReader
    'Dim cDataMySQL As MySqlDataReader

    Dim cDataTransOle As OleDbTransaction
    Dim cDataTransODBC As OdbcTransaction
    Dim cDataTransSQL As SqlTransaction
    'Dim cDataTransMySQL As MySqlTransaction

    Dim cDataDataAdapter As Object

    Dim cRowsAffected As Long
    Dim _Return_Val As Object

    Dim cErrors As String

    Public ReadOnly Property Errors() As String
        Get
            Errors = cErrors
            'ClearErrors()
        End Get
    End Property

    Public Sub ErrorsClear()
        cErrors = ""
    End Sub

    Public Property Path() As String
        Get
            Path = cPath
        End Get
        Set(ByVal Value As String)
            cPath = Value
        End Set
    End Property

    Public Property Return_Val() As String
        Get
            Return_Val = _Return_Val
        End Get
        Set(ByVal Value As String)
            _Return_Val = Value
        End Set
    End Property

    Public Property SQLDataSource() As String
        Get
            SQLDataSource = cSqlDataSource
        End Get
        Set(ByVal Value As String)
            cSqlDataSource = Value
        End Set
    End Property

    Public Property SQLDatabase() As String
        Get
            SQLDatabase = cSqlDatabase
        End Get
        Set(ByVal Value As String)
            cSqlDatabase = Value
        End Set
    End Property
    Public Property SQLUserID() As String
        Get
            SQLUserID = cSqlUserID
        End Get
        Set(ByVal Value As String)
            cSqlUserID = Value
        End Set
    End Property
    Public Property SQLPassword() As String
        Get
            SQLPassword = cSqlPassword
        End Get
        Set(ByVal Value As String)
            cSqlPassword = Value
        End Set
    End Property

    Public Property OriginData() As Double
        Get
            OriginData = cOriginData
        End Get
        Set(ByVal Value As Double)
            cOriginData = Value
            'cOriginData = 0 ' OleDb
            'cOriginData = 1 ' ODBC
            'cOriginData = 2 ' SQLServer
            'cOriginData = 3 ' MySQL
        End Set
    End Property

    Public ReadOnly Property RowsAffected() As Long
        Get
            RowsAffected = cRowsAffected
        End Get
    End Property

    Public Function Connect() As Boolean
        Dim strConn As String
        Try
            Select Case cOriginData
                Case 0, 0.10000000000000001
                    If cOriginData = 0 Then
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cPath & ";"
                    ElseIf cOriginData = 0.10000000000000001 Then
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & cPath & ";"
                    Else
                        Throw New Exception("Provider not accepted.")
                    End If
                    cDataConnectionOle = New OleDbConnection(strConn)
                    cDataConnectionOle.Open()
                    cOriginData = 0
                Case 1, 1.1000000000000001
                    If cOriginData = 1 Then
                        strConn = "DSN=" & cPath
                    ElseIf cOriginData = 1.1000000000000001 Then
                        strConn = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" & cPath & ";"
                    Else
                        Throw New Exception("Provider not accepted.")
                    End If
                    cDataConnectionODBC = New OdbcConnection(strConn)
                    cDataConnectionODBC.Open()
                    cOriginData = 1
                Case 2
                    strConn = cPath
                    cDataConnectionSQL = New SqlConnection(strConn)
                    cDataConnectionSQL.Open()
                Case 3
                    ''strConn = "Persist Security Info=False;Data Source=" & cSqlDataSource
                    ''strConn += ";Database=" & cSqlDatabase
                    ''strConn += ";User ID=" & cSqlUserID & ";Password=" & cSqlPassword
                    'strConn = cPath
                    'cDataConnectionMySQL = New MySqlConnection(strConn)
                    'cDataConnectionMySQL.Open()
            End Select
        Catch e As Exception
            cErrors += " " & e.Message
            Exit Function
        End Try
        Return True
    End Function

    Public Function State() As Boolean
        Try
            Select Case cOriginData
                Case 0
                    If cDataConnectionOle.State = ConnectionState.Open Then Return True
                Case 1
                    If cDataConnectionODBC.State = ConnectionState.Open Then Return True
                Case 2
                    If cDataConnectionSQL.State = ConnectionState.Open Then Return True
                Case 3
                    'If cDataConnectionMySQL.State = ConnectionState.Open Then Return True
            End Select
        Catch e As Exception
            cErrors += " " & e.Message
            Exit Function
        End Try
        Return True
    End Function

    Public Function Refresh() As Boolean
        Select Case cOriginData
            Case 0
                If cDataConnectionOle.State = ConnectionState.Open Then
                    If Disconnect() Then Return Connect()
                End If
            Case 1
                If cDataConnectionODBC.State = ConnectionState.Open Then
                    If Disconnect() Then Return Connect()
                End If
            Case 2
                If cDataConnectionSQL.State = ConnectionState.Open Then
                    If Disconnect() Then Return Connect()
                End If
            Case 3
                'If cDataConnectionMySQL.State = ConnectionState.Open Then
                ' If Disconnect() Then Return Connect()
                'End If
        End Select
    End Function

    Public Function Disconnect() As Boolean
        Select Case cOriginData
            Case 0
                If Not cDataConnectionOle Is Nothing Then cDataConnectionOle.Close()
            Case 1
                If Not cDataConnectionODBC Is Nothing Then cDataConnectionODBC.Close()
            Case 2
                If Not cDataConnectionSQL Is Nothing Then cDataConnectionSQL.Close()
            Case 3
                'cDataConnectionMySQL.Close()
        End Select
        Return True
    End Function

    Public Function ReadReturn() As Object
        Try
            If cOriginData = 0 Then
                Return cDataOleDB
            ElseIf cOriginData = 1 Then
                Return cDataODBC
            ElseIf cOriginData = 2 Then
                Return cDataSQL
            ElseIf cOriginData = 3 Then
                'Return cDataMySQL
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function

    Public Function ReadNext() As Boolean
        Try
            If cOriginData = 0 Then
                Return cDataOleDB.Read
            ElseIf cOriginData = 1 Then
                Return cDataODBC.Read
            ElseIf cOriginData = 2 Then
                Return cDataSQL.Read
            ElseIf cOriginData = 3 Then
                'Return cDataMySQL.Read
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function

    Public Function ReadClose() As Boolean
        Try
            If cOriginData = 0 Then
                cDataOleDB.Close()
            ElseIf cOriginData = 1 Then
                cDataODBC.Close()
            ElseIf cOriginData = 2 Then
                cDataSQL.Close()
            ElseIf cOriginData = 3 Then
                'cDataMySQL.Close()
            End If
            Return True
        Catch ex As Exception
            cErrors += " " & ex.Message
            Return False
        End Try
    End Function

    Public Function ReadHaveData() As Boolean
        Try
            If cOriginData = 0 Then
                Return cDataOleDB.HasRows()
            ElseIf cOriginData = 1 Then
                Return cDataODBC.HasRows()
            ElseIf cOriginData = 2 Then
                Return cDataSQL.HasRows()
            ElseIf cOriginData = 3 Then
                'Return cDataMySQL.HasRows
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
            Return False
        End Try
    End Function
    Public Function Read(ByVal Value As String) As Object
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value)
                Else
                    Return ""
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value)
                Else
                    Return ""
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value)
                Else
                    Return ""
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return ""
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function Read(ByVal Value As Integer) As Object
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value).name
                Else
                    Return ""
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value).name
                Else
                    Return ""
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value).name
                Else
                    Return ""
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return ""
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadDate(ByVal Value As String) As Date
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value)
                Else
                    Return "1/1/1"
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value)
                Else
                    Return "1/1/1"
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value)
                Else
                    Return "1/1/1"
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return "1/1/1"
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadString(ByVal Value As String) As String
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return 0
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadLong(ByVal Value As String) As Long
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return 0
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadBoolean(ByVal Value As String) As Boolean
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value)
                Else
                    Return False
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value)
                Else
                    Return False
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value)
                Else
                    Return False
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return 0
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadDouble(ByVal Value As String) As Double
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return 0
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadInteger(ByVal Value As String) As Integer
        Try
            If cOriginData = 0 Then
                If Not cDataOleDB(Value) Is DBNull.Value Then
                    Return cDataOleDB(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 1 Then
                If Not cDataODBC(Value) Is DBNull.Value Then
                    Return cDataODBC(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 2 Then
                If Not cDataSQL(Value) Is DBNull.Value Then
                    Return cDataSQL(Value)
                Else
                    Return 0
                End If
            ElseIf cOriginData = 3 Then
                'If Not cDataMySQL(Value) Is DBNull.Value Then
                ' Return cDataMySQL(Value)
                'Else
                ' Return 0
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadSP(ByVal value As String) As Object
        Try
            Return cDataSQL(value)
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function

    Private Sub ReadDataStoreProcPrepareAux(ByVal SP As String)
        cDataCommandSQL = New SqlCommand(SP, cDataConnectionSQL)
        cDataCommandSQL.CommandType = CommandType.StoredProcedure
        cDataDataAdapter = New SqlDataAdapter(cDataCommandSQL)
    End Sub
    Public Function ReadDataStoreProcPrepare(ByVal SP As String, ByVal Parameters() As String, ByVal ParametersValue() As String) As Boolean
        Try
            ReadDataStoreProcPrepareAux(SP)
            For i As Integer = 0 To Parameters.GetUpperBound(0)
                cDataCommandSQL.Parameters.Add(New SqlParameter(Parameters(i), SqlDbType.NVarChar))
            Next
            For i As Integer = 0 To Parameters.GetUpperBound(0)
                cDataDataAdapter.SelectCommand.Parameters(Parameters(i)).Value = ParametersValue(i)
            Next
            'cDataSQL = cDataCommandSQL.ExecuteReader()
            ReadDataStoreProcPrepare = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcPrepare = False
        End Try
    End Function
    Public Function ReadDataStoreProcPrepareExecute() As Boolean
        Try
            cDataSQL = cDataCommandSQL.ExecuteReader()
            ReadDataStoreProcPrepareExecute = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcPrepareExecute = False
        End Try
    End Function
    Public Function ReadDataStoreProcPrepare(ByVal SP As String) As Boolean
        Try
            ReadDataStoreProcPrepareAux(SP)
            ReadDataStoreProcPrepare = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcPrepare = False
        End Try
    End Function
    Public Function ReadDataStoreProcPrepare(ByVal SP As String, ByVal Read As Boolean) As Boolean
        Try
            ReadDataStoreProcPrepareAux(SP)
            If Read And cOriginData = 2 Then
                cDataSQL = cDataCommandSQL.ExecuteReader()
            End If
            ReadDataStoreProcPrepare = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcPrepare = False
        End Try
    End Function
    Public Function ReadDataStoreProcExecute(ByVal SP As String) As Boolean
        Try
            cDataCommandSQL = New SqlCommand(SP, cDataConnectionSQL)
            cDataCommandSQL.CommandType = CommandType.StoredProcedure
            cDataCommandSQL.ExecuteNonQuery()
            ReadDataStoreProcExecute = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcExecute = False
        End Try
    End Function
    Public Function ReadDataStoreProcExecute(ByVal SP As String, ByVal Parameters() As Object, ByVal ParametersValue() As Object) As Boolean
        Try
            Select Case cOriginData
                Case 2
                    cDataCommandSQL = New SqlCommand(SP, cDataConnectionSQL)
                    cDataCommandSQL.CommandType = CommandType.StoredProcedure
                    For i As Integer = 0 To Parameters.GetUpperBound(0)
                        If IsDate(ParametersValue(i)) Then
                            Dim Aux As Date = ParametersValue(i)
                            cDataCommandSQL.Parameters.AddWithValue(Parameters(i), Aux)
                        Else
                            cDataCommandSQL.Parameters.Add(Parameters(i), ParametersValue(i))
                        End If
                    Next
                    Dim ReturnValueParam As SqlParameter = cDataCommandSQL.Parameters.Add("@RETURN_VALUE", SqlDbType.NVarChar, 100)
                    ReturnValueParam.Direction = ParameterDirection.ReturnValue
                    cDataCommandSQL.ExecuteNonQuery()
                    _Return_Val = cDataCommandSQL.Parameters("@RETURN_VALUE").Value.ToString
                Case 3
                    'cDataCommandMySQL = New MySqlCommand(SP, cDataConnectionMySQL)
                    'cDataCommandMySQL.CommandType = CommandType.StoredProcedure
                    'For i As Integer = 0 To Parameters.GetUpperBound(0)
                    ' System.Diagnostics.Debug.Print(Parameters(i))
                    ' cDataCommandMySQL.Parameters.Add(Parameters(i), ParametersValue(i))
                    'Next
                    'cDataCommandMySQL.ExecuteNonQuery()
            End Select
            ReadDataStoreProcExecute = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcExecute = False
        End Try
    End Function
    Public Function ReadDataStoreProcExecute(ByVal SP As String, ByVal Parameters() As Object, ByVal ParametersValue() As Object, ByVal ParametersOUT() As Object, ByRef ParametersOUTValue() As Object) As Boolean
        Try
            Select Case cOriginData
                Case 2
                    cDataCommandSQL = New SqlCommand(SP, cDataConnectionSQL)
                    cDataCommandSQL.CommandType = CommandType.StoredProcedure
                    For i As Integer = 0 To Parameters.GetUpperBound(0)
                        cDataCommandSQL.Parameters.Add(Parameters(i), ParametersValue(i))
                    Next
                    For i As Integer = 0 To ParametersOUT.GetUpperBound(0)
                        Dim ReturnValueParam As SqlParameter = cDataCommandSQL.Parameters.Add(ParametersOUT(i), SqlDbType.NVarChar, 50)
                        ReturnValueParam.Direction = ParameterDirection.Output
                    Next
                    Dim ReturnValueParamx As SqlParameter = New SqlParameter("@Return_Value", DbType.Int32)
                    ReturnValueParamx.Direction = ParameterDirection.ReturnValue
                    cDataCommandSQL.Parameters.Add(ReturnValueParamx)
                    cDataCommandSQL.ExecuteNonQuery()
                    ReDim ParametersOUTValue(ParametersOUT.GetUpperBound(0))
                    For i As Integer = 0 To ParametersOUT.GetUpperBound(0)
                        ParametersOUTValue(i) = cDataCommandSQL.Parameters(ParametersOUT(i)).Value.ToString
                    Next
                    _Return_Val = cDataCommandSQL.Parameters("@Return_Value").Value.ToString
                Case 3
                    'cDataCommandMySQL = New MySqlCommand(SP, cDataConnectionMySQL)
                    'cDataCommandMySQL.CommandType = CommandType.StoredProcedure
                    'For i As Integer = 0 To Parameters.GetUpperBound(0)
                    ' System.Diagnostics.Debug.Print(Parameters(i))
                    ' cDataCommandMySQL.Parameters.Add(Parameters(i), ParametersValue(i))
                    'Next
                    'cDataCommandMySQL.ExecuteNonQuery()
            End Select
            ReadDataStoreProcExecute = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcExecute = False
        End Try
    End Function
    Public Function ReadDataStoreProc() As DataSet
        Dim cDataDataSet As New DataSet
        If cOriginData = 2 Then cDataDataAdapter.Fill(cDataDataSet)
        Return cDataDataSet
    End Function
    Public Function ReadDataStoreProcExecute() As Boolean
        Try
            If cOriginData = 2 Then cDataCommandSQL.ExecuteNonQuery()
            ReadDataStoreProcExecute = True
        Catch ex As System.Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcExecute = False
        End Try
    End Function
    Public Function ReadDataStoreProcClose() As Boolean
        Try
            If Not cDataDataAdapter.SelectCommand Is Nothing Then
                If Not cDataDataAdapter.SelectCommand.Connection Is Nothing Then
                    cDataDataAdapter.SelectCommand.Connection.Dispose()
                End If
                cDataDataAdapter.SelectCommand.Dispose()
            End If
            cDataDataAdapter.Dispose()
            ReadDataStoreProcClose = True
        Catch ex As Exception
            cErrors += " " & ex.Message
            ReadDataStoreProcClose = False
        End Try
    End Function
    Public Function ReadData(ByVal Sql As String, Optional ByVal Read As Boolean = True, Optional ByVal Trans As Boolean = False) As Object
        Try
            If cOriginData = 0 Then
                cDataCommandOle = New OleDbCommand(Sql, cDataConnectionOle)
                If Trans Then cDataCommandOle.Transaction = cDataTransOle
                If Read Then
                    cDataOleDB = cDataCommandOle.ExecuteReader()
                    Return True
                Else
                    Return cDataCommandOle.ExecuteReader()
                End If
            ElseIf cOriginData = 1 Then
                cDataCommandODBC = New OdbcCommand(Sql, cDataConnectionODBC)
                If Trans Then cDataCommandODBC.Transaction = cDataTransODBC
                If Read Then
                    cDataODBC = cDataCommandODBC.ExecuteReader()
                    Return True
                Else
                    Return cDataCommandODBC.ExecuteReader()
                End If
            ElseIf cOriginData = 2 Then
                cDataCommandSQL = New SqlCommand(Sql, cDataConnectionSQL)
                If Trans Then cDataCommandSQL.Transaction = cDataTransSQL
                If Read Then
                    cDataSQL = cDataCommandSQL.ExecuteReader()
                    Return True
                Else
                    Return cDataCommandSQL.ExecuteReader()
                End If
            ElseIf cOriginData = 3 Then
                'cDataCommandMySQL = New MySqlCommand(Sql, cDataConnectionMySQL)
                'If Trans Then cDataCommandMySQL.Transaction = cDataTransMySQL
                'If Read Then
                ' cDataMySQL = cDataCommandMySQL.ExecuteReader()
                ' Return True
                'Else
                ' Return cDataCommandMySQL.ExecuteReader()
                'End If
            End If
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function
    Public Function ReadData(ByVal Sql As String, ByVal NombreConsulta As String) As DataSet
        Dim cDataDataAdapter As Object
        Dim cDataDataSet As New DataSet
        Try
            If cOriginData = 0 Then
                cDataCommandOle = New OleDbCommand(Sql, cDataConnectionOle)
                cDataDataAdapter = New OleDbDataAdapter(cDataCommandOle)
            ElseIf cOriginData = 1 Then
                cDataCommandODBC = New OdbcCommand(Sql, cDataConnectionODBC)
                cDataDataAdapter = New OdbcDataAdapter(cDataCommandODBC)
            ElseIf cOriginData = 2 Then
                cDataCommandSQL = New SqlCommand(Sql, cDataConnectionSQL)
                cDataDataAdapter = New SqlDataAdapter(cDataCommandSQL)
            ElseIf cOriginData = 3 Then
                'cDataCommandMySQL = New MySqlCommand(Sql, cDataConnectionMySQL)
                'cDataDataAdapter = New MySqlDataAdapter(cDataCommandMySQL)
            End If
            cDataDataAdapter.Fill(cDataDataSet, NombreConsulta)
        Catch ex As System.Exception
            cErrors += " " & ex.Message
        End Try
        Return cDataDataSet
    End Function

    Public Function Execute(ByVal Sql As String, Optional ByVal Trans As Boolean = False) As Boolean
        'Give back the number of the affected rows
        Try
            If cOriginData = 0 Then
                cDataCommandOle = New OleDbCommand(Sql, cDataConnectionOle)
                If Trans Then cDataCommandOle.Transaction = cDataTransOle
                cRowsAffected = cDataCommandOle.ExecuteNonQuery()
            ElseIf cOriginData = 1 Then
                cDataCommandODBC = New OdbcCommand(Sql, cDataConnectionODBC)
                If Trans Then cDataCommandODBC.Transaction = cDataTransODBC
                cRowsAffected = cDataCommandODBC.ExecuteNonQuery()
            ElseIf cOriginData = 2 Then
                cDataCommandSQL = New SqlCommand(Sql, cDataConnectionSQL)
                If Trans Then cDataCommandSQL.Transaction = cDataTransSQL
                cRowsAffected = cDataCommandSQL.ExecuteNonQuery()
            ElseIf cOriginData = 3 Then
                'cDataCommandMySQL = New MySqlCommand(Sql, cDataConnectionMySQL)
                'If Trans Then cDataCommandMySQL.Transaction = cDataTransMySQL
                'cRowsAffected = cDataCommandMySQL.ExecuteNonQuery()
            End If
        Catch ex As System.Exception
            cErrors += " " & ex.Message
            Return False
        End Try
        If cRowsAffected > 0 Then Return True
    End Function

    '***************************Transactions*********************************************
    Public Function Transaccion() As Boolean
        Return Transaccion(0)
    End Function
    Public Function Transaccion(ByVal Value As Integer) As Boolean
        Try
            If cOriginData = 0 Then
                Select Case Value
                    Case -1
                        cDataTransOle.Rollback()
                    Case 0
                        cDataTransOle = cDataConnectionOle.BeginTransaction
                        'cDataCommand.Transaction = cDataTrans
                    Case 1
                        cDataTransOle.Commit()
                End Select
            ElseIf cOriginData = 1 Then
                Select Case Value
                    Case -1
                        cDataTransODBC.Rollback()
                    Case 0
                        cDataTransODBC = cDataConnectionODBC.BeginTransaction
                    Case 1
                        cDataTransODBC.Commit()
                End Select
            ElseIf cOriginData = 2 Then
                Select Case Value
                    Case -1
                        cDataTransSQL.Rollback()
                    Case 0
                        cDataTransSQL = cDataConnectionSQL.BeginTransaction
                    Case 1
                        cDataTransSQL.Commit()
                End Select
            ElseIf cOriginData = 3 Then
                'Select Case Value
                ' Case -1
                ' cDataTransMySQL.Rollback()
                ' Case 0
                ' cDataTransMySQL = cDataConnectionMySQL.BeginTransaction
                ' Case 1
                ' cDataTransMySQL.Commit()
                'End Select
            End If
            Return True
        Catch ex As Exception
            cErrors += " " & ex.Message
        End Try
    End Function

    Protected Overrides Sub Finalize()
        cDataConnectionOle = Nothing
        cDataCommandOle = Nothing
        cDataConnectionODBC = Nothing
        cDataCommandODBC = Nothing
        cDataConnectionSQL = Nothing
        cDataCommandSQL = Nothing
        'cDataConnectionMySQL = Nothing
        'cDataCommandMySQL = Nothing

        cDataOleDB = Nothing
        cDataODBC = Nothing
        cDataSQL = Nothing
        'cDataMySQL = Nothing

        cDataTransOle = Nothing
        cDataTransODBC = Nothing
        cDataTransSQL = Nothing
        'cDataTransMySQL = Nothing

        MyBase.Finalize()
    End Sub
End Class
