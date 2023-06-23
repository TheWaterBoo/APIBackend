using APIBackend.Services.Interfaces;
using APIBackend.Modelos;
using APIBackend.Modelos.Responses;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Services
{
    public class CuentaService : CuentaInterface
    {
        private readonly BackendapiContext _dbcontext;

        public CuentaService(BackendapiContext _context)
        {
            _dbcontext = _context;
        }

        public List<CuentaRes> ListarCuentas()
        {
            try
            {
                var lista = _dbcontext.Cuenta.Include(c => c.oCliente)
                    .ThenInclude(m => m.oPersona)
                    .Select(c => new CuentaRes
                    {
                        numeroCuenta = (int)c.NumeroCuenta,
                        tipo = c.TipoCuenta,
                        saldoInicial = (int)c.SaldoInicial,
                        estado = c.Estado,
                        nombreCliente = c.oCliente.oPersona.Nombre

                    }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de cuentas", ex);
            }
        }

        public CuentaRes ObtenerCuenta(int idCuenta)
        {
            Cuenta oCuenta = _dbcontext.Cuenta.Find(idCuenta);

            if (oCuenta == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                throw new Exception("Cuenta no encontrada!");
            }

            try
            {
                var cuentaPorId = _dbcontext.Cuenta.Include(c => c.oCliente)
                    .ThenInclude(m => m.oPersona)
                    .Where(p => p.Id == idCuenta)
                    .Select(c => new CuentaRes
                    {
                        numeroCuenta = (int)c.NumeroCuenta,
                        tipo = c.TipoCuenta,
                        saldoInicial = (int)c.SaldoInicial,
                        estado = c.Estado,
                        nombreCliente = c.oCliente.oPersona.Nombre

                    }).FirstOrDefault();

                return cuentaPorId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cuenta por ID", ex);
            }
        }

        public void GuardarCuenta(Cuenta objCuenta)
        {
            Cliente oCliente = _dbcontext.Clientes.Find(objCuenta.Id);

            if (oCliente == null)
            {
                throw new Exception("Cliente no encontrado!");
            }

            Persona oPersona = _dbcontext.Personas.Find(oCliente.ClienteId);

            if (oPersona == null)
            {
                throw new Exception("persona no encontrada!");
            }

            try
            {
                objCuenta.oCliente = oCliente;
                objCuenta.oCliente.oPersona = oPersona;

                _dbcontext.Cuenta.Add(objCuenta);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception("No se pudo guardar la cuenta");
            }
        }

        public void EditarCuenta(Cuenta objCuenta)
        {
            Cuenta oCuenta = _dbcontext.Cuenta.Find(objCuenta.Id);

            if (oCuenta == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                throw new Exception("Cuenta no encontrada!");
            }

            try
            {
                oCuenta.TipoCuenta = objCuenta.TipoCuenta is null ? oCuenta.TipoCuenta : objCuenta.TipoCuenta;
                oCuenta.NumeroCuenta = objCuenta.NumeroCuenta is null ? oCuenta.NumeroCuenta : objCuenta.NumeroCuenta;
                oCuenta.SaldoInicial = objCuenta.SaldoInicial is null ? oCuenta.SaldoInicial : objCuenta.SaldoInicial;
                oCuenta.Estado = objCuenta.Estado is null ? oCuenta.Estado : objCuenta.Estado;

                _dbcontext.Cuenta.Update(oCuenta);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la cuenta ", ex);
            }
        }

        public void EliminarCuenta(int idCuenta)
        {
            Cuenta oCuenta = _dbcontext.Cuenta.Find(idCuenta);

            if (oCuenta == null)
            {
                throw new Exception("Cuenta no encontrada!");
            }

            try
            {
                _dbcontext.Cuenta.Remove(oCuenta);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la cuenta ", ex);
            }
        }
    }
}
