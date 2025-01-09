using BuildingBlocks.Exception;

namespace CatalogAPI.Exception
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id)
        {

        }
    }
}
