using Applications.Interface;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public  class PurchaseInvoiceRepo : IpurchaseInvoiceRepo
    {
        private readonly AppDbContext _appDbContext;
        public PurchaseInvoiceRepo(AppDbContext appDbContext)
            {
            _appDbContext = appDbContext;
            }
       public async Task AddInvoice(PurchaseInvoice purchaseInvoice)
        {
            await _appDbContext.PurchaseInvoices.AddAsync(purchaseInvoice);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Vendor> GetvendorsId(int vendorId)
        {
            return await _appDbContext.Vendors.FirstOrDefaultAsync(x => x.VendorId == vendorId);
        }
        public async Task<List<PurchaseInvoice>> GetInvoice()
        {
            return await _appDbContext.PurchaseInvoices.Include(x => x.purchaseItems).ToListAsync();
        }
        public async Task<bool> Deleteinvoice(int id)
        {
            var del = await _appDbContext.PurchaseInvoices.Include(x => x.purchaseItems).
                FirstOrDefaultAsync(x => x.Id == id);
            if (del == null)
            {
                return false;
            }
             _appDbContext.PurchaseInvoices.Remove(del);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PurchaseInvoice> GetInvoiceById(int id)
        {
         return
            await _appDbContext.PurchaseInvoices.Include(x => x.purchaseItems).
                   FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
