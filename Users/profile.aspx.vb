Partial Class profile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim User As New dllUser
            Try
                gvUsers.DataSource = User.GetUsers
                gvUsers.DataBind()

            Catch ex As Exception
                lblMessages.Text = User.Errors
            End Try
            User = Nothing
        End If
    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Response.Redirect("newuser.aspx")
    End Sub

End Class