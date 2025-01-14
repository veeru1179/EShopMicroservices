﻿
namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }

        private CustomerId(Guid value) => Value = value;

        public static CustomerId Of(Guid value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value.ToString());
            if (value == Guid.Empty)
            {
                throw new DomainException("CustomerId Cannot be Empty!");
            }

            return new CustomerId(value);
        }
    }
}
