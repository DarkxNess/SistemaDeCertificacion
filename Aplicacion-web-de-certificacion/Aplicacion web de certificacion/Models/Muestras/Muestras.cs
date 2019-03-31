using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Muestras
    {
        private MuestrasContext context;

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

        public int IdMuestras { get; set; }

        [Display(Name = "FechaIngreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaIngreso { get; set; }

        [Display(Name = "FechaIngreso2")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaIngreso2 { get; set; }

        [Display(Name = "Etiqueta")]
        public int Etiqueta { get; set; }

        [Display(Name = "Producto")]
        [StringLength(160, MinimumLength = 3)]
        public string Producto { get; set; }

        public int Cantidad { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Fabricante { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string PaisOrigen { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string DestinoMuestras { get; set; }

        public int pedidoEnsayo_IdPedidoEnsayo { get; set; }

    }
}

