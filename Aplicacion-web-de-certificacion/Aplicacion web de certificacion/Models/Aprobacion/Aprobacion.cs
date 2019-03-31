using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Aprobacion
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

        private AprobacionContext context;

        public int IdAprobacion { get; set; }

        //Es la fecha en la que se realizo la operacion
        [Display(Name = "FechaCreacionAprobacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaCreacionAprobacion { get; set; }

        //Copia de Fecha, su finalidad es ordenar la fecha traída de la base de datos al formato usado por la región
        [Display(Name = "FechaCreacionAprobacion2")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaCreacionAprobacion2 { get; set; }


        [StringLength(160, MinimumLength = 3)]
        public string UsuarioDesignadoAprobacion { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string ComentariosAprobacion { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string TipoAprobacion { get; set; }

        public string EstadoAprobacion { get; set; }

        public int PedidoEnsayo_IdPedidoEnsayo { get; set; }
    }
}
