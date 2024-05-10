using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Model.Context;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.ProductListRepo
{
    public class ProductsListRepository : GenericRepository<ProductsList>, IProductsListRepository
    {
        public ProductsListRepository(MySQLContext context) : base(context)
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
