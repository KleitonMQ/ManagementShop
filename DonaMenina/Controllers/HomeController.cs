using DonaMenina.Context;
using DonaMenina.Entities;
using DonaMenina.Filters;
using DonaMenina.Helper;
using DonaMenina.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DonaMenina.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserSession _session;
        private readonly DataBaseContext _dbContext;

        public HomeController(IUserSession session, DataBaseContext dbContext)
        {
            _session = session;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            Console.WriteLine("entrou no index");
            
            if (_session.GetWorkerSession() != null)
            {
                var userSession = _session.GetWorkerSession();
                Console.WriteLine(userSession.Password);
                return RedirectToAction("WorkSpace", "WorkSpace", new { password = userSession.Password });
            }
            Console.WriteLine("não entrou no if");
            return View();
        }

        [HttpPost]
        public IActionResult WorkSpace(string password)
        {
            if (_session.GetWorkerSession() == null)
            {
                var worker = _dbContext.Workers.FirstOrDefault(w => w.Password == password);
                Console.WriteLine("var worker veriricada");
                if (worker == null)
                {
                    
                    TempData["ErrorMessage"] = "Senha incorreta.";
                    return View("Index");
                }else if(!worker.IsActive)
                {
                    TempData["ErrorMessage"] = "Usuário Inativo.";
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("WorkSpace", "WorkSpace", new { password = password});
                }
            }
            Console.WriteLine("Ja estava logado");
            WorkSpaceModel prevWorkSpaceModel = new WorkSpaceModel();
            prevWorkSpaceModel.Worker = _session.GetWorkerSession();
            prevWorkSpaceModel.Products = _dbContext.Products.ToList();
            Console.WriteLine(prevWorkSpaceModel.Worker);
            return RedirectToAction("WorkSpace", "WorkSpace", new { password = prevWorkSpaceModel.Worker.Password });
        }

        public IActionResult Exit()
        {
            _session.EndUserSession();
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}