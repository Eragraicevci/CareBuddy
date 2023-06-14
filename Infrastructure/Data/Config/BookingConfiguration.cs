using Core.Entities.BookingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.OwnsOne(o => o.Info, a =>
            {
                a.WithOwner();
            });
            builder.Navigation(a => a.Info).IsRequired();
            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (BookingStatus)Enum.Parse(typeof(BookingStatus), o)
                );
            builder.HasMany(o => o.BookingItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}