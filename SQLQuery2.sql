create table employees(
EmployeeId int primary key identity,
EmployeeName nvarchar(100) not null,
ContactInfo nvarchar(100) not null,
Role nvarchar(100) not null,
HireDate date not null
);