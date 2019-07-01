use AdventureWorks2016CTP3; -- LAPTOP-TVCRG0TN\MSSQLSERVER01

--drop table CellPhone;
--drop table People;
--drop table CellPhoneColor;

select * from CellPhone;
select * from People; 
select * from CellPhoneColor;

drop table if exists People;
create table People(
	PersonID int Primary KEY,
	Name varchar(30),
	CellPhoneID int	);

insert People values (1, 'Serge',		1);
insert People values (2, 'Tristan',	1);
insert People values (3, 'Dedushka',	2);
insert People values (4, 'McKayla',	Null);
insert People values (5, 'Babushka',		3);
insert People values (6, 'Serge',		8);

drop table if exists CellPhoneColor;
Create table CellPhoneColor(
	ID int Primary KEY,
	Color varchar(10))

insert CellPhoneColor values(1, 'Red');
insert CellPhoneColor values(2, 'Blue');
insert CellPhoneColor values(3, 'Green');

drop table if exists CellPhone;
create table CellPhone(
	CellPhoneID int Primary KEY,
	Number varchar(10),
	CellPhoneColorID int Default Null
	constraint FK_CellPhone_CellPhoneColor Foreign Key (CellPhoneColorID) 
		references CellPhoneColor (ID) 
		--on delete cascade 	
	);

insert CellPhone values (1, '1111',		1);
insert CellPhone values (2, '2222',		3);
insert CellPhone values (3, '3333',		null);
insert CellPhone values (4, '4444',		null);

--delete from CellPhoneColor  -- test cascade delete
