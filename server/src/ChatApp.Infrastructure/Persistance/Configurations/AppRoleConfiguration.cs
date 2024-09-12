using ChatApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Persistance.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
            new AppRole
            {
                Name = "User",
                NormalizedName = "USER"
            },
            new AppRole
            {
                Name = "Player",
                NormalizedName = "PLAYER"
            },
            new AppRole
            {
                Name = "Helper",
                NormalizedName = "HELPER"
            },
            new AppRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
        }
    }
}
