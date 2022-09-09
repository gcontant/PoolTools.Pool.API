using PoolTools.Pool.API.Domain.Entities.TypeConverters;
using System.ComponentModel;

namespace PoolTools.Pool.API.Domain.Common;

[TypeConverter(typeof(StronglyTypedIdConverter))]
public abstract record StronglyTypedId<TValue>(TValue Value) where TValue : notnull
{
#pragma warning disable CS8603 // Possible null reference return.
    public sealed override string ToString() => Value.ToString();
#pragma warning restore CS8603 // Possible null reference return.
}
