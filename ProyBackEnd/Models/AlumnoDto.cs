using Capa_Entidad;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace ProyBackEnd.Models
{
    public class AlumnoDto
    {
        public async Task<List<AlumnoEnt>> ListarAlumno(int orden, int idaumno)
        {
            List<AlumnoEnt> objLista = new List<AlumnoEnt>();
            using (SqlConnection cnn = new SqlConnection(MetaGlobal.Cnx))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_Alumno_List", cnn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@orden", orden);
                        cmd.Parameters.AddWithValue("@idalumno", idaumno);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                AlumnoEnt obj = new AlumnoEnt();
                                obj.IdAlumno = Convert.ToInt32(reader["IdAlumno"].ToString());
                                obj.Nombres = reader["Nombres"].ToString();
                                obj.Apellidos = reader["Apellidos"].ToString();
                                obj.Ciclo = reader["Ciclo"].ToString();
                                obj.Carrera = reader["Carrera"].ToString();
                                objLista.Add(obj);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        objLista = new List<AlumnoEnt>();
                    }
                }
            }
            return objLista;
        }

        public async Task<AlumnoEnt> ListarAlumno_XID(int orden, int idaumno)
        {
            AlumnoEnt objregistro = new AlumnoEnt();
            using (SqlConnection cnn = new SqlConnection(MetaGlobal.Cnx))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_Alumno_List", cnn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@orden", orden);
                        cmd.Parameters.AddWithValue("@idalumno", idaumno);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                objregistro.IdAlumno = Convert.ToInt32(reader["IdAlumno"].ToString());
                                objregistro.Nombres = reader["Nombres"].ToString();
                                objregistro.Apellidos = reader["Apellidos"].ToString();
                                objregistro.Ciclo = reader["Ciclo"].ToString();
                                objregistro.Carrera = reader["Carrera"].ToString();
                                objregistro.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"].ToString());
                                objregistro.ListaCurso = new List<AlmunoCursoEnt>();
                                if (await reader.NextResultAsync())
                                {
                                    while (reader.Read())
                                    {
                                        AlmunoCursoEnt registrodet = new AlmunoCursoEnt();
                                        registrodet.IdAlumnoCurso = Convert.ToInt32(reader["IdAlumnoCurso"].ToString());
                                        registrodet.IdAlumno = Convert.ToInt32(reader["IdAlumno"].ToString());
                                        registrodet.IdCurso = Convert.ToInt32(reader["IdCurso"].ToString());
                                        registrodet.Nota = Convert.ToInt32(reader["Nota"].ToString());
                                        objregistro.ListaCurso.Add(registrodet);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        objregistro = new AlumnoEnt();
                    }
                }
            }
            return objregistro;
        }

        public async Task<ResultadoTrasationE> RegistrarAlumno(AlumnoEnt objalumno)
        {
            ResultadoTrasationE resultado = new ResultadoTrasationE();
            using (SqlConnection cnn = new SqlConnection(MetaGlobal.Cnx))
            {
                cnn.Open();
                SqlTransaction transaction = cnn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("SP_Alumno_Insert", cnn, transaction))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombres", objalumno.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", objalumno.Apellidos);
                        cmd.Parameters.AddWithValue("@Ciclo", objalumno.Ciclo);
                        cmd.Parameters.AddWithValue("@Carrera", objalumno.Carrera);
                        cmd.Parameters.Add("@IdAlumno", SqlDbType.Int, 11).Direction = ParameterDirection.Output;
                        await cmd.ExecuteNonQueryAsync();

                        int IdAlumno = Convert.ToInt32(cmd.Parameters["@IdAlumno"].Value.ToString());

                        if (objalumno.ListaCurso.Count() > 0)
                        {
                            foreach (var item in objalumno.ListaCurso)
                            {
                                var Resultadoinsert = await RegistrarNuevoCurso(item, IdAlumno, cnn, transaction);
                                if (Resultadoinsert.IdRegistro == -1)
                                {
                                    transaction.Rollback();
                                    resultado.IdRegistro = -1;
                                    resultado.Mensaje = Resultadoinsert.Mensaje;
                                    return resultado;
                                }
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            resultado.IdRegistro = -1;
                            resultado.Mensaje = "Error, no se encontraron Productos";
                            return resultado;

                        }

                        transaction.Commit();
                        transaction.Dispose();
                        resultado.IdRegistro = 0;
                        resultado.Mensaje = "Registro Correcto";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        resultado.IdRegistro = -1;
                        resultado.Mensaje = ex.Message;
                    }
                }
            }
            return resultado;
        }

        public async Task<ResultadoTrasationE> ActualizarAlumno(AlumnoEnt objalumno)
        {
            ResultadoTrasationE resultado = new ResultadoTrasationE();
            using (SqlConnection cnn = new SqlConnection(MetaGlobal.Cnx))
            {
                cnn.Open();
                SqlTransaction transaction = cnn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("SP_Alumno_Update", cnn, transaction))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdAlumno", objalumno.IdAlumno);
                        cmd.Parameters.AddWithValue("@Nombres", objalumno.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", objalumno.Apellidos);
                        cmd.Parameters.AddWithValue("@Ciclo", objalumno.Ciclo);
                        cmd.Parameters.AddWithValue("@Carrera", objalumno.Carrera);
                        await cmd.ExecuteNonQueryAsync();


                        if (objalumno.ListaCurso.Count() > 0)
                        {
                            //registrar nuevos productos
                            foreach (var item in objalumno.ListaCurso.Where(w => w.IdAlumnoCurso == 0))
                            {
                                var respuestaInsert = await RegistrarNuevoCurso(item, objalumno.IdAlumno, cnn, transaction);
                                if (respuestaInsert.IdRegistro == -1)
                                {
                                    transaction.Rollback();
                                    resultado.IdRegistro = -1;
                                    resultado.Mensaje = respuestaInsert.Mensaje;

                                    return resultado;
                                }
                            }
                            //actualizar productos
                            foreach (var item in objalumno.ListaCurso.Where(w => w.IdAlumnoCurso > 0))
                            {
                                var respuestaUpdate = await ActualizarCurso(item, cnn, transaction);
                                if (respuestaUpdate.IdRegistro == -1)
                                {
                                    transaction.Rollback();
                                    resultado.IdRegistro = -1;
                                    resultado.Mensaje = respuestaUpdate.Mensaje;
                                    return resultado;
                                }
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            resultado.IdRegistro = -1;
                            resultado.Mensaje = "Error, no se encontraron Productos";
                            return resultado;

                        }

                        transaction.Commit();
                        transaction.Dispose();
                        resultado.IdRegistro = 0;
                        resultado.Mensaje = "Registro Correcto";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        resultado.IdRegistro = -1;
                        resultado.Mensaje = ex.Message;
                    }
                }
            }
            return resultado;
        }

        public async Task<ResultadoTrasationE> RegistrarNuevoCurso(AlmunoCursoEnt objalumnocurso, int Idalumno, SqlConnection cnn, SqlTransaction transaction)
        {
            ResultadoTrasationE resultado = new ResultadoTrasationE();
            using (SqlCommand cmd = new SqlCommand("SP_AlumnoCurso_Insert", cnn, transaction))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAlumno", Idalumno);
                    cmd.Parameters.AddWithValue("@IdCurso", objalumnocurso.IdCurso);
                    cmd.Parameters.AddWithValue("@Nota", objalumnocurso.Nota);
                    await cmd.ExecuteNonQueryAsync();
                    resultado.IdRegistro = 0;
                    resultado.Mensaje = "OK";

                }
                catch (Exception ex)
                {
                    resultado.IdRegistro = -1;
                    resultado.Mensaje = ex.Message;
                }
            }
            return resultado;
        }

        public async Task<ResultadoTrasationE> ActualizarCurso(AlmunoCursoEnt objalumnocurso, SqlConnection cnn, SqlTransaction transaction)
        {
            ResultadoTrasationE resultado = new ResultadoTrasationE();
            using (SqlCommand cmd = new SqlCommand("SP_AlmunoCurso_Update", cnn, transaction))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAlumnoCurso", objalumnocurso.IdAlumnoCurso);
                    cmd.Parameters.AddWithValue("@IdCurso", objalumnocurso.IdCurso);
                    cmd.Parameters.AddWithValue("@Nota", objalumnocurso.Nota);
                    await cmd.ExecuteNonQueryAsync();
                    resultado.IdRegistro = 0;
                    resultado.Mensaje = "OK";

                }
                catch (Exception ex)
                {
                    resultado.IdRegistro = -1;
                    resultado.Mensaje = ex.Message;
                }
            }
            return resultado;
        }
    }
}

