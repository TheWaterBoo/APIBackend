using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIBackend.Modelos;
using APIBackend.Responses;

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
            //List<Cliente> lista = new List<Cliente>();

            try
            {
                var lista = _dbcontext.Clientes.Include(c => c.oPersona)
                    .Select(c => new ClienteRes
                    {
                        Nombre = c.oPersona.Nombre,
                        Direccion = c.oPersona.Direccion,
                        Telefono = c.oPersona.Telefono,
                        Contraseña = c.Contraseña,
                        Estado = c.Estado

                    }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
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
                var clientePorId = _dbcontext.Clientes.Include(c => c.oPersona)
                    .Where(p => p.ClienteId == idCliente)
                    .Select(c => new ClienteRes
                    {
                        Nombre = c.oPersona.Nombre,
                        Direccion = c.oPersona.Direccion,
                        Telefono = c.oPersona.Telefono,
                        Contraseña = c.Contraseña,
                        Estado = c.Estado

                    }).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { response = clientePorId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
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

        [HttpDelete]
        [Route("eliminarCliente/{idCliente:int}")]
        public IActionResult eliminarCliente(int idCliente)
        {
            Cliente oCliente = _dbcontext.Clientes.Find(idCliente);
            Persona oPersona = _dbcontext.Personas.Find(idCliente);

            if (oCliente == null || oPersona == null)
            {
                return BadRequest("Cliente o persona no encontrada");
            }

            try
            {
                _dbcontext.Clientes.Remove(oCliente);
                _dbcontext.Personas.Remove(oPersona);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Cliente y persona eliminada" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
