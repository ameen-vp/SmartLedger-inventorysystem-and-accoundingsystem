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

        public CostomerService(ICostmerRepo costmerRepo , IMapper mapper)
        {
            _costmerRepo = costmerRepo;
            _mapper = mapper;
        }
        public async Task<Apiresponse<string>> AddCostomer(CostomerDto costomerDto)
        {
          
            var model = _mapper.Map<Costomer>(costomerDto);

            var res = await _costmerRepo.AddCostomers(model);
            if (res)
            {
                return new Apiresponse<string>
                {
                    Success = true,
                    Message = "Costomer Added Successfully"
              
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
            if(res == null)
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
    }
}
