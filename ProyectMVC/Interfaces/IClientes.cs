using ProyectMVC.Models;

namespace ProyectMVC.Interfaces
{
    public interface IClientes
    {
        List<Cliente> GetClientes();

        bool DeleteClientes(int idCliente, out string message);

    }
}
