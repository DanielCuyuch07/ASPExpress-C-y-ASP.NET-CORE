using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectMVC.Interfaces;
using ProyectMVC.Models;
using System.Data;

namespace ProyectMVC.Clases
{
    public class ClientesServices : IClientes
    {
        private readonly DbbancolombiaContext _dbContext;
        public ClientesServices(DbbancolombiaContext context)
        {
            _dbContext = context;
        }

        public List<Cliente> GetClientes()
        {
            try { 
                return _dbContext.Clientes.ToList();
            }catch(Exception ex) { 
                return new List<Cliente>(); 
            }
        }

        public bool DeleteClientes(int idCliente, out string message)
        {
            try
            {
                var idClienteDelete = _dbContext.Clientes.Find(idCliente);
                Console.WriteLine(idClienteDelete);
                if (idClienteDelete != null)
                {
                    _dbContext.Clientes.Remove(idClienteDelete);
                    _dbContext.SaveChanges();
                    Console.WriteLine("Cliente eliminada exitosamente");
                    message = "Cliente eliminada exitosamente";
                    return true;
                }
                else
                {
                    Console.WriteLine("La persona no fue encontrada");
                    message = "La persona no fue encontrada";
                    return false;
                }

            }catch(Exception ex)
            {
                Console.WriteLine("Error al eliminar la persona: " + ex.Message);
                message = "Error interno del servidor al eliminar la persona: " + ex.Message;
                return false;
            }
        }

        public FileResult GenerarListaClientes(string nombreArchivoCliente, IEnumerable<Cliente> archivoClienteList)
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
                    clientes.IdInversion
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
                        FileDownloadName = nombreArchivoCliente
                    };
                }
            }
        }


    }
}
