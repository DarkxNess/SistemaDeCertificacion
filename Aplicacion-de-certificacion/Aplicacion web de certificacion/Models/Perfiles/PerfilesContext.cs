using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class PerfilesContext
    {
        public string ConnectionString { get; set; }

        public PerfilesContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Perfiles> GetAllPerfiles()
        {
            List<Perfiles> list = new List<Perfiles>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from aspnetroles", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Perfiles()
                        {
                            IdPerfiles = reader["Id"].ToString(),
                            NombrePerfil = reader["Name"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        public bool AgregarPerfil(Perfiles model)
        {
            List<Perfiles> list = new List<Perfiles>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO perfiles (`NombrePerfil`) VALUES ('" + model.NombrePerfil + "');", conn);
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ModificarPerfilUsuario(Usuarios model)
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE aspnetuserroles SET `RoleId` = '" + model.Perfiles_IdPerfiles + "' where UserId ='" + model.IdUsuarios + "'", conn);
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
