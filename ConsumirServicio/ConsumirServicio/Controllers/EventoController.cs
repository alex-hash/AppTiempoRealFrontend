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
            if(Session["username"] == null)
            {
                return RedirectToAction("Index", "User", new { error = "Debes loguearte para acceder a los eventos" });
            }
            return View();
        }
    }
}
