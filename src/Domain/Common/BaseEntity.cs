﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PoolTools.Pool.API.Domain.Common;

public class BaseEntity<T,TId> where T : StronglyTypedId<TId> where TId : notnull
{
    public T Id { get; set; } = default!;
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}