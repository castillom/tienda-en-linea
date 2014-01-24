Imports Microsoft.VisualBasic
Imports System.Configuration.ConfigurationManager
'Imports GBData

Public Class dllBase

    Friend _ID As String
    Private ConnectionPath As String = ConnectionStrings("DataBase").ConnectionString

    Friend cData As dllData

    Public Function Errors() As String
        Errors = cData.Errors
    End Function

    Friend Sub New()
        cData = New dllData
        cData.OriginData = 2
        cData.Path = ConnectionPath
    End Sub
    Protected Overrides Sub Finalize()
        cData = Nothing
        MyBase.Finalize()
    End Sub

    Public Property ID() As String
        Get
            ID = _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

End Class