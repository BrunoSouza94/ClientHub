using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Clients;
using ClientHub.Domain.Entities.Clients.Events;
using System.Runtime.Serialization;

namespace ClientHub.Domain.Entities.Addresses;

public sealed class Address : Entity
{
    public Address(Guid id, Thoroughfare thoroughfare, LocationNumber locationNumber, Neighborhood neighborhood, City city, State state, Guid clientId)
        : base(id)
    {
        Thoroughfare = thoroughfare;
        LocationNumber = locationNumber;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ClientId = clientId;
    }

    public Address()
    {
    }

    public Thoroughfare Thoroughfare { get; private set; }

    public LocationNumber LocationNumber { get; private set; }

    public Neighborhood Neighborhood { get; private set; }

    public City City { get; private set; }

    public State State { get; private set; }

    public Guid ClientId { get; private set; }

    [IgnoreDataMember]
    public Client Client { get; }

    public static Address Create(Thoroughfare thoroughfare, LocationNumber locationNumber, Neighborhood neighborhood, City city, State state, Guid clientId)
    {
        Address address = new(Guid.NewGuid(), thoroughfare, locationNumber, neighborhood, city, state, clientId);

        address.RaiseDomainEvent(new AddressCreatedDomainEvent(address.Id));

        return address;
    }
}