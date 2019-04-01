using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class ListaPreciosContext
    {
        public string ConnectionString { get; set; }

        public ListaPreciosContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todas las entidades

        public List<ListaPrecios> GetAllListasPrecios()
        {
            List<ListaPrecios> list = new List<ListaPrecios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from listaprecios", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ListaPrecios()
                        {
                            IdListaPrecios = Convert.ToInt32(reader["IdListaPrecios"]),
                            NombreLista = reader["NombreLista"].ToString(),
                            PrecioUnitario = reader.GetDouble("PrecioUnitario"),

                        });
                    }
                }
            }

            return list;
        }

        //Insertar un producto en la base de datos

        public bool InsertarListaPrecios(Presupuesto model)
        {
            List<Productos> list = new List<Productos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO listaPrecios (`NombreLista`, `PrecioUnitario`) VALUES ('" + model.NombreCotizacion + "', '" + model.PrecioUnitario + "');", conn);
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

        public List<ListaPrecios> BuscarUnaLista(int IdListaPrecios)
        {
            List<ListaPrecios> list = new List<ListaPrecios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from listaPrecios where IdListaPrecios='" + IdListaPrecios + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ListaPrecios()
                        {
                            IdListaPrecios = Convert.ToInt32(reader["IdListaPrecios"]),
                            NombreLista = reader["NombreLista"].ToString(),
                            PrecioUnitario = reader.GetDouble("PrecioUnitario"),

                        });
                    }
                }
            }

            return list;
        }



        public bool ModificarProducto(Productos model)
        {
            List<Productos> list = new List<Productos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE productos SET `NombreProducto` = '" + model.NombreProducto + "', `MarcaProducto` = '" + model.MarcaProducto + "', `ModeloProducto` = '" + model.ModeloProducto + "', `FamiliaProducto` = '" + model.FamiliaProducto + "', `NumeroDeSerieProducto` = '" + model.NumeroSerieProducto + "' WHERE `IdProductos` = '" + model.IdProductos + "'", conn);
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

