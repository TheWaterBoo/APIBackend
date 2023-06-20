using APIBackend.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        public readonly BackendapiContext _dbcontext;
        public MovimientoController(BackendapiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listarMovimientos")]
        public IActionResult ListarMovimientos()
        {
            List<Movimiento> lista = new List<Movimiento>();

            try
            {
                lista = _dbcontext.Movimientos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
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
                _dbcontext.Movimientos.Add(objMovimiento);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "nuevo movimiento creado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarMovimiento")]
        public IActionResult EditarMovimiento([FromBody] Movimiento objMovimiento)
        {
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(objMovimiento.Id);

            if (oMovimiento == null)
            {
                return BadRequest("Movimiento no encontrado");
            }

            try
            {
                oMovimiento.TipoMovimiento = objMovimiento.TipoMovimiento is null ? oMovimiento.TipoMovimiento : objMovimiento.TipoMovimiento;
                oMovimiento.Fecha = objMovimiento.Fecha is null ? oMovimiento.Fecha : objMovimiento.Fecha;
                oMovimiento.Saldo = objMovimiento.Saldo is null ? oMovimiento.Saldo : objMovimiento.Saldo;

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Movimiento editado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarMovimiento/{idMovimiento}")]
        public IActionResult EliminarMovimiento(int idMovimiento)
        {
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(idMovimiento);

            if(oMovimiento == null)
            {
                return BadRequest("Movimiento no encontrado");
            }

            try
            {
                _dbcontext.Movimientos.Remove(oMovimiento);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Movimiento eliminado!" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
