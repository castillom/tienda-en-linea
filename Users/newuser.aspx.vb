
Partial Class newuser
    Inherits System.Web.UI.Page

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim User As New dllUser()

        User.UserName = txtUserName.Text
        User.Password = txtPassword.Text

        If User.UserAdd Then

            lblTextReturn.Text = "Hecho"

            txtUserName.Text = ""
            txtPassword.Text = ""

        Else
            lblTextReturn.Text = User.ErrorsUser
        End If
        User = Nothing

    End Sub

End Class
