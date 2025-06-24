using Applications.Dto;
using Applications.Interface;
using AutoMapper;
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
   public class SalesInvoiceREpo : ISalesInvoiceRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public SalesInvoiceREpo(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
      public async  Task AddSalesInvoice(SalesInvoice salesInvoice)
        {
            await _appDbContext.SalesInvoices.AddAsync(salesInvoice);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Costomer> GetCostomerId(int id)
        {
            return await _appDbContext.Costomer.FirstOrDefaultAsync(x => x.CostomerId == id);

        }
        public async Task<int> GetLastInvoiceNumber()
        {
            var lastInvoice = await _appDbContext.SalesInvoices
                .OrderByDescending(i => i.Id)
                .FirstOrDefaultAsync();

            if (lastInvoice == null || string.IsNullOrEmpty(lastInvoice.InvoiceNumber))
                return 0;

            var digits = new string(lastInvoice.InvoiceNumber.Where(char.IsDigit).ToArray());
            return int.TryParse(digits, out int number) ? number : 0;
        }
      public async Task<List<SalesInvoice>> GetSalesInvoices()
        {
            return await _appDbContext.SalesInvoices.Include(x => x.SalesItems).ToListAsync();
        }
        public async Task Updatestatus(SalesInvoice salesInvoice)
        {
             _appDbContext.SalesInvoices.Update(salesInvoice);
            await _appDbContext.SaveChangesAsync();
        }
       public async Task<SalesInvoice> GetInvoiceId(int id)
        {
            return await _appDbContext.SalesInvoices.Include(x => x.SalesItems).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<SalesInvoice?> GetInvoiceWithDetailsAsync(int id)
        {
            return await _appDbContext.SalesInvoices
                .Include(i => i.SalesItems)
                    .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
