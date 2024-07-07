create procedure SP_AlmunoCurso_Update(
	@IdAlumnoCurso int,
	@IdCurso int,
	@Nota int
)

as

	update AlumnoCurso
		set IdAlumnoCurso = @IdAlumnoCurso,
			IdCurso = @IdCurso,
			Nota = @Nota
		where IdAlumnoCurso = @IdAlumnoCurso