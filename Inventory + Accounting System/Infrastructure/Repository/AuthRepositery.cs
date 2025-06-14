using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using AutoMapper;
using Domain.Models;
using Infrastructure.Contexts;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
   public class AuthRepositery : IAuthRepo
    {
        private readonly AppDbContext _context;
        public AuthRepositery(AppDbContext context)
        {
            _context = context;
        }
      public async Task Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        
        }
      public async  Task<bool> UserExits(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
        public async Task<User> Getemail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

       public async Task<bool> Deleteuser(string Name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == Name);
            if (user == null)
            {
                return false;
            }
             _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<User>> Getusers()
        {
            return await _context.Users.ToListAsync();
          
        }
        public async Task Updateuser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
