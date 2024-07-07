create procedure  SP_Curso_List(
	@orden int,
	@idalumno int
)
as

if  (@orden = 1) --- devuelve toda la lista
	begin 
		select 
			IdCurso,
			CodCurso,
			NombreCurso
		from Curso
		where FlgEliminado = 0
end