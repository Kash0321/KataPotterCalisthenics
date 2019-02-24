namespace KataPotter
{
    public class Book
    {
        readonly ISBN ISBN;

        public Book(string ISBNCode)
        {
            ISBN = new ISBN(ISBNCode);
        }

        public bool IsMyISBN(ISBN ISBN)
        {
            return this.ISBN == ISBN;
        }
    }
}
