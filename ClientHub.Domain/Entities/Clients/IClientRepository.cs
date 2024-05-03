namespace ClientHub.Domain.Entities.Clients;

public interface IClientRepository
{
    Task AddAsync(Client Client);
    void Update(Client Client);
    Task RemoveAsync(Guid id);
}