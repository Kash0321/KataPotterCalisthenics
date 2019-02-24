namespace KataPotter.Model
{
    public class Book
    {
        readonly ISBN ISBN;

        public Book(string ISBNCode)
        {
            ISBN = new ISBN(ISBNCode);
        }

        public bool MatchBy(ISBN ISBN)
        {
            return this.ISBN == ISBN;
        }
    }
}
