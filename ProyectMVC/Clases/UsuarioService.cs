using Microsoft.EntityFrameworkCore;
using ProyectMVC.Interfaces.Contrato;
using ProyectMVC.Models;

namespace ProyectMVC.Clases
{
    public class UsuarioService : IUsuarioServices
    {
        private readonly DbbancolombiaContext _dbContext;

        public UsuarioService(DbbancolombiaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.Where(u => u.Correo == correo && u.Clave == clave)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }

    }
}
