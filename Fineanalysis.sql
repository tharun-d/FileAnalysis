create database FileAnalysis
drop table DetailsOfFile

Create table DetailsOfFile
(
Sno int primary key identity(1,1),
EmployeeId varchar(max),EmployeeName varchar(max),ActDate datetime,ExtProject varchar(max),Esnumber varchar(max),ExternalProject varchar(max),Project varchar(max),Wbs varchar(max),Attribute varchar(max),AAtype varchar(max),HoursMentioned float
)

create procedure insertintodetailsoffile(@EmployeeId varchar(max),@EmployeeName varchar(max),@ActDate datetime,@ExtProject varchar(max),@Esnumber varchar(max),@ExternalProject varchar(max),@Project varchar(max),@Wbs varchar(max),@Attribute varchar(max),@AAtype varchar(max),@HoursMentioned float)
as begin
insert into DetailsOfFile values(@EmployeeId,@EmployeeName,@ActDate,@ExtProject,@Esnumber,@ExternalProject,@Project,@Wbs,@Attribute,@AAtype,@HoursMentioned)
end
