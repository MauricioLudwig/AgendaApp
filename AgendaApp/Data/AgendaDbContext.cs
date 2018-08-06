using AgendaApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.Data
{
    public class AgendaDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(o => o.Alias)
                .HasDefaultValue("Anonymous");

            builder.Entity<Agenda>()
                .Property(o => o.Archived)
                .HasDefaultValue(false);

            builder.Entity<Agenda>()
                .Property(o => o.Title)
                .HasDefaultValue("Agenda");

            builder.Entity<Item>()
                .Property(o => o.Completed)
                .HasDefaultValue(false);
        }

    }
}