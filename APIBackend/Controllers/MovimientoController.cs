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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("crearMovimiento")]
        public IActionResult crearMovimiento([FromBody] Movimiento objMovimiento)
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
    }
}
