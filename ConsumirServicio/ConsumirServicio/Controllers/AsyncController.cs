using Celula.Extensions.Controllers;
using ConsumirServicio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace ConsumirServicio.Controllers
{
    public class AsyncController : ApiBaseController
    {
        private string baseUrl = "http://localhost:63376/"; 

        [System.Web.Http.HttpGet]
        public ActionResult Get()
        {

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    Task<HttpResponseMessage> tarea = cliente.GetAsync("api/Evento");
                    tarea.Wait();
                    var response = tarea.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var jugadoresResponse = response.Content.ReadAsStringAsync().Result;
                        var listaEventos = JsonConvert.DeserializeObject<List<Evento>>(jugadoresResponse);

                        return JsonSuccess(listaEventos);
                    }
                    else
                    {
                        return JsonError("No se ha podido recuperar");
                    }
                }catch(Exception ex)
                {
                    return null;
                }
                
            }

        }

    }
}
