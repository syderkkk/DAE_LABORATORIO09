using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaboratorioCRUDAlumnos.Models;

namespace LaboratorioCRUDAlumnos.Repositorios
{
    public class AlumnoRepositorio
    {
        private const string ClaveSesion = "alumnos";

        private readonly HttpSessionStateBase _sesion;

        public AlumnoRepositorio(HttpSessionStateBase sesion)
        {
            _sesion = sesion;
        }

        private List<AlumnoModel> Alumnos
        {
            get
            {
                var alumnos = _sesion[ClaveSesion] as List<AlumnoModel>;
                if (alumnos == null)
                {
                    alumnos = new List<AlumnoModel>
                    {
                        new AlumnoModel { Id = 1, Nombres = "Ana", Apellidos = "Torres Ruiz", Edad = 20, Correo = "ana.torres@correo.com" },
                        new AlumnoModel { Id = 2, Nombres = "Luis", Apellidos = "Perez Gomez", Edad = 22, Correo = "luis.perez@correo.com" },
                        new AlumnoModel { Id = 3, Nombres = "Marta", Apellidos = "Diaz Vega", Edad = 19, Correo = "marta.diaz@correo.com" }
                    };
                    _sesion[ClaveSesion] = alumnos;
                }
                return alumnos;
            }
        }

        public IEnumerable<AlumnoModel> ObtenerTodos()
        {
            return Alumnos;
        }

        public AlumnoModel ObtenerPorId(int id)
        {
            return Alumnos.FirstOrDefault(a => a.Id == id);
        }

        public void Agregar(AlumnoModel alumno)
        {
            alumno.Id = Alumnos.Select(a => a.Id).DefaultIfEmpty(0).Max() + 1;
            Alumnos.Add(alumno);
        }

        public bool Actualizar(AlumnoModel alumno)
        {
            var existente = ObtenerPorId(alumno.Id);
            if (existente == null) return false;

            existente.Nombres = alumno.Nombres;
            existente.Apellidos = alumno.Apellidos;
            existente.Edad = alumno.Edad;
            existente.Correo = alumno.Correo;
            return true;
        }

        public bool Eliminar(int id)
        {
            var alumno = ObtenerPorId(id);
            return alumno != null && Alumnos.Remove(alumno);
        }
    }
}
