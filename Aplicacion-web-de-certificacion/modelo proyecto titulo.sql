-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema lenor
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema lenor
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `lenor` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin ;
USE `lenor` ;

-- -----------------------------------------------------
-- Table `lenor`.`entidades`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`entidades` (
  `IdEntidades` INT(11) NOT NULL AUTO_INCREMENT,
  `RazonSocial` VARCHAR(160) NULL DEFAULT NULL,
  `RutEntidad` VARCHAR(160) NULL DEFAULT NULL,
  `RepresentanteLegal` VARCHAR(160) NULL DEFAULT NULL,
  `RutRepresentanteLegal` VARCHAR(160) NULL DEFAULT NULL,
  `InicioActividad` DATE NULL DEFAULT NULL,
  `TipoEntidad` VARCHAR(160) NULL DEFAULT NULL,
  `CondicionVenta` VARCHAR(160) NULL DEFAULT NULL,
  `SegmentoVenta` VARCHAR(160) NULL DEFAULT NULL,
  `Pais` VARCHAR(160) NULL DEFAULT NULL,
  `Ciudad` VARCHAR(160) NULL DEFAULT NULL,
  `Localidad` VARCHAR(160) NULL DEFAULT NULL,
  `Domicilio` VARCHAR(160) NULL DEFAULT NULL,
  PRIMARY KEY (`IdEntidades`))
ENGINE = InnoDB
AUTO_INCREMENT = 1
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `lenor`.`contactoentidad`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`contactoentidad` (
  `IdContactoEntidad` INT(11) NOT NULL AUTO_INCREMENT,
  `RepresentanteLegal` VARCHAR(160) NULL DEFAULT NULL,
  `ApellidoRepresentante` VARCHAR(160) NULL DEFAULT NULL,
  `TelefonoRepresentante` VARCHAR(160) NULL DEFAULT NULL,
  `EmailRepresentante` VARCHAR(160) NULL DEFAULT NULL,
  `ServicioTecnico` VARCHAR(3) NULL DEFAULT NULL,
  `Direccion` VARCHAR(160) NULL DEFAULT NULL,
  `Entidades_IdEntidades` INT(11) NOT NULL,
  PRIMARY KEY (`IdContactoEntidad`, `Entidades_IdEntidades`),
  INDEX `fk_ContactoEntidad_Entidades1_idx` (`Entidades_IdEntidades` ASC),
  CONSTRAINT `fk_ContactoEntidad_Entidades1`
    FOREIGN KEY (`Entidades_IdEntidades`)
    REFERENCES `lenor`.`entidades` (`IdEntidades`))
