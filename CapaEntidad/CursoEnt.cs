using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CursoEnt
    {
        public int IdCurso { get; set; }
        public string CodCurso { get; set; }
        public string NombreCurso { get; set; }


        public CursoEnt() { }

        public CursoEnt(int idCurso, string _codcurso, string _nombreCurso ) {
            this.IdCurso = idCurso;
            this.CodCurso = _codcurso;
            this.NombreCurso = _nombreCurso;
        }
    }
}
