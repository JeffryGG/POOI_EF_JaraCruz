create procedure SP_AlumnoCurso_Insert(
	@IdAlumno int,
	@IdCurso int,
	@Nota int
)

as

declare @IdAlumnoCurso int = isnull((select max(IdAlumnoCurso) from AlumnoCurso),0)+1

	insert into AlumnoCurso
	(IdAlumnoCurso, IdAlumno, IdCurso, Nota,FlgEliminado)
	values
	(@IdAlumnoCurso, @IdAlumno,@IdCurso, @Nota,0)