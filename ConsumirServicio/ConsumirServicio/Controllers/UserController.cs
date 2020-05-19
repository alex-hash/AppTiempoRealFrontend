using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsumirServicio.Models;

namespace ConsumirServicio.Controllers
{
    public class UserController : Controller
    {
        string baseUrl = "http://localhost:63376/";
        public ActionResult Index(string error)
        {
            if (error != null)
            {
                ViewBag.Mensaje = error;
            }
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult EnvioRegistro(string username, string password, string balance, string nombre, string apellido)
        {

            Jugador jug = new Jugador();
            jug.login = username;
            jug.password = password;
            jug.nombre = nombre;
            jug.apellido = apellido;
            HttpResponseMessage response = CrearJugadorDB(jug);
            var responseJugador = response.Content.ReadAsStringAsync().Result;
            jug = JsonConvert.DeserializeObject<Jugador>(responseJugador);

            Monedero monedero = new Monedero();
            monedero.saldo = Decimal.Parse(balance);
            monedero.idJugador = jug.id; 
            CrearMonederoDB(monedero);
            
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

        public HttpResponseMessage CrearJugadorDB(Jugador jugador)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(baseUrl + "api/Jugador");
                    var postTask = cliente.PostAsJsonAsync<Jugador>("Jugador", jugador);
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
                    HttpResponseMessage response = await cliente.GetAsync(String.Format("/api/Jugador/?login={0}&pass={1}", username, password));

                    if (response.IsSuccessStatusCode)
                    {
                        var jugadorResponse = response.Content.ReadAsStringAsync().Result;
                        jugador = JsonConvert.DeserializeObject<Jugador>(jugadorResponse);
                        if(jugador == null)
                        {
                            return RedirectToAction("Index", new { error = "Error en el login" });
                        }
                        HttpResponseMessage responseM = await cliente.GetAsync(String.Format("/api/Monedero/?id={0}", jugador.id));
                        if (responseM.IsSuccessStatusCode)
                        {
                            var responseMonedero = responseM.Content.ReadAsStringAsync().Result;
                            monedero = JsonConvert.DeserializeObject<Monedero>(responseMonedero);
                            System.Web.HttpContext.Current.Session["idJugador"] = jugador.id;
                            System.Web.HttpContext.Current.Session["username"] = jugador.nombre;
                            System.Web.HttpContext.Current.Session["balance"] = monedero.saldo;
                            System.Web.HttpContext.Current.Session["idMonedero"] = monedero.idMonedero;
                            return RedirectToAction("Index", "Evento");
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