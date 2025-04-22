using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.Json;

namespace Proyecto_Final.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Reportes()
        {
            return View();
        }



        [HttpPost]
    public IActionResult GenerarReporte()
     {
       try
         {
                QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

                var rutaJson = @"C:\Users\El Doritos PC\Documents\ReportesPAW\cateteres.json";

        if (!System.IO.File.Exists(rutaJson))
            return NotFound("No se encontró el archivo de catéteres.");

        var jsonData = System.IO.File.ReadAllText(rutaJson);
        var cateteres = JsonSerializer.Deserialize<List<CateterModel>>(jsonData);

        if (cateteres == null || !cateteres.Any())
            return BadRequest("No hay datos para generar el reporte.");

        var fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var nombreArchivo = $"ReporteCateteres_{fechaHora}.pdf";

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Header()
                    .Text("Reporte de Catéteres")
                    .FontSize(20)
                    .SemiBold()
                    .AlignCenter();

                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Nombre");
                            header.Cell().Element(CellStyle).Text("Tipo");
                            header.Cell().Element(CellStyle).Text("Tamaño");
                            header.Cell().Element(CellStyle).Text("Material");
                            header.Cell().Element(CellStyle).Text("Fabricante");

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5);
                            }
                        });

                        foreach (var c in cateteres)
                        {
                            table.Cell().Text(c?.Nombre ?? "N/A");
                            table.Cell().Text(c?.Tipo ?? "N/A");
                            table.Cell().Text(c?.Tamaño ?? "N/A");
                            table.Cell().Text(c?.Material ?? "N/A");
                            table.Cell().Text(c?.Fabricante ?? "N/A");
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Generado: ");
                        x.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}");
                    });
            });
        });

        var pdfStream = new MemoryStream();
        document.GeneratePdf(pdfStream);
        pdfStream.Position = 0;

        return File(pdfStream, "application/pdf", nombreArchivo);
    }
    catch (Exception ex)
    {
        return BadRequest($"Ocurrió un error: {ex.Message}");
    }
}

    }
}
