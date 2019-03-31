using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class ProductosContext
    {
        public string ConnectionString { get; set; }

        public ProductosContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todas las entidades

        public List<Productos> GetAllProductos()
        {
            List<Productos> list = new List<Productos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from productos", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Productos()
                        {
                            IdProductos = Convert.ToInt32(reader["IdProductos"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            MarcaProducto = reader["MarcaProducto"].ToString(),
                            ModeloProducto = reader["ModeloProducto"].ToString(),
                            FamiliaProducto = reader["FamiliaProducto"].ToString(),
                            NumeroSerieProducto = reader["NumeroDeSerieProducto"].ToString(),
                            NormaProducto = reader["NormaProducto"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            NombreFabricante = reader["NombreFabricante"].ToString(),
                            DireccionFabricante = reader["DireccionFabricante"].ToString(),
                            presupuestos_IdPresupuestos = Convert.ToInt32(reader["presupuestos_IdPresupuestos"]),

                        });
                    }
                }
            }

            return list;
        }

        //Insertar un producto en la base de datos

        public bool InsertarProducto(Productos model)
        {
            List<Productos> list = new List<Productos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO productos (`NombreProducto`, `MarcaProducto`, `ModeloProducto`, `FamiliaProducto`, `NumeroDeSerieProducto`, `NormaProducto`, `Descripcion`, `NombreFabricante`, `DireccionFabricante`, `presupuestos_IdPresupuestos`) VALUES ('" + model.NombreProducto + "', '" + model.MarcaProducto + "', '" + model.ModeloProducto + "', '" + model.FamiliaProducto + "', '" + model.NumeroSerieProducto + "',  '" + model.NormaProducto + "', '" + model.Descripcion + "','" + model.NombreFabricante + "', '" + model.DireccionFabricante + "','" + model.presupuestos_IdPresupuestos + "');", conn);
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

        public List<Productos> BuscarUnProducto(int IdProductos)
        {
            List<Productos> list = new List<Productos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from productos where IdProductos='" + IdProductos + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Productos()
                        {
                            IdProductos = Convert.ToInt32(reader["IdProductos"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            MarcaProducto = reader["MarcaProducto"].ToString(),
                            ModeloProducto = reader["ModeloProducto"].ToString(),
                            FamiliaProducto = reader["FamiliaProducto"].ToString(),
                            NumeroSerieProducto = reader["NumeroDeSerieProducto"].ToString(),
                            NormaProducto = reader["NormaProducto"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            NombreFabricante = reader["NombreFabricante"].ToString(),
                            DireccionFabricante = reader["DireccionFabricante"].ToString(),
                            presupuestos_IdPresupuestos = Convert.ToInt32(reader["presupuestos_IdPresupuestos"]),
                        });
                    }
                }
            }

            return list;
        }

        public List<Productos> BuscarProductoPorIdPresupuesto(int presupuestos_IdPresupuestos)
        {
            List<Productos> list = new List<Productos>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from productos where presupuestos_IdPresupuestos='" + presupuestos_IdPresupuestos + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Productos()
                        {
                            IdProductos = Convert.ToInt32(reader["IdProductos"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            MarcaProducto = reader["MarcaProducto"].ToString(),
                            ModeloProducto = reader["ModeloProducto"].ToString(),
                            FamiliaProducto = reader["FamiliaProducto"].ToString(),
                            NumeroSerieProducto = reader["NumeroDeSerieProducto"].ToString(),
                            NormaProducto = reader["NormaProducto"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            NombreFabricante = reader["NombreFabricante"].ToString(),
                            DireccionFabricante = reader["DireccionFabricante"].ToString(),
                            presupuestos_IdPresupuestos = Convert.ToInt32(reader["presupuestos_IdPresupuestos"]),
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
                MySqlCommand cmd = new MySqlCommand("UPDATE productos SET `NombreProducto` = '" + model.NombreProducto + "', `MarcaProducto` = '" + model.MarcaProducto + "', `ModeloProducto` = '" + model.ModeloProducto + "', `FamiliaProducto` = '" + model.FamiliaProducto + "', `NumeroDeSerieProducto` = '" + model.NumeroSerieProducto + "', `NormaProducto` = '" + model.NormaProducto + "', `Descripcion` = '" + model.Descripcion + "', `NombreFabricante` = '" + model.NombreFabricante + "',`DireccionFabricante` = '" + model.DireccionFabricante + "' WHERE `IdProductos` = '" + model.IdProductos + "'", conn);
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


