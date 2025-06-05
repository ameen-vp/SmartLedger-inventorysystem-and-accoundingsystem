using Applications.ApiResponse;
using Applications.Dto;
using Applications.Interface;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Service
{
   public class CategoryService : ICatservice
    { 
        private readonly ICatRepo _catRepo;
        private readonly IMapper _mapper;

    public CategoryService(ICatRepo catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;
        }
     public async Task<Apiresponse<CategoryAdddto>> AddCategory(CategoryAdddto categoryAdddto)
        {
            try
            {

                var exit = await _catRepo.Catagoryexit(categoryAdddto.CategoryName);
                if (exit)
                {
                    return new Apiresponse<CategoryAdddto>
                    {
                        Data = null,
                        Message = "Category already exists.",
                        Success = false,
                        Statuscode = 400
                    };
                }
                var category = _mapper.Map<Category>(categoryAdddto);
                var res = _catRepo.AddCatagory(category);
                return new Apiresponse<CategoryAdddto>
                {
                    Data = categoryAdddto,
                    Message = "Category added successfully.",
                    Success = true,
                    Statuscode = 200
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       public async Task<Apiresponse< List<Category>>> GetAllCategorys()
        {
            var res = await _catRepo.GetAllCategorys();
            return new Apiresponse<List<Category>>
            {
                Data = res,
                Message = "category Fetched sucessessfully"
            };
        }
    }
}
