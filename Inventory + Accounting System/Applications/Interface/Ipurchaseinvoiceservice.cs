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
   public interface Ipurchaseinvoiceservice
    {
        Task<Apiresponse<string>> AddInvoices(AddPurchaseinvoiceDto addPurchaseinvoiceDto);

        Task<Apiresponse<List<PurchaseinvoiceDto>>> GetInvoice();

        Task<Apiresponse<string>> Delete(int id);

        Task<Apiresponse<PurchaseinvoiceDto>> GetInvoiceById(int id);
    }
}
