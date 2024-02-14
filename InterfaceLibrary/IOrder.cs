using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary
{
    public interface IOrder
    {
        //Get set property
         int ID { get; set; }

        //Get set property
        int ProductID { get; set; }

        //Get set property
        DateTime Date { get; set; }

        //Get set property
        int Quantity { get; set; }
    }
}
