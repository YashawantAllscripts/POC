using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EntityLibrary;
using UtilitiesLibrary;
using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace RepositoryLibrary
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProductOrderDbContext _dbContext;
        private readonly IConfiguration _configuration;
        MySqlException mySqlException = new MySqlException();

        //Constructor for dbcontext intialization
        public OrderRepository(ProductOrderDbContext context,IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }
        //Methods to get Records of orders based on productId using stored procedure
        /// <summary>
        /// Get Orders By productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<IOrder> GetOrdersByProductId(int productId)
        {
            var orders = new List<IOrder>();
            try
            {
                orders = _dbContext.Orders.FromSqlRaw($" GetOrdersByProductIDSelPr {productId} ").Cast<IOrder>().ToList();
            }
            catch (SqlException ex)
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
                _dbContext.Database.ExecuteSqlRaw("EXEC CreateNewOrderInsPr @productID, @date, @quantity",
                    new SqlParameter("@productID", productID),
                    new SqlParameter("@date", date),
                    new SqlParameter("@quantity", quantity));
            }
            catch (SqlException ex)
            {
                mySqlException.ExceptionStackTrace(ex);
                throw ex;
            }
        }
        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="productID"></param>
        /// <param name="date"></param>
        /// <param name="quantity"></param>
        public void UpdateOrder(int Id, int productID, DateTimeOffset date, int quantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UpdateOrderUpdPr", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", Id);
                        command.Parameters.AddWithValue("@ProductID", productID);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@Quantity", quantity);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// DeleteOrder Method
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteOrder(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand command = new SqlCommand("DeleteOrderDelPr", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("ID", Id);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                mySqlException.ExceptionStackTrace(ex);
                throw ex;
            }
        }
    }
}
