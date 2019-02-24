using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KataPotter.Model
{
    internal class PotterClassification
    {
        List<ClassificationItem> classification;

        public PotterClassification()
        {
            classification = new List<ClassificationItem>()
            {
                new ClassificationItem("111"),
                new ClassificationItem("222"),
                new ClassificationItem("333"),
                new ClassificationItem("444"),
                new ClassificationItem("555")
            };
        }

        public void AddItem(Book book)
        {
            var item = classification.Where(citem => book.MatchBy(citem.ISBN)).First();
            item.IncrementCount();
        }
    }
}
