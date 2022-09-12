using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoolTools.Pool.API.Domain.Entities;
using PoolTools.Pool.API.Domain.Entities.TypeConverters;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Persistence;
public class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext()
    {

    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
    }

    public DbSet<Pool> Pools => Set<Pool>();
    public DbSet<PoolTeam> PoolTeams => Set<PoolTeam>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasSequence(Constants.PoolSequenceName);
        builder.HasSequence(Constants.PoolTeamSequenceName);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        base.OnModelCreating(builder);

        AddStronglyTypedIdConversions(builder);
    }

    private static void AddStronglyTypedIdConversions(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (StronglyTypedIdHelper.IsStronglyTypedId(property.ClrType, out var valueType))
                {
                    var converter = StronglyTypedIdConverters.GetOrAdd(
                        property.ClrType,
                        _ => CreateStronglyTypedIdConverter(property.ClrType, valueType));
                    property.SetValueConverter(converter);
                }
            }
        }
    }

    private static readonly ConcurrentDictionary<Type, ValueConverter> StronglyTypedIdConverters = new();

    private static ValueConverter CreateStronglyTypedIdConverter(
        Type stronglyTypedIdType,
        Type valueType)
    {
        // id => id.Value
        var toProviderFuncType = typeof(Func<,>)
            .MakeGenericType(stronglyTypedIdType, valueType);
        var stronglyTypedIdParam = Expression.Parameter(stronglyTypedIdType, "id");
        var toProviderExpression = Expression.Lambda(
            toProviderFuncType,
            Expression.Property(stronglyTypedIdParam, "Value"),
            stronglyTypedIdParam);

        // value => new ProductId(value)
        var fromProviderFuncType = typeof(Func<,>)
            .MakeGenericType(valueType, stronglyTypedIdType);
        var valueParam = Expression.Parameter(valueType, "value");
        var ctor = stronglyTypedIdType.GetConstructor(new[] { valueType });
        var fromProviderExpression = Expression.Lambda(
            fromProviderFuncType,
            Expression.New(ctor, valueParam),
            valueParam);

        var converterType = typeof(ValueConverter<,>)
            .MakeGenericType(stronglyTypedIdType, valueType);

        return (ValueConverter)Activator.CreateInstance(
            converterType,
            toProviderExpression,
            fromProviderExpression,
            null);
    }

    Task IApplicationContext.SaveChanges(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
