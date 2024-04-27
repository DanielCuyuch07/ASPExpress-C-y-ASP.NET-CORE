using ProyectMVC.Interfaces;
using ProyectMVC.Models;

namespace ProyectMVC.Clases
{
    public class ClientesServices : IClientes
    {
        private readonly DbbancolombiaContext _bbancolombiaContext;

        public ClientesServices(DbbancolombiaContext context)
        {
            _bbancolombiaContext = context;
        }

        public List<Cliente> GetClientes()
        {
            try { 
                return _bbancolombiaContext.Clientes.ToList();
            }catch(Exception ex) { 
                return new List<Cliente>(); 
            }
        }



        public bool DeleteClientes(int idCliente, out string message)
        {
            try
            {
                var idClienteDelete = _bbancolombiaContext.Clientes.Find(idCliente);
                Console.WriteLine(idClienteDelete);
                if (idClienteDelete != null)
                {
                    _bbancolombiaContext.Clientes.Remove(idClienteDelete);
                    _bbancolombiaContext.SaveChanges();
                    Console.WriteLine("Cliente eliminada exitosamente");
                    message = "Cliente eliminada exitosamente";
                    return true;
                }
                else
                {
                    Console.WriteLine("La persona no fue encontrada");
                    message = "La persona no fue encontrada";
                    return false;
                }

            }catch(Exception ex)
            {
                Console.WriteLine("Error al eliminar la persona: " + ex.Message);
                message = "Error interno del servidor al eliminar la persona: " + ex.Message;
                return false;
            }
        }


    }
}
