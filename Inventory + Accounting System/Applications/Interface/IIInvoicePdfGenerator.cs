using Domain.Models;
using QuestPDF.Infrastructure;

namespace Applications.Interface
{
    public interface IIInvoicePdfGenerator
    {
        byte[] GenerateInvoicePdf(SalesInvoice invoice);

 
    }
}
