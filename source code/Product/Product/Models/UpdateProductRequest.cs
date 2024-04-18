namespace Product.Models
{
    public class UpdateProductRequest
    {
       public List<ProductRequestList> productList { get; set; }
    }

    public class ProductRequestList
    {
        public int ProductId { get; set; }

        public int Price { get; set; }
    }
}
