using RetailPulseAI.Application.Abstractions;
using RetailPulseAI.Persistence.Context;

namespace RetailPulseAI.Persistence.Repositories;

public sealed class UnitOfWork(RetailPulseDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
