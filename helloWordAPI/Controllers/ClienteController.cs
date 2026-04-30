using helloWordWeb.Data;
using helloWordWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace helloWordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ClienteController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/<ClienteController>
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> listaCliente()
        {
            List<Cliente> listaClientes = _db.Clientes.ToList();


            return Ok(listaClientes);

        }

        [HttpGet("obterUmCliente/{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Cliente> obeter1Cliente(int id)
        {
            Cliente p = _db.Clientes.Find(id);
            if (id <= 0)
            {
                return BadRequest();

            }
            if (p != null)
                return Ok(p);
            else return NotFound();

        }

        [HttpPost("inserirCliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> InserirCliente([FromBody] Cliente p)
        {
            if (p.Id != 0)
            {
                return BadRequest();
            }
            _db.Clientes.Add(p);
            _db.SaveChanges();
            return Ok(p);

        }
        [HttpDelete("apagarCliente/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Produto> ApagarCliente(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Cliente p = _db.Clientes.Find(id);
            if (p == null)
            {
                return NotFound();
            }

            _db.Clientes.Remove(p);
            _db.SaveChanges();
            return Ok(p);
        }

        [HttpPut("atualizarCliente/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> AtualizarCliente(int id, [FromBody] Cliente p)
        {
            if (id <= 0 || p == null || id != p.Id)
            {
                return BadRequest();
            }

            Cliente clienteExistente = _db.Clientes.Find(id);
            if (clienteExistente == null)
            {
                return NotFound();
            }
            if (clienteExistente  != null)
            {

                clienteExistente.Name = p.Name;
                clienteExistente.Email = p.Email;
                clienteExistente.Tel = p.Tel;


                _db.Clientes.Update(clienteExistente);
                _db.SaveChanges();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("atualizarClienteRetorna/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> atualizarClienteRetorna(int id, [FromBody] Cliente p)
        {
            if (id <= 0 || p == null || id != p.Id)
            {
                return BadRequest();
            }

            Cliente clienteExistente = _db.Clientes.Find(id);
            if (clienteExistente == null)
            {
                return NotFound();
            }
            if (clienteExistente != null)
            {

                clienteExistente.Name = p.Name;
                clienteExistente.Email = p.Email;
                clienteExistente.Tel = p.Tel;



                _db.Clientes.Update(clienteExistente);
                _db.SaveChanges();
                return Ok(clienteExistente);
            }
            return NotFound();
        }

    
}
}
