using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aplicacion_web_de_certificacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using MySql.Data.MySqlClient;


namespace Aplicacion_web_de_certificacion.Controllers
{
    [Authorize]
    public class EnsayosController : Controller
    {
        private readonly IHostingEnvironment _environment;

        // Constructor
        public EnsayosController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }

        public IActionResult Index()
        {
            EnsayosContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;

            return View(context.GetAllEnsayos());
        }

        public IActionResult NoAsignados()
        {
            EnsayosContext context = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;

            return View(context.GetNoAsigEnsayos());
        }

        [HttpPost]
        public ActionResult GenerarEnsayo(PedidoEnsayo model)
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
            pre.StatusPresupuesto = 1+ListaPresupuesto[0].StatusPresupuesto;

            pre.entidades_IdEntidades = ListaPresupuesto[0].entidades_IdEntidades;

            PedidoEnsayo pedido = new PedidoEnsayo();


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
            pedido.StatusPedidoEnsayo = pre.StatusPresupuesto;
            pedido.AutorPedido = model.AutorPedido;
            pedido.presupuestos_IdPresupuestos = model.presupuestos_IdPresupuestos;
            pedido.MuestrasLista = contextMuestras.BuscarUnaMuestra(pedido.IdPedidoEnsayo);
            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
            Ensayos ensay = new Ensayos();

            ensay.pedidoEnsayo_IdPedidoEnsayo = pedido.IdPedidoEnsayo;
            ensay.ClienteCertificadora = "MiCliente";
            ensay.ClienteProducto = pedido.EntidadesLista[0].RazonSocial;
            ensay.Contacto = pedido.ContactosLista[0].EmailRepresentante;
            ensay.Segmento = pre.SegmentoVenta;
            ensay.StatusEnsayo = "En Ensayos";
            ensay.DescripcionEnsayo = pedido.CondicionesDeEnsayo;

            EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;

         
           

            bool resultado = contextEnsayos.InsertarNuevoEnsayoConIdPedidoEnsayos(ensay);

