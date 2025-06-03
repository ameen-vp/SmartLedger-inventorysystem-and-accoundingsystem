using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
  public  class UserResponsedto
    {
        public int? Id { get; set; }            
        public string? UserName { get; set; }  
        public string? UserEmail { get; set; }  
        public string? Token { get; set; }
    }
}
