using Microsoft.EntityFrameworkCore;
using Perevod36.Features.Common;
using Perevod36.Features.Orders;

namespace Perevod36.Data;

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders => Set<Order>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {

        configurationBuilder.Properties<OrderId>()
            .HaveConversion<OrderIdConverter>();

        configurationBuilder.Properties<ClientId>()
            .HaveConversion<ClientIdConverter>();

       
    }


    public class OrderIdConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<OrderId, int>
    {
        public OrderIdConverter() : base(id => id.Id, value => new OrderId(value)) { }
    }
    public class ClientIdConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<ClientId, int>
    {
        public ClientIdConverter() : base(id => id.Id, value => new ClientId(value)) { }
    }


}

