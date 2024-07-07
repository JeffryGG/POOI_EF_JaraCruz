CREATE PROCEDURE SP_Alumno_Insert(
	@Nombres varchar(30),
	@Apellidos varchar(30),
	@Ciclo varchar(2),
	@Carrera varchar(30),
	@IdAlumno int output
)

as 

set @IdAlumno = ISNULL ((select max (IdAlumno) from Alumno), 0) +1

	insert into Alumno
	(IdAlumno, Nombres, Apellidos, Ciclo, Carrera, FechaRegistro,FlgEliminado)
	values (@IdAlumno, @Nombres, @Apellidos, @Ciclo, @Carrera, getdate(),0)