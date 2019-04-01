using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class PedidoEnsayo
    {
        public List<Entidades> EntidadesLista { get; set; }

        public List<Contacto> ContactosLista { get; set; }

        public List<LugarEnsayos> LugarEnsayosLista { get; set; }

        public List<Usuarios> UsuariosLista { get; set; }

        public List<Productos> ProductosLista { get; set; }

        public List<Cotizaciones> CotizacionesLista { get; set; }

        public List<Presupuesto> PresupuestoLista { get; set; }

        public List<PedidoEnsayo> PedidoEnsayoLista { get; set; }

        public List<Muestras> MuestrasLista { get; set; }

        public List<Aprobacion> AprobacionLista { get; set; }

        public List<Ensayos> EnsayosLista { get; set; }

        private PedidoEnsayoContext context;

        public int IdPedidoEnsayo { get; set; }

        public string ProtocoloAplicable { get; set; }

        public string CondicionesDeEnsayo { get; set; }

        public string AutorPedido { get; set; }

        public int presupuestos_IdPresupuestos { get; set; }

        public string NumeroSec { get; set; }

        public int StatusPedidoEnsayo { get; set; }

        public string Comentarios { get; set; }
    }
}