using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class ListaPrecios
    {
        private ListaPreciosContext context;

        public List<Entidades> EntidadesLista { get; set; }

        public List<Contacto> ContactosLista { get; set; }

        public List<LugarEnsayos> LugarEnsayosLista { get; set; }

        public List<Usuarios> UsuariosLista { get; set; }

        public List<Productos> ProductosLista { get; set; }

        public List<Cotizaciones> CotizacionesLista { get; set; }

        public List<ListaPrecios> ListaPreciosLista { get; set; }

        public int IdListaPrecios { get; set; }

        [Display(Name = "NombreLista")]
        public string NombreLista { get; set; }

        [Display(Name = "PrecioUnitario")]
        public Double PrecioUnitario { get; set; }

        [Display(Name = "PrecioUF")]
        public Double PrecioUF { get; set; }
    }
}
