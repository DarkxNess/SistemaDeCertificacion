using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class LugarEnsayos
    {
        public List<Entidades> EntidadesLista { get; set; }

        public List<Contacto> ContactosLista { get; set; }

        public List<LugarEnsayos> LugarEnsayosLista { get; set; }

        private ContactoContext context;

        public int IdLugar_De_Ensayos { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string EntidadEncargada { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string Direccion { get; set; }


        [Required(ErrorMessage = "Debé ingresar un Email")]
        [Display(Name = "CorreoEncargado")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un formato de Email correcto")]
        public string CorreoEncargado { get; set; }

        [StringLength(160, MinimumLength = 3)]
        public string TelefonoEncargado { get; set; }

        public int Entidades_IdEntidades { get; set; }

    }
}
