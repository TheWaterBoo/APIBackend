using APIBackend.Services.Interfaces;
using APIBackend.Modelos;
using APIBackend.Modelos.Responses;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Services
{
    public class ClientService : ClientInterface
    {
        private readonly BackendapiContext _dbcontext;

        public ClientService(BackendapiContext _context)
        {
            _dbcontext = _context;
        }

        public List<ClienteRes> ListarClientes()
        {
            try
            {
                var lista = _dbcontext.Clientes.Include(c => c.oPersona)
                    .Select(c => new ClienteRes
                    {
                        ID = c.ClienteId,
                        Nombre = c.oPersona.Nombre,
                        Direccion = c.oPersona.Direccion,
                        Telefono = c.oPersona.Telefono,
                        Genero = c.oPersona.Genero,
                        Estado = c.Estado
                    }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de clientes", ex);
            }
        }

        public ClienteRes ObtenerCliente(int idCliente)
        {
            Cliente oCliente = _dbcontext.Clientes.Find(idCliente);

            if (oCliente == null)
            {
                throw new ArgumentException("Cliente no encontrado");
            }

            try
            {
                var clientePorId = _dbcontext.Clientes.Include(c => c.oPersona)
                    .Where(p => p.ClienteId == idCliente)
                    .Select(c => new ClienteRes
                    {
                        Nombre = c.oPersona.Nombre,
                        Direccion = c.oPersona.Direccion,
                        Telefono = c.oPersona.Telefono,
                        Genero = c.oPersona.Genero,
                        Estado = c.Estado
                    }).FirstOrDefault();

                return clientePorId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el cliente por ID", ex);
            }
        }

        public void GuardarCliente(Cliente objCliente)
        {
            try
            {
                _dbcontext.Clientes.Add(objCliente);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el cliente", ex);
            }
        }

        public void EditarCliente(Cliente objCliente)
        {
            Cliente oCliente = _dbcontext.Clientes.Find(objCliente.ClienteId);

            if (oCliente == null)
            {
                throw new ArgumentException("Cliente no encontrado");
            }

            try
            {
                oCliente.Contraseña = objCliente.Contraseña ?? oCliente.Contraseña;
                oCliente.Estado = objCliente.Estado ?? oCliente.Estado;

                _dbcontext.Clientes.Update(oCliente);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el cliente", ex);
            }
        }

        public void EliminarCliente(int idCliente)
        {
            Cliente oCliente = _dbcontext.Clientes.Find(idCliente);
            Persona oPersona = _dbcontext.Personas.Find(idCliente);

            if (oCliente == null || oPersona == null)
            {
                throw new ArgumentException("Cliente o persona no encontrada");
            }

            try
            {
                _dbcontext.Clientes.Remove(oCliente);
                _dbcontext.Personas.Remove(oPersona);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente", ex);
            }
        }
    }
}
