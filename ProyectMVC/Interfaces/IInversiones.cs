using ProyectMVC.Models;

namespace ProyectMVC.Interfaces
{
    public interface IInversiones
    {
        List<Inversione> GetInversiones();

        bool DeleteInversiones(int IdInversion, out string message);


    }
}
