<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="newuser.aspx.vb" Inherits="newuser" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="1" cellspacing="1">
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Text="Nombre Usuario:"></asp:Label>
        </td>
        <td style="width: 125px">
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
        ErrorMessage="Nombre Usuario requerido">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
        </td>
        <td style="width: 125px">
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
        ErrorMessage="Password requerido">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:Label ID="Label3" runat="server" Text="Número de cédula:"></asp:Label>
        </td>
        <td style="width: 125px">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1"
        ErrorMessage="Número de cédula requerido">*</asp:RequiredFieldValidator>
        </td>
    </tr>
</table>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Tienes que poner un valor en los siguientes campos:"
ShowMessageBox="True" ShowSummary="False" />
<br />
<asp:Button ID="btnAdd" runat="server" Text="Añadir" />
<asp:Label ID="lblTextReturn" runat="server"></asp:Label>
</asp:Content>