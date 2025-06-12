using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IVentorrepo
    {
        Task  Addvendor(Vendor vendor);

        Task<bool> Vendorexits(int id);

        Task<List<Vendor>> Getventors();

      

        Task<Vendor> Findid(int id);
    }
}
