using JoaoDiasDev.ProductList.Model;
using JoaoDiasDev.ProductList.Model.Context;
using JoaoDiasDev.ProductList.Repository.Generic;

namespace JoaoDiasDev.ProductList.Repository.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MySQLContext context) : base(context)
        {
        }

        public Product Disable(long id)
        {
            if (!_context.Products.Any(p => p.Id.Equals(id)))
            {
                return new Product();
            }

            var product = _context.Products.SingleOrDefault(p => p.Id.Equals(id));

            if (product != null)
            {
                product.Enabled = false;
                try
                {
                    _context.Entry(product).CurrentValues.SetValues(product);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return product ?? new Product();
        }

        public List<Product> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.Products
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<Product>();
        }
    }
}
