using System;
using System.Collections.Generic;
using System.Text;

namespace KataPotter
{
    internal class ClassificationItem
    {
        readonly string id;
        public int Count { get; protected set; } = 0;

        public ClassificationItem(string id)
        {
            this.id = id;
        }

        public void IncrementCount(int increment = 1)
        {
            Count += increment;
        }

        public ISBN GetISBN()
        {
            return new ISBN(id);
        }
    }
}
