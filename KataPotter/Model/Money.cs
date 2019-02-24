using System;

namespace KataPotter.Model
{
    public class Money : IEquatable<Money>
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
            if (a is null && b is null) return true;
            if (a is null && !(b is null)) return false;
            if (!(a is null) && b is null) return false;

            return a.amount == b.amount;
        }

        public static bool operator !=(Money a, Money b)
        {
            if (a is null && b is null) return false;
            if (a is null && !(b is null)) return true;
            if (!(a is null) && b is null) return true;

            return a.amount != b.amount;
        }

        public Money ApplyDiscount(decimal discount)
        {
            return new Money(amount * discount);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money other)
        {
            return other != null && this == other;
        }

        public override int GetHashCode()
        {
            return -1658239311 + amount.GetHashCode();
        }
    }
}
