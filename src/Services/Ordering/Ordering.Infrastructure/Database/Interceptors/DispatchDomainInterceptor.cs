using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Entities.Abstractions;

namespace Ordering.Infrastructure.Database.Interceptors;

public sealed class DispatchDomainInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context is null) return;
        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(x => x.Entity.Events.Any())
            .Select(x => x.Entity).ToList();

        if (aggregates.Count != 0)
        {
            var domainEvents = aggregates.SelectMany(x => x.Events).ToList();
            aggregates.ToList().ForEach(x => x.ClearEvents());
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}