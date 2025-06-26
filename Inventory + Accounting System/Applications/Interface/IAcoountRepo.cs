using Applications.ApiResponse;
using Applications.Dto;
using Domain.Enum;
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
        Task<Accounts> Addacoount(Accounts accounts);
        Task<List< Accounts>> Get();

        IEnumerable<Accounts> GetTypes(AccountType type);

        Task<bool> Delete(int id);

        Task<int> GetcustomerId(int id);

        Task<int> GetAccountsId( );

    }
    public interface IAccountService
    {
        Task<Apiresponse<string>> AddAcount(AddaccountDto addaccountDto);

        Task<Apiresponse<List<Accounts>>> Get();

        Apiresponse<IEnumerable<Accounts>> GetByTypes(AccountType type);

        Task<Apiresponse<string>> Delete(int id);
    }
    
}
