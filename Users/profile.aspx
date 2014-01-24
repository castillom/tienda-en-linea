<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="profile.aspx.vb" Inherits="profile" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Button ID="btnAddUser" runat="server" Text="Nuevo Usuario" />
<br />
<asp:GridView ID="gvUsers" runat="server" Caption="Usuarios" GridLines="Horizontal" AutoGenerateColumns="False">
<Columns>
<asp:BoundField DataField="ID" HeaderText="ID" />
<asp:BoundField DataField="UserName" HeaderText="Usuario" />
<asp:BoundField HeaderText="Activo" DataField="Active" />
<asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="modify.aspx?id={0}"
DataTextField="ID" DataTextFormatString="Modificar" HeaderText="Modificar" />
<asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="delete.aspx?id={0}"
DataTextField="ID" DataTextFormatString="Borrar" HeaderText="Borrar" />
</Columns>
</asp:GridView>
<asp:Label ID="lblMessages" runat="server"></asp:Label><br />
</asp:Content>