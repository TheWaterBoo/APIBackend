using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIBackend.Modelos;

namespace APIBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public readonly BackendapiContext _dbcontext;
        public ClienteController(BackendapiContext _context)
        {
            _dbcontext = _context;
        }

        //Obtener toda la lista de clientes
        [HttpGet]
        [Route("ListarClientes")]

        public IActionResult ListarClientes() {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                lista = _dbcontext.Clientes.Include(c => c.oPersona).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        //Obtener cliente por id
        [HttpGet]
        [Route("cliente/{idCliente:int}")]

        public IActionResult Cliente(int idCliente)
        {
            Cliente oCliente = _dbcontext.Clientes.Find(idCliente);

            if (oCliente == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                return BadRequest("Cliente no encontrado!");
            }

            try
            {
                oCliente = _dbcontext.Clientes.Include(c => c.oPersona).Where(p => p.ClienteId == idCliente).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oCliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oCliente });
            }
        }

        //Crear cliente nuevo (incluye persona tambien)
        [HttpPost]
        [Route("crearCliente")]
        public IActionResult GuardarCliente([FromBody] Cliente objCliente)
        {
            try
            {
                _dbcontext.Clientes.Add(objCliente);    //Se agrega objCliente dentro del modelo
                _dbcontext.SaveChanges();   //Almacena informacion del cliente en la tabla

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "cliente guardado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //Editar Cliente ya existente
        [HttpPut]
        [Route("editarCliente")]
        public IActionResult EditarCliente([FromBody] Cliente objCliente)
        {
            Cliente oCliente = _dbcontext.Clientes.Find(objCliente.ClienteId);

            if (oCliente == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                return BadRequest("Cliente no encontrado!");
            }

            try
            {
                oCliente.Contraseña = objCliente.Contraseña is null ? oCliente.Contraseña : objCliente.Contraseña;
                oCliente.Estado = objCliente.Estado is null ? oCliente.Estado : objCliente.Estado;

                _dbcontext.Clientes.Update(oCliente);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Cliente actualizado!"});
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
