using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotesProject_ASP.Models
{
    public class Author
    {
        public int ID { get; set; }

        [RegularExpression("[żźćńółęąśŻŹĆĄŚĘŁÓŃA-Za-z ]+", ErrorMessage = "First name must contains only letters")]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [RegularExpression("[żźćńółęąśŻŹĆĄŚĘŁÓŃA-Za-z ]+", ErrorMessage = "Last name must contains only letters")]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [RegularExpression("[żźćńółęąśŻŹĆĄŚĘŁÓŃA-Za-z ]+", ErrorMessage = "Country must contains only letters")]
        public string Country { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
    }
}