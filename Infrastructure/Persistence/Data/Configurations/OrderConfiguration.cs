using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(O=>O.Subtotal)
                   .HasColumnType("decimal(8,2)");

            builder.HasMany(O => O.Items)
                   .WithOne();

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(O => O.DeliveryMethodId);

            builder.OwnsOne(O => O.Address);
        }
    }
}
