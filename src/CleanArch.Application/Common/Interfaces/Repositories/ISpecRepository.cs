using Ardalis.Specification;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.Common.Interfaces.Repositories;

/// <summary>
/// Interface which defines the generic specification based repository.
/// </summary>
/// <typeparam name="T">Type of the Entity (or rather Aggregate Root) this repository works on.</typeparam>
public interface ISpecRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}