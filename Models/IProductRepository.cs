using System.Collections.Generic;

namespace WebApplication7.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<StackInfo> StackInfos { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}