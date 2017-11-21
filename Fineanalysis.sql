create database FileAnalysis
drop table DetailsOfFile
delete from detailsoffile

Create table DetailsOfFile
(
Sno int primary key identity(1,1),
EmployeeId varchar(max),
EmployeeName varchar(max),
ActDate datetime,ExtProject varchar(max),
Esnumber varchar(max),
ExternalProject varchar(max),
Project varchar(max),
Wbs varchar(max),
Attribute varchar(max),
AAtype varchar(max),
ProjectType varchar(max),
HoursMentioned float
)
select * from DetailsOfFile

alter procedure insertintodetailsoffile(@EmployeeId varchar(max),@EmployeeName varchar(max),@ActDate datetime,@ExtProject varchar(max),@Esnumber varchar(max),@ExternalProject varchar(max),@Project varchar(max),@Wbs varchar(max),@Attribute varchar(max),@AAtype varchar(max),@Projecttype varchar(max),@HoursMentioned float)
as begin
insert into DetailsOfFile values(@EmployeeId,@EmployeeName,@ActDate,@ExtProject,@Esnumber,@ExternalProject,@Project,@Wbs,@Attribute,@AAtype,@Projecttype,@HoursMentioned)
end


alter procedure daywisedetails
as begin
select EmployeeId,EmployeeName,cast(ActDate as date),SUM(HoursMentioned) from DetailsOfFile
group by ActDate,EmployeeName,EmployeeId
order by EmployeeId
end


alter procedure GettingAll
as begin
select EmployeeId,EmployeeName,cast(ActDate as date),ExtProject,Esnumber,ExternalProject,project,Wbs,Attribute,AAtype,ProjectType,HoursMentioned from DetailsOfFile
order by EmployeeId
end

alter procedure AllEmployeesNames as
select distinct EmployeeId,EmployeeName from DetailsOfFile
order by EmployeeId

alter procedure GettingMissedDates(@EmployeeName varchar(max))as
begin
select * from temp_table
where num not in(
select distinct RIGHT(cast(ActDate as date),2) from DetailsOfFile
where EmployeeName=@EmployeeName)
order by num
end


GettingMissedDates 'atul bansal'

select actdate from detailsoffile

create table temp_table
(
num int
)

delete from temp_table

select * from temp_table
insert into temp_table values(01)
insert into temp_table values(02)
insert into temp_table values(03)
insert into temp_table values(04)
insert into temp_table values(05)
insert into temp_table values(06)
insert into temp_table values(07)
insert into temp_table values(08)
insert into temp_table values(09)
insert into temp_table values(10)
insert into temp_table values(11)
insert into temp_table values(12)
insert into temp_table values(13)
insert into temp_table values(14)
insert into temp_table values(15)
insert into temp_table values(16)
insert into temp_table values(17)
insert into temp_table values(18)
insert into temp_table values(19)
insert into temp_table values(20)
insert into temp_table values(21)
insert into temp_table values(22)
insert into temp_table values(23)
insert into temp_table values(24)
insert into temp_table values(25)
insert into temp_table values(26)
insert into temp_table values(27)
insert into temp_table values(28)
insert into temp_table values(30)
insert into temp_table values(31)

alter procedure clearall as
begin
delete from DetailsOfFile
delete from catwmisseddates 
delete from PPMtotalHoursFilled
end


--From Now PPM
create table PPMDetailsOfFile
(
Sno int primary key identity(1,1),
ProjectNumber varchar(max),
ProjectName varchar(max),
ResourceNumber varchar(max),
ResourceName varchar(max),
TaskName varchar(max),
Summary varchar(max),
DateMentioned datetime,
HoursMentioned float,
ResourceRole varchar(max),
ResourceType varchar(max),
BillingCode varchar(max),
ResourceHourlyRate float,
ProgrameeProjectManager varchar(max),
AfeDescrimination varchar(max),
ProgrameeGroup varchar(max),
Programee varchar(max),
ProgrameeManager varchar(max),
BussinessLead varchar(max),
UAVP varchar(max) null,
ITSABuildingCategory varchar(max),
FundingCategory varchar(max),
AFENumber varchar(max),
ServiceCategory varchar(max),
BillingRateOnShore varchar(max),
BillingRateOffShore varchar(max)
)

drop table PPMDetailsOfFile

alter procedure insertintoppmdetailsoffile(
@ProjectNumber varchar(max),
@ProjectName varchar(max),
@ResourceNumber varchar(max),
@ResourceName varchar(max),
@TaskName varchar(max),
@Summary varchar(max),
@DateMentioned datetime,
@HoursMentioned float,
@ResourceRole varchar(max),
@ResourceType varchar(max),
@BillingCode varchar(max),
@ResourceHourlyRate float,
@ProgrameeProjectManager varchar(max),
@AfeDescrimination varchar(max),
@ProgrameeGroup varchar(max),
@Programee varchar(max),
@ProgrameeManager varchar(max),
@BussinessLead varchar(max),
@UAVP varchar(max),
@ITSABuildingCategory varchar(max),
@FundingCategory varchar(max),
@AFENumber varchar(max),
@ServiceCategory varchar(max),
@BillingRateOnShore varchar(max),
@BillingRateOffShore varchar(max)
) as
begin 
insert into PPMdetailsoffile values(
@ProjectNumber,@ProjectName,@ResourceNumber,@ResourceName,@TaskName,@Summary,@DateMentioned,@HoursMentioned,@ResourceRole,@ResourceType,@BillingCode,@ResourceHourlyRate,@ProgrameeProjectManager,@AfeDescrimination,@ProgrameeGroup,@Programee,@ProgrameeManager,@BussinessLead,@UAVP,@ITSABuildingCategory,@FundingCategory,@AFENumber,@ServiceCategory,@BillingRateOnShore,@BillingRateOffShore
)
end

