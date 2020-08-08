--Creating database only if database is not created yet
IF DB_ID('Nedeljni_I_Boris_Prpos_DDL') IS NULL
CREATE DATABASE Nedeljni_I_Boris_Prpos_DDL
GO
USE Nedeljni_I_Boris_Prpos_DDL;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblProject')
drop table tblProject;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmploye')
drop table tblEmploye;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmployeEdit')
drop table tblEmployeEdit;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblReport')
drop table tblReport;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblManager')
drop table tblManager;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblAdmin')
drop table tblAdmin;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblSector')
drop table tblSector;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblPosition')
drop table tblPosition;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblAdminType')
drop table tblAdminType;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblGender')
drop table tblGender;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblMarried')
drop table tblMarried;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEducation')
drop table tblEducation;


if OBJECT_ID('vwEmploye','v') IS NOT NULL DROP VIEW vwEmploye;
if OBJECT_ID('vwAdmin','v') IS NOT NULL DROP VIEW vwAdmin;
if OBJECT_ID('vwManager','v') IS NOT NULL DROP VIEW vwManager;



create table tblUser (
UserId int identity(1,1) primary key,
Firstname nvarchar (50) not null,
Lastname nvarchar (50) not null,
JMBG nvarchar (13) unique not null,
GenderID int not null,
Place nvarchar (50) not null,
MariageID int not null,
Username nvarchar (50) unique not null,
Pasword nvarchar (50) unique not null 
)

create table tblEmploye (
EmployeID int identity(1,1) primary key,
UserID int not null,
SectorID int not null,
PositionID int,
Salary float,
ExperienceYear int not null,
EducationID int not null,
ManagerID int not null
)

create table tblEmployeEdit (
EmployeEditID int identity(1,1) primary key,
UserID int,
SectorID int,
PositionID int,
Salary float,
ExperienceYear int ,
EducationID int,
EditStatus nvarchar,
ManagerID int not null
)

create table tblManager(
ManagerID int identity (1,1) primary key,
UserID int not null,
Mail nvarchar (50) not null,
HelpPass nvarchar (50) not null,
DutyLevel int,
ProjectsDone int,
Salary float,
OfficeNumber int
)

create table tblAdmin(
AdminID int identity (1,1) primary key,
UserID int not null,
AcountExpire date not null,
AdminTypeID int 
)

create table tblProject(
ProjectID int identity(1,1) primary key,
ProjectName nvarchar(100),
ProjectDesc nvarchar (100),
ClientName nvarchar (100),
ContractDate date not null,
ManagerID int not null,
StartDate date not null,
EndDate date not null,
HourValue float not null,
Leader int,
Realisation int
)

create table tblReport(
ReportID int identity(1,1) primary key,
ProjectName nvarchar (50) not null,
ClientName nvarchar (50) not null,
ReportDate date not null,
WorkDesc nvarchar (100),
TotalHours int not null
)

create table tblGender(
GenderID int identity(1,1) primary key,
Gender nvarchar (1)
)

create table tblMarried(
MarriedID int identity(1,1) primary key,
MarriedStatus nvarchar(30)
)

create table tblSector(
SectorID int identity (1,1) primary key,
SectorName nvarchar (50) not null,
SectorDesc nvarchar (100) 
)

create table tblPosition(
PositionID int identity(1,1) primary key,
PoisitionName nvarchar (50) not null,
PoisitionDesc nvarchar (100)
)

create table tblEducation(
EducationID int identity (1,1) primary key,
Education nvarchar (3) not null
)

create table tblAdminType(
AdminTypeID int identity (1,1) primary key,
AdminDesc nvarchar (30) not null
)
Alter table tblUser
Add foreign key (GenderID) references tblGender(GenderID);

Alter table tblUser
Add foreign key (MariageID) references tblMarried(MarriedID);

Alter Table tblEmploye
Add foreign key (UserID) references tblUser(UserID);

Alter Table tblEmploye
Add foreign key (SectorID) references tblSector(SectorID);

Alter Table tblEmploye
Add foreign key (PositionID) references tblPosition(PositionID);

Alter Table tblEmploye
Add foreign key (ManagerID) references tblManager(ManagerID);

Alter Table tblEmploye
Add foreign key (EducationID) references tblEducation(EducationID);

Alter Table tblManager
Add foreign key (UserID) references tblUser(UserID);

Alter Table tblAdmin
Add foreign key (UserID) references tblUser(UserID);

Alter Table tblAdmin
Add foreign key (AdminTypeID) references tblAdminType(AdminTypeID);

Insert into tblGender values ('M'),('Z'),('N'),('X');
Insert into tblEducation values ('I'),('II'),('III'),('IV'),('V'),('VI'),('VII');
Insert into tblMarried values ('Married'),('Unmarried'),('Divorced');
Insert into tblAdminType values ('Team'),('System'),('Local');
Insert into tblSector values ('Default',null);

GO
CREATE VIEW vwEmploye AS
	SELECT	tblUser.*, 
			tblEmploye.ExperienceYear, tblEmploye.Salary, tblEmploye.EducationID, tblEmploye.SectorID, 
			tblEmploye.PositionID, tblEmploye.ManagerID,tblEmploye.EmployeID
	FROM	tblUser, tblEmploye
	WHERE	tblUser.UserID = tblEmploye.UserID

GO
CREATE VIEW vwManager AS
	SELECT	tblUser.*, 
			tblManager.Mail, tblManager.HelpPass, tblManager.Salary, tblManager.DutyLevel, 
			tblManager.ProjectsDone, tblManager.OfficeNumber, tblManager.ManagerID
	FROM	tblUser, tblManager 
	WHERE	tblUser.UserID = tblManager.UserID

GO
CREATE VIEW vwAdmin AS
	SELECT	tblUser.*, tblAdmin.AcountExpire, tblAdmin.AdminTypeID, tblAdmin.AdminID
	FROM	tblUser, tblAdmin
	WHERE	tblUser.UserID = tblAdmin.UserID


	

