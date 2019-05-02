using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual IEnumerable<CartLine> Lines => lineCollection;

        public virtual void AddItem(Product product, int quantity)
        {
            var line = lineCollection
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
                line.Quantity += quantity;
        }

        public virtual void RemoveLine(Product product)
            => lineCollection.RemoveAll(l => l.Product.Id == product.Id);

        public virtual decimal ComputeTotalValues()
            => lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void ClearCart() => lineCollection.Clear();

    }

    public class CartLine
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
