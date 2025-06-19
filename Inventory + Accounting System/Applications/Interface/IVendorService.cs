using Applications.ApiResponse;
using Applications.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IVendorService
    {
        Task<Apiresponse<Vendor>> Addvendors(VendorAdddto vendorAdddto);

        Task<Apiresponse<List<vendeorviewDto>>> Get();
        Task<Apiresponse<Vendor>> GetventorById(int id);

        Task<Apiresponse<string>> Delete(int id);
    }
}
