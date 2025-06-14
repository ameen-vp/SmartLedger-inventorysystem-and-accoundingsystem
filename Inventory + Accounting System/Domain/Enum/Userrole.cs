using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum Userrole
    {
        Admin = 1,
        Accountant = 2,
        Staff = 3
    }
    public enum Transactiontype
    {
        Purchase,
        Sales,
        Return,
        Damage
    }
}
