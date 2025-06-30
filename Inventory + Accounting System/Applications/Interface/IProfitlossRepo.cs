using Applications.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface IProfitlossRepo
    {
        Task<ProfitandLoss> CalculateProfitandloss(DateOnly startDate, DateOnly endDate);
    }
}
