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
           await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

        }
       public async Task<Product> Exits(string name)
        {
            return await _appDbContext.Products.FirstOrDefaultAsync(x => x.ProductName == name);
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
    }
}
