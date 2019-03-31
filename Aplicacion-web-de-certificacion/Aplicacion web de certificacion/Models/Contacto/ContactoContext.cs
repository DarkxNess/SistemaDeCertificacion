using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class ContactoContext
    {
        public string ConnectionString { get; set; }

        public ContactoContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Contacto> BuscarContactos(int IdEntidades)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from contactoentidad c inner join entidades e on e.IdEntidades=c.Entidades_IdEntidades where e.IdEntidades = '" + IdEntidades + "'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Contacto()
                        {

                            IdContactoEntidad = Convert.ToInt32(reader["IdContactoEntidad"]),
                            Entidades_IdEntidades = Convert.ToInt32(reader["Entidades_IdEntidades"]),
                            ContactoRepresentanteLegal = reader["RepresentanteLegal"].ToString(),
                            ApellidoRepresentante = reader["ApellidoRepresentante"].ToString(),
                            TelefonoRepresentante = reader["TelefonoRepresentante"].ToString(),
                            EmailRepresentante = reader["EmailRepresentante"].ToString(),
                            ServicioTecnico = reader["ServicioTecnico"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                        });
                    }
                }
            }

            return list;
        }
        public List<Contacto> BuscarContactoPorIdEntidad(int IdEntidades)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from contactoentidad where Entidades_IdEntidades = '" + IdEntidades + "'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Contacto()
                        {
                            IdContactoEntidad = Convert.ToInt32(reader["IdContactoEntidad"]),
                            Entidades_IdEntidades = Convert.ToInt32(reader["Entidades_IdEntidades"]),
                            ContactoRepresentanteLegal = reader["RepresentanteLegal"].ToString(),
                            ApellidoRepresentante = reader["ApellidoRepresentante"].ToString(),
                            TelefonoRepresentante = reader["TelefonoRepresentante"].ToString(),
                            EmailRepresentante = reader["EmailRepresentante"].ToString(),
                            ServicioTecnico = reader["ServicioTecnico"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                        });
                    }
                }
            }

            return list;
        }

        public List<Contacto> BuscarUnContacto(int IdContactoEntidad)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from contactoentidad where IdContactoEntidad = '" + IdContactoEntidad + "'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Contacto()
                        {
                            IdContactoEntidad = Convert.ToInt32(reader["IdContactoEntidad"]),
                            Entidades_IdEntidades = Convert.ToInt32(reader["Entidades_IdEntidades"]),
                            ContactoRepresentanteLegal = reader["RepresentanteLegal"].ToString(),
                            ApellidoRepresentante = reader["ApellidoRepresentante"].ToString(),
                            TelefonoRepresentante = reader["TelefonoRepresentante"].ToString(),
                            EmailRepresentante = reader["EmailRepresentante"].ToString(),
                            ServicioTecnico = reader["ServicioTecnico"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                        });
                    }
                }
            }

            return list;
        }

        public bool ModificarContacto(Contacto model)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE `contactoentidad` SET `RepresentanteLegal` = '" + model.ContactoRepresentanteLegal + "', `ApellidoRepresentante` = '" + model.ApellidoRepresentante + "', `TelefonoRepresentante` = '" + model.TelefonoRepresentante + "', `EmailRepresentante` = '" + model.EmailRepresentante + "', `ServicioTecnico` = '" + model.ServicioTecnico + "', `Direccion` = '" + model.Direccion + "' WHERE `IdContactoEntidad` = '" + model.IdContactoEntidad + "' and `Entidades_IdEntidades` = '" + model.Entidades_IdEntidades + "';", conn);
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

        public bool InsertarContacto(Contacto model)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO contactoentidad (`RepresentanteLegal`, `ApellidoRepresentante`, `TelefonoRepresentante`, `EmailRepresentante`, `ServicioTecnico`, `Direccion`,`Entidades_IdEntidades`) VALUES ('" + model.ContactoRepresentanteLegal + "', '" + model.ApellidoRepresentante + "', '" + model.TelefonoRepresentante + "', '" + model.EmailRepresentante + "', '" + model.ServicioTecnico + "', '" + model.Direccion + "','" + model.Entidades_IdEntidades + "');", conn);
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

        public bool InsertarContactoDefault(Entidades model)
        {
            List<LugarEnsayos> list = new List<LugarEnsayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                String direccion = model.Domicilio + " " + model.Ciudad;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO contactoentidad (`RepresentanteLegal`, `ApellidoRepresentante`, `TelefonoRepresentante`, `EmailRepresentante`, `ServicioTecnico`, `Direccion`,`Entidades_IdEntidades`) VALUES (' ', ' ', ' ', ' ', ' ', ' ','" + model.IdEntidades + "');", conn);
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

