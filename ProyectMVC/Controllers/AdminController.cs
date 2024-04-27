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

namespace ProyectMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbbancolombiaContext _dbbancolombia;
        private readonly IDepartamentos _departamentos;
        private readonly IInversiones _inversiones;
        private readonly IClientes _clientes;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AdminController(DbbancolombiaContext context, IDepartamentos departamentos, IInversiones inversiones, IClientes clientes, IHttpContextAccessor httpContextAccessor)
        {
            _dbbancolombia = context;
            _departamentos = departamentos;
            _inversiones = inversiones;
            _clientes = clientes;
            _httpContextAccessor = httpContextAccessor;

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
            return GenerarExcel(nombreArchivoDepartamento, archivoDepartamentos);
        }

        private FileResult GenerarExcel(string nombreArchivoDepartamento, IEnumerable<Departamento> archivoDepartamentos)
        {
            DataTable dataTable = new DataTable("archivoDepartamentos");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("id"),
                new DataColumn("Departamento1")
            });

            foreach (var departamento in archivoDepartamentos)
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    nombreArchivoDepartamento);
                }
            }

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
            return GenerarExcelClientes(nombreArchivoCliente, archivoClienteList);
        }

        private FileResult GenerarExcelClientes(string nombreArchivoCliente, IEnumerable<Cliente> archivoClienteList)
        {
            DataTable dataTable = new DataTable("archivoClienteList");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("IdCliente"),
                new DataColumn("NombreCliente"),
                new DataColumn("NumeroDeCuenta"),
                new DataColumn("CorreoElectronico"),
                new DataColumn("Saldo"),
                new DataColumn("IdInversionNavigation"),
            });

            foreach (var clientes in archivoClienteList)
            {
                dataTable.Rows.Add(
                    clientes.IdCliente,
                    clientes.NombreCliente,
                    clientes.NumeroDeCuenta,
                    clientes.CorreoElectronico, 
                    clientes.Saldo,
                    clientes.IdInversion, 
                    clientes.IdInversionNavigation

                );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    nombreArchivoCliente);
                }
            }

        }


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

        public async Task<FileResult> GenerarExcelInversiones()
        {
            var archivoInversionesList = await _dbbancolombia.Inversiones.ToListAsync();
            var nombreArchivoInversiones = $"Inversiones.xlsx";
            return GenerarListadoDeInversiones(nombreArchivoInversiones, archivoInversionesList);
        }

        public FileResult GenerarListadoDeInversiones(string nombreArchivoInversiones, IEnumerable<Inversione> archivoInversionesList)
        {
            DataTable dataColumn = new DataTable("archivoInversionesList");
            dataColumn.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id Inversiones"),
                new DataColumn("Inversiones"),
            });

            foreach (var inversiones in archivoInversionesList)
            {
                dataColumn.Rows.Add(
                    inversiones.IdInversion,
                    inversiones.NombreInversion
                );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataColumn);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    nombreArchivoInversiones);
                }
            }


        }

    }
}
