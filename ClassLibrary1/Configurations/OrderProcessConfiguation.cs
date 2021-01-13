using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvPShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Configurations
{
    class OrderProcessConfiguation : IEntityTypeConfiguration<OrderProcess>
    {
        public void Configure(EntityTypeBuilder<OrderProcess> builder)
        {
            builder.ToTable("OrderProcess");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Order).WithMany(x => x.OrderProcesses).HasForeignKey(x => x.IdOrder);

        }
    }
}
