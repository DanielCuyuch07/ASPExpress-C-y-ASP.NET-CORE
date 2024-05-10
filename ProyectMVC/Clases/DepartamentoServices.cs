using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using ProyectMVC.Interfaces;
using ProyectMVC.Models;
using System.Data;

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

        public FileResult GenerarListaDepartamento(string nombreArchivo, IEnumerable<Departamento> departamentos)
        {
            DataTable dataTable = new DataTable("archivoDepartamentos");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("id"),
                new DataColumn("Departamento1")
            });

            foreach (var departamento in departamentos)
            {
                dataTable.Rows.Add(
                    departamento.IdDepartamento,
                    departamento.Departamento1
                );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin); // Asegurarse de que el puntero de lectura está al inicio del flujo
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = nombreArchivo
                    };
                }
            }
        }

    }
}
