using APIBackend.Modelos;
using APIBackend.Responses;
using APIBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Services
{
    public class MovimientoService : MovimientoInterface
    {
        private readonly BackendapiContext _dbcontext;

        public MovimientoService(BackendapiContext _context)
        {
            _dbcontext = _context;
        }

        public List<MovimientoRes> ListarMovimientos()
        {
            try
            {
                var lista = _dbcontext.Movimientos.Include(c => c.Cuenta)
                    .ThenInclude(m => m.Cliente)
                    .Select(c => new MovimientoRes
                    {
                        Id = (int)c.CuentaId,
                        Fecha = (DateTime)c.FechaMovimiento,
                        NombreCliente = c.Cuenta.Cliente.Persona.Nombre,
                        NumeroCuenta = (int)c.Cuenta.NumeroCuenta,
                        TipoCuenta = c.Cuenta.TipoCuenta,
                        SaldoInicial = (int)c.Cuenta.SaldoInicial,
                        Estado = c.Cuenta.Estado,
                        Movimiento = c.TipoMovimiento,
                        ValorMovimiento = (int)c.ValorMovimiento,
                        SaldoDisponible = (int)c.SaldoDisponible

                    }).ToList();

                if (lista.Count == 0)
                {
                    throw new Exception("Por el momento no hay movimientos realizados");
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al listar los movimientos");
            }
        }

        public List<MovimientoRes> ObtenerMovimiento(int idMovimiento)
        {
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(idMovimiento);

            if (oMovimiento == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                throw new Exception("No se encontro el movimiento");
            }

            try
            {
                var movimientoporId = _dbcontext.Movimientos.Where(p => p.CuentaId == idMovimiento)
                    .Select(c => new MovimientoRes
                    {
                        Id = (int)c.CuentaId,
                        Fecha = (DateTime)c.FechaMovimiento,
                        NombreCliente = c.Cuenta.Cliente.Persona.Nombre,
                        NumeroCuenta = (int)c.Cuenta.NumeroCuenta,
                        TipoCuenta = c.Cuenta.TipoCuenta,
                        SaldoInicial = (int)c.Cuenta.SaldoInicial,
                        Estado = c.Cuenta.Estado,
                        Movimiento = c.TipoMovimiento,
                        ValorMovimiento = (int)c.ValorMovimiento,
                        SaldoDisponible = (int)c.SaldoDisponible

                    })
                    .ToList();

                return movimientoporId;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el movimiento", ex);
            }
        }

        public void GuardarMovimiento(Movimiento objMovimiento)
        {
            if (objMovimiento == null)
            {
                throw new ArgumentNullException(nameof(objMovimiento));
            }

            _dbcontext.Movimientos.Add(objMovimiento);
            _dbcontext.SaveChanges();
        }

        public void EditarMovimiento(Movimiento objMovimiento)
        {
            var movimientoExistente = _dbcontext.Movimientos.FirstOrDefault(m => m.MovimientoId == objMovimiento.MovimientoId);
            if (movimientoExistente == null)
            {
                throw new ArgumentException($"No se encontró el movimiento con el ID: {objMovimiento.MovimientoId}");
            }

            // Actualizar los campos necesarios del movimiento existente con los valores del movimiento actualizado
            movimientoExistente.TipoMovimiento = objMovimiento.TipoMovimiento;
            movimientoExistente.ValorMovimiento = objMovimiento.ValorMovimiento;
            movimientoExistente.SaldoDisponible = objMovimiento.SaldoDisponible;

            _dbcontext.SaveChanges();
        }

        public void EliminarMovimiento(int idMovimiento)
        {
            var movimientoExistente = _dbcontext.Movimientos.FirstOrDefault(m => m.MovimientoId == idMovimiento);
            if (movimientoExistente == null)
            {
                throw new ArgumentException($"No se encontró el movimiento con el ID: {idMovimiento}");
            }

            _dbcontext.Movimientos.Remove(movimientoExistente);
            _dbcontext.SaveChanges();
        }
    }
}
