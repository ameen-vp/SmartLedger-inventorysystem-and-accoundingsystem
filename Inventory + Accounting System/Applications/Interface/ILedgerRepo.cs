using Applications.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interface
{
   public interface ILedgerRepo
    {
        Task AddEntry(AddLedgerDto addLedgerDto);
    }
}
