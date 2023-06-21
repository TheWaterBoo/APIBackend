using APIBackend.Modelos;
using APIBackend.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        public readonly BackendapiContext _dbcontext;
        public CuentaController(BackendapiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listarCuentas")]
        public IActionResult ListarCuenta()
        {
            //List<Cuenta> lista = new List<Cuenta>();

            try
            {
                var lista = _dbcontext.Cuenta.Include(c => c.oCliente)
                    .ThenInclude(m => m.oPersona)
                    .Select(c => new CuentaRes
                    {
                        numeroCuenta = c.NumeroCuenta,
                        tipo = c.TipoCuenta,
                        saldoInicial = (int)c.SaldoInicial,
                        estado = c.Estado,
                        nombreCliente = c.oCliente.oPersona.Nombre

                    }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("cuenta/{idCuenta:int}")]
        public IActionResult Cuenta(int idCuenta)
        {
            Cuenta oCuenta = _dbcontext.Cuenta.Find(idCuenta);

            if (oCuenta == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                return BadRequest("Cliente no encontrado!");
            }

            try
            {
                var cuentaPorId = _dbcontext.Cuenta.Include(c => c.oCliente)
                    .ThenInclude(m => m.oPersona)
                    .Where(p => p.Id == idCuenta)
                    .Select(c => new CuentaRes
                    {
                        numeroCuenta = c.NumeroCuenta,
                        tipo = c.TipoCuenta,
                        saldoInicial = (int)c.SaldoInicial,
                        estado = c.Estado,
                        nombreCliente = c.oCliente.oPersona.Nombre

                    }).FirstOrDefault();

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
            Cliente oCliente = _dbcontext.Clientes.Find(objCuenta.Id);

            if (oCliente == null)
            {
                return BadRequest("Cliente no encontrado!");
            }

            Persona oPersona = _dbcontext.Personas.Find(oCliente.ClienteId);

            if (oPersona == null)
            {
                return BadRequest("persona no encontrada!");
            }

            try
            {
                objCuenta.oCliente = oCliente;
                objCuenta.oCliente.oPersona = oPersona;

                _dbcontext.Cuenta.Add(objCuenta);
                _dbcontext.SaveChanges();

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
            Cuenta oCuenta = _dbcontext.Cuenta.Find(objCuenta.Id);

            if (oCuenta == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                return BadRequest("Cuenta no encontrada!");
            }

            try
            {
                oCuenta.TipoCuenta = objCuenta.TipoCuenta is null ? oCuenta.TipoCuenta : objCuenta.TipoCuenta;
                oCuenta.NumeroCuenta = objCuenta.NumeroCuenta is null ? oCuenta.NumeroCuenta : objCuenta.NumeroCuenta;
                oCuenta.SaldoInicial = objCuenta.SaldoInicial is null ? oCuenta.SaldoInicial : objCuenta.SaldoInicial;
                oCuenta.Estado = objCuenta.Estado is null ? oCuenta.Estado : objCuenta.Estado;

                _dbcontext.Cuenta.Update(oCuenta);
                _dbcontext.SaveChanges();

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
            Cuenta oCuenta = _dbcontext.Cuenta.Find(idCuenta);

            if (oCuenta == null)
            {
                return BadRequest("Cuenta no encontrada!");
            }

            try
            {
                _dbcontext.Cuenta.Remove(oCuenta);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "cuenta eliminada" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
