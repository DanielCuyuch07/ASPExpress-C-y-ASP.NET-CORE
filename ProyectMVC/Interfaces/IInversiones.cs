using Microsoft.AspNetCore.Mvc;
using ProyectMVC.Models;

namespace ProyectMVC.Interfaces
{
    public interface IInversiones
    {
        List<Inversione> GetInversiones();

        bool DeleteInversiones(int IdInversion, out string message);

        FileResult GenerarListadoDeInversiones(string nombreArchivoInversiones, IEnumerable<Inversione> archivoInversionesList);

    }
}
