using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public virtual ApplicationUser Buyer { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal TotalPrice { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        [Required]
        public DateTime CreatedDateTime { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}