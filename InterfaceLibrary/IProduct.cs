using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary
{
    public interface IProduct
    {
        //Get set property
        int ID { get; set; }

        //Get set property
        string Name { get; set; }
        
        //Get set property
        decimal Price { get; set; }
        
        //Get set property
        string Manufacturer { get; set; }
    }
}
