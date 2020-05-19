using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumirServicio.Models
{
    public class Monedero
    {
        public int idMonedero { get; set; }
        public int idJugador { get; set; }
        public decimal saldo { get; set; }
    }
}