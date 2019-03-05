using KataPotter.Model;

namespace KataPotter.Domain
{
    public interface IClassifier
    {
        bool AddBook(Book book);

        Money GetBestPrice();
    }
}
