use master;--1
--ctrl shift u--2
drop database Veterinary_Hospital;--3
create database Veterinary_Hospital;--4
use Veterinary_Hospital;--5
--6
create table Doctors(
ID_Doctor int primary key identity,
FIO_Doctor nvarchar(50),
Hired_Date date,
Specialization nvarchar(50),
Date_Of_Birth date,
Telephone nvarchar(20));

insert into Doctors values ('dfjvd','2020-03-02','ej','2020-09-02','23183172863');
insert into Doctors values ('ffffff','2020-03-02','ej','2020-09-02','23183172863');
insert into Doctors values ('dfjvd','2020-03-02','ej','2020-09-02','23183172863');
insert into Doctors values ('dfjvd','2020-03-02','ej','2020-09-02','23183172863');
insert into Doctors values ('ffffff','2020-03-02','ej','2020-09-02','23183172863');
insert into Doctors values ('dfjvd','2020-03-02','ej','2020-09-02','23183172863');
insert into Doctors values ('ttttt','2020-03-02','ej','2020-09-02','23183172863');

create table Owners(
ID_Owner int primary key identity,
FIO_Owner nvarchar(50),
Owner_Address nvarchar(50),
Telephone nvarchar(50));

insert into Owners values ('dkvj','gbk','dfljb');

create table Pets(
ID_Pet int primary key identity,
Type_Of_Pet nvarchar(50),
Pet_Name nvarchar(50),
Date_Of_Birth nvarchar(50),
Breed nvarchar(50),
Pet_Male bit,
Coat_Color nvarchar(50),
Pet_Weight int,
Special_Sings nvarchar(50),
Enabled_Pet bit,
ID_Owner int,
foreign key (ID_Owner) references Owners(ID_Owner) on delete cascade on update cascade);

insert into Pets values('dfv','1','2020-09-02','lfjv',1, 'dfjvkj',232,'fvkef',1,1);
insert into Pets values('dfv','1','2020-09-02','lfjv',1, 'dfjvkj',232,'fvkef',1,1);
insert into Pets values('dfv','1','2020-09-02','lfjv',1, 'dfjvkj',232,'fvkef',1,1);
insert into Pets values('dfv','2','2020-09-02','lfjv',1, 'dfjvkj',232,'fvkef',1,1);
insert into Pets values('dfv','3','2020-09-02','lfjv',1, 'dfjvkj',232,'fvkef',1,1);


create table Servises(
ID_Service int primary key identity,
Name_Of_Service nvarchar(50),
Description_Service nvarchar(50),
Price int,
Date_Of_Approval date);


insert into Servises values ('serv1','des1',324,'2020-02-03');
create table Registers(
ID_Register int primary key identity,
ID_Doctor int,
ID_Owner int,
ID_Pet int,
ID_Service int,
Date_Of_Visit date,
Time_Of_Visit time,
Arrived bit,
foreign key (ID_Doctor) references Doctors(ID_Doctor) on delete cascade on update cascade,
foreign key (ID_Owner) references Owners(ID_Owner) on delete cascade on update cascade,
foreign key (ID_Pet) references Pets(ID_Pet),
foreign key (ID_Service) references Servises(ID_Service));

insert into Registers values (1,1,1,1,'2020-01-01','20:01',1);
insert into Registers values (2,1,4,1,'2020-01-02','20:01',0);
insert into Registers values (3,1,3,1,'2020-02-03','20:01',1);
insert into Registers values (4,1,4,1,'2020-01-04','20:01',1);
insert into Registers values (1,1,2,1,'2020-02-03','20:01',0);
insert into Registers values (1,1,2,1,'2020-02-06','20:01',1);
insert into Registers values (2,1,4,1,'2020-01-07','20:01',1);
insert into Registers values (2,1,1,1,'2020-01-08','20:01',0);
insert into Registers values (1,1,4,1,'2020-01-09','20:01',1);

--insert into Pets values ();




select * from Doctors;
select * from Owners;
select * from Pets;
select * from Registers;

go
create procedure insertRegisters
@ID_Doctor int, 
@ID_Owner int,
@ID_Pet int,
@Date_Of_Visit date,
@Time_Of_Visit time
AS
begin
insert into Registers (ID_Doctor,ID_Owner,ID_Pet,Date_Of_Visit,Time_Of_Visit)
values (@ID_Doctor, 
@ID_Owner,
@ID_Pet,
@Date_Of_Visit,
@Time_Of_Visit)
end
go

go
create procedure insertPets
@Type_Of_Pet nvarchar(50),
@Pet_Name nvarchar(50),
@Date_Of_Birth nvarchar(50),
@Breed nvarchar(50),
@Pet_Male nvarchar(50),
@Coat_Color nvarchar(50),
@Special_Sings nvarchar(50),
@ID_Owner int
AS
begin
INSERT INTO Pets (Type_Of_Pet,Pet_Name,Date_Of_Birth,Breed,Pet_Male,Coat_Color,Special_Sings,ID_Owner)
values (@Type_Of_Pet,@Pet_Name,@Date_Of_Birth,@Breed,@Pet_Male,@Coat_Color,@Special_Sings,@ID_Owner) 
end
go


--все посещения для заданного питомца
Select * from Registers where ID_Pet=(select ID_Pet from Pets where Pet_Name = '2');	
--вывести всех, кто не пришел сегодня
Select * from Registers where Arrived=0;
--количество посещений для каждого врача в выбранном месяце
Select ID_Doctor,count(ID_Register) from Registers where Month(Date_Of_Visit)=01 group by (ID_Doctor); --+
--общее количество записей на каждую дату в выбранном месяце
select Date_Of_Visit,count(ID_Register) from Registers where Month(Date_Of_Visit)=02 group by (Date_Of_Visit); 
--вывести всех пациентов для конкретного врача *Я ВООБЩЕ НЕ УВЕРЕНА, ЧТО ЭТОТ ЗАПРОС НУЖЕН*

--вывести всех пациентов на сегодня вместе с врачами
select ID_Pet from Pets where Pet_Name = '2';