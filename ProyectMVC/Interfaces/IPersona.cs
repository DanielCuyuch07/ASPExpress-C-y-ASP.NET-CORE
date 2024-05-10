using ProyectMVC.Models;
using Microsoft.AspNetCore.Mvc;



namespace ProyectMVC.Interfaces
{
    public interface IPersona
    {
        List<Persona> GetPerson();
        bool DeletePersona(int idPersona, out string message);

        FileResult GererarListadoPersonas(string nombreArchivo, IEnumerable<Persona> personas);
    }
}