alter procedure PPMGettingAll as
begin
select * from PPMDetailsOfFile
end

alter procedure PPMclearall as
begin
delete from PPMDetailsOfFile
delete from ppmmisseddates
delete from PPMtotalHoursFilled
end

select * from PPMDetailsOfFile


alter procedure ppmdaywisedetails
as begin
select ResourceNumber,ResourceName,CAST(DateMentioned as date),SUM(HoursMentioned) from ppmDetailsOfFile
group by DateMentioned,ResourceName,ResourceNumber
order by ResourceNumber
end

alter procedure PPMAllEmployeesNames as
select distinct ResourceNumber,ResourceName from PPMDetailsOfFile
order by ResourceNumber

Create procedure PPMGettingMissedDates(@ResourceName varchar(max))as
begin
select * from temp_table
where num not in(
select distinct RIGHT(cast(DateMentioned as date),2) from PPMDetailsOfFile
where ResourceName=@ResourceName)
order by num
end


--create table missingdatesforppmcatw
--(
--sno int primary key identity(1,1),
--EmployeeNumber varchar(max),
--EmployeeName varchar(max),
--PPMMissingDates varchar(max),
--CATWMissingDates varchar(max),
--)

--drop table missingdatesforppmcatw

--create procedure insertintomissingdatesforppmcatw(@EmployeeNumber varchar(max),@employeeName varchar(max),@PPMMissingDates varchar(max)) as
--begin
--insert into missingdatesforppmcatw values(@EmployeeNumber,@EmployeeNumber,@PPMMissingDates,null)
--end

create table catwmisseddates
(
sno int primary key identity(1,1),
EmployeeNumber varchar(max),
EmployeeName varchar(max),
CATWMissingDates varchar(max)
)

select * from catwmisseddates

create procedure insertintocatwmisseddates(@EmployeeNumber varchar(max),@EmployeeName varchar(max),@CATWMissedDates varchar(max)) as
begin
insert into catwmisseddates values(@EmployeeNumber,@EmployeeName,@CATWMissedDates)
end

create table ppmmisseddates
(
sno int primary key identity(1,1),
EmployeeNumber varchar(max),
EmployeeName varchar(max),
ppmMissingDates varchar(max)
)

select * from ppmmisseddates

create procedure insertintoppmmisseddates(@EmployeeNumber varchar(max),@EmployeeName varchar(max),@CATWMissedDates varchar(max)) as
begin
insert into ppmmisseddates values(@EmployeeNumber,@EmployeeName,@CATWMissedDates)
end

create procedure GettingPPMMissedDates as 
begin
select EmployeeNumber,EmployeeName,ppmMissingDates from ppmmisseddates
end

create procedure GettingCATWMissedDates as 
begin
select EmployeeNumber,EmployeeName,catwMissingDates from catwmisseddates
end



create procedure gettingMissedDatesOfPPMandCATW as
begin
select p.EmployeeNumber,p.EmployeeName,p.ppmMissingDates,c.catwMissingDates from ppmmisseddates p 
inner join catwmisseddates c 
on p.EmployeeNumber=c.EmployeeNumber
order by p.EmployeeNumber
end



create table PPMtotalHoursFilled(
sno int primary key identity(1,1),
EmployeeNumber varchar(max),
EmployeeName varchar(max),
TotalHoursForppm float
)
select * from PPMtotalHoursFilled

alter procedure PPMtotalHoursFilledPerPerson(@EmployeeName varchar(max))as
begin
select ResourceNumber,ResourceName,sum(HoursMentioned) from PPMDetailsOfFile 
where ResourceName=@EmployeeName
group by ResourceNumber,ResourceName
end

create procedure insertintoPPMtotalHoursFilled(@EmployeeNumber varchar(max),@EmployeeName varchar(max),@TotalHoursForPPM varchar(max))as
begin
insert into PPMtotalHoursFilled values(@EmployeeNumber,@EmployeeName,@TotalHoursForPPM)
end


create table CATWtotalHoursFilled(
sno int primary key identity(1,1),
EmployeeNumber varchar(max),
EmployeeName varchar(max),
TotalHoursForCATW float
)
drop table CATWtotalHoursFilled
select * from CATWtotalHoursFilled

create procedure CATWtotalHoursFilledPerPerson(@EmployeeName varchar(max))as
begin
select EmployeeId,EmployeeName,sum(HoursMentioned) from DetailsOfFile 
where EmployeeName=@EmployeeName
group by EmployeeId,EmployeeName
end

create procedure insertintoCATWtotalHoursFilled(@EmployeeNumber varchar(max),@EmployeeName varchar(max),@TotalHoursForCATW varchar(max))as
begin
insert into CATWtotalHoursFilled values(@EmployeeNumber,@EmployeeName,@TotalHoursForCATW)
end


create procedure GettingPPMHoursFilled(@EmployeeNumber varchar(max))
as
begin
select TotalHoursForppm from PPMtotalHoursFilled
where EmployeeNumber=@EmployeeNumber
end

create procedure GettingCATWHoursFilled(@EmployeeNumber varchar(max))
as
begin
select TotalHoursForcatw from catwtotalHoursFilled
where EmployeeNumber=@EmployeeNumber
end