using ClientHub.Domain.Entities.Addresses;

namespace ClientHub.Infrastructure.Repositories;

internal sealed class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}