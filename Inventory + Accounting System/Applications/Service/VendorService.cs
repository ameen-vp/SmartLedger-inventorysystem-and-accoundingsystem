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
   public class VendorService : IVendorService
    {
        private readonly IVentorrepo _vendorRepo;
        private readonly IMapper _mapper;
        public VendorService(IVentorrepo vendorRepo ,IMapper mapper)
        {
            _vendorRepo = vendorRepo;
            _mapper = mapper;
        }
        public async Task<Apiresponse<Vendor>> Addvendors(VendorAdddto vendorAdddto)
        {
            try
            {   
                var dto = _mapper.Map<Vendor>(vendorAdddto);
                var exit = await _vendorRepo.Vendorexits(dto.VendorId);
                if (exit)
                {
                    return new Apiresponse<Vendor>
                    {
                        Data = null,
                        Message = "Vendor already exists.",
                        Statuscode = 400,
                        Success = false
                    };
                }
                var ven =  _vendorRepo.Addvendor(dto);
                return new Apiresponse<Vendor>
                {
                    Data = dto,
                    Message = "Vendor added successfully.",
                    Statuscode = 200,
                    Success = true
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Apiresponse<List<Vendor>>> Get()
        {
            var res = await _vendorRepo.Getventors();
            if(res == null)
            {
                return new Apiresponse<List<Vendor>>
                {
                    Data = null,
                    Message = "Vendors not found",
                    Statuscode = 404,
                    Success = false

                };
            }
            return new Apiresponse<List<Vendor>>
            {
                Data = res,
                Message = "Vendors fetched sucessessfully",
                Success = true,
                Statuscode = 200
            };
        }
        public async Task<Apiresponse<List<Vendor>>> GetventorById(int id)
        {
            var check = await _vendorRepo.Findid(id);
            if(check == null)
            {
                return new Apiresponse<List<Vendor>>
                {
                    Message = "vemdor not dound",
                    Data = null,
                    Success = false,
                    Statuscode = 400
                };
            }
            var get = await _vendorRepo.GetVendorsById(id);
            return new Apiresponse<List<Vendor>>
            {
                Data = get,
                Message = "Venter fetched sucessessfully",
                Success = true,
                Statuscode = 200
            };
        }
    }
}
