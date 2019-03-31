using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class PedidoEnsayoContext
    {
        public string ConnectionString { get; set; }

        public PedidoEnsayoContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //Obtener lista de todas las entidades

        public List<PedidoEnsayo> GetAllPedidoEnsayo()
        {
            List<PedidoEnsayo> list = new List<PedidoEnsayo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from pedidoEnsayo", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PedidoEnsayo()
                        {
                            IdPedidoEnsayo = Convert.ToInt32(reader["IdPedidoEnsayo"]),
                            ProtocoloAplicable = reader["ProtocoloAplicable"].ToString(),
                            CondicionesDeEnsayo = reader["CondicionesDeEnsayo"].ToString(),
                            AutorPedido = reader["AutorPedido"].ToString(),
                            NumeroSec = reader["NumeroSec"].ToString(),
                            Comentarios = reader["Comentarios"].ToString(),
                            presupuestos_IdPresupuestos = Convert.ToInt32(reader["presupuestos_IdPresupuestos"]),
                            StatusPedidoEnsayo = Convert.ToInt32(reader["StatusPedidoEnsayo"]),
                        });
                    }
                }
            }

            return list;
        }


        public bool InsertarPedidoEnsayo(PedidoEnsayo model)
        {
            List<PedidoEnsayo> list = new List<PedidoEnsayo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO pedidoEnsayo (`StatusPedidoEnsayo`, `presupuestos_IdPresupuestos`) VALUES ('" + model.PresupuestoLista[0].StatusPresupuesto + "','" + model.PresupuestoLista[0].IdPresupuestos + "');", conn);
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
        //Insertar un PedidoEnsayo en la base de datos

        //public bool InsertarPedidoEnsayo(PedidoEnsayo model)
        //{
        //    List<PedidoEnsayo> list = new List<PedidoEnsayo>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("INSERT INTO pedidoEnsayo (`ProtocoloAplicable`, `CondicionesDeEnsayo`, `AutorPedido`, `presupuestos_IdPresupuestos`) VALUES ('" + model.ProtocoloAplicable + "', '" + model.CondicionesDeEnsayo + "', '" + model.AutorPedido + "', '" + model.presupuestos_IdPresupuestos + "');", conn);
        //        int i = cmd.ExecuteNonQuery();
        //        conn.Close();
        //        if (i > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        public List<PedidoEnsayo> BuscarUnaMuestra(int IdPedidoEnsayo)
        {
            List<PedidoEnsayo> list = new List<PedidoEnsayo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from pedidoEnsayo where IdPedidoEnsayo='" + IdPedidoEnsayo + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PedidoEnsayo()
                        {
                            IdPedidoEnsayo = Convert.ToInt32(reader["IdPedidoEnsayo"]),
                            ProtocoloAplicable = reader["ProtocoloAplicable"].ToString(),
                            CondicionesDeEnsayo = reader["CondicionesDeEnsayo"].ToString(),
                            AutorPedido = reader["AutorPedido"].ToString(),
                            NumeroSec = reader["NumeroSec"].ToString(),
                            presupuestos_IdPresupuestos = Convert.ToInt32(reader["presupuestos_IdPresupuestos"]),
                            StatusPedidoEnsayo = Convert.ToInt32(reader["StatusPedidoEnsayo"]),
                        });
                    }
                }
            }

            return list;
        }

        public List<PedidoEnsayo> BuscarPedidoEnsayoPorIdPresupuesto(int presupuestos_IdPresupuestos)
        {
            List<PedidoEnsayo> list = new List<PedidoEnsayo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from pedidoEnsayo where presupuestos_IdPresupuestos='" + presupuestos_IdPresupuestos + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PedidoEnsayo()
                        {
                            IdPedidoEnsayo = Convert.ToInt32(reader["IdPedidoEnsayo"]),
                            ProtocoloAplicable = reader["ProtocoloAplicable"].ToString(),
                            CondicionesDeEnsayo = reader["CondicionesDeEnsayo"].ToString(),
                            AutorPedido = reader["AutorPedido"].ToString(),
                            NumeroSec = reader["NumeroSec"].ToString(),
                            Comentarios = reader["Comentarios"].ToString(),
                            presupuestos_IdPresupuestos = Convert.ToInt32(reader["presupuestos_IdPresupuestos"]),
                            StatusPedidoEnsayo = Convert.ToInt32(reader["StatusPedidoEnsayo"]),
                        });
                    }
                }
            }

            return list;
        }



        public List<PedidoEnsayo> BuscarPedidoEnsayoPorIdEnsayos(int IdPedidoEnsayo)
        {
            List<PedidoEnsayo> list = new List<PedidoEnsayo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from pedidoEnsayo where IdPedidoEnsayo='" + IdPedidoEnsayo + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PedidoEnsayo()
                        {
                            IdPedidoEnsayo = Convert.ToInt32(reader["IdPedidoEnsayo"]),
                            ProtocoloAplicable = reader["ProtocoloAplicable"].ToString(),
                            CondicionesDeEnsayo = reader["CondicionesDeEnsayo"].ToString(),
                            AutorPedido = reader["AutorPedido"].ToString(),
                            NumeroSec = reader["NumeroSec"].ToString(),
                            presupuestos_IdPresupuestos = Convert.ToInt32(reader["presupuestos_IdPresupuestos"]),
                            StatusPedidoEnsayo = Convert.ToInt32(reader["StatusPedidoEnsayo"]),
                            Comentarios = reader["Comentarios"].ToString(),

                        });
                    }
                }
            }

            return list;
        }


        public bool ModificarPedidoEnsayo(PedidoEnsayo model)
        {
            List<PedidoEnsayo> list = new List<PedidoEnsayo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE pedidoEnsayo SET `ProtocoloAplicable` = '" + model.ProtocoloAplicable + "', `CondicionesDeEnsayo` = '" + model.CondicionesDeEnsayo + "', `AutorPedido` = '" + model.AutorPedido + "', `NumeroSec` = '" + model.NumeroSec + "', `Comentarios` = '" + model.Comentarios + "',`presupuestos_IdPresupuestos` = '" + model.presupuestos_IdPresupuestos + "' WHERE `IdPedidoEnsayo` = '" + model.IdPedidoEnsayo + "'", conn);
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

        public bool PasarStatus(PedidoEnsayo model)
        {
            List<PedidoEnsayo> list = new List<PedidoEnsayo>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE pedidoEnsayo SET `StatusPedidoEnsayo` = '" + model.StatusPedidoEnsayo + "' WHERE `IdPedidoEnsayo` = '" + model.IdPedidoEnsayo + "'", conn);
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


