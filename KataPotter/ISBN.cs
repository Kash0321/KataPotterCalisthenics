namespace KataPotter
{
    public class ISBN
    {
        readonly string isbn = string.Empty;

        public ISBN(string isbn)
        {
            this.isbn = isbn;
        }

        public static bool operator ==(ISBN a, ISBN b)
        {
            return a.isbn == b.isbn;
        }

        public static bool operator !=(ISBN a, ISBN b)
        {
            return a.isbn != b.isbn;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == (ISBN)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
