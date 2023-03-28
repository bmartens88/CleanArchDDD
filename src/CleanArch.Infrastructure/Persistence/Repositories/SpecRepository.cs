using Ardalis.Specification.EntityFrameworkCore;
using CleanArch.Application.Common.Interfaces.Repositories;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementation of <see cref="ISpecRepository{T}"/>, which is a generic specification based repository.
/// </summary>
/// <typeparam name="T">Type of the Entity (Aggregate Root) this repository is used for.</typeparam>
internal sealed class SpecRepository<T> : RepositoryBase<T>, ISpecRepository<T>
    where T : class, IAggregateRoot
{
    /// <summary>
    /// Constructor of the <see cref="SpecRepository{T}"/> class.
    /// </summary>
    /// <param name="dbContext"><see cref="CleanArchDbContext"/> instance for database access.</param>
    public SpecRepository(CleanArchDbContext dbContext)
        : base(dbContext)
    {
    }
}