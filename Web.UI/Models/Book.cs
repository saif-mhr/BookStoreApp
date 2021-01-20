using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.UI.Models
{
    public class Book
    {
        public long Id { get; set; }

        [Required ]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        public int Isbn { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }
        public decimal? Price { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Remarks { get; set; }
    }
}
