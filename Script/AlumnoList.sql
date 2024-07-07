create procedure  SP_Alumno_List(
	@orden int,
	@idalumno int
)
as

if  (@orden = 1) --- devuelve toda la lista
	begin 
		select 
			IdAlumno,
			Nombres,
			Apellidos,
			Ciclo,
			Carrera,
			FechaRegistro
		from Alumno
		where FlgEliminado = 0
	end
if (@orden = 2) -- devuelve x Id
	begin
		select
			IdAlumno,
			Nombres,
			Apellidos,
			Ciclo,
			Carrera,
			FechaRegistro
		from Alumno
		where IdAlumno = @idalumno

		select
			IdAlumnoCurso,
			IdAlumno,
			IdCurso,
			Nota
		from AlumnoCurso
		where IdAlumno = @idalumno
	end