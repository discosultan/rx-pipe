using System;

namespace RxPipe.Lib.Models
{
    /// <summary>
    /// Book entity from Contoso library.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identificator of the book.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; }

        // Rest of the properties...
    }
}