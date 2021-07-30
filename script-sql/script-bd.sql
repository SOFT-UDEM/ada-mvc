/*
			****** Script Base de datos ********
	**** Gestión de Proyectos de Software ****
*/

USE master
GO

--Comprobar si existe la base de datos.
IF EXISTS(SELECT * FROM sys.databases WHERE name='bdsoft')
BEGIN
	ALTER DATABASE bdsoft SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE bdsoft
END
GO

--Creamos la base de datos.
CREATE DATABASE bdsoft
GO

--Comprobar si existe el usuario userbdsoft.
IF EXISTS(SELECT name FROM master.dbo.syslogins WHERE name='userbdsoft')
BEGIN
	DROP LOGIN userbdsoft
END
GO

--Crear login para el servidor.
CREATE LOGIN userbdsoft WITH PASSWORD = '$Udem2021*', DEFAULT_DATABASE = bdsoft
GO

USE bdsoft
GO

--Crear usuario para base de datos.
CREATE USER userbdsoft FOR LOGIN userbdsoft
GO

--Asignar rol de usuario.
EXEC sp_addrolemember 'db_owner', 'userbdsoft'
GO

/*
	Creaci�n de tablas.
*/

-- Tabla LogTablas
CREATE TABLE LogTablas
(
	IdLog INT IDENTITY(1,1) NOT NULL,
	NombreTabla VARCHAR(255) NOT NULL,
	Accion VARCHAR(6) NOT NULL,
	Descripcion VARCHAR(255) NOT NULL,
	Usuario VARCHAR(20) NOT NULL,
	Fecha DATETIME NOT NULL
)
GO

-- Tabla Usuarios.
CREATE TABLE Usuarios (
	IdUsuario INT IDENTITY,
	Nombre VARCHAR(64),
	Apellido VARCHAR(64),
	Cargo VARCHAR(64),
	RolDeAcceso VARCHAR(20),
	UserName VARCHAR(64),
	Password VARCHAR(8000),
	Observacion VARCHAR(100)
)
GO
-- Fin tabla Usuarios.

-- Tabla EquiposTecnologicos.
CREATE TABLE EquiposTecnologicos (
	IdEquipo INT IDENTITY,
	Descripcion VARCHAR(255),
	Modelo VARCHAR(64),
	Marca VARCHAR(64),
	NumeroDeSerie VARCHAR(64),
	CodigoInterno VARCHAR(64),
	Estado VARCHAR(20),
	CodEmpleado INT,
	ValorMonetario MONEY,
	CreadoPorUserName INT,
	Observacion VARCHAR(100)
)
GO
-- Fin tabla EquiposTecnologicos.

-- Tabla Areas.
CREATE TABLE Areas (
	IdArea INT IDENTITY,
	Nombre VARCHAR(64),
	Funcion VARCHAR(140),
	Observacion VARCHAR(100)
)
GO
-- Fin tabla areas.

-- Tabla Empleados.
CREATE TABLE Empleados (
	CodEmpleado INT IDENTITY,
	Nombre VARCHAR(64),
	Apellido VARCHAR(64),
	Identificacion VARCHAR(20),
	Cargo VARCHAR(20),
	IdArea INT,
	Observacion VARCHAR(100),
)
GO
-- Fin tabla Empleados.

-- Tabla Asistencias.
CREATE TABLE Asistencias (
	IdAsistencia INT IDENTITY,
	CodEmpleado INT
)
GO
-- Fin tabla Asistencias.

-- Tabla DetalleDeAsistencias.
CREATE TABLE DetalleDeAsistencias (
	IdDetalle INT IDENTITY,
	IdAsistencia INT,
	FechaHoraEntrada DATETIME,
	FechaHoraSalida DATETIME,
	Observacion VARCHAR(100)
)
GO
-- Fin tabla DetalleDeAsistencias.

/*
	Creaci�n de restricciones.
*/