            if (resultado == true)
            {

                List<Ensayos> listaEnsayitos = contextEnsayos.GetEnsayoCreado(ensay);
                Ensayos ensayito = new Ensayos();
                ensayito.NombreEnsayo = listaEnsayitos[0].NombreEnsayo;
                ensayito.FechaDeAlta = listaEnsayitos[0].FechaDeAlta;
                ensayito.FechaDeAlta2 = listaEnsayitos[0].FechaDeAlta;
                ensayito.FechaPedido = listaEnsayitos[0].FechaPedido;
                ensayito.FechaPedido2 = listaEnsayitos[0].FechaPedido;
                ensayito.DescripcionEnsayo = listaEnsayitos[0].DescripcionEnsayo;
                ensayito.tecnicoAsignado = listaEnsayitos[0].tecnicoAsignado;
                ensayito.jefeLaboratorioAsignado = listaEnsayitos[0].jefeLaboratorioAsignado;
                ensayito.ClienteCertificadora = listaEnsayitos[0].ClienteCertificadora;
                ensayito.ClienteProducto = listaEnsayitos[0].ClienteProducto;
                ensayito.IdEnsayos = listaEnsayitos[0].IdEnsayos;
                ensayito.Contacto = listaEnsayitos[0].Contacto;
                ensayito.Segmento = listaEnsayitos[0].Segmento;
                ensayito.StatusEnsayo = listaEnsayitos[0].StatusEnsayo;
                ensayito.pedidoEnsayo_IdPedidoEnsayo = listaEnsayitos[0].pedidoEnsayo_IdPedidoEnsayo;

                ensayito.PedidoEnsayoLista = ListaPedidoEnsayos;
                ensayito.PresupuestoLista = pedido.PresupuestoLista;
                ensayito.ProductosLista = pedido.ProductosLista;
                ensayito.LugarEnsayosLista = pedido.LugarEnsayosLista;
                ensayito.EntidadesLista = pedido.EntidadesLista;
                ensayito.CotizacionesLista = pedido.CotizacionesLista;

              

                contextPedidoEnsayo.PasarStatus(pedido);
                contextPresupuesto.PasarStatus(pre);
                contextPresupuesto.BuscarUnPresupuesto(pre.IdPresupuestos);
                pedido.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(pedido.IdPedidoEnsayo);

                UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

                List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
                Presupuesto presu = new Presupuesto();
                presu.UsuariosLista = ListarTodosUsuarios;
                ViewBag.MiListado = ListarTodosUsuarios;


                



                ensayito.UsuariosLista = ListarTodosUsuarios;
                return RedirectToAction("EditarEnsayo", ensayito);
            }
            else {

                return RedirectToAction("Index");
            }
        }

        public ActionResult EditarEnsayo(Ensayos model)
        {

            EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<Ensayos> listaDeEnsayosx = contextEnsayos.BuscarEnsayoPorIdEnsayo(model.IdEnsayos);
            Ensayos ensay = new Ensayos();
            ensay.IdEnsayos = listaDeEnsayosx[0].IdEnsayos;
            ensay.NombreEnsayo = listaDeEnsayosx[0].NombreEnsayo;
            ensay.FechaDeAlta = listaDeEnsayosx[0].FechaDeAlta;
            ensay.FechaDeAlta2 = listaDeEnsayosx[0].FechaDeAlta;
            ensay.ClienteCertificadora = listaDeEnsayosx[0].ClienteCertificadora;
            ensay.ClienteProducto = listaDeEnsayosx[0].ClienteProducto;
            ensay.Contacto = listaDeEnsayosx[0].Contacto;
            ensay.FechaPedido = listaDeEnsayosx[0].FechaPedido;
            ensay.FechaPedido2 = listaDeEnsayosx[0].FechaPedido;
            ensay.DescripcionEnsayo = listaDeEnsayosx[0].DescripcionEnsayo;
            ensay.Segmento = listaDeEnsayosx[0].Segmento;
            ensay.tecnicoAsignado = listaDeEnsayosx[0].tecnicoAsignado;
            ensay.jefeLaboratorioAsignado = listaDeEnsayosx[0].jefeLaboratorioAsignado;
            ensay.StatusEnsayo = listaDeEnsayosx[0].StatusEnsayo;
            ensay.pedidoEnsayo_IdPedidoEnsayo = listaDeEnsayosx[0].pedidoEnsayo_IdPedidoEnsayo;

            int idensayoaeditar = listaDeEnsayosx[0].IdEnsayos;

            ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            ArchivosContext contextArchivos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ArchivosContext)) as ArchivosContext;
            ensay.ArchivosLista = contextArchivos.GetArchivos(idensayoaeditar);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (ensay.ProductosLista != null)
            {
                ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
            ensay.MuestrasLista = contextMuestras.BuscarUnaMuestra(ensay.pedidoEnsayo_IdPedidoEnsayo);
                ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);

            UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

            List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
            Presupuesto presu = new Presupuesto();
            presu.UsuariosLista = ListarTodosUsuarios;
            ViewBag.MiListado = ListarTodosUsuarios;
            List<Usuarios> listajefes = contextUsuarios.GetJefesLab();
            ensay.JefesLista = listajefes;
            ensay.UsuariosLista = ListarTodosUsuarios;
            return View(ensay);
        }



        public ActionResult EditarEnsayoDesdePresupuesto(Presupuesto model)
        {

            EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<PedidoEnsayo> listaBuscarPedidoEnsayo = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdPresupuesto(model.IdPresupuestos);
            List<Ensayos> buscarEnsayoPorIdPedido = contextEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(listaBuscarPedidoEnsayo[0].IdPedidoEnsayo);

            Ensayos ensay = new Ensayos();
            ensay.IdEnsayos = buscarEnsayoPorIdPedido[0].IdEnsayos;
            ensay.NombreEnsayo = buscarEnsayoPorIdPedido[0].NombreEnsayo;
            ensay.FechaDeAlta = buscarEnsayoPorIdPedido[0].FechaDeAlta;
            ensay.FechaDeAlta2 = buscarEnsayoPorIdPedido[0].FechaDeAlta;
            ensay.ClienteCertificadora = buscarEnsayoPorIdPedido[0].ClienteCertificadora;
            ensay.ClienteProducto = buscarEnsayoPorIdPedido[0].ClienteProducto;
            ensay.Contacto = buscarEnsayoPorIdPedido[0].Contacto;
            ensay.FechaPedido = buscarEnsayoPorIdPedido[0].FechaPedido;
            ensay.FechaPedido2 = buscarEnsayoPorIdPedido[0].FechaPedido;
            ensay.DescripcionEnsayo = buscarEnsayoPorIdPedido[0].DescripcionEnsayo;
            ensay.Segmento = buscarEnsayoPorIdPedido[0].Segmento;
            ensay.tecnicoAsignado = buscarEnsayoPorIdPedido[0].tecnicoAsignado;
            ensay.jefeLaboratorioAsignado = buscarEnsayoPorIdPedido[0].jefeLaboratorioAsignado;
            ensay.StatusEnsayo = buscarEnsayoPorIdPedido[0].StatusEnsayo;
            ensay.RutaArchivo = buscarEnsayoPorIdPedido[0].RutaArchivo;
            ensay.pedidoEnsayo_IdPedidoEnsayo = buscarEnsayoPorIdPedido[0].pedidoEnsayo_IdPedidoEnsayo;

            ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (ensay.ProductosLista != null)
            {
                ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

            ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);
       


            UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

            List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
            Presupuesto presu = new Presupuesto();
            presu.UsuariosLista = ListarTodosUsuarios;
            ViewBag.MiListado = ListarTodosUsuarios;
            List<Usuarios> listajefes = contextUsuarios.GetJefesLab();
            ensay.JefesLista = listajefes;
            ensay.UsuariosLista = ListarTodosUsuarios;
            return RedirectToAction("EditarEnsayo", ensay);
        }
        public ActionResult EditarEnsayoDesdePedido(PedidoEnsayo model)
        {

            EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;

            List<Ensayos> buscarEnsayoPorIdPedido = contextEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(model.IdPedidoEnsayo);

            Ensayos ensay = new Ensayos();
            ensay.IdEnsayos = buscarEnsayoPorIdPedido[0].IdEnsayos;
            ensay.NombreEnsayo = buscarEnsayoPorIdPedido[0].NombreEnsayo;
            ensay.FechaDeAlta = buscarEnsayoPorIdPedido[0].FechaDeAlta;
            ensay.FechaDeAlta2 = buscarEnsayoPorIdPedido[0].FechaDeAlta;
            ensay.ClienteCertificadora = buscarEnsayoPorIdPedido[0].ClienteCertificadora;
            ensay.ClienteProducto = buscarEnsayoPorIdPedido[0].ClienteProducto;
            ensay.Contacto = buscarEnsayoPorIdPedido[0].Contacto;
            ensay.FechaPedido = buscarEnsayoPorIdPedido[0].FechaPedido;
            ensay.FechaPedido2 = buscarEnsayoPorIdPedido[0].FechaPedido;
            ensay.DescripcionEnsayo = buscarEnsayoPorIdPedido[0].DescripcionEnsayo;
            ensay.Segmento = buscarEnsayoPorIdPedido[0].Segmento;
            ensay.tecnicoAsignado = buscarEnsayoPorIdPedido[0].tecnicoAsignado;
            ensay.jefeLaboratorioAsignado = buscarEnsayoPorIdPedido[0].jefeLaboratorioAsignado;
            ensay.StatusEnsayo = buscarEnsayoPorIdPedido[0].StatusEnsayo;
            ensay.RutaArchivo = buscarEnsayoPorIdPedido[0].RutaArchivo;
            ensay.pedidoEnsayo_IdPedidoEnsayo = buscarEnsayoPorIdPedido[0].pedidoEnsayo_IdPedidoEnsayo;

            ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (ensay.ProductosLista != null)
            {
                ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

            ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);



            UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

            List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
            Presupuesto presu = new Presupuesto();
            presu.UsuariosLista = ListarTodosUsuarios;
            ViewBag.MiListado = ListarTodosUsuarios;
            List<Usuarios> listajefes = contextUsuarios.GetJefesLab();
            ensay.JefesLista = listajefes;
            ensay.UsuariosLista = ListarTodosUsuarios;
            return RedirectToAction("EditarEnsayo", ensay);
        }

        public ActionResult VerPedido(Ensayos model)
        {

            EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
            PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
            List<Ensayos> listaDeEnsayosx = contextEnsayos.BuscarEnsayoPorIdEnsayo(model.IdEnsayos);
            Ensayos ensay = new Ensayos();
            ensay.IdEnsayos = listaDeEnsayosx[0].IdEnsayos;
            ensay.NombreEnsayo = listaDeEnsayosx[0].NombreEnsayo;
            ensay.FechaDeAlta = listaDeEnsayosx[0].FechaDeAlta;
            ensay.FechaDeAlta2 = listaDeEnsayosx[0].FechaDeAlta;
            ensay.ClienteCertificadora = listaDeEnsayosx[0].ClienteCertificadora;
            ensay.ClienteProducto = listaDeEnsayosx[0].ClienteProducto;
            ensay.Contacto = listaDeEnsayosx[0].Contacto;
            ensay.FechaPedido = listaDeEnsayosx[0].FechaPedido;
            ensay.FechaPedido2 = listaDeEnsayosx[0].FechaPedido;
            ensay.DescripcionEnsayo = listaDeEnsayosx[0].DescripcionEnsayo;
            ensay.Segmento = listaDeEnsayosx[0].Segmento;
            ensay.tecnicoAsignado = listaDeEnsayosx[0].tecnicoAsignado;
            ensay.jefeLaboratorioAsignado = listaDeEnsayosx[0].jefeLaboratorioAsignado;
            ensay.StatusEnsayo = listaDeEnsayosx[0].StatusEnsayo;
            ensay.RutaArchivo = listaDeEnsayosx[0].RutaArchivo;
            ensay.pedidoEnsayo_IdPedidoEnsayo = listaDeEnsayosx[0].pedidoEnsayo_IdPedidoEnsayo;

            ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

            PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
            MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
            ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
            ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

            CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
            if (ensay.ProductosLista != null)
            {
                ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
            }
            EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
            ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

            ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
            ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

            AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;
            ensay.MuestrasLista = contextMuestras.BuscarUnaMuestra(ensay.pedidoEnsayo_IdPedidoEnsayo);
            ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);
            return View(ensay);
        }


        [HttpPost]
        public ActionResult GuardarEnsayo(Ensayos model)
        {
            try
            {
                var newFileName = string.Empty;
                EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
                Ensayos cot = new Ensayos();

                if (HttpContext.Request.Form.Files != null)
                {
                    var fileName = string.Empty;
                    var fileNameOriginal = string.Empty;
                    string PathDB = string.Empty;

                    var files = HttpContext.Request.Form.Files;

                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Getting FileName
                            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            fileNameOriginal = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            //Assigning Unique Filename (Guid)
                            //var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            //Getting file Extension
                            var FileExtension = Path.GetExtension(fileName);

                            // concating  FileName + FileExtension
                            //newFileName = myUniqueFileName + FileExtension;

                            // Combines two strings into a path.
                            fileName = Path.Combine(_environment.WebRootPath, "repos") + $@"\{fileName}";

                            // if you want to store path of folder in database
                            PathDB = "repos/" + fileNameOriginal;

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);                                
                                fs.Flush();
                            }
                            
                        }
                    }
                    cot.NombreEnsayo = model.NombreEnsayo;
                    cot.FechaDeAlta = model.FechaDeAlta;
                    cot.ClienteCertificadora = model.ClienteCertificadora;
                    cot.Contacto = model.Contacto;
                    cot.FechaPedido = model.FechaPedido;
                    cot.DescripcionEnsayo = model.DescripcionEnsayo;
                    cot.ClienteProducto = model.ClienteProducto;
                    cot.Segmento = model.Segmento;
                    cot.tecnicoAsignado = model.tecnicoAsignado;
                    cot.jefeLaboratorioAsignado = model.jefeLaboratorioAsignado;
                    cot.StatusEnsayo = model.StatusEnsayo;
                    //cot.RutaArchivo = model.RutaArchivo;
                    cot.RutaArchivo = PathDB.ToString();
                    cot.NombreArchivo = fileNameOriginal.ToString();
                    cot.IdEnsayos = model.IdEnsayos;
                    cot.pedidoEnsayo_IdPedidoEnsayo = model.pedidoEnsayo_IdPedidoEnsayo;
                    

                    bool resultado = contextEnsayos.ModificarEnsayo(cot);

                    if (resultado == true)
                    {
                        PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                        List<PedidoEnsayo> listaBuscarPedidoEnsayo = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(model.IdEnsayos);
                        List<Ensayos> buscarEnsayoPorIdPedido = contextEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(listaBuscarPedidoEnsayo[0].IdPedidoEnsayo);

                        Ensayos ensay = new Ensayos();
                        ensay.PedidoEnsayoLista = listaBuscarPedidoEnsayo;
                        ensay.IdEnsayos = buscarEnsayoPorIdPedido[0].IdEnsayos;
                        ensay.NombreEnsayo = buscarEnsayoPorIdPedido[0].NombreEnsayo;
                        ensay.FechaDeAlta = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                        ensay.FechaDeAlta2 = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                        ensay.ClienteCertificadora = buscarEnsayoPorIdPedido[0].ClienteCertificadora;
                        ensay.ClienteProducto = buscarEnsayoPorIdPedido[0].ClienteProducto;
                        ensay.Contacto = buscarEnsayoPorIdPedido[0].Contacto;
                        ensay.FechaPedido = buscarEnsayoPorIdPedido[0].FechaPedido;
                        ensay.FechaPedido2 = buscarEnsayoPorIdPedido[0].FechaPedido;
                        ensay.DescripcionEnsayo = buscarEnsayoPorIdPedido[0].DescripcionEnsayo;
                        ensay.Segmento = buscarEnsayoPorIdPedido[0].Segmento;
                        ensay.tecnicoAsignado = buscarEnsayoPorIdPedido[0].tecnicoAsignado;
                        ensay.jefeLaboratorioAsignado = buscarEnsayoPorIdPedido[0].jefeLaboratorioAsignado;
                        ensay.StatusEnsayo = buscarEnsayoPorIdPedido[0].StatusEnsayo;
                        ensay.RutaArchivo = buscarEnsayoPorIdPedido[0].RutaArchivo;
                        ensay.pedidoEnsayo_IdPedidoEnsayo = buscarEnsayoPorIdPedido[0].pedidoEnsayo_IdPedidoEnsayo;

                        ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

                        PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                        MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
                        ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                        ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                        ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                        CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
                        if (ensay.ProductosLista != null)
                        {
                            ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
                        }
                        EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
                        ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

                        ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
                        ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

                        AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

                        ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);



                        UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

                        List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
                        Presupuesto presu = new Presupuesto();
                        presu.UsuariosLista = ListarTodosUsuarios;
                        ViewBag.MiListado = ListarTodosUsuarios;
                        List<Usuarios> ListarJefes = contextUsuarios.GetJefesLab();
                        ensay.UsuariosLista = ListarTodosUsuarios;
                        ensay.JefesLista = ListarJefes;
                        return RedirectToAction("EditarEnsayo", ensay);
                    }
                    else
                    {
                        return RedirectToAction("Fracaso");
                    }
                }
                else
                {
                    //EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
                    //Ensayos cot = new Ensayos();
                    cot.NombreEnsayo = model.NombreEnsayo;
                    cot.FechaDeAlta = model.FechaDeAlta;
                    cot.ClienteCertificadora = model.ClienteCertificadora;
                    cot.Contacto = model.Contacto;
                    cot.FechaPedido = model.FechaPedido;
                    cot.DescripcionEnsayo = model.DescripcionEnsayo;
                    cot.ClienteProducto = model.ClienteProducto;
                    cot.Segmento = model.Segmento;
                    cot.tecnicoAsignado = model.tecnicoAsignado;
                    cot.jefeLaboratorioAsignado = model.jefeLaboratorioAsignado;
                    cot.StatusEnsayo = model.StatusEnsayo;
                    cot.RutaArchivo = model.RutaArchivo;
                    cot.IdEnsayos = model.IdEnsayos;
                    cot.pedidoEnsayo_IdPedidoEnsayo = model.pedidoEnsayo_IdPedidoEnsayo;

                    bool resultado = contextEnsayos.ModificarEnsayo(cot);

                    if (resultado == true)
                    {
                        PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                        List<PedidoEnsayo> listaBuscarPedidoEnsayo = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(model.IdEnsayos);
                        List<Ensayos> buscarEnsayoPorIdPedido = contextEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(listaBuscarPedidoEnsayo[0].IdPedidoEnsayo);

                        Ensayos ensay = new Ensayos();
                        ensay.PedidoEnsayoLista = listaBuscarPedidoEnsayo;
                        ensay.IdEnsayos = buscarEnsayoPorIdPedido[0].IdEnsayos;
                        ensay.NombreEnsayo = buscarEnsayoPorIdPedido[0].NombreEnsayo;
                        ensay.FechaDeAlta = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                        ensay.FechaDeAlta2 = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                        ensay.ClienteCertificadora = buscarEnsayoPorIdPedido[0].ClienteCertificadora;
                        ensay.ClienteProducto = buscarEnsayoPorIdPedido[0].ClienteProducto;
                        ensay.Contacto = buscarEnsayoPorIdPedido[0].Contacto;
                        ensay.FechaPedido = buscarEnsayoPorIdPedido[0].FechaPedido;
                        ensay.FechaPedido2 = buscarEnsayoPorIdPedido[0].FechaPedido;
                        ensay.DescripcionEnsayo = buscarEnsayoPorIdPedido[0].DescripcionEnsayo;
                        ensay.Segmento = buscarEnsayoPorIdPedido[0].Segmento;
                        ensay.tecnicoAsignado = buscarEnsayoPorIdPedido[0].tecnicoAsignado;
                        ensay.jefeLaboratorioAsignado = buscarEnsayoPorIdPedido[0].jefeLaboratorioAsignado;
                        ensay.StatusEnsayo = buscarEnsayoPorIdPedido[0].StatusEnsayo;
                        ensay.RutaArchivo = buscarEnsayoPorIdPedido[0].RutaArchivo;
                        ensay.pedidoEnsayo_IdPedidoEnsayo = buscarEnsayoPorIdPedido[0].pedidoEnsayo_IdPedidoEnsayo;

                        ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

                        PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                        MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
                        ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                        ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                        ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                        CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
                        if (ensay.ProductosLista != null)
                        {
                            ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
                        }
                        EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
                        ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

                        ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
                        ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

                        AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

                        ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);



                        UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

                        List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
                        Presupuesto presu = new Presupuesto();
                        presu.UsuariosLista = ListarTodosUsuarios;
                        ViewBag.MiListado = ListarTodosUsuarios;
                        List<Usuarios> ListarJefes = contextUsuarios.GetJefesLab();
                        ensay.UsuariosLista = ListarTodosUsuarios;
                        ensay.JefesLista = ListarJefes;
                        return RedirectToAction("EditarEnsayo", ensay);
                    }
                    else
                    {
                        return RedirectToAction("Fracaso");
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;

            }


        }



        [HttpPost]
        public ActionResult Rechazar(Ensayos model)
        {
            try
            {
                EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
                Ensayos cot = new Ensayos();
                cot.NombreEnsayo = model.NombreEnsayo;
                cot.FechaDeAlta = model.FechaDeAlta;
                cot.ClienteCertificadora = model.ClienteCertificadora;
                cot.Contacto = model.Contacto;
                cot.FechaPedido = model.FechaPedido;
                cot.DescripcionEnsayo = model.DescripcionEnsayo;
                cot.ClienteProducto = model.ClienteProducto;
                cot.Segmento = model.Segmento;
                cot.tecnicoAsignado = model.tecnicoAsignado;
                cot.jefeLaboratorioAsignado = model.jefeLaboratorioAsignado;
                cot.StatusEnsayo = "Rechazado";
                cot.RutaArchivo = model.RutaArchivo;
                cot.IdEnsayos = model.IdEnsayos;
                cot.pedidoEnsayo_IdPedidoEnsayo = model.pedidoEnsayo_IdPedidoEnsayo;

                bool resultado = contextEnsayos.RechazarEnsayo(cot);

                if (resultado == true)
                {
                    PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                    List<PedidoEnsayo> listaBuscarPedidoEnsayo = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(model.IdEnsayos);
                    List<Ensayos> buscarEnsayoPorIdPedido = contextEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(listaBuscarPedidoEnsayo[0].IdPedidoEnsayo);

                    Ensayos ensay = new Ensayos();
                    ensay.PedidoEnsayoLista = listaBuscarPedidoEnsayo;
                    ensay.IdEnsayos = buscarEnsayoPorIdPedido[0].IdEnsayos;
                    ensay.NombreEnsayo = buscarEnsayoPorIdPedido[0].NombreEnsayo;
                    ensay.FechaDeAlta = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                    ensay.FechaDeAlta2 = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                    ensay.ClienteCertificadora = buscarEnsayoPorIdPedido[0].ClienteCertificadora;
                    ensay.ClienteProducto = buscarEnsayoPorIdPedido[0].ClienteProducto;
                    ensay.Contacto = buscarEnsayoPorIdPedido[0].Contacto;
                    ensay.FechaPedido = buscarEnsayoPorIdPedido[0].FechaPedido;
                    ensay.FechaPedido2 = buscarEnsayoPorIdPedido[0].FechaPedido;
                    ensay.DescripcionEnsayo = buscarEnsayoPorIdPedido[0].DescripcionEnsayo;
                    ensay.Segmento = buscarEnsayoPorIdPedido[0].Segmento;
                    ensay.tecnicoAsignado = buscarEnsayoPorIdPedido[0].tecnicoAsignado;
                    ensay.jefeLaboratorioAsignado = buscarEnsayoPorIdPedido[0].jefeLaboratorioAsignado;
                    ensay.StatusEnsayo = buscarEnsayoPorIdPedido[0].StatusEnsayo;
                    ensay.RutaArchivo = buscarEnsayoPorIdPedido[0].RutaArchivo;
                    ensay.pedidoEnsayo_IdPedidoEnsayo = buscarEnsayoPorIdPedido[0].pedidoEnsayo_IdPedidoEnsayo;

                    ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

                    PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
                    ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                    ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                    ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                    CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
                    if (ensay.ProductosLista != null)
                    {
                        ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
                    }
                    EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
                    ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

                    ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
                    ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

                    AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

                    ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);



                    UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

                    List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
                    Presupuesto presu = new Presupuesto();
                    presu.UsuariosLista = ListarTodosUsuarios;
                    ViewBag.MiListado = ListarTodosUsuarios;
                    List<Usuarios> ListarJefes = contextUsuarios.GetJefesLab();
                    ensay.UsuariosLista = ListarTodosUsuarios;
                    ensay.JefesLista = ListarJefes;


                    return RedirectToAction("EditarEnsayo", ensay);
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
        public ActionResult Aprobar(Ensayos model)
        {
            try
            {
                EnsayosContext contextEnsayos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EnsayosContext)) as EnsayosContext;
                Ensayos cot = new Ensayos();
                cot.NombreEnsayo = model.NombreEnsayo;
                cot.FechaDeAlta = model.FechaDeAlta;
                cot.ClienteCertificadora = model.ClienteCertificadora;
                cot.Contacto = model.Contacto;
                cot.FechaPedido = model.FechaPedido;
                cot.DescripcionEnsayo = model.DescripcionEnsayo;
                cot.ClienteProducto = model.ClienteProducto;
                cot.Segmento = model.Segmento;
                cot.tecnicoAsignado = model.tecnicoAsignado;
                cot.jefeLaboratorioAsignado = model.jefeLaboratorioAsignado;
                cot.StatusEnsayo = "Aprobado";
                cot.RutaArchivo = model.RutaArchivo;
                cot.IdEnsayos = model.IdEnsayos;
                cot.pedidoEnsayo_IdPedidoEnsayo = model.pedidoEnsayo_IdPedidoEnsayo;

                bool resultado = contextEnsayos.AprobarEnsayo(cot);

                if (resultado == true)
                {
                    PedidoEnsayoContext contextPedidoEnsayo = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PedidoEnsayoContext)) as PedidoEnsayoContext;
                    List<PedidoEnsayo> listaBuscarPedidoEnsayo = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(model.IdEnsayos);
                    List<Ensayos> buscarEnsayoPorIdPedido = contextEnsayos.BuscarEnsayoPorIdPedidoDeEnsayo(listaBuscarPedidoEnsayo[0].IdPedidoEnsayo);

                    Ensayos ensay = new Ensayos();
                    ensay.PedidoEnsayoLista = listaBuscarPedidoEnsayo;
                    ensay.IdEnsayos = buscarEnsayoPorIdPedido[0].IdEnsayos;
                    ensay.NombreEnsayo = buscarEnsayoPorIdPedido[0].NombreEnsayo;
                    ensay.FechaDeAlta = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                    ensay.FechaDeAlta2 = buscarEnsayoPorIdPedido[0].FechaDeAlta;
                    ensay.ClienteCertificadora = buscarEnsayoPorIdPedido[0].ClienteCertificadora;
                    ensay.ClienteProducto = buscarEnsayoPorIdPedido[0].ClienteProducto;
                    ensay.Contacto = buscarEnsayoPorIdPedido[0].Contacto;
                    ensay.FechaPedido = buscarEnsayoPorIdPedido[0].FechaPedido;
                    ensay.FechaPedido2 = buscarEnsayoPorIdPedido[0].FechaPedido;
                    ensay.DescripcionEnsayo = buscarEnsayoPorIdPedido[0].DescripcionEnsayo;
                    ensay.Segmento = buscarEnsayoPorIdPedido[0].Segmento;
                    ensay.tecnicoAsignado = buscarEnsayoPorIdPedido[0].tecnicoAsignado;
                    ensay.jefeLaboratorioAsignado = buscarEnsayoPorIdPedido[0].jefeLaboratorioAsignado;
                    ensay.StatusEnsayo = buscarEnsayoPorIdPedido[0].StatusEnsayo;
                    ensay.RutaArchivo = buscarEnsayoPorIdPedido[0].RutaArchivo;
                    ensay.pedidoEnsayo_IdPedidoEnsayo = buscarEnsayoPorIdPedido[0].pedidoEnsayo_IdPedidoEnsayo;

                    ensay.PedidoEnsayoLista = contextPedidoEnsayo.BuscarPedidoEnsayoPorIdEnsayos(ensay.pedidoEnsayo_IdPedidoEnsayo);

                    PresupuestoContext contextPresupuesto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.PresupuestoContext)) as PresupuestoContext;
                    MuestrasContext contextMuestras = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.MuestrasContext)) as MuestrasContext;
                    ensay.PresupuestoLista = contextPresupuesto.BuscarUnPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                    ProductosContext contextProductos = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ProductosContext)) as ProductosContext;
                    ensay.ProductosLista = contextProductos.BuscarProductoPorIdPresupuesto(ensay.PedidoEnsayoLista[0].presupuestos_IdPresupuestos);

                    CotizacionesContext contextCotizaciones = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.CotizacionesContext)) as CotizacionesContext;
                    if (ensay.ProductosLista != null)
                    {
                        ensay.CotizacionesLista = contextCotizaciones.BuscarUnaCotizacion(ensay.ProductosLista[0].IdProductos);
                    }
                    EntidadesContext contextEntidades = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.EntidadesContext)) as EntidadesContext;
                    ensay.EntidadesLista = contextEntidades.BuscarUnaEntidad(ensay.PresupuestoLista[0].entidades_IdEntidades);

                    ContactoContext contextContacto = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.ContactoContext)) as ContactoContext;
                    ensay.ContactosLista = contextContacto.BuscarContactos(ensay.EntidadesLista[0].IdEntidades);

                    AprobacionContext contextoAprobacion = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.AprobacionContext)) as AprobacionContext;

                    ensay.AprobacionLista = contextoAprobacion.BuscarAprobacionPorIdPedidoEnsayo(ensay.pedidoEnsayo_IdPedidoEnsayo);



                    UsuariosContext contextUsuarios = HttpContext.RequestServices.GetService(typeof(Aplicacion_web_de_certificacion.Models.UsuariosContext)) as UsuariosContext;

                    List<Usuarios> ListarTodosUsuarios = contextUsuarios.GetAllUsuariosTecnicos();
                    Presupuesto presu = new Presupuesto();
                    presu.UsuariosLista = ListarTodosUsuarios;
                    ViewBag.MiListado = ListarTodosUsuarios;
                    List<Usuarios> ListarJefes = contextUsuarios.GetJefesLab();
                    ensay.UsuariosLista = ListarTodosUsuarios;
                    ensay.JefesLista = ListarJefes;

                    


                    return RedirectToAction("EditarEnsayo", ensay);
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

        public async Task<IActionResult> Descargar(string filename)
        {
            try
            {
                var path = Path.Combine(
                     Directory.GetCurrentDirectory(),
                     "wwwroot","repos", filename);
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}