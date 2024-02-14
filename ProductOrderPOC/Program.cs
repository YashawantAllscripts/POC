using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryLibrary;
using EntityLibrary;
using UtilitiesLibrary;
using System.ComponentModel.Design;
using ServicesLibrary;
using InterfaceLibrary;
using Microsoft.Identity.Client;

class Program
{
    static void Main()
    { 
        //Database Connection
        var option=new DbContextOptionsBuilder<ProductOrderDbContext>().UseSqlServer("Data Source=PF2ASHM9; Initial Catalog=ProductOrder;Integrated Security=True;Encrypt=False;").Options;
        
        var dbContext = new ProductOrderDbContext(option);
      
        var orderRepository=new OrderRepository(dbContext);
        int Id;
        string strId="";
        var productRepository=new ProductRepository(dbContext);
        var inputParameterValidator = InputParameterValidator.inputParameterValidator;
        //var validateId2 = new InputParameterValidator();
        var mySqlException = new MySqlException();
        var productService = new ProductService(productRepository);

        Console.WriteLine("Getting Records of Tables With using Entity Framework");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("ProductId:ProductName:Price:Manufacturer");
        // Methods to Get All Products
        
        try
        {
            var product = productService.GetAllProducts();

            if (product != null)
            {
                foreach (var prd in product)
                {
                    Console.WriteLine($"{prd.ID} :{prd.Name} :{prd.Price} : {prd.Manufacturer}  ");

                }
            }
            else
            {
                Console.WriteLine("No Records found in table");
            }
        }
        catch (Exception ex)
        {
            // Call the Methods of MySqlException class
            mySqlException.MyExceptionMessage(ex);

        }

        Console.WriteLine("Getting Records By Using Store Procedure GetProductById");
        Console.WriteLine("--------------------------------------------------------");
        try
        {
            Console.WriteLine("Enter Product to Display Product");
            strId = Console.ReadLine();
            //Validate the given input is Integer or not
            if (inputParameterValidator.ValidateString(strId))
            {
                Id = Convert.ToInt32(strId);

                //Id Must be greater than 0 and less than 10000
                if (!inputParameterValidator.IdValidator(Id))
                {
                    throw new ArgumentException("Invalid ProductID :"+ Id +", ID must be greater than 0 and less than 10000");
                }

                // calling the method GetProductsById
                var product = productService.GetProductsById(Id); 

                Console.WriteLine("ProductId:ProductName:Price:Manufacturer");
                foreach (var prd in product)
                {
                    Console.WriteLine($"{prd.ID} :{prd.Name}  : {prd.Price} : {prd.Manufacturer}  ");
                }
            }
        }

        catch (SqlException ex)
        {
            //Calling methods of MySqlException class
            mySqlException.ExceptionStackTrace(ex);
            mySqlException.MyExceptionMessage(ex);
            
        }

        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("Getting Records By Using Store Procedure GetOrderByProductId");
        Console.WriteLine("------------------------------------------------------------");
        try
        {
            Console.WriteLine("Enter ProductId to Display Order Details ");
            strId = Console.ReadLine();
            //Validate the given input is Integer or not
            if (inputParameterValidator.ValidateString(strId))
            {
                Id = Convert.ToInt32(strId);
                //Id Must be greater than 0 and less than 10000
                if (!inputParameterValidator.IdValidator(Id)) 
                {
                    throw new ArgumentException("Invalid OrderID, ID must be greater than 0 and less than 10000 ");
                }
                //Calling the method Get orders by productId
                var order = orderRepository.GetOrdersByProductId(Id);

                Console.WriteLine("OrderId:ProductId:OrderDate:Quantity ");
                foreach (var ord in order)
                {
                    Console.WriteLine($"{ord.ID} : {ord.ProductID}  :{ord.Date}: {ord.Quantity}  ");
                }

            }
        }
        catch (SqlException ex)
        {
            //Calling methods of MySqlException class
            mySqlException.MyExceptionMessage(ex);
            mySqlException.ExceptionStackTrace(ex);
         

        }
        
    }
}