ENGINE = InnoDB
AUTO_INCREMENT = 1
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `lenor`.`lugar_de_ensayos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`lugar_de_ensayos` (
  `IdLugar_De_Ensayos` INT(11) NOT NULL AUTO_INCREMENT,
  `EntidadEncargada` VARCHAR(160) NULL DEFAULT NULL,
  `Direccion` VARCHAR(160) NULL DEFAULT NULL,
  `CorreoEncargado` VARCHAR(160) NULL DEFAULT NULL,
  `TelefonoEncargado` VARCHAR(160) NULL DEFAULT NULL,
  `Entidades_IdEntidades` INT(11) NOT NULL,
  PRIMARY KEY (`IdLugar_De_Ensayos`, `Entidades_IdEntidades`),
  INDEX `fk_Lugar_De_Ensayos_Entidades1_idx` (`Entidades_IdEntidades` ASC),
  CONSTRAINT `fk_Lugar_De_Ensayos_Entidades1`
    FOREIGN KEY (`Entidades_IdEntidades`)
    REFERENCES `lenor`.`entidades` (`IdEntidades`))
ENGINE = InnoDB
AUTO_INCREMENT = 1
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `lenor`.`presupuestos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`presupuestos` (
  `IdPresupuestos` INT(11) NOT NULL AUTO_INCREMENT,
  `Cliente` VARCHAR(160) NULL DEFAULT NULL,
  `Contacto` VARCHAR(160) NULL DEFAULT NULL,
  `SegmentoVenta` VARCHAR(160) NULL DEFAULT NULL,
  `CondicionVenta` VARCHAR(160) NULL DEFAULT NULL,
  `ComercialAsignado` VARCHAR(160) NULL DEFAULT NULL,
  `ClienteAsociado` VARCHAR(160) NULL DEFAULT NULL,
  `ContactoAsociado` VARCHAR(160) NULL DEFAULT NULL,
  `PaisFacturacion` VARCHAR(160) NULL DEFAULT NULL,
  `ClienteFacturacionPais` VARCHAR(160) NULL DEFAULT NULL,
  `ContactoClienteFacturacionPais` VARCHAR(160) NULL DEFAULT NULL,
  `FechaCreacion` DATE NULL,
  `StatusPresupuesto` INT NULL,
  `entidades_IdEntidades` INT(11) NOT NULL,
  PRIMARY KEY (`IdPresupuestos`, `entidades_IdEntidades`),
  INDEX `fk_presupuestos_entidades1_idx` (`entidades_IdEntidades` ASC),
  CONSTRAINT `fk_presupuestos_entidades1`
    FOREIGN KEY (`entidades_IdEntidades`)
    REFERENCES `lenor`.`entidades` (`IdEntidades`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `lenor`.`productos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`productos` (
  `IdProductos` INT NOT NULL AUTO_INCREMENT,
  `NombreProducto` VARCHAR(160) NULL,
  `MarcaProducto` VARCHAR(160) NULL,
  `ModeloProducto` VARCHAR(160) NULL,
  `FamiliaProducto` VARCHAR(160) NULL,
  `NumeroDeSerieProducto` VARCHAR(160) NULL,
  `NormaProducto` VARCHAR(160) NULL,
  `Descripcion` VARCHAR(100) NULL,
  `NombreFabricante` VARCHAR(45) NULL,
  `DireccionFabricante` VARCHAR(100) NULL,
  `presupuestos_IdPresupuestos` INT(11) NOT NULL,
  PRIMARY KEY (`IdProductos`, `presupuestos_IdPresupuestos`),
  INDEX `fk_productos_presupuestos1_idx` (`presupuestos_IdPresupuestos` ASC),
  CONSTRAINT `fk_productos_presupuestos1`
    FOREIGN KEY (`presupuestos_IdPresupuestos`)
    REFERENCES `lenor`.`presupuestos` (`IdPresupuestos`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`cotizaciones`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`cotizaciones` (
  `IdCotizacion` INT NOT NULL AUTO_INCREMENT,
  `NombreCotizacion` VARCHAR(45) NULL,
  `PrecioUnitario` DOUBLE NULL,
  `CantidadProductos` INT NULL,
  `SubTotal` DOUBLE NULL,
  `TotalPesoChile` DOUBLE NULL,
  `IVA` VARCHAR(45) NULL,
  `productos_IdProductos` INT NOT NULL,
  PRIMARY KEY (`IdCotizacion`, `productos_IdProductos`),
  INDEX `fk_cotizaciones_productos1_idx` (`productos_IdProductos` ASC),
  CONSTRAINT `fk_cotizaciones_productos1`
    FOREIGN KEY (`productos_IdProductos`)
    REFERENCES `lenor`.`productos` (`IdProductos`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`listaPrecios`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`listaPrecios` (
  `IdListaPrecios` INT NOT NULL AUTO_INCREMENT,
  `NombreLista` VARCHAR(160) NULL,
  `PrecioUnitario` DOUBLE NULL,
  PRIMARY KEY (`IdListaPrecios`))
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`pedidoEnsayo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`pedidoEnsayo` (
  `IdPedidoEnsayo` INT NOT NULL AUTO_INCREMENT,
  `ProtocoloAplicable` VARCHAR(160) NULL,
  `CondicionesDeEnsayo` VARCHAR(300) NULL,
  `AutorPedido` VARCHAR(160) NULL,
  `NumeroSec` VARCHAR(160) NULL,
  `StatusPedidoEnsayo` VARCHAR(160) NULL,
  `Comentarios` VARCHAR(300) NULL,
  `presupuestos_IdPresupuestos` INT(11) NOT NULL,
  PRIMARY KEY (`IdPedidoEnsayo`, `presupuestos_IdPresupuestos`),
  INDEX `fk_pedidoEnsayo_presupuestos1_idx` (`presupuestos_IdPresupuestos` ASC),
  CONSTRAINT `fk_pedidoEnsayo_presupuestos1`
    FOREIGN KEY (`presupuestos_IdPresupuestos`)
    REFERENCES `lenor`.`presupuestos` (`IdPresupuestos`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`muestras`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`muestras` (
  `IdMuestras` INT NOT NULL AUTO_INCREMENT,
  `FechaIngreso` DATE NULL,
  `Etiqueta` INT NULL,
  `Producto` VARCHAR(160) NULL,
  `Cantidad` INT NULL,
  `Marca` VARCHAR(160) NULL,
  `Modelo` VARCHAR(160) NULL,
  `Fabricante` VARCHAR(160) NULL,
  `PaisOrigen` VARCHAR(160) NULL,
  `DestinoMuestras` VARCHAR(160) NULL,
  `pedidoEnsayo_IdPedidoEnsayo` INT NOT NULL,
  PRIMARY KEY (`IdMuestras`, `pedidoEnsayo_IdPedidoEnsayo`),
  INDEX `fk_muestras_pedidoEnsayo1_idx` (`pedidoEnsayo_IdPedidoEnsayo` ASC),
  CONSTRAINT `fk_muestras_pedidoEnsayo1`
    FOREIGN KEY (`pedidoEnsayo_IdPedidoEnsayo`)
    REFERENCES `lenor`.`pedidoEnsayo` (`IdPedidoEnsayo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`ensayos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`ensayos` (
  `IdEnsayos` INT NOT NULL AUTO_INCREMENT,
  `NombreEnsayo` VARCHAR(160) NULL,
  `FechaDeAlta` DATE NULL,
  `ClienteCertificadora` VARCHAR(160) NULL,
  `Contacto` VARCHAR(160) NULL,
  `FechaPedido` DATE NULL,
  `DescripcionEnsayo` VARCHAR(300) NULL,
  `ClienteProducto` VARCHAR(160) NULL,
  `Segmento` VARCHAR(160) NULL,
  `tecnicoAsignado` VARCHAR(160) NULL,
  `jefeLaboratorioAsignado` VARCHAR(160) NULL,
  `StatusEnsayo` VARCHAR(160) NULL,
  `pedidoEnsayo_IdPedidoEnsayo` INT NOT NULL,
  PRIMARY KEY (`IdEnsayos`, `pedidoEnsayo_IdPedidoEnsayo`),
  INDEX `fk_ensayos_pedidoEnsayo1_idx` (`pedidoEnsayo_IdPedidoEnsayo` ASC),
  CONSTRAINT `fk_ensayos_pedidoEnsayo1`
    FOREIGN KEY (`pedidoEnsayo_IdPedidoEnsayo`)
    REFERENCES `lenor`.`pedidoEnsayo` (`IdPedidoEnsayo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`Aprobacion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`Aprobacion` (
  `IdAprobacion` INT NOT NULL AUTO_INCREMENT,
  `FechaCreacionAprobacion` DATE NULL,
  `UsuarioDesignadoAprobacion` VARCHAR(160) NULL,
  `ComentariosAprobacion` VARCHAR(160) NULL,
  `TipoAprobacion` VARCHAR(160) NULL,
  `EstadoAprobacion` VARCHAR(160) NULL,
  `PedidoEnsayo_IdPedidoEnsayo` INT NOT NULL,
  PRIMARY KEY (`IdAprobacion`, `PedidoEnsayo_IdPedidoEnsayo`),
  INDEX `fk_Aprobacion_pedidoEnsayo1_idx` (`PedidoEnsayo_IdPedidoEnsayo` ASC),
  CONSTRAINT `fk_Aprobacion_pedidoEnsayo1`
    FOREIGN KEY (`PedidoEnsayo_IdPedidoEnsayo`)
    REFERENCES `lenor`.`pedidoEnsayo` (`IdPedidoEnsayo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`Aspnetusers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`Aspnetusers` (
  `Id` VARCHAR(160) NOT NULL,
  `AccessFailedCount` VARCHAR(45) NULL,
  `EMail` VARCHAR(45) NULL,
  `PasswordHash` VARCHAR(160) NULL,
  `ConcurrencyStamp` VARCHAR(160) NULL,
  `EmailConfirmed` TINYINT NULL,
  `SecurityStamp` VARCHAR(160) NULL,
  `PhoneNumber` VARCHAR(45) NULL,
  `PhoneNumberConfirmed` VARCHAR(45) NULL,
  `TwoFactorEnabled` VARCHAR(45) NULL,
  `UserName` VARCHAR(45) NULL,
  `LockoutEnabled` VARCHAR(45) NULL,
  `LockoutEndDateUtc` VARCHAR(45) NULL,
  `LockoutEnd` VARCHAR(45) NULL,
  `NormalizedEmail` VARCHAR(45) NULL,
  `NormalizedUserName` VARCHAR(45) NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `lenor`.`AspNetUserClaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`AspNetUserClaims` (
  `IdAsp` VARCHAR(160) NOT NULL,
  `Id` VARCHAR(45) NULL,
  `ClaimType` VARCHAR(45) NULL,
  `ClaimValue` VARCHAR(45) NULL,
  `UserId` VARCHAR(160) NOT NULL,
  PRIMARY KEY (`IdAsp`, `UserId`),
  INDEX `fk_AspNetUserClaims_Aspnetusers1_idx` (`UserId` ASC),
  CONSTRAINT `fk_AspNetUserClaims_Aspnetusers1`
    FOREIGN KEY (`UserId`)
    REFERENCES `lenor`.`Aspnetusers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `lenor`.`AspNetRoles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`AspNetRoles` (
  `Id` VARCHAR(160) NOT NULL,
  `Name` VARCHAR(45) NULL,
  `NormalizedName` VARCHAR(45) NULL,
  `ConcurrencyStamp` VARCHAR(45) NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `lenor`.`AspNetUserRoles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`AspNetUserRoles` (
  `UserId` VARCHAR(160) NOT NULL,
  `RoleId` VARCHAR(160) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  INDEX `fk_AspNetUserRoles_AspNetRoles1_idx` (`RoleId` ASC),
  CONSTRAINT `fk_AspNetUserRoles_Aspnetusers1`
    FOREIGN KEY (`UserId`)
    REFERENCES `lenor`.`Aspnetusers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_AspNetUserRoles_AspNetRoles1`
    FOREIGN KEY (`RoleId`)
    REFERENCES `lenor`.`AspNetRoles` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `lenor`.`AspNetUserLogins`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`AspNetUserLogins` (
  `IdUserLogin` VARCHAR(160) NOT NULL,
  `LoginProvider` VARCHAR(160) NULL,
  `ProviderKey` VARCHAR(45) NULL,
  `UserId` VARCHAR(160) NOT NULL,
  PRIMARY KEY (`IdUserLogin`, `UserId`),
  INDEX `fk_AspNetUserLogins_Aspnetusers1_idx` (`UserId` ASC),
  CONSTRAINT `fk_AspNetUserLogins_Aspnetusers1`
    FOREIGN KEY (`UserId`)
    REFERENCES `lenor`.`Aspnetusers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `lenor`.`AspNetRoleClaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`AspNetRoleClaims` (
  `Id` VARCHAR(160) NOT NULL,
  `ClaimType` VARCHAR(45) NULL,
  `ClaimValue` VARCHAR(45) NULL,
  `RoleId` VARCHAR(160) NOT NULL,
  PRIMARY KEY (`Id`, `RoleId`),
  INDEX `fk_AspNetRoleClaims_AspNetRoles1_idx` (`RoleId` ASC),
  CONSTRAINT `fk_AspNetRoleClaims_AspNetRoles1`
    FOREIGN KEY (`RoleId`)
    REFERENCES `lenor`.`AspNetRoles` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `lenor`.`archivos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lenor`.`archivos` (
  `IdArchivos` INT NOT NULL AUTO_INCREMENT,
  `RutaArchivos` VARCHAR(255) NULL,
  `NombreArchivos` VARCHAR(255) NULL,
  `ensayos_IdEnsayos` INT NOT NULL,
  PRIMARY KEY (`IdArchivos`, `ensayos_IdEnsayos`),
  INDEX `fk_archivos_ensayos1_idx` (`ensayos_IdEnsayos` ASC),
  CONSTRAINT `fk_archivos_ensayos1`
    FOREIGN KEY (`ensayos_IdEnsayos`)
    REFERENCES `lenor`.`ensayos` (`IdEnsayos`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
