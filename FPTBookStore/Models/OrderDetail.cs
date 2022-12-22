using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookStore.Models
{
    public class OrderDetail
    {
        [Display(Name = "Purchased Quantity")]
        [Range(1, 1000)]
        [Required]
        public int PurchasedQuantity { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

    }
}