using System;
using System.Collections.Generic;
using System.Text;

namespace KataPotter
{
    public interface IClassifier
    {
        bool AddItem(Book book);

        Money GetBestPrice();
    }
}
