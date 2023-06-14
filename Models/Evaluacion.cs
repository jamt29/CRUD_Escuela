using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEscuela.Models
{
    public class Evaluacion: EscuelaBase
    {
     

        public string AlumnoId { get; set; }
        public string AsignaturaId { get; set; }
        //public string NombreAlumno { get; set; }
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura { get; set; }
        public double Nota { get; set; }

        //Constructor

    }
}
