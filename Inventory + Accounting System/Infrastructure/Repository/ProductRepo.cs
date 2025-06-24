using Applications.ApiResponse;
using Applications.Dto;
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
   public class ProductRepo : IProductRepo
    {  
        private readonly AppDbContext _appDbContext;

      public ProductRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
      public async Task Addproduct(Product product)
        {
            try
            {
                await _appDbContext.Products.AddAsync(product);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error: " + ex.InnerException?.Message);
                throw;
            }

        }
       public async Task<bool> Exits(string name)
        {
            return await _appDbContext.Products.AnyAsync(x => x.SKU == name);
        }
        public async Task UpdateProduct(Product product)
        {
             _appDbContext.Products.Update(product);
            await _appDbContext.SaveChangesAsync();

        }
        public async Task<bool> Categorycheck(int id)
        {
           return  await _appDbContext.categories.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _appDbContext.Products.Include(x => x.category).ToListAsync();
        }
        public async Task<Product> GetproductbyId(int id)
        {
            return await _appDbContext.Products.Include(x => x.category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> productexit(int id)
        {
            return await _appDbContext.Products.AnyAsync(x => x.Id == id);
        }
       
        public async Task<bool> Idcheck(int id)
        {
            return await _appDbContext.Products.AnyAsync(x => x.Id == id);
        }
        public async Task<bool> DeleteProducts(int id)
        {
            var del = await _appDbContext.Products.FindAsync(id);
            if (del == null)
            {
                return false;
            }
            _appDbContext.Products.Remove(del);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
      public async  Task<List<Product>> GetProductviewdtos()
        {
            var pro = await _appDbContext.Products.Include(x => x.StockTransactions).ToListAsync();
            return pro;
        }
      public async Task<decimal> FetchPurchasePrize(int ProductId)
        {
            var prize = await _appDbContext.PurchaseItems.Where(x => x.ProductId == ProductId)
                                                         .OrderByDescending(x => x.PurchaseInvoice.Date)
                                                         .Select(x => x.UnitPrice)
                                                         .FirstOrDefaultAsync();
            return prize;
        }
    }
}
