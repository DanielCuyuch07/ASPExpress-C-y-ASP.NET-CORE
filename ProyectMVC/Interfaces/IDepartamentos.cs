using Microsoft.AspNetCore.Mvc;
using ProyectMVC.Models;

namespace ProyectMVC.Interfaces
{
    public interface IDepartamentos
    {
        List<Departamento> GetDepartamentos();
        bool DeleteDepartamentos(int idDepartamento, out string message);

        FileResult GenerarListaDepartamento(string nombreArchivo, IEnumerable<Departamento> departamentos);
       
    }
}
