using KataPotter.Model;
using System.Collections.Generic;
using System.Linq;

namespace KataPotter.Domain
{
    public class PotterClassifier : IClassifier
    {
        const decimal UNIT_PRICE = 8m;
        readonly List<ISBN> potterISBNs;
        readonly PotterClassification classification;

        public PotterClassifier()
        {
            potterISBNs = new List<ISBN>() { new ISBN("111"), new ISBN("222"), new ISBN("333"), new ISBN("444"), new ISBN("555") };
            classification = new PotterClassification();
        }

        public bool AddItem(Book book)
        {
            if (!IsPotterBook(book))
            {
                return false;
            }

            classification.AddItem(book);
            return true;
        }

        public Money GetBestPrice()
        {
            GetBestPrice(classification);
        }

        public Money GetBestPrice(PotterClassification classification)
        {
            var totalPrice = new Money(0);
            var orderedClassification = classification.GetOrdered();
            if (orderedClassification.Any(c => c.Count > 0))
            {
                // Con el primer elemento, sabemos cuantas colecciones de 5 libros tenemos
                totalPrice += GetPriceForCollectionOf(5, orderedClassification[0].Count);
                // Con el segundo elemento, sabemos cuantas colecciones de 4 libros tenemos, restando las que ya hemos usado con el anterior
                totalPrice += GetPriceForCollectionOf(4, orderedClassification[1].Count - orderedClassification[0].Count);
                // Con el tercer elemento, sabemos cuantas colecciones de 3 libros tenemos, restando las que ya hemos usado con el anterior
                totalPrice += GetPriceForCollectionOf(3, orderedClassification[2].Count - orderedClassification[1].Count);
                // Con el cuarto elemento, sabemos cuantas colecciones de 2 libros tenemos, restando las que ya hemos usado con el anterior
                totalPrice += GetPriceForCollectionOf(2, orderedClassification[3].Count - orderedClassification[2].Count);
                // Con el quinto elemento, sabemos cuantos libros sueltos tenemos, restando lo que ya hemos usado con el anterior
                totalPrice += GetPriceForCollectionOf(1, orderedClassification[4].Count - orderedClassification[3].Count);

                return totalPrice;
            }

            return new Money(0);
        }

        Money GetPriceForCollectionOf(int books, int collections)
        {
            var price = new Money(UNIT_PRICE * books);
            price = price.ApplyDiscount(GetDiscountFor(books));
            price *= collections;
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
            return potterISBNs.Any(isbn => book.MatchBy(isbn));
        }
    }
}