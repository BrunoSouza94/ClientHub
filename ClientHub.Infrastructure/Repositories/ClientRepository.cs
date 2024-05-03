using ClientHub.Domain.Entities.Clients;

namespace ClientHub.Infrastructure.Repositories;

internal sealed class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}