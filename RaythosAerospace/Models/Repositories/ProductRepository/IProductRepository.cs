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

        int GetSoldAircraftCount(string aircraftId);
        ProductModel DeleteProduct(string productid);

        IList<ProductModel> GetAllProductsForAnOrder(string orderId);

        void UpdateTheProducts(IList<ProductModel> products);
        void ReduceProductCountByOne(string productId);

        IList<ProductModel> GetAllProducts();
        bool AssigningOrderIDsTotheProduct(string orderId, string productId);

        IList<ProductModel> GetProductAddedToTheCartByUser(string userid);

        void AddOrder(ProductModel model);



    }
}
