using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Usuarios
    {
        private UsuariosContext context;

        public String IdUsuarios { get; set; }

        public string NombreUsuario { get; set; }

        public string PasswordUsuario { get; set; }

        [Required(ErrorMessage = "Debé ingresar un Email")]
        [Display(Name = "EmailUsuario")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un formato de Email correcto")]
        public string EmailUsuario { get; set; }

        public string Telefono { get; set; }

        public String Perfiles_IdPerfiles { get; set; }

        public string NombrePerfil { get; set; }
    }
}
