using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumirServicio.Models
{
    public class Evento
    {
        private int id;
        private string nombre;
        private DateTime fecha;
        private float cuota;
        private string resultado;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public float Cuota { get => cuota; set => cuota = value; }
        public string Resultado { get => resultado; set => resultado = value; }

    }
}