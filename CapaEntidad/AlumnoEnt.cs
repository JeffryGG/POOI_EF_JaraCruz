using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class AlumnoEnt
    {
        public int IdAlumno { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Ciclo { get; set; }
        public string Carrera { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int FlgEliminado { get; set; }
        public List<AlmunoCursoEnt> ListaCurso { get; set; }
    }
}
