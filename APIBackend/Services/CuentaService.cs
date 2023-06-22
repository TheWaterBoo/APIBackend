using APIBackend.Modelos;
using APIBackend.Modelos.Responses;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Services
{
    public class CuentaService
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
                        numeroCuenta = c.NumeroCuenta,
                        tipo = c.TipoCuenta,
                        saldoInicial = (int)c.SaldoInicial,
                        estado = c.Estado,
                        nombreCliente = c.oCliente.oPersona.Nombre

                    }).ToList();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
