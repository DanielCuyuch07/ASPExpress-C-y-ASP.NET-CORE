using ProyectMVC.Models;

namespace ProyectMVC.Interfaces
{
    public interface IPersona
    {
        List<Persona> GetPerson();
        bool DeletePersona(int idPersona, out string message);   

    }
}
