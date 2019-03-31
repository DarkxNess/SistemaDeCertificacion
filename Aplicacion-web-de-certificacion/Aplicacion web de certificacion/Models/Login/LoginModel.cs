using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class LoginModel
    {
        
        //public int Iduser { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //public string Nombre { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        //public int Idperfil { get; set; }

        //[DataType(DataType.Text)]
        //public string Perfil { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
