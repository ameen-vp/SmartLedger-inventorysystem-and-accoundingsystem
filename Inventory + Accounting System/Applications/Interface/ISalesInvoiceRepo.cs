using Applications.ApiResponse;
using Applications.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface ISalesInvoiceRepo
    {
        Task AddSalesInvoice(SalesInvoice salesInvoice);

        Task<Costomer> GetCostomerId(int id);

        Task<int> GetLastInvoiceNumber();

        Task<List<SalesInvoice>> GetSalesInvoices();

        Task Updatestatus(SalesInvoice salesInvoice);

        Task <SalesInvoice> GetInvoiceId(int id);

        Task<SalesInvoice?> GetInvoiceWithDetailsAsync(int id);



    }

    public interface ISalesInvoiceService
    {
        Task<Apiresponse<ADDSalesInvoice>> AddInvoice(ADDSalesInvoice salesInvoice);

        Task<Apiresponse<List<SalesInvoicesDto>>>Get();

        Task<Apiresponse<string>> UpdateSataus(UpdateStatusDto dto);

        Task<Apiresponse<SalesInvoice>> GetInvoiceById(int id);

        Task<byte[]?> GenerateInvoicePdfAsync(int id);
    }
}
