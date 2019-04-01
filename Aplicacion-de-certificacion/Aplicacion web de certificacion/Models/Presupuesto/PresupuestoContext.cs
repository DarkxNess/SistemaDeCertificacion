using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class PresupuestoContext
    {
        public string ConnectionString { get; set; }

        public PresupuestoContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todas las entidades

        public List<Presupuesto> GetAllPresupuestos()
               {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from presupuestos order by StatusPresupuesto asc", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Presupuesto()
                        {
                            IdPresupuestos = Convert.ToInt32(reader["IdPresupuestos"]),
                            entidades_IdEntidades = Convert.ToInt32(reader["entidades_IdEntidades"]),
                            Cliente = reader["Cliente"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            SegmentoVenta = reader["SegmentoVenta"].ToString(),
                            CondicionVenta = reader["CondicionVenta"].ToString(),
                            ComercialAsignado = reader["ComercialAsignado"].ToString(),
                            ClienteAsociado = reader["ClienteAsociado"].ToString(),
                            ContactoAsociado = reader["ContactoAsociado"].ToString(),
                            PaisFacturacion = reader["PaisFacturacion"].ToString(),
                            ClienteFacturacionPais = reader["ClienteFacturacionPais"].ToString(),
                            ContactoClienteFacturacionPais = reader["ContactoClienteFacturacionPais"].ToString(),
                            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                            FechaCreacion2 = Convert.ToDateTime(reader["FechaCreacion"]),
                            StatusPresupuesto = Convert.ToInt32(reader["StatusPresupuesto"]),
                        });
                    }
                }
            }

            return list;
        }

        //Insertar una Entidad en la base de datos

        public bool InsertarPresupuesto(Presupuesto model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                String FechaCreacion = model.FechaCreacion.Year + "-" + model.FechaCreacion.Month + "-" + model.FechaCreacion.Day;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO presupuestos (`Cliente`, `Contacto`, `SegmentoVenta`, `CondicionVenta`, `ComercialAsignado`, `ClienteAsociado`, `ContactoAsociado`, `PaisFacturacion`, `ClienteFacturacionPais`, `ContactoClienteFacturacionPais`, `FechaCreacion`, `StatusPresupuesto`, `entidades_IdEntidades`) VALUES ('" + model.Cliente + "', '" + model.Contacto + "', '" + model.SegmentoVenta + "', '" + model.CondicionVenta + "', '" + model.ComercialAsignado + "', '" + model.ClienteAsociado + "', '" + model.ContactoAsociado + "', '" + model.PaisFacturacion + "', '" + model.ClienteFacturacionPais + "', '" + model.ContactoClienteFacturacionPais + "', '" + FechaCreacion + "', '" + model.StatusPresupuesto + "', '" + model.entidades_IdEntidades + "');", conn);
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

        public List<Presupuesto> BuscarUnPresupuesto(int IdPresupuestos)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from presupuestos where IdPresupuestos='" + IdPresupuestos + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Presupuesto()
                        {
                            IdPresupuestos = Convert.ToInt32(reader["IdPresupuestos"]),
                            entidades_IdEntidades = Convert.ToInt32(reader["entidades_IdEntidades"]),
                            Cliente = reader["Cliente"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            SegmentoVenta = reader["SegmentoVenta"].ToString(),
                            CondicionVenta = reader["CondicionVenta"].ToString(),
                            ComercialAsignado = reader["ComercialAsignado"].ToString(),
                            ClienteAsociado = reader["ClienteAsociado"].ToString(),
                            ContactoAsociado = reader["ContactoAsociado"].ToString(),
                            PaisFacturacion = reader["PaisFacturacion"].ToString(),
                            ClienteFacturacionPais = reader["ClienteFacturacionPais"].ToString(),
                            ContactoClienteFacturacionPais = reader["ContactoClienteFacturacionPais"].ToString(),
                            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                            FechaCreacion2 = Convert.ToDateTime(reader["FechaCreacion"]),
                            StatusPresupuesto = Convert.ToInt32(reader["StatusPresupuesto"]),

                        });
                    }
                }
            }

            return list;
        }



        public bool ModificarPresupuesto(Presupuesto model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                String FechaCreacion = model.FechaCreacion.Year + "-" + model.FechaCreacion.Month + "-" + model.FechaCreacion.Day;
                MySqlCommand cmd = new MySqlCommand("UPDATE presupuestos SET `Cliente` = '" + model.Cliente + "', `Contacto` = '" + model.Contacto + "', `SegmentoVenta` = '" + model.SegmentoVenta + "', `CondicionVenta` = '" + model.CondicionVenta + "', `ComercialAsignado` = '" + model.ComercialAsignado + "', `ClienteAsociado` = '" + model.ClienteAsociado + "', `ContactoAsociado` = '" + model.ContactoAsociado + "', `PaisFacturacion` = '" + model.PaisFacturacion + "', `ClienteFacturacionPais` = '" + model.ClienteFacturacionPais + "', `ContactoClienteFacturacionPais` = '" + model.ContactoClienteFacturacionPais + "', `FechaCreacion` = '" + FechaCreacion + "' WHERE `IdPresupuestos` = '" + model.IdPresupuestos + "'", conn);
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


        public List<Presupuesto> GetPresupuestoCreado(Presupuesto model)
        {
            List<Presupuesto> list = new List<Presupuesto>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                String inicioActividad = model.FechaCreacion.Year + "-" + model.FechaCreacion.Month + "-" + model.FechaCreacion.Day;

                MySqlCommand cmd = new MySqlCommand("select * from presupuestos where `Cliente` = '" + model.Cliente + "' and `Contacto` = '" + model.Contacto + "' and `SegmentoVenta` = '" + model.SegmentoVenta + "' and `CondicionVenta` = '" + model.CondicionVenta + "' and `ComercialAsignado` = '" + model.ComercialAsignado + "' and `ClienteAsociado` = '" + model.ClienteAsociado + "' and `ContactoAsociado` = '" + model.ContactoAsociado + "' and `PaisFacturacion` = '" + model.PaisFacturacion + "' and `ClienteFacturacionPais` = '" + model.ClienteFacturacionPais + "' and `ContactoClienteFacturacionPais` = '" + model.ContactoClienteFacturacionPais + "' and `FechaCreacion` = '" + inicioActividad + "'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Presupuesto()
                        {
                            IdPresupuestos = Convert.ToInt32(reader["IdPresupuestos"]),
                            entidades_IdEntidades = Convert.ToInt32(reader["entidades_IdEntidades"]),
                            Cliente = reader["Cliente"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            SegmentoVenta = reader["SegmentoVenta"].ToString(),
                            CondicionVenta = reader["CondicionVenta"].ToString(),
                            ComercialAsignado = reader["ComercialAsignado"].ToString(),
                            ClienteAsociado = reader["ClienteAsociado"].ToString(),
                            ContactoAsociado = reader["ContactoAsociado"].ToString(),
                            PaisFacturacion = reader["PaisFacturacion"].ToString(),
                            ClienteFacturacionPais = reader["ClienteFacturacionPais"].ToString(),
                            ContactoClienteFacturacionPais = reader["ContactoClienteFacturacionPais"].ToString(),
                            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                            FechaCreacion2 = Convert.ToDateTime(reader["FechaCreacion"]),
                            StatusPresupuesto = Convert.ToInt32(reader["StatusPresupuesto"]),
                        });
                    }
                }
            }

            return list;
        }


    }
}


