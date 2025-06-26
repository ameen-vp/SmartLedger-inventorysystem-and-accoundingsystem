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
   public interface ILedgerRepo
    {
        Task AddEntry(LedgerEntry ledgerEntry);
        Task<Accounts> GetdebitId(int id);
        Task<Accounts> GetcreditId(int id);

        Task Update(Accounts accounts);

        Task<bool> Delete(int id);
    }
    public interface ILedgerSErvice
    {
        Task<Apiresponse<AddLedgerDto>> AddEntry(AddLedgerDto addLedgerDto);

        Task<Apiresponse<string>> Delete(int id);
    }
}
