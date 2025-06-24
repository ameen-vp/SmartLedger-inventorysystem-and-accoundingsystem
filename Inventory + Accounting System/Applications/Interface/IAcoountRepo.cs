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
   public interface IAccountRepo
    {
        Task Addacoount(Accounts accounts);
    }
    public interface IAccountService
    {
        Task<Apiresponse<string>> AddAcount(AddaccountDto addaccountDto);
    }
}
