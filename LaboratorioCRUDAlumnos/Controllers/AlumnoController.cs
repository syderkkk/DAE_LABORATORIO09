using System.Web.Mvc;
using LaboratorioCRUDAlumnos.Models;
using LaboratorioCRUDAlumnos.Repositorios;

namespace LaboratorioCRUDAlumnos.Controllers
{
    public class AlumnoController : Controller
    {
        private AlumnoRepositorio Repositorio
        {
            get { return new AlumnoRepositorio(Session); }
        }

        public ActionResult Index()
        {
            return View(Repositorio.ObtenerTodos());
        }

        public ActionResult Details(int id)
        {
            var alumno = Repositorio.ObtenerPorId(id);
            if (alumno == null) return HttpNotFound();
            return View(alumno);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")] AlumnoModel alumno)
        {
            if (!ModelState.IsValid) return View(alumno);

            Repositorio.Agregar(alumno);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var alumno = Repositorio.ObtenerPorId(id);
            if (alumno == null) return HttpNotFound();
            return View(alumno);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(AlumnoModel alumno)
        {
            if (!ModelState.IsValid) return View(alumno);
            if (!Repositorio.Actualizar(alumno)) return HttpNotFound();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var alumno = Repositorio.ObtenerPorId(id);
            if (alumno == null) return HttpNotFound();
            return View(alumno);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Repositorio.Eliminar(id)) return HttpNotFound();

            return RedirectToAction("Index");
        }
    }
}
