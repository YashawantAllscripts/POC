using Microsoft.EntityFrameworkCore.Metadata.Internal;
using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public class Orders:IOrder
    {
        [Key]
        
        //Get set property for Id
        public int ID { get; set; }
        
        //Get set property for ProductId
        public int ProductID {  get; set; }
        
        //Get set property for Date
        public DateTime Date { get; set;}

        //Get set property for Quantity
        [Range(0,100,ErrorMessage ="Quantity Must be between 0 and 100")]
        public int Quantity {  get; set; }

        public Product Products { get; set; }


 
    }
}
