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
select EmployeeId,EmployeeName,ActDate,HoursMentioned from DetailsOfFile
order by EmployeeId
end
