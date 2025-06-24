using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using Domain.Models;

using Microsoft.EntityFrameworkCore;


namespace Applications.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task<Apiresponse<string>> AddAcount(AddaccountDto addaccountDto)
        {
            try
            {
                var account = new Accounts
                {
                    Name = addaccountDto.Name,
                    Type = addaccountDto.Type
                };
                await _accountRepo.Addacoount(account);
                return new Apiresponse<string>
                {
                    Message = "Account Added",
                    Statuscode = 200,
                    Success = true,
                    Data = null
                };
            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Error saving account: {message}", ex);
            }


        }

    } 
    }

