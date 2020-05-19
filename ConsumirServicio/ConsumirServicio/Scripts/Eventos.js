eventos = [{ id: 1, nombre: "nombre1", fecha: new Date(), cuota: 3.3, resultado: '1-1' }, { id: 2, nombre: "nombre2", fecha: new Date(), cuota: 2.3, resultado: '2-1'  },];

window.onload = OnLoad();

function OnLoad() {
    setInterval(function () { CargarEventos(eventos); }, 500);
}

function CargarEventos(eventos) {
    BorrarTabla();

    var cuerpoTabla = $("#tablaEventos>tbody");

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


