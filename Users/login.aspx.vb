
Partial Class users_login
    Inherits System.Web.UI.Page

    Private Function ValidateUser(ByVal User As String, ByVal Pass As String) As Boolean
        Dim UserLogin As New dllUser()
        If UserLogin.Login(User, Pass) Then
            Return True
        End If
        UserLogin = Nothing
    End Function

    Protected Sub lgLogin_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles lgLogin.Authenticate
        If ValidateUser(lgLogin.UserName, lgLogin.Password) Then
            e.Authenticated = True
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lgLogin.Focus()
    End Sub
End Class