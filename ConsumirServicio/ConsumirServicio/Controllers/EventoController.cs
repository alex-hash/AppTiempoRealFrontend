using ConsumirServicio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumirServicio.Controllers
{
    public class EventoController : Controller
    {
        private string baseUrl = "http://localhost:63376/";
        public ActionResult Index()
        {
            List<Evento> eventos = GetEventos();

            if (eventos == null)
            {
                return RedirectToAction("Index");
            }

            return View(eventos);
        }

        private List<Evento> GetEventos()
        {

            Evento evento1 = new Evento()
            {
                Cuota = 2.2f,
                Fecha = DateTime.Now,
                Nombre = "asdf",
                Resultado = "adfsdfsd",
            };

            Evento evento2 = new Evento()
            {
                Cuota = 2.2f,
                Fecha = DateTime.Now,
                Nombre = "asdf",
                Resultado = "adfsdfsd",
            };

            Evento evento3 = new Evento()
            {
                Cuota = 2.2f,
                Fecha = DateTime.Now,
                Nombre = "asdf",
                Resultado = "adfsdfsd",
            };

            List<Evento> lista = new List<Evento>();

            lista.Add(evento1);

            lista.Add(evento2);

            lista.Add(evento3);

            return lista;
            /*
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> tarea = cliente.GetAsync("api/Evento");
                tarea.Wait();
                var response = tarea.Result;

                if (response.IsSuccessStatusCode)
                {
                    var jugadoresResponse = response.Content.ReadAsStringAsync().Result;
                    var listaEventos = JsonConvert.DeserializeObject<List<Evento>>(jugadoresResponse);

                    return listaEventos;
                }
                else
                {
                    return null;
                }
            }
            */
        }
    }
}
