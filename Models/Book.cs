using System;
using System.Collections.Generic;

#nullable disable

namespace userManagement_API.Models
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
