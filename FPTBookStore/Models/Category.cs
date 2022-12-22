using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FPTBookStore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(128, MinimumLength = 3)]
        [Display(Name = "Category")]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        [Required]
        public DateTime CreatedDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated at")]
        [Required]
        public DateTime UpdatedDateTime { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}