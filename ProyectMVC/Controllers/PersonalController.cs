using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectMVC.Clases;
using ProyectMVC.Interfaces;
using ProyectMVC.Models;
using System.Data;

namespace ProyectMVC.Controllers
{
    public class PersonalController : Controller
    {
        private readonly IPersona _persona;
        private readonly DbbancolombiaContext _dbContext;

        public PersonalController(IPersona persona, DbbancolombiaContext dbContext)
        {
            _persona = persona;
            _dbContext = dbContext; 
        }

        public IActionResult ViewPersona()
        {
            var personas = _persona.GetPerson();
            return View(personas); 
        }


        [HttpPost]
        [Route("DeletePersona")]
        public IActionResult DeletePersona(int idPersona)
        {
            try
            {
                var personaService = new PersonServices(_dbContext);
                if (personaService.DeletePersona(idPersona, out var message))
                {
                    return Json(new { success = true, message });
                }
                else
                {
                    return Json(new { success = false, message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error interno del servidor al eliminar la persona: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<FileResult> ExportarPersonasAExcel()
        {
            var personas = await _dbContext.Personas.ToListAsync();
            var nombreArchivo = $"Personas.xlsx";
            return GenerarExcel(nombreArchivo, personas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Persona> personas)
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

            foreach(var persona in personas)
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    nombreArchivo);
                }
            }

        }




    }
}
