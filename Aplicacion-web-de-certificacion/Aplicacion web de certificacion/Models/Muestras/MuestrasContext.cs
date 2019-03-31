using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class MuestrasContext
    {
        public string ConnectionString { get; set; }

        public MuestrasContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todas las entidades
        public bool ModificarMuestra(Muestras model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String FechaCreacion = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                MySqlCommand cmd = new MySqlCommand("UPDATE muestras SET `Etiqueta` = '" + model.Etiqueta + "', `Producto` = '" + model.Producto + "', `Cantidad` = '" + model.Cantidad + "', `Marca` = '" + model.Marca + "', `Modelo` = '" + model.Modelo + "', `Fabricante` = '" + model.Fabricante + "', `PaisOrigen` = '" + model.PaisOrigen + "', `DestinoMuestras` = '" + model.DestinoMuestras + "' WHERE `IdMuestras` = '" + model.IdMuestras + "'", conn);
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

        public List<Muestras> GetAllMuestras()
        {
            List<Muestras> list = new List<Muestras>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from muestras", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Muestras()
                        {
                            IdMuestras = Convert.ToInt32(reader["IdMuestras"]),
                            Producto = reader["Producto"].ToString(),
                            FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]),
                            FechaIngreso2 = Convert.ToDateTime(reader["FechaIngreso"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Marca = reader["Marca"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            Fabricante = reader["Fabricante"].ToString(),
                            PaisOrigen = reader["PaisOrigen"].ToString(),
                            DestinoMuestras = reader["DestinoMuestras"].ToString(),
                            Etiqueta = Convert.ToInt32(reader["Etiqueta"]),
                            
                            pedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["pedidoEnsayo_IdPedidoEnsayo"]),

                        });
                    }
                }
            }

            return list;
        }

        public bool InsertarMuestra(Muestras model)
        {
            List<Muestras> list = new List<Muestras>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                DateTime fecha = DateTime.Now;
                String FechaCreacion = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO muestras (`FechaIngreso`, `Etiqueta`, `Producto`, `Cantidad`, `Marca`, `Modelo`, `Fabricante`, `PaisOrigen`, `DestinoMuestras`, `pedidoEnsayo_IdPedidoEnsayo`) VALUES ('"+FechaCreacion+"', '" + model.Etiqueta + "', '" + model.Producto + "', '" + model.Cantidad + "', '" + model.Marca + "', '" + model.Modelo + "',  '" + model.Fabricante + "', '" + model.PaisOrigen + "', '"+model.DestinoMuestras+"', '" + model.pedidoEnsayo_IdPedidoEnsayo + "');", conn);
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

        public List<Muestras> BuscarUnaMuestra(int pedidoEnsayo_IdPedidoEnsayo)
        {
            List<Muestras> list = new List<Muestras>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from muestras where pedidoEnsayo_IdPedidoEnsayo='" + pedidoEnsayo_IdPedidoEnsayo + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Muestras()
                        {
                            IdMuestras = Convert.ToInt32(reader["IdMuestras"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Marca = reader["Marca"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]),
                            FechaIngreso2 = Convert.ToDateTime(reader["FechaIngreso"]),
                            Etiqueta = Convert.ToInt32(reader["Etiqueta"]),
                            PaisOrigen = reader["PaisOrigen"].ToString(),
                            DestinoMuestras = reader["DestinoMuestras"].ToString(),
                            Fabricante = reader["Fabricante"].ToString(),
                            Producto = reader["Producto"].ToString(),
                            pedidoEnsayo_IdPedidoEnsayo = Convert.ToInt32(reader["pedidoEnsayo_IdPedidoEnsayo"]),
                        });
                    }
                }
            }

            return list;
        }

    }
}

