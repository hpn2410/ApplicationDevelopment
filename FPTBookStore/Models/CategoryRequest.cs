using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FPTBookStore.Models
{
    public class CategoryRequest
    {
        public int Id { get; set; }

        [StringLength(128, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}