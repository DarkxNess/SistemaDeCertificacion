using System;
using System.Collections.Generic;
using Aplicacion_web_de_certificacion.Data;
using Aplicacion_web_de_certificacion.Models;
using Aplicacion_web_de_certificacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aplicacion_web_de_certificacion.Controllers
{
    [Authorize]
    public class PresupuestoController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public PresupuestoController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        public ActionResult Index()
        {
            PresupuestoContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;

            return View(context.GetAllPresupuestos());
        }

        //Get
        public IActionResult CrearPresupuesto()
        {
            PresupuestoContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;
            List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuarios();
            Presupuesto prs = new Presupuesto();
            prs.UsuariosLista = ListarTodosUsuarios;
            ViewBag.MiListado = ListarTodosUsuarios;

            EntidadesContext EntidadesContext = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            List<Entidades> ListarEntidades = EntidadesContext.GetAllEntidades();
            prs.EntidadesLista = ListarEntidades;
            ViewBag.ListadoEntidad = ListarEntidades;
            return View();
        }

        //Post
        public JsonResult getContactsList(int IdEntidades)
        {
            ContactoContext EntidadesContext = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            List<Contacto> ListarContactos = EntidadesContext.BuscarContactoPorIdEntidad(IdEntidades);
            Entidades en = new Entidades();
            en.ContactosLista = ListarContactos;
            return Json(ListarContactos);
        }
        //Falta generar automaticamente los campos clienteasociado y clientefacturacion a traves de dropdownlist y que traigan una id, para ello hay que crear idclienteasociado en la clase de presupuesto y tambien la idclientefabricacion
        [HttpPost]
        public ActionResult GuardarPresupuesto(Presupuesto model)
        {
            try
            {
                PresupuestoContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

 
                List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuarios();
                Presupuesto prs = new Presupuesto();
                prs.UsuariosLista = ListarTodosUsuarios;
                prs.IdPresupuestos = model.IdPresupuestos;
                prs.Cliente = model.Cliente;
                prs.CondicionVenta = model.CondicionVenta;
                prs.Contacto = model.Contacto;
                prs.FechaCreacion = model.FechaCreacion;
                prs.ComercialAsignado = _userManager.GetUserName(User);
                prs.ClienteAsociado = model.ClienteAsociado;
                prs.ContactoAsociado = model.ContactoAsociado;
                prs.PaisFacturacion = model.PaisFacturacion;
                prs.SegmentoVenta = model.SegmentoVenta;
                prs.ClienteFacturacionPais = model.ClienteFacturacionPais;
                prs.ContactoClienteFacturacionPais = model.ContactoClienteFacturacionPais;
                prs.StatusPresupuesto = 1;
                prs.entidades_IdEntidades = model.entidades_IdEntidades;

                bool resultado = context.InsertarPresupuesto(prs);
                List<Presupuesto> listaConId = context.GetPresupuestoCreado(prs);
                prs.IdPresupuestos = listaConId[0].IdPresupuestos;
                if (resultado == true)
                {
                    return RedirectToAction("EditarPresupuesto", prs);
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

        [HttpPost]
        public ActionResult UpdatePresupuesto(Presupuesto model)
        {
            try
            {
                PresupuestoContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

                Presupuesto prs = new Presupuesto();
                prs.IdPresupuestos = model.IdPresupuestos;
                prs.Cliente = model.Cliente;
                prs.CondicionVenta = model.CondicionVenta;
                prs.Contacto = model.Contacto;
                prs.FechaCreacion = model.FechaCreacion;
                prs.ComercialAsignado = _userManager.GetUserName(User);
                prs.ClienteAsociado = model.ClienteAsociado;
                prs.ContactoAsociado = model.ContactoAsociado;
                prs.PaisFacturacion = model.PaisFacturacion;
                prs.SegmentoVenta = model.SegmentoVenta;
                prs.ClienteFacturacionPais = model.ClienteFacturacionPais;
                prs.ContactoClienteFacturacionPais = model.ContactoClienteFacturacionPais;
                prs.StatusPresupuesto = model.StatusPresupuesto;
                prs.entidades_IdEntidades = model.entidades_IdEntidades;

                bool resultado = context.ModificarPresupuesto(prs);
                List<Presupuesto> listaConId = context.GetPresupuestoCreado(prs);
                prs.IdPresupuestos = listaConId[0].IdPresupuestos;
                if (resultado == true)
                {
                    return RedirectToAction("EditarPresupuesto", prs);
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

        public ActionResult EditarPresupuesto(int IdPresupuestos)
        {

            PresupuestoContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;
            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;

            List<Productos> ListaProductos = contextProductos.BuscarProductoPorIdPresupuesto(IdPresupuestos);
            List<Presupuesto> Lista = context.BuscarUnPresupuesto(IdPresupuestos);
            Presupuesto prs = new Presupuesto();
            List<Cotizaciones> ListarCotizaciones = new List<Cotizaciones>();
            List<Cotizaciones> ListaFinal = new List<Cotizaciones>();


            List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuarios();
            Presupuesto presu = new Presupuesto();
            presu.UsuariosLista = ListarTodosUsuarios;
            ViewBag.MiListado = ListarTodosUsuarios;

            EntidadesContext EntidadesContext = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            List<Entidades> ListarEntidades = EntidadesContext.GetAllEntidades();
            prs.EntidadesLista = ListarEntidades;
            ViewBag.ListadoEntidad = ListarEntidades;


            List<Usuarios> ListarTodosUsuariosx = contextUsuarios.GetAllUsuarios();
            ViewBag.MiListado = ListarTodosUsuariosx;



            if (ListaProductos.Count > 0)
            {
                ListaFinal = contextCotizaciones.BuscarUnaCotizacion(ListaProductos[0].IdProductos);
                prs.CotizacionesLista = ListaFinal;
            }
            prs.ProductosLista = ListaProductos;
            prs.IdPresupuestos = Lista[0].IdPresupuestos;
            prs.Cliente = Lista[0].Cliente;
            prs.CondicionVenta = Lista[0].CondicionVenta;
            prs.Contacto = Lista[0].Contacto;
            prs.FechaCreacion = Lista[0].FechaCreacion;
            prs.ComercialAsignado = Lista[0].ComercialAsignado;
            prs.ClienteAsociado = Lista[0].ClienteAsociado;
            prs.ContactoAsociado = Lista[0].ContactoAsociado;
            prs.PaisFacturacion = Lista[0].PaisFacturacion;
            prs.SegmentoVenta = Lista[0].SegmentoVenta;
            prs.ClienteFacturacionPais = Lista[0].ClienteFacturacionPais;
            prs.ContactoClienteFacturacionPais = Lista[0].ContactoClienteFacturacionPais;
            prs.StatusPresupuesto = Lista[0].StatusPresupuesto;
            return View(prs);
        }

        public ActionResult EditarProductos(int id)
        {

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            Productos products = new Productos();
            products.ProductosLista = contextProductos.BuscarUnProducto(id);
            Productos prt = new Productos();
            prt.ProductosLista = contextProductos.BuscarUnProducto(id);
            prt.NombreProducto = products.ProductosLista[0].NombreProducto;
            prt.MarcaProducto = products.ProductosLista[0].MarcaProducto;
            prt.FamiliaProducto = products.ProductosLista[0].FamiliaProducto;
            prt.ModeloProducto = products.ProductosLista[0].ModeloProducto;
            prt.NumeroSerieProducto = products.ProductosLista[0].NumeroSerieProducto;
            prt.NormaProducto = products.ProductosLista[0].NormaProducto;
            prt.IdProductos = products.ProductosLista[0].IdProductos;
            prt.Descripcion = products.ProductosLista[0].Descripcion;
            prt.NombreFabricante = products.ProductosLista[0].NombreFabricante;
            prt.DireccionFabricante = products.ProductosLista[0].DireccionFabricante;
            prt.presupuestos_IdPresupuestos = products.ProductosLista[0].presupuestos_IdPresupuestos;
            return View(prt);
        }


        //Post del editar Productos
        [HttpPost]
        public ActionResult GuardarModificacionProductos(Productos model)
        {
            try
            {
                ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                Productos pro = new Productos();
                pro.IdProductos = model.IdProductos;
                pro.NombreProducto = model.NombreProducto;
                pro.MarcaProducto = model.MarcaProducto;
                pro.ModeloProducto = model.ModeloProducto;
                pro.FamiliaProducto = model.FamiliaProducto;
                pro.NumeroSerieProducto = model.NumeroSerieProducto;
                pro.NormaProducto = model.NormaProducto;
                pro.Descripcion = model.Descripcion;
                pro.NombreFabricante = model.NombreFabricante;
                pro.DireccionFabricante = model.DireccionFabricante;
                pro.presupuestos_IdPresupuestos = model.presupuestos_IdPresupuestos;
                bool resultado = contextProductos.ModificarProducto(pro);

                if (resultado == true)
                {
                    PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(pro.presupuestos_IdPresupuestos);
                    Presupuesto pre = new Presupuesto();
                    pre.IdPresupuestos = ListaPresupuesto[0].IdPresupuestos;
                    pre.Cliente = ListaPresupuesto[0].Cliente;
                    pre.Contacto = ListaPresupuesto[0].Contacto;
                    pre.SegmentoVenta = ListaPresupuesto[0].SegmentoVenta;
                    pre.CondicionVenta = ListaPresupuesto[0].CondicionVenta;
                    pre.ComercialAsignado = ListaPresupuesto[0].ComercialAsignado;
                    pre.ClienteAsociado = ListaPresupuesto[0].ClienteAsociado;
                    pre.ContactoAsociado = ListaPresupuesto[0].ContactoAsociado;
                    pre.PaisFacturacion = ListaPresupuesto[0].PaisFacturacion;
                    pre.ClienteFacturacionPais = ListaPresupuesto[0].ClienteFacturacionPais;
                    pre.ContactoClienteFacturacionPais = ListaPresupuesto[0].ContactoClienteFacturacionPais;
                    pre.FechaCreacion = ListaPresupuesto[0].FechaCreacion;
                    pre.StatusPresupuesto = ListaPresupuesto[0].StatusPresupuesto;
                    return RedirectToAction("EditarPresupuesto", pre);
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

        public ActionResult CrearProductos(int id)
        {
            Productos p = new Productos();
            p.presupuestos_IdPresupuestos = id;
            return View(p);
        }

        [HttpPost]
        public ActionResult GuardarProductos(Productos model)
        {
            try
            {
                ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                Productos pro = new Productos();
                pro.NombreProducto = model.NombreProducto;
                pro.MarcaProducto = model.MarcaProducto;
                pro.ModeloProducto = model.ModeloProducto;
                pro.FamiliaProducto = model.FamiliaProducto;
                pro.NumeroSerieProducto = model.NumeroSerieProducto;
                pro.NormaProducto = model.NormaProducto;
                pro.Descripcion = model.Descripcion;
                pro.NombreFabricante = model.NombreFabricante;
                pro.DireccionFabricante = model.DireccionFabricante;
                pro.presupuestos_IdPresupuestos = model.presupuestos_IdPresupuestos;
                bool resultado = contextProductos.InsertarProducto(pro);

                if (resultado == true)
                {
                    PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(pro.presupuestos_IdPresupuestos);
                    Presupuesto pre = new Presupuesto();
                    pre.IdPresupuestos = ListaPresupuesto[0].IdPresupuestos;
                    pre.Cliente = ListaPresupuesto[0].Cliente;
                    pre.Contacto = ListaPresupuesto[0].Contacto;
                    pre.SegmentoVenta = ListaPresupuesto[0].SegmentoVenta;
                    pre.CondicionVenta = ListaPresupuesto[0].CondicionVenta;
                    pre.ComercialAsignado = ListaPresupuesto[0].ComercialAsignado;
                    pre.ClienteAsociado = ListaPresupuesto[0].ClienteAsociado;
                    pre.ContactoAsociado = ListaPresupuesto[0].ContactoAsociado;
                    pre.PaisFacturacion = ListaPresupuesto[0].PaisFacturacion;
                    pre.ClienteFacturacionPais = ListaPresupuesto[0].ClienteFacturacionPais;
                    pre.ContactoClienteFacturacionPais = ListaPresupuesto[0].ContactoClienteFacturacionPais;
                    pre.FechaCreacion = ListaPresupuesto[0].FechaCreacion;
                    pre.StatusPresupuesto = ListaPresupuesto[0].StatusPresupuesto;
                    return RedirectToAction("EditarPresupuesto", pre);
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

        //Crear Cotizacion
        public ActionResult CrearCotizacion(int id)
        {
            ListaPreciosContext contextListaPrecios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ListaPreciosContext)) as ListaPreciosContext;
            List<ListaPrecios> ListarListaPrecios = contextListaPrecios.GetAllListasPrecios();
            ListaPrecios lispre = new ListaPrecios();
            ViewBag.MiListado = ListarListaPrecios;

            Cotizaciones c = new Cotizaciones();
            c.productos_IdProductos = id;
            return View(c);
        }

        [HttpPost]
        public ActionResult GuardarCotizacion(Cotizaciones model)
        {
            try
            {
                CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
                Cotizaciones cot = new Cotizaciones();
                cot.NombreCotizacion = model.NombreCotizacion;
                cot.PrecioUnitario = model.PrecioUnitario;
                cot.CantidadProductos = model.CantidadProductos;
                cot.SubTotal = model.SubTotal;
                cot.TotalPesoChile = model.TotalPesoChile;
                cot.productos_IdProductos = model.productos_IdProductos;
                cot.IVA = model.IVA;

                bool resultado = contextCotizaciones.InsertarCotizaciones(cot);

                if (resultado == true)
                {
                    PresupuestoContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;
                    ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                    Presupuesto prs = new Presupuesto();
                    List<Productos> ListaProductos = contextProductos.BuscarUnProducto(model.productos_IdProductos);

                    prs.ProductosLista = ListaProductos;
                    List<Presupuesto> Lista = context.BuscarUnPresupuesto(prs.ProductosLista[0].presupuestos_IdPresupuestos);
                    List<Cotizaciones> ListarCotizaciones = new List<Cotizaciones>();
                    List<Cotizaciones> ListaFinal = new List<Cotizaciones>();

                    prs.IdPresupuestos = Lista[0].IdPresupuestos;
                    prs.Cliente = Lista[0].Cliente;
                    prs.CondicionVenta = Lista[0].CondicionVenta;
                    prs.Contacto = Lista[0].Contacto;
                    prs.FechaCreacion = Lista[0].FechaCreacion;
                    prs.ComercialAsignado = Lista[0].ComercialAsignado;
                    prs.ClienteAsociado = Lista[0].ClienteAsociado;
                    prs.ContactoAsociado = Lista[0].ContactoAsociado;
                    prs.PaisFacturacion = Lista[0].PaisFacturacion;
                    prs.SegmentoVenta = Lista[0].SegmentoVenta;
                    prs.ClienteFacturacionPais = Lista[0].ClienteFacturacionPais;
                    prs.ContactoClienteFacturacionPais = Lista[0].ContactoClienteFacturacionPais;
                    prs.StatusPresupuesto = Lista[0].StatusPresupuesto;
                    return RedirectToAction("EditarPresupuesto", prs);
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

        public ActionResult EditarCotizacion(int id)
        {
            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            Cotizaciones coti = new Cotizaciones();
            List<Cotizaciones> lista = new List<Cotizaciones>();
            lista = contextCotizaciones.BuscarUnaCotizacion(id);
            coti.IdCotizacion = lista[0].IdCotizacion;
            coti.NombreCotizacion = lista[0].NombreCotizacion;
            coti.PrecioUnitario = lista[0].PrecioUnitario;
            coti.CantidadProductos = lista[0].CantidadProductos;
            coti.SubTotal = lista[0].SubTotal;
            coti.TotalPesoChile = lista[0].TotalPesoChile;
            coti.productos_IdProductos = lista[0].productos_IdProductos;
            coti.IVA = lista[0].IVA;

            ListaPreciosContext contextListaPrecios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ListaPreciosContext)) as ListaPreciosContext;
            List<ListaPrecios> ListarListaPrecios = contextListaPrecios.GetAllListasPrecios();
            ListaPrecios lispre = new ListaPrecios();
            ViewBag.MiListado = ListarListaPrecios;

            return View(coti);
          
        }
        [HttpPost]
        public ActionResult ModificarCotizacion(Cotizaciones model)
        {
            try
            {
                CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
                Cotizaciones cot = new Cotizaciones();
                cot.IdCotizacion = model.IdCotizacion;
                cot.NombreCotizacion = model.NombreCotizacion;
                cot.PrecioUnitario = model.PrecioUnitario;
                cot.CantidadProductos = model.CantidadProductos;
                cot.SubTotal = model.SubTotal;
                cot.TotalPesoChile = model.TotalPesoChile;
                cot.productos_IdProductos = model.productos_IdProductos;
                cot.IVA = model.IVA;

                bool resultado = contextCotizaciones.ModificarCotizacion(cot);

                if (resultado == true)
                {
                    PresupuestoContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;
                    ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                    Presupuesto prs = new Presupuesto();
                    List<Productos> ListaProductos = contextProductos.BuscarUnProducto(model.productos_IdProductos);

                    prs.ProductosLista = ListaProductos;
                    List<Presupuesto> Lista = context.BuscarUnPresupuesto(prs.ProductosLista[0].presupuestos_IdPresupuestos);
                    List<Cotizaciones> ListarCotizaciones = new List<Cotizaciones>();
                    List<Cotizaciones> ListaFinal = new List<Cotizaciones>();

                    prs.IdPresupuestos = Lista[0].IdPresupuestos;
                    prs.Cliente = Lista[0].Cliente;
                    prs.CondicionVenta = Lista[0].CondicionVenta;
                    prs.Contacto = Lista[0].Contacto;
                    prs.FechaCreacion = Lista[0].FechaCreacion;
                    prs.ComercialAsignado = Lista[0].ComercialAsignado;
                    prs.ClienteAsociado = Lista[0].ClienteAsociado;
                    prs.ContactoAsociado = Lista[0].ContactoAsociado;
                    prs.PaisFacturacion = Lista[0].PaisFacturacion;
                    prs.SegmentoVenta = Lista[0].SegmentoVenta;
                    prs.ClienteFacturacionPais = Lista[0].ClienteFacturacionPais;
                    prs.ContactoClienteFacturacionPais = Lista[0].ContactoClienteFacturacionPais;
                    prs.StatusPresupuesto = Lista[0].StatusPresupuesto;
                    return RedirectToAction("EditarPresupuesto", prs);
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
        public ActionResult PlantillaPresupuesto(Presupuesto model)
        {

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(model.IdPresupuestos);
            Presupuesto pre = new Presupuesto();
            pre.IdPresupuestos = ListaPresupuesto[0].IdPresupuestos;
            pre.Cliente = ListaPresupuesto[0].Cliente;
            pre.Contacto = ListaPresupuesto[0].Contacto;
            pre.SegmentoVenta = ListaPresupuesto[0].SegmentoVenta;
            pre.CondicionVenta = ListaPresupuesto[0].CondicionVenta;
            pre.ComercialAsignado = ListaPresupuesto[0].ComercialAsignado;
            pre.ClienteAsociado = ListaPresupuesto[0].ClienteAsociado;
            pre.ContactoAsociado = ListaPresupuesto[0].ContactoAsociado;
            pre.PaisFacturacion = ListaPresupuesto[0].PaisFacturacion;
            pre.ClienteFacturacionPais = ListaPresupuesto[0].ClienteFacturacionPais;
            pre.ContactoClienteFacturacionPais = ListaPresupuesto[0].ContactoClienteFacturacionPais;
            pre.FechaCreacion = ListaPresupuesto[0].FechaCreacion;
            pre.FechaCreacion2 = ListaPresupuesto[0].FechaCreacion;
            pre.StatusPresupuesto = ListaPresupuesto[0].StatusPresupuesto;
            pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;
            pre.PresupuestoLista = ListaPresupuesto;

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            pre.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(model.IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            pre.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pre.ProductosLista[0].IdProductos);

            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            pre.EntidadesLista = contextEntidades.BuscarUnaEntidad(pre.entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            pre.ContactosLista = contextContacto.BuscarContactos(pre.EntidadesLista[0].IdEntidades);


            return View(pre);
        }
    




    public ActionResult PasarStatus(Presupuesto model)
    {

        PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
        List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(model.IdPresupuestos);
        Presupuesto pre = new Presupuesto();
        pre.IdPresupuestos = ListaPresupuesto[0].IdPresupuestos;
        pre.Cliente = ListaPresupuesto[0].Cliente;
        pre.Contacto = ListaPresupuesto[0].Contacto;
        pre.SegmentoVenta = ListaPresupuesto[0].SegmentoVenta;
        pre.CondicionVenta = ListaPresupuesto[0].CondicionVenta;
        pre.ComercialAsignado = ListaPresupuesto[0].ComercialAsignado;
        pre.ClienteAsociado = ListaPresupuesto[0].ClienteAsociado;
        pre.ContactoAsociado = ListaPresupuesto[0].ContactoAsociado;
        pre.PaisFacturacion = ListaPresupuesto[0].PaisFacturacion;
        pre.ClienteFacturacionPais = ListaPresupuesto[0].ClienteFacturacionPais;
        pre.ContactoClienteFacturacionPais = ListaPresupuesto[0].ContactoClienteFacturacionPais;
        pre.FechaCreacion = ListaPresupuesto[0].FechaCreacion;
        pre.FechaCreacion2 = ListaPresupuesto[0].FechaCreacion;
        pre.StatusPresupuesto = -ListaPresupuesto[0].StatusPresupuesto;
        pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;


        ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
        pre.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(model.IdPresupuestos);

        CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
        pre.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pre.ProductosLista[0].IdProductos);

        EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
        pre.EntidadesLista = contextEntidades.BuscarUnaEntidad(pre.entidades_IdEntidades);

        ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
        pre.ContactosLista = contextContacto.BuscarContactos(pre.EntidadesLista[0].IdEntidades);

        
        return View(pre);
    }


        [HttpPost]
        public ActionResult CrearListaDePrecios(Presupuesto model)
        {
            try
            {
                PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                ListaPreciosContext contextListaPrecios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ListaPreciosContext)) as ListaPreciosContext;

                bool resultado = contextListaPrecios.InsertarListaPrecios(model);

                if (resultado == true)
                {
                    List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(model.IdPresupuestos);
                    Presupuesto pre = new Presupuesto();
                    pre.IdPresupuestos = ListaPresupuesto[0].IdPresupuestos;
                    pre.Cliente = ListaPresupuesto[0].Cliente;
                    pre.Contacto = ListaPresupuesto[0].Contacto;
                    pre.SegmentoVenta = ListaPresupuesto[0].SegmentoVenta;
                    pre.CondicionVenta = ListaPresupuesto[0].CondicionVenta;
                    pre.ComercialAsignado = ListaPresupuesto[0].ComercialAsignado;
                    pre.ClienteAsociado = ListaPresupuesto[0].ClienteAsociado;
                    pre.ContactoAsociado = ListaPresupuesto[0].ContactoAsociado;
                    pre.PaisFacturacion = ListaPresupuesto[0].PaisFacturacion;
                    pre.ClienteFacturacionPais = ListaPresupuesto[0].ClienteFacturacionPais;
                    pre.ContactoClienteFacturacionPais = ListaPresupuesto[0].ContactoClienteFacturacionPais;
                    pre.FechaCreacion = ListaPresupuesto[0].FechaCreacion;
                    pre.StatusPresupuesto = ListaPresupuesto[0].StatusPresupuesto;
                    return RedirectToAction("EditarPresupuesto", pre);
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

        public ActionResult CancelarItem(Presupuesto model)
        {
            try
            {
                PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                PedidoEnsayoContext contextPedido = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                List<PedidoEnsayo> ListaPedidos = contextPedido.BuscarPedidoEnsayoPorIdPresupuesto(model.IdPresupuestos);
                PedidoEnsayo pedido = new PedidoEnsayo();
                if (ListaPedidos != null && ListaPedidos.Count !=0) {
                pedido.IdPedidoEnsayo = ListaPedidos[0].IdPedidoEnsayo;
                pedido.ProtocoloAplicable = ListaPedidos[0].ProtocoloAplicable;
                pedido.CondicionesDeEnsayo = ListaPedidos[0].CondicionesDeEnsayo;
                pedido.AutorPedido = ListaPedidos[0].AutorPedido;
                pedido.NumeroSec = ListaPedidos[0].NumeroSec;
                pedido.Comentarios = ListaPedidos[0].Comentarios;
                pedido.presupuestos_IdPresupuestos = ListaPedidos[0].presupuestos_IdPresupuestos;
                    pedido.StatusPedidoEnsayo = 7;
                    contextPedido.PasarStatus(pedido);
                }
                List<Presupuesto> listaPresupuestos;
                listaPresupuestos = contextPresupuesto.BuscarUnPresupuesto(model.IdPresupuestos);
                Presupuesto presupuestos = new Presupuesto();
                presupuestos.StatusPresupuesto = 7;
                presupuestos.IdPresupuestos = listaPresupuestos[0].IdPresupuestos;
    

                contextPresupuesto.PasarStatus(presupuestos);
              

                List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(model.IdPresupuestos);
                    Presupuesto pre = new Presupuesto();
                    pre.IdPresupuestos = ListaPresupuesto[0].IdPresupuestos;
                    pre.Cliente = ListaPresupuesto[0].Cliente;
                    pre.Contacto = ListaPresupuesto[0].Contacto;
                    pre.SegmentoVenta = ListaPresupuesto[0].SegmentoVenta;
                    pre.CondicionVenta = ListaPresupuesto[0].CondicionVenta;
                    pre.ComercialAsignado = ListaPresupuesto[0].ComercialAsignado;
                    pre.ClienteAsociado = ListaPresupuesto[0].ClienteAsociado;
                    pre.ContactoAsociado = ListaPresupuesto[0].ContactoAsociado;
                    pre.PaisFacturacion = ListaPresupuesto[0].PaisFacturacion;
                    pre.ClienteFacturacionPais = ListaPresupuesto[0].ClienteFacturacionPais;
                    pre.ContactoClienteFacturacionPais = ListaPresupuesto[0].ContactoClienteFacturacionPais;
                    pre.FechaCreacion = ListaPresupuesto[0].FechaCreacion;
                    pre.StatusPresupuesto = ListaPresupuesto[0].StatusPresupuesto;
                    return RedirectToAction("EditarPresupuesto", pre);


            }

            catch (Exception ex)
            {
                throw ex;

            }


        }
    }
}