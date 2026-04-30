using helloWordWeb.Data;
using helloWordWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace helloWordWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ClienteController(ApplicationDbContext db)

        {
            _db = db;
        }
        public IActionResult Index()
        {
            var listaCliente = _db.Clientes.ToList();
            return View(listaCliente);
        }
        public IActionResult AdicionarCliente()
        {
            return View();
        }
         [HttpPost]
        public IActionResult AdicionarCliente(Cliente c)
        {
            if (ModelState.IsValid)
            {
                _db.Clientes.Add(c);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        [HttpGet]
        public IActionResult EditarCliente(int? id)
        {
            var cliente = _db.Clientes.Find(id);
            if (cliente == null)
            {
                TempData["erro"] = "nao foi passado nem um cliente";
                return RedirectToAction("index");
            }
            return View(cliente);
        }
        [HttpPost]
        public IActionResult EditarCliente(Cliente c)
        {
            if (ModelState.IsValid)
            {
                _db.Update(c);
                _db.SaveChanges();
                TempData["sucesso"] = "Atualizado com sucesso";
                return RedirectToAction("Index");
            }
            return View(c);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cliente = _db.Clientes.Find(id);
            if (cliente == null)
            {
                TempData["erro"] = "usuario nao existe";
                return RedirectToAction("index");
            }

            _db.Clientes.Remove(cliente);
            _db.SaveChanges();
            TempData["sucesso"] = "usuario apagado com sucesso";

            return RedirectToAction("Index");
        }

    }
}
