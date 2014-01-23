<%@ Page Language="VB" AutoEventWireup="false" CodeFile="organigrama.aspx.vb" Inherits="file_Default" %>

<%@ Register Assembly="OrgChart" Namespace="OrgChart.Core" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Organigrama</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css">    
    <script type="text/javascript" src="../js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../js/src.js"></script>
</head>
<body>    
    <header>
        <div class="franja-oscura"></div>
        <div class="contenedor">
            <div class="izquierda">
            <img src="../images/logo.jpg">
            <h1 class="nombre-de-empresa">Lorem Ipsum</h1>  
        </div>  
        <div id="login">
            <form id="inicio-sesion">
                <label for='usuario'>Usuario:</label>
                <input type="text" name="usuario" placeholder="Usuario" class="usuario"></input>
                <label for='contrasena'>Contrase&ntilde;a:</label>
                <input type="password" name="contrasena" class="contrasena"></input>
                <input type="submit" class="submit" value="Iniciar sesi&oacute;n"></button>  
            </form>
        </div>
        <nav>
            <ul>
                <li><a href="../index.aspx">Pagina 1</a></li>
                <li  id="current"><a href="../file/pagina2.html">Pagina 2</a></li>
                <li><a href="../file/pagina3.html">Pagina 3</a></li>
                <li><a href="../file/pagina4.html">Pagina 4</a></li>
                <li  id="current"><a href="">Organigrama</a></li>
                <li><a href="../file/pagina5.html">Contacto</a></li>
            </ul>
        </nav>
        </div>
    </header>

    <section>
        <form id="form1" runat="server">
            <div class="organigrama">
                <cc1:DataBoundOrganisationChart ID="DataBoundOrganisationChart1" runat=server 
                    BackImageUrl="" DataSourceID="afiliados" LineColour="Blue" SortOrder="">
                    
                </cc1:DataBoundOrganisationChart>
                <asp:SqlDataSource ID="afiliados" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:db_testConnectionString1 %>" 
                    
                    SelectCommand="SELECT [UniqueId], [afi_str_nombre], [afi_str_apellido1], [ManagerId] FROM [tbl_afiliados]">
                </asp:SqlDataSource>
                <!--http://www.orgchartcomponent.com/walkthrough/VeryLargeOrgCharts/ -->
            </div>
        </form>
    </section>

    <footer>
        <div class="contenedor">
            <span  class="nombre-de-empresa izquierda">Lorem Ipsum</span>
            <span class="derecha">Sed ut perspiciatis unde omnis iste natus</span>
        </div>
    </footer>
</body>
</html>
