using Microsoft.EntityFrameworkCore;
using InterfaceLibrary;
using UtilitiesLibrary;
using RepositoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Utilities_Library;
using System.Diagnostics;

namespace ServicesLibrary
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        MySqlException mySqlException=new MySqlException();

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        //Get All Product Method to get records
        public IList<IProduct> GetAllProducts()
        {
            var products = new List<IProduct>();
            try
            {
                products =_productRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                // Call the Methods of MySqlException class
                mySqlException.MyExceptionMessage(ex);
                throw ex;

            }
            return products;
        }
        //Method to Get Product by Id
        public IList<IProduct> GetProductsById(int productId)
        {
            var products = new List<IProduct>();
            try
            {
                products = _productRepository.GetProductsById(productId);
            }
            
            catch (Exception ex)
            {
                // Call the Methods of MySqlException class
                mySqlException.MyExceptionMessage(ex);
                throw ex;

            }
            return products;
        }
     /// <summary>
     /// Create a new product
     /// </summary>
     /// <param name="Name"></param>
     /// <param name="Price"></param>
     /// <param name="Manufacturer"></param>
        
        public void CreateNewProduct(string Name, decimal Price, string Manufacturer)
        {
            try
            {
                _productRepository.CreateNewProduct(Name,Price,Manufacturer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <param name="Manufacturer"></param>
        public void UpdateProduct(int ID,string Name, decimal Price, string Manufacturer)
        {
            try
            {
                _productRepository.UpdateProduct(ID,Name, Price, Manufacturer);
            }
            catch (Exception ex)
            {
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
                _productRepository.DeleteProduct(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
