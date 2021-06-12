using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestauranteEtec.DAL;
using RestauranteEtec.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEtec.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly CargoDAL cargoDAL = new CargoDAL();
        private readonly FuncionarioDAL funcionarioDAL = new FuncionarioDAL();
        private readonly IWebHostEnvironment webHostEnvironment;

        public FuncionariosController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: FuncionariosController
        public ActionResult Index()
        {
            var funcionarios = funcionarioDAL.GetAll();
            return View(funcionarios);
        }

        // GET: FuncionariosController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var funcionario = funcionarioDAL.GetById(id);
            if (funcionario == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(funcionario);
        }

        // GET: FuncionariosController/Create
        public ActionResult Create()
        {
            ViewData["Cargos"] = new SelectList(cargoDAL.GetAll(), "Id", "Nome");
            return View();
        }

        // POST: FuncionariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] Funcionario funcionario, IFormFile Foto)
        {
            ViewData["Cargos"] = new SelectList(cargoDAL.GetAll(), "Id", "Nome");
            if (!ModelState.IsValid)
                return View(funcionario);
            try
            {
                if (Foto != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\funcionarios");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await Foto.CopyToAsync(stream);
                    };
                    funcionario.Foto = "/images/funcionarios/" + nomeArquivo;
                }
                funcionarioDAL.Add(funcionario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(funcionario);
            }
        }

        // GET: FuncionariosController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var funcionario = funcionarioDAL.GetById(id);
            if (funcionario == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            ViewData["Cargos"] = new SelectList(cargoDAL.GetAll(), "Id", "Nome");
            return View(funcionario);
        }

        // POST: FuncionariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind] Funcionario funcionario, IFormFile NovaFoto)
        {
            if (id != funcionario.Id)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            ViewData["Cargos"] = new SelectList(cargoDAL.GetAll(), "Id", "Nome");
            if (!ModelState.IsValid)
                return View(funcionario);
            try
            {
                if (NovaFoto != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\funcionarios");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await NovaFoto.CopyToAsync(stream);
                    };
                    funcionario.Foto = "/images/funcionarios/" + nomeArquivo;
                }
                funcionarioDAL.Update(funcionario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(funcionario);
            }
        }

        // GET: FuncionariosController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var funcionario = funcionarioDAL.GetById(id);
            if (funcionario == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(funcionario);
        }

        // POST: CargosController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            funcionarioDAL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
