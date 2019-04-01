using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class EntidadesContext
    {

        public string ConnectionString { get; set; }

        public EntidadesContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todas las entidades

        public List<Entidades> GetAllEntidades()
        {
            List<Entidades> list = new List<Entidades>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from entidades", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Entidades()
                        {
                            IdEntidades = Convert.ToInt32(reader["IdEntidades"]),
                            RazonSocial = reader["RazonSocial"].ToString(),
                            RutEntidad = reader["RutEntidad"].ToString(),
                            RepresentanteLegal = reader["RepresentanteLegal"].ToString(),
                            RutRepresentanteLegal = reader["RutRepresentanteLegal"].ToString(),
                            InicioActividad = Convert.ToDateTime(reader["InicioActividad"]),
                            InicioActividad2 = Convert.ToDateTime(reader["InicioActividad"]),
                            TipoEntidad = reader["TipoEntidad"].ToString(),
                            CondicionVenta = reader["CondicionVenta"].ToString(),
                            SegmentoVenta = reader["SegmentoVenta"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Localidad = reader["Localidad"].ToString(),
                            Domicilio = reader["Domicilio"].ToString(),
                        });
                    }
                }
            }

            return list;
        }

        //Insertar una Entidad en la base de datos

        public bool InsertarEntidad(Entidades model)
        {
            List<Entidades> list = new List<Entidades>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                String inicioActividad = model.InicioActividad.Year + "-" + model.InicioActividad.Month + "-" + model.InicioActividad.Day;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO entidades (`RazonSocial`, `RutEntidad`, `RepresentanteLegal`, `RutRepresentanteLegal`, `InicioActividad`, `TipoEntidad`, `CondicionVenta`, `SegmentoVenta`, `Pais`, `Ciudad`, `Localidad`, `Domicilio`) VALUES ('" + model.RazonSocial + "', '" + model.RutEntidad + "', '" + model.RepresentanteLegal + "', '" + model.RutRepresentanteLegal + "', '" + inicioActividad + "' , '" + model.TipoEntidad + "',  '" + model.CondicionVenta + "', '" + model.SegmentoVenta + "', '" + model.Pais + "', '" + model.Ciudad + "', '" + model.Localidad + "','" + model.Domicilio + "');", conn);
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

        public List<Entidades> BuscarUnaEntidad(int IdEntidades)
        {
            List<Entidades> list = new List<Entidades>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from entidades where IdEntidades='" + IdEntidades + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Entidades()
                        {
                            IdEntidades = Convert.ToInt32(reader["IdEntidades"]),
                            RazonSocial = reader["RazonSocial"].ToString(),
                            RutEntidad = reader["RutEntidad"].ToString(),
                            RepresentanteLegal = reader["RepresentanteLegal"].ToString(),
                            RutRepresentanteLegal = reader["RutRepresentanteLegal"].ToString(),
                            InicioActividad = Convert.ToDateTime(reader["InicioActividad"]),
                            InicioActividad2 = Convert.ToDateTime(reader["InicioActividad"]),
                            TipoEntidad = reader["TipoEntidad"].ToString(),
                            CondicionVenta = reader["CondicionVenta"].ToString(),
                            SegmentoVenta = reader["SegmentoVenta"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Localidad = reader["Localidad"].ToString(),
                            Domicilio = reader["Domicilio"].ToString(),
                        });
                    }
                }
            }

            return list;
        }



        public bool Modificar(Entidades model)
        {
            List<Entidades> list = new List<Entidades>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                String inicioActividad = model.InicioActividad.Year + "-" + model.InicioActividad.Month + "-" + model.InicioActividad.Day;
                MySqlCommand cmd = new MySqlCommand("UPDATE entidades SET `RazonSocial` = '" + model.RazonSocial + "', `RutEntidad` = '" + model.RutEntidad + "', `RepresentanteLegal` = '" + model.RepresentanteLegal + "', `RutRepresentanteLegal` = '" + model.RutRepresentanteLegal + "', `InicioActividad` = '" + inicioActividad + "', `TipoEntidad` = '" + model.TipoEntidad + "', `CondicionVenta` = '" + model.CondicionVenta + "', `SegmentoVenta` = '" + model.SegmentoVenta + "', `Pais` = '" + model.Pais + "', `Ciudad` = '" + model.Ciudad + "', `Localidad` = '" + model.Localidad + "', `Domicilio` = '" + model.Domicilio + "' WHERE `IdEntidades` = '" + model.IdEntidades + "'", conn);
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
        

              public List<Entidades> GetEntidadCreada(Entidades model)
        {
            List<Entidades> list = new List<Entidades>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                String inicioActividad = model.InicioActividad.Year + "-" + model.InicioActividad.Month + "-" + model.InicioActividad.Day;
                MySqlCommand cmd = new MySqlCommand("select * from entidades where `RazonSocial` = '" + model.RazonSocial + "' and `RutEntidad` = '" + model.RutEntidad + "' and `RepresentanteLegal` = '" + model.RepresentanteLegal + "' and `RutRepresentanteLegal` = '" + model.RutRepresentanteLegal + "' and `InicioActividad` = '" + inicioActividad + "' and `TipoEntidad` = '" + model.TipoEntidad + "'and `CondicionVenta` = '" + model.CondicionVenta + "' and `SegmentoVenta` = '" + model.SegmentoVenta + "' and `Pais` = '" + model.Pais + "' and `Ciudad` = '" + model.Ciudad + "' and `Localidad` = '" + model.Localidad + "' and `Domicilio` = '" + model.Domicilio + "' ", conn);



                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Entidades()
                        {
                            IdEntidades = Convert.ToInt32(reader["IdEntidades"]),
                            RazonSocial = reader["RazonSocial"].ToString(),
                            RutEntidad = reader["RutEntidad"].ToString(),
                            RepresentanteLegal = reader["RepresentanteLegal"].ToString(),
                            RutRepresentanteLegal = reader["RutRepresentanteLegal"].ToString(),
                            InicioActividad = Convert.ToDateTime(reader["InicioActividad"]),
                            InicioActividad2 = Convert.ToDateTime(reader["InicioActividad"]),
                            TipoEntidad = reader["TipoEntidad"].ToString(),
                            CondicionVenta = reader["CondicionVenta"].ToString(),
                            SegmentoVenta = reader["SegmentoVenta"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Localidad = reader["Localidad"].ToString(),
                            Domicilio = reader["Domicilio"].ToString(),
                        });
                    }
                }
            }

            return list;
        }


    }
}


