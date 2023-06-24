using APIBackend.Modelos;
using APIBackend.Responses;

namespace APIBackend.Services.Interfaces
{
    public interface MovimientoInterface
    {
        List<MovimientoRes> ListarMovimientos();
        List<MovimientoRes> ObtenerMovimiento(int idMovimiento);
        void GuardarMovimiento(Movimiento objMovimiento);
        void EditarMovimiento(Movimiento objMovimiento);
        void EliminarMovimiento(int idMovimiento);
    }
}
