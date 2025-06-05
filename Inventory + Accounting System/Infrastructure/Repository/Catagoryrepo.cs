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
  public class Catagoryrepo : ICatRepo
    {  
        private readonly AppDbContext _Context;

        public Catagoryrepo(AppDbContext context)
        {
            _Context = context;
        }
        public async Task AddCatagory(Category category)
        {
            await _Context.categories.AddAsync(category);
            await _Context.SaveChangesAsync();
        }
        public async Task<bool> Catagoryexit(string name)
        {
            return await _Context.categories.AnyAsync(x => x.CategoryName == name);
        }
        public async Task<List<Category>> GetAllCategorys()
        {
            return await _Context.categories.ToListAsync();
        }
    }
}
