using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KataPotter.Model
{
    internal class PotterClassification
    {
        List<ClassificationItem> classification;

        List<ClassificationItem> GetCopy()
        {
            var result = new List<ClassificationItem>();

            foreach (var item in classification)
            {
                result.Add(new ClassificationItem(item.ISBN.ToString(), item.Count));
            }

            return result;
        }

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

        PotterClassification(List<ClassificationItem> classification)
        {
            this.classification = classification;
        }

        public void AddBook(Book book)
        {
            var item = classification.Where(citem => book.MatchBy(citem.ISBN)).First();
            item.IncrementCount();
        }

        public bool HasSomeBookMoreThanOnce()
        {
            return classification.Any(i => i.Count > 1);
        }

        public bool HasNegativeCounts()
        {
            return classification.Any(i => i.Count < 0);
        }

        public int GetBooksCount()
        {
            return classification.Sum(i => i.Count);
        }

        public string GetKey()
        {
            var result = new StringBuilder();

            var k = classification.OrderBy(c => c.Count).ToList();

            foreach (var item in k)
            {
                result.Append($".{item.Count}");
            }

            return result.ToString();
        }

        public PotterClassification StepCaseSet(int pos, out PotterClassification extracted)
        {
            var extractedClassificationList = new List<ClassificationItem>()
            {
                new ClassificationItem("111"),
                new ClassificationItem("222"),
                new ClassificationItem("333"),
                new ClassificationItem("444"),
                new ClassificationItem("555")
            };
            var resultClassificationList = GetCopy();

            resultClassificationList[pos - 1].IncrementCount(-1);
            extractedClassificationList[pos - 1].IncrementCount();

            extracted = new PotterClassification(extractedClassificationList);
            return new PotterClassification(resultClassificationList);
        }

        public override string ToString()
        {
            return GetKey();
        }
    }
}
