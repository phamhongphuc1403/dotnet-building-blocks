using BuildingBlock.Core.Application;
using BuildingBlock.Infrastructure.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;
    private readonly IMediator _mediator;

    protected BaseDbContext(DbContextOptions options, ICurrentUser currentUser, IMediator mediator) : base(options)
    {
        _currentUser = currentUser;
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}