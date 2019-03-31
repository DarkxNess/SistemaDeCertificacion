using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Contacto
    {
        public List<Entidades> EntidadesLista { get; set; }

        public List<Contacto> ContactosLista { get; set; }

        private ContactoContext context;

        public int IdContactoEntidad { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string ContactoRepresentanteLegal { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string ApellidoRepresentante { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string TelefonoRepresentante { get; set; }

        [Required(ErrorMessage = "Debé ingresar un Email")]
        [Display(Name = "EmailRepresentante")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un formato de Email correcto")]
        public string EmailRepresentante { get; set; }

        [StringLength(160, MinimumLength = 1)]
        public string ServicioTecnico { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Direccion { get; set; }

        public int Entidades_IdEntidades { get; set; }
    }
}

