
drop table dbo.oldcliente

create table dbo.oldcliente (
	nrocli int null,
	nombre varchar(200)  null,
	direcc varchar(200)  null,
	ecalle1 varchar(200)  null,
	ecalle2 varchar(200)  null,
	localidad varchar(200)  null,
	nroloc int  null,
	categoria varchar(200)  null,
	observ varchar(200) not null,
	telefono varchar(200) not null,
	vinopor varchar(200) not null
)

drop table dbo.oldtranu

create table dbo.oldtranu (
	nrotra int null,
	creado bit,
	local int null,
	empresa int null,
	nroemp int null,
	nrocli int null,
	nrocom int null,
	lugar varchar(200) null,
	fechacom varchar(200) null,
	faccom int null,
	nrotec int null,
	nromar int null,
	nrotip int null,
	modelo varchar(200) null,
	serie varchar(200) null,
	serbus varchar(200) null,
	garantia varchar(200) null,
	nroest varchar(200) null,
	impreso varchar(200) null,
	limite varchar(200) null,
	decomercio varchar(200) null,
	presupparaf varchar(200) null,
	presuparah varchar(200) null,
	nrofac varchar(200) null,
	prioridad varchar(200) null,
	sector varchar(200) null,
	timestamp varchar(200) null,
	ubicacion varchar(200) null,
	llamar varchar(200) null,
	plantec varchar(200) null,
	planrec varchar(200) null,
	planemp varchar(200) null,
	precio varchar(200) null,
	rep varchar(200) null,
	movil varchar(200) null,
	orden varchar(200) null,
	turno varchar(200) null,
	trajo varchar(200) null,
	lleva varchar(200) null,
	entregar varchar(200) null,
	desde varchar(200) null,
	viejo varchar(200) null,
	estant varchar(200) null,
	desdeant varchar(200) null,
	nroes varchar(200) null
)

drop table dbo.oldnovedad

create table dbo.oldnovedad (
	transac varchar(200) null,
	nrotra int null,
	local varchar(200) null,
	nrotec int null,
	codnov varchar(200) null,
	fecha varchar(200) null,
	pesos varchar(200) null,
	hora varchar(200) null,
	minutos varchar(200) null,
	creado varchar(200) null,
	origino varchar(200) null,
	visto varchar(200) null
)

drop table dbo.oldobserv

create table dbo.oldobserv (
	transac int null,
	local int null,
	observ varchar(1000) null,
	creado int null
)

drop table dbo.oldentrega

create table dbo.oldentrega (
	fecha varchar(200) null,
	tur varchar(200) null,
	local varchar(200) null,
	tipo varchar(200) null,
	nombre varchar(1000) null,
	direcc varchar(1000) null,
	ecalle1 varchar(1000) null,
	ecalle2 varchar(1000) null,
	localidad varchar(200) null,
	telefono varchar(1000) null,
	locali varchar(200) null,
	observ varchar(1000) null,
	hecho varchar(200) null,
	tipoent varchar(200) null,
	ubicacion varchar(1000) null
)


drop table dbo.oldcaja

create table dbo.oldcaja (
	fecha datetime null,
	transac int null,
	sucur int null,
	nrocomp int null,
	tipo int null,
	concepto varchar(1000) null,
	nrocue int null,
	entrada decimal null,
	salida decimal null,
	otro decimal null,
	cond varchar(200) null,
	estado varchar(1000) null,
	cerrada varchar(200) null,
	poriva int null
)

drop table dbo.oldresponsable 

create table dbo.oldresponsable (
	nroRes int null,
	nombre varchar(200) not null,
	activo varchar(200)
)


drop table dbo.oldtecnico 

create table dbo.oldtecnico (
	nroTec int null,
	nombre varchar(200) not null,
	activo varchar(200)
)

drop table oldEstadoRep
CREATE TABLE dbo.oldEstadoRep (
	 nombre varchar(200) not null,
	 descripcion varchar(400) null,
	 categoria varchar(200) null,
	 activo bit,
	 modificadoPor varchar(200) null,
	 modificadoEn datetime null,
)

