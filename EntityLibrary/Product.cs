using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class Product:IProduct
    {
        [Key]
        //Get Set property for Id
        public int ID { get; set; }

        //Get Set property for Name
        public string Name { get; set; }

        //Get Set property for price
        public decimal Price {  get; set; }

        //Get Set property for Manufacturer
        public string Manufacturer {  get; set; }

        public List<Orders> Orders { get; set; }



    }
}
