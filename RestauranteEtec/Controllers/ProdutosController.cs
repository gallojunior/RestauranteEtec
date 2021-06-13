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
    public class ProdutosController : Controller
    {

        private readonly CategoriaDAL categoriaDAL = new CategoriaDAL();
        private readonly ProdutoDAL produtoDAL = new ProdutoDAL();
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProdutosController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: ProdutoController
        public ActionResult Index()
        {
            var produtos = produtoDAL.GetAll();
            return View(produtos);
        }

        // GET: ProdutoController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var produto = produtoDAL.GetById(id);
            if (produto == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(produto);
        }

        // GET: ProdutoController/Create
        public ActionResult Create()
        {
            ViewData["Categorias"] = new SelectList(categoriaDAL.GetAll(), "Id", "Nome");
            var produto = new Produto();
            produto.Ativo = true;
            produto.ExibirHome = false;
            return View(produto);
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] Produto produto, IFormFile Foto)
        {
            ViewData["Categorias"] = new SelectList(categoriaDAL.GetAll(), "Id", "Nome");
            if (!ModelState.IsValid)
                return View(produto);
            try
            {
                if (Foto != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\produtos");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await Foto.CopyToAsync(stream);
                    };
                    produto.Foto = "/images/produtos/" + nomeArquivo;
                }
                produtoDAL.Add(produto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(produto);
            }
        }

        // GET: ProdutoController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var produto = produtoDAL.GetById(id);
            if (produto == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            ViewData["Categorias"] = new SelectList(categoriaDAL.GetAll(), "Id", "Nome");
            return View(produto);
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind] Produto produto, IFormFile NovaFoto)
        {
            if (id != produto.Id)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            ViewData["Categorias"] = new SelectList(categoriaDAL.GetAll(), "Id", "Nome");
            if (!ModelState.IsValid)
                return View(produto);
            try
            {
                if (NovaFoto != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\produtos");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await NovaFoto.CopyToAsync(stream);
                    };
                    produto.Foto = "/images/produtos/" + nomeArquivo;
                }
                produtoDAL.Update(produto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(produto);
            }
        }

        // GET: ProdutoController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var produto = produtoDAL.GetById(id);
            if (produto == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(produto);
        }

        // POST: ProdutoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            produtoDAL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
