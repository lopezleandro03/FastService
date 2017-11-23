insert EstadoReparacion values (99,'SIN DETERMINAR', 'Sin Determinar','Sin determinar',1,'sysadmin',getdate())
insert marca values ('SIN DETERMINAR','Codigo de marca utilizado durante la migracion para transacciones sin marca','sysadmin',getdate())
insert tipodispositivo values ('SIN DETERMINAR', 'Sin determinar','sysadmim',getdate())

--Migro clientes
insert cliente
select nrocli,
SUBSTRING(nombre,CHARINDEX(' ',nombre),LEN(nombre)),
SUBSTRING(nombre,0,CHARINDEX(' ',nombre)),
'imcompleto@email.com',
telefono,
null,
RTRIM(LTRIM(direcc)) + ' entre ' + RTRIM(Ltrim(ecalle1)) + ' y ' + ltrim(rtrim(ecalle2)) + ', ' + ltrim(rtrim(localidad))
,null,null
from oldcliente

--Migro respobsables y tecnicos a usuarios
insert Usuario values ('sinasignar@gmail.com','SIN ASIGNAR','SIN ASIGNAR','84123124','','',null)

insert usuario 
select 
SUBSTRING(nombre,0,CHARINDEX(' ',nombre)),
SUBSTRING(nombre,0,CHARINDEX(' ',nombre)),
SUBSTRING(nombre,CHARINDEX(' ',nombre),LEN(nombre)),
'123456',
nrotec,
'',
null
from oldtecnico where activo = 'true'
union
select 
SUBSTRING(nombre,0,CHARINDEX(' ',nombre)),
SUBSTRING(nombre,0,CHARINDEX(' ',nombre)),
SUBSTRING(nombre,CHARINDEX(' ',nombre),LEN(nombre)),
'123456',
nrores,
'',
null
from  oldresponsable where activo = 'true'

--Migro transacciones a reparaciones
insert reparacion
select nrotra,
ISNULL((select top 1 c.clienteid from cliente c where c.Dni = nrocli),0),
ISNULL((select top 1 c.UserId from Usuario c where c.Direccion = nroemp),(select c2.userid from usuario c2 where c2.nombre = 'SIN ASIGNAR')),
ISNULL((select top 1 c.UserId from Usuario c where c.Direccion = nrotec),(select c2.userid from usuario c2 where c2.nombre = 'SIN ASIGNAR')),
ISNULL((select top 1 e.EstadoReparacionId from EstadoReparacion e where e.EstadoReparacionId = nroest),(select top 1 e2.EstadoReparacionId from EstadoReparacion e2 where nombre = 'SIN DETERMINAR')),
ISNULL((select top 1 m.MarcaId from Marca m where m.descripcion = nromar),(select top 1 m2.marcaid from marca m2 where nombre = 'SIN DETERMINAR')),
ISNULL((select top 1 t.tipoDispositivoid from tipoDispositivo t where t.descripcion = nrotip),(select top 1 t2.TipoDispositivoId from tipoDispositivo t2 where t2.nombre = 'SIN DETERMINAR')),
0,
0,
'sysadmin',
getdate()
from oldtranu o
where not exists (select top 1 1 from oldtranu o2 where o2.timestamp > o.timestamp )

----Migro novedades
insert novedad 
select 
n.nrotra,
isnull((select top 1 userid from Usuario where Direccion = nrotec),(select c2.userid from usuario c2 where c2.nombre = 'SIN ASIGNAR')),
codnov,
case pesos when '' then 0 else isnull(pesos,0) end,
observ,
'sysadmin',
getdate()
from oldobserv o join oldnovedad n on n.transac = o.transac order by nrotra asc