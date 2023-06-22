using APIBackend.Modelos;
using APIBackend.Modelos.Responses;

namespace APIBackend.Services.Interfaces
{
    public interface CuentaInterface
    {
        List<CuentaRes> ListarCuentas();
        CuentaRes ObtenerCuenta(int idCuenta);
        void GuardarCuenta(Cuenta objCuenta);
        void EditarCuenta(Cuenta objCuenta);
        void EliminarCuenta(int idCuenta);
    }
}
