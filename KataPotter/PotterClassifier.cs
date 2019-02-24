﻿using System.Collections.Generic;
using System.Linq;

namespace KataPotter
{
    public class PotterClassifier : IClassifier
    {
        const decimal UNIT_PRICE = 8m;
        readonly List<ISBN> potterISBNs = new List<ISBN>() { new ISBN("111"), new ISBN("222"), new ISBN("333"), new ISBN("444"), new ISBN("555") };
        readonly List<ClassificationItem> classification = new List<ClassificationItem>()
        {
            new ClassificationItem("111"),
            new ClassificationItem("222"),
            new ClassificationItem("333"),
            new ClassificationItem("444"),
            new ClassificationItem("555")
        };

        public bool AddItem(Book book)
        {
            if (!IsPotterBook(book))
            {
                return false;
            }

            var item = classification.Where(b => book.IsMyISBN(b.GetISBN())).FirstOrDefault();
            if (item != null)
            {
                item.IncrementCount();
                return true;
            }

            return false;
        }

        public Money GetBestPrice()
        {
            var totalPrice = new Money(0);
            var orderedClassification = classification.OrderBy(i => i.Count).ToList();
            if (orderedClassification.Any(c => c.Count > 0))
            {
                // Con el primer elemento, sabemos cuantas colecciones de 5 libros tenemos
                totalPrice += GetPriceForColectionOf(5, orderedClassification[0].Count);
                // Con el segundo elemento, sabemos cuantas colecciones de 4 libros tenemos, restando las que ya hemos usado con el anterior
                totalPrice += GetPriceForColectionOf(4, orderedClassification[1].Count - orderedClassification[0].Count);
                // Con el tercer elemento, sabemos cuantas colecciones de 3 libros tenemos, restando las que ya hemos usado con el anterior
                totalPrice += GetPriceForColectionOf(3, orderedClassification[2].Count - orderedClassification[1].Count);
                // Con el cuarto elemento, sabemos cuantas colecciones de 2 libros tenemos, restando las que ya hemos usado con el anterior
                totalPrice += GetPriceForColectionOf(2, orderedClassification[3].Count - orderedClassification[2].Count);
                // Con el quinto elemento, sabemos cuantos libros sueltos tenemos, restando lo que ya hemos usado con el anterior
                totalPrice += GetPriceForColectionOf(1, orderedClassification[4].Count - orderedClassification[3].Count);

                return totalPrice;
            }

            return new Money(0);
        }

        Money GetPriceForColectionOf(int books, int colections)
        {
            var price = new Money(UNIT_PRICE * books);
            price = price.ApplyDiscount(GetDiscountFor(books));
            price *= colections;
            return price;
        }

        decimal GetDiscountFor(int differentBooks)
        {
            if (differentBooks == 2) return 0.95m;
            if (differentBooks == 3) return 0.9m;
            if (differentBooks == 4) return 0.8m;
            if (differentBooks == 5) return 0.75m;

            return 1m;
        }
        
        bool IsPotterBook(Book book)
        {
            return potterISBNs.Any(isbn => book.IsMyISBN(isbn));
        }
    }
}