window.onload = OnLoad();

function OnLoad() {
    setInterval(function () { CargarEventosDeApi(eventos); }, 500);
}

function CargarEventosEnTabla(eventos) {
    BorrarTabla();

    var cuerpoTabla = $("#tablaEventos>tbody");
    console.log('Eventos.length=' + eventos.length);

    for (var i = 0; i < eventos.length; i++) {

        var fila = document.createElement("tr");

        var casillaNombre = document.createElement("td");
        casillaNombre.textContent = eventos[i].nombre;

        var casillaFecha = document.createElement("td");
        casillaFecha.textContent = eventos[i].fecha;

        var casillaCuota = document.createElement("td");
        casillaCuota.textContent = eventos[i].cuota;

        var casillaResultado = document.createElement("td");
        casillaResultado.textContent = eventos[i].resultado;

        fila.appendChild(casillaNombre);
        fila.appendChild(casillaFecha);
        fila.appendChild(casillaCuota);
        fila.appendChild(casillaResultado);

        fila.classList.add('fila-evento');

        cuerpoTabla.append(fila);
    }
}

function BorrarTabla() {
    console.log("Borrando tabla");
    $("#tablaEventos>tbody>tr.fila-evento").remove();
}

function CargarEventosDeApi() {
    $.ajax({
        url: 'Async/Get',
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8', 
        cache: false,
        data: {},
        success: function (data) {
            // data is your result from controller
            if (data.success) {
                CargarEventosEnTabla(data.data);
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
}


