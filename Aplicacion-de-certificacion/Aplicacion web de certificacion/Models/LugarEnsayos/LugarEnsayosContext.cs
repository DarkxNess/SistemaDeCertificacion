using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class LugarEnsayosContext
    {
        public string ConnectionString { get; set; }

        public LugarEnsayosContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<LugarEnsayos> BuscarLugarEnsayo(int IdEntidades)
        {
            List<LugarEnsayos> list = new List<LugarEnsayos>();

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from lugar_de_ensayos l inner join entidades e on e.IdEntidades=l.Entidades_IdEntidades where e.IdEntidades = '" + IdEntidades + "'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LugarEnsayos()
                        {
                            IdLugar_De_Ensayos = Convert.ToInt32(reader["IdLugar_De_Ensayos"]),
                            Entidades_IdEntidades = Convert.ToInt32(reader["Entidades_IdEntidades"]),
                            EntidadEncargada = reader["EntidadEncargada"].ToString(),
                            TelefonoEncargado = reader["TelefonoEncargado"].ToString(),
                            CorreoEncargado = reader["CorreoEncargado"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                        });
                    }
                }
            }

            return list;
        }

        public List<LugarEnsayos> BuscarUnLugarEnsayos(int IdLugar_De_Ensayos)
        {
            List<LugarEnsayos> list = new List<LugarEnsayos>();

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from lugar_de_ensayos where IdLugar_De_Ensayos = '" + IdLugar_De_Ensayos + "'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LugarEnsayos()
                        {
                            IdLugar_De_Ensayos = Convert.ToInt32(reader["IdLugar_De_Ensayos"]),
                            Entidades_IdEntidades = Convert.ToInt32(reader["Entidades_IdEntidades"]),
                            EntidadEncargada = reader["EntidadEncargada"].ToString(),
                            TelefonoEncargado = reader["TelefonoEncargado"].ToString(),
                            CorreoEncargado = reader["CorreoEncargado"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                        });
                    }
                }
            }

            return list;
        }

        public bool ModificarLugarEnsayos(LugarEnsayos model)
        {
            List<LugarEnsayos> list = new List<LugarEnsayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE `lugar_de_ensayos` SET `EntidadEncargada` = '"+model.EntidadEncargada+"', `Direccion` = '"+model.Direccion+"', `CorreoEncargado` = '"+model.CorreoEncargado+"', `TelefonoEncargado` = '"+model.TelefonoEncargado+"' WHERE `IdLugar_De_Ensayos` = '"+model.IdLugar_De_Ensayos+"' and `Entidades_IdEntidades` = '"+model.Entidades_IdEntidades+"'; ", conn);
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

        public bool InsertarLugarEnsayoDefault(Entidades model)
        {
            List<LugarEnsayos> list = new List<LugarEnsayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                String direccion = model.Domicilio +" "+model.Ciudad;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO lugar_de_ensayos (`EntidadEncargada`, `Direccion`, `CorreoEncargado`, `TelefonoEncargado`, `Entidades_IdEntidades`) VALUES (' ', '" + direccion + "', ' ', ' ', '" + model.IdEntidades + "');", conn);
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


