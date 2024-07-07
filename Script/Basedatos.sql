create database  EXFINALPOO
use [EXFINALPOO]

create table Alumno(
IdAlumno int primary key not null,
Nombres varchar(30) null, 
Apellidos varchar(30) null, 
Ciclo varchar(2) null ,
Carrera varchar(30) null, 
FechaRegistro smalldatetime null, 
FlgEliminado bit null 
)
create table Curso(
IdCurso Int primary key not null,
CodCurso varchar(5) null,
NombreCurso varchar(50) null,
FlgEliminado bit null
)
create table AlumnoCurso(
IdAlumnoCurso int primary key not null,
IdAlumno int not null,
IdCurso int not null, 
Nota int null, 
FlgEliminado bit null,