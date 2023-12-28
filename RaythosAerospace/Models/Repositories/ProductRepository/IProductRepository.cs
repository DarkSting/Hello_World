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

        IList<ProductModel> GetProductByUser(string userid);



    }
}
