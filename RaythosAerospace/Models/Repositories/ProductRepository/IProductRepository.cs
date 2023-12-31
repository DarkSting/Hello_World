using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        ProductModel GetProduct(string productid);

        ProductModel AddProduct(ProductModel model);

        ProductModel DeleteProduct(string productid);

        IList<ProductModel> GetAllProducts();
        bool AssigningOrderIDsTotheProduct(string orderId, string productId);

        IList<ProductModel> GetProductsByUser(string userid);

        void AddOrder(ProductModel model);



    }
}
