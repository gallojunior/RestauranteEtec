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
    public class BlogController : Controller
    {
        private readonly BlogDAL blogDAL = new BlogDAL();
        private readonly IWebHostEnvironment webHostEnvironment;

        public BlogController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: BlogController
        public ActionResult Index()
        {
            var blogs = blogDAL.GetAll();
            return View(blogs);
        }

        // GET: BlogController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = blogDAL.GetById(id);
            if (blog == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(blog);
        }

        // GET: BlogController/Create
        public ActionResult Create()
        {
            var blog = new Blog();
            blog.DataCadastro = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
            return View(blog);
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] Blog blog, IFormFile Imagem)
        {
            if (!ModelState.IsValid)
                return View(blog);
            try
            {
                if (Imagem != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\blogs");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + Imagem.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await Imagem.CopyToAsync(stream);
                    };
                    blog.Imagem = "/images/blogs/" + nomeArquivo;
                }
                blogDAL.Add(blog);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(blog);
            }
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = blogDAL.GetById(id);
            if (blog == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(blog);
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind] Blog blog, IFormFile NovaImagem)
        {
            if (id != blog.Id)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            if (!ModelState.IsValid)
                return View(blog);
            try
            {
                if (NovaImagem != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\blogs");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaImagem.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await NovaImagem.CopyToAsync(stream);
                    };
                    blog.Imagem = "/images/blogs/" + nomeArquivo;
                }
                blogDAL.Update(blog);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(blog);
            }
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = blogDAL.GetById(id);
            if (blog == null)
                return NotFound();
            ViewData["wwwroot"] = webHostEnvironment.WebRootPath;
            return View(blog);
        }

        // POST: BlogController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            blogDAL.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
