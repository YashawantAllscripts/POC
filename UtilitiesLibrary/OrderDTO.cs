using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_Library
{
    public class OrderDTO
    {
        public int ProductID {  get; set; }
        public DateTimeOffset Date { get; set; }
        public int Quantity { get; set; }

    }
}
