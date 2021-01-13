using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvPShop.Data.Entities;

namespace NvPShop.Data.Configurations
{
    public class ComboProductConfiguration : IEntityTypeConfiguration<ComboProduct>
    {
        public void Configure(EntityTypeBuilder<ComboProduct> builder)
        {
            builder.ToTable("ComboProduct");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Combo).WithMany(x=>x.ComboProducts).HasForeignKey(x => x.IdCombo);
            builder.HasOne(x => x.Product).WithMany(x => x.ComboProducts).HasForeignKey(x => x.IdProduct);
        }
    }
}
