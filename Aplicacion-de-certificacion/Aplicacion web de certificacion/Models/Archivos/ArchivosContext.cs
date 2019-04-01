using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class ArchivosContext
    {
        public string ConnectionString { get; set; }

        public ArchivosContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todos los ensayos asignados

        public List<Archivos> GetArchivos(int IdEnsayos)
        {
            List<Archivos> list = new List<Archivos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from archivos where ensayos_IdEnsayos = '"+IdEnsayos+"'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Archivos()
                        {
                            idarchivos = Convert.ToInt32(reader["IdArchivos"]),
                            RutaArchivo = reader["RutaArchivos"].ToString(),
                            NombreArchivo = reader["NombreArchivos"].ToString(),                            
                            ensayo_IdEnsayo = Convert.ToInt32(reader["ensayos_IdEnsayos"]),


                        });
                    }
                }
            }

            return list;
        }


    }
}


