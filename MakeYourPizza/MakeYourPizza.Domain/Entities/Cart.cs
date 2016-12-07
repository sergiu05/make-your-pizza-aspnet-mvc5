using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.Domain.Entities
{
    /* Learning sources: 
     * "Pro ASP.NET MVC 5", Adam Freeman, 5th Ed., p.215 
     * implementation of the IEnumerable interface: https://msdn.microsoft.com/en-us/library/system.collections.ienumerable.getenumerator(v=vs.110).aspx
     */
    public class Cart : IEnumerable
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public int CountItems
        {
            get
            {
                return lineCollection.Count;
            }
        }

        public void AddItem(ProductInterface product, int quantity = 1)
        {
            CartLine line = lineCollection
                            .Where(p => p.Product.Id == product.Id)
                            .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(ProductInterface product)
        {
            lineCollection.RemoveAll(l => l.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get
            {
                return lineCollection;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private CartEnumerator GetEnumerator()
        {
            return new CartEnumerator(lineCollection);
        }
    }

    internal class CartEnumerator : IEnumerator
    {
        private List<CartLine> collection;

        int position = -1;

        public CartEnumerator(List<CartLine> lineCollection)
        {
            collection = lineCollection;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public CartLine Current
        {
            get
            {
                try
                {
                    return collection[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < collection.Count);
        }

        public void Reset()
        {
            position = -1;
        }
    }

    public class CartLine
    {
        public ProductInterface Product { get; set; }
        public int Quantity { get; set; }
    }
}
