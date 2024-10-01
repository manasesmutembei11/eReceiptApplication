using Carter;
using eReceiptApplication.Contracts;
using eReceiptApplication.Services;
using Razor.Templating.Core;
using System.Net.Mime;

namespace eReceiptApplication.Modules.InvoiceModule;

public class Endpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/invoice-report", async (InvoiceFactory invoiceFactory) =>
        {
            Invoice invoice = invoiceFactory.Create();

            var html = await RazorTemplateEngine.RenderAsync("Views/InvoiceReport.cshtml", invoice);

            var renderer = new ChromePdfRenderer();

            using var pdfDocument = renderer.RenderHtmlAsPdf(html);

            return Results.Text(html, "text/html");

        });
        
        app.MapGet("api/download", async (InvoiceFactory invoiceFactory) =>
        {
            Invoice invoice = invoiceFactory.Create();

            var html = await RazorTemplateEngine.RenderAsync("Views/InvoiceReport.cshtml", invoice);

            var renderer = new ChromePdfRenderer();

            using var pdfDocument = renderer.RenderHtmlAsPdf(html);
            var contentDisposition = $"attachment; filename=invoice-{invoice.Number}.pdf";

            return Results.File(
                pdfDocument.BinaryData,
                "application/pdf",
                contentDisposition);

        });
    }

}
