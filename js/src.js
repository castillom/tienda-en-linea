$( document ).ready(function() {
    $("#current").on( "click", function(e) {
        e.preventDefault();
		console.log("preventDefault");
    });

    $(".submit").on( "click", function(e) {
        var form = $("#form"),
        	requerido = $(".requerido"),
    		err = $(".msj-requerido"),
    		esInvalido = false,
    		emailValido = /([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})/;;

        e.preventDefault();

    	requerido.removeClass("requerido-fail");
    	err.css("visibility", "hidden");
    	
    	for (var i = 0 ; i < requerido.length; i++) {
    		var myValue = requerido[i].value.trim();

            if (myValue === "") {
                $(requerido[i]).addClass("requerido-fail");
                err.css("visibility", "visible");
                esInvalido = true;
            }
    	};

    	if (!esValido) {
                requerido.removeClass("requerido-fail");
    			err.css("visibility", "hidden");   
                form.submit();
        };
    });

});