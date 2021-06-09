namespace ProductApi.Models.Interfaces
{
    public interface IProductMapper
    {
        Product Map(ProductDto productDto);
    }
}
