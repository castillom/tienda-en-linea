Imports Microsoft.VisualBasic

Public Class dllUser
    Inherits dllBase

    Private _UserName As String
    Private _Password As String
    Private _Active As Boolean 'Desactivará momentáneamente la descarga para esa tienda.
    'Private _Date_In As Date
    'Private _Date_Out As Date 'Esto permitirá que el aplicativo ya no se pueda usar por una tienda.
    'Private _Comments As String

    Private _Return_Value As Integer
    Private _Errors As String

    Public ReadOnly Property ErrorsUser() As String
        Get
            ErrorsUser = _Errors
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Property UserName() As String
        Get
            UserName = _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Password = _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property Active() As Boolean
        Get
            Active = _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property
    'Public Property Date_New() As Date
    '    Get
    '        Date_New = _Date_In
    '    End Get
    '    Set(ByVal value As Date)
    '        _Date_In = value
    '    End Set
    'End Property
    'Public Property Date_Out() As Date
    '    Get
    '        Date_Out = _Date_Out
    '    End Get
    '    Set(ByVal value As Date)
    '        _Date_Out = value
    '    End Set
    'End Property
    'Public Property Comments() As String
    '    Get
    '        Comments = _Comments
    '    End Get
    '    Set(ByVal value As String)
    '        _Comments = value
    '    End Set
    'End Property
    Public Property Return_Value() As Integer
        Get
            Return_Value = _Return_Value
        End Get
        Set(ByVal value As Integer)
            _Return_Value = value
        End Set
    End Property

    Public Function Login(ByVal UserName As String, ByVal Password As String) As Boolean
        Dim Parameters(1) As String, ParametersValue(1) As String

        Parameters(0) = "@UserName"
        ParametersValue(0) = UserName
        Parameters(1) = "@Password"
        ParametersValue(1) = Password

        Try
            If cData.Connect() Then
                cData.ErrorsClear()
                If Not cData.ReadDataStoreProcPrepare("UserLogin", Parameters, ParametersValue) Then
                    _Errors = cData.Errors
                    Login = False
                Else
                    cData.ReadDataStoreProcPrepareExecute()
                    If cData.ReadHaveData Then Login = True
                End If
                cData.ReadDataStoreProcClose()
                cData.Disconnect()
            Else
                Login = False
                _Errors = "No se pudo conectar con la base de datos."
            End If
        Catch ex As Exception
            Login = False
            _Errors = ex.Message
        End Try

    End Function

    Public Function GetPassword() As String
        Dim Parameters(0) As String, ParametersValue(0) As String

        Parameters(0) = "@Email"
        ParametersValue(0) = My.User.Name

        If cData.Connect() Then
            cData.ReadDataStoreProcPrepare("UserGetPassword", Parameters, ParametersValue)
            Dim a As Data.DataSet = cData.ReadDataStoreProc
            GetPassword = cData.ReadSP("Password")
            'GetPassword = a.Tables(0).Rows(0).Item("Password")
            cData.ReadDataStoreProcClose()
            cData.Disconnect()
        End If
    End Function

    Public Function ChangePassword(ByVal NewP As String) As Boolean
        Dim Parameters(1) As String, ParametersValue(1) As String

        Parameters(0) = "@Email"
        ParametersValue(0) = My.User.Name
        Parameters(1) = "@Password"
        ParametersValue(1) = NewP

        If cData.Connect() Then
            If Not cData.ReadDataStoreProcExecute("UserChangePassword", Parameters, ParametersValue) Then
                ChangePassword = False
            Else
                ChangePassword = True
            End If
            cData.ReadDataStoreProcClose()
            cData.Disconnect()
        End If
    End Function

    Public Function UserDelete(ByVal ID As Integer) As Boolean
        Dim Parameters(0) As String, ParametersValue(0) As String

        Parameters(0) = "@ID"
        ParametersValue(0) = ID

        Try
            If cData.Connect() Then
                cData.ErrorsClear()
                If cData.ReadDataStoreProcExecute("UserDelete", Parameters, ParametersValue) Then
                    If cData.Return_Val = 0 Then
                        UserDelete = True
                    Else
                        _Errors = "Usuario no existia."
                    End If
                Else
                    _Errors = cData.Errors
                    UserDelete = False
                End If
                cData.ReadDataStoreProcClose()
                cData.Disconnect()
            Else
                _Errors = "No se pudo conectar con la base de datos."
                UserDelete = False
            End If
        Catch ex As Exception
            _Errors = ex.Message
        End Try
    End Function

    Public Function UserAdd() As Boolean

        Dim Parameters(3) As String, ParametersValue(3) As String

        Parameters(0) = "@UserName"
        Parameters(1) = "@Password"
        'Parameters(2) = "@Comments"
        Parameters(3) = "@Active"

        ParametersValue(0) = _UserName
        ParametersValue(1) = _Password
        'ParametersValue(2) = _Comments
        ParametersValue(3) = _Active

        If cData.Connect() Then
            cData.ErrorsClear()
            If Not cData.ReadDataStoreProcExecute("UserAdd", Parameters, ParametersValue) Then
                _Errors = cData.Errors
            Else
                If cData.Return_Val = "-1" Then
                    _Errors = "El nombre de usuario ya existe."
                ElseIf cData.Return_Val = 0 Then
                    UserAdd = True
                End If
            End If
            cData.ReadDataStoreProcClose()
            cData.Disconnect()
        Else
            _Errors = "No se pudo conectar con la base de datos."
            UserAdd = False
        End If

    End Function

    Public Function UserModify(ByVal ID As Long) As Boolean

        Dim Parameters(5) As String, ParametersValue(5) As String

        Parameters(0) = "@ID"
        Parameters(1) = "@UserName"
        Parameters(2) = "@Password"
        'Parameters(3) = "@Comments"
        Parameters(4) = "@Active"
        'Parameters(5) = "@Date_Out"

        ParametersValue(0) = ID
        ParametersValue(1) = _UserName
        ParametersValue(2) = _Password
        'ParametersValue(3) = _Comments
        ParametersValue(4) = _Active
        'ParametersValue(5) = _Date_Out

        Try
            If cData.Connect() Then
                cData.ErrorsClear()
                If Not cData.ReadDataStoreProcExecute("UserModify", Parameters, ParametersValue) Then
                    _Errors = cData.Errors
                Else
                    If cData.Return_Val = 0 Then
                        UserModify = True
                    End If
                End If
                cData.ReadDataStoreProcClose()
                cData.Disconnect()
            Else
                _Errors = "No se pudo conectar con la base de datos."
                UserModify = False
            End If
        Catch ex As Exception
            _Errors = ex.Message
        End Try

    End Function

    Public Function GetMessages() As Data.DataSet
        Dim Parameters(0) As String, ParametersValue(0) As String

        Parameters(0) = "@Email"
        ParametersValue(0) = My.User.Name

        If cData.Connect() Then
            If cData.ReadDataStoreProcPrepare("UserGetMessages", Parameters, ParametersValue) Then
                GetMessages = cData.ReadDataStoreProc()
                cData.ReadDataStoreProcClose()
                cData.Disconnect()
            Else
                _Errors = "Can't read the messages."
            End If
        Else
            _Errors = "Cant connect with the database."
        End If
    End Function

    Public Function GetMessagesByMonth(ByVal Month As Integer) As Data.DataSet
        Dim Parameters(1) As String, ParametersValue(1) As String

        Parameters(0) = "@Email"
        ParametersValue(0) = My.User.Name
        Parameters(1) = "@Month"
        ParametersValue(1) = Month

        If cData.Connect() Then
            If cData.ReadDataStoreProcPrepare("UserGetMessagesByMonth", Parameters, ParametersValue) Then
                GetMessagesByMonth = cData.ReadDataStoreProc()
                cData.ReadDataStoreProcClose()
                cData.Disconnect()
            Else
                _Errors = "Can't read the messages."
            End If
        Else
            _Errors = "Cant connect with the database."
        End If
    End Function

    Public Function GetMessagesSearch(ByVal Str As String) As Data.DataSet
        Dim Parameters(1) As String, ParametersValue(1) As String

        Parameters(0) = "@Email"
        ParametersValue(0) = My.User.Name
        Parameters(1) = "@String"
        ParametersValue(1) = Str

        If cData.Connect() Then
            If cData.ReadDataStoreProcPrepare("UserGetMessagesSearch", Parameters, ParametersValue) Then
                GetMessagesSearch = cData.ReadDataStoreProc()
                cData.ReadDataStoreProcClose()
                cData.Disconnect()
            Else
                _Errors = "Can't read the messages."
            End If
        Else
            _Errors = "Cant connect with the database."
        End If
    End Function

    Public Function UserRead(ByVal ID As Long) As Boolean
        Dim Parameters(0) As String, ParametersValue(0) As String
        Dim DS As New Data.DataSet

        If ID > 0 Then
            Try
                Parameters(0) = "@ID"
                ParametersValue(0) = ID

                If cData.Connect() Then
                    cData.ErrorsClear()
                    If cData.ReadDataStoreProcPrepare("UserRead", Parameters, ParametersValue) Then
                        DS = cData.ReadDataStoreProc()
                        '_Nombre_Tienda = cData.ReadSP("Nombre_Tienda")
                        With DS.Tables(0).Rows(0)
                            _Password = .Item("Password")
                            _UserName = .Item("UserName")
                            _Active = .Item("Active")
                            '_Date_Out = IIf(.Item("Date_Out") Is DBNull.Value, "#12:00:00 AM#", .Item("Date_Out"))
                            '_Date_In = .Item("Date_In")
                            '_Comments = .Item("Comments")
                        End With
                        UserRead = True
                    Else
                        _Errors = cData.Errors
                    End If
                    cData.ReadDataStoreProcClose()
                    cData.Disconnect()
                Else
                    _Errors = "Cant connect with the database."
                End If

            Catch ex As Exception
                _Errors = ex.Message
            End Try
        Else
            _Errors = "User not provided."
        End If
    End Function

    Public Function GetUsers() As Data.DataSet
        If cData.Connect() Then
            cData.ErrorsClear()
            If cData.ReadDataStoreProcPrepare("GetUsers") Then
                GetUsers = cData.ReadDataStoreProc()
                cData.ReadDataStoreProcClose()
                cData.Disconnect()
            Else
                _Errors = cData.Errors
            End If
        Else
            _Errors = "Cant connect with the database."
        End If
    End Function

End Class