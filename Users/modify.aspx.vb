
Partial Class users_modify
    Inherits System.Web.UI.Page

    Sub ServerValidation(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)

        'Dim num As Integer = Integer.Parse(arguments.Value)
        'arguments.IsValid = ((num Mod 2) = 0)
        arguments.IsValid = IsDate(arguments.Value) AndAlso arguments.Value > Now
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request("ID").Trim.Length > 0 Then
                Dim User As New dllUser
                If User.UserRead(Request("ID")) Then
                    txtPassword.Text = User.Password
                    txtUserName.Text = User.UserName
                    chkActive.Checked = User.Active
                Else
                    lblMessages.Text = User.ErrorsUser
                End If
                User = Nothing
            Else
                lblMessages.Text = "Debe especificar un usuario."
            End If
        End If
    End Sub

    Protected Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If Request("ID").Trim.Length > 0 Then
            If Page.IsValid Then
                Try
                    Dim User As New dllUser
                    User.UserName = txtUserName.Text
                    User.Password = txtPassword.Text
                    User.Active = chkActive.Checked

                    If User.UserModify(Request("ID")) Then
                        lblMessages.Text = "Usuario modificado correctamente."
                    Else
                        lblMessages.Text = User.ErrorsUser
                    End If
                    User = Nothing
                Catch ex As Exception
                    lblMessages.Text = ex.Message
                End Try
            Else
                lblMessages.Text = "Hay elementos no validos en la pagina."
            End If
        Else
            lblMessages.Text = "Debe especificar un usuario."
        End If
    End Sub

End Class
