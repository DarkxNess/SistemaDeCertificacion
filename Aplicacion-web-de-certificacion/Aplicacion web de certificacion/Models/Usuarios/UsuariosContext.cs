using Microsoft.AspNetCore.Authorization;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models
{
    [Authorize(Roles = "Admin")]
    public class UsuariosContext
    {
        public string ConnectionString { get; set; }

        public UsuariosContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Usuarios> GetAllUsuarios2()
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("select u.IdUsuarios, u.NombreUsuario, u.PasswordUsuario, u.EmailUsuario, u.Perfiles_IdPerfiles, p.NombrePerfil from Usuarios u inner join Perfiles p on u.Perfiles_IdPerfiles=p.IdPerfiles", conn);
                MySqlCommand cmd = new MySqlCommand("select u.IdUsuarios, u.NombreUsuario, u.PasswordUsuario, u.EmailUsuario, u.Perfiles_IdPerfiles, p.NombrePerfil from Usuarios u inner join Perfiles p on u.Perfiles_IdPerfiles=p.IdPerfiles", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Usuarios()
                        {
                            IdUsuarios = reader["Id"].ToString(),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            EmailUsuario = reader["EmailUsuario"].ToString(),
                            NombrePerfil = reader["NombrePerfil"].ToString(),
                            Perfiles_IdPerfiles = reader["NombrePerfil"].ToString(),

                        });
                    }
                }
            }
            return list;
        }
        public List<Usuarios> GetAllUsuarios()
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("select u.IdUsuarios, u.NombreUsuario, u.PasswordUsuario, u.EmailUsuario, u.Perfiles_IdPerfiles, p.NombrePerfil from Usuarios u inner join Perfiles p on u.Perfiles_IdPerfiles=p.IdPerfiles", conn);
                MySqlCommand cmd = new MySqlCommand("select u.Id, u.UserName, u.Email, u.PhoneNumber, rol.Name from Aspnetusers u inner join AspNetUserRoles p on u.Id=p.UserId inner join AspNetRoles rol on p.RoleId=rol.Id", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Usuarios()
                        {
                            IdUsuarios = reader["Id"].ToString(),
                            NombreUsuario = reader["UserName"].ToString(),
                            EmailUsuario = reader["Email"].ToString(),
                            Telefono = reader["PhoneNumber"].ToString(),
                            NombrePerfil = reader["Name"].ToString(),


                        });
                    }
                }
            }
            return list;
        }

        public List<Usuarios> GetAllUsuariosTecnicos()
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("select u.IdUsuarios, u.NombreUsuario, u.PasswordUsuario, u.EmailUsuario, u.Perfiles_IdPerfiles, p.NombrePerfil from Usuarios u inner join Perfiles p on u.Perfiles_IdPerfiles=p.IdPerfiles", conn);
                MySqlCommand cmd = new MySqlCommand("select u.Id, u.UserName, u.Email, u.PhoneNumber, rol.Name from Aspnetusers u inner join AspNetUserRoles p on u.Id=p.UserId inner join AspNetRoles rol on p.RoleId=rol.Id where rol.Name='Tecnico'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Usuarios()
                        {
                            IdUsuarios = reader["Id"].ToString(),
                            NombreUsuario = reader["UserName"].ToString(),
                            EmailUsuario = reader["Email"].ToString(),
                            Telefono = reader["PhoneNumber"].ToString(),
                            NombrePerfil = reader["Name"].ToString(),
                            

                        });
                    }
                }
            }
            return list;
        }

        public List<Usuarios> GetJefesLab()
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("select u.IdUsuarios, u.NombreUsuario, u.PasswordUsuario, u.EmailUsuario, u.Perfiles_IdPerfiles, p.NombrePerfil from Usuarios u inner join Perfiles p on u.Perfiles_IdPerfiles=p.IdPerfiles", conn);
                MySqlCommand cmd = new MySqlCommand("select u.Id, u.UserName, u.Email, u.PhoneNumber, rol.Name from Aspnetusers u inner join AspNetUserRoles p on u.Id=p.UserId inner join AspNetRoles rol on p.RoleId=rol.Id where rol.Name='JefeLab'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Usuarios()
                        {
                            IdUsuarios = reader["Id"].ToString(),
                            NombreUsuario = reader["UserName"].ToString(),
                            EmailUsuario = reader["Email"].ToString(),
                            Telefono = reader["PhoneNumber"].ToString(),
                            NombrePerfil = reader["Name"].ToString(),


                        });
                    }
                }
            }
            return list;
        }
        public List<Usuarios> BuscarUnUsuarioPorId(String IdUsuarios)
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from aspnetusers where Id='"+IdUsuarios+"' ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Usuarios()
                        {
                            IdUsuarios = reader["Id"].ToString(),
                            EmailUsuario = reader["Email"].ToString(),
                            PasswordUsuario = reader["PasswordHash"].ToString(),
                            NombreUsuario = reader["UserName"].ToString(),


                        });
                    }
                }
            }

            return list;
        }

        public bool EliminarRolUsuarioPorIdUsuario(Usuarios model)
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `aspnetuserroles` WHERE `UserId` = '"+model.IdUsuarios+"';", conn);
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

        public bool EliminarUsuarioPorID(Usuarios model)
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `aspnetusers` WHERE `Id` = '" + model.IdUsuarios + "';", conn);
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

        public bool AgregarUser(Usuarios model)
        {
            List<Usuarios> list = new List<Usuarios>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO usuarios (`NombreUsuario`, `PasswordUsuario`, `EmailUsuario`, `perfiles_IdPerfiles`) VALUES ('" + model.NombreUsuario + "', '" + model.PasswordUsuario + "', '" + model.EmailUsuario + "', '" + model.Perfiles_IdPerfiles + "');", conn);
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
