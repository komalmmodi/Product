
using Dapper;
using Product.Entities;
using Product.Helpers;
namespace Product.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ProductDetails> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            SELECT * FROM tabProductDetails 
            WHERE ProductId = @id
        """;
            return await connection.QuerySingleOrDefaultAsync<ProductDetails>(sql, new { id });
        }

        public async Task Update(ProductDetails product)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            UPDATE tabProductDetails 
            SET Price = @Price
            WHERE ProductId = @ProductId
        """;
            await connection.ExecuteAsync(sql, product);
        }
    }
}
