using hey_url_challenge_code_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyUrlChallengeCodeDotnet.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }
        public DbSet<LogUrl> LogUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>(entity =>
               {
                   entity.HasKey(e => e.IdUrl)
                   .HasName("PK_IDURL_02072022");

                   entity.ToTable("URLS");

                   entity.Property(e => e.IdUrl)
                   .HasColumnType("int")
                   .ValueGeneratedOnAdd()
                   .HasColumnName("IDURL");

               });

            modelBuilder.Entity<LogUrl>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                .HasName("PK_LOGURL_02072022");

                entity.ToTable("LOGURL");

                entity.Property(e => e.IdLog)
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .HasColumnName("IDLOG");

            });


        }


    }
}