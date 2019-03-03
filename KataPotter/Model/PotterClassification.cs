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

        PotterClassification(List<ClassificationItem> classification)
        {
            this.classification = classification;
        }

        public void AddItem(Book book)
        {
            var item = classification.Where(citem => book.MatchBy(citem.ISBN)).First();
            item.IncrementCount();
        }

        public IReadOnlyCollection<ClassificationItem> GetItems()
        {
            return new ReadOnlyCollection<ClassificationItem>(classification);
        }

        public int GetBooksCount()
        {
            return classification.Sum(i => i.Count);
        }

        private List<ClassificationItem> CopyClassification(List<ClassificationItem> classification)
        {
            var result = new List<ClassificationItem>();

            foreach (var item in classification)
            {
                result.Add(new ClassificationItem(item.ISBN.ToString(), item.Count));
            }

            return result;
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
            var resultClassificationList = CopyClassification(classification);

            resultClassificationList[pos - 1].IncrementCount(-1);
            extractedClassificationList[pos - 1].IncrementCount();

            extracted = new PotterClassification(extractedClassificationList);
            return new PotterClassification(resultClassificationList);
        }

            public PotterClassification StepCaseSet(out PotterClassification extracted)
        {
            var extractedClassificationList = new List<ClassificationItem>()
            {
                new ClassificationItem("111"),
                new ClassificationItem("222"),
                new ClassificationItem("333"),
                new ClassificationItem("444"),
                new ClassificationItem("555")
            };

            for (int i = 0; i < 5; i++)
            {
                if (classification[i].Count > 1)
                {
                    classification[i].IncrementCount(-1);
                    extractedClassificationList[i].IncrementCount();
                }
            }

            extracted = new PotterClassification(extractedClassificationList);

            return this;
        }
    }
}
