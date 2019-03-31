using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Productos
    {
        public List<Contacto> ContactosLista { get; set; }
        public List<Entidades> EntidadesLista { get; set; }
        public List<LugarEnsayos> LugarEnsayosLista { get; set; }
        public List<Productos> ProductosLista { get; set; }

        private ProductosContext context;

        public int IdProductos { get; set; }

        [Display(Name = "NombreProducto")]
        [StringLength(160, MinimumLength = 3)]
        [Required(ErrorMessage = "Debé ingresar un Nombre de Producto")]
        public string NombreProducto { get; set; }

        [Display(Name = "MarcaProducto")]
        [StringLength(160, MinimumLength = 3)]
        public string MarcaProducto { get; set; }

        [Display(Name = "ModeloProducto")]
        [StringLength(160, MinimumLength = 3)]
        public string ModeloProducto { get; set; }

        public string FamiliaProducto { get; set; }

        public string NormaProducto { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string NumeroSerieProducto { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Descripcion { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string NombreFabricante { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string DireccionFabricante { get; set; }

        public int presupuestos_IdPresupuestos { get; set; }
       
    }
}