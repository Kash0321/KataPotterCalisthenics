using KataPotter.Model;

namespace KataPotter.Domain
{
    public interface IClassifier
    {
        bool AddItem(Book book);

        Money GetBestPrice();
    }
}
