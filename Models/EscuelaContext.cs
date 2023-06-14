using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebEscuela.Models
{
    public class EscuelaContext: DbContext 
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas{ get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Evaluacion> Evaluaciones{ get; set; }


        public EscuelaContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            


            base.OnModelCreating(modelBuilder);
            //creacion de escuelas
            var escuela1 = new Escuela();

            escuela1.Nombre = "UFG";
            escuela1.Id = Guid.NewGuid().ToString();
            escuela1.Fundacion = 1980;
            escuela1.Direccion = "San Salvador";
            escuela1.AtributoTipoDeEscuela = TiposEscuela.Superior;


         


            //por cada escuela asignar Cursos



            var listaCurso = CargarCursos(escuela1);






            //Por cada curso asignar asignaturas


            var listaAsignatura = CargarAsignaturas(listaCurso);


            //Por cada curso asignar alumnos

            var listaAlumnos = CargarAlumnos(listaCurso);


            //Por cada alumno asignar evaluaciones


            var listaEvaluaciones = CargarEvaluaciones(listaAlumnos, listaAsignatura);










            modelBuilder.Entity<Escuela>().HasData(escuela1);
            modelBuilder.Entity<Curso>().HasData(listaCurso);
            modelBuilder.Entity<Asignatura>().HasData(listaAsignatura);
            modelBuilder.Entity<Alumno>().HasData(listaAlumnos);
            modelBuilder.Entity<Evaluacion>().HasData(listaEvaluaciones);





        }

        private List<Evaluacion> CargarEvaluaciones(List<Alumno> listaAlumnos, List<Asignatura> listaAsignaturas)
        {
            var listaGeneral = new List<Evaluacion>();

            foreach (var asignatura in listaAsignaturas)
            {
                foreach (var alumno in listaAlumnos)
                {
                    Random random = new Random();
                    double numero = random.NextDouble() * 9 + 1;

                    if (alumno.CursoId == asignatura.CursoId)
                    {
                        var listaTmp = new List<Evaluacion>()
                        {
                            new Evaluacion()
                            {
                                AlumnoId = alumno.Id,
                                AsignaturaId = asignatura.Id,
                                Nombre = asignatura.Nombre,
                                Nota = Math.Round(numero,1)
                            }

                        };
                        listaGeneral.AddRange(listaTmp);
                    }

                }
            }

            return listaGeneral;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> listaCurso)
        {
            var listaGeneral = new List<Asignatura>();
            foreach (var curso in listaCurso)
            {
                var listTmp = new List<Asignatura>()
                {
                    new Asignatura()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id,
                        Nombre = "Programacion"
                    },
                    new Asignatura()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id,
                        Nombre = "Matematicas"
                    },
                    new Asignatura()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id,
                        Nombre = "Ciencias"
                    },
                    new Asignatura()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id,
                        Nombre = "Lenguaje"
                    }
                };

                listaGeneral.AddRange(listTmp);

            }

            return listaGeneral;
            
        }





        private static List<Curso> CargarCursos(Escuela escuela1)
        {
            var listaCursos = new List<Curso>()
            {
                new Curso()
                {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela1.Id,
                    Nombre = "Curso A",
                    Jornada = TiposJornada.Mañana
                },
                new Curso()
                {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela1.Id,
                    Nombre = "Curso B",
                    Jornada = TiposJornada.Mañana
                },
                new Curso()
                {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela1.Id,
                    Nombre = "Curso C",
                    Jornada = TiposJornada.Mañana
                },
                new Curso()
                {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela1.Id,
                    Nombre = "Curso D",
                    Jornada = TiposJornada.Mañana
                }
            };

            return listaCursos;
        }


        private List<Alumno> CargarAlumnos(List<Curso> listaCurso)
        {
            var listaGeneral = new List<Alumno>();

            foreach (var curso in listaCurso)
            {
                var listTmp = GenerarAlumnos(curso);
                listaGeneral.AddRange(listTmp);
                
            }

            return listaGeneral;
        }


        public List<Alumno> GenerarAlumnos(Curso curso, int cantidad = 10)
            {
                string[] nombres = { "Ana", "Manuel", "Alejandro", "María", "Josue" };
                string[] apellidos = { "Martinez", "Nerio", "Guzman", "Maltez", "Cruz" };

                var listaAlumnos = from nom in nombres
                                   from ape in apellidos
                                   select new Alumno()
                                   {
                                       CursoId = curso.Id,
                                       Id = Guid.NewGuid().ToString(),
                                       Nombre = $"{nom} {ape}"
                                   };
            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
            }

    }
}
