using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteEtec.DAL;
using RestauranteEtec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEtec.Controllers
{
    public class CargosController : Controller
    {
        private readonly CargoDAL cargoDAL = new CargoDAL();

        // GET: CargosController
        public ActionResult Index()
        {
            var cargos = cargoDAL.GetAll();
            return View(cargos);
        }

        // GET: CargosController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var cargo = cargoDAL.GetById(id);
            if (cargo == null)
                return NotFound();
            return View(cargo);
        }

        // GET: CargosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind]Cargo cargo)
        {
            if (!ModelState.IsValid)
                return View(cargo);
            try
            {
                cargoDAL.Add(cargo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(cargo);
            }
        }

        // GET: CargosController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var cargo = cargoDAL.GetById(id);
            if (cargo == null)
                return NotFound();
            return View(cargo);
        }

        // POST: CargosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return View(cargo);
            try
            {
                cargoDAL.Update(cargo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(cargo);
            }
        }

        // GET: CargosController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var cargo = cargoDAL.GetById(id);
            if (cargo == null)
                return NotFound();
            return View(cargo);
        }

        // POST: CargosController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            cargoDAL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
