using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Ensayos
    {
        private EnsayosContext context;

        public List<Entidades> EntidadesLista { get; set; }

        public List<Archivos> ArchivosLista { get; set; }

        public List<Contacto> ContactosLista { get; set; }

        public List<LugarEnsayos> LugarEnsayosLista { get; set; }

        public List<Usuarios> UsuariosLista { get; set; }

        public List<Usuarios> JefesLista { get; set; }

        public List<Productos> ProductosLista { get; set; }

        public List<Cotizaciones> CotizacionesLista { get; set; }

        public List<Presupuesto> PresupuestoLista { get; set; }

        public List<PedidoEnsayo> PedidoEnsayoLista { get; set; }

        public List<Muestras> MuestrasLista { get; set; }

        public List<Aprobacion> AprobacionLista { get; set; }

        public List<Ensayos> EnsayosLista { get; set; }

        public int IdEnsayos { get; set; }

        [Required(ErrorMessage = "Debé ingresar un NombreEnsayo")]
        [Display(Name = "NombreEnsayo")]
        [StringLength(160, MinimumLength = 2)]
        public string NombreEnsayo { get; set; }

        [Required(ErrorMessage = "Debé ingresar un Cliente / En caso de que no exista entonces debe crear una nueva Entidad")]
        [Display(Name = "ClienteCertificadora")]
        [StringLength(160, MinimumLength = 4)]
        public string ClienteCertificadora { get; set; }

        [Display(Name = "Contacto")]
        [StringLength(160, MinimumLength = 3)]
        public string Contacto { get; set; }

        [Display(Name = "DescripcionEnsayo")]
        [StringLength(160, MinimumLength = 3)]
        public string DescripcionEnsayo { get; set; }

        [StringLength(160, MinimumLength = 8)]
        public string ClienteProducto { get; set; }

        [Display(Name = "Segmento")]
        public string Segmento { get; set; }


        public string tecnicoAsignado { get; set; }

        public string jefeLaboratorioAsignado { get; set; }
        
        [Display(Name = "FechaDeAlta")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaDeAlta { get; set; }

        //Copia de FechaCreacion, su finalidad es ordenar la fecha traída de la base de datos al formato usado por la región
        [Display(Name = "FechaDeAlta2")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaDeAlta2 { get; set; }

        [Display(Name = "FechaPedido")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaPedido { get; set; }

        //Copia de FechaCreacion, su finalidad es ordenar la fecha traída de la base de datos al formato usado por la región
        [Display(Name = "FechaPedido2")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaPedido2 { get; set; }

        public String StatusEnsayo { get; set; }

        public int idarchivos { get; set; }

        public string RutaArchivo { get; set; }

        public string NombreArchivo { get; set; }

        public int usuarios_IdUsuarios { get; set; }        

        public int pedidoEnsayo_IdPedidoEnsayo { get; set; }

    }
}

