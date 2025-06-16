using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IpurchaseInvoiceRepo
    {
        Task AddInvoice(PurchaseInvoice purchaseInvoice);

        Task<Vendor> GetvendorsId(int vendorId);

        Task<List<PurchaseInvoice>> GetInvoice();

        Task<PurchaseInvoice> GetInvoiceById(int id);

        Task<bool> Deleteinvoice(int id);
    }
}
