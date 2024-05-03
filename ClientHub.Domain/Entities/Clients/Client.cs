using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Addresses;
using ClientHub.Domain.Entities.Clients.Events;

namespace ClientHub.Domain.Entities.Clients;

public sealed class Client : Entity
{
    public Client(Guid id, Name name, Email email, Logo logo) 
        : base(id)
    {
        Name = name;
        Email = email;
        Logo = logo;
    }

    public Client()
    {
    }

    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Logo Logo { get; private set; }

    public List<Address> Addresses { get; private set; }

    public static Client Create(Name name,  Email email, Logo logo)
    {
        Client client = new(Guid.NewGuid(), name, email, logo);

        client.RaiseDomainEvent(new ClientCreatedDomainEvent(client.Id));

        return client;
    }
}