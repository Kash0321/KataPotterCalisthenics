using System;
using System.Collections.Generic;

namespace KataPotter.Model
{
    public class ISBN : IEquatable<ISBN>
    {
        readonly string isbn = string.Empty;

        public ISBN(string isbn)
        {
            this.isbn = isbn;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ISBN);
        }

        public bool Equals(ISBN other)
        {
            return other != null && this == other;
        }

        public override int GetHashCode()
        {
            return 1553653001 + EqualityComparer<string>.Default.GetHashCode(isbn);
        }

        public static bool operator ==(ISBN a, ISBN b)
        {
            if (a is null && b is null) return true;
            if (a is null && !(b is null)) return false;
            if (!(a is null) && b is null) return false;

            return a.isbn == b.isbn;
        }

        public static bool operator !=(ISBN a, ISBN b)
        {
            if (a is null && b is null) return false;
            if (a is null && !(b is null)) return true;
            if (!(a is null) && b is null) return true;

            return a.isbn != b.isbn;
        }
    }
}
