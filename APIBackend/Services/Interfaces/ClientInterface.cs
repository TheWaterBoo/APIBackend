using APIBackend.Modelos;
using APIBackend.Modelos.Responses;

namespace APIBackend.Services.Interfaces
{
    public interface ClientInterface
    {
        List<ClienteRes> ListarClientes();
        ClienteRes ObtenerCliente(int idCliente);
        void GuardarCliente(Cliente objCliente);
        void EditarCliente(Cliente objCliente);
        void EliminarCliente(int idCliente);
    }
}
