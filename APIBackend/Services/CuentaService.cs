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
                var lista = _dbcontext.Cuentas.Include(c => c.Cliente)
                    .ThenInclude(m => m.Persona)
                    .Select(c => new CuentaRes
                    {
                        clienteId = (int)c.ClienteId,
                        numeroCuenta = c.NumeroCuenta,
                        tipo = c.TipoCuenta,
                        saldoInicial = (int)c.SaldoInicial,
                        estado = c.Estado,
                        nombreCliente = c.Cliente.Persona.Nombre
                    }).ToList();

                if (lista.Count == 0)
                {
                    throw new Exception("No existen cuentas para mostrar!");
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de cuentas", ex);
            }
        }

        public List<CuentaRes> ObtenerCuenta(int idCuenta)
        {
            Cuenta oCuenta = _dbcontext.Cuentas.Find(idCuenta);

            if (oCuenta == null)
            {
                //Excepcion por si no se encuentra el cliente con el id
                throw new Exception("Cuenta/s no encontrada!");
            }

            try
            {
                var cuentaPorId = _dbcontext.Cuentas.Include(c => c.Cliente)
                    .ThenInclude(m => m.Persona)
                    .Where(p => p.ClienteId == idCuenta)
                    .Select(c => new CuentaRes
                    {
                        clienteId = (int)c.ClienteId,
                        numeroCuenta = c.NumeroCuenta,
                        tipo = c.TipoCuenta,
                        saldoInicial = (int)c.SaldoInicial,
                        estado = c.Estado,
                        nombreCliente = c.Cliente.Persona.Nombre

                    }).ToList();

                if (cuentaPorId.Count == 0)
                {
                    throw new Exception("No se encontro ninguna cuenta con el id de cliente");
                }

                return cuentaPorId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cuenta por ID", ex);
            }
        }

        public void GuardarCuenta(Cuenta objCuenta)
        {
            /*Cliente oCliente = _dbcontext.Clientes.Find(objCuenta.CuentaId);

            if (oCliente == null)
            {
                throw new Exception("Cliente no encontrado!");
            }

            Persona oPersona = _dbcontext.Personas.Find(oCliente.ClienteId);*/

            /*if (oPersona == null)
            {
                throw new Exception("persona no encontrada!");
            }

            try
            {
                objCuenta.Cliente = oCliente;
                objCuenta.Cliente.Persona = oPersona;

                _dbcontext.Cuentas.Add(objCuenta);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception("No se pudo guardar la cuenta");
            }*/

            if (objCuenta == null)
            {
                throw new ArgumentNullException(nameof(objCuenta));
            }

            _dbcontext.Cuentas.Add(objCuenta);
            _dbcontext.SaveChanges();
        }

        public void EditarCuenta(Cuenta objCuenta)
        {
            /*Cuenta oCuenta = _dbcontext.Cuentas.Find(objCuenta.CuentaId);

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

                _dbcontext.Cuentas.Update(oCuenta);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la cuenta ", ex);
            }*/

            if (objCuenta == null)
            {
                throw new ArgumentNullException(nameof(objCuenta));
            }

            var cuentaExistente = _dbcontext.Cuentas.FirstOrDefault(c => c.CuentaId == objCuenta.CuentaId);
            if (cuentaExistente != null)
            {
                cuentaExistente.TipoCuenta = objCuenta.TipoCuenta;
                cuentaExistente.SaldoInicial = objCuenta.SaldoInicial;
                cuentaExistente.Estado = objCuenta.Estado;
                // Actualiza otras propiedades de la cuenta según sea necesario

                _dbcontext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"No se encontró la cuenta con el ID: {objCuenta.CuentaId}");
            }
        }

        public void EliminarCuenta(int idCuenta)
        {
            /*Cuenta oCuenta = _dbcontext.Cuentas.Find(idCuenta);

            if (oCuenta == null)
            {
                throw new Exception("Cuenta no encontrada!");
            }

            try
            {
                _dbcontext.Cuentas.Remove(oCuenta);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la cuenta ", ex);
            }*/

            var cuentaExistente = _dbcontext.Cuentas.FirstOrDefault(c => c.CuentaId == idCuenta);
            if (cuentaExistente != null)
            {
                _dbcontext.Cuentas.Remove(cuentaExistente);
                _dbcontext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"No se encontró la cuenta con el ID: {idCuenta}");
            }
        }
    }
}
