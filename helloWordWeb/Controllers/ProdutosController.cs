using helloWordWeb.Data;
using helloWordWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace helloWordWeb.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProdutosController(ApplicationDbContext db)

        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var listaProdutos = _db.Produtos.ToList();
            return View(listaProdutos);
        }
        [HttpGet]
        public IActionResult AdicionarProduto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdicionarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _db.Produtos.Add(produto);
                _db.SaveChanges();
                TempData["sucesso"] = "produto criado com sucesso";
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        [HttpGet]
        public IActionResult EditarProduto(int? id)
        {
            var produto = _db.Produtos.Find(id);
            if (produto == null)
            {
                TempData["erro"] = "nao foi passado nem um produto";
                return RedirectToAction("index");
            }
            return View(produto);
        }
        [HttpPost]
        public IActionResult EditarProduto(Produto p)
        {
            if (ModelState.IsValid)
            {
                _db.Update(p);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var produto = _db.Produtos.Find(id);
            if (produto == null)
                {
                return NotFound();
            }

            _db.Produtos.Remove(produto);
            _db.SaveChanges();

           
            return RedirectToAction("Index");
        }
    } }