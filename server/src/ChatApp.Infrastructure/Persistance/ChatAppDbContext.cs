using ChatApp.Core.Entities;
using ChatApp.Infrastructure.Persistance.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Persistance
{
    public class ChatAppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public string DbPath { get; }
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppRoleConfiguration());
        }
        public DbSet<ChatMessage> chatMessages { get; set; }
        public DbSet<DirectMessage> directMessages { get; set; }
    }
}
