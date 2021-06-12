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
    public class CategoriasController : Controller
    {
        private readonly CategoriaDAL categoriaDAL = new CategoriaDAL();
        
        // GET: CategoriasController
        public ActionResult Index()
        {
            var categorias = categoriaDAL.GetAll();
            return View(categorias);
        }

        // GET: CategoriasController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var categoria = categoriaDAL.GetById(id);
            if (categoria == null)
                return NotFound();
            return View(categoria);
        }

        // GET: CategoriasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind]Categoria categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);
            try
            {
                categoriaDAL.Add(categoria);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(categoria);
            }
        }

        // GET: CategoriasController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var categoria = categoriaDAL.GetById(id);
            if (categoria == null)
                return NotFound();
            return View(categoria);
        }

        // POST: CategoriasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind]Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return View(categoria);
            try
            {
                categoriaDAL.Update(categoria);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(categoria);
            }
        }

        // GET: CategoriasController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var categoria = categoriaDAL.GetById(id);
            if (categoria == null)
                return NotFound();
            return View(categoria);
        }

        // POST: CategoriasController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            categoriaDAL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
