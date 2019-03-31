using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Perfiles
    {
        private PerfilesContext context;

        public String IdPerfiles { get; set; }

        [Required(ErrorMessage = "Debé ingresar un Nombre de Perfil")]
        public string NombrePerfil { get; set; }
    }
}
