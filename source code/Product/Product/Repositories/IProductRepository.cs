using Product.Entities;

namespace Product.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDetails> GetById(int id);
        Task Update(ProductDetails product);
    }
}
