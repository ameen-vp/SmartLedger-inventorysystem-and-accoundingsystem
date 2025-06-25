using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using Domain.Enum;
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
                    Type = addaccountDto.Type,
                    Balance = addaccountDto.Balance
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
        public async Task<Apiresponse<List<Accounts>>> Get()
        {
            try
            {
                var get = await _accountRepo.Get();
                if(get == null || get.Count() == 0)
                {
                    return new Apiresponse<List<Accounts>>
                    {
                        Data = null,
                        Message = "Accounts not Found",
                        Statuscode = 404,
                        Success = false
                    };
                }
                return new Apiresponse<List<Accounts>>
                {
                    Data = get,
                    Message = "Accounts Fetched SucsessFully",
                    Success = true,
                    Statuscode = 200
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public  Apiresponse<IEnumerable<Accounts>> GetByTypes(AccountType type)
        {
            try
            {
                var accounts =  _accountRepo.GetTypes(type);
                if(accounts == null || accounts.Count() == 0)
                {
                    return new Apiresponse<IEnumerable<Accounts>>
                    {
                        Statuscode = 404,
                        Message = "Transactions Not found"
                    };
                }
                return new Apiresponse<IEnumerable<Accounts>>
                {
                    Data = accounts,
                    Message = "AccountHeads Fetched Sucessfully",
                    Statuscode = 200,
                    Success = true
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Apiresponse<string>> Delete(int id)
        {
            try
            {
                var status = await _accountRepo.Delete(id);

                if(!status)
                {
                    return new Apiresponse<string>
                    {
                        Message = "Account Not Found",
                        Statuscode = 404,
                        Success = false,
                        Data = null
                    };
                }
                return new Apiresponse<string>
                {
                    Message = "Account Deleted Successfully",
                    Statuscode = 200,
                    Success = true,
                    Data = null
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    } 
    }

