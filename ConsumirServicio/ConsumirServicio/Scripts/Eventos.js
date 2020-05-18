function BorrarTabla() {
    console.log("Borrando tabla");
    $("#tablaEventos>tbody>tr.fila-evento").remove();
}

function CargarPagina() {
    console.log("sadf");
}

window.onload = OnLoad();

function OnLoad() {
    var eventos = [{ id: 1, nombre: "nombre1", fecha: new Date(), cuota: "cuota1" }, { id: 2, nombre: "nombre2", fecha: new Date(), cuota: "cuota2" },];
    var intervalID = window.setInterval(BorrarTabla(), 500);
}


function CargarEventosDeApi(idEvento) {
    $.ajax({
        url: 'http://localhost:63376/Evento/Get',
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8', 
        cache: false,
        data: { id: idEvento },
        success: function (data) {
            // data is your result from controller
            if (data.success) {
                CargarEventos(data.message);
            }
        },
        error: function (xhr) {
            alert('error');
        }
    });
}

function CargarEventos(eventos) {
    var cuerpoTabla = $("#tablaEventos>tbody");

    for (var i = 0; i < eventos.length; i++) {
        var fila = document.createElement("tr");

        var casillaNombre = document.createElement("th");
        casillaNombre.textContent = eventos[i].nombre;

        var casillaFecha = document.createElement("th");
        casillaFecha.textContent = eventos[i].fecha;

        var casillaCuota = document.createElement("th");
        casillaCuota.textContent = eventos[i].cuota;

        var casillaResultado = document.createElement("th");
        casillaResultado.textContent = eventos[i].resultado;

        fila.add(casillaNombre);
        fila.add(casillaFecha);
        fila.add(casillaCuota);
        fila.add(casillaResultado);

        cuerpoTabla.add(fila)
    }

}
