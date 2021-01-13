using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvPShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Configurations
{
    public class ComboTranslationConfiguration : IEntityTypeConfiguration<ComboTranslation>
    {
        public void Configure(EntityTypeBuilder<ComboTranslation> builder)
        {
            builder.ToTable("ComboTranslation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Combo).WithMany(x => x.ComboTranslations).HasForeignKey(x => x.IdCombo);
            builder.HasOne(x => x.Language).WithMany(x => x.ComboTranslations).HasForeignKey(x => x.IdLanguage);
        }
    }
}
