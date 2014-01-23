<?php
    $nombre = $_POST['nombre'];
	$email = $_POST['email'];
	$asunto = $_POST['asunto'];
	$mensaje = $_POST['mensaje'];

    $to = "monicastillos@gmail.com";
    $subject = "Nuevo mensaje - $asunto";
    $message =  "Ha recibido un nuevo mensaje. ".
	"Detalles:\n ".
	"Nombre: $nombre \n ".
	"Email: $email_address\n".
	"Mensaje: \n $mensaje";

    $headers = "From: $email";
    $sent = mail($to, $subject, $message, $headers) ;

    if ($sent) {
      header("Location: http://www.monicac.2trweb.com/file/pagina5.html");
      exit();
    } else {
      print "We encountered an error sending your mail"; 
    }

?>