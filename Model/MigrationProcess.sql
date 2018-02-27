insert EstadoReparacion values ('SIN DETERMINAR', 'Sin Determinar','Sin determinar',1,'sysadmin',getdate())
insert marca values ('SIN DETERMINAR','Codigo de marca utilizado durante la migracion para transacciones sin marca','sysadmin',getdate())
insert tipodispositivo values ('SIN DETERMINAR', 'Sin determinar','sysadmim',getdate())
insert cliente values(99999999,'SIN DETERMINAR', 'SIN DETERMINAR','sindeterminar@gmail.com','SIN DETERMINAR', 'SIN DETERMINAR', 'SIN DETERMINAR',null, 'SIN DETERMINAR', null,null)
insert Comercio values('SIN DETERMINAR', null,null,null,null,null,null,null,null,'sysadmin', getdate())

--migrate estados
insert EstadoReparacion
select nombre, descripcion,categoria,activo,modificadoPor,modificadoEn from oldEstadoRep

--insert direccion placeholder
insert direccion
select null,null,null,null,null,null,null,null,null,getdate(),nrocli
from oldcliente

--Migro clientes
insert cliente
select nrocli,
SUBSTRING(nombre,CHARINDEX(' ',nombre),LEN(nombre)),
SUBSTRING(nombre,0,CHARINDEX(' ',nombre)),
'imcompleto@email.com',
telefono,
null,
RTRIM(LTRIM(direcc)) + ' entre ' + RTRIM(Ltrim(ecalle1)) + ' y ' + ltrim(rtrim(ecalle2)) + ', ' + ltrim(rtrim(localidad))
,(select top 1 direccionid from direccion where changedby = nrocli)
,null,null,null
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

--clean up duplicated records
if ((select 1 from tempdb.sys.tables where name like '%tokeep%') > 0)
	drop table #tokeep

create table #tokeep (timestamp varchar(20),nrotra varchar(20) )

insert #tokeep 
select MAX(timestamp), nrotra from oldtranu group by nrotra having COUNT(nrotra) > 1
union
select MAX(timestamp), nrotra from oldtranu group by nrotra having COUNT(nrotra) = 1

delete t
from oldtranu t
left join #tokeep d on t.nrotra = d.nrotra and t.timestamp = d.timestamp
where d.nrotra is null 

delete oldtranu where timestamp  =''

--Migra detalle de reparaciones
insert reparaciondetalle
select 
CASE o.garantia WHEN 'C' THEN 1 ELSE 0 END,  
CASE o.movil WHEN 0 THEN 0 ELSE 1 END,--to be determined
o.nrotra,
null,
null,
CASE PRECIO WHEN '.00' THEN 0 ELSE SUBSTRING(PRECIO,0,CHARINDEX('.',precio)) END as precio,
getdate(),
CASE PRECIO WHEN '.00' THEN 0 ELSE SUBSTRING(PRECIO,0,CHARINDEX('.',precio)) END as presup,
modelo,
serie,
serbus,
'sysadmin',
getdate()
from oldtranu o
--where not exists (select top 1 1 from oldtranu o2 where o2.timestamp > o.timestamp and o2.nrotra = o.nrotra)
--and not exists (select top 1 1 from oldtranu o2 where o2.nrocli > o.nrocli and o2.nrotra = o.nrotra)

--Migro transacciones a reparaciones
insert reparacion
select nrotra,
ISNULL((select top 1 c.clienteid from cliente c where c.Dni = nrocli),(SELECT  top 1 clienteid from cliente where nombre = 'SIN DETERMINAR')),
ISNULL((select top 1 c.UserId from Usuario c where c.Direccion = nroemp),(select top 1 c2.userid from usuario c2 where c2.nombre = 'SIN ASIGNAR')),
ISNULL((select top 1 c.UserId from Usuario c where c.Direccion = nrotec),(select top 1 c2.userid from usuario c2 where c2.nombre = 'SIN ASIGNAR')),
ISNULL((select top 1 e.EstadoReparacionId from EstadoReparacion e where e.EstadoReparacionId = nroest),(select top 1 c2.EstadoReparacionId from EstadoReparacion c2 where c2.nombre = 'SIN DETERMINAR')),
ISNULL((select top 1 c.ComercioId from Comercio c where c.telefono2 = nrocom),(select top 1 c2.ComercioId from Comercio c2 where c2.code = 'SIN DETERMINAR')),
ISNULL((select top 1 m.MarcaId from Marca m where m.descripcion = nromar),(select top 1 m2.marcaid from marca m2 where nombre = 'SIN DETERMINAR')),
ISNULL((select top 1 t.tipoDispositivoid from tipoDispositivo t where t.descripcion = nrotip),(select top 1 t2.TipoDispositivoId from tipoDispositivo t2 where t2.nombre = 'SIN DETERMINAR')),
rd.reparaciondetalleid,
'sysadmin',
getdate()
from oldtranu o
join reparacionDetalle rd on o.nrotra = rd.nroReferencia
--where not exists (select top 1 1 from oldtranu o2 where o2.timestamp > o.timestamp and o2.nrotra = o.nrotra)
--and not exists (select top 1 1 from oldtranu o2 where o2.nrocli > o.nrocli and o2.nrotra = o.nrotra)

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

--select * from oldtranu where nrotra = 521
--select nrotra from oldtranu group by nrotra having COUNT(nrotra) > 1


