﻿using APIBackend.Modelos;
using APIBackend.Responses;
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
            //List<Movimiento> lista = new List<Movimiento>();

            try
            {
                var lista = _dbcontext.Movimientos.Include(c => c.oCuenta)
                    .ThenInclude(m => m.oCliente)
                    .Select(c => new MovimientoRes
                    {
                        Fecha = (DateTime)c.Fecha,
                        NombreCliente = c.oCuenta.oCliente.oPersona.Nombre,
                        NumeroCuenta = c.oCuenta.NumeroCuenta,
                        TipoCuenta = c.oCuenta.TipoCuenta,
                        SaldoInicial = (int)c.oCuenta.SaldoInicial,
                        Estado = c.oCuenta.Estado,
                        Movimiento = c.TipoMovimiento,
                        SaldoDisponible = (int)c.Saldo

                    }).ToList();

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
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(idMovimiento);

            if (oMovimiento == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                return BadRequest("Cliente no encontrado!");
            }

            try
            {
                var movimientoporId = _dbcontext.Movimientos.Include(c => c.oCuenta)
                    .ThenInclude(m => m.oCliente)
                    .ThenInclude(cli => cli.oPersona)
                    .Where(p => p.Id == idMovimiento)
                    .Select(c => new MovimientoRes
                    {
                        Fecha = (DateTime)c.Fecha,
                        NombreCliente = c.oCuenta.oCliente.oPersona.Nombre,
                        NumeroCuenta = c.oCuenta.NumeroCuenta,
                        TipoCuenta = c.oCuenta.TipoCuenta,
                        SaldoInicial = (int)c.oCuenta.SaldoInicial,
                        Estado = c.oCuenta.Estado,
                        Movimiento = c.TipoMovimiento,
                        SaldoDisponible = (int)c.Saldo

                    })
                    .FirstOrDefault();

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
                Cuenta oCuenta = _dbcontext.Cuenta.Find(objMovimiento.Id);

                if(oCuenta == null)
                {
                    return BadRequest("No se encontro la cuenta");
                }

                Cliente oCliente = _dbcontext.Clientes.Find(oCuenta.Id);

                if(oCliente == null)
                {
                    return BadRequest("No se encontro el cliente");
                }

                Persona oPersona = _dbcontext.Personas.Find(oCliente.ClienteId);

                if (oPersona == null)
                {
                    return BadRequest("No se encontro la persona proporcionada");
                }

                objMovimiento.oCuenta = oCuenta;
                objMovimiento.oCuenta.oCliente = oCliente;
                objMovimiento.oCuenta.oCliente.oPersona = oPersona;

                int saldoInicial = (int)oCuenta.SaldoInicial;
                int valorMovimiento = (int)objMovimiento.Valor;

                string[] cadena = objMovimiento.TipoMovimiento.Split(' ');
                string tipoMovimiento = cadena[0].ToLower();

                if (tipoMovimiento == "retiro")
                {
                    objMovimiento.Saldo = saldoInicial - valorMovimiento;
                }
                else if (tipoMovimiento == "deposito")
                {
                    objMovimiento.Saldo = saldoInicial + valorMovimiento;
                }
                else
                {
                    return BadRequest("Tipo de movimiento inválido");
                }

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

            if(oMovimiento == null)
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminarMovimiento/{idMovimiento}")]
        public IActionResult EliminarMovimiento(int idMovimiento)
        {
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(idMovimiento);

            if (oMovimiento == null)
            {
                return BadRequest("Movimiento no encontrado");
            }

            try
            {
                _dbcontext.Movimientos.Remove(oMovimiento);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Movimiento eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
