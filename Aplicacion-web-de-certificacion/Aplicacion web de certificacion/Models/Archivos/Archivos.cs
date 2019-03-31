using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class Archivos
    {
        private ArchivosContext context;

        public List<Ensayos> EnsayosLista { get; set; }          

        public int idarchivos { get; set; }

        public string RutaArchivo { get; set; }

        public string NombreArchivo { get; set; }    

        public int ensayo_IdEnsayo { get; set; }

    }
}

