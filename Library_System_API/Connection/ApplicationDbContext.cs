using Library_System_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Library_System_API.Connection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData
                (
                    new Genre { GenreId = 1, Name = "Action", }
                     
                );
            modelBuilder.Entity<Book>().HasData
                (
                    new Book { BookId=1,Title="Book1",PublishedYear=new DateTime(2024,1,1)},
                    new Book { BookId = 2, Title = "Book2", PublishedYear = new DateTime(2024, 1, 1) }

                );
            modelBuilder.Entity<Author>().HasData
                (
                    new Author { AuthorId=1,AuthorName="Author1",Email="Author1@gmail.com",Phone="01224447482"},
                    new Author { AuthorId = 2, AuthorName = "Author2", Email = "Author2@gmail.com", Phone = "01224447482" }

                );

            modelBuilder.Entity<Book>()
                .HasMany(x => x.Genres)
                .WithMany(x => x.Books)
                .UsingEntity(x => x.HasData(

                    new { BooksBookId = 1, GenresGenreId = 1 },
                    new { BooksBookId = 2, GenresGenreId = 1 }
                ));

            modelBuilder.Entity<Book>()
                .HasMany(x => x.Authors)
                .WithMany(x => x.Books)
                .UsingEntity(x => x.HasData(

                    new { AuthorsAuthorId = 1, BooksBookId = 1 },
                    new { AuthorsAuthorId = 2, BooksBookId = 2 }

                    ));






            base.OnModelCreating(modelBuilder);
        }
    }
}



//modelBuilder.Entity<Book>()
//    .HasMany(x => x.Genres)
//    .WithMany(x => x.Books)
//    .UsingEntity(x => x.HasData(

//        new { BooksBookId = 1, GenresGenreId = 1 },
//        new { BooksBookId = 2, GenresGenreId = 1 }
//    ));

//modelBuilder.Entity<Book>()
//    .HasMany(x => x.Authors)
//    .WithMany(x => x.Books)
//    .UsingEntity(x => x.HasData(

//        new { AuthorsAuthorId = 1, BooksBookId = 1 },
//        new { AuthorsAuthorId = 1, BooksBookId = 1 }

//        ));