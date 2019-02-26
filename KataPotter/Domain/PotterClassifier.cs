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
            return GetBestPrice(classification);
        }

        Money GetBestPrice(PotterClassification classification)
        {
            Money result = Money.Zero();
            if (classification.GetItems().Any(i => i.Count > 1))
            {
                var steppedClassification = classification.StepCaseSet(out PotterClassification extracted);
                result += GetTrivialSetPrice(extracted.GetBooksCount());

                return result + GetBestPrice(steppedClassification);
            }

            return GetTrivialSetPrice(classification.GetBooksCount());
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