-- Clave primaria de tabla LogTablas
ALTER TABLE LogTablas ADD CONSTRAINT pk_logtablas_IdLog 
PRIMARY KEY NONCLUSTERED(IdLog)
GO

-- Creando restricci�n para campo Accion (Solo permite INSERT, DELETE, UPDATE)
ALTER TABLE LogTablas ADD CONSTRAINT ck_logtablas_Accion 
CHECK(Accion in ('INSERT', 'DELETE', 'UPDATE'))
GO

-- Clave primaria de tabla Usuarios.
ALTER TABLE Usuarios ADD CONSTRAINT pk_usuarios_IdUsuario
PRIMARY KEY NONCLUSTERED(IdUsuario)
GO

-- Clave primaria de tabla EquiposTecnologicos.
ALTER TABLE EquiposTecnologicos ADD CONSTRAINT pk_EquiposTecnologicos_IdEquipo
PRIMARY KEY NONCLUSTERED(IdEquipo)
GO

-- Clave primaria de tabla Areas.
ALTER TABLE Areas ADD CONSTRAINT pk_Areas_IdArea
PRIMARY KEY NONCLUSTERED(IdArea)
GO

-- Clave primaria de tabla Empleados.
ALTER TABLE Empleados ADD CONSTRAINT pk_Empleados_CodEmpleado
PRIMARY KEY NONCLUSTERED(CodEmpleado)
GO

-- Clave primaria de tabla Asistencias.
ALTER TABLE Asistencias ADD CONSTRAINT pk_Asistencias_IdAsistencia
PRIMARY KEY NONCLUSTERED(IdAsistencia)
GO

-- Clave primaria de tabla DetalleDeAsistencias.
ALTER TABLE DetalleDeAsistencias ADD CONSTRAINT pk_DetalleDeAsistencias_IdDetalle
PRIMARY KEY NONCLUSTERED(IdDetalle)
GO

-- Claves foraneas en tabla EquiposTecnologicos.
ALTER TABLE EquiposTecnologicos ADD CONSTRAINT fk_EquiposTecnologicos_CodEmpleado
FOREIGN KEY(CodEmpleado) REFERENCES Empleados(CodEmpleado) ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE EquiposTecnologicos ADD CONSTRAINT fk_EquiposTecnologicos_CreadoPorUserName
FOREIGN KEY(CreadoPorUserName) REFERENCES Usuarios(IdUsuario) ON UPDATE CASCADE ON DELETE CASCADE
GO

-- Clave foranea en tabla Empleados.
ALTER TABLE Empleados ADD CONSTRAINT fk_Empleados_IdArea
FOREIGN KEY(IdArea) REFERENCES Areas(IdArea) ON UPDATE CASCADE ON DELETE CASCADE
GO

-- Clave foranea en tabla Asistencias.
ALTER TABLE Asistencias ADD CONSTRAINT fk_Asistencias_CodEmpleado
FOREIGN KEY(CodEmpleado) REFERENCES Empleados(CodEmpleado) ON UPDATE CASCADE ON DELETE CASCADE
GO

-- Clave foranea en tabla DetalleDeAsistencias.
ALTER TABLE DetalleDeAsistencias ADD CONSTRAINT fk_DetalleDeAsistencias_IdAsistencia
FOREIGN KEY(IdAsistencia) REFERENCES Asistencias(IdAsistencia) ON UPDATE CASCADE ON DELETE CASCADE
GO

-- Creación de usuario por defecto del sistema
INSERT INTO bdsoft..Usuarios 
(
	Nombre, 
	Apellido, 
	Cargo, 
	RolDeAcceso, 
	UserName, 
	Password, 
	Observacion
)
VALUES
(
	'Usuario',
	'Por defecto',
	'Administrador',
	'Admin',
	'prueba',
	'0d9d09e157f7c29a43c02e57f081915d6fe2f10da1710672086f8ad73bb95cb2',
	'Este usuario es creado por defecto al momento de crear la base de datos'
)
GO