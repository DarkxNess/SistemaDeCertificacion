using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Presupuesto
    {
        private PresupuestoContext context;

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

        public int IdPresupuestos { get; set; }

        [Required(ErrorMessage = "Debé ingresar un Cliente / En caso de que no exista entonces debe crear una nueva Entidad")]
        [Display(Name = "DropdownCliente")]
        public string DropdownCliente { get; set; }

        [Required(ErrorMessage = "Debé ingresar un Cliente Asociado / En caso de que no exista entonces debe crear una nueva Entidad")]
        [Display(Name = "DropdownClienteAsociado")]
        public string DropdownClienteAsociado { get; set; }
        
        [Required(ErrorMessage = "Debé ingresar un Cliente / En caso de que no exista entonces debe crear una nueva Entidad")]
        [Display(Name = "Cliente")]
        [StringLength(160, MinimumLength = 4)]
        public string Cliente { get; set; }

        [Required(ErrorMessage = "Debé ingresar un contacto para el Cliente Asociado / En caso de que no exista entonces debe crear una nueva Entidad")]
        [Display(Name = "ListarContactosAsociados")]
        public string ListarContactosAsociados { get; set; }

        [Required(ErrorMessage = "Debé ingresar Usuario de comercial")]
        [Display(Name = "DropDownComercialAsignado")]
        public string DropDownComercialAsignado { get; set; }

        [Display(Name = "Contacto")]
        [StringLength(160, MinimumLength = 3)]
        public string Contacto { get; set; }

        [Display(Name = "SegmentoVenta")]
        [StringLength(160, MinimumLength = 3)]
        public string SegmentoVenta { get; set; }

        [StringLength(160, MinimumLength = 8)]
        public string CondicionVenta { get; set; }

        [Display(Name = "ComercialAsignado")]
        public string ComercialAsignado { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string ClienteAsociado { get; set; }

        [Required(ErrorMessage = "Debé ingresar un Contacto Asociado / En caso de que no exista entonces debe crear una nueva Entidad")]
        [StringLength(160, MinimumLength = 3)]
        public string ContactoAsociado { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string PaisFacturacion { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string ClienteFacturacionPais { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string ContactoClienteFacturacionPais { get; set; }

        [Display(Name = "FechaCreacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaCreacion { get; set; }

        //Copia de FechaCreacion, su finalidad es ordenar la fecha traída de la base de datos al formato usado por la región
        [Display(Name = "FechaCreacion2")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaCreacion2 { get; set; }

        public int StatusPresupuesto { get; set; }

        public String Usuarios_IdUsuarios { get; set; }

        public int entidades_IdEntidades { get; set; }

        public Double PrecioUnitario { get; set; }

        public String NombreCotizacion { get; set; }

    }
}

