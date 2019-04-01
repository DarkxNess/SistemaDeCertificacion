using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Cotizaciones
    {
        private PresupuestoContext context;

        public List<Entidades> EntidadesLista { get; set; }

        public List<Contacto> ContactosLista { get; set; }

        public List<LugarEnsayos> LugarEnsayosLista { get; set; }

        public List<Usuarios> UsuariosLista { get; set; }

        public List<Productos> ProductosLista { get; set; }

        public List<Cotizaciones> CotizacionesLista { get; set; }

        public int IdCotizacion { get; set; }

        [Display(Name = "NombreCotizacion")]
        public string NombreCotizacion { get; set; }

        [Display(Name = "PrecioUnitario")]
        public Double PrecioUnitario { get; set; }

        [Display(Name = "CantidadProductos")]
        public int CantidadProductos { get; set; }

        [Display(Name = "SubTotal")]
        public Double SubTotal { get; set; }

        [Display(Name = "TotalPesoChile")]
        public Double TotalPesoChile { get; set; }

        [Display(Name = "IVA")]
        public Double IVA { get; set; }

        public int productos_IdProductos { get; set; }
    }
}

