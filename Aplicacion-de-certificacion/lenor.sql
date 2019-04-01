-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 01-04-2019 a las 21:44:16
-- Versión del servidor: 10.1.37-MariaDB
-- Versión de PHP: 7.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `lenor`
--
CREATE DATABASE IF NOT EXISTS `lenor` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin;
USE `lenor`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aprobacion`
--

CREATE TABLE `aprobacion` (
  `IdAprobacion` int(11) NOT NULL,
  `FechaCreacionAprobacion` date DEFAULT NULL,
  `UsuarioDesignadoAprobacion` varchar(160) COLLATE utf8_bin DEFAULT '',
  `ComentariosAprobacion` varchar(160) COLLATE utf8_bin DEFAULT '',
  `TipoAprobacion` varchar(160) COLLATE utf8_bin DEFAULT '',
  `EstadoAprobacion` varchar(160) COLLATE utf8_bin DEFAULT '',
  `PedidoEnsayo_IdPedidoEnsayo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `archivos`
--

CREATE TABLE `archivos` (
  `IdArchivos` int(11) NOT NULL,
  `RutaArchivos` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `NombreArchivos` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `ensayos_IdEnsayos` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aspnetroleclaims`
--

CREATE TABLE `aspnetroleclaims` (
  `Id` varchar(160) COLLATE utf8_bin NOT NULL,
  `ClaimType` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `ClaimValue` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `RoleId` varchar(160) COLLATE utf8_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aspnetroles`
--

CREATE TABLE `aspnetroles` (
  `Id` varchar(160) COLLATE utf8_bin NOT NULL,
  `Name` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `NormalizedName` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `ConcurrencyStamp` varchar(45) COLLATE utf8_bin DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aspnetuserclaims`
--

CREATE TABLE `aspnetuserclaims` (
  `IdAsp` varchar(160) COLLATE utf8_bin NOT NULL,
  `Id` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `ClaimType` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `ClaimValue` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `UserId` varchar(160) COLLATE utf8_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aspnetuserlogins`
--

CREATE TABLE `aspnetuserlogins` (
  `IdUserLogin` varchar(160) COLLATE utf8_bin NOT NULL,
  `LoginProvider` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ProviderKey` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `UserId` varchar(160) COLLATE utf8_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aspnetuserroles`
--

CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(160) COLLATE utf8_bin NOT NULL,
  `RoleId` varchar(160) COLLATE utf8_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aspnetusers`
--

CREATE TABLE `aspnetusers` (
  `Id` varchar(160) COLLATE utf8_bin NOT NULL,
  `AccessFailedCount` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `EMail` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `PasswordHash` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ConcurrencyStamp` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `EmailConfirmed` tinyint(4) DEFAULT NULL,
  `SecurityStamp` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `PhoneNumber` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `PhoneNumberConfirmed` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `TwoFactorEnabled` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `UserName` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `LockoutEnabled` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `LockoutEndDateUtc` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `LockoutEnd` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `NormalizedEmail` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `NormalizedUserName` varchar(45) COLLATE utf8_bin DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contactoentidad`
--

CREATE TABLE `contactoentidad` (
  `IdContactoEntidad` int(11) NOT NULL,
  `RepresentanteLegal` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ApellidoRepresentante` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `TelefonoRepresentante` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `EmailRepresentante` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ServicioTecnico` varchar(3) COLLATE utf8_bin DEFAULT NULL,
  `Direccion` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Entidades_IdEntidades` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cotizaciones`
--

CREATE TABLE `cotizaciones` (
  `IdCotizacion` int(11) NOT NULL,
  `NombreCotizacion` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `PrecioUnitario` double DEFAULT NULL,
  `CantidadProductos` int(11) DEFAULT NULL,
  `SubTotal` double DEFAULT NULL,
  `TotalPesoChile` double DEFAULT NULL,
  `IVA` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `productos_IdProductos` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ensayos`
--

CREATE TABLE `ensayos` (
  `IdEnsayos` int(11) NOT NULL,
  `NombreEnsayo` varchar(160) COLLATE utf8_bin DEFAULT '',
  `FechaDeAlta` date DEFAULT NULL,
  `ClienteCertificadora` varchar(160) COLLATE utf8_bin DEFAULT '',
  `Contacto` varchar(160) COLLATE utf8_bin DEFAULT '',
  `FechaPedido` date DEFAULT NULL,
  `DescripcionEnsayo` varchar(300) COLLATE utf8_bin DEFAULT '',
  `ClienteProducto` varchar(160) COLLATE utf8_bin DEFAULT '',
  `Segmento` varchar(160) COLLATE utf8_bin DEFAULT '',
  `tecnicoAsignado` varchar(160) COLLATE utf8_bin DEFAULT '',
  `jefeLaboratorioAsignado` varchar(160) COLLATE utf8_bin DEFAULT '',
  `StatusEnsayo` varchar(160) COLLATE utf8_bin DEFAULT '',
  `pedidoEnsayo_IdPedidoEnsayo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `entidades`
--

CREATE TABLE `entidades` (
  `IdEntidades` int(11) NOT NULL,
  `RazonSocial` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `RutEntidad` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `RepresentanteLegal` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `RutRepresentanteLegal` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `InicioActividad` date DEFAULT NULL,
  `TipoEntidad` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `CondicionVenta` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `SegmentoVenta` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Pais` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Ciudad` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Localidad` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Domicilio` varchar(160) COLLATE utf8_bin DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `listaprecios`
--

CREATE TABLE `listaprecios` (
  `IdListaPrecios` int(11) NOT NULL,
  `NombreLista` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `PrecioUnitario` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `lugar_de_ensayos`
--

CREATE TABLE `lugar_de_ensayos` (
  `IdLugar_De_Ensayos` int(11) NOT NULL,
  `EntidadEncargada` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Direccion` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `CorreoEncargado` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `TelefonoEncargado` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Entidades_IdEntidades` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `muestras`
--

CREATE TABLE `muestras` (
  `IdMuestras` int(11) NOT NULL,
  `FechaIngreso` date DEFAULT NULL,
  `Etiqueta` int(11) DEFAULT NULL,
  `Producto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Cantidad` int(11) DEFAULT NULL,
  `Marca` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Modelo` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Fabricante` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `PaisOrigen` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `DestinoMuestras` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `pedidoEnsayo_IdPedidoEnsayo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pedidoensayo`
--

CREATE TABLE `pedidoensayo` (
  `IdPedidoEnsayo` int(11) NOT NULL,
  `ProtocoloAplicable` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `CondicionesDeEnsayo` varchar(300) COLLATE utf8_bin DEFAULT NULL,
  `AutorPedido` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `NumeroSec` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `StatusPedidoEnsayo` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Comentarios` varchar(300) COLLATE utf8_bin DEFAULT NULL,
  `presupuestos_IdPresupuestos` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `presupuestos`
--

CREATE TABLE `presupuestos` (
  `IdPresupuestos` int(11) NOT NULL,
  `Cliente` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Contacto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `SegmentoVenta` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `CondicionVenta` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ComercialAsignado` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ClienteAsociado` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ContactoAsociado` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `PaisFacturacion` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ClienteFacturacionPais` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ContactoClienteFacturacionPais` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `FechaCreacion` date DEFAULT NULL,
  `StatusPresupuesto` int(11) DEFAULT NULL,
  `entidades_IdEntidades` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `IdProductos` int(11) NOT NULL,
  `NombreProducto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `MarcaProducto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `ModeloProducto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `FamiliaProducto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `NumeroDeSerieProducto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `NormaProducto` varchar(160) COLLATE utf8_bin DEFAULT NULL,
  `Descripcion` varchar(100) COLLATE utf8_bin DEFAULT NULL,
  `NombreFabricante` varchar(45) COLLATE utf8_bin DEFAULT NULL,
  `DireccionFabricante` varchar(100) COLLATE utf8_bin DEFAULT NULL,
  `presupuestos_IdPresupuestos` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `aprobacion`
--
ALTER TABLE `aprobacion`
  ADD PRIMARY KEY (`IdAprobacion`,`PedidoEnsayo_IdPedidoEnsayo`),
  ADD KEY `fk_Aprobacion_pedidoEnsayo1_idx` (`PedidoEnsayo_IdPedidoEnsayo`);

--
-- Indices de la tabla `archivos`
--
ALTER TABLE `archivos`
  ADD PRIMARY KEY (`IdArchivos`,`ensayos_IdEnsayos`),
  ADD KEY `fk_archivos_ensayos1_idx` (`ensayos_IdEnsayos`);

--
-- Indices de la tabla `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD PRIMARY KEY (`Id`,`RoleId`),
  ADD KEY `fk_AspNetRoleClaims_AspNetRoles1_idx` (`RoleId`);

--
-- Indices de la tabla `aspnetroles`
--
ALTER TABLE `aspnetroles`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD PRIMARY KEY (`IdAsp`,`UserId`),
  ADD KEY `fk_AspNetUserClaims_Aspnetusers1_idx` (`UserId`);

--
-- Indices de la tabla `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD PRIMARY KEY (`IdUserLogin`,`UserId`),
  ADD KEY `fk_AspNetUserLogins_Aspnetusers1_idx` (`UserId`);

--
-- Indices de la tabla `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `fk_AspNetUserRoles_AspNetRoles1_idx` (`RoleId`);

--
-- Indices de la tabla `aspnetusers`
--
ALTER TABLE `aspnetusers`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `contactoentidad`
--
ALTER TABLE `contactoentidad`
  ADD PRIMARY KEY (`IdContactoEntidad`,`Entidades_IdEntidades`),
  ADD KEY `fk_ContactoEntidad_Entidades1_idx` (`Entidades_IdEntidades`);

--
-- Indices de la tabla `cotizaciones`
--
ALTER TABLE `cotizaciones`
  ADD PRIMARY KEY (`IdCotizacion`,`productos_IdProductos`),
  ADD KEY `fk_cotizaciones_productos1_idx` (`productos_IdProductos`);

--
-- Indices de la tabla `ensayos`
--
ALTER TABLE `ensayos`
  ADD PRIMARY KEY (`IdEnsayos`,`pedidoEnsayo_IdPedidoEnsayo`),
  ADD KEY `fk_ensayos_pedidoEnsayo1_idx` (`pedidoEnsayo_IdPedidoEnsayo`);

--
-- Indices de la tabla `entidades`
--
ALTER TABLE `entidades`
  ADD PRIMARY KEY (`IdEntidades`);

--
-- Indices de la tabla `listaprecios`
--
ALTER TABLE `listaprecios`
  ADD PRIMARY KEY (`IdListaPrecios`);

--
-- Indices de la tabla `lugar_de_ensayos`
--
ALTER TABLE `lugar_de_ensayos`
  ADD PRIMARY KEY (`IdLugar_De_Ensayos`,`Entidades_IdEntidades`),
  ADD KEY `fk_Lugar_De_Ensayos_Entidades1_idx` (`Entidades_IdEntidades`);

--
-- Indices de la tabla `muestras`
--
ALTER TABLE `muestras`
  ADD PRIMARY KEY (`IdMuestras`,`pedidoEnsayo_IdPedidoEnsayo`),
  ADD KEY `fk_muestras_pedidoEnsayo1_idx` (`pedidoEnsayo_IdPedidoEnsayo`);

--
-- Indices de la tabla `pedidoensayo`
--
ALTER TABLE `pedidoensayo`
  ADD PRIMARY KEY (`IdPedidoEnsayo`,`presupuestos_IdPresupuestos`),
  ADD KEY `fk_pedidoEnsayo_presupuestos1_idx` (`presupuestos_IdPresupuestos`);

--
-- Indices de la tabla `presupuestos`
--
ALTER TABLE `presupuestos`
  ADD PRIMARY KEY (`IdPresupuestos`,`entidades_IdEntidades`),
  ADD KEY `fk_presupuestos_entidades1_idx` (`entidades_IdEntidades`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`IdProductos`,`presupuestos_IdPresupuestos`),
  ADD KEY `fk_productos_presupuestos1_idx` (`presupuestos_IdPresupuestos`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `aprobacion`
--
ALTER TABLE `aprobacion`
  MODIFY `IdAprobacion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `archivos`
--
ALTER TABLE `archivos`
  MODIFY `IdArchivos` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `contactoentidad`
--
ALTER TABLE `contactoentidad`
  MODIFY `IdContactoEntidad` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `cotizaciones`
--
ALTER TABLE `cotizaciones`
  MODIFY `IdCotizacion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ensayos`
--
ALTER TABLE `ensayos`
  MODIFY `IdEnsayos` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `entidades`
--
ALTER TABLE `entidades`
  MODIFY `IdEntidades` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `listaprecios`
--
ALTER TABLE `listaprecios`
  MODIFY `IdListaPrecios` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `lugar_de_ensayos`
--
ALTER TABLE `lugar_de_ensayos`
  MODIFY `IdLugar_De_Ensayos` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `muestras`
--
ALTER TABLE `muestras`
  MODIFY `IdMuestras` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `pedidoensayo`
--
ALTER TABLE `pedidoensayo`
  MODIFY `IdPedidoEnsayo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `presupuestos`
--
ALTER TABLE `presupuestos`
  MODIFY `IdPresupuestos` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `productos`
--
ALTER TABLE `productos`
  MODIFY `IdProductos` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `aprobacion`
--
ALTER TABLE `aprobacion`
  ADD CONSTRAINT `fk_Aprobacion_pedidoEnsayo1` FOREIGN KEY (`PedidoEnsayo_IdPedidoEnsayo`) REFERENCES `pedidoensayo` (`IdPedidoEnsayo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `archivos`
--
ALTER TABLE `archivos`
  ADD CONSTRAINT `fk_archivos_ensayos1` FOREIGN KEY (`ensayos_IdEnsayos`) REFERENCES `ensayos` (`IdEnsayos`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD CONSTRAINT `fk_AspNetRoleClaims_AspNetRoles1` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD CONSTRAINT `fk_AspNetUserClaims_Aspnetusers1` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD CONSTRAINT `fk_AspNetUserLogins_Aspnetusers1` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD CONSTRAINT `fk_AspNetUserRoles_AspNetRoles1` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_AspNetUserRoles_Aspnetusers1` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `contactoentidad`
--
ALTER TABLE `contactoentidad`
  ADD CONSTRAINT `fk_ContactoEntidad_Entidades1` FOREIGN KEY (`Entidades_IdEntidades`) REFERENCES `entidades` (`IdEntidades`);

--
-- Filtros para la tabla `cotizaciones`
--
ALTER TABLE `cotizaciones`
  ADD CONSTRAINT `fk_cotizaciones_productos1` FOREIGN KEY (`productos_IdProductos`) REFERENCES `productos` (`IdProductos`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `ensayos`
--
ALTER TABLE `ensayos`
  ADD CONSTRAINT `fk_ensayos_pedidoEnsayo1` FOREIGN KEY (`pedidoEnsayo_IdPedidoEnsayo`) REFERENCES `pedidoensayo` (`IdPedidoEnsayo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `lugar_de_ensayos`
--
ALTER TABLE `lugar_de_ensayos`
  ADD CONSTRAINT `fk_Lugar_De_Ensayos_Entidades1` FOREIGN KEY (`Entidades_IdEntidades`) REFERENCES `entidades` (`IdEntidades`);

--
-- Filtros para la tabla `muestras`
--
ALTER TABLE `muestras`
  ADD CONSTRAINT `fk_muestras_pedidoEnsayo1` FOREIGN KEY (`pedidoEnsayo_IdPedidoEnsayo`) REFERENCES `pedidoensayo` (`IdPedidoEnsayo`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `pedidoensayo`
--
ALTER TABLE `pedidoensayo`
  ADD CONSTRAINT `fk_pedidoEnsayo_presupuestos1` FOREIGN KEY (`presupuestos_IdPresupuestos`) REFERENCES `presupuestos` (`IdPresupuestos`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `presupuestos`
--
ALTER TABLE `presupuestos`
  ADD CONSTRAINT `fk_presupuestos_entidades1` FOREIGN KEY (`entidades_IdEntidades`) REFERENCES `entidades` (`IdEntidades`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `productos`
--
ALTER TABLE `productos`
  ADD CONSTRAINT `fk_productos_presupuestos1` FOREIGN KEY (`presupuestos_IdPresupuestos`) REFERENCES `presupuestos` (`IdPresupuestos`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
