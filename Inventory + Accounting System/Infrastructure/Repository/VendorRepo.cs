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
   public class VendorRepo : IVentorrepo
    {
        private readonly AppDbContext _context;

        public VendorRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Addvendor(Vendor vendor)
        {
            await _context.Vendors.AddAsync(vendor);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Vendorexits(string name)
        {
            return await _context.Vendors.AnyAsync(x => x.VendorName == name);

        }
        public async Task<List<Vendor>> Getventors()
        {
            return await _context.Vendors.Include(x => x.PurchaseInvoices).ToListAsync();
        }
      
        public async Task<Vendor> Findid(int id)
        {
            return await _context.Vendors.FirstOrDefaultAsync(x => x.VendorId == id);
        }
        public async Task<bool> Delete(int id)
        {
            var del = await _context.Vendors.FirstOrDefaultAsync(x => x.VendorId == id);
            if(del != null)
            {
                 _context.Vendors.Remove(del);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
