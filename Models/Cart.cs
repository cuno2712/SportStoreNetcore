using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication7.Models
{
    public class Cart
    {
        private readonly List<CartLine> lineCollection = new List<CartLine>();
        public virtual IEnumerable<CartLine> Lines => lineCollection;

        public virtual void AddItem(Product product, int quantity)
        {
            var line = lineCollection.Where(x => x.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            else
                line.Quantity += quantity;
        }

        public virtual void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public virtual decimal CaculateTotal()
        {
            return lineCollection.Sum(x => x.Quantity * x.Product.Price);
        }

        public virtual void Clear()
        {
            lineCollection.Clear();
        }

        public class CartLine
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int CartLineID { get; set; }

            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}