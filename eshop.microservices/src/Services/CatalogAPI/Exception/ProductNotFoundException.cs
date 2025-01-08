namespace CatalogAPI.Exception
{
    public class ProductNotFoundException : FormatException
    {
        public ProductNotFoundException() : base("Product not found!")
        {

        }
    }
}
