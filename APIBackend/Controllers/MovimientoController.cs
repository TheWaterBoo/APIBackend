using APIBackend.Modelos;
using APIBackend.Responses;
using APIBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly MovimientoInterface _movimientoInterface;
        public MovimientoController(MovimientoInterface _service)
        {
            _movimientoInterface = _service;
        }

        [HttpGet]
        [Route("listarMovimientos")]
        public IActionResult ListarMovimientos()
        {
            try
            {
                var lista = _movimientoInterface.ListarMovimientos();
                return StatusCode(StatusCodes.Status200OK, new { response = lista });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("movimiento/{idMovimiento:int}")]
        public IActionResult Movimiento(int idMovimiento)
        {
            try
            {
                var movimientoporId = _movimientoInterface.ObtenerMovimiento(idMovimiento);
                return StatusCode(StatusCodes.Status200OK, new { response = movimientoporId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("crearMovimiento")]
        public IActionResult CrearMovimiento([FromBody] Movimiento objMovimiento)
        {
            try
            {
                _movimientoInterface.GuardarMovimiento(objMovimiento);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "nuevo movimiento creado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarMovimiento")]
        public IActionResult EditarMovimiento([FromBody] Movimiento objMovimiento)
        {
            try
            {
                _movimientoInterface.EditarMovimiento(objMovimiento);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Movimiento editado" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarMovimiento/{idMovimiento}")]
        public IActionResult EliminarMovimiento(int idMovimiento)
        {
            try
            {
                _movimientoInterface.EliminarMovimiento(idMovimiento);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Movimiento eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
