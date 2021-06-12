using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEtec.Controllers
{
    public class RelatoController : Controller
    {
        // GET: RelatoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RelatoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RelatoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RelatoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RelatoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RelatoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RelatoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RelatoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
