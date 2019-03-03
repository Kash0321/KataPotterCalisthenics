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

                var prices = new List<Money>();
                var steppedClassification1 = classification.StepCaseSet(1, out PotterClassification extracted1);
                if (!steppedClassification1.GetItems().Any(i => i.Count < 0))
                {
                    prices.Add(GetBestPrice(steppedClassification1) + GetTrivialSetPrice(extracted1.GetBooksCount()));
                }
                var steppedClassification2 = classification.StepCaseSet(2, out PotterClassification extracted2);
                if (!steppedClassification2.GetItems().Any(i => i.Count < 0))
                {
                    prices.Add(GetBestPrice(steppedClassification2) + GetTrivialSetPrice(extracted2.GetBooksCount()));
                }
                var steppedClassification3 = classification.StepCaseSet(3, out PotterClassification extracted3);
                if (!steppedClassification3.GetItems().Any(i => i.Count < 0))
                {
                    prices.Add(GetBestPrice(steppedClassification3) + GetTrivialSetPrice(extracted3.GetBooksCount()));
                }
                var steppedClassification4 = classification.StepCaseSet(4, out PotterClassification extracted4);
                if (!steppedClassification4.GetItems().Any(i => i.Count < 0))
                {
                    prices.Add(GetBestPrice(steppedClassification4) + GetTrivialSetPrice(extracted4.GetBooksCount()));
                }
                var steppedClassification5 = classification.StepCaseSet(5, out PotterClassification extracted5);
                if (!steppedClassification5.GetItems().Any(i => i.Count < 0))
                {
                    prices.Add(GetBestPrice(steppedClassification5) + GetTrivialSetPrice(extracted5.GetBooksCount()));
                }

                var min = prices.Min();
                return min;
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