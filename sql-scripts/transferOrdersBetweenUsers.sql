select * from usuario

--insert novedad
--select r.ReparacionId, 78,17,0, 'Orden transferida de Javier a Luis', getdate(),78
--from Reparacion r 
--where r.TecnicoAsignadoId = (select UserId from usuario where nombre = 'JAVIER' and Activo = 1) 
--and r.EstadoReparacionId != 7
--and r.EstadoReparacionId != 4
--and r.EstadoReparacionId != 17
--and r.ReparacionId > 100000

--update Reparacion
--set TecnicoAsignadoId = 55
--where TecnicoAsignadoId = (select UserId from usuario where nombre = 'JAVIER' and Activo = 1) 
--and EstadoReparacionId != 7
--and EstadoReparacionId != 4
--and EstadoReparacionId != 17
--and ReparacionId > 100000

select * from TipoNovedad

111299
111977
113363
113426
113607
113922
114029
114875

select top 1000 * from Novedad order by 1 desc

select * from EstadoReparacion where activo = 1

select top 1000 * from Cliente order by 1 desc