using Microsoft.EntityFrameworkCore;
using InterfaceLibrary;
using UtilitiesLibrary;
using RepositoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        MySqlException mySqlException = new MySqlException();

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        /// <summary>
        /// GetOrdersByProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IList<IOrder> GetOrdersByProductId(int productId)
        {
            var orders = new List<IOrder>();
            try
            {
                //Check Product Id greathan zero or not
                orders = _orderRepository.GetOrdersByProductId(productId);   
            }
            catch (Exception ex)
            {
                // Call the Methods of MySqlException class
                mySqlException.ExceptionStackTrace(ex);
                throw ex;
            }
            return orders;
        }

        /// <summary>
        /// Create a new Order 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="productID"></param>
        /// <param name="date"></param>
        /// <param name="quantity"></param>
        public void CreateNewOrder(int productID, DateTimeOffset date, int quantity)
        {
            try
            {
                _orderRepository.CreateNewOrder(productID, date, quantity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       /// <summary>
       /// Update order method
       /// </summary>
       /// <param name="Id"></param>
       /// <param name="productID"></param>
       /// <param name="date"></param>
       /// <param name="quantity"></param>
        public void UpdateOrder(int Id,int productID, DateTimeOffset date, int quantity)
        {
            try
            {
                _orderRepository.UpdateOrder(Id,productID, date, quantity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete Order Method
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteOrder(int Id)
        {
           try
           {
             _orderRepository.DeleteOrder(Id);
           }
           catch (Exception ex)
           {
               throw ex;
           }
            
        }
    }
}
