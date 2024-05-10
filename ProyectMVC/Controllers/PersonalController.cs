
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectMVC.Clases;
using ProyectMVC.Interfaces;
using ProyectMVC.Models;
using ClosedXML.Excel;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;


namespace ProyectMVC.Controllers
{
    public class PersonalController : Controller
    {
        private readonly IPersona _persona;
        private readonly DbbancolombiaContext _dbContext;
        private readonly IWebHostEnvironment _host;

        public PersonalController(IPersona persona, DbbancolombiaContext dbContext, IWebHostEnvironment host)
        {
            _persona = persona;
            _dbContext = dbContext;
            _host = host;
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
            return _persona.GererarListadoPersonas(nombreArchivo, personas);
        }

        [HttpPost]
        public IActionResult DescargarPDF()
        {
            var personas = _dbContext.Personas.ToList();            // Crear el documento PDF

            var document = Document.Create(contenedor =>
            {
                contenedor.Page(page =>
                {
                    page.Margin(20);

                    /* Header */
                    page.Header().ShowOnce().Row(row =>
                    {
                        var rutaImage = Path.Combine(_host.WebRootPath, "img/img-logo1.png");
                        byte[] imageData = System.IO.File.ReadAllBytes(rutaImage);
                        row.ConstantItem(170).Image(imageData);



                        row.RelativeItem().Column(col =>
                        {

                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Padding(50).Text("LISTADO DEL PERSONAL ").Bold().FontSize(14);
                        });
                    });

                    /* Contenido Principal */
                    page.Content().PaddingVertical(0).Column(col1 =>
                    {

                        col1.Item().LineHorizontal(0.5f);

                        /*Tabla principal */
                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1); // Ancho relativo de la primera columna
                                columns.RelativeColumn(3); // Ancho relativo de la segunda columna
                                columns.RelativeColumn(3); // Ancho relativo de la tercera columna

                                columns.RelativeColumn(3); // Ancho relativo de la cuarta columna
                                columns.RelativeColumn(3); // Ancho relativo de la séptima columna
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272").Padding(2).Text("Id").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Full Name").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Phone").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Adrress").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(3).Text("Email").FontColor("#fff");
                            });

                            // Agregar filas de personas a la tabla
                            personas.ForEach(persona =>
                            {
                                tabla.Cell().Padding(5).Text(persona.IdPersona.ToString()).FontSize(10);
                                tabla.Cell().Padding(5).Text(persona.FullName).FontSize(10);
                                tabla.Cell().Padding(5).Text(persona.Phone).FontSize(10);
                                tabla.Cell().Padding(5).Text(persona.AddressPerson).FontSize(10);
                                tabla.Cell().Padding(5).Text(persona.Email).FontSize(10);
                                //tabla.Cell().Text(persona.Dpi).FontSize(10);
                            });

                        });

                        col1.Item().PaddingBottom(100);


                        if (1 == 1)
                            col1.Item().Background(QuestPDF.Helpers.Colors.Grey.Lighten3).Padding(10)
                            .Column(column =>
                            {
                                column.Item().Text("Comentarios").FontSize(14);
                                column.Item().Text(Placeholders.LoremIpsum());
                                column.Spacing(5);
                            });

                        page.Footer()
        .AlignRight()
        .Text(txt =>
        {
            txt.Span("Pagina ").FontSize(10);
            txt.CurrentPageNumber().FontSize(10);
            txt.Span(" de ").FontSize(10);
            txt.TotalPages().FontSize(10);
        });

                        col1.Spacing(10);
                    });
                });
            });

            // Generar el documento PDF en un arreglo de bytes
            var pdfBytes = document.GeneratePdf();


            // Devolver el archivo PDF como una descarga
            return new FileContentResult(pdfBytes, "application/pdf")
            {
                FileDownloadName = "listado_personal.pdf"
            };
        }
    }
}
