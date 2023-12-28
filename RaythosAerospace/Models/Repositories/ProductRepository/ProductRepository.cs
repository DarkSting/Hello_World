using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _context;

        public ProductRepository(AppDBContext context)
        {
            _context = context;
        }
        public ProductModel AddProduct(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public ProductModel DeleteProduct(string productid)
        {
            throw new NotImplementedException();
        }

        public IList<ProductModel> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProduct(string productid)
        {
            throw new NotImplementedException();
        }

        public IList<ProductModel> GetProductByUser(string userid)
        {
            throw new NotImplementedException();
        }
    }
}
