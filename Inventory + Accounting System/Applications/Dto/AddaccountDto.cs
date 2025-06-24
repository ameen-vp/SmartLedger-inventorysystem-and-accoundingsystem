using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
   public class AddaccountDto
    {
        public string Name { get; set; }
        public AccountType Type { get; set; }
    }
}
