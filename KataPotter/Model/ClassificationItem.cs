namespace KataPotter.Model
{
    internal class ClassificationItem
    {
        public ISBN ISBN { get; private set; }
        public int Count { get; private set; }

        public ClassificationItem(string ISBNCode, int count = 0)
        {
            ISBN = new ISBN(ISBNCode);
            Count = count;
        }

        public void IncrementCount(int increment = 1)
        {
            Count += increment;
        }

        public override string ToString()
        {
            return $"ISBN: {ISBN}, Count: {Count}";
        }
    }
}
