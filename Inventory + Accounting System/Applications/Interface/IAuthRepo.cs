using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IAuthRepo
    {
        Task Register(User user);
        Task<bool> UserExits(string email);

        Task<User> Getemail(string email);
        Task<bool> Deleteuser(string Name);

        Task<List<User>> Getusers();

        Task Updateuser(User user);
    }
}
