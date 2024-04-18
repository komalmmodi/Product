using AutoMapper;
using Product.Entities;
using Product.Models;
using Product.Repositories;

namespace Product.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(
       IProductRepository productRepository,
       IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDetails> GetById(int id)
        {
            var user = await _productRepository.GetById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            return user;
        }

        public async Task Update(ProductRequestList model)
        {
            var product = await _productRepository.GetById(model.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            // copy model props to user
            //_mapper.Map(model, product);
            product.Price = model.Price;

            // save user
            await _productRepository.Update(product);
        }

    }
}
