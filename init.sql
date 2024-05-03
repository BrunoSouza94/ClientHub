if not exists (
	select 1 from sys.databases where name = 'ClientHub'
)
	create database ClientHub;
go

use ClientHub
go

if not exists (select * from sys.tables where name = 'Client')
create table Client
(
	Id uniqueidentifier primary key,
	Name varchar(100) not null,
	Email varchar(100) not null,
	Logo varchar(max) not null,
	constraint UC_ClientEmail unique(Email)
);
go

if not exists (select * from sys.tables where name = 'Address')
create table Address
(
    Id uniqueidentifier primary key,
	Thoroughfare varchar(100) not null,
	LocationNumber varchar(20) not null,
	Neighborhood varchar(50) not null,
	City varchar(50) not null,
	State varchar(50) not null,
	ClientId uniqueidentifier not null
);
go

alter table Address with check add constraint FK_Address_Client_ClientId foreign key(ClientId)
references Client (Id)
on delete cascade
go

alter table Address check constraint FK_Address_Client_ClientId
go

create procedure GetAllClients
as
begin
	select *
	from Client c
end
go

create procedure GetClientById
	@Id uniqueidentifier
as
begin
	select *
	from Client
	where Id = @Id;

	select *
	from Address
	where ClientId = @Id
end
go

create procedure GetAddressesByClientId
	@ClientId uniqueidentifier
as
begin
	select *
	from Address
	where ClientId = @ClientId
end
go

create procedure GetAddressById
	@Id uniqueidentifier
as
begin
	select *
	from Address
	where Id = @Id;
end