using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIBackend.Modelos;
using APIBackend.Modelos.Responses;
using APIBackend.Services.Interfaces;

namespace APIBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClientInterface _clientService;

        public ClienteController(ClientInterface _service)
        {
            _clientService = _service;
        }

        //Obtener toda la lista de clientes
        [HttpGet]
        [Route("ListarClientes")]
        public IActionResult ListarClientes()
        {
            var lista = _clientService.ListarClientes();
            return StatusCode(StatusCodes.Status200OK, new { response = lista });
        }

        //Obtener cliente por id
        [HttpGet]
        [Route("cliente/{idCliente:int}")]

        public IActionResult Cliente(int idCliente)
        {
            var cliente = _clientService.ObtenerCliente(idCliente);
            return StatusCode(StatusCodes.Status200OK, new { response = cliente });
        }

        //Crear cliente nuevo (incluye persona tambien)
        [HttpPost]
        [Route("crearCliente")]
        public IActionResult GuardarCliente([FromBody] Cliente objCliente)
        {
             _clientService.GuardarCliente(objCliente);
             return StatusCode(StatusCodes.Status200OK, new { mensaje = "cliente guardado" });
        }

        //Editar Cliente ya existente
        [HttpPut]
        [Route("editarCliente")]
        public IActionResult EditarCliente([FromBody] Cliente objCliente)
        {
            _clientService.EditarCliente(objCliente);
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Cliente actualizado!"});
        }

        [HttpDelete]
        [Route("eliminarCliente/{idCliente:int}")]
        public IActionResult eliminarCliente(int idCliente)
        {
            try
            {
                _clientService.EliminarCliente(idCliente);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Cliente y persona eliminada" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
