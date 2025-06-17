using AutoMapper;
using Domain.Models;
using Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
   public class SalesInvoiceREpo
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
    }
}
