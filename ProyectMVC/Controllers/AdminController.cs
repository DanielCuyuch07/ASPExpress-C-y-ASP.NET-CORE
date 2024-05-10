using ClosedXML.Excel;
using DocumentFormat.OpenXml.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectMVC.Clases;
using ProyectMVC.Interfaces;
using ProyectMVC.Models;
using System.Collections;
using System.Data;


using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;

namespace ProyectMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbbancolombiaContext _dbbancolombia;
        private readonly IDepartamentos _departamentos;
        private readonly IInversiones _inversiones;
        private readonly IClientes _clientes;
        private readonly IWebHostEnvironment _host;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public AdminController(DbbancolombiaContext context, IDepartamentos departamentos, IInversiones inversiones, IClientes clientes, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment host)
        {
            _dbbancolombia = context;
            _departamentos = departamentos;
            _inversiones = inversiones;
            _clientes = clientes;
            _httpContextAccessor = httpContextAccessor;
            _host = host;   
        }

        /************************************  DEPARTAMENTOS  *****************************************************/

        public IActionResult Departamentos()
        {
            var departamento = _departamentos.GetDepartamentos();
            return View(departamento);  
        }


        [HttpPost]
        [Route("DeleteDepartamentos")]
        public IActionResult DeleteDepartamentos(int idDepartamento)
        {
            try
            {
                var departamentoService = new DepartamentoServices(_dbbancolombia); // Cambié el nombre del servicio a "departamentoService"
                if (departamentoService.DeleteDepartamentos(idDepartamento, out var message))
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
                return Json(new { success = false, message = "Error interno del servidor al eliminar el departamento: " + ex.Message });
            }
        }


        [HttpGet]
        public async Task<FileResult> ExportarDepartamentosExcel()
        {
            var archivoDepartamentos = await _dbbancolombia.Departamentos.ToListAsync();
            var nombreArchivoDepartamento = $"Informacion_Departamento.xlsx";
            return _departamentos.GenerarListaDepartamento(nombreArchivoDepartamento, archivoDepartamentos);
        }



        /************************************  CLIENTES  *****************************************************/

        public IActionResult Clientes()
        {
            var infCliente = _clientes.GetClientes();
            return View(infCliente);
        }

        [HttpPost]
        [Route("DeleteClientes")]
        public IActionResult DeleteClientes(int idCliente)
        {
            try
            {
                var clientesServices = new ClientesServices(_dbbancolombia);
                if (clientesServices.DeleteClientes(idCliente, out var message))
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
                return Json(new { success = false, message = "Error interno del servidor al eliminar el departamento: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<FileResult> ExportacionClientesExcel()
        {
            var archivoClienteList = await _dbbancolombia.Clientes.ToListAsync();
            var nombreArchivoCliente = $"Informacion_Clientes.xlsx";
            return _clientes.GenerarListaClientes(nombreArchivoCliente, archivoClienteList);
        }

        //private FileResult GenerarExcelClientes(string nombreArchivoCliente, IEnumerable<Cliente> archivoClienteList)
        //{
        //    DataTable dataTable = new DataTable("archivoClienteList");
        //    dataTable.Columns.AddRange(new DataColumn[]
        //    {
        //        new DataColumn("IdCliente"),
        //        new DataColumn("NombreCliente"),
        //        new DataColumn("NumeroDeCuenta"),
        //        new DataColumn("CorreoElectronico"),
        //        new DataColumn("Saldo"),
        //        new DataColumn("IdInversionNavigation"),
        //    });

        //    foreach (var clientes in archivoClienteList)
        //    {
        //        dataTable.Rows.Add(
        //            clientes.IdCliente,
        //            clientes.NombreCliente,
        //            clientes.NumeroDeCuenta,
        //            clientes.CorreoElectronico, 
        //            clientes.Saldo,
        //            clientes.IdInversion, 
        //            clientes.IdInversionNavigation

        //        );
        //    }

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dataTable);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //            nombreArchivoCliente);
        //        }
        //    }

        //}


        /*********************************** Inversiones **************************************/
        public IActionResult Inversiones()
        {
           var infInversiones = _inversiones.GetInversiones();
            return View(infInversiones);
        }

        [HttpPost]
        [Route("DeleteInversiones")]
        public IActionResult DeleteInversiones(int IdInversion)
        {
            try
            {
                var inversionesServices = new InversionesServives(_dbbancolombia);
                if (inversionesServices.DeleteInversiones(IdInversion, out var message))
                {
                    return Json(new { success = true, message });

                }
                else
                {
                    return Json(new { success = false, message });

                }
            } catch (Exception ex)
            {
                return Json(new { success = false, message = "Error interno del servidor al eliminar el departamento: " + ex.Message });

            }
        }

        [HttpGet]
        public async Task<FileResult> GenerarExcelInversiones()
        {
            var archivoInversionesList = await _dbbancolombia.Inversiones.ToListAsync();
            var nombreArchivoInversiones = $"Informacion_Inversiones.xlsx";
            return _inversiones.GenerarListadoDeInversiones(nombreArchivoInversiones, archivoInversionesList);
        }

        //[HttpPost]

        //public IActionResult DescargarPDFInversiones()
        //{
        //    try
        //    {
        //        return _inversiones.DescargarPDF();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar cualquier excepción y devolver un mensaje de error
        //        return Json(new { success = false, message = "Error interno del servidor al generar el PDF: " + ex.Message });
        //    }
        //}

        public IActionResult DescargarPDF()
        {
            var personas = _dbbancolombia.Personas.ToList();            // Crear el documento PDF

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



        //public FileResult GenerarListadoDeInversiones(string nombreArchivoInversiones, IEnumerable<Inversione> archivoInversionesList)
        //{
        //    DataTable dataColumn = new DataTable("archivoInversionesList");
        //    dataColumn.Columns.AddRange(new DataColumn[]
        //    {
        //        new DataColumn("Id Inversiones"),
        //        new DataColumn("Inversiones"),
        //    });

        //    foreach (var inversiones in archivoInversionesList)
        //    {
        //        dataColumn.Rows.Add(
        //            inversiones.IdInversion,
        //            inversiones.NombreInversion
        //        );
        //    }

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dataColumn);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //            nombreArchivoInversiones);
        //        }
        //    }


        //}

    }
}
