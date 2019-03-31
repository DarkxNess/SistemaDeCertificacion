using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class EnsayosContext
    {
        public string ConnectionString { get; set; }

        public EnsayosContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todos los ensayos asignados

        public List<Ensayos> GetAllEnsayos()
        {
            List<Ensayos> list = new List<Ensayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from ensayos where tecnicoAsignado != '' order by FechaDeAlta asc", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Ensayos()
                        {
                            IdEnsayos = Convert.ToInt32(reader["IdEnsayos"]),
                            NombreEnsayo = reader["NombreEnsayo"].ToString(),
                            ClienteCertificadora = reader["ClienteCertificadora"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            DescripcionEnsayo = reader["DescripcionEnsayo"].ToString(),
                            ClienteProducto = reader["ClienteProducto"].ToString(),
                            Segmento = reader["Segmento"].ToString(),
                            tecnicoAsignado = reader["tecnicoAsignado"].ToString(),
                            jefeLaboratorioAsignado = reader["jefeLaboratorioAsignado"].ToString(),
                            FechaDeAlta = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaDeAlta2 = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                            FechaPedido2 = Convert.ToDateTime(reader["FechaPedido"]),
                            StatusEnsayo = reader["StatusEnsayo"].ToString(),
                            pedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["pedidoEnsayo_IdPedidoEnsayo"]),


                        });
                    }
                }
            }

            return list;
        }

        //Obtener lista de todos los ensayos NO asignados

        public List<Ensayos> GetNoAsigEnsayos()
        {
            List<Ensayos> list = new List<Ensayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from ensayos where tecnicoAsignado = '' order by FechaDeAlta asc", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Ensayos()
                        {
                            IdEnsayos = Convert.ToInt32(reader["IdEnsayos"]),
                            NombreEnsayo = reader["NombreEnsayo"].ToString(),
                            ClienteCertificadora = reader["ClienteCertificadora"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            DescripcionEnsayo = reader["DescripcionEnsayo"].ToString(),
                            ClienteProducto = reader["ClienteProducto"].ToString(),
                            Segmento = reader["Segmento"].ToString(),
                            tecnicoAsignado = "NINGUNO",
                            jefeLaboratorioAsignado = reader["jefeLaboratorioAsignado"].ToString(),
                            FechaDeAlta = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaDeAlta2 = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                            FechaPedido2 = Convert.ToDateTime(reader["FechaPedido"]),
                            StatusEnsayo = reader["StatusEnsayo"].ToString(),
                            pedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["pedidoEnsayo_IdPedidoEnsayo"]),


                        });
                    }
                }
            }

            return list;
        }

        //Insertar una Entidad en la base de datos

        public bool InsertarNuevoEnsayoConIdPedidoEnsayos(Ensayos model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String FechaCreacion = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO ensayos (`FechaDeAlta`,`ClienteCertificadora`, `Contacto`, `FechaPedido`,`ClienteProducto`, `Segmento`, `StatusEnsayo`, `pedidoEnsayo_IdPedidoEnsayo`) VALUES ('" + FechaCreacion + "','" + model.ClienteCertificadora + "', '" + model.Contacto + "', '" + FechaCreacion + "', '" + model.ClienteProducto + "', '" + model.Segmento + "', '" + model.StatusEnsayo + "', '" + model.pedidoEnsayo_IdPedidoEnsayo + "');", conn);
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

        public List<Ensayos> BuscarEnsayoPorIdEnsayo(int IdEnsayos)
        {
            List<Ensayos> list = new List<Ensayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from ensayos where IdEnsayos='" + IdEnsayos + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Ensayos()
                        {
                            IdEnsayos = Convert.ToInt32(reader["IdEnsayos"]),
                            NombreEnsayo = reader["NombreEnsayo"].ToString(),
                            ClienteCertificadora = reader["ClienteCertificadora"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            DescripcionEnsayo = reader["DescripcionEnsayo"].ToString(),
                            ClienteProducto = reader["ClienteProducto"].ToString(),
                            Segmento = reader["Segmento"].ToString(),
                            tecnicoAsignado = reader["tecnicoAsignado"].ToString(),
                            jefeLaboratorioAsignado = reader["jefeLaboratorioAsignado"].ToString(),
                            FechaDeAlta = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaDeAlta2 = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                            FechaPedido2 = Convert.ToDateTime(reader["FechaPedido"]),
                            StatusEnsayo = reader["StatusEnsayo"].ToString(),
                            pedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["pedidoEnsayo_IdPedidoEnsayo"]),

                        });
                    }
                }
            }

            return list;
        }

        public List<Ensayos> BuscarEnsayoPorIdPedidoDeEnsayo(int pedidoEnsayo_IdPedidoEnsayo)
        {
            List<Ensayos> list = new List<Ensayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from ensayos where pedidoEnsayo_IdPedidoEnsayo='" + pedidoEnsayo_IdPedidoEnsayo + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Ensayos()
                        {
                            IdEnsayos = Convert.ToInt32(reader["IdEnsayos"]),
                            NombreEnsayo = reader["NombreEnsayo"].ToString(),
                            ClienteCertificadora = reader["ClienteCertificadora"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            DescripcionEnsayo = reader["DescripcionEnsayo"].ToString(),
                            ClienteProducto = reader["ClienteProducto"].ToString(),
                            Segmento = reader["Segmento"].ToString(),
                            tecnicoAsignado = reader["tecnicoAsignado"].ToString(),
                            jefeLaboratorioAsignado = reader["jefeLaboratorioAsignado"].ToString(),
                            FechaDeAlta = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaDeAlta2 = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                            FechaPedido2 = Convert.ToDateTime(reader["FechaPedido"]),
                            StatusEnsayo = reader["StatusEnsayo"].ToString(),
                            pedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["pedidoEnsayo_IdPedidoEnsayo"]),

                        });
                    }
                }
            }

            return list;
        }


        public bool ModificarEnsayo(Ensayos model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String FechaCreacion = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                String fechapedido = model.FechaPedido.Year + "-" + model.FechaPedido.Month + "-" + model.FechaPedido.Day;
                if (model.RutaArchivo.ToString() != null && model.RutaArchivo.ToString() != "" && model.NombreArchivo.ToString() != "" && model.NombreArchivo.ToString() != null)
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE ensayos SET `NombreEnsayo` = '" + model.NombreEnsayo + "', `Contacto` = '" + model.Contacto + "', `FechaDeAlta` = '" + FechaCreacion + "', `ClienteCertificadora` = '" + model.ClienteCertificadora + "', `Contacto` = '" + model.Contacto + "', `FechaPedido` = '" + fechapedido + "', `DescripcionEnsayo` = '" + model.DescripcionEnsayo + "', `ClienteProducto` = '" + model.ClienteProducto + "', `Segmento` = '" + model.Segmento + "', `tecnicoAsignado` = '" + model.tecnicoAsignado + "', `jefeLaboratorioAsignado` = '" + model.jefeLaboratorioAsignado + "' WHERE `IdEnsayos` = '" + model.IdEnsayos + "'; INSERT INTO archivos (RutaArchivos, NombreArchivos, ensayos_IdEnsayos) VALUES ('"+model.RutaArchivo+"', '"+model.NombreArchivo+"', '"+model.IdEnsayos+"')", conn);
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
                else
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE ensayos SET `NombreEnsayo` = '" + model.NombreEnsayo + "', `Contacto` = '" + model.Contacto + "', `FechaDeAlta` = '" + FechaCreacion + "', `ClienteCertificadora` = '" + model.ClienteCertificadora + "', `Contacto` = '" + model.Contacto + "', `FechaPedido` = '" + fechapedido + "', `DescripcionEnsayo` = '" + model.DescripcionEnsayo + "', `ClienteProducto` = '" + model.ClienteProducto + "', `Segmento` = '" + model.Segmento + "', `tecnicoAsignado` = '" + model.tecnicoAsignado + "', `jefeLaboratorioAsignado` = '" + model.jefeLaboratorioAsignado + "' WHERE `IdEnsayos` = '" + model.IdEnsayos + "'", conn);
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

        public bool AprobarEnsayo(Ensayos model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String FechaCreacion = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                String fechapedido = model.FechaPedido.Year + "-" + model.FechaPedido.Month + "-" + model.FechaPedido.Day;
                MySqlCommand cmd = new MySqlCommand("UPDATE ensayos SET `StatusEnsayo` = 'Aprobado' WHERE `IdEnsayos` = '" + model.IdEnsayos + "'", conn);
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

        public bool RechazarEnsayo(Ensayos model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String FechaCreacion = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                String fechapedido = model.FechaPedido.Year + "-" + model.FechaPedido.Month + "-" + model.FechaPedido.Day;
                MySqlCommand cmd = new MySqlCommand("UPDATE ensayos SET `StatusEnsayo` = 'Aprobado' WHERE `IdEnsayos` = '" + model.IdEnsayos + "'", conn);
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

        public bool PasarStatus(Presupuesto model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE presupuestos SET `StatusPresupuesto` = '" + model.StatusPresupuesto + "' WHERE `IdPresupuestos` = '" + model.IdPresupuestos + "'", conn);
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


        public List<Ensayos> GetEnsayoCreado(Ensayos model)
        {
            List<Ensayos> list = new List<Ensayos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from ensayos where `ClienteCertificadora` = '" + model.ClienteCertificadora + "' and `ClienteProducto` = '" + model.ClienteProducto + "' and `Contacto` = '" + model.Contacto + "' and `Segmento` = '" + model.Segmento + "' and `StatusEnsayo` = '" + model.StatusEnsayo + "' and `pedidoEnsayo_IdPedidoEnsayo` = '" + model.pedidoEnsayo_IdPedidoEnsayo + "'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Ensayos()
                        {
                            IdEnsayos = Convert.ToInt32(reader["IdEnsayos"]),
                            NombreEnsayo = reader["NombreEnsayo"].ToString(),
                            ClienteCertificadora = reader["ClienteCertificadora"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            DescripcionEnsayo = reader["DescripcionEnsayo"].ToString(),
                            ClienteProducto = reader["ClienteProducto"].ToString(),
                            Segmento = reader["Segmento"].ToString(),
                            tecnicoAsignado = reader["tecnicoAsignado"].ToString(),
                            jefeLaboratorioAsignado = reader["jefeLaboratorioAsignado"].ToString(),
                            FechaDeAlta = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaDeAlta2 = Convert.ToDateTime(reader["FechaDeAlta"]),
                            FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                            FechaPedido2 = Convert.ToDateTime(reader["FechaPedido"]),
                            StatusEnsayo = reader["StatusEnsayo"].ToString(),
                            pedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["pedidoEnsayo_IdPedidoEnsayo"]),
                        });
                    }
                }
            }

            return list;
        }


    }
}


