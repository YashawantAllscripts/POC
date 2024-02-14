using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary
{
    public interface IOrderService
    {
        IList<IOrder> GetOrdersByProductId(int productId);

        //Declration of Cereate new order
        void CreateNewOrder(int productID, DateTimeOffset date, int quantity);

        void UpdateOrder(int Id, int productID, DateTimeOffset date, int quantity);

        void DeleteOrder(int Id);
    }
}
