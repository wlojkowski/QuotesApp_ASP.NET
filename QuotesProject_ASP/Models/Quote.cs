using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotesProject_ASP.Models
{
    public enum QuoteCategory
    {
        Religion, Life, War,Happiness
    }

    public class Quote
    {
        public int ID { get; set; }

        public int AuthorID { get; set; }
          
        [StringLength(300, MinimumLength = 7, ErrorMessage = "Content of quote must have 7 - 300 characters")]
        [RegularExpression("[,.żźćńółęąśŻŹĆĄŚĘŁÓŃA-Za-z ]+",ErrorMessage ="Content must contains only letters")]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [RegularExpression("[,.żźćńółęąśŻŹĆĄŚĘŁÓŃA-Za-z ]+", ErrorMessage = "Source must contains only letters")]
        public string Source { get; set; }

        public QuoteCategory Category { get; set; }

        public virtual Author Author { get; set; }
    }
}