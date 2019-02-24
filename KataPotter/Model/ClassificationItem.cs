namespace KataPotter.Model
{
    internal class ClassificationItem
    {
        public ISBN ISBN { get; private set; }
        public int Count { get; private set; }

        public ClassificationItem(string ISBNCode)
        {
            ISBN = new ISBN(ISBNCode);
            Count = 0;
        }

        public void IncrementCount(int increment = 1)
        {
            Count += increment;
        }
    }
}
