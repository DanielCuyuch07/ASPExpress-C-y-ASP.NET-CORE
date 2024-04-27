using Microsoft.EntityFrameworkCore;
using ProyectMVC.Models;

namespace ProyectMVC.Interfaces.Contrato
{
    public interface IUsuarioServices
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
