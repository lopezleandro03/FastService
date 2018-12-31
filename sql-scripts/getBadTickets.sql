use FastService

--select SUM(monto) from venta where MONTH(Fecha) = 8 and YEAR(fecha) = 2018


select r.reparacionId, er.nombre, DATEDIFF(day,r.modificadoEn,getdate()), c.Mail
from Reparacion r 
join EstadoReparacion er on r.EstadoReparacionId = er.EstadoReparacionId 
join Cliente c on r.ClienteId = c.ClienteId
where er.nombre != 'RETIRADO' and er.nombre != 'CANCELADO' and er.nombre != 'VISITADO'  and er.nombre != 'Rechazo presup.' and er.nombre != 'Rechazado' 
 and er.nombre != 'Rep. Domic.'
--and r.CreadoEn > '01/01/2018'
and  DATEDIFF(day,r.modificadoEn,getdate()) > 30
and  DATEDIFF(day,r.modificadoEn,getdate()) < 60
order by DATEDIFF(day,r.modificadoEn,getdate()) desc

--select * from EstadoReparacion

select * from [vw_ReparacionTiempos]
