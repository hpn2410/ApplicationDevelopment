using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPTBookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(128, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(128, MinimumLength = 3)]
        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Cover")]
        public string CoverUrl { get; set; }

        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Quantity Left")]
        [Required]
        public int StockedQuantity { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        [Required]
        public DateTime CreatedDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated at")]
        [Required]
        public DateTime UpdatedDateTime { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}