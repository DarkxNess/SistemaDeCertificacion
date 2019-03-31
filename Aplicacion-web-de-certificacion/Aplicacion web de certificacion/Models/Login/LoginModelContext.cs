using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    public class LoginModelContext
    {
        public string ConnectionString { get; set; }

        public LoginModelContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //public List<LoginModel> GetAllUsers()
        //{
        //    List<LoginModel> list = new List<LoginModel>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("select u.IdUsuarios, u.UsernameUsuario, u.NombreUsuario, u.PasswordUsuario, u.EmailUsuario, u.Perfiles_IdPerfiles, p.NombrePerfil from Usuarios u inner join Perfiles p on u.Perfiles_IdPerfiles=p.IdPerfiles", conn);

        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                list.Add(new LoginModel()
        //                {
        //                    Iduser = Convert.ToInt32(reader["IdUsuarios"]),
        //                    Username = reader["UsernameUsuario"].ToString(),
        //                    Nombre = reader["NombreUsuario"].ToString(),
        //                    Email = reader["EmailUsuario"].ToString(),
        //                    Perfil = reader["NombrePerfil"].ToString(),
        //                    Idperfil = Convert.ToInt32(reader["Perfiles_IdPerfiles"]),

        //                });
        //            }
        //        }
        //    }

        //    return list;
        //}

        //public List<LoginModel> GetUser(string username, string password)
        //{
        //    List<LoginModel> list = new List<LoginModel>();

        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("select u.IdUsuarios, u.UsernameUsuario, u.NombreUsuario, u.PasswordUsuario, u.EmailUsuario, u.Perfiles_IdPerfiles, p.NombrePerfil from Usuarios u inner join Perfiles p on u.Perfiles_IdPerfiles=p.IdPerfiles where u.UsernameUsuario = '"+username.ToString()+"' and u.PasswordUsuario = '"+password.ToString()+"' ", conn);

        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                list.Add(new LoginModel()
        //                {
        //                    Iduser = Convert.ToInt32(reader["IdUsuarios"]),
        //                    Username = reader["UsernameUsuario"].ToString(),
        //                    Nombre = reader["NombreUsuario"].ToString(),
        //                    Email = reader["EmailUsuario"].ToString(),
        //                    Perfil = reader["NombrePerfil"].ToString(),
        //                    Idperfil = Convert.ToInt32(reader["Perfiles_IdPerfiles"]),

        //                });
        //            }
        //        }
        //    }

        //    return list;
        //}

    }
}
