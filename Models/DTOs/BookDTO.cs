﻿using Books.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> Authors { get; set; }

        public void Load(Book book)
        {
            BookId = book.BookId;
            Title = book.Title;
            Price = book.Price;
            Authors = new Dictionary<int, string>();

            foreach (BookAuthor ba in book.BookAuthors)
            {
                Authors.Add(ba.AuthorId, ba.Author.FullName);
            }
        }
    }
}
