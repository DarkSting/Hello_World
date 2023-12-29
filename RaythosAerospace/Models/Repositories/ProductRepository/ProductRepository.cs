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

        public void AddOrder(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public ProductModel AddProduct(ProductModel model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();

            return model;
        }

        public ProductModel DeleteProduct(string productid)
        {
            ProductModel foundproduct =_context.Products.FirstOrDefault(f => f.ProductId == productid);

            _context.Products.Remove(foundproduct);
            _context.SaveChanges();

            return foundproduct;
        }

        public IList<ProductModel> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public ProductModel GetProduct(string productid)
        {
            return _context.Products.Find(productid);
        }
    

        public IList<ProductModel> GetProductByUser(string userid)
        {
            return _context.Products.Where(u => u.UserId == userid).ToList();
        }
    }
}
