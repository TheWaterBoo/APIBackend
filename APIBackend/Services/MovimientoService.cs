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

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al listar los movimientos");
            }
        }

        public MovimientoRes ObtenerMovimiento(int idMovimiento)
        {
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(idMovimiento);

            if (oMovimiento == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                throw new Exception("No se encontro el movimiento");
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
                return movimientoporId;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el movimiento", ex);
            }
        }

        public void GuardarMovimiento(Movimiento objMovimiento)
        {
            try
            {
                Cuenta oCuenta = _dbcontext.Cuenta.Find(objMovimiento.Id);

                if (oCuenta == null)
                {
                    throw new Exception("No se encontro la cuenta");
                }

                Cliente oCliente = _dbcontext.Clientes.Find(oCuenta.Id);

                if (oCliente == null)
                {
                    throw new Exception("No se encontro el cliente");
                }

                Persona oPersona = _dbcontext.Personas.Find(oCliente.ClienteId);

                if (oPersona == null)
                {
                    throw new Exception("No se encontro la persona proporcionada");
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
                    throw new Exception("El movimiento que deseas realizar no esta disponible");
                }

                _dbcontext.Movimientos.Add(objMovimiento);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al crear el movimiento\n",ex);
            }
        }

        public void EditarMovimiento(Movimiento objMovimiento)
        {
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(objMovimiento.Id);

            if (oMovimiento == null)
            {
                throw new Exception("Movimiento no encontrado");
            }

            try
            {
                oMovimiento.TipoMovimiento = objMovimiento.TipoMovimiento is null ? oMovimiento.TipoMovimiento : objMovimiento.TipoMovimiento;
                oMovimiento.Fecha = objMovimiento.Fecha is null ? oMovimiento.Fecha : objMovimiento.Fecha;
                oMovimiento.Saldo = objMovimiento.Saldo is null ? oMovimiento.Saldo : objMovimiento.Saldo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al editar el movimiento", ex);
            }
        }

        public void EliminarMovimiento(int idMovimiento)
        {
            Movimiento oMovimiento = _dbcontext.Movimientos.Find(idMovimiento);

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
            }
        }
    }
}
