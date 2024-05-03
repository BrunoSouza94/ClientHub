using ClientHub.Domain.Entities.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientHub.Infrastructure.Configurations;

internal sealed class AddressesConfigurations : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.HasKey(address => address.Id);

        builder.Property(address => address.Thoroughfare)
            .HasConversion(thoroughfare => thoroughfare.Value, value => new Thoroughfare(value));

        builder.Property(address => address.LocationNumber)
            .HasConversion(locationNumber => locationNumber.Value, value => new LocationNumber(value));

        builder.Property(address => address.Neighborhood)
            .HasConversion(neighborhood => neighborhood.Value, value => new Neighborhood(value));

        builder.Property(address => address.City)
            .HasConversion(city => city.Value, value => new City(value));

        builder.Property(address => address.State)
            .HasConversion(state => state.Value, value => new State(value));
    }
}