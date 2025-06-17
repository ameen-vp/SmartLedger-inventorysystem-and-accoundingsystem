using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        [EnumMember(Value = "PURCHASE")]
        Purchase,
        [EnumMember(Value = "SALES")]
        Sales,
        [EnumMember(Value = "RETURN")]
        Return,
        [EnumMember(Value = "DAMAGE")]
        Damage
    }
    public enum InvoiceStatus
    {
        [EnumMember(Value = "PENDING")]
        PENDING,
         [EnumMember(Value = "PAID")]
        PAID,
        [EnumMember(Value = "CANCELLED")]
        CANCELLED
    }
}
