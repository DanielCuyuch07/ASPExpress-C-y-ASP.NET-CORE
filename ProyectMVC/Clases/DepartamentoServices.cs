using ProyectMVC.Interfaces;
using ProyectMVC.Models;

namespace ProyectMVC.Clases
{
    public class DepartamentoServices : IDepartamentos
    {
        private readonly DbbancolombiaContext _dbContext;

        public DepartamentoServices(DbbancolombiaContext context)
        {
            _dbContext = context;
        }  

        public List<Departamento> GetDepartamentos()
        {
            try
            {
                return _dbContext.Departamentos.ToList();
            }catch (Exception ex)
            {
                return new List<Departamento>();

            }
        }

        public bool DeleteDepartamentos(int idDepartamento, out string message)
        {
            try
            {
                var deleteDepartamentos = _dbContext.Departamentos.Find(idDepartamento);
                if (deleteDepartamentos != null)
                {
                    Console.WriteLine("Persona encontrada. Eliminando...");
                    _dbContext.Departamentos.Remove(deleteDepartamentos);
                    _dbContext.SaveChanges();

                    Console.WriteLine("Persona eliminada exitosamente");
                    message = "Persona eliminada exitosamente";
                    return true;
                }
                else
                {
                    Console.WriteLine("La persona no fue encontrada");
                    message = "La persona no fue encontrada";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la persona: " + ex.Message);
                message = "Error interno del servidor al eliminar la persona: " + ex.Message;
                return false;
            }
        }


    }
}
