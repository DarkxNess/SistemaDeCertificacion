using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion_web_de_certificacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion_web_de_certificacion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        public IActionResult ListarUsers()
        {
            UsuariosContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

            return View(context.GetAllUsuarios());
        }

        public ActionResult NuevoUsuario()
        {

            try
            {
                PerfilesContext contextPerfiles = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PerfilesContext)) as PerfilesContext;
            List<Perfiles> Pefiles = contextPerfiles.GetAllPerfiles();
            if (Pefiles.Count == 0) {
                    Perfiles comercial = new Perfiles();
                    comercial.NombrePerfil = "Comercial";
                    contextPerfiles.AgregarPerfil(comercial);
                    Perfiles admin = new Perfiles();
                    admin.NombrePerfil = "Administrador";
                    contextPerfiles.AgregarPerfil(admin);
                    List<Perfiles> PefilesNuevo = contextPerfiles.GetAllPerfiles();
                    ViewBag.MiListado = PefilesNuevo;
                    Usuarios users = new Usuarios();
                    return View(users);
                }
            else
            {
                ViewBag.MiListado = Pefiles;
                Usuarios users = new Usuarios();
                return View(users);
            }
            }

            catch (Exception ex)
            {
                throw ex;

            }

        }

        [HttpPost]
        public ActionResult NuevoUser(Usuarios model)
        {
            try
            {
                Usuarios us = new Usuarios();
                us.NombreUsuario = model.NombreUsuario;
                us.PasswordUsuario = model.PasswordUsuario;
                us.EmailUsuario = model.EmailUsuario;
                us.Perfiles_IdPerfiles = model.Perfiles_IdPerfiles;

                UsuariosContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

                bool resultado = context.AgregarUser(us);

                if (resultado == true)
                {
                    return RedirectToAction("ListarUsers");
                }
                else
                {
                    return RedirectToAction("Fracaso");
                }

            }

            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ActionResult EditarUsuario(Usuarios model)
        {
            UsuariosContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;
            PerfilesContext contextPerfiles = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PerfilesContext)) as PerfilesContext;
            List<Perfiles> Pefiles = contextPerfiles.GetAllPerfiles();
            ViewBag.MiListado = Pefiles;
            List<Usuarios> listausers = context.BuscarUnUsuarioPorId(model.IdUsuarios);
            Usuarios usu = new Usuarios();
            usu.EmailUsuario = listausers[0].EmailUsuario;
            usu.IdUsuarios = listausers[0].IdUsuarios;
            usu.NombreUsuario = listausers[0].NombreUsuario;
            usu.PasswordUsuario = listausers[0].PasswordUsuario;


            return View(usu);

        }

        [HttpPost]
        public ActionResult ModificarUsuario(Usuarios model)
        {
            try
            {
                Usuarios us = new Usuarios();
                us.NombreUsuario = model.NombreUsuario;
                us.PasswordUsuario = model.PasswordUsuario;
                us.EmailUsuario = model.EmailUsuario;
                us.IdUsuarios = model.IdUsuarios;
                us.Perfiles_IdPerfiles = model.Perfiles_IdPerfiles;

                UsuariosContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;
                PerfilesContext contextPerfiles = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PerfilesContext)) as PerfilesContext;

                bool resultado = context.EliminarRolUsuarioPorIdUsuario(us);

                if (resultado == true)
                {
                    context.EliminarUsuarioPorID(us);
                    return RedirectToAction("ListarUsers");
                }
                else
                {
                    return RedirectToAction("Fracaso");
                }

            }

            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}