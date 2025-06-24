using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Domain.Models;
using Applications.Interface;
using System.Linq;// Adjust if your SalesInvoice is in another namespace

namespace Application.Services
{
    public class InvoicePdfGenerator : IIInvoicePdfGenerator
    {
        public byte[] GenerateInvoicePdf(SalesInvoice invoice)
        {
            // Set license type for QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            var document = new SalesInvoiceDocument(invoice);

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }
    }


    public class SalesInvoiceDocument : IDocument
    {
        private readonly SalesInvoice _invoice;

        public SalesInvoiceDocument(SalesInvoice invoice)
        {
            _invoice = invoice;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
    .PaddingBottom(10)
    .AlignCenter()
    .Text("Sales Invoice")
    .FontSize(24)
    .Bold();


                page.Content().Column(column =>
                {
                    column.Spacing(5);

                    column.Item().Text($"Invoice ID: {_invoice.Id}");

                    column.Item().Text($"Date: {_invoice.InvoiceDate.ToShortDateString()}");

                    column.Item().PaddingTop(15).Text("Items").Bold().FontSize(14);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Product").Bold();
                            header.Cell().Text("Qty").Bold().AlignCenter();
                            header.Cell().Text("Unit Price").Bold().AlignRight();
                            header.Cell().Text("Discount").Bold().AlignRight();
                            header.Cell().Text("Total").Bold().AlignRight();
                        });
                        foreach (var item in _invoice.SalesItems)
                        {
                            if (item.Product == null)
                            {

                                throw new NullReferenceException($"SalesItem with ProductId {item.ProductId} has null Product reference.");
                            }
                            if (string.IsNullOrEmpty(item.Product.ProductName))
                            {
                                throw new NullReferenceException($"SalesItem with ProductId {item.ProductId} has Product with null or empty Name.");
                            }
                        }
                        foreach (var item in _invoice.SalesItems ?? Enumerable.Empty<SalesItems>())
                        {
                            var productName = item.Product?.ProductName;
                            var unitPrice = item.UNITPrice;
                            var discount = item.Discount;
                            var totalPrice = item.TotalPrice;

                            table.Cell().Text(productName ?? "N/A").AlignLeft();
                            table.Cell().Text(item.Quantity.ToString()).AlignCenter();
                            table.Cell().Text($"{unitPrice:C}").AlignRight();
                            table.Cell().Text($"{discount:C}").AlignRight();
                            table.Cell().Text($"{totalPrice:C}").AlignRight();
                        }



                        decimal subtotal = _invoice.SalesItems.Sum(x => x.Quantity * x.UNITPrice);
                        decimal totalDiscount = _invoice.SalesItems.Sum(x => x.Discount);
                        decimal totalGst = _invoice.SalesItems.Sum(x => x.Gst);
                        decimal finalTotal = _invoice.TotalAmount;

                        column.Item().PaddingTop(15).AlignRight().Text($"Subtotal: {subtotal:C}");
                        column.Item().AlignRight().Text($"Discount: -{totalDiscount:C}");
                        column.Item().AlignRight().Text($"GST: {totalGst:C}");
                        column.Item().AlignRight().Text($"Total Amount: {finalTotal:C}").Bold().FontSize(14);

                    });

                    page.Footer()
                      .AlignCenter()
                      .Container()
          .PaddingTop(10)
          .Text("Generated using QuestPDF")
          .FontSize(10)
          .Italic();

                });
            });
        }
    }
}
