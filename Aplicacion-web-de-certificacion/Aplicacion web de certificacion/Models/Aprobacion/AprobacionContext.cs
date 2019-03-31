using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class AprobacionContext
    {
        public string ConnectionString { get; set; }

        public AprobacionContext(string connectionString)
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
        public List<Aprobacion> BuscarAprobacionPorIdPedidoEnsayo(int PedidoEnsayo_IdPedidoEnsayo)
        {
            List<Aprobacion> list = new List<Aprobacion>();

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Aprobacion where PedidoEnsayo_IdPedidoEnsayo = '" + PedidoEnsayo_IdPedidoEnsayo + "' ORDER BY TipoAprobacion ASC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Aprobacion()
                        {
                            IdAprobacion = Convert.ToInt32(reader["IdAprobacion"]),
                            FechaCreacionAprobacion = Convert.ToDateTime(reader["FechaCreacionAprobacion"]),
                            FechaCreacionAprobacion2 = Convert.ToDateTime(reader["FechaCreacionAprobacion"]),
                            UsuarioDesignadoAprobacion = reader["UsuarioDesignadoAprobacion"].ToString(),
                            ComentariosAprobacion = reader["ComentariosAprobacion"].ToString(),
                            TipoAprobacion = reader["TipoAprobacion"].ToString(),
                            EstadoAprobacion = reader["EstadoAprobacion"].ToString(),
                            PedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["PedidoEnsayo_IdPedidoEnsayo"]),

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

        public bool ModificarAprobacion(Aprobacion model)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                DateTime fecha = DateTime.Now;
                String inicioActividad = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                MySqlCommand cmd = new MySqlCommand("UPDATE `Aprobacion` SET `FechaCreacionAprobacion` = '" + inicioActividad + "',`UsuarioDesignadoAprobacion` = '" + model.UsuarioDesignadoAprobacion + "', `EstadoAprobacion` = 'SI', `ComentariosAprobacion` = '" + model.ComentariosAprobacion + "' WHERE `IdAprobacion` = '" + model.IdAprobacion + "';", conn);
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

        public bool InsertarAprobacionDefault1(PedidoEnsayo model)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String inicioActividad = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Aprobacion (`FechaCreacionAprobacion`,`TipoAprobacion`, `EstadoAprobacion`, `PedidoEnsayo_IdPedidoEnsayo`) VALUES ('" + inicioActividad + "', 'Aprobación Solicitud', 'NO', '" + model.IdPedidoEnsayo + "');", conn);
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

        public bool InsertarAprobacionDefault2(PedidoEnsayo model)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String inicioActividad = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Aprobacion (`FechaCreacionAprobacion`,`TipoAprobacion`, `EstadoAprobacion`, `PedidoEnsayo_IdPedidoEnsayo`) VALUES ('" + inicioActividad + "', 'Evaluación', 'NO', '" + model.IdPedidoEnsayo + "');", conn);
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
        public bool InsertarAprobacionDefault3(PedidoEnsayo model)
        {
            List<Contacto> list = new List<Contacto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String inicioActividad = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Aprobacion (`FechaCreacionAprobacion`,`TipoAprobacion`, `EstadoAprobacion`, `PedidoEnsayo_IdPedidoEnsayo`) VALUES ('" + inicioActividad + "', 'Revisión y Decisión', 'NO', '" + model.IdPedidoEnsayo + "');", conn);
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

