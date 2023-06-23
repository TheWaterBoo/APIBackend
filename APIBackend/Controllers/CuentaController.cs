using APIBackend.Modelos;
using APIBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly CuentaInterface _cuentaInterface;
        public CuentaController(CuentaInterface _service)
        {
            _cuentaInterface = _service;
        }

        [HttpGet]
        [Route("listarCuentas")]
        public IActionResult ListarCuenta()
        {
            try
            {
                var lista = _cuentaInterface.ListarCuentas();
                return StatusCode(StatusCodes.Status200OK, new { response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("cuenta/{idCuenta:int}")]
        public IActionResult Cuenta(int idCuenta)
        {
            try 
            {
                var cuentaPorId = _cuentaInterface.ObtenerCuenta(idCuenta);
                return StatusCode(StatusCodes.Status200OK, new { response = cuentaPorId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("crearCuenta")]
        public IActionResult crearCuenta([FromBody] Cuenta objCuenta)
        {
            try
            {
                _cuentaInterface.GuardarCuenta(objCuenta);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "cuenta agregada" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarCuenta")]
        public IActionResult editarCuenta([FromBody] Cuenta objCuenta)
        {
            try
            {
                _cuentaInterface.EditarCuenta(objCuenta);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Cuenta actualizada!" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarCuenta/{idCuenta:int}")]
        public IActionResult eliminarCuenta(int idCuenta)
        {
            try
            {
                _cuentaInterface.EliminarCuenta(idCuenta);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "cuenta eliminada" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
