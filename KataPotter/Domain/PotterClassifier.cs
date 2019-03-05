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

        Dictionary<string, Money> PriceMemoryCache { get; set; } = new Dictionary<string, Money>();

        public PotterClassifier()
        {
            potterISBNs = new List<ISBN>() { new ISBN("111"), new ISBN("222"), new ISBN("333"), new ISBN("444"), new ISBN("555") };
            classification = new PotterClassification();
        }

        public bool AddBook(Book book)
        {
            if (!IsPotterBook(book))
            {
                return false;
            }

            classification.AddBook(book);
            return true;
        }

        public Money GetBestPrice()
        {
            return GetBestPrice(classification);
        }

        Money GetBestPrice(PotterClassification classification)
        {
            if (PriceMemoryCache.ContainsKey(classification.GetKey()))
            {
                return PriceMemoryCache[classification.GetKey()];
            }

            if (classification.HasSomeBookMoreThanOnce())
            {
                var bestPrice = GetNonTrivialSetBestPrice(classification);
                PriceMemoryCache.Add(classification.GetKey(), bestPrice);
                return bestPrice;
            }

            var price = GetTrivialSetPrice(classification.GetBooksCount());
            PriceMemoryCache.Add(classification.GetKey(), price);
            return price;
        }

        Money GetNonTrivialSetBestPrice(PotterClassification classification)
        {
            var prices = new List<Money>();

            for (int i = 1; i <= 5; i++)
            {
                var steppedClassification = classification.StepCaseSet(i, out PotterClassification extractedClassification);
                if (!steppedClassification.HasNegativeCounts())
                {
                    var steppedSetBestPrice = GetBestPrice(steppedClassification);
                    var extractedSetPrice = GetTrivialSetPrice(extractedClassification.GetBooksCount());
                    prices.Add(steppedSetBestPrice + extractedSetPrice);
                }
            }

            var bestPrice = prices.Min();

            return bestPrice;
        }

        Money GetTrivialSetPrice(int books)
        {
            var price = new Money(UNIT_PRICE * books);
            price = price.ApplyDiscount(GetDiscountFor(books));
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