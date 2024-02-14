using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary
{
  public interface IOrderRepository
    {
        //Method declartaion Get Orders By ProductId
        List<IOrder> GetOrdersByProductId(int productId);

        //Method Declaration of Creating a new record
        void CreateNewOrder( int productID, DateTimeOffset date, int quantity);

        void UpdateOrder(int Id, int productID, DateTimeOffset date, int quantity);

        void DeleteOrder(int Id);
    }
}
