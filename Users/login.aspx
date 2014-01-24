<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
CodeFile="login.aspx.vb" Inherits="users_login" Title="Untitled Page" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:Login ID="lgLogin" runat="server" DestinationPageUrl="~/users/profile.aspx"
DisplayRememberMe="True" FailureText="Login no valido, intentelo otra vez."
LoginButtonText="Enviar" PasswordRequiredErrorMessage="El Password es requerido."
RememberMeText="Recordarme la proxima vez." TitleText="Iniciar sesión"
UserNameLabelText="Usuario:" UserNameRequiredErrorMessage="El nombre de usuario es requerido.">
</asp:Login>
</asp:Content>