using ClientHub.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientHub.Infrastructure.Configurations;

internal sealed class ClientsConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");

        builder.HasKey(client => client.Id);

        builder.Property(client => client.Name)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(client => client.Email)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(client => client.Logo)
            .HasConversion(logo => logo.Value, value => new Logo(value));
    }
}