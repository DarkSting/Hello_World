using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        
        public bool AssigningOrderIDsTotheProduct(string orderId,string productId)
        {
            ProductModel model = _context.Products.Find(productId);

            model.OrderId = orderId;

            bool recordUpdated = false;

            try
            {

                EntityEntry changed = _context.Products.Attach(model);
                changed.State = EntityState.Modified;
                _context.SaveChanges();
                recordUpdated = true;

            }catch(Exception e)
            {
                recordUpdated = false;
            }

            return recordUpdated;
        }


        public IList<ProductModel> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public ProductModel GetProduct(string productid)
        {
            return _context.Products.Find(productid);
        }

        public IList<ProductModel> GetProductsByUser(string userid)
        {
            return _context.Products.Where(u => u.UserId == userid && u.OrderId==null).ToList();
        }


        //reducing the count for a product
        public void ReduceProductCountByOne(string productId)
        {
            ProductModel foundProduct = _context.Products.Find(productId);

            foundProduct.Count--;

            EntityEntry changed =_context.Products.Attach(foundProduct);
            changed.State = EntityState.Modified;

            _context.SaveChanges();

        }

        public IList<ProductModel> GetAllProductsForAnOrder(string orderId)
        {
            IList<ProductModel> foundList = _context.Products.Where(o => o.OrderId == orderId).ToList();

            foreach(ProductModel current in foundList)
            {
                current.AirCraft = _context.AirCrafts.Find(current.AirCraftId);
            }

            return foundList;
        }

        public void UpdateTheProducts(IList<ProductModel> products)
        {
            foreach(ProductModel current in products)
            {
                ProductModel foundProduct = _context.Products.Find(current.ProductId);

                foundProduct.ComponentAssemblyStatus = current.ComponentAssemblyStatus;
                foundProduct.DesignEngineeringStatus = current.DesignEngineeringStatus;
                foundProduct.DeliveredStatus = current.DeliveredStatus;
                foundProduct.TestingCertificationStatus = current.TestingCertificationStatus;
                foundProduct.PrototypingTestingStatus = current.PrototypingTestingStatus;
                foundProduct.ShippedStatus = current.ShippedStatus;

                _context.Products.Update(foundProduct);
            }

            _context.SaveChanges();
        }
    }
}
