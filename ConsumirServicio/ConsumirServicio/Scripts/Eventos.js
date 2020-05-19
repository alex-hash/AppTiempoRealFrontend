eventos = [{ Id: 1, Nombre: "nombre1", Fecha: new Date(), Cuota: 3.3, Resultado: '1-1' }, { Id: 2, Nombre: "nombre2", Fecha: new Date(), Cuota: 2.3, Resultado: '2-1' },];

BaseAddress = "http://localhost:63376/";

window.onload = OnLoad();

function OnLoad() {
    setInterval(function () { CargarEventosDeApi(eventos); }, 500);
}

function CargarEventosEnTabla(eventos) {
    BorrarTabla();

    var cuerpoTabla = $("#tablaEventos>tbody");

    for (var i = 0; i < eventos.length; i++) {

        var fila = document.createElement("tr");

        var casillaNombre = document.createElement("td");
        casillaNombre.textContent = eventos[i].Nombre;

        var casillaFecha = document.createElement("td");
        casillaFecha.textContent = eventos[i].Fecha;

        var casillaCuota = document.createElement("td");
        casillaCuota.textContent = eventos[i].Cuota;

        var casillaResultado = document.createElement("td");
        casillaResultado.textContent = eventos[i].Resultado;

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
    /*
    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri(BaseAddress);

    // Add an Accept header for JSON format.
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    // List data response.
    HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
    if (response.IsSuccessStatusCode) {
        // Parse the response body.
        var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
        /*foreach(var d in dataObjects)
        {
            Console.WriteLine("{0}", d.Name);
        }*//*
    }
    else {
        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
    }*/

    /*
    $.ajax({
        url: url,
        headers: {
            Accept: "application/json;odata=verbose"
        },
        xhrFields: { withCredentials: true },
        async: false,
        success: function (data) {
            var items = data.d;
            console.log("Login Name: " + items.LoginName);
            console.log("Email: " + items.Email);
            console.log("ID: " + items.Id);
            console.log("Title: " + items.Title);
        },
        error: function (jqxr, errorCode, errorThrown) {
            console.log(jqxr.responseText);
        }
    });
    */



    $.ajax({
        url: 'api/Async/Eventos',
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8', 
        cache: false,
        data: {},
        success: function (data) {
            // data is your result from controller
            if (data.success) {
                CargarEventosEnTabla(data.message);
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
}


