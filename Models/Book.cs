using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace userManagement_API.Models
{
    public partial class Book
    {
        public int BookId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Category { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Author { get; set; }
        [StringLength(200)]
        public string Publisher { get; set; }
    }
}
