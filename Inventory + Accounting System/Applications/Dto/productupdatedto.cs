using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Dto
{
    public class productupdatedto
    {
        public int Quantity { get; set; }
        public decimal Sellingprice { get; set; }

        public int categoryId { get; set; }

        public string Categoryname { get; set; }
        public string Suppilername { get; set; }

        public string Suppliername
        {
            get; set;
        }
    }
}
