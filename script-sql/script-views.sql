/*
	Script crear vistas
*/

USE bdsoft
GO

-- Vista para reporte de empleados por área.
CREATE VIEW Vw_ListaEmpleadosPorAreas AS
SELECT t1.CodEmpleado, t1.Apellido + ' ' + t1.Nombre AS Nombre_Empleado ,t1.Identificacion, t1.Cargo, t2.Nombre as Area, t1.Observacion
FROM Empleados as t1
INNER JOIN Areas as t2
ON t1.IdArea = t2.IdArea
GO

-- Vista para reporte de inventario general.
CREATE VIEW Vw_ListaDeTodosLosEquipos AS
SELECT t1.IdEquipo, t1.Descripcion, t1.Modelo, t1.Marca, t1.NumeroDeSerie, t1.CodigoInterno, t1.Estado, t2.Nombre + ' ' + t2.Apellido AS Empleado, t1.ValorMonetario, t3.UserName AS Usuario, t1.Observacion 
FROM EquiposTecnologicos AS t1
INNER JOIN Empleados AS t2
ON t1.CodEmpleado = t2.CodEmpleado
INNER JOIN Usuarios AS t3
ON t1.CreadoPorUserName = t3.IdUsuario
GO

-- Vista para reporte de inventario por área.
CREATE VIEW Vw_ListaDeEquiposPorAreas AS
SELECT t1.IdEquipo, t4.Nombre AS Area ,t1.Descripcion, t1.Modelo, t1.Marca, t1.NumeroDeSerie, t1.CodigoInterno, t1.Estado, t2.Nombre + ' ' + t2.Apellido AS Empleado, t1.ValorMonetario, t3.UserName AS Usuario, t1.Observacion 
FROM EquiposTecnologicos AS t1
INNER JOIN Empleados AS t2
ON t1.CodEmpleado = t2.CodEmpleado
INNER JOIN Usuarios AS t3
ON t1.CreadoPorUserName = t3.IdUsuario
INNER JOIN Areas AS T4
ON t2.IdArea = t4.IdArea
GO

-- Vista para reporte de inventario costos.
CREATE VIEW Vw_ListaDeInventarioCostos AS
SELECT t1.IdEquipo, t1.Descripcion, t1.Modelo, t1.Marca, t1.NumeroDeSerie, t1.CodigoInterno, t1.Estado, t2.Nombre + ' ' + t2.Apellido AS Empleado, t1.ValorMonetario, t3.UserName AS Usuario, t1.Observacion 
FROM EquiposTecnologicos AS t1
INNER JOIN Empleados AS t2
ON t1.CodEmpleado = t2.CodEmpleado
INNER JOIN Usuarios AS t3
ON t1.CreadoPorUserName = t3.IdUsuario
GO

-- Vista para reporte general de entradas y salidas del personal
CREATE VIEW Vw_ListaDeEntradaSalidaPersonal AS
SELECT t1.IdAsistencia, t3.Nombre + ' ' + t3.Apellido as Empleado ,t2.FechaHoraEntrada, t2.FechaHoraSalida, t2.Observacion
FROM Asistencias AS t1
INNER JOIN DetalleDeAsistencias AS t2
ON t1.IdAsistencia = t2.IdAsistencia
INNER JOIN Empleados AS t3
ON T1.CodEmpleado = t3.CodEmpleado
GO

-- Reporte general de entradas y salidas por área.
CREATE VIEW Vw_Vw_ListaDeEntradaSalidaPersonalPorArea AS
SELECT t1.IdAsistencia, t4.Nombre as Area ,t3.Nombre + ' ' + t3.Apellido as Empleado ,t2.FechaHoraEntrada, t2.FechaHoraSalida, t2.Observacion
FROM Asistencias AS t1
INNER JOIN DetalleDeAsistencias AS t2
ON t1.IdAsistencia = t2.IdAsistencia
INNER JOIN Empleados AS t3
ON T1.CodEmpleado = t3.CodEmpleado
INNER JOIN Areas AS t4
ON t3.IdArea = t4.IdArea
GO