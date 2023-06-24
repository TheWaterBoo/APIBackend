using APIBackend.Services.Interfaces;
using APIBackend.Modelos;
using APIBackend.Modelos.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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
                var lista = _dbcontext.Clientes.Include(c => c.Persona)
                    .Select(c => new ClienteRes
                    {
                        ID = (int)c.PersonaId,
                        Nombre = c.Persona.Nombre,
                        Direccion = c.Persona.Direccion,
                        Telefono = c.Persona.Telefono,
                        Genero = c.Persona.Genero,
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
                var clientePorId = _dbcontext.Clientes.Include(c => c.Persona)
                    .Where(p => p.ClienteId == idCliente)
                    .Select(c => new ClienteRes
                    {
                        ID = (int)c.PersonaId,
                        Nombre = c.Persona.Nombre,
                        Direccion = c.Persona.Direccion,
                        Telefono = c.Persona.Telefono,
                        Genero = c.Persona.Genero,
                        Estado = c.Estado
                    }).FirstOrDefault();

                return clientePorId;

                /*var cliente = _dbcontext.Clientes.FirstOrDefault(c => c.ClienteId == idCliente);
                if (cliente != null)
                {
                    cliente.Persona = _dbcontext.Personas.FirstOrDefault(p => p.PersonaId == cliente.PersonaId);
                }
                else
                {
                    throw new Exception("No se encontro ningun cliente con ese id");
                }

                return cliente;*/
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
                if (objCliente == null)
                {
                    throw new ArgumentNullException(nameof(objCliente));
                }

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
            /*Cliente oCliente = _dbcontext.Clientes.Find(objCliente.ClienteId);

            if (oCliente == null)
            {
                throw new ArgumentException("Cliente no encontrado");
            }*/

            /*try
            {
                oCliente.Contrasena = objCliente.Contrasena ?? oCliente.Contrasena;
                oCliente.Estado = objCliente.Estado ?? oCliente.Estado;

                _dbcontext.Clientes.Update(oCliente);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el cliente", ex);
            }*/

            if (objCliente == null)
            {
                throw new ArgumentNullException(nameof(objCliente));
            }

            var clienteExistente = _dbcontext.Clientes.FirstOrDefault(c => c.ClienteId == objCliente.ClienteId);
            if (clienteExistente != null)
            {
                clienteExistente.Persona.Nombre = objCliente.Persona.Nombre;
                // Actualiza otras propiedades del cliente según sea necesario

                _dbcontext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"No se encontró el cliente con el ID: {objCliente.ClienteId}");
            }
        }

        public void EliminarCliente(int idCliente)
        {
            /*Cliente oCliente = _dbcontext.Clientes.Find(idCliente);
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
            }*/

            var clienteExistente = _dbcontext.Clientes.FirstOrDefault(c => c.ClienteId == idCliente);
            if (clienteExistente != null)
            {
                _dbcontext.Clientes.Remove(clienteExistente);
                _dbcontext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"No se encontró el cliente con el ID: {idCliente}");
            }
        }
    }
}
