using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteEtec.DAL;
using RestauranteEtec.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEtec.Controllers
{
    public class RelatosController : Controller
    {
        private readonly RelatoDAL relatoDAL = new RelatoDAL();
        private readonly IWebHostEnvironment webHostEnvironment;

        public RelatosController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: RelatoController
        public ActionResult Index()
        {
            var relatos = relatoDAL.GetAll();
            return View(relatos);
        }

        // GET: RelatoController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var relato = relatoDAL.GetById(id);
            if (relato == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(relato);
        }

        // GET: RelatoController/Create
        public ActionResult Create()
        {
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            var relato = new Relato();
            relato.Ativo = true;
            relato.ExibirHome = false;
            relato.DataCadastro = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
            return View(relato);
        }

        // POST: RelatoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] Relato relato, IFormFile Foto)
        {
            if (!ModelState.IsValid)
                return View(relato);
            try
            {
                if (Foto != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\relatos");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await Foto.CopyToAsync(stream);
                    };
                    relato.FotoPessoa = "/images/relatos/" + nomeArquivo;
                }
                relatoDAL.Add(relato);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(relato);
            }
        }

        // GET: RelatoController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var relato = relatoDAL.GetById(id);
            if (relato == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(relato);
        }

        // POST: RelatoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind] Relato relato, IFormFile NovaFoto)
        {
            if (id != relato.Id)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            if (!ModelState.IsValid)
                return View(relato);
            try
            {
                if (NovaFoto != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\relatos");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await NovaFoto.CopyToAsync(stream);
                    };
                    relato.FotoPessoa = "/images/relatos/" + nomeArquivo;
                }
                relatoDAL.Update(relato);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(relato);
            }
        }

        // GET: RelatoController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var relato = relatoDAL.GetById(id);
            if (relato == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(relato);
        }

        // POST: RelatoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            relatoDAL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
