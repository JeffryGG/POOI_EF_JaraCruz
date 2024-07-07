using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class AlmunoCursoEnt
    {
        public int IdAlumnoCurso { get; set; }
        public int IdAlumno { get; set; }
        public int IdCurso { get; set; }
        public int Nota { get; set; }
        public int FlgEliminado { get; set; }
    }
}
