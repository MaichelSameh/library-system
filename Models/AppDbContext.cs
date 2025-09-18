using library_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace library_system.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Pubblisher> Pubblishers { get; set; }
        public DbSet<Tipology> Tipologys { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrow> Borrows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Tipology)
                .WithMany(t => t.Books)
                .HasForeignKey(b => b.TipologyId)
                 ; // Optional    // FK in Employee table

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Authors)
                .WithMany(t => t.Books)
                .HasForeignKey(e => e.AuthorId)
                ;

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Pubblisher)
                .WithMany(t => t.Books)
                .HasForeignKey(e => e.PubblisherId)
                ;

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Client)
                .WithMany(t => t.borrows)
                .HasForeignKey(e => e.ClientId)
                ;

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.book)
                .WithMany(t => t.borrows)
                .HasForeignKey(e => e.BookId)
               ;
            
            
            modelBuilder.Entity<Author>()
                .HasOne(b => b.Creator)
                .WithMany(t => t.Authors)
                .HasForeignKey(b => b.CreatedBy)
                ;
            
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Creator)
                .WithMany(t => t.Books)
                .HasForeignKey(b => b.CreatedBy)
                ;
            
            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Creator)
                .WithMany(t => t.Borrows)
                .HasForeignKey(b => b.CreatedBy)
                ;
            
            modelBuilder.Entity<Client>()
                .HasOne(b => b.Creator)
                .WithMany(t => t.Clients)
                .HasForeignKey(b => b.CreatedBy)
                ;
            
            modelBuilder.Entity<Pubblisher>()
                .HasOne(b => b.Creator)
                .WithMany(t => t.Publishers)
                .HasForeignKey(b => b.CreatedBy)
                ;
            
            modelBuilder.Entity<Tipology>()
                .HasOne(b => b.Creator)
                .WithMany(t => t.Tipologies)
                .HasForeignKey(b => b.CreatedBy)
                ;

            base.OnModelCreating(modelBuilder);
        }
    }
}
