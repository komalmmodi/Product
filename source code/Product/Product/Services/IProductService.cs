using Product.Entities;
using Product.Models;

namespace Product.Services
{
    public interface IProductService
    {
        Task<ProductDetails> GetById(int id);
        Task Update(ProductRequestList model);
    }
}
