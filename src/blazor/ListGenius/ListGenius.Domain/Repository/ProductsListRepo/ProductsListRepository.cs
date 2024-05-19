using ListGenius.Domain.Context;
using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.ProductsListRepo
{
    public class ProductsListRepository : GenericRepository<ProductsList>, IProductsListRepository
    {
        public ProductsListRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ProductsList Disable(long id)
        {
            if (!_context.Products.Any(p => p.Id.Equals(id)))
            {
                return new ProductsList();
            }

            var productsList = _context.ProductsList.SingleOrDefault(p => p.Id.Equals(id));

            if (productsList != null)
            {
                productsList.Enabled = false;
                try
                {
                    _context.Entry(productsList).CurrentValues.SetValues(productsList);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return productsList ?? new ProductsList();
        }

        public List<ProductsList> FindByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return _context.ProductsList
                 .Where(
                     p => p.Name.ToLower().Contains(Name.ToLower()))
                 .ToList();
            }
            return new List<ProductsList>();
        }
    }
}
