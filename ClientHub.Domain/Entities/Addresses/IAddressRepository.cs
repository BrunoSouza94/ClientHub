namespace ClientHub.Domain.Entities.Addresses;

public interface IAddressRepository
{
    Task AddAsync(Address address);
    void Update(Address address);
    Task RemoveAsync(Guid id);
}