
Partial Class users_delete
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblUserNumber.Text = " Desea borrar al usuario " & Request("ID") & "?"
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim User As New dllUser()
        If User.UserDelete(Request("ID")) Then
            lblUserNumber.Text = "Borrado"
            btnDelete.Visible = False
        Else
            lblUserNumber.Text = User.ErrorsUser
        End If
        User = Nothing
    End Sub
End Class
