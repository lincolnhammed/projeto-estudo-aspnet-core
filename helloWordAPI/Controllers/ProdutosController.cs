using helloWordWeb.Data;
using helloWordWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace helloWordAPI.Controllers
{
    //https://localhost:7223/swagger/index.html

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProdutosController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("ListaCompleta")]
        public ActionResult mostrarLista()
        {
            String[] listaFrutas = new string[] { "pera", "laranja", "Abacate" };

            return Ok(listaFrutas);
        }

       // [HttpGet("duasvezes/{num}")]// o valor é passado por rota
        [HttpGet("duasvezes")]// o valor é passado por parametro duasvezes?num=2
        public ActionResult<int> duasVezes(int num)
        {
            return num * 2;
        }
        [HttpGet("mostrarproduto")]
        //public ActionResult listaProduto() {

        //    var listaProdutos = _db.Produtos.ToList();
            
        //    return Ok(listaProdutos);

        //}
        public ActionResult <IEnumerable<Produto>> listaProduto()
        {
            List<Produto> listaProdutos = _db.Produtos.ToList();

           
            return Ok(listaProdutos);

        }

        [HttpGet("obterUmProduto/{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Produto>obeter1produto(int id)
        {
            Produto p = _db.Produtos.Find(id);
            if (id <= 0)
            {
                return BadRequest();
          
            }
            if (p != null)
                return Ok(p);
            else return NotFound();

        }

        [HttpPost("inserirProduto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Produto> InserirProduto([FromBody]Produto p)
        {
            if (p.Id != 0) {
                return BadRequest();
            }
            _db.Produtos.Add(p);
            _db.SaveChanges();
                return Ok(p);

        }
        [HttpDelete("apagarProduto/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Produto> ApagarProduto(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Produto p = _db.Produtos.Find(id);
            if (p == null)
            {
                return NotFound();
            }

            _db.Produtos.Remove(p);
            _db.SaveChanges();
            return Ok(p);
        }

        [HttpPut("atualizarProduto/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> AtualizarProduto(int id, [FromBody] Produto p)
        {
            if (id <= 0 || p == null || id != p.Id)
            {
                return BadRequest();
            }

            Produto produtoExistente = _db.Produtos.Find(id);
            if (produtoExistente == null)
            {
                return NotFound();
            }
            if (produtoExistente != null) { 

            produtoExistente.Name = p.Name;
            produtoExistente.Preco = p.Preco;
           

            _db.Produtos.Update(produtoExistente);
            _db.SaveChanges();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("atualizarProdutoRetorna/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> atualizarProdutoRetorna(int id, [FromBody] Produto p)
        {
            if (id <= 0 || p == null || id != p.Id)
            {
                return BadRequest();
            }

            Produto produtoExistente = _db.Produtos.Find(id);
            if (produtoExistente == null)
            {
                return NotFound();
            }
            if (produtoExistente != null)
            {

                produtoExistente.Name = p.Name;
                produtoExistente.Preco = p.Preco;


                _db.Produtos.Update(produtoExistente);
                _db.SaveChanges();
                return Ok(produtoExistente);
            }
            return NotFound();
        }
        
    }
}