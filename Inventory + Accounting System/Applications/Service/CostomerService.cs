using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Service
{
   public class CostomerService : ICostomerService
    {
        private readonly ICostmerRepo _costmerRepo;
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountrepo;

        public CostomerService(ICostmerRepo costmerRepo , IMapper mapper, IAccountRepo accountrepo)
        {
            _costmerRepo = costmerRepo;
            _mapper = mapper;
            _accountrepo = accountrepo;
        }
        public async Task<Apiresponse<string>> AddCostomer(CostomerDto costomerDto)
        {
          
            var model = _mapper.Map<Costomer>(costomerDto);

            var res = await _costmerRepo.AddCostomers(model);
            if (res)
            {
                var account = new Accounts
                {
                    Name = model.Name,
                    Type = Domain.Enum.AccountType.ASSET,
                    Balance = 0
                };

                var createdAccount = await _accountrepo.Addacoount(account);

                if (createdAccount == null || createdAccount.Id == 0)
                {
                    return new Apiresponse<string>
                    {
                        Success = false,
                        Statuscode = 500,
                        Message = "Account creation failed"
                    };
                }

                model.AccountId = createdAccount.Id;
                 var update =  await _costmerRepo.Update(model);
                if (!update)
                {
                    return new Apiresponse<string>
                    {
                        Success = false,
                        Statuscode = 500,
                        Message = "Failed to update customer with account link"
                    };
                }
                return new Apiresponse<string>
                {
                    Success = true,
                    Message = "Costomer Added Successfully with Account"
                };
            }
            else
            {
                return new Apiresponse<string>
                {
                    Success = false,
                    Statuscode = 400,
                    Message = "Costomer Not Added",
                };
            }
        }
        public async Task<Apiresponse<List<Costomer>>> Get()
        {
            var res = await _costmerRepo.GetCostomers();
            if(res == null || res.Count ==0)
            {
                return new Apiresponse<List<Costomer>>
                {
                    Data = null,
                    Message = "Costomers not found",
                    Statuscode = 400,
                    Success = false
                };
            }
            return new Apiresponse<List<Costomer>>
            {
                Data = res,
                Message = "Costomers fetched SucessFully",
                Statuscode = 200,
                Success = true
            };
        }
        public async Task<Apiresponse<string>> Delete(int id)
        {
            var res = await _costmerRepo.DeleteCostomers(id);
            if (res)
            {
                return new Apiresponse<string>
                {
                    Success = true,
                    Message = "Costomer Deleted Successfully"
                };
            }
            else
            {
                return new Apiresponse<string>
                {
                    Success = false,
                    Statuscode = 400,
                    Message = "Costomer Not Deleted",
                };
            }
        }
    }
}
