using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using ProyectMVC.Interfaces;
using ProyectMVC.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using System.ComponentModel;
using System.Data;

namespace ProyectMVC.Clases
{


    public class PersonServices : IPersona
    {
        private readonly DbbancolombiaContext _dbContext;

        public PersonServices(DbbancolombiaContext context)
        {
            _dbContext = context;
        }


        public List<Persona> GetPerson()
        {
            try
            {
                return _dbContext.Personas.ToList();
            }catch (Exception ex) {
                return new List<Persona>();
            }
        }

        public bool DeletePersona(int idPersona, out string message)
        {
            try
            {
                Console.WriteLine("Iniciando eliminación de persona con ID: " + idPersona);

                var deletePersona = _dbContext.Personas.Find(idPersona);
                if (deletePersona != null)
                {
                    Console.WriteLine("Persona encontrada. Eliminando...");

                    _dbContext.Personas.Remove(deletePersona);
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

        public FileResult GererarListadoPersonas(string nombreArchivo, IEnumerable<Persona> personas)
        {
            DataTable dataTable = new DataTable("Personas");
            dataTable.Columns.AddRange(new DataColumn[]
{
                new DataColumn("id"),
                new DataColumn("FullName"),
                new DataColumn("Phone"),
                new DataColumn("AddressPerson"),
                new DataColumn("Email"),
                new DataColumn("Dpi"),
                new DataColumn("AccountNumber"),
            });

            foreach (var persona in personas)
            {
                dataTable.Rows.Add(
                    persona.IdPersona,
                    persona.FullName,
                    persona.Phone,
                    persona.AddressPerson,
                    persona.Email,
                    persona.Dpi,
                    persona.AccountNumber
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
