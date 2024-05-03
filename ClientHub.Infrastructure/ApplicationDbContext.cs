using ClientHub.Application.Exceptions;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Addresses;
using ClientHub.Domain.Entities.Clients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClientHub.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Ocorreu uma exceção de concorrência.", ex);
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is not null)
                throw new Exception(ex.InnerException.Message);

            throw;
        }
    }

    private async Task PublishDomainEventAsync()
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
                                        .Select(entry => entry.Entity)
                                        .SelectMany(entity =>
                                        {
                                            var domainEvents = entity.GetDomainEvents();

                                            entity.ClearDomainEvents();

                                            return domainEvents;
                                        })
                                        .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}