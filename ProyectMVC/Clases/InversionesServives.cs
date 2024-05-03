using Microsoft.Data.SqlClient.DataClassification;
using ProyectMVC.Interfaces;
using ProyectMVC.Models;

namespace ProyectMVC.Clases
{
    public class InversionesServives : IInversiones
    {
        private readonly DbbancolombiaContext _dbContext;

        public InversionesServives(DbbancolombiaContext context)
        {
            _dbContext = context;
        }

        public List<Inversione> GetInversiones() {
            try
            {
                return _dbContext.Inversiones.ToList();

            } catch (Exception ex)
            {
                return new List<Inversione>();

            }
        }

        public bool DeleteInversiones(int IdInversion, out string message)
        {
            try
            {
                // Intenta encontrar la inversión por su ID
                var idInversionesDelete = _dbContext.Inversiones.Find(IdInversion);

                // Verifica si se encontró la inversión
                if (idInversionesDelete != null)
                {
                    // Elimina la inversión
                    _dbContext.Inversiones.Remove(idInversionesDelete);

                    // Guarda los cambios en la base de datos
                    _dbContext.SaveChanges();

                    // Indica que la inversión fue eliminada con éxito
                    Console.WriteLine("Inversión eliminada exitosamente");
                    message = "Inversión eliminada exitosamente";
                    return true;
                }
                else
                {
                    // Indica que la inversión no fue encontrada
                    Console.WriteLine("La inversión no fue encontrada");
                    message = "La inversión no fue encontrada";
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Captura la excepción y registra los detalles
                Console.WriteLine("Error al eliminar la inversión: " + ex.Message);
                message = "Error interno del servidor al eliminar la inversión: " + ex.Message;
                return false;
            }
        }



    }
}
