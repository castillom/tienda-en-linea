<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="modify.aspx.vb" Inherits="users_modify" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Text="Nombre Usuario:"></asp:Label>
        </td>
        <td style="width: 141px">
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
            ErrorMessage="Nombre de usuario requerido.">*</asp:RequiredFieldValidator>
        </td>
        
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
        </td>
        <td style="width: 141px">
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
            ErrorMessage="Password requerido.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label7" runat="server" Text="Activo:"></asp:Label>
        </td>
        <td style="width: 141px">
            <asp:CheckBox ID="chkActive" runat="server" />
        </td>
    </tr>
    
</table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Tienes que poner un valor en los siguientes campos:"
ShowMessageBox="False" ShowSummary="true" />
<br />
<asp:Label ID="lblMessages" runat="server"></asp:Label>
&nbsp;
&nbsp; &nbsp;&nbsp;
</asp:Content>