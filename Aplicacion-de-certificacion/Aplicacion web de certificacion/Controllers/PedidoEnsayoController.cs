using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion_web_de_certificacion.Data;
using Aplicacion_web_de_certificacion.Models;
using Aplicacion_web_de_certificacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aplicacion_web_de_certificacion.Controllers
{
    [Authorize]
    public class PedidoEnsayoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public PedidoEnsayoController(
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
        public IActionResult Index()
        {
            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<PedidoEnsayo> listapedidos = contextPedidoEnsayo.GetAllPedidoEnsayo();


            return View(listapedidos);
        }

        public IActionResult Editar()
        {
            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<PedidoEnsayo> listapedidos = contextPedidoEnsayo.GetAllPedidoEnsayo();
            PedidoEnsayo pedido = new PedidoEnsayo();
            pedido.AutorPedido = listapedidos[0].AutorPedido;
            pedido.CondicionesDeEnsayo = listapedidos[0].CondicionesDeEnsayo;
            pedido.NumeroSec = listapedidos[0].NumeroSec;
            pedido.Comentarios = listapedidos[0].Comentarios;
            pedido.ProtocoloAplicable = listapedidos[0].ProtocoloAplicable;
            pedido.presupuestos_IdPresupuestos = listapedidos[0].presupuestos_IdPresupuestos;
            pedido.PedidoEnsayoLista = listapedidos;
            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            pedido.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(pedido.presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            pedido.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(pedido.PresupuestoLista[0].IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;

            if (pedido.ProductosLista != null)
            {
                pedido.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pedido.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            pedido.EntidadesLista = contextEntidades.BuscarUnaEntidad(pedido.PresupuestoLista[0].entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            pedido.ContactosLista = contextContacto.BuscarContactos(pedido.EntidadesLista[0].IdEntidades);

            LugarEnsayosContext contextLugarEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.LugarEnsayosContext)) as LugarEnsayosContext;
            pedido.LugarEnsayosLista = contextLugarEnsayos.BuscarLugarEnsayo(pedido.EntidadesLista[0].IdEntidades);



            return View(pedido);
        }
        public ActionResult PasarStatus(Presupuesto model)
        {
            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
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
            int status = ListaPresupuesto[0].StatusPresupuesto; ;
            pre.StatusPresupuesto = status + 1;
            pre.Usuarios_IdUsuarios = ListaPresupuesto[0].Usuarios_IdUsuarios;
            pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;
            contextPresupuesto.PasarStatus(pre);
            PedidoEnsayo pedido = new PedidoEnsayo();

            pedido.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(model.IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            pedido.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(pedido.PresupuestoLista[0].IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;

            if (pedido.ProductosLista.Count > 0) { 
            pedido.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pedido.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            pedido.EntidadesLista = contextEntidades.BuscarUnaEntidad(pedido.PresupuestoLista[0].entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            pedido.ContactosLista = contextContacto.BuscarContactos(pedido.EntidadesLista[0].IdEntidades);

            LugarEnsayosContext contextLugarEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.LugarEnsayosContext)) as LugarEnsayosContext;
            pedido.LugarEnsayosLista = contextLugarEnsayos.BuscarLugarEnsayo(pedido.EntidadesLista[0].IdEntidades);
            List<PedidoEnsayo> listaComprobarSiExiste = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdPresupuesto(model.IdPresupuestos);

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;


            if (listaComprobarSiExiste.Count >=1)
            {
                    pedido.IdPedidoEnsayo = listaComprobarSiExiste[0].IdPedidoEnsayo;
                return RedirectToAction("EditarPedidoEnsayo", pedido);
            }
            else
            { 
            contextPedidoEnsayo.InsertarPedidoEnsayo(pedido);
                List<PedidoEnsayo> listaComprobarSiExiste2 = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdPresupuesto(model.IdPresupuestos);
                pedido.IdPedidoEnsayo = listaComprobarSiExiste2[0].IdPedidoEnsayo;
                pedido.presupuestos_IdPresupuestos = listaComprobarSiExiste2[0].presupuestos_IdPresupuestos;
                contextoAprobacion.InsertarAprobacionDefault1(pedido);
                contextoAprobacion.InsertarAprobacionDefault2(pedido);
                contextoAprobacion.InsertarAprobacionDefault3(pedido);
                pedido.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(pedido.IdPedidoEnsayo);

                MuestrasContext contextoMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
                Muestras muestra = new Muestras();
   
                muestra.FechaIngreso = DateTime.Now;
                muestra.Producto = pedido.ProductosLista[0].NombreProducto;
                if (pedido.CotizacionesLista.Count > 0)
                {
                    muestra.Cantidad = pedido.CotizacionesLista[0].CantidadProductos;
                }
                muestra.Marca = pedido.ProductosLista[0].MarcaProducto;
                muestra.Fabricante = pedido.ProductosLista[0].NombreFabricante;
                muestra.PaisOrigen = pedido.ProductosLista[0].DireccionFabricante;
                muestra.Modelo = pedido.ProductosLista[0].ModeloProducto;
                muestra.pedidoEnsayo_IdPedidoEnsayo = pedido.IdPedidoEnsayo;
                contextoMuestras.InsertarMuestra(muestra);

                return RedirectToAction("EditarPedidoEnsayo", pedido);
            }
               
            

        }




        public ActionResult EditarPedidoEnsayo(PedidoEnsayo model)
        {

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(model.presupuestos_IdPresupuestos);
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
            pre.Usuarios_IdUsuarios = ListaPresupuesto[0].Usuarios_IdUsuarios;
            pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;

            PedidoEnsayo pedido = new PedidoEnsayo();

            pedido.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(model.presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            pedido.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(model.presupuestos_IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (pedido.ProductosLista !=null) { 
            pedido.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pedido.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            pedido.EntidadesLista = contextEntidades.BuscarUnaEntidad(pre.entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            pedido.ContactosLista = contextContacto.BuscarContactos(pedido.EntidadesLista[0].IdEntidades);

            

            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
           List<PedidoEnsayo> ListaPedidoEnsayos = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdPresupuesto(model.presupuestos_IdPresupuestos);
            pedido.IdPedidoEnsayo = ListaPedidoEnsayos[0].IdPedidoEnsayo;
            pedido.ProtocoloAplicable = ListaPedidoEnsayos[0].ProtocoloAplicable;
            pedido.CondicionesDeEnsayo = ListaPedidoEnsayos[0].CondicionesDeEnsayo;
            pedido.AutorPedido = ListaPedidoEnsayos[0].AutorPedido;
            pedido.Comentarios = ListaPedidoEnsayos[0].Comentarios;
            pedido.presupuestos_IdPresupuestos = ListaPedidoEnsayos[0].presupuestos_IdPresupuestos;
            pedido.MuestrasLista = contextMuestras.BuscarUnaMuestra(pedido.IdPedidoEnsayo);
            pedido.StatusPedidoEnsayo = ListaPedidoEnsayos[0].StatusPedidoEnsayo;
            pedido.NumeroSec = ListaPedidoEnsayos[0].NumeroSec;
            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

            EnsayosContext contextoEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;

            List<Ensayos> listaensayos = contextoEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(pedido.IdPedidoEnsayo);
            if (listaensayos != null) {
                pedido.EnsayosLista = listaensayos;
            }
            pedido.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(pedido.IdPedidoEnsayo);
            return View(pedido);
        }

        public ActionResult CrearMuestra(int id)
        {
            Muestras p = new Muestras();
            p.pedidoEnsayo_IdPedidoEnsayo = id;
            return View(p);
        }

        [HttpPost]
        public ActionResult GuardarMuestra(Muestras model)
        {
            try
            {
                MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
                Muestras pro = new Muestras();
                pro.Cantidad = model.Cantidad;
                pro.DestinoMuestras = model.DestinoMuestras;
                pro.Etiqueta = model.Etiqueta;
                pro.Fabricante = model.Fabricante;
                pro.FechaIngreso2 = model.FechaIngreso2;
                pro.FechaIngreso = model.FechaIngreso2;
                pro.Marca = model.Marca;
                pro.Modelo = model.Modelo;
                pro.PaisOrigen = model.PaisOrigen;
                pro.Producto = model.Producto;
                pro.pedidoEnsayo_IdPedidoEnsayo = model.pedidoEnsayo_IdPedidoEnsayo;
                bool resultado = contextMuestras.InsertarMuestra(pro);

                if (resultado == true)
                {
                    PedidoEnsayoContext contextPedido = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                    List<PedidoEnsayo> ListaPedidos = contextPedido.BuscarUnaMuestra(pro.pedidoEnsayo_IdPedidoEnsayo);
                    PedidoEnsayo pre = new PedidoEnsayo();
                    pre.IdPedidoEnsayo = ListaPedidos[0].IdPedidoEnsayo;
                    pre.ProtocoloAplicable = ListaPedidos[0].ProtocoloAplicable;
                    pre.CondicionesDeEnsayo = ListaPedidos[0].CondicionesDeEnsayo;
                    pre.AutorPedido = ListaPedidos[0].AutorPedido;
                    pre.Comentarios = ListaPedidos[0].Comentarios;
                    pre.NumeroSec = ListaPedidos[0].NumeroSec;
                    pre.presupuestos_IdPresupuestos = ListaPedidos[0].presupuestos_IdPresupuestos;
                    return RedirectToAction("EditarPedidoEnsayo", pre);
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

        public ActionResult EditarPedidoEnsayo2(int presupuestos_IdPresupuestos, int IdPresupuestos)
        {
            if (IdPresupuestos < 1) {
                IdPresupuestos = presupuestos_IdPresupuestos;
            }
            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(IdPresupuestos);
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
            pre.Usuarios_IdUsuarios = ListaPresupuesto[0].Usuarios_IdUsuarios;
            pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;

            PedidoEnsayo pedido = new PedidoEnsayo();

            pedido.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            pedido.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (pedido.ProductosLista != null)
            {
                pedido.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pedido.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            pedido.EntidadesLista = contextEntidades.BuscarUnaEntidad(pre.entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            pedido.ContactosLista = contextContacto.BuscarContactos(pedido.EntidadesLista[0].IdEntidades);



            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<PedidoEnsayo> ListaPedidoEnsayos = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdPresupuesto(IdPresupuestos);
            pedido.IdPedidoEnsayo = ListaPedidoEnsayos[0].IdPedidoEnsayo;
            pedido.ProtocoloAplicable = ListaPedidoEnsayos[0].ProtocoloAplicable;
            pedido.CondicionesDeEnsayo = ListaPedidoEnsayos[0].CondicionesDeEnsayo;
            pedido.AutorPedido = ListaPedidoEnsayos[0].AutorPedido;
            pedido.Comentarios = ListaPedidoEnsayos[0].Comentarios;
            pedido.presupuestos_IdPresupuestos = ListaPedidoEnsayos[0].presupuestos_IdPresupuestos;
            pedido.MuestrasLista = contextMuestras.BuscarUnaMuestra(pedido.IdPedidoEnsayo);
            pedido.StatusPedidoEnsayo = ListaPedidoEnsayos[0].StatusPedidoEnsayo;
            pedido.NumeroSec = ListaPedidoEnsayos[0].NumeroSec;

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;


            pedido.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(pedido.IdPedidoEnsayo);
            return RedirectToAction("EditarPedidoEnsayo", pedido);
        }



        public ActionResult EditarAprobacion(int id)
        {

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
            Aprobacion aprob = new Aprobacion();
            aprob.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(id);
            Aprobacion apr = new Aprobacion();
            apr.IdAprobacion = aprob.AprobacionLista[0].IdAprobacion;
            apr.FechaCreacionAprobacion = aprob.AprobacionLista[0].FechaCreacionAprobacion;
            apr.FechaCreacionAprobacion2 = aprob.AprobacionLista[0].FechaCreacionAprobacion;
            apr.UsuarioDesignadoAprobacion = _userManager.GetUserName(User);
            apr.ComentariosAprobacion = aprob.AprobacionLista[0].ComentariosAprobacion;
            apr.TipoAprobacion = aprob.AprobacionLista[0].TipoAprobacion;
            apr.EstadoAprobacion = aprob.AprobacionLista[0].EstadoAprobacion;
            apr.PedidoEnsayo_IdPedidoEnsayo = aprob.AprobacionLista[0].PedidoEnsayo_IdPedidoEnsayo;
            return View(apr);
        }

        public ActionResult EditarAprobacion2(int id)
        {

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
            Aprobacion aprob = new Aprobacion();
            aprob.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(id);
            Aprobacion apr = new Aprobacion();
            apr.IdAprobacion = aprob.AprobacionLista[1].IdAprobacion;
            apr.FechaCreacionAprobacion = aprob.AprobacionLista[1].FechaCreacionAprobacion;
            apr.FechaCreacionAprobacion2 = aprob.AprobacionLista[1].FechaCreacionAprobacion;
            apr.UsuarioDesignadoAprobacion = _userManager.GetUserName(User);
            apr.ComentariosAprobacion = aprob.AprobacionLista[1].ComentariosAprobacion;
            apr.TipoAprobacion = aprob.AprobacionLista[1].TipoAprobacion;
            apr.EstadoAprobacion = aprob.AprobacionLista[1].EstadoAprobacion;
            apr.PedidoEnsayo_IdPedidoEnsayo = aprob.AprobacionLista[1].PedidoEnsayo_IdPedidoEnsayo;
            return View(apr);
        }

        public ActionResult EditarAprobacion3(int id)
        {

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
            Aprobacion aprob = new Aprobacion();
            aprob.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(id);
            Aprobacion apr = new Aprobacion();
            apr.IdAprobacion = aprob.AprobacionLista[2].IdAprobacion;
            apr.FechaCreacionAprobacion = aprob.AprobacionLista[2].FechaCreacionAprobacion;
            apr.FechaCreacionAprobacion2 = aprob.AprobacionLista[2].FechaCreacionAprobacion;
            apr.UsuarioDesignadoAprobacion = _userManager.GetUserName(User);
            apr.ComentariosAprobacion = aprob.AprobacionLista[2].ComentariosAprobacion;
            apr.TipoAprobacion = aprob.AprobacionLista[2].TipoAprobacion;
            apr.EstadoAprobacion = aprob.AprobacionLista[2].EstadoAprobacion;
            apr.PedidoEnsayo_IdPedidoEnsayo = aprob.AprobacionLista[2].PedidoEnsayo_IdPedidoEnsayo;
            return View(apr);
        }

        //Post del editar Productos
        [HttpPost]
        public ActionResult GuardarModficacionAprobacion(Aprobacion model)
        {
            try
            {
                AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
                Aprobacion apro = new Aprobacion();

                model.UsuarioDesignadoAprobacion = _userManager.GetUserName(User);
                bool resultado = contextoAprobacion.ModificarAprobacion(model);

                if (resultado == true)
                {
                    PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    PedidoEnsayoContext contextPedido = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                    List<PedidoEnsayo> ListaPedidos = contextPedido.BuscarUnaMuestra(model.PedidoEnsayo_IdPedidoEnsayo);
                    PedidoEnsayo pre = new PedidoEnsayo();
                    pre.IdPedidoEnsayo = ListaPedidos[0].IdPedidoEnsayo;
                    pre.ProtocoloAplicable = ListaPedidos[0].ProtocoloAplicable;
                    pre.CondicionesDeEnsayo = ListaPedidos[0].CondicionesDeEnsayo;
                    pre.AutorPedido = ListaPedidos[0].AutorPedido;
                    pre.NumeroSec = ListaPedidos[0].NumeroSec;
                    pre.Comentarios = ListaPedidos[0].Comentarios;
                    pre.presupuestos_IdPresupuestos = ListaPedidos[0].presupuestos_IdPresupuestos;
                    List<Presupuesto> listaPresupuestos;
                    listaPresupuestos = contextPresupuesto.BuscarUnPresupuesto(pre.presupuestos_IdPresupuestos);
                    Presupuesto presupuestos = new Presupuesto();
                    presupuestos.StatusPresupuesto = 1 + listaPresupuestos[0].StatusPresupuesto;
                    presupuestos.IdPresupuestos = listaPresupuestos[0].IdPresupuestos;
                    pre.StatusPedidoEnsayo = presupuestos.StatusPresupuesto;
                    contextPedido.PasarStatus(pre);
                    contextPresupuesto.PasarStatus(presupuestos);
                    return RedirectToAction("EditarPedidoEnsayo", pre);
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
        public ActionResult GuardarPedidoEnsayo(PedidoEnsayo model)
        {

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(model.presupuestos_IdPresupuestos);
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
            pre.Usuarios_IdUsuarios = ListaPresupuesto[0].Usuarios_IdUsuarios;
            pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;

            PedidoEnsayo pedido = new PedidoEnsayo();

            pedido.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(model.presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            pedido.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(model.presupuestos_IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (pedido.ProductosLista != null)
            {
                pedido.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pedido.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            pedido.EntidadesLista = contextEntidades.BuscarUnaEntidad(pre.entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            pedido.ContactosLista = contextContacto.BuscarContactos(pedido.EntidadesLista[0].IdEntidades);



            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<PedidoEnsayo> ListaPedidoEnsayos = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdPresupuesto(model.presupuestos_IdPresupuestos);
            pedido.IdPedidoEnsayo = model.IdPedidoEnsayo;
            pedido.ProtocoloAplicable = model.ProtocoloAplicable;
            pedido.CondicionesDeEnsayo = model.CondicionesDeEnsayo;
            pedido.NumeroSec = model.NumeroSec;
            pedido.AutorPedido = _userManager.GetUserName(User);
            pedido.Comentarios = model.Comentarios;
            pedido.presupuestos_IdPresupuestos = model.presupuestos_IdPresupuestos;
            pedido.MuestrasLista = contextMuestras.BuscarUnaMuestra(pedido.IdPedidoEnsayo);
            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
            contextPedidoEnsayo.ModificarPedidoEnsayo(pedido);

            pedido.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(pedido.IdPedidoEnsayo);
            return RedirectToAction("EditarPedidoEnsayo", pedido);
        }


        public ActionResult EditarMuestra(int id)
        {

            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            Muestras products = new Muestras();
            products.MuestrasLista = contextMuestras.BuscarUnaMuestra(id);
            Muestras prt = new Muestras();
            prt.MuestrasLista = contextMuestras.BuscarUnaMuestra(id);
            prt.FechaIngreso = products.MuestrasLista[0].FechaIngreso;
            prt.Etiqueta = products.MuestrasLista[0].Etiqueta;
            prt.Producto = products.MuestrasLista[0].Producto;
            prt.Cantidad = products.MuestrasLista[0].Cantidad;
            prt.Marca = products.MuestrasLista[0].Marca;
            prt.Modelo = products.MuestrasLista[0].Modelo;
            prt.Fabricante = products.MuestrasLista[0].Fabricante;
            prt.PaisOrigen = products.MuestrasLista[0].PaisOrigen;
            prt.IdMuestras = products.MuestrasLista[0].IdMuestras;
            prt.DestinoMuestras = products.MuestrasLista[0].DestinoMuestras;
            prt.pedidoEnsayo_IdPedidoEnsayo = products.MuestrasLista[0].pedidoEnsayo_IdPedidoEnsayo;
            return View(prt);
        }


        //Post del editar Productos
        [HttpPost]
        public ActionResult GuardarModificacionMuestra(Muestras model)
        {
            try
            {
                MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
                Muestras pro = new Muestras();
                pro.Etiqueta = model.Etiqueta;
                pro.Producto = model.Producto;
                pro.Cantidad = model.Cantidad;
                pro.Marca = model.Marca;
                pro.Modelo = model.Modelo;
                pro.Fabricante = model.Fabricante;
                pro.PaisOrigen = model.PaisOrigen;
                pro.DestinoMuestras = model.DestinoMuestras;
                pro.pedidoEnsayo_IdPedidoEnsayo = model.pedidoEnsayo_IdPedidoEnsayo;
                pro.IdMuestras = model.IdMuestras;
                bool resultado = contextMuestras.ModificarMuestra(pro);

                if (resultado == true)
                {
                    PedidoEnsayoContext contextPedido = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                    List<PedidoEnsayo> ListaPedidos = contextPedido.BuscarUnaMuestra(pro.pedidoEnsayo_IdPedidoEnsayo);
                    PedidoEnsayo pre = new PedidoEnsayo();
                    pre.IdPedidoEnsayo = ListaPedidos[0].IdPedidoEnsayo;
                    pre.ProtocoloAplicable = ListaPedidos[0].ProtocoloAplicable;
                    pre.CondicionesDeEnsayo = ListaPedidos[0].CondicionesDeEnsayo;
                    pre.AutorPedido = ListaPedidos[0].AutorPedido;
                    pre.NumeroSec = ListaPedidos[0].NumeroSec;
                    pre.presupuestos_IdPresupuestos = ListaPedidos[0].presupuestos_IdPresupuestos;
                    return RedirectToAction("EditarPedidoEnsayo", pre);

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


        public ActionResult EmitirCertificado(PedidoEnsayo model)
        {

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            List<Presupuesto> ListaPresupuesto = contextPresupuesto.BuscarUnPresupuesto(model.presupuestos_IdPresupuestos);
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
            pre.Usuarios_IdUsuarios = ListaPresupuesto[0].Usuarios_IdUsuarios;
            pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;

            PedidoEnsayo pedido = new PedidoEnsayo();

            pedido.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(model.presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            pedido.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(model.presupuestos_IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (pedido.ProductosLista != null)
            {
                pedido.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(pedido.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            pedido.EntidadesLista = contextEntidades.BuscarUnaEntidad(pre.entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            pedido.ContactosLista = contextContacto.BuscarContactos(pedido.EntidadesLista[0].IdEntidades);



            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<PedidoEnsayo> ListaPedidoEnsayos = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdPresupuesto(model.presupuestos_IdPresupuestos);
            pedido.IdPedidoEnsayo = ListaPedidoEnsayos[0].IdPedidoEnsayo;
            pedido.ProtocoloAplicable = ListaPedidoEnsayos[0].ProtocoloAplicable;
            pedido.CondicionesDeEnsayo = ListaPedidoEnsayos[0].CondicionesDeEnsayo;
            pedido.AutorPedido = ListaPedidoEnsayos[0].AutorPedido;
            pedido.Comentarios = ListaPedidoEnsayos[0].Comentarios;
            pedido.presupuestos_IdPresupuestos = ListaPedidoEnsayos[0].presupuestos_IdPresupuestos;
            pedido.MuestrasLista = contextMuestras.BuscarUnaMuestra(pedido.IdPedidoEnsayo);
            pedido.StatusPedidoEnsayo = ListaPedidoEnsayos[0].StatusPedidoEnsayo;
            pedido.NumeroSec = ListaPedidoEnsayos[0].NumeroSec;
            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

            EnsayosContext contextoEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;

            List<Ensayos> listaensayos = contextoEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(pedido.IdPedidoEnsayo);
            if (listaensayos != null)
            {
                pedido.EnsayosLista = listaensayos;
            }
            pedido.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(pedido.IdPedidoEnsayo);
            return View(pedido);
        }



        [HttpPost]
        public ActionResult CancelarProducto(PedidoEnsayo model)
        {
            try
            {
                    PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    PedidoEnsayoContext contextPedido = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                    List<PedidoEnsayo> ListaPedidos = contextPedido.BuscarUnaMuestra(model.IdPedidoEnsayo);
                    PedidoEnsayo pre = new PedidoEnsayo();
                    pre.IdPedidoEnsayo = ListaPedidos[0].IdPedidoEnsayo;
                    pre.ProtocoloAplicable = ListaPedidos[0].ProtocoloAplicable;
                    pre.CondicionesDeEnsayo = ListaPedidos[0].CondicionesDeEnsayo;
                    pre.AutorPedido = ListaPedidos[0].AutorPedido;
                    pre.NumeroSec = ListaPedidos[0].NumeroSec;
                    pre.Comentarios = ListaPedidos[0].Comentarios;
                    pre.presupuestos_IdPresupuestos = ListaPedidos[0].presupuestos_IdPresupuestos;
                    List<Presupuesto> listaPresupuestos;
                    listaPresupuestos = contextPresupuesto.BuscarUnPresupuesto(pre.presupuestos_IdPresupuestos);
                    Presupuesto presupuestos = new Presupuesto();
                    presupuestos.StatusPresupuesto = 7;
                    presupuestos.IdPresupuestos = listaPresupuestos[0].IdPresupuestos;
                    pre.StatusPedidoEnsayo = 7;
                    contextPedido.PasarStatus(pre);
                    contextPresupuesto.PasarStatus(presupuestos);
                    return RedirectToAction("EditarPedidoEnsayo", pre);
                

            }

            catch (Exception ex)
            {
                throw ex;

            }
        }

    }
}