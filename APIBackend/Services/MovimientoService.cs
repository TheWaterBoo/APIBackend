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
                        NumeroCuenta = c.Cuenta.NumeroCuenta,
                        TipoCuenta = c.Cuenta.TipoCuenta,
                        SaldoInicial = (int)c.Cuenta.SaldoInicial,
                        Estado = c.Cuenta.Estado,
                        Movimiento = c.TipoMovimiento,
                        SaldoDisponible = (int)c.SaldoDisponible

                    }).ToList();

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
                        NumeroCuenta = c.Cuenta.NumeroCuenta,
                        TipoCuenta = c.Cuenta.TipoCuenta,
                        SaldoInicial = (int)c.Cuenta.SaldoInicial,
                        Estado = c.Cuenta.Estado,
                        Movimiento = c.TipoMovimiento,
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
            /*try
            {
                Cuenta oCuenta = _dbcontext.Cuentas.Find(objMovimiento.CuentaId);

                if (oCuenta == null)
                {
                    throw new Exception("No se encontro la cuenta");
                }

                Cliente oCliente = _dbcontext.Clientes.Find(oCuenta.ClienteId);

                if (oCliente == null)
                {
                    throw new Exception("No se encontro el cliente");
                }

                Persona oPersona = _dbcontext.Personas.Find(oCliente.PersonaId);

                if (oPersona == null)
                {
                    throw new Exception("No se encontro la persona proporcionada");
                }

                objMovimiento.Cuenta = oCuenta;
                objMovimiento.Cuenta.Cliente = oCliente;
                objMovimiento.Cuenta.Cliente.Persona = oPersona;

                int saldoInicial = (int)oCuenta.SaldoInicial;
                int valorMovimiento = (int)objMovimiento.ValorMovimiento;

                string[] cadena = objMovimiento.TipoMovimiento.Split(' ');
                string tipoMovimiento = cadena[0].ToLower();

                if (tipoMovimiento == "retiro")
                {
                    objMovimiento.SaldoDisponible = saldoInicial - valorMovimiento;
                }
                else if (tipoMovimiento == "deposito")
                {
                    objMovimiento.SaldoDisponible = saldoInicial + valorMovimiento;
                }
                else
                {
                    throw new Exception("El movimiento que deseas realizar no esta disponible");
                }

                _dbcontext.Movimientos.Add(objMovimiento);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al crear el movimiento\n",ex);
            }*/

            if (objMovimiento == null)
            {
                throw new ArgumentNullException(nameof(objMovimiento));
            }

            _dbcontext.Movimientos.Add(objMovimiento);
            _dbcontext.SaveChanges();
        }

        public void EditarMovimiento(Movimiento objMovimiento)
        {
            /*Movimiento oMovimiento = _dbcontext.Movimientos.Find(objMovimiento.MovimientoId);

            if (oMovimiento == null)
            {
                throw new Exception("Movimiento no encontrado");
            }

            try
            {
                oMovimiento.TipoMovimiento = objMovimiento.TipoMovimiento is null ? oMovimiento.TipoMovimiento : objMovimiento.TipoMovimiento;
                oMovimiento.FechaMovimiento = objMovimiento.FechaMovimiento is null ? oMovimiento.FechaMovimiento : objMovimiento.FechaMovimiento;
                oMovimiento.SaldoDisponible = objMovimiento.SaldoDisponible is null ? oMovimiento.SaldoDisponible : objMovimiento.SaldoDisponible;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al editar el movimiento", ex);
            }*/

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
            /*Movimiento oMovimiento = _dbcontext.Movimientos.Find(idMovimiento);

            if (oMovimiento == null)
            {
                throw new Exception("Movimiento no encontrado");
            }

            try
            {
                _dbcontext.Movimientos.Remove(oMovimiento);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al eliminar un movimiento", ex);
            }*/

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
