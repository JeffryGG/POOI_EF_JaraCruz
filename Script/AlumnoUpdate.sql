CREATE PROCEDURE SP_Alumno_Update(
	@IdAlumno int,
	@Nombres varchar(30),
	@Apellidos varchar(30),
	@Ciclo varchar(2),
	@Carrera varchar(30)
)

as

	update Alumno
		set
			Nombres = @Nombres,
			Apellidos = @Apellidos,
			Ciclo = @Ciclo,
			Carrera = @Carrera
		where IdAlumno = @IdAlumno