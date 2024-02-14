using Microsoft.EntityFrameworkCore;
using EntityLibrary;
using UtilitiesLibrary;
using InterfaceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Utilities_Library;
using Microsoft.Extensions.Configuration;
using System.Buffers.Text;
using System.Data;


namespace RepositoryLibrary
{
    public class ProductRepository:IProductRepository
    {
        private readonly ProductOrderDbContext _dbContext;
        private readonly IConfiguration _configuration;
        
        MySqlException mySqlException = new MySqlException();
        
        //Constructor for dbcontext intialization
        public ProductRepository(ProductOrderDbContext context,IConfiguration configuration) 
        {
            _dbContext = context;
            _configuration = configuration;

        }

        // Pulic method to get all the records from product table
        //public List<IProduct> GetAllProducts()
        //{
        //    var product=new List<IProduct>();
        //    try
        //    {
        //        product = _dbContext.Product.ToList<IProduct>();
                
        //    }
        //    catch (Exception ex)
        //    {
        //        // Call the Methods of MySqlException class
        //        mySqlException.MyExceptionMessage(ex);
        //        throw ex;

        //    }
        //    return product;
        //}

        // Pulic method to get all the records from product table
        public List<IProduct> GetAllProducts()
        {
            List<IProduct> products = new List<IProduct>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Product", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Manufacturer = reader["Manufacturer"].ToString()
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                mySqlException.MyExceptionMessage(ex);
                throw ex;
            }
            return products;
        }


        ////Methods to get Records of proudct based on Id using stored procedure
        //public List<IProduct> GetProductsById(int productId) 
        //{
        //    var products=new List<IProduct>();
        //    try
        //    {

        //        products= _dbContext.Product.FromSqlRaw($"exec GetProductsByIdSelPr {productId}").Cast<IProduct>().ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Call the Methods of MySqlException class
        //        mySqlException.MyExceptionMessage(ex);
        //        throw ex;
        //    }
        //    return products;
        //}

        //Methods to get Records of proudct based on Id using stored procedure
        public List<IProduct> GetProductsById(int productId)
        {
          List<IProduct> products = new List<IProduct>();
             try
             {
                using(SqlConnection connection=new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand command = new SqlCommand("GetProductsByIdSelPr", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", productId);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet=new DataSet();
                        connection.Open();
                        adapter.Fill(dataSet);
                        foreach(DataRow row in dataSet.Tables[0].Rows)
                        {
                            Product product = new Product
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                Name = row["Name"].ToString(),
                                Price = Convert.ToDecimal(row["Price"]),
                                Manufacturer = row["Manufacturer"].ToString()

                            };
                            products.Add(product);
                        }

                    }
                }
             }
             catch(SqlException ex)
             {
                mySqlException.MyExceptionMessage(ex);
                throw ex;
             }
          return products;
        }
        
        /// <summary>
        /// Create a new Product
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <param name="Manufacturer"></param>
        public void CreateNewProduct(string Name,decimal Price,string Manufacturer)
        {
            try
            {
                _dbContext.Database.ExecuteSqlRaw("EXEC CreateNewProductInsPr @name, @price, @manufacturer",
                  new SqlParameter("@name", Name),
                  new SqlParameter("@price", Price),
                  new SqlParameter("@manufacturer", Manufacturer));
            }
            catch (SqlException ex)
            {
                mySqlException.ExceptionStackTrace(ex);
                throw ex;
            }
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <param name="Manufacturer"></param>
        public void UpdateProduct(int Id, string Name, decimal Price, string Manufacturer)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand command = new SqlCommand("UpdateProductUpdPr", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("ID", Id);
                        command.Parameters.AddWithValue("Name", Name);
                        command.Parameters.AddWithValue("Price", Price);
                        command.Parameters.AddWithValue("Manufacturer", Manufacturer);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException ex)
            {
                mySqlException.ExceptionStackTrace(ex);
                throw ex;
            }
        }
        
        /// <summary>
        /// Method Delete Product
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand command = new SqlCommand("DeleteProductDelPr", connection))
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
