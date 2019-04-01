using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Entidades
    {
        public List<Contacto> ContactosLista { get; set; }
        public List<Entidades> EntidadesLista { get; set; }
        public List<LugarEnsayos> LugarEnsayosLista { get; set; }

        private EntidadesContext context;

        public int IdEntidades { get; set; }

        [Display(Name = "RazonSocial")]
        [StringLength(160, MinimumLength = 3)]
        [Required(ErrorMessage = "Debé ingresar una Razón Social")]
        public string RazonSocial { get; set; }

        [Display(Name = "RutEntidad")]
        [StringLength(160, MinimumLength = 3)]
        public string RutEntidad { get; set; }

        [Display(Name = "RepresentanteLegal")]
        [StringLength(160, MinimumLength = 3)]
        public string RepresentanteLegal { get; set; }

        [StringLength(160, MinimumLength = 8)]
        public string RutRepresentanteLegal { get; set; }

        //Es la fecha en la que la entidad comenzo a trabajar con la empresa, o por defecto la fecha de creación del registro
        [Display(Name = "InicioActividad")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InicioActividad { get; set; }

        //Copia de InicioActividad, su finalidad es ordenar la fecha traída de la base de datos al formato usado por la región
        [Display(Name = "InicioActividad2")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime InicioActividad2 { get; set; }

        //El tipo entidad puede ser Cliente, Certificadora, Proveedor, etc. Se debé mostrar en un combobox.
        [StringLength(160, MinimumLength = 2)]
        public string TipoEntidad { get; set; }

        //Vendedor puede ser sin especificar hasta que se cree un presupuesto por un usuario de comercial y se designa automaticamente.

        //Es la cantidad de días que se dara al cliente para realizar los ensayos y realizar factura, a veces se paga 100% anticipado.
        [StringLength(160, MinimumLength = 3)]
        public string CondicionVenta { get; set; }

        //Solo puede tomar dos valores: Seguridad Electrica que es normalizado o sin normalizar que es para clientes que quieren realizar ciertos ensayos a sus productos pero sin buscar una certificación.
        [StringLength(160, MinimumLength = 3)]
        public string SegmentoVenta { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Pais { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Ciudad { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Localidad { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Domicilio { get; set; }
    }
}  

