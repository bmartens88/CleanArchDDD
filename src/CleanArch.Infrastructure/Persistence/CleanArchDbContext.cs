using CleanArch.Domain.Interfaces;
using CleanArch.Infrastructure.Events.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Persistence;

/// <summary>
/// Database Context class for database interaction.
/// </summary>
internal sealed class CleanArchDbContext : DbContext
{
    /// <summary>
    /// Dispatcher for publishing Domain Event(s).
    /// </summary>
    private readonly IEventDispatcher? _dispatcher;

    /// <summary>
    /// Constructor of the <see cref="CleanArchDbContext"/> class.
    /// </summary>
    /// <param name="options"><see cref="DbContextOptions{TContext}"/> instance used for configuration.</param>
    /// <param name="dispatcher"><see cref="IEventDispatcher"/> instance for publishing Domain Event(s).</param>
    /// <seealso cref="DbContextOptions{TContext}"/>
    /// <seealso cref="IEventDispatcher"/>
    public CleanArchDbContext(DbContextOptions<CleanArchDbContext> options, IEventDispatcher? dispatcher = null)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        if (_dispatcher is null) return result;

        var entitiesWithEvents = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    /// <inheritdoc />
    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}