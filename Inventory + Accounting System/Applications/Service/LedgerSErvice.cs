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
   public class LedgerSErvice : ILedgerSErvice
    {
        private readonly ILedgerRepo _ledgerRepo;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public LedgerSErvice(ILedgerRepo ledgerRepo,IMapper mapper)
        {
            _ledgerRepo = ledgerRepo;
            _mapper = mapper;
        }
        public async Task<Apiresponse<AddLedgerDto>> AddEntry(AddLedgerDto addLedgerDto)

        {
            try
            {
                var map = _mapper.Map<LedgerEntry>(addLedgerDto);
                if (map == null)
                {
                    return new Apiresponse<AddLedgerDto>
                    {
                        Data = null,
                        Message = "Mapping failed",
                        Success = false,
                        Statuscode = 400
                    };
                }
                var debitid = await _ledgerRepo.GetdebitId(addLedgerDto.DebitAccountId);
                var creditid = await _ledgerRepo.GetcreditId(addLedgerDto.CreditAccountId);
                if (debitid == null || creditid == null)
                {
                    return new Apiresponse<AddLedgerDto>
                    {
                        Message = "Invalid Accounts",
                        Statuscode = 400,
                        Success = false,
                        Data = null
                    };
                }

                if (debitid.Type == Domain.Enum.AccountType.ASSET || debitid.Type == Domain.Enum.AccountType.EXPENSE)
                    debitid.Balance += addLedgerDto.Amount;
                else debitid.Balance -= addLedgerDto.Amount;

                if (creditid.Type == Domain.Enum.AccountType.ASSET || creditid.Type == Domain.Enum.AccountType.EXPENSE)
                    creditid.Balance -= addLedgerDto.Amount;
                else creditid.Balance += addLedgerDto.Amount;

                map.EntryDate = DateTime.Now;
                await _ledgerRepo.AddEntry(map);

                await _ledgerRepo.Update(debitid);
                await _ledgerRepo.Update(creditid);

               
                return new Apiresponse<AddLedgerDto>
                {
                    Data = addLedgerDto,
                    Message = "Ledger entry added successfully",
                    Success = true,
                    Statuscode = 200
                };
            }
            catch (Exception ex)
            {
                return new Apiresponse<AddLedgerDto>
                {
                    Data = null,
                    Message = $"Error adding ledger entry: {ex.Message}",
                    Success = false,
                    Statuscode = 500
                };
            }
           
        }
        public async Task<Apiresponse<string>> Delete(int id)
        {
            try
            {
                var del = await _ledgerRepo.Delete(id);
                if (!del)
                {
                    return new Apiresponse<string>
                    {
                        Data = null,
                        Message = "Invalid Id",
                        Statuscode = 400,
                        Success = false
                    };
                }
                return new Apiresponse<string>
                {
                    Data = null,
                    Message = "Deleted Sucsessfully",
                    Success = true,
                    Statuscode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
