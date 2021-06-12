using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestauranteEtec.DAL;
using RestauranteEtec.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEtec.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var funcionarios = new FuncionarioDAL();
            var chefes = funcionarios.GetAll().Where(f => f.Ativo && f.ExibirHome).OrderBy(f => f.OrdemExibicao).ToList();
            ViewData["Chefes"] = chefes;

            return View();
        }

        public IActionResult QuemSomos()
        {

            return View();
        }

        public IActionResult Chefes()
        {
            var funcionarios = new FuncionarioDAL();
            var chefes = funcionarios.GetAll().Where(f => f.Ativo && f.ExibirHome).OrderBy(f => f.OrdemExibicao).ToList();
            ViewData["Chefes"] = chefes;
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Reservas()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contatos()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
