using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvPShop.Data.Entities;
using System;

namespace NvPShop.Data.Configurations
{
    public class CommentProductConfiguration : IEntityTypeConfiguration<CommentProduct>
    {
        public void Configure(EntityTypeBuilder<CommentProduct> builder)
        {
            builder.ToTable("CommentProduct");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.AppUser).WithMany(x => x.CommentProducts).HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.Product).WithMany(x => x.CommentProducts).HasForeignKey(x => x.IdProduct);
        }
    }
}
