using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class CotizacionesContext
    {
        public string ConnectionString { get; set; }

        public CotizacionesContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todas las entidades

        public List<Cotizaciones> GetAllCotizaciones()
        {
            List<Cotizaciones> list = new List<Cotizaciones>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from cotizaciones", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Cotizaciones()
                        {
                            IdCotizacion = Convert.ToInt32(reader["IdCotizacion"]),
                            NombreCotizacion = reader["NombreCotizacion"].ToString(),
                            PrecioUnitario = reader.GetDouble("PrecioUnitario"),
                            CantidadProductos = Convert.ToInt32(reader["CantidadProductos"]),
                            SubTotal = reader.GetDouble("SubTotal"),
                            productos_IdProductos = Convert.ToInt32(reader["productos_IdProductos"]),
                            TotalPesoChile = reader.GetDouble("TotalPesoChile"),
                            IVA = reader.GetDouble("IVA"),
                        });
                    }
                }
            }

            return list;
        }

        //Insertar una cotizacion en la base de datos

        public bool InsertarCotizaciones(Cotizaciones model)
        {
            List<Cotizaciones> list = new List<Cotizaciones>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO cotizaciones (`NombreCotizacion`, `PrecioUnitario`, `CantidadProductos`, `SubTotal`, `TotalPesoChile`, `IVA`,`productos_IdProductos`) VALUES ('" + model.NombreCotizacion + "', '" + model.PrecioUnitario + "', '" + model.CantidadProductos + "', '" + model.SubTotal + "', '" + model.TotalPesoChile + "', '" + model.IVA + "', '" + model.productos_IdProductos + "');", conn);
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

        public List<Cotizaciones> BuscarUnaCotizacion(int IdProducto)
        {
            List<Cotizaciones> list = new List<Cotizaciones>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from cotizaciones where productos_IdProductos='" + IdProducto + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Cotizaciones()
                        {
                            IdCotizacion = Convert.ToInt32(reader["IdCotizacion"]),
                            NombreCotizacion = reader["NombreCotizacion"].ToString(),
                            PrecioUnitario = reader.GetDouble("PrecioUnitario"),
                            CantidadProductos = Convert.ToInt32(reader["CantidadProductos"]),
                            SubTotal = reader.GetDouble("SubTotal"),
                            productos_IdProductos = Convert.ToInt32(reader["productos_IdProductos"]),
                            TotalPesoChile = reader.GetDouble("TotalPesoChile"),
                            IVA = reader.GetDouble("IVA"),

                        });
                    }
                }
            }

            return list;
        }



        public bool ModificarCotizacion(Cotizaciones model)
        {
            List<Cotizaciones> list = new List<Cotizaciones>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE cotizaciones SET `NombreCotizacion` = '" + model.NombreCotizacion + "', `PrecioUnitario` = '" + model.PrecioUnitario + "', `CantidadProductos` = '" + model.CantidadProductos + "', `SubTotal` = '" + model.SubTotal + "', `TotalPesoChile` = '" + model.TotalPesoChile + "', `IVA` = '" + model.IVA + "' WHERE `IdCotizacion` = '" + model.IdCotizacion + "'", conn);
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

