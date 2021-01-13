using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvPShop.Data.Entities;
using System;

namespace NvPShop.Data.Configurations
{
    class LikesProductConfiguraton : IEntityTypeConfiguration<LikesProduct>
    {
        public void Configure(EntityTypeBuilder<LikesProduct> builder)
        {
            builder.ToTable("LikesProduct");

            builder.HasKey(x => new {x.IdProduct,x.IdUser });

            builder.HasOne(x => x.AppUser).WithMany(x => x.LikesProducts).HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Product).WithMany(x => x.LikesProducts).HasForeignKey(x => x.IdProduct);
        }
    }
}
