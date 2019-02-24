using System;
using System.Collections.Generic;

namespace KataPotter
{
    public class ShoppingCart
    {
        readonly List<Book> books = new List<Book>();
        readonly IEnumerable<IClassifier> classifiers;

        public ShoppingCart(IEnumerable<IClassifier> classifiers)
        {
            this.classifiers = classifiers ?? throw new ArgumentNullException(nameof(classifiers));
        }

        public int GetCount()
        {
            return books.Count;
        }

        public void AddItem(Book book)
        {
            books.Add(book);
            foreach (var classifier in classifiers)
            {
                classifier.AddItem(book);
            }
        }

        public Money GetCartTotalPrice()
        {
            var totalPrice = Money.Zero();

            foreach (var classifier in classifiers)
            {
                totalPrice += classifier.GetBestPrice();
            }

            return totalPrice;
        }
    }
}
