using System;
using System.Collections.Generic;
using Aplicacion_web_de_certificacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Aplicacion_web_de_certificacion.Controllers
{
    [Authorize]
    public class EntidadController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;

            return View(context.GetAllEntidades());
        }

        [Authorize]
        public ActionResult GetUsers(Entidades model)
        {
            EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            List<Entidades> lista = new List<Entidades>();
            lista = context.GetAllEntidades();
            return Json(new { data = lista });

        }

        //public JsonResult LlamarJson()
        //{
        //    EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;


        //    // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
        //    var output = context.GetAllEntidades();
        //   // return Json(output, JsonRequestBehavior.AllowGet);
        //    // return View(context.GetAllEntidades());
        //}
        [Authorize]
        public ActionResult GuardarDatos()
        {
            return View();

        }

        [Authorize]
        public ActionResult EditarEntidad(int IdEntidades)
        {

            ContactoContext contextContactos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            LugarEnsayosContext contextLugarEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.LugarEnsayosContext)) as LugarEnsayosContext;

            List<Entidades> Lista = context.BuscarUnaEntidad(IdEntidades);
            List<Contacto> ListaContactos = contextContactos.BuscarContactos(IdEntidades);
            List<LugarEnsayos> ListaLugarEnsayos = contextLugarEnsayos.BuscarLugarEnsayo(IdEntidades);
            Entidades ent = new Entidades();
            ent.ContactosLista = ListaContactos;
            ent.LugarEnsayosLista = ListaLugarEnsayos;
            ent.IdEntidades = Lista[0].IdEntidades;
            ent.Ciudad = Lista[0].Ciudad;
            ent.CondicionVenta = Lista[0].CondicionVenta;
            ent.InicioActividad = Lista[0].InicioActividad;
            ent.Localidad = Lista[0].Localidad;
            ent.Pais = Lista[0].Pais;
            ent.RazonSocial = Lista[0].RazonSocial;
            ent.RepresentanteLegal = Lista[0].RepresentanteLegal;
            ent.RutEntidad = Lista[0].RutEntidad;
            ent.RutRepresentanteLegal = Lista[0].RutRepresentanteLegal;
            ent.SegmentoVenta = Lista[0].SegmentoVenta;
            ent.TipoEntidad = Lista[0].TipoEntidad;
            ent.Domicilio = Lista[0].Domicilio;

            UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;
            List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuarios();
            Presupuesto prs = new Presupuesto();
            prs.UsuariosLista = ListarTodosUsuarios;
            ViewBag.MiListado = ListarTodosUsuarios;
            return View(ent);
        }

        //public ActionResult EditarContactos(int IdContactoEntidad, int Entidades_IdEntidades, String ContactoRepresentanteLegal, String ApellidoRepresentante, String TelefonoRepresentante, String EmailRepresentante, String ServicioTecnico, String Direccion)
        //{

        //    ContactosContext contextContactos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.Contactos.ContactosContext)) as ContactosContext;
        //    Contactos cnt = new Contactos();
        //    cnt.IdContactoEntidad = IdContactoEntidad;
        //    cnt.Entidades_IdEntidades = Entidades_IdEntidades;
        //    cnt.ContactoRepresentanteLegal = ContactoRepresentanteLegal;
        //    cnt.ApellidoRepresentante = ApellidoRepresentante;
        //    cnt.TelefonoRepresentante = TelefonoRepresentante;
        //    cnt.EmailRepresentante = EmailRepresentante;
        //    cnt.ServicioTecnico = ServicioTecnico;
        //    cnt.Direccion = Direccion;


        //    return View(cnt);
        //}

        [Authorize]
        [HttpPost]
        public ActionResult SaveRecord(Entidades model)
        {
            try
            {
                Entidades ent = new Entidades();
                ent.Ciudad = model.Ciudad;
                ent.CondicionVenta = model.CondicionVenta;
                ent.InicioActividad = model.InicioActividad;
                ent.Localidad = model.Localidad;
                ent.Pais = model.Pais;
                ent.RazonSocial = model.RazonSocial;
                ent.RepresentanteLegal = model.RepresentanteLegal;
                ent.RutEntidad = model.RutEntidad;
                ent.RutRepresentanteLegal = model.RutRepresentanteLegal;
                ent.SegmentoVenta = model.SegmentoVenta;
                ent.TipoEntidad = model.TipoEntidad;
                ent.Domicilio = model.Domicilio;

                EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;

               bool resultado = contextEntidades.InsertarEntidad(ent);

                if (resultado == true) {
                    Entidades entidadFinal = new Entidades();
                    List<Entidades> listafinal = contextEntidades.GetEntidadCreada(ent);
                    entidadFinal.Ciudad = listafinal[0].Ciudad;
                    entidadFinal.CondicionVenta = listafinal[0].CondicionVenta;
                    entidadFinal.InicioActividad = listafinal[0].InicioActividad;
                    entidadFinal.Localidad = listafinal[0].Localidad;
                    entidadFinal.Pais = listafinal[0].Pais;
                    entidadFinal.RazonSocial = listafinal[0].RazonSocial;
                    entidadFinal.RepresentanteLegal = listafinal[0].RepresentanteLegal;
                    entidadFinal.RutEntidad = listafinal[0].RutEntidad;
                    entidadFinal.RutRepresentanteLegal = listafinal[0].RutRepresentanteLegal;
                    entidadFinal.SegmentoVenta = listafinal[0].SegmentoVenta;
                    entidadFinal.TipoEntidad = listafinal[0].TipoEntidad;
                    entidadFinal.Domicilio = listafinal[0].Domicilio;
                    entidadFinal.IdEntidades = listafinal[0].IdEntidades;
                    LugarEnsayosContext contextLugarEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.LugarEnsayosContext)) as LugarEnsayosContext;
                    ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
                    contextLugarEnsayos.InsertarLugarEnsayoDefault(entidadFinal);
                    contextContacto.InsertarContactoDefault(entidadFinal);
                    return RedirectToAction("EditarEntidad", entidadFinal);
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

        public ActionResult Exito()
        {
            return View();

        }
        [Authorize]
        [HttpPost]
        public ActionResult Modificar(Entidades model, int IdEntidadesx)
        {
            try
            {
                Entidades ent = new Entidades();
                ent.IdEntidades = model.IdEntidades;
                ent.Ciudad = model.Ciudad;
                ent.CondicionVenta = model.CondicionVenta;
                ent.InicioActividad = model.InicioActividad;
                ent.Localidad = model.Localidad;
                ent.Pais = model.Pais;
                ent.RazonSocial = model.RazonSocial;
                ent.RepresentanteLegal = model.RepresentanteLegal;
                ent.RutEntidad = model.RutEntidad;
                ent.RutRepresentanteLegal = model.RutRepresentanteLegal;
                ent.SegmentoVenta = model.SegmentoVenta;
                ent.TipoEntidad = model.TipoEntidad;
                ent.Domicilio = model.Domicilio;

                EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;

                bool resultado = context.Modificar(ent);

                if (resultado == true)
                {
                   
                        Entidades entidadFinal = new Entidades();
                        List<Entidades> listafinal = context.GetEntidadCreada(ent);
                        entidadFinal.Ciudad = listafinal[0].Ciudad;
                        entidadFinal.CondicionVenta = listafinal[0].CondicionVenta;
                        entidadFinal.InicioActividad = listafinal[0].InicioActividad;
                        entidadFinal.Localidad = listafinal[0].Localidad;
                        entidadFinal.Pais = listafinal[0].Pais;
                        entidadFinal.RazonSocial = listafinal[0].RazonSocial;
                        entidadFinal.RepresentanteLegal = listafinal[0].RepresentanteLegal;
                        entidadFinal.RutEntidad = listafinal[0].RutEntidad;
                        entidadFinal.RutRepresentanteLegal = listafinal[0].RutRepresentanteLegal;
                        entidadFinal.SegmentoVenta = listafinal[0].SegmentoVenta;
                        entidadFinal.TipoEntidad = listafinal[0].TipoEntidad;
                        entidadFinal.Domicilio = listafinal[0].Domicilio;
                        entidadFinal.IdEntidades = listafinal[0].IdEntidades;
                        return RedirectToAction("EditarEntidad", entidadFinal);
                 
   
            
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

        //Sección para editar los contactos de la empresa
        [Authorize]
        //Buscar todos los contactos dependiendo de la id de la entidad
        public ActionResult EditarContactos(int id)
        {

            ContactoContext contextContactos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            List<Contacto> lista = new List<Contacto>();
            Contacto contact = new Contacto();
            contact.ContactosLista = contextContactos.BuscarUnContacto(id);
            Contacto cnt = new Contacto();
            cnt.ApellidoRepresentante = contact.ContactosLista[0].ApellidoRepresentante;
            cnt.ContactoRepresentanteLegal = contact.ContactosLista[0].ContactoRepresentanteLegal;
            cnt.Entidades_IdEntidades = contact.ContactosLista[0].Entidades_IdEntidades;
            cnt.IdContactoEntidad = contact.ContactosLista[0].IdContactoEntidad;
            cnt.Direccion = contact.ContactosLista[0].Direccion;
            cnt.EmailRepresentante = contact.ContactosLista[0].EmailRepresentante;
            cnt.ServicioTecnico = contact.ContactosLista[0].ServicioTecnico;
            cnt.TelefonoRepresentante = contact.ContactosLista[0].TelefonoRepresentante;
            return View(cnt);
        }


        //Modificar el contacto seleccionado a traves de la ventana modal, guardarlo y redirigir a la pantalla en la que se encontraba
        [HttpPost]
        public ActionResult ModificarContacto(Contacto model, int IdEntidadesx)
        {
            try
            {
                ContactoContext contextContactos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
                Contacto cnt = new Contacto();
                cnt.ApellidoRepresentante = model.ApellidoRepresentante;
                cnt.ContactoRepresentanteLegal = model.ContactoRepresentanteLegal;
                cnt.Entidades_IdEntidades = model.Entidades_IdEntidades;
                cnt.IdContactoEntidad = model.IdContactoEntidad;
                cnt.Direccion = model.Direccion;
                cnt.EmailRepresentante = model.EmailRepresentante;
                cnt.ServicioTecnico = model.ServicioTecnico;
                cnt.TelefonoRepresentante = model.TelefonoRepresentante;
                bool resultado = contextContactos.ModificarContacto(cnt);

                if (resultado == true)
                {
                    EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
                    List<Entidades> Lista = context.BuscarUnaEntidad(cnt.Entidades_IdEntidades);
                    Entidades ent = new Entidades();
                    List<Contacto> lista = new List<Contacto>();
                    Contacto contact = new Contacto();
                    contact.EntidadesLista = context.BuscarUnaEntidad(cnt.Entidades_IdEntidades);
                    

                    ent.IdEntidades = contact.EntidadesLista[0].IdEntidades;
                    ent.Ciudad = contact.EntidadesLista[0].Ciudad;
                    ent.CondicionVenta = contact.EntidadesLista[0].CondicionVenta;
                    ent.InicioActividad = contact.EntidadesLista[0].InicioActividad;
                    ent.Localidad = contact.EntidadesLista[0].Localidad;
                    ent.Pais = contact.EntidadesLista[0].Pais;
                    ent.RazonSocial = contact.EntidadesLista[0].RazonSocial;
                    ent.RepresentanteLegal = contact.EntidadesLista[0].RepresentanteLegal;
                    ent.RutEntidad = contact.EntidadesLista[0].RutEntidad;
                    ent.RutRepresentanteLegal = contact.EntidadesLista[0].RutRepresentanteLegal;
                    ent.SegmentoVenta = contact.EntidadesLista[0].SegmentoVenta;
                    ent.TipoEntidad = contact.EntidadesLista[0].TipoEntidad;
                    ent.Domicilio = contact.EntidadesLista[0].Domicilio;
                    return RedirectToAction("EditarEntidad",ent);
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

        //Controlador que redirigira a una nueva vista que tendra los contactos
        public ActionResult EditarEntidad2(Entidades model)
        {

            ContactoContext contextContactos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            List<Entidades> Lista = context.BuscarUnaEntidad(model.IdEntidades);
            List<Contacto> ListaContactos = contextContactos.BuscarContactos(model.IdEntidades);
            Entidades ent = new Entidades();
            ent.ContactosLista = ListaContactos;
            ent.IdEntidades = model.IdEntidades;
            ent.Ciudad = model.Ciudad;
            ent.CondicionVenta = model.CondicionVenta;

            ent.InicioActividad = model.InicioActividad;
            ent.Localidad = model.Localidad;
            ent.Pais = model.Pais;
            ent.RazonSocial = model.RazonSocial;
            ent.RepresentanteLegal = model.RepresentanteLegal;
            ent.RutEntidad = model.RutEntidad;
            ent.RutRepresentanteLegal = model.RutRepresentanteLegal;
            ent.SegmentoVenta = model.SegmentoVenta;
            ent.TipoEntidad = model.TipoEntidad;
            ent.Domicilio = model.Domicilio;
            return View(ent);
        }


        //Sección para editar los lugares de ensayos

        public ActionResult EditarLugarEnsayo(int id)
        {

            LugarEnsayosContext contextLugarEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.LugarEnsayosContext)) as LugarEnsayosContext;
            List<LugarEnsayos> lista = new List<LugarEnsayos>();
            LugarEnsayos ensy = new LugarEnsayos();
            ensy.LugarEnsayosLista = contextLugarEnsayos.BuscarUnLugarEnsayos(id);
            LugarEnsayos ens = new LugarEnsayos();
            ens.CorreoEncargado = ensy.LugarEnsayosLista[0].CorreoEncargado;
            ens.Direccion = ensy.LugarEnsayosLista[0].Direccion;
            ens.EntidadEncargada = ensy.LugarEnsayosLista[0].EntidadEncargada;
            ens.TelefonoEncargado = ensy.LugarEnsayosLista[0].TelefonoEncargado;
            ens.Entidades_IdEntidades = ensy.LugarEnsayosLista[0].Entidades_IdEntidades;
            ens.IdLugar_De_Ensayos = ensy.LugarEnsayosLista[0].IdLugar_De_Ensayos;
            return View(ens);
        }

        [HttpPost]
        public ActionResult ModificarLugarEnsayo(LugarEnsayos model, int IdEntidadesx)
        {
            try
            {
                LugarEnsayosContext contextLugarEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.LugarEnsayosContext)) as LugarEnsayosContext;
                LugarEnsayos ensy = new LugarEnsayos();
                ensy.CorreoEncargado = model.CorreoEncargado;
                ensy.Direccion = model.Direccion;
                ensy.EntidadEncargada = model.EntidadEncargada;
                ensy.TelefonoEncargado = model.TelefonoEncargado;
                ensy.Entidades_IdEntidades = model.Entidades_IdEntidades;
                ensy.IdLugar_De_Ensayos = model.IdLugar_De_Ensayos;
                bool resultado = contextLugarEnsayos.ModificarLugarEnsayos(ensy);

                if (resultado == true)
                {
                    EntidadesContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
                    List<Entidades> Lista = context.BuscarUnaEntidad(ensy.Entidades_IdEntidades);
                    Entidades ent = new Entidades();
                    List<LugarEnsayos> lista = new List<LugarEnsayos>();
                    LugarEnsayos ensayos = new LugarEnsayos();
                    ensayos.EntidadesLista = context.BuscarUnaEntidad(ensy.Entidades_IdEntidades);


                    ent.IdEntidades = ensayos.EntidadesLista[0].IdEntidades;
                    ent.Ciudad = ensayos.EntidadesLista[0].Ciudad;
                    ent.CondicionVenta = ensayos.EntidadesLista[0].CondicionVenta;
                    ent.InicioActividad = ensayos.EntidadesLista[0].InicioActividad;
                    ent.Localidad = ensayos.EntidadesLista[0].Localidad;
                    ent.Pais = ensayos.EntidadesLista[0].Pais;
                    ent.RazonSocial = ensayos.EntidadesLista[0].RazonSocial;
                    ent.RepresentanteLegal = ensayos.EntidadesLista[0].RepresentanteLegal;
                    ent.RutEntidad = ensayos.EntidadesLista[0].RutEntidad;
                    ent.RutRepresentanteLegal = ensayos.EntidadesLista[0].RutRepresentanteLegal;
                    ent.SegmentoVenta = ensayos.EntidadesLista[0].SegmentoVenta;
                    ent.TipoEntidad = ensayos.EntidadesLista[0].TipoEntidad;
                    ent.Domicilio = ensayos.EntidadesLista[0].Domicilio;
                    return RedirectToAction("EditarEntidad", ent);
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

        //Crear contacto
        public ActionResult CrearContacto(int id)
        {
            Contacto c = new Contacto();
            c.Entidades_IdEntidades = id;
            return View(c);
        }

        [HttpPost]
        public ActionResult GuardarContacto(Contacto model)
        {
            try
            {
                ContactoContext contextContactos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
                Contacto cnt = new Contacto();
                cnt.ContactoRepresentanteLegal = model.ContactoRepresentanteLegal;
                cnt.ApellidoRepresentante = model.ApellidoRepresentante;
                cnt.TelefonoRepresentante = model.TelefonoRepresentante;
                cnt.EmailRepresentante = model.EmailRepresentante;
                cnt.ServicioTecnico = model.ServicioTecnico;
                cnt.Direccion = model.Direccion;
                cnt.Entidades_IdEntidades = model.Entidades_IdEntidades;
                bool resultado = contextContactos.InsertarContacto(cnt);

                if (resultado == true)
                {
                    EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
                    List<Entidades> ListaEntidades = contextEntidades.BuscarUnaEntidad(cnt.Entidades_IdEntidades);
                    Entidades ent = new Entidades();
                    ent.IdEntidades = ListaEntidades[0].IdEntidades;
                    ent.RazonSocial = ListaEntidades[0].RazonSocial;
                    ent.RutEntidad = ListaEntidades[0].RutEntidad;
                    ent.RepresentanteLegal = ListaEntidades[0].RepresentanteLegal;
                    ent.RutRepresentanteLegal = ListaEntidades[0].RutRepresentanteLegal;
                    ent.InicioActividad = ListaEntidades[0].InicioActividad;
                    ent.TipoEntidad = ListaEntidades[0].TipoEntidad;
                    ent.CondicionVenta = ListaEntidades[0].CondicionVenta;
                    ent.SegmentoVenta = ListaEntidades[0].SegmentoVenta;
                    ent.Pais = ListaEntidades[0].Pais;
                    ent.Ciudad = ListaEntidades[0].Ciudad;
                    ent.Localidad = ListaEntidades[0].Localidad;
                    ent.Domicilio = ListaEntidades[0].Domicilio;
                    return RedirectToAction("EditarEntidad", ent);
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