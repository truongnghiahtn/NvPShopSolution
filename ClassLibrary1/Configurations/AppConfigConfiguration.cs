﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvPShop.Data.Entities;

namespace NvPShop.Data.Configurations
{
    public class AppConfigConfiguration : IEntityTypeConfiguration<AppConfig>
    {
        public void Configure(EntityTypeBuilder<AppConfig> builder)
        {
            builder.ToTable("AppConfigs");

            builder.HasKey(x => x.Key);

            builder.Property(x => x.Value).IsRequired(true);
        }
    }
}
