using Books.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models.DataLayer
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
          : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            //set primary keys
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });

            //Many-to-many is broken down into two one-to-many relationship
            //set foreign keys
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book)
              .WithMany(b => b.BookAuthors)
              .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author)
              .WithMany(b => b.BookAuthors)
              .HasForeignKey(ba => ba.AuthorId);


            // Cascading delete in genres set to be off. So, in order to delete a genre first it needs to delete 
            //all the books that belong to that genre. With that we avoid to have orphan books that will not 
            //belong to any genre
            //remove cascading delete with genre
            modelBuilder.Entity<Book>().HasOne(b => b.Genre)
              .WithMany(g => g.Books)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
