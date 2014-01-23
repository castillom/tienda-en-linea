<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Pagina principal</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
    <script type="text/javascript" src="jquery.jOrgChart.js"></script>
    <script type="text/javascript" src="js/src.js"></script>

</head>
<body runat="server">
    <form id="form1" runat="server">
    <header>
        <div class="franja-oscura"></div>
        <div class="contenedor">
            <div class="izquierda">
                <img alt="logo" src="images/logo.jpg">
                <h1 class="nombre-de-empresa">Lorem Ipsum</h1>  
            </div>  
            <div class="login">
                <a href="">Iniciar sesi&oacute;n</a> 
            </div>
            <nav>
                <ul>
                    <li  id="current"><a href="">Pagina 1</a></li>
                    <li><a href="file/pagina2.html">Pagina 2</a></li>
                    <li><a href="file/pagina3.html">Pagina 3</a></li>
                    <li><a href="file/pagina4.html">Pagina 4</a></li>
                    <li><a href="memberPages/organigrama.aspx">Organigrama</a></li>
                    <li><a href="file/pagina5.html">Contacto</a></li>
                </ul>
            </nav>
        </div>
    </header>

    <section>

        
        <h2>Lorem ipsum</h2>
        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.</p>

        <p>Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.</p>

        <p>Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi.</p>

        <p>Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,</p>
    </section>

    <footer>
        <div class="contenedor">
            <span  class="nombre-de-empresa izquierda">Lorem Ipsum</span>
            <span class="derecha">Sed ut perspiciatis unde omnis iste natus</span>
        </div>
    </footer>
    </form>
</body>
</html>