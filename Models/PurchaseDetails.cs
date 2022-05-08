using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCoursework.Models
{
    public class PurchaseDetails
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Purchase")]
        public int PurchaseID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Price { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual Product Product { get; set; }
    }
}
