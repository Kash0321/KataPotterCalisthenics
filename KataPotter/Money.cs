namespace KataPotter
{
    public class Money
    {
        readonly decimal amount;

        public Money(decimal amount = 0)
        {
            this.amount = amount;
        }

        public static Money Zero()
        {
            return new Money(0);
        }

        public static Money operator +(Money a, Money b)
        {
            return new Money(a.amount + b.amount);
        }

        public static Money operator *(Money a, int b)
        {
            return new Money(a.amount * b);
        }

        public static bool operator ==(Money a, Money b)
        {
            return a.amount == b.amount;
        }

        public static bool operator !=(Money a, Money b)
        {
            return a.amount != b.amount;
        }

        public Money ApplyDiscount(decimal discount)
        {
            return new Money(amount * discount);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == ((Money)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
