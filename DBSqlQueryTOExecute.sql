create database MasterDB
go

use MasterDB
go

create table ms_user(
id uniqueidentifier not null primary key,
user_names varchar(20) not null,
passwords varchar(50) not null ,
isactive bit not null 
)
go
insert into ms_user values (newID(),'jhonUmiro','admin1*',1)
insert into ms_user values (newID(),'trisNatan','admin2@',1)
insert into ms_user values (newID(),'hugoRess','admin3#',1)
go
create table ms_storage_location(
location_id varchar(10) not null primary key,
location_name varchar(100) not null
)
go
insert into ms_storage_location values ('115','Jakarta')
insert into ms_storage_location values ('145','Ciputat')
insert into ms_storage_location values ('175','Pandeglang')
insert into ms_storage_location values ('190','Bekasi')
go
create table tr_bpkb(
agreementnumber varchar(100) not null primary key,
bpkb_no varchar(100),
branch_id varchar(10),
bpkb_date	datetime,
faktur_no varchar(100),
faktur_date datetime,
location_id varchar(10),
police_no varchar(20),
bpkb_date_in datetime,
created_by varchar(20),
created_on datetime,
last_updated_by varchar(20),
last_updated_on datetime,
FOREIGN KEY (location_id) REFERENCES ms_storage_location(location_id)
)

