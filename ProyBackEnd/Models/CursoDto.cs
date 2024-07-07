using CapaEntidad;
using System.Data.SqlClient;

namespace ProyBackEnd.Models
{
    public class CursoDto
    {
        public async Task<List<CursoEnt>> ListarCurso()
        {
            List<CursoEnt> Listar = new List<CursoEnt>();
            using (SqlConnection cnn = new SqlConnection(MetaGlobal.Cnx))
            {

                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("SP_Curso_List", cnn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                       /// cmd.Parameters.AddWithValue("@IdCurso", codcurso);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                CursoEnt cursocab = new CursoEnt();
                                cursocab.IdCurso = Convert.ToInt32(reader["IdCurso"].ToString());
                                cursocab.CodCurso = reader["CodCurso"].ToString();
                                cursocab.NombreCurso = reader["NombreCurso"].ToString();
                                Listar.Add(cursocab);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Listar = new List<CursoEnt>();
                    }
                }
            }
            return Listar;

        }
    }
}
