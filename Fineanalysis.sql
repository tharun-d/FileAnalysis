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
select EmployeeId,EmployeeName,ActDate,SUM(HoursMentioned) from DetailsOfFile
group by ActDate,EmployeeName,EmployeeId
order by EmployeeId
end


alter procedure GettingAll
as begin
select EmployeeId,EmployeeName,ActDate,ExtProject,Esnumber,ExternalProject,project,Wbs,Attribute,AAtype,ProjectType,HoursMentioned from DetailsOfFile
order by EmployeeId
end

create procedure AllEmployeesNames as
select distinct EmployeeName from DetailsOfFile
order by EmployeeName

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

create procedure clearall as
delete from DetailsOfFile 



--From Now PPM
create table PPMDetailsOfFile
(
ProjectNumber varchar(max),
ProjectName varchar(max),
ResourceNumber varchar(max),
ResourceName varchar(max),
TaskName varchar(max),
Summary varchar(max),
DateMentioned varchar(max),
HoursMentioned float,
ResourceRole varchar(max),
ResourceType varchar(max),
BillingCode varchar(max),
ResourceHourlyRate varchar(max),
ProgrameeProjectManager varchar(max),
AfeDescrimination varchar(max),
ProgrameeGroup varchar(max),
Programee varchar(max),
ProgrameeManager varchar(max),
BussinessLead varchar(max),
UAVP varchar(max),
ITSABuildingCategory varchar(max),
FundingCategory varchar(max),
AFENumber varchar(max),
ServiceCategory varchar(max),
BillingRateOnShore varchar(max),
BillingRateOffShore varchar(max)
)
