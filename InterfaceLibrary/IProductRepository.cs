using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_Library;

namespace InterfaceLibrary
{
    public interface IProductRepository
    {
        //Method Get All Product
        public List<IProduct> GetAllProducts();

        //Method for Get Products By Id
        public List<IProduct> GetProductsById(int productId); 
        
        void CreateNewProduct(string Name, decimal Price, string Manufacturer);

        void UpdateProduct(int id, string Name, decimal Price, string Manufacturer);

        void DeleteProduct(int Id);

    }
}
