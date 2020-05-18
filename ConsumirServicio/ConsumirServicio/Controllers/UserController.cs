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
    public class UserController : Controller
    {
        string baseUrl = "http://localhost:52664/";
        public ActionResult Index(string error)
        {
            if (error != null)
            {
                ViewBag.Mensaje = error;
            }
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult EnvioRegistro(string username, string password, string balance)
        {
            Monedero monedero = new Monedero();
            monedero.balance = Decimal.Parse(balance);
            monedero.IdMonedero = Guid.NewGuid().ToString();
            CrearMonederoDB(monedero);

            Jugador jug = new Jugador();
            jug.Nombre = username;
            jug.Password = password;
            jug.IdJugador = Guid.NewGuid().ToString();
            jug.IdMonedero = monedero.IdMonedero;
            CrearJugadorDB(jug);


            return RedirectToAction("Index");
        }

        public HttpResponseMessage CrearMonederoDB(Monedero monedero)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseUrl + "api/Monedero");
                    var postTask = cliente.PostAsJsonAsync<Monedero>("Monedero", monedero);
                    postTask.Wait();
                    return postTask.Result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage CrearJugadorDB(Jugador jug)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseUrl + "api/Jugador");
                    var postTask = cliente.PostAsJsonAsync<Jugador>("Jugador", jug);
                    postTask.Wait();
                    return postTask.Result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> EnvioLogin(string username, string password)
        {
            Jugador jugador = new Jugador();
            Monedero monedero = new Monedero();
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseUrl);
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await cliente.GetAsync(String.Format("/api/Jugador/login/?username={0}&password={1}", username, password));

                    if (response.IsSuccessStatusCode)
                    {
                        var jugadorResponse = response.Content.ReadAsStringAsync().Result;
                        jugador = JsonConvert.DeserializeObject<Jugador>(jugadorResponse);
                        HttpResponseMessage responseM = await cliente.GetAsync(String.Format("/api/Monedero/?id={0}", jugador.IdMonedero));
                        if (responseM.IsSuccessStatusCode)
                        {
                            var responseMonedero = responseM.Content.ReadAsStringAsync().Result;
                            monedero = JsonConvert.DeserializeObject<Monedero>(responseMonedero);
                            System.Web.HttpContext.Current.Session["idJugador"] = jugador.IdJugador;
                            System.Web.HttpContext.Current.Session["username"] = jugador.Nombre;
                            System.Web.HttpContext.Current.Session["balance"] = monedero.balance;
                            System.Web.HttpContext.Current.Session["idMonedero"] = monedero.IdMonedero;
                            return RedirectToAction("Listado", "Evento");
                        }
                        else
                        {
                            return RedirectToAction("Index", new { error = "Error en el login" });
                        }

                    }
                    else
                    {
                        return RedirectToAction("Index", new { error = "Error en el login" });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}