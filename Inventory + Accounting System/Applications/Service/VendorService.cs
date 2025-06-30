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
                var exit = await _vendorRepo.Vendorexits(dto.VendorName);
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
        public async Task<Apiresponse<List<vendeorviewDto>>>Get()
        {
            var res = await _vendorRepo.Getventors();
            if(res == null)
            {
                return new Apiresponse<List<vendeorviewDto>>
                {
                    Data = null,
                    Message = "Vendors not found",
                    Statuscode = 404,
                    Success = false

                };
            }
            var vendors = res.Select(x => new vendeorviewDto
            {
                VendorName = x.VendorName,
                place = x.place,
                Phone = x.Phone,
                purchaseinvoiceDtos = x.PurchaseInvoices.Select(y => new PurchaseinvoiceDto
                {
                    Id = y.Id,
                    InvoiceNumber = y.InvoiceNumber,
                    VentorsId = y.VentorsId,
                    TotalAmount = y.TotalAmount,
                    GST = y.GST,
                    GrantToTal = y.GrantToTal



                }).ToList()

            }).ToList();
            return new Apiresponse<List<vendeorviewDto>>
            {
                Data = vendors,
                Message = "Vendors fetched sucessessfully",
                Success = true,
                Statuscode = 200
            };
        }
        public async Task<Apiresponse<Vendor>> GetventorById(int id)
        {
            var check = await _vendorRepo.Findid(id);
            if(check == null)
            {
                return new Apiresponse<Vendor>
                {
                    Message = "vemdor not dound",
                    Data = null,
                    Success = false,
                    Statuscode = 400
                };
            }
       
            return new Apiresponse<Vendor>
            {
                Data = check,
                Message = "Venter fetched sucessessfully",
                Success = true,
                Statuscode = 200
            };
        }
        public async Task<Apiresponse<string>> Delete(int id)
        {
            var del = await _vendorRepo.Delete(id);
            if(del == null)
            {
                return new Apiresponse<string>
                {
                    Message = "Id not Found",
                    Statuscode = 404
                };
            }
            return new Apiresponse<string>
            {
                Message = "Deleted Sucsessfully",
                Statuscode = 200,
                Success = true,
                Data = null
            };
        }
    }
